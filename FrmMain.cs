using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using static NewMeteo;

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
            try
            {
                NewMeteo objGetMeteoFromService = new NewMeteo();
                WeatherInfo weatherInfo = await objGetMeteoFromService.GetWeatherInfoAsync(txtCity.Text.ToUpper());

                if (weatherInfo != null)
                {
                    lstResult.Items.Add($"{weatherInfo.timestamps.ToString()}  # {weatherInfo.Name} # {weatherInfo.WeatherDescription} # {weatherInfo.Temperature }");
                }
                else
                {
                    lstResult.Items.Add("Failed to get weather information.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
