using UnityEngine;
using InventorySystem;
using SaveSystem;
using NUnit.Framework.Internal;

namespace CharacterSystem
{
    public class Player : Character, IDataPersistence
    {
        public Animator animatorAnimations;
        [SerializeField] Animator animatorFlip;
        bool isMoving;
        bool isMovingBackwards;

        [Header("Interact Settings")]
        [SerializeField] float detectionRadius = 3f;
        [SerializeField] LayerMask interactableLayer;
        [SerializeField] LayerMask obstructionLayer;
        [SerializeField] LayerMask playerLayer;
        public KeyCode input = KeyCode.E;

        GameObject currentTarget; // alvo visivel mais proximo

        private void Start()
        {
            if (animatorFlip == null) { animatorFlip = GetComponent<Animator>(); }
            if (animatorAnimations == null) { animatorAnimations = GetComponentInChildren<Animator>(); }
            if (sprite == null) { sprite = GetComponentInChildren<SpriteRenderer>(); }
        }

        private void Update()
        {
            CharacterMove();
            DetectObjects();

            if (UnityEngine.Input.GetKeyDown(input) == true) { animatorAnimations.SetTrigger("isInteracting"); }
        }

        #region Move
        public override void CharacterMove()
        {
            // capturando o input continuo (valores entre -1 e 1)
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            // Debug.Log("X: " + x + " | Z: " + z);

            moveDirection = new Vector3(x, 0f, z).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // movendo-se
            isMoving = moveDirection.magnitude > 0;
            animatorAnimations.SetBool("isMoving", isMoving);

            // flip em x, para andar direita e esquerda
            if (sprite.flipX == false && moveDirection.x < 0) { animatorFlip.SetTrigger("Flip"); sprite.flipX = true; }
            else if (sprite.flipX == true && moveDirection.x > 0) { animatorFlip.SetTrigger("Flip"); sprite.flipX = false; }

            // movendo-se de costas
            /* if (isMovingBackwards == false && moveDirection.y > 0) { isMovingBackwards = true; animatorFlip.SetTrigger("Flip"); }
            else if(isMovingBackwards == true && moveDirection.y < 0) { isMovingBackwards = false; animatorFlip.SetTrigger("Flip"); }
            animatorAnimations.SetBool("isMovingBackwards", isMovingBackwards);*/
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
                }
                else if (currentTarget.GetComponent<NPC>() == true)
                {
                    // npc talk
                }
            }
        }

        private void HighlightTarget(GameObject newTarget)
        {
            // removendo destaque anterior
            if (currentTarget != null)
            {
                Outline oldOutline = currentTarget.GetComponent<Outline>();
                if (oldOutline != null) oldOutline.OutlineWidth = 0;
            }

            currentTarget = newTarget;
            if (currentTarget != null)
            {
                Outline newOutline = currentTarget.GetComponent<Outline>();
                if (newOutline != null) newOutline.OutlineWidth = 7;
            }
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