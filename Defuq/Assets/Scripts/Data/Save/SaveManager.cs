using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Data.Save
{
    public static class SaveManager
    {
        private static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Save/";

        public static void Initialize()
        {
            if (!Directory.Exists(SAVE_FOLDER))
            {
                Directory.CreateDirectory(SAVE_FOLDER);
            }
        }

        public static void Save(string saveString)
        {
            File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
        }

        public static string Load()
        {
            if (File.Exists(SAVE_FOLDER + "save.txt"))
            {
                string saveString = File.ReadAllText(SAVE_FOLDER + "save.txt");
                return saveString;
            }
            else
            {
                return null;
            }
        }

    }
}

