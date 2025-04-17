using UnityEngine;
using CharacterSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Player player;

    [Header("Follow Settings")]
    [SerializeField] Vector3 offSet;
    [SerializeField] Quaternion rotation;
    public bool isToFollow = false;
    [SerializeField] float speedFollow = 5f;

    Camera _camera;

    private void Start()
    {
        if (player == null) { player = FindFirstObjectByType<Player>().GetComponent<Player>(); }
        if (_camera == null) { _camera = GetComponent<Camera>(); }
    }

    private void Update()
    {
        if (isToFollow == true) { FollowPlayer(); }
        else
        {
            // logica de ir para um ponto pre-definido na cena :D
        }
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.transform.position + offSet;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speedFollow * Time.deltaTime);
        transform.rotation = rotation;
    }
}
