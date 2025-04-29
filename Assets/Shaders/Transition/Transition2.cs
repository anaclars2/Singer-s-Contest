using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition2 : MonoBehaviour
{
    [SerializeField] private Material transitionMaterial;
    [SerializeField] private float transitionSpeed = 1f;
    [SerializeField] private SCENES sceneToLoad; // nome da cena

    private float maskAmount = 0f;
    private bool transitioningIn = false;
    private bool transitioningOut = false;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
     
        // Começa a cena revelando (inverso da transição)
        maskAmount = 1f;
        transitionMaterial.SetFloat("_MaskAmount", maskAmount);
        transitioningIn = true;

    }

    private void Update()
    {
        if (transitioningIn)
        {
            maskAmount -= Time.deltaTime * transitionSpeed;
            transitionMaterial.SetFloat("_MaskAmount", maskAmount);

            if (maskAmount <= 0f)
            {
                transitioningIn = false;
                maskAmount = 0f;
                transitionMaterial.SetFloat("_MaskAmount", maskAmount);
            }
        }

        if (transitioningOut)
        {
            maskAmount += Time.deltaTime * transitionSpeed;
            transitionMaterial.SetFloat("_MaskAmount", maskAmount);

            if (maskAmount >= 1f)
            {
                transitioningOut = false;
                maskAmount = 1f;
                transitionMaterial.SetFloat("_MaskAmount", maskAmount);
                // Cena está totalmente coberta → troca a cena

                StartCoroutine(DelayedSceneLoad());
            }


        }
    }

    private IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(0.2f); // Um pequeno delay para garantir o preenchimento
        
    }

    public void StartTransition(SCENES targetScene)
    {
        Debug.Log("startTransition");
        transitioningOut = true;
        maskAmount = 0f;
        sceneToLoad = targetScene;
    }
}

