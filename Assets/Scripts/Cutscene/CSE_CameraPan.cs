using System.Collections;
using UnityEngine;

public class CSE_CameraPan : CutsceneElementBase
{
  private Camera cam;
  [SerializeField] private Vector2 distanceToMove; //I may have to change this to only Vector
    

  public override void Execute()
    {
        cam = cutsceneHandler.cam;
        StartCoroutine(PanCoroutine());
    }

    private IEnumerator PanCoroutine() // LERP
    {
        Vector3 originalPosition = cam.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(distanceToMove.x, distanceToMove.y, 0);

        float startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            cam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            elapsedTime = Time.time - startTime;

            yield return null; //Allows the frame toc end, and it goes back to the top of the loop
        }

        cam.transform.position = targetPosition;
        cutsceneHandler.PlayNextElement();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
