using UnityEngine;

public class CutsceneInitiatior : MonoBehaviour
{
 private CutsceneHandler cutsceneHandler;

    public void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("É o jogador! Cutscene iniciando...");
            cutsceneHandler.PlayNextElement();
        }
    }
}
