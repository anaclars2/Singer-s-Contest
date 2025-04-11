using UnityEngine;

namespace CharacterSystem
{
    public class Player : Character
    {
        [Header("Interact")]
        [SerializeField] float detectionRadius = 3f;
        [SerializeField] LayerMask interactableLayer;
        [SerializeField] LayerMask obstructionLayer;

        GameObject currentTarget; // alvo visivel mais proximo
        Renderer targetRenderer;
        Color originalColor;

        private void Update()
        {
            Move();
            DetectObjects();

            if (Input.GetKeyDown(KeyCode.E) && currentTarget != null)
            {
                Debug.Log("Interacting with: " + currentTarget.name);
                // currentTarget.GetComponent<SeuScriptInterativo>()?.Interagir();
            }
        }

        public override void Move()
        {
            // capturando o input continuo (valores entre -1 e 1)
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector3(x, y, 0f).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

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
                    if (hit.collider.gameObject == collider.gameObject)
                    {
                        Debug.Log("Interactive object detected: " + collider.name);
                        if (distance < closestDistance)
                        {
                            closest = collider.gameObject;
                            closestDistance = distance;
                        }
                    }
                    else { Debug.Log("Something is blocking the view to " + collider.name); }

                    Debug.DrawRay(transform.position, direction * distance, Color.cyan);
                }
            }

            HighlightTarget(closest);
        }

        private void HighlightTarget(GameObject newTarget) 
        {
            // removendo destaque anterior
            if (currentTarget != null && targetRenderer != null) { targetRenderer.material.color = originalColor; }

            currentTarget = newTarget;
            if (currentTarget != null)
            {
                targetRenderer = currentTarget.GetComponent<Renderer>();
                if (targetRenderer != null)
                {
                    originalColor = targetRenderer.material.color;
                    targetRenderer.material.color = Color.yellow;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}