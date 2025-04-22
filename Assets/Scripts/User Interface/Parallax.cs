using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Tooltip("Subtracts 0.3f the closer it gets to the background.\nAdds 0.3f the closer it gets to the camera.")]
    [SerializeField] float offsetMultiplier = 0f;
    [SerializeField] float smoothTime = 0.5f;

    Vector2 startPosition;
    Vector3 velocity;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
    }
}
