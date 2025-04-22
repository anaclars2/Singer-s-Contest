using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI speakerNameText;
    public Image portraitLeft;
    public Image portraitRight;
    public TextMeshProUGUI textComponent;
    public List<DialogueLine> lines = new List<DialogueLine>();
    public float textSpeed;

    private int index;

    private void OnEnable()
    {
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
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
        DialogueLine line = lines[index];

        speakerNameText.text = line.speakerName;
        textComponent.text = "";

        switch(line.side)
        {
            case DialogueSide.Left:
            portraitLeft.gameObject.SetActive(true);
            portraitLeft.sprite = line.speakerIcon;
            portraitRight.gameObject.SetActive(false);
            break;

            case DialogueSide.Right:
            portraitRight.gameObject.SetActive(true);
            portraitRight.sprite =line.speakerIcon;
            portraitLeft.gameObject.SetActive(false);
            break;
        }
        
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
            gameObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class DialogueLine
{
    public DialogueSide side;
    public string speakerName;
    public Sprite speakerIcon;
    [TextArea(3, 10)] public string text;
}

public enum DialogueSide
{
    Left,
    Right
}
