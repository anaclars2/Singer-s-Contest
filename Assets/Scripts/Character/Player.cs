using UnityEngine;
using InventorySystem;
using SaveSystem;
using NUnit.Framework.Internal;
using UISystem;

namespace CharacterSystem
{
    public class Player : MonoBehaviour, IDataPersistence
    {
        bool isMoving;

        [Header("Move Settings")]
        [SerializeField] protected float moveSpeed = 5f;
        protected Vector3 moveDirection;
        [SerializeField] Camera _camera;

        [Header("Interact Settings")]
        [SerializeField] float detectionRadius = 3f;
        [SerializeField] LayerMask interactableLayer;
        [SerializeField] LayerMask obstructionLayer;
        [SerializeField] LayerMask playerLayer;
        public KeyCode input = KeyCode.E;

        GameObject currentTarget; // alvo visivel mais proximo

        private void Update()
        {
            if (UIManager.instance.pauseActive == false) { CharacterMove(); }
            DetectObjects();

            if (UnityEngine.Input.GetKeyDown(input))
            {
                Interact();
            }
        }

        #region Move
        public void CharacterMove()
        {
            // capturando o input continuo (valores entre -1 e 1)
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            // Debug.Log("X: " + x + " | Z: " + z);

            moveDirection = new Vector3(x, 0f, z).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // movendo-se
            isMoving = moveDirection.magnitude > 0;
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
                    if (((1 << hit.collider.gameObject.layer) & playerLayer) != 0) { Debug.Log("Player is blocking the view to " + collider.name); continue; ; }
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
                    InventoryManager.instance.AddItem(item);
                    item.collected = true;

                    item.RemoveFromScene();

                    ItemPopup.instance.ShowItem(item._name, item.thought);
                }
<<<<<<< HEAD
                else if (currentTarget.GetComponent<NPC>() == true)
                {
                    NPC npc = currentTarget.GetComponent <NPC>();
                    npc.Interact();
                }
               
=======
>>>>>>> main
            }
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