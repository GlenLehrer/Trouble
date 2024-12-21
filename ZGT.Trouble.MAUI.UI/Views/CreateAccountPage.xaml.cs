using System.Reflection;
using ZGT.Trouble.MAUI.UI.Views.MyMauiApp;
using Microsoft.Maui.Graphics.Platform;
using ZGT.Trouble.MAUI.UI.Models;
using Newtonsoft.Json;
using iText.StyledXmlParser.Jsoup.Nodes;
using System.Net;
namespace ZGT.Trouble.MAUI.UI.Views;
/*Page Creates an account.  
 * The goal here is to create an account.  
 * The code needs to be able to talk with the API.  
 */
public partial class CreateAccountPage : ContentPage
{
    string APIAddress = App.APIAddress; //Port number 7009, put in the port number your local API runs on
    public CreateAccountPage()
    {
        InitializeComponent();
        Title = "Create Account";
    }
    async void CreateUser_Clicked(System.Object sender, System.EventArgs e)
    {
        //Test for valid password data before calling API
        if (txtPass.Text != txtPass2.Text)
        {
            await DisplayAlert("Errror", "Your Passwords do not match!", "Ok");
            return;
        }
        //Test email has @ and a . in it to be a proper email format, before calling API
        if (!txtEmail.Text.Contains("@") || txtEmail.Text.LastIndexOf(".") < txtEmail.Text.LastIndexOf("@"))
        {
            await DisplayAlert("Errror", "Invalid Email Address!", "Ok");
            return;
        }
        //Call API to see if login is valid
        ZGT.Trouble.BL.Models.Player player = new ZGT.Trouble.BL.Models.Player { UserName = txtUser.Text,
            Password = txtPass.Text, Email = txtEmail.Text, NumberOfWins = 0, DateJoined = DateTime.Now };
        HttpClient client = new HttpClient();
        var data = JsonConvert.SerializeObject(player);
        var response = await client.PostAsync(new Uri(APIAddress + "player/false"), new StringContent(data, System.Text.Encoding.UTF8, "application/json"));
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Title = player.UserName;
            string result = response.Content.ReadAsStringAsync().Result;
            result = result.Replace("\'", "").Replace("\\", "").Replace("\"", "").Trim(); //Return string has quotes inside the string
            App.PlayerID = new Guid(result);  //Entire app knows the PlayerID that logs in.
            await DisplayAlert("Success", "Account Created: " + player.UserName, "Ok");
        }
            
        else
            Title = response.StatusCode.ToString();
    }
}
/*
var data = JsonConvert.SerializeObject(player);
HttpClient client = new HttpClient();
//var response = await client.PostAsync(new Uri("https://joewetzel.com/fvtc/account/createuser"), new StringContent(data, System.Text.Encoding.UTF8, "application/json"));
//var accountStatus = response.Content.ReadAsStringAsync().Result;

// user exists
// email exists
// complete

if (accountStatus == "user exists")
{
    await DisplayAlert("Errror", "Username is already taken!", "Ok");
    return;
}

if (accountStatus == "email exists")
{
    await DisplayAlert("Errror", "This email is already in use!  Try Logging In", "Ok");
    return;
}


if (accountStatus == "complete")
{
    //response = await client.PostAsync(new Uri("https://joewetzel.com/fvtc/account/login"), new StringContent(data, System.Text.Encoding.UTF8, "application/json"));
    var sKey = response.Content.ReadAsStringAsync().Result;

    if (string.IsNullOrEmpty(sKey))
    {
        await DisplayAlert("Errror", "Error occurred logging In", "Ok");
        return;
    }
    else
    {
        App.SessionKey = sKey;
        //(MainPage.main_page as MainPage)?.inputKey();
        await Shell.Current.GoToAsync("///JoinGameRoomPage");
    }

}
else
{
    await DisplayAlert("Errror", "Account Creation Error!", "Ok");
    return;
}
*/

