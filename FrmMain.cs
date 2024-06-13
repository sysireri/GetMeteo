using System;
using System.Windows.Forms;
using System.Threading.Tasks;

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
                NewGetMeteo.NewMeteo objGetMeteoFromService = new NewGetMeteo.NewMeteo();
                NewGetMeteo.WeatherInfo weatherInfo = await objGetMeteoFromService.GetWeatherInfoAsync(txtCity.Text.ToUpper());


                if (weatherInfo != null)
                {

                    txtResult.Text = weatherInfo.timestamps.ToString();
                    txtResult.Text = weatherInfo.Name;

                }
                else
                {
                    Console.WriteLine("Failed to get weather information.");
                    txtResult.Text = "Failed to get weather information.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
