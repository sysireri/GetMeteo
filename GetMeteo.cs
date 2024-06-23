using System;
//using System.Diagnostics;
//using System.IO;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Newtonsoft.Json.Linq;

public class NewGetMeteo
{
    public class NewMeteo
    {
        public async System.Threading.Tasks.Task<WeatherInfo> GetWeatherInfoAsync(string vstrCity)
        {
            WeatherInfo objWeatherInfo = null;
            string strApiKey = RunEncryptDecryptUtility("DECRYPT", "Iex7loq71216qjD4Qqjjyd77QRplEh7k2RUIk8mz3iEtErSdgQP4bG1o1A4UNgw0");
            string strApiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={vstrCity}&appid={strApiKey}&units=metric";

            try
            {
                using (System.Net.Http.HttpClient objHttpClient = new System.Net.Http.HttpClient())
                {
                    System.Net.Http.HttpResponseMessage objHttpResponseMessage = await objHttpClient.GetAsync(strApiUrl);

                    if (objHttpResponseMessage != null && objHttpResponseMessage.IsSuccessStatusCode)
                    {
                        string strResponseBody = await objHttpResponseMessage.Content.ReadAsStringAsync();
                        Newtonsoft.Json.Linq.JObject objJObjectWeatherData = Newtonsoft.Json.Linq.JObject.Parse(strResponseBody);

                        objWeatherInfo = new WeatherInfo();
                        objWeatherInfo.timestamps = DateTime.Now;

                        if (objJObjectWeatherData.TryGetValue("main", out Newtonsoft.Json.Linq.JToken mainToken))
                        {
                            if (mainToken["temp"] != null)
                            {
                                objWeatherInfo.Temperature = (double)mainToken["temp"];
                            }
                            else
                            {
                                objWeatherInfo.Temperature = -999;
                            }
                        }

                        if (objJObjectWeatherData.TryGetValue("weather", out Newtonsoft.Json.Linq.JToken weatherToken))
                        {
                            if (weatherToken[0]["description"] != null)
                            {
                                objWeatherInfo.WeatherDescription = weatherToken[0]["description"].ToString();
                            }
                            else
                            {
                                objWeatherInfo.WeatherDescription = "Inconnu";
                            }
                        }

                        if (objJObjectWeatherData.TryGetValue("name", out Newtonsoft.Json.Linq.JToken nameToken))
                        {
                            objWeatherInfo.Name = nameToken.ToString();
                        }
                    }
                    else
                    {
                        throw new Exception("Ville inconnue.");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return objWeatherInfo;
        }

        private string RunEncryptDecryptUtility(string command, string inputText)
        {
            string result = "";
            try
            {
                // Chemin de l'exécutable EncryptorUtilities.exe à partir de l'application en cours
                string strEncryptorUtilities = @"C:\Users\eric\source\repos\csharp\EncryptorUtilities\bin\EncryptorUtilities.exe";

                if (!System.IO.File.Exists(strEncryptorUtilities))
                {
                    throw new Exception($"Le .EXE {strEncryptorUtilities} N'exsiste pas.");
                }

                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = strEncryptorUtilities,
                    Arguments = $"{command} \"{inputText}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(startInfo))
                {
                    using (System.IO.StreamReader reader = process.StandardOutput)
                    {
                        result = reader.ReadToEnd().Trim();
                        Console.WriteLine(result); // Afficher le résultat dans la console
                    }
                }
            }
            catch (Exception ex)
            {
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return result;
        }
   
    }

    public class WeatherInfo
    {
        public DateTime timestamps { get; set; }
        public string Name { get; set; }
        public double Temperature { get; set; }
        public string WeatherDescription { get; set; }
    }
}
