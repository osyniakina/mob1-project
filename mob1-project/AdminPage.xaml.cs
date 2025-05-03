namespace mob1_project;

public partial class AdminPage : ContentPage
{
    private DBHelper _databaseHelper;
    public AdminPage()
	{
		InitializeComponent();
        _databaseHelper = new DBHelper();
        

        LoadAccounts(); // Wywołujemy ładowanie danych
        LoadRest();
    }

    


    private async void LoadAccounts()
    {
        var accounts = await _databaseHelper.GetKontasAsync();

        if (accounts == null || accounts.Count == 0)
        {
            await DisplayAlert("Uwaga", "Brak kont w bazie danych!", "OK");
        }
        else
        {
            UsersListView.ItemsSource = accounts; 
        }
    }

    private async void LoadRest()
    {
        var rests = await _databaseHelper.GetRestAsync();

        if (rests == null || rests.Count == 0)
        {
            await DisplayAlert("Uwaga", "Brak restauracji w bazie danych!", "OK");
        }
        else
        {
            RestsListView.ItemsSource = rests;
        }
    }
}