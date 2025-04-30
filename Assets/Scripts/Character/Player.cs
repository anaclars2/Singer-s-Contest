using UnityEngine;
using InventorySystem;
using UISystem;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System.Collections;
using SaveSystem;

namespace CharacterSystem
{
    public class Player : MonoBehaviour, IDataPersistence
    {
        bool isMoving;

        [Header("Move Settings")]
        [SerializeField] protected float moveSpeed = 5f;
        protected Vector3 moveDirection;
        [SerializeField] CharacterController controller;

        [Header("Camera Settings")]
        [SerializeField] Camera _camera;
        [SerializeField] float mouseSensitivity = 100f;
        private float xRotation = 0f;

        [Header("Interact Settings")]
        [SerializeField] float detectionRadius = 3f;
        [SerializeField] LayerMask interactableLayer;
        [SerializeField] LayerMask obstructionLayer;
        [SerializeField] LayerMask playerLayer;
        public KeyCode input = KeyCode.E;
        [SerializeField] GameObject ideaArea;

        GameObject currentTarget; // alvo visivel mais proximo
        SCENES scene;

        private void Start()
        {
            if (ideaArea != null) { ideaArea.SetActive(false); }
        }

        private void Update()
        {
            if (UIManager.instance.pauseActive == false)
            {
                CharacterMove();
                CharacterLook(); // rotacao da camera
            }
            DetectObjects();

            if (UnityEngine.Input.GetKeyDown(input)) { Interact(); }
        }
        #region Move
        public void CharacterMove()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 forward = _camera.transform.forward;
            Vector3 right = _camera.transform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            moveDirection = (forward * z + right * x).normalized * moveSpeed;
            controller.Move(moveDirection * Time.deltaTime);

            isMoving = moveDirection.magnitude > 0;
        }



        void CharacterLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            _camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            transform.Rotate(Vector3.up * mouseX);
        }


        #endregion

        #region Interact
        private void DetectObjects()
        {
            // deteccao em formato de esfera
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, interactableLayer);

            GameObject closest = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider collider in hitColliders)
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance, interactableLayer | obstructionLayer))
                {
                    // se o que estiver na frente for o jogador
                    // nao destaca o objeto
                    if (hit.collider.gameObject == collider.gameObject)
                    {
                        // Debug.Log("Interactive object detected: " + collider.name);
                        if (distance < closestDistance)
                        {
                            closest = collider.gameObject;
                            closestDistance = distance;
                        }
                    }
                    else { Debug.Log(hit.collider.gameObject.name + " is blocking the view to " + collider.name); }

                    Debug.DrawRay(transform.position, direction * distance, Color.red);
                }
            }

            HighlightTarget(closest);
        }

        public void Interact()
        {
            if (currentTarget != null)
            {
                // Debug.Log("Interacting with: " + currentTarget.name);
                if (currentTarget.GetComponent<Item>() == true)
                {
                    Item item = currentTarget.GetComponent<Item>();
                    if (item.isCollectible == true) { InventoryManager.instance.AddItem(item); }
                    item.collected = true;
                    InventoryManager.instance.evidences.Add(item.evidenceType);

                    if (ideaArea != null)
                    {
                        ideaArea.SetActive(true);
                        TMP_Text text = ideaArea.GetComponentInChildren<TMP_Text>();
                        text.text = item.idea;

                        Invoke("DeactivateIdeaArea", 5f);
                    }

                    string group = item.group;
                    List<Item> items = FindAllItemObjects();
                    List<Item> itemsGroup = ItemsByGroup(items, group);
                    bool groupIsEnded = InventoryHasAllItems(itemsGroup);

                    item.RemoveFromScene();

                    if (groupIsEnded == true)
                    {
                        scene = SCENES.None;
                        switch (group)
                        {
                            // AQUI VAI FICAR A LOGICA DE CADA FALA
                            // NAO PRECISA SER CENARIO, SE NAO FOR VC DEVE REORGANIZAR
                            case "A": scene = SCENES.Menu; break;
                            case "B": scene = SCENES.None; break;
                            case "C": scene = SCENES.None; break;
                            case "D": scene = SCENES.None; break;
                            case "E": scene = SCENES.None; break;
                            case "F": scene = SCENES.None; break;
                        }

                        if (ideaArea.activeInHierarchy == false)
                        {
                            // NAO PRECISA SER CENARIO, SE NAO FOR VC DEVE REORGANIZAR
                            GameManager.instance.sceneToLoad = scene;
                            GameManager.instance.LoadSceneWithTransition(TRANSITION.CrossFade);
                        }
                        else
                        {
                            StartCoroutine(WaitAndLoadScene(3f));
                        }
                    }
                }
            }
        }

        private void DeactivateIdeaArea() { ideaArea.SetActive(false); }

        IEnumerator WaitAndLoadScene(float delay)
        {
            // NAO PRECISA SER CENARIO, SE NAO FOR VC DEVE REORGANIZAR
            yield return new WaitForSeconds(delay);
            GameManager.instance.sceneToLoad = scene;
            GameManager.instance.LoadSceneWithTransition(TRANSITION.CrossFade);
        }

        private List<Item> FindAllItemObjects()
        {
            IEnumerable<Item> _dataPersistenceObjects = Resources.FindObjectsOfTypeAll<MonoBehaviour>()
       .Where(mb => mb.hideFlags == HideFlags.None && mb.gameObject.scene.IsValid()) // ignora Prefabs e Assets
       .OfType<Item>();

            return new List<Item>(_dataPersistenceObjects);
        }

        private bool InventoryHasAllItems(List<Item> itemsGroup)
        {
            for (int i = 0; i < itemsGroup.Count; i++)
            {
                bool b = InventoryManager.instance.CheckIfContains(itemsGroup[i].evidenceType);
                if (b == false) { return false; }
                else { continue; }
            }
            return true;
        }

        private List<Item> ItemsByGroup(List<Item> items, string group)
        {
            List<Item> itemsGroup = new List<Item>();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].group == group) { itemsGroup.Add(items[i]); }
            }
            return itemsGroup;
        }

        private void HighlightTarget(GameObject newTarget)
        {
            // removendo destaque anterior

            currentTarget = newTarget;

            // add
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
        #endregion

        #region SaveData
        public void LoadData(GameData data) { transform.position = data.playerPosition; }

        public void SaveData(GameData data) { data.playerPosition = transform.position; }
        #endregion
    }
}