using InventorySystem;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [System.Serializable]
    public class GameData
    {
        public Vector3 playerPosition;
        public SerializableDictionary<string, bool> collectedItems; // respectivamente, id e status (coletado ou nao-coletado)
        public SerializableDictionary<string, string> itemSlot; 

        // valores iniciais das variaveis quando não tiver nenhuma arquivo de save
        public GameData()
        {
            playerPosition = Vector3.zero;
            collectedItems = new SerializableDictionary<string, bool>();
            itemSlot = new SerializableDictionary<string, string>();
        }
    }
}