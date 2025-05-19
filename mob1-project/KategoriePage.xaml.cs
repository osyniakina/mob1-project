namespace mob1_project;

public partial class KategoriePage : ContentPage
{
	public KategoriePage()
	{
		InitializeComponent();
	}

    private async void BtnKatAmerykanska_Clicked(object sender, EventArgs e)
    {
        var miasto = Preferences.Get("Miasto", "DomyślneMiasto");
        await Navigation.PushAsync(new RestMainPage(miasto, "Amerykanska"));
    }

    private async void BtnKatWloska_Clicked(object sender, EventArgs e)
    {
        var miasto = Preferences.Get("Miasto", "DomyślneMiasto");
        await Navigation.PushAsync(new RestMainPage(miasto, "Wloska"));

    }

    private async void BtnKatAzjatycka_Clicked(object sender, EventArgs e)
    {
        var miasto = Preferences.Get("Miasto", "DomyślneMiasto");
        await Navigation.PushAsync(new RestMainPage(miasto, "Azjatycka"));
    }

    private async void BtnKatEuropejska_Clicked(object sender, EventArgs e)
    {
        var miasto = Preferences.Get("Miasto", "DomyślneMiasto");
        await Navigation.PushAsync(new RestMainPage(miasto, "Europejska"));
    }

   
}