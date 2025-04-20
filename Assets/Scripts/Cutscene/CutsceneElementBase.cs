using System.Collections;
using UnityEngine;

public class CutsceneElementBase : MonoBehaviour
{
    public float duration;
    public CutsceneHandler cutsceneHandler { get; private set; }

    public void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
    }

    public virtual void Execute() //We can override the Elements on Test
    {

    }

    protected IEnumerator WaitandAdvance()
    {
        yield return new WaitForSeconds(duration); //Pauses the corroutine for set amount of time
        cutsceneHandler.PlayNextElement();
    }
}
