using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace WPFPractices
{
    [Serializable]
    public class Configuration
    {
        public float PositionX;
        public float PositionY;

        public float Width = 450;
        public float Height = 800;
    }


    public static class ConfigurationLoader
    {
        public static Configuration Load(string path)
        {
            if (File.Exists(path))
            {
                string configjson = File.ReadAllText(path);

                Configuration config = JsonConvert.DeserializeObject<Configuration>(configjson);

                return config;
            }
            else
            {
                return null;
            }
        }

        public static void Save(Configuration config, string path)
        {
            string configjson = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(path, configjson);
        }
    }

}
