using UnityEngine;
using UISystem;
using AudioSystem;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        UIManager.instance.Animation(ANIMATION.SlideInAndOut, true);
        // decidir ainda AudioManager.instance.PlaySfx();
    }

    private void Update()
    {
        Test();
    }

    private void Test()
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
