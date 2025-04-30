using System.Collections;
using UnityEngine;

public class DialogueTriggerZone : MonoBehaviour
{
    public GameObject dialogueCanvas; // arraste o Canvas desativado aqui

    private bool triggered = false;
    public string requiredFlag;
    public bool invertCondition = false; //se quiser que o di�logo s� aconte�a quando a flag N�O estiver ativa


    private void OnTriggerEnter(Collider other)
    {
        if (triggered || dialogueCanvas == null) return;

        if (other.CompareTag("Player"))
        {
            bool condition = true;

            if (!string.IsNullOrEmpty(requiredFlag))
            {

                condition = GameManager.instance.GetFlag(requiredFlag);
                if (invertCondition) { condition = !condition; }
            }

            if (condition)
            {
                triggered = true;
                StartCoroutine(ActivateAndStartDialogue());
            }
        }
    }

    IEnumerator ActivateAndStartDialogue()
    {
        dialogueCanvas.SetActive(true); // ativa o Canvas
        yield return null; // espera 1 frame

        Dialogue dialogue = dialogueCanvas.GetComponentInChildren<Dialogue>();
        if (dialogue != null)
        {
            dialogue.StartDialogue(); // inicia o di�logo manualmente
        }
    }

}
