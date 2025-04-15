using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Objects/item")]
public class Item : ScriptableObject
{
       
 
    [Header("Only gameplay")]
    public ItemType type;
    public ActionType actionType;

    [Header("Only UI")]
    public TMP_Text itemDescription;
    
    [Header("Both")]
    public Sprite image; // sprite shown in the inventory
}

public enum ItemType
{
    Letters,
    Object 
}

public enum ActionType
{
    Read,
    Give
}
