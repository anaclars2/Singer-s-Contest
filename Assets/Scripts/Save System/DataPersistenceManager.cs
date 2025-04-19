using InventorySystem;
using UnityEngine;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace SaveSystem
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File Storage Settings")]
        [SerializeField] string fileName;

        GameData gameData;
        List<IDataPersistence> dataPersistenceObjects;
        FileDataHandler dataHandler;

        public static DataPersistenceManager instance;

        private void Awake() // singleton
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }

            // DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            this.dataPersistenceObjects = FindAllDataPersistencesObjects();
            LoadGame();
        }

        private List<IDataPersistence> FindAllDataPersistencesObjects()
        {
            // encontrando todos os gameObjects que herdam de MonoBehaviour e IDataPersistence 
            // e em seguida retornando eles como uma lista
            IEnumerable<IDataPersistence> _dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).
                OfType<IDataPersistence>();

            return new List<IDataPersistence>(_dataPersistenceObjects);
        }

        public void NewGame()
        {
            this.gameData = new GameData();
        }

        public void LoadGame()
        {
            // carregar todos os arquivos salvos
            this.gameData = dataHandler.Load();

            // se nao tiver nenhum criar um novo
            if (this.gameData == null)
            {
                Debug.Log("No data was found. Initializing data to defaults.");
                NewGame();
            }

            // depois enviar todos os dados carregados para os gameobjects
            Debug.Log("In LoadGame");
            foreach (IDataPersistence dataPersistence in dataPersistenceObjects) { dataPersistence.LoadData(gameData); }
            Debug.Log("Loaded test number: " + gameData.test);
            Debug.Log("All is okay in LoadGame");
        }

        public void SaveGame()
        {
            // passar os dados para outros scripts, para atualizar os dados
            // salvar esses dados em uma arquivo de texto usando um manipulador de dados
            Debug.Log("In SaveGame");
            foreach (IDataPersistence dataPersistence in dataPersistenceObjects) { dataPersistence.SaveData(gameData); }
            Debug.Log("Saved test number: " + gameData.test);
            Debug.Log("All is okay in SaveGame");

            dataHandler.Save(gameData);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }
    }
}