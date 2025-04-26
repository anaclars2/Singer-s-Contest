using UnityEngine;
using CharacterSystem;
using TMPro;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Player player;

    [Header("Follow Settings")]
    [SerializeField] Vector3 offSet;
    [SerializeField] Quaternion rotation;
    public bool isToFollow = false;
    public bool usingTransform;
    [SerializeField] float speedFollow = 5f;

    Camera _camera;

    private void Start()
    {
        if (player == null) { player = FindFirstObjectByType<Player>().GetComponent<Player>(); }
        if (_camera == null) { _camera = GetComponent<Camera>(); }

        if (usingTransform == true)
        {
            offSet.x = _camera.transform.position.x - player.gameObject.transform.position.x;
            offSet.y = _camera.transform.position.y - player.gameObject.transform.position.y;
            offSet.z = _camera.transform.position.z - player.gameObject.transform.position.z;
        }
    }

    private void Update()
    {
        if (isToFollow == true) { FollowPlayer(); }
        else
        {
            // logica de ir para um ponto pre-definido na cena 
        }
    }

    private void FollowPlayer()
    {
        if (usingTransform == false)
        {
            transform.rotation = rotation;
        }

        Vector3 targetPosition = player.transform.position + offSet;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speedFollow * Time.deltaTime);
    }
}
