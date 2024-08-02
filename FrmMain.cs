using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using static NewMeteo;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GetMeteo
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private async void butExtractMeteo_Click(object sender, EventArgs e)
        {
            NewMeteo objNewMeteo = new NewMeteo();
            List<System.Collections.Generic.KeyValuePair<string, object>> dicCity = null;
            try
            {
                dicCity = await objNewMeteo.GetCity(txtCity.Text.ToUpper());

                var orderedKeys = new List<string>
                {
                    "name",
                    "coord.lon",
                    "coord.lat",
                    "weather.main",
                    "main.temp",
                    "main.feels_like",
                    "main.temp_min",
                    "main.temp_max",
                    "main.pressure",
                    "main.humidity"
                };
                lstResult.Items.Add($"Timestamp : {DateTime.Now}");
                foreach (var key in orderedKeys)
                {
                    foreach (var item in dicCity)
                    {
                        if (item.Key == key)
                        {
                            lstResult.Items.Add($"{item.Key}: {item.Value}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
            }
        }
    }
}