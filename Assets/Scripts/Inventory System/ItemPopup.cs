using UnityEngine;
using TMPro;


public class ItemPopup : MonoBehaviour
{
    public static ItemPopup instance;

    [Header("UI References")]
    [SerializeField] public GameObject popupUI;
    [SerializeField] public TextMeshProUGUI itemNameText;
    [SerializeField] public TextMeshProUGUI descriptionText;

   
    private bool isShowing = false;

    private void Awake()
    {
        if(instance == null) {instance = this;}
        else {Destroy(gameObject);}
    }
    public void Start()
    {
        popupUI.SetActive(false);
    }

    private void Update()
    {
        if (isShowing && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButton(0)))
        {
            popupUI.SetActive(false);
            isShowing = false;
        }
    }

    public void ShowItem(string itemName, string description)
    {
        popupUI.SetActive(true);
        itemNameText.text = itemName;
        descriptionText.text = description;
        isShowing = true;


    }

}
