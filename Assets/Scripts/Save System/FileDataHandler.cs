using System.IO;
using System;
using UnityEngine;

namespace SaveSystem
{
    public class FileDataHandler
    {
        string _dataPath = " ";
        string _fileName = " ";

        public FileDataHandler(string dataPath, string fileName)
        {
            _dataPath = dataPath;
            _fileName = fileName;
        }

        public GameData Load()
        {
            string path = Path.Combine(_dataPath, _fileName);
            GameData loadedData = null;

            // verificando se o arquivo ? encontrado no computador
            if (File.Exists(path))
            {
                try
                {
                    // carregando os dados serializados do arquivo
                    string dataToLoad = " ";
                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    // desserializando os dados .txt em C# objetos
                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {

                }
            }
            return loadedData;
        }

        public void Save(GameData gameData)
        {
            string path = Path.Combine(_dataPath, _fileName);
            try
            {
                // criando o diretorio onde o arquivo vai ser salvo
                // se nao existir no computador
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                // serializando os dados do jogo em um arquivo .json
                string dataToStore = JsonUtility.ToJson(gameData, true);

                // escrevendo o arquivo
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }

            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to save to file {path}\n{e}");
            }
        }
    }
}