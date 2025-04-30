using UnityEngine;
using CharacterSystem;

public class Teleport : MonoBehaviour
{
    [SerializeField] Vector3 newPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() == true)
        {
            Debug.Log("oi player");
            other.transform.position = newPosition;
        }
    }
}
