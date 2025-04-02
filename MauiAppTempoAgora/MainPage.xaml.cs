using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using Microsoft.Maui.Networking;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
           InitializeComponent();
        }

        

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    // Verifica a conexão com a internet
                    if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                    {
                        await DisplayAlert("Erro", "Sem conexão com a internet.", "OK");
                        return;
                    }

                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null && t.lat != 0 && t.lon != 0) // Verifica se os dados são válidos
                    {
                        string dados_previsao = $"Latitude: {t.lat} \n" +
                                                $"Longitude: {t.lon} \n" +
                                                $"Nascer do Sol: {t.sunrise} \n" +
                                                $"Por do Sol: {t.sunset} \n" +
                                                $"Temp Máx: {t.temp_max} \n" +
                                                $"Temp Min: {t.temp_min} \n" +
                                                $"Descricao: {t.description} \n" +
                                                $"Velocidade: {t.speed} \n" +
                                                $"Visibilidade: {t.visibility} \n";
                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "Cidade não encontrada.";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}