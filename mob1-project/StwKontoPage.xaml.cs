using Microsoft.Maui.Controls;

namespace mob1_project;

public partial class StwKontoPage : ContentPage
{
    public string inp_uzytkownik = "";
    public string inp_haslo = "";
    public string inp_tel = "";
    public string name = "qwert";
    public string pass = "123";

    private DBHelper _databaseHelper;

    public StwKontoPage()
	{
		InitializeComponent();
        _databaseHelper = new DBHelper();
    }

    public async void StwBtn_Clicked(object sender, EventArgs e)
    {
        
        if (string.IsNullOrEmpty(inp_uzytkownik) || string.IsNullOrEmpty(inp_haslo) || string.IsNullOrEmpty(inp_tel))
        {
            Console.WriteLine("Isn't entered");
        }
        else {

        }

        if (StwKontoInpUzytkownik == null || StwKontoInpHaslo == null || StwKontoInpTel == null)
        {
            await DisplayAlert("Błąd", "Wszystkie pola muszą być wypełnione", "OK");
            return;
        }

        inp_uzytkownik = StwKontoInpUzytkownik.Text;
        inp_haslo = StwKontoInpHaslo.Text;
        inp_tel = StwKontoInpTel.Text;
        var user = await _databaseHelper.GetKontaByNamePassAsync(inp_uzytkownik, inp_haslo);

        if (string.IsNullOrEmpty(inp_uzytkownik) || string.IsNullOrEmpty(inp_haslo) || string.IsNullOrEmpty(inp_tel))
        {
            await DisplayAlert("Błąd", "Wszystkie pola muszą być wypełnione", "OK");
            return;
        }
        else if (user != null)   
        {
            await DisplayAlert("Błąd", "Taki użytkownik już istnieje", "OK");
            return;
        }
        else
        {
            var newAccount = new Konta                                      //dodawanie do db
            {
                Nazwa = inp_uzytkownik,
                Haslo = inp_haslo,
                Telefon = inp_tel
            };

            int result = await _databaseHelper.AddKontaAsync(newAccount);

            if (result > 0)
            {
                await DisplayAlert("Witaj!", "Witaj " + inp_uzytkownik + " w Smaczne.pl!", "OK");
                await Navigation.PushAsync(new LocPage());
            }
            else
            {
                Console.WriteLine("faled");
                await DisplayAlert("Error", "Nie udało się założyć konta!", "OK");
            }


            //var accounts = await _databaseHelper.GetKontasAsync();

            //if (accounts.Count == 0)
            //{
            //    Console.WriteLine("❌ No accounts found in the database!");
            //    await DisplayAlert("Error", "No accounts found!", "OK");
            //}
            //else
            //{
            //    foreach (var account in accounts)
            //    {
            //        Console.WriteLine($"📌 Id: {account.Id}, Nazwa: {account.Nazwa}, Telefon: {account.Telefon}");
            //    }
            //    await DisplayAlert("Success", $"Loaded {accounts.Count} accounts!", "OK");
            //}

        }
    }

    private void StwBtn_Focused(object sender, FocusEventArgs e)
    {
        StwBtn.TextColor = Colors.Black;
        StwBtn.BackgroundColor = Colors.White;
    }

    private void ZalBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void StwBtn_Unfocused(object sender, FocusEventArgs e)
    {
        StwBtn.TextColor = Colors.White;
        StwBtn.BackgroundColor = Color.Parse("#ff6600");
    }
}