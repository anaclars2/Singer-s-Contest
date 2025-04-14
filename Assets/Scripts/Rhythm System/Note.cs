using UnityEngine;

namespace RhythmSystem
{
    public class Note : MonoBehaviour
    {
        [HideInInspector] public Lane lane;
        public float noteSpeed { get { return (RhythmManager.instance.noteSpawnY - RhythmManager.instance.noteTapY) / RhythmManager.instance.noteTime; } }

        [Header("Hit Settings")]
        double timeInstantiated;
        [HideInInspector] public float assignedTime; // quando ela deve ser tocada pelo jogador
        public bool canBePressed = false;

        public float colliderPosition;
        float position;

        [Header("Long Notes Settings")]
        public bool isLong = false;
        public float duration = 0; // apenas para notas longas
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] GameObject endedNote;

        private void Start()
        {
            timeInstantiated = RhythmManager.GetAudioSourceTime();
            GetComponent<SpriteRenderer>().enabled = true;
            /*lineRenderer = GetComponent<LineRenderer>();
            endedNote = GetComponentInChildren<GameObject>();

            if (isLong == true)
            {
                endedNote.SetActive(true);
                lineRenderer.enabled = true;
                DrawLineRenderer();
            }
            else
            {
                endedNote.SetActive(false);
                lineRenderer.enabled = false;
            }*/
        }

        private void Update()
        {
            // posicionando as notas com base no 'tempo de instanciacao' e 'quando deve ser tocada'
            double timeSinceInstantiated = RhythmManager.GetAudioSourceTime() - timeInstantiated;

            // calculando a nova posicao com base na velocidade constante
            float yOffset = (float)timeSinceInstantiated * noteSpeed;
            Vector3 vector = Vector3.up * (RhythmManager.instance.noteSpawnY - yOffset);
            transform.localPosition = vector;
        }

        private void DrawLineRenderer()
        {
            lineRenderer.SetPosition(0, transform.position); // comeco do lineRenderer

            // fim da nota longa
            float holdLength = noteSpeed * duration;
            Vector3 endPosition = transform.position + Vector3.down * holdLength;
            lineRenderer.SetPosition(1, endPosition);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            position = transform.position.y;

            if (collision.tag == "Activator")
            {
                var _colliderPosition = collision.bounds.center.y;
                colliderPosition = _colliderPosition;

                canBePressed = true;
            }
            else if (collision.tag == "Destroy") { Destroy(gameObject); }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Activator") { canBePressed = false; }
        }
    }
}