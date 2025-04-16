using UnityEngine;
using UISystem;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.instance.Animation(ANIMATION.SlideInAndOut, true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            UIManager.instance.Animation(ANIMATION.SlideInAndOut, false);
        }
    }
}
