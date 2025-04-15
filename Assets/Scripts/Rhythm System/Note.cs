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
        [SerializeField] GameObject spriteEnd;

        private void Start()
        {
            timeInstantiated = RhythmManager.GetAudioSourceTime();
            GetComponent<SpriteRenderer>().enabled = true;

            if (isLong == true)
            {
                spriteEnd.SetActive(true);
                RotationSpriteEnd();
            }
            else { spriteEnd.SetActive(false); }
        }

        private void RotationSpriteEnd()
        {
            float holdLength = noteSpeed * duration;
            Vector3 endOffset = Vector3.zero;
            Vector3 rot = transform.eulerAngles;

            // direcao baseada na rotacao
            // respectivamente, up arrow, down arrow e right/left arrow
            if (Mathf.Approximately(rot.z, 90)) { endOffset = new Vector3(holdLength, 0, 0); }
            else if (Mathf.Approximately(rot.z, 270)) { endOffset = new Vector3(-holdLength, 0, 0); }
            else { endOffset = new Vector3(0, holdLength, 0); }

            spriteEnd.transform.localPosition = endOffset;
        }

        private void Update()
        {
            // posicionando as notas com base no 'tempo de instanciacao' e 'quando deve ser tocada'
            double timeSinceInstantiated = RhythmManager.GetAudioSourceTime() - timeInstantiated;

            // calculando a nova posicao com base na velocidade constante
            float yOffset = (float)timeSinceInstantiated * noteSpeed;
            Vector3 vector = Vector3.up * (RhythmManager.instance.noteSpawnY - yOffset);
            transform.localPosition = vector;

            if (isLong == true && spriteEnd != null)
            {
                float holdLength = noteSpeed * duration;
                RotationSpriteEnd();
            }
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