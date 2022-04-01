using Newtonsoft.Json;
using System;
using System.IO;
namespace FinTech
{
    public class AppConfig
    {
        public static string DataPath
        {
            get => typeof(AppConfig).Assembly.Location.Split("src")[0] + "data/";
        }
        private string ConfigFileName
        {
            get => Path.Combine(DataPath, "AppConfig.json");
        }


        public dynamic Get()
        {
            string txtJson;
            try
            {
                txtJson = File.ReadAllText(ConfigFileName);
            }
            catch
            {
                txtJson = JsonConvert.SerializeObject(new
                {
                    App = new DirectoryInfo(DataPath).Name,
                });
            }
            dynamic config= JsonConvert.DeserializeObject(txtJson);
            config.DataPath = DataPath;
            return config;
        }
        public void Save(dynamic Config)
        {
            string json = JsonConvert.SerializeObject(Config, Formatting.Indented);
            File.WriteAllText(ConfigFileName, json);
        }
    }
}
