using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace EfialtisBot
{
    class DataStorage
    {
        public static Dictionary<string, string> pairs = new Dictionary<string, string>();

        static DataStorage()
        {
            //Load Data
            ValidateStorageFile("DataStorage.json");
            string json = File.ReadAllText("DataStorage.json");
            pairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
       
        }

        public static void SaveData()
        {
            //Save Data
        }

        private static string ValidateStorageFile(string filePath)
        {
            if(!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
                SaveData();
            }
        }
    }
}
