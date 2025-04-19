using InventorySystem;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [System.Serializable]
    public class GameData
    {
        /*public int[] scores;
        public bool[] questComplete;
        public int actualQuest;*/

        public Vector3 playerPosition;
        public Dictionary<string, bool> collectedItems; // respectivamente, id e status (coletado ou nao-coletado)

        public int test;

        // public ItemSlot[] slots;

        // valores iniciais das variaveis quando não tiver nenhuma arquivo de save
        public GameData()
        {
            test = 0;
            playerPosition = Vector3.zero;
            collectedItems = new Dictionary<string, bool>();
        }
    }
}