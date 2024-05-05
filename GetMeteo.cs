﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace GetMeteo
{
    class GetMeteo : IDisposable
    {
        #region "IDisposable"

        // Définir un champ pour suivre si l'objet a déjà été disposé
        private bool bolDisposed = false;

        // Méthode Dispose() pour libérer les ressources non managées
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Méthode virtuelle pour libérer les ressources managées et non managées
        protected virtual void Dispose(bool disposing)
        {
            if (!bolDisposed)
            {
                if (disposing)
                {
                    // Libérer les ressources managées
                    // (ex. fermer des fichiers, libérer des connexions)
                }

                // Libérer les ressources non managées
                // (ex. libérer des handles de fichiers, des ressources système)

                // Marquer l'objet comme disposé
                bolDisposed = true;
            }
        }

        // Finaliseur pour libérer les ressources non managées (au cas où Dispose() ne serait pas appelé)
        ~GetMeteo()
        {
            Dispose(false);
        }

        #endregion

        public async System.Threading.Tasks.Task<string> ReadMeteoByCity(string vstrCity)
        {
            string strMeteoCity = "";
            try
            {
                strMeteoCity = await MainAsync(vstrCity);
            }
            catch (Exception ex)
            {
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
            }
            finally
            {
            }

            return strMeteoCity;
        }

        private async System.Threading.Tasks.Task<string> MainAsync(string vstrCity)
        {
            string strApiKey = "aeac817b953adf4ea3773e53b296ab43";
            string strMeteoCity = "";
            OpenWeatherAPI.QueryResponse objQueryResponse = null;
            System.Text.StringBuilder objStringBuilder = null;
            try
            {
                if(vstrCity != null && vstrCity.Length > 0)
                {
                    using (OpenWeatherAPI.OpenWeatherApiClient objOpenWeatherApiClient = new OpenWeatherAPI.OpenWeatherApiClient(strApiKey))
                    {
                        objQueryResponse = await objOpenWeatherApiClient.QueryAsync(vstrCity);

                        if (objQueryResponse != null)
                        {
                            if (objQueryResponse.ValidRequest)
                            {
                                objStringBuilder = new System.Text.StringBuilder();

                                objStringBuilder.AppendLine(DateTime.Now.ToString());
                                objStringBuilder.AppendLine();
                                objStringBuilder.AppendLine($"{objQueryResponse.Name} \t\tLongitude : {objQueryResponse.Coordinates.Longitude} \tLatitude : {objQueryResponse.Coordinates.Latitude} Altitude : {objQueryResponse.Sys.Message}");
                                objStringBuilder.AppendLine($"Température : \t{objQueryResponse.Main.Temperature.CelsiusCurrent}");
                                objStringBuilder.AppendLine($"Température Min : \t{objQueryResponse.Main.Temperature.CelsiusMinimum}");
                                objStringBuilder.AppendLine($"Température Max : \t{objQueryResponse.Main.Temperature.CelsiusMaximum}");

                                objStringBuilder.AppendLine($"Altitude : \t{objQueryResponse.Main.GroundLevelAtm}");
                                objStringBuilder.AppendLine($"Nuage : \t{objQueryResponse.Clouds.All}");

                                objStringBuilder.AppendLine($"Nuage : \t{objQueryResponse.Cod}");


                                strMeteoCity = objStringBuilder.ToString();
                            }
                        }

                    }
                }
                else
                {
                    strMeteoCity = "Vous n'avez pas fournie de ville.";
                }
            }
            catch (Exception ex)
            {
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
            }
            finally
            {
                if (objStringBuilder != null)
                {
                    objStringBuilder.Clear();
                    objStringBuilder = null;
                }

                objQueryResponse = null;
            }

            return strMeteoCity;
        }
    }
}
