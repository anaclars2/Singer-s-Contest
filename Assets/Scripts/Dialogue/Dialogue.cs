using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private bool startOnAwake = false;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private SCENES nextScene;
    public TextMeshProUGUI speakerNameText;
    public Image portraitLeft;
    public Image portraitRight;
    public TextMeshProUGUI textComponent;
    public GameObject headerObject;
    public List<DialogueLine> lines = new List<DialogueLine>();
    public float textSpeed;


    private int index;


    private void OnEnable()
    {
        if (startOnAwake)
            StartDialogue();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (textComponent.text == lines[index].text)
            {
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[index].text;
            }
        }
    }

    void StartDialogue()
    {

        index = 0;
        textComponent.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(Typeline());
    }

    IEnumerator Typeline()
    {
        portraitLeft.color = new Color(portraitLeft.color.r, portraitLeft.color.g, portraitLeft.color.b, 0f);
        portraitRight.color = new Color(portraitRight.color.r, portraitRight.color.g, portraitRight.color.b, 0f);

        DialogueLine line = lines[index];

        headerObject.SetActive(line.isSpeech);

        speakerNameText.text = line.isSpeech ? line.speakerName : "";

        //    speakerNameText.text = line.speakerName;
        //textComponent.text = "";

        //switch(line.side)
        //{
        //    case DialogueSide.Left:
        //    portraitLeft.gameObject.SetActive(true);
        //    portraitLeft.sprite = line.speakerIcon;
        //    portraitRight.gameObject.SetActive(false);
        //        StartCoroutine(FadeInUI(portraitLeft));
        //    break;

        //    case DialogueSide.Right:
        //    portraitRight.gameObject.SetActive(true);
        //    portraitRight.sprite =line.speakerIcon;
        //    portraitLeft.gameObject.SetActive(false);
        //        StartCoroutine(FadeInUI(portraitRight));
        //        break;
        //}
        if (line.speakerIcon != null)
        {
            if (line.side == DialogueSide.Left)
            {
                portraitLeft.gameObject.SetActive(true);
                portraitLeft.sprite = line.speakerIcon;
                portraitRight.gameObject.SetActive(false);
                StartCoroutine(FadeInUI(portraitLeft));
            }
            else
            {
                portraitRight.gameObject.SetActive(true);
                portraitRight.sprite = line.speakerIcon;
                portraitLeft.gameObject.SetActive(false);
                StartCoroutine(FadeInUI(portraitRight));
            }
        }
        else
        {
            portraitLeft.gameObject.SetActive(false);
            portraitRight.gameObject.SetActive(false);
        }

        textComponent.text = "";

        foreach (char c in line.text.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

    }

    void NextLine()
    {
        if (index < lines.Count - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(Typeline());
        }
        else
        {
            if (continueButton != null)
            {
                continueButton.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }


    IEnumerator FadeInUI(Graphic uiElement, float duration = 0.4f)
    {
        Color color = uiElement.color;
        color.a = 0f;
        uiElement.color = color;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            color.a = Mathf.Lerp(0f, 1f, t);
            uiElement.color = color;
            yield return null;
        }
    }

    public void ContinueToNextScene()
    {
        GameManager.instance.sceneToLoad = nextScene;
        GameManager.instance.LoadScene();
    }
    
}



[System.Serializable]
public class DialogueLine
{
    public DialogueSide side;
    public string speakerName;
    public Sprite speakerIcon;
    public bool isSpeech;
    [TextArea(3, 10)] public string text;
}

public enum DialogueSide
{
    Left,
    Right
}
