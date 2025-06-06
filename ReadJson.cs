﻿using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ReadJsonObject
{
    public async System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, object>> ConvertToDictionary(System.Net.Http.HttpResponseMessage robjHttpResponseMessage)
    {
        JObject jsonObject = null;
        System.Collections.Generic.Dictionary<string, object> DicJson = null;
        try
        {
            jsonObject = await ReadJsonFromHttpResponseAsync(robjHttpResponseMessage);

            if (jsonObject != null)
            {
                DicJson =  ConvertJsonToDictionary(jsonObject);
            }
        }
        catch (Exception ex)
        {
            System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
        }
        finally
        {
            jsonObject = null;
        }
        return DicJson;
    }

    private async System.Threading.Tasks.Task<JObject> ReadJsonFromHttpResponseAsync(System.Net.Http.HttpResponseMessage robjHttpResponseMessage)
    {
        JObject jsonObject = null;
        try
        {
            if (robjHttpResponseMessage != null && robjHttpResponseMessage.IsSuccessStatusCode)
            {
                string strResponseBody = await robjHttpResponseMessage.Content.ReadAsStringAsync();
                jsonObject = JObject.Parse(strResponseBody);
            }
        }
        catch (Exception ex)
        {
            System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
        }
        finally
        {
        }
        return jsonObject;
    }

    public System.Collections.Generic.Dictionary<string, object> ConvertJsonToDictionary(JObject jsonObject)
    {
        System.Collections.Generic.Dictionary<string, object> dicJson = new System.Collections.Generic.Dictionary<string, object>();

        try
        {
            // Call the recursive method to populate the dictionary
            PopulateDictionary(jsonObject, dicJson, "");
        }
        catch (Exception ex)
        {
            System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
        }

        return dicJson;
    }
    private void PopulateDictionary(JObject jsonObject, System.Collections.Generic.Dictionary<string, object> dicJson, string parentKey)
    {
        try
        {
            foreach (var kvp in jsonObject)
            {
                // Create a full key by appending the current key to the parent key
                string key = string.IsNullOrEmpty(parentKey) ? kvp.Key : $"{parentKey}.{kvp.Key}";

                // Handle different types of JTokens
                switch (kvp.Value)
                {
                    case JObject nestedObject:
                        // Recursively process nested JObject
                        PopulateDictionary(nestedObject, dicJson, key);
                        break;

                    case JArray jsonArray:
                        // Handle arrays by converting them to a list of strings
                        dicJson[key] = jsonArray.ToObject<System.Collections.Generic.List<object>>();
                        break;

                    default:
                        // Convert other JTokens (e.g., JValue) to string and add to dictionary
                        dicJson[key] = kvp.Value.ToObject<object>();
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
        }

    }
    public string ReadEncryptDataPassord()
    {
        KeyDataCustum objKeyDataCustum = null;
        string strJsonPath = "";
        string strJsonData = "";
        KeyDataCustum ApiKey_Meteo = null;
        string strPwdEnc = null;
        try
        {
            objKeyDataCustum = new KeyDataCustum();
            strJsonPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Encrypt.Json");

            strJsonData = System.IO.File.ReadAllText(strJsonPath);

            ApiKey_Meteo = Newtonsoft.Json.JsonConvert.DeserializeObject<KeyDataCustum>(strJsonData);

            objKeyDataCustum.ApiKey_Meteo = ApiKey_Meteo.ApiKey_Meteo;
            strPwdEnc = ApiKey_Meteo.ApiKey_Meteo;
        }
        catch (Exception ex)
        {
            System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
        }
        finally
        {
            objKeyDataCustum = null;
        }
        return strPwdEnc;
    }
}

public class KeyDataCustum
{
    public string ApiKey_Meteo { get; set; }
}
