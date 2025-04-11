using UnityEngine;

namespace RhythmSystem
{
    public class Note : MonoBehaviour
    {
        double timeInstantiated;
        [HideInInspector] public float assignedTime; // quando ela deve ser tocada pelo jogador

        private void Start() { timeInstantiated = RhythmManager.GetAudioSourceTime(); }

        private void Update()
        {
            // posicionando as notas com base no 'tempo de instanciacao' e 'quando deve ser tocada'
            double timeSinceInstantiated = RhythmManager.GetAudioSourceTime() - timeInstantiated;
            float t = (float)(timeSinceInstantiated / (RhythmManager.instance.noteTime * 2)); // descobrindo a posicao

            if (t > 1) { Destroy(gameObject); } // basicamente saiu da tela :D == morte
            else // == spawn
            {
                transform.localPosition = Vector3.Lerp(Vector3.up * RhythmManager.instance.noteSpawnY, Vector3.up * RhythmManager.instance.noteDespawnY, t);
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
