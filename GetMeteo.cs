using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class NewMeteo
{
    public async  System.Threading.Tasks.Task<System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, object>>> GetCity(string vstrCity)
    {
        ReadJsonObject objReadJsonObject = null;
        objReadJsonObject = new ReadJsonObject();
        System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, object>> ToReturn = null;
        string strApiKey = "";
        string strApiUrl = "";

        System.Threading.Tasks.Task < System.Collections.Generic.Dictionary<string, object> > dicHttpMsg = null;
        try
        {
            strApiKey = objReadJsonObject.ReadEncryptDataPassord();

            if (!string.IsNullOrEmpty(strApiKey))
            {
                strApiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={vstrCity}&appid={strApiKey}&units=metric";

                using (System.Net.Http.HttpClient objHttpClient = new System.Net.Http.HttpClient())
                {
                    System.Net.Http.HttpResponseMessage objHttpResponseMessage = await objHttpClient.GetAsync(strApiUrl);

                    dicHttpMsg = objReadJsonObject.ConvertToDictionary(objHttpResponseMessage);

                    ToReturn = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, object>>();

                    foreach (System.Collections.Generic.KeyValuePair<string, object> item in dicHttpMsg.Result)
                    {
                        if (!string.IsNullOrEmpty(item.Value?.ToString()))
                        {
                            ToReturn.Add(new System.Collections.Generic.KeyValuePair<string, object>(item.Key, item.Value));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
        }
        finally
        {
            objReadJsonObject = null;
        }

        return ToReturn;
    }
}
