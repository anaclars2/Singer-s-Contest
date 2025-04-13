using UnityEngine;

namespace RhythmSystem
{
    public class Note : MonoBehaviour
    {
        [HideInInspector] public Lane lane;

        [Header("Hit Settings")]
        double timeInstantiated;
        [HideInInspector] public float assignedTime; // quando ela deve ser tocada pelo jogador
        public bool canBePressed = false;

        [Header("Debug")]
        public float colliderPosition;
        [SerializeField] float position;

        private void Start()
        {
            timeInstantiated = RhythmManager.GetAudioSourceTime();
            GetComponent<SpriteRenderer>().enabled = true;
        }

        private void Update()
        {
            // posicionando as notas com base no 'tempo de instanciacao' e 'quando deve ser tocada'
            double timeSinceInstantiated = RhythmManager.GetAudioSourceTime() - timeInstantiated;
            float t = (float)(timeSinceInstantiated / (RhythmManager.instance.noteTime * 2)); // descobrindo a posicao

            Vector3 vector = Vector3.Lerp(Vector3.up * RhythmManager.instance.noteSpawnY, Vector3.up * RhythmManager.instance.noteDespawnY, t);
            transform.localPosition = vector;
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