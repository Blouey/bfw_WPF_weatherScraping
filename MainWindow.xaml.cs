

using System.Net.Http;
using System.Windows;


namespace Wetter_WebScraping_WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void BtnSuche_Click(object sender, RoutedEventArgs e)
    {
        
        string url = $"https://www.wetteronline.de/wetter/{TxtOrt.Text}";
         
        
        
        // send request to the website
        HttpClient client = new HttpClient();
        string userAgent = "Mozilla/5.0 (Linux; Android 7.1.2; MI 5X; Flow) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/347.0.0.268 Mobile Safari/537.36";
        client.DefaultRequestHeaders.Add("User-Agent", userAgent);

        string html = "";
        HttpResponseMessage response;
        try
        {
             //html = client.GetStringAsync(url).Result;
              response = client.GetAsync(url).Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            MessageBox.Show("Ort nicht gefunden");
            return;
        }
        html = response.Content.ReadAsStringAsync().Result;
        HtmlTxt.Text = html;
        
        int index = html.IndexOf("current-temp") + 14;
        int index2 = html.IndexOf("&deg", index);
        
        if (index != -1)
        {
            string temp = html.Substring(index, index2 - index) + "°C";
            TempTxt.Text = temp;
        }
   
        
    }
    

    
}