using UnityEngine;

public class CSE_TEST : CutsceneElementBase
{
   public override void Execute()
    {
        StartCoroutine(WaitandAdvance());
        Debug.Log("Executing " + name);
    }
}
