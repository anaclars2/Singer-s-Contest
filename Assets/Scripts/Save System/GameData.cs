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

        public List<bool> rhythmVictory;

        // valores iniciais das variaveis quando n?o tiver nenhuma arquivo de save
        public GameData()
        {
            playerPosition = new Vector3(0, 1.5f, -28.01f);
            collectedItems = new SerializableDictionary<string, bool>();
            itemSlot = new SerializableDictionary<string, string>();

            rhythmVictory = new List<bool>();
        }
    }
}
