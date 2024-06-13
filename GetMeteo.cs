using System;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Newtonsoft.Json.Linq;

namespace GetMeteo
{
    public class NewGetMeteo
    {
        public class NewMeteo
        {
            public async System.Threading.Tasks.Task<WeatherInfo> GetWeatherInfoAsync(string vstrCity)
            {
                WeatherInfo objWeatherInfo = null;
                string strApiKey = "aeac817b953adf4ea3773e53b296ab43";
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
        }

        public class WeatherInfo
        {
            public DateTime timestamps { get; set; }
            public string Name { get; set; }
            public double Temperature { get; set; }
            public string WeatherDescription { get; set; }
        }
    }
}
