using UnityEngine;
using CharacterSystem;
using UnityEngine.InputSystem.XR;

public class Teleport : MonoBehaviour
{
    [SerializeField] Vector3 newPosition;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        CharacterController controller = player.gameObject.GetComponent<CharacterController>();

        if (controller != null)
        {
            Debug.Log("oi player");

            controller.enabled = false; 
            player.transform.position = newPosition; // teletransporta o jogador
            controller.enabled = true; // reativa apos mover
        }

    }
}
