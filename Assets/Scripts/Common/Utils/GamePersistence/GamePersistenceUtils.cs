using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace WotN.Common.Utils.GamePersistance
{
    public class GamePersistenceUtils : MonoBehaviour
    {
        private const string supportedFileExtension = ".wotn";

        public static void SaveGame(PersistedCharacterData characterData)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath
                         + $"/{characterData.heroName}.wotn");

            bf.Serialize(file, characterData);
            file.Close();

        }

        public static PersistedCharacterData LoadGame(string savedHeroName)
        {
            if (File.Exists(Application.persistentDataPath
                       + $"/{savedHeroName}.wotn"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file =
                           File.Open(Application.persistentDataPath
                           + $"/{savedHeroName}.wotn", FileMode.Open);
                PersistedCharacterData savedData = (PersistedCharacterData)bf.Deserialize(file);
                file.Close();

                return savedData;
            }
            else
            {
                Debug.LogError("There is no save data!");

                return null;
            }

        }

        public static void DeleteSavedGame(string savedHeroName)
        {
            var savedDataPath = Application.persistentDataPath
                       + $"/{savedHeroName}.wotn";

            if (File.Exists(savedDataPath))
            {
                File.Delete(savedDataPath);
            }
            else
            {
                Debug.LogError("Cannot delete saved hero data. There is no save data!");

                return;
            }
        }

        public static IEnumerable<PersistedCharacterData> LoadAllCharacters()
        {
            var files = Directory.GetFiles(Application.persistentDataPath);

            var characterList = new List<PersistedCharacterData>();

            foreach (var filePath in files)
            {
                var fileExtension = Path.GetExtension(filePath);

                if (fileExtension == supportedFileExtension)
                {
                    var heroName = Path.GetFileNameWithoutExtension(filePath);

                    var loadedCharacterData = LoadGame(heroName);

                    if (loadedCharacterData != null)
                    {
                        characterList.Add(loadedCharacterData);
                    }

                }
            }

            return characterList;
        }
    }
}
