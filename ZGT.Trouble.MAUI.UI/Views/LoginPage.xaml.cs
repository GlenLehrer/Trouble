using System.Reflection;
using ZGT.Trouble.MAUI.UI.Views.MyMauiApp;
using Microsoft.Maui.Graphics.Platform;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Maui.Controls;
namespace ZGT.Trouble.MAUI.UI.Views;
/*Page logs in to an account.  
 * The goal here is to log in an account.  
 * The code needs to be able to talk with the API.  
 */
public partial class LoginPage : ContentPage
{
    
    string APIAddress = App.APIAddress; //port number, the 7009 depends on what your local API runs on
    public LoginPage()
    {
        InitializeComponent();
        Title = "Login";
    }
     
    async void Login_Clicked(System.Object sender, System.EventArgs e) //Login Button is clicked
    {
        //Call API to see if login is valid
        ZGT.Trouble.BL.Models.Player player = new BL.Models.Player { UserName = txtUser.Text, Password = txtPass.Text, Email = "12@12.com", NumberOfWins = 0, DateJoined = DateTime.Now };
        HttpClient client = new HttpClient();
        var data = JsonConvert.SerializeObject(player);
        var response = await client.PostAsync(new Uri(APIAddress + "player/login"), new StringContent(data, System.Text.Encoding.UTF8, "application/json"));
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Title = player.UserName;
            string result = response.Content.ReadAsStringAsync().Result;
            result = result.Replace("\'", "").Replace("\\", "").Replace("\"", "").Trim(); //Return string has quotes inside the string
            App.PlayerID = new Guid(result); //Entire app knows the PlayerID that logs in.
            App.PlayerUserName = player.UserName;
            App.GameID = Guid.Empty;
            await DisplayAlert("Success", "Logged In: " + player.UserName, "Ok");
        }
            
        else
            Title = response.StatusCode.ToString();
        //var data = JsonConvert.SerializeObject(player);
        //HttpClient client = new HttpClient();
        //var response = await client.PostAsync(new Uri("https://joewetzel.com/fvtc/account/login"), new StringContent(data, System.Text.Encoding.UTF8, "application/json"));
        //var sKey = response.Content.ReadAsStringAsync().Result;

        //if (string.IsNullOrEmpty(sKey))
        //{
        //    await DisplayAlert("Errror", "Error occurred logging In", "Ok");
        //    return;
        //}
        //else
        //{
        //    App.SessionKey = sKey;
        //    //(MainPage.main_page as MainPage)?.inputKey();
        //    await Shell.Current.GoToAsync("///JoinGameRoomPage");
        //}

    }

    async void CreateAccount_Clicked(System.Object sender, System.EventArgs e)
    {      
        await Shell.Current.GoToAsync("///CreateAccountPage"); //Redirect to CreateAccountPage after CreateAccountButton is clicked
    }
}
