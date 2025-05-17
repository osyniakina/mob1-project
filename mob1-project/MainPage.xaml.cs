

namespace mob1_project
{
    public partial class MainPage : ContentPage
    {
        public string inp_uzytkownik = "";
        public string inp_haslo = "";
        public string nameadmin = "qwert";
        public string passadmin = "123";

        private DBHelper _databaseHelper;

        public MainPage()
        {
            InitializeComponent();
            _databaseHelper = new DBHelper();
            _ = _databaseHelper.AddSampleRestauracjeAsync();
        }


        public async void ZalogBtnClicked(object sender, EventArgs e)
        {
            if (ZalogInpUzytkownik == null || ZalogInpHaslo == null)
            {
                await DisplayAlert("Błąd", "Wszystkie pola muszą być wypełnione", "OK");
                return;
            }

            inp_uzytkownik = ZalogInpUzytkownik.Text;
            inp_haslo = ZalogInpHaslo.Text;
            var user = await _databaseHelper.GetKontaByNamePassAsync(inp_uzytkownik, inp_haslo);

            if (string.IsNullOrEmpty(inp_uzytkownik) || string.IsNullOrEmpty(inp_haslo))
            {
                await DisplayAlert("Błąd", "Wszystkie pola muszą być wypełnione", "OK");
            }
            else if (inp_uzytkownik == nameadmin && inp_haslo == passadmin)
            {
                await Navigation.PushAsync(new AdminPage());
            }
            else if (user != null)
            {
                await Navigation.PushAsync(new LocPage());
            }
            else
            {
                await DisplayAlert("Błąd", "Nazwa lub hasło są niepoprawne", "OK");
            }
        }

        //private void ZalogBtn_Focused(object sender, FocusEventArgs e)
        //{
        //    ZalogBtn.TextColor = Colors.Black;
        //    ZalogBtn.BackgroundColor = Colors.White;
        //}

        //private void ZalogBtn_Unfocused(object sender, FocusEventArgs e)
        //{
        //    ZalogBtn.TextColor = Colors.White;
        //    ZalogBtn.BackgroundColor = Color.Parse("#ff6600");
        //}

        private void UtwKontBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StwKontoPage());
        }
    }

}