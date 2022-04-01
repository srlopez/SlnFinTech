using Newtonsoft.Json;
using System;
using System.IO;
namespace FinTech
{
    public class AppConfig
    {
        //private string getAppPath() => Path.GetFullPath("../../");
        //private string getDataPath() =>Path.Combine(getAppPath(), "data/");
        public static string DataPath
        {
            get => typeof(AppConfig).Assembly.Location.Split("src")[0] + "data/";
        }
        private string getFileName() => Path.Combine(DataPath, "AppConfig.json");


        public dynamic Get()
        {
            string txtJson;
            try
            {
                txtJson = File.ReadAllText(getFileName());
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
            File.WriteAllText(getFileName(), json);
        }
    }
}
