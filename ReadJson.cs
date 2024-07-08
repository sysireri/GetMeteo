using System;
//using System.IO;
//using Newtonsoft.Json;

public class ReadJson
{
    public KeyData ReadKeayData()
    {
        KeyData objKeyData = null;
        string JsonPath = "";
        string strJson = "";
        KeyData ApiKey_Meteo = null;
        try
        {
            objKeyData = new KeyData();
            JsonPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Encrypt.Json");

            strJson = System.IO.File.ReadAllText(JsonPath);

            ApiKey_Meteo = Newtonsoft.Json.JsonConvert.DeserializeObject<KeyData>(strJson);

            objKeyData.ApiKey_Meteo = ApiKey_Meteo.ApiKey_Meteo;
        }
        catch(Exception ex)
        {
            System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
        }
        finally
        {
            ApiKey_Meteo = null;
            JsonPath = null;
            strJson = null;
            ApiKey_Meteo = null;
        }
        return objKeyData;
    }
}
    // Classe pour désérialiser les données JSON
    public class KeyData
    {
        public string ApiKey_Meteo { get; set; }
    }
