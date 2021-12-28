using Newtonsoft.Json;
using System;
using System.IO;
namespace FinTech
{
    public class AppConfig
    {
        private string getAppPath() => Path.GetFullPath("../../");
        private string getDataPath() =>Path.Combine(getAppPath(), "data/");
        private string getFileName() =>Path.Combine(getDataPath(), "AppConfig.json");
        
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
                    App = new DirectoryInfo(getAppPath()).Name,
                    DataPath = getDataPath(),
                });
            }
            return JsonConvert.DeserializeObject(txtJson);

        }
        public void Save(dynamic Config)
        {
            string json = JsonConvert.SerializeObject(Config, Formatting.Indented);
            File.WriteAllText(getFileName(), json);
        }
    }
}
