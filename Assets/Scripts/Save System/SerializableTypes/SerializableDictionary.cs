using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] List<TKey> keys = new List<TKey>();
        [SerializeField] List<TValue> values = new List<TValue>();

        // salvando o dicionario nas listas
        public void OnBeforeSerialize()
        {
            // garantindo que as listas estao limpas
            keys.Clear(); values.Clear();

            // colocando cada chave e valor do dicionario nas listas
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        // carregando o dicionario da lista
        public void OnAfterDeserialize()
        {
            // garantindo que o dicionario esta limpo
            this.Clear();

            // garantindo que as listas de key e value tem o mesmo tamanho
            if (keys.Count != values.Count ) { Debug.LogError($"Tried to deserialize a SerializableDicionary, but the amount of keys {keys.Count} " +
                $"does not match the number of values {values.Count}."); }

            // adicionando os valores no dicionario
            for (int i = 0; i < keys.Count; i++) { this.Add(keys[i], values[i]); }
        }
    }
}