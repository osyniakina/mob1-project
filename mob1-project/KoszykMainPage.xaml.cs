namespace mob1_project;

public partial class KoszykMainPage : ContentPage
{
    private DBHelper _databaseHelper;
    private Dictionary<int, int> _ilosciWybranych;
    private float _koszykCena;
    

    public KoszykMainPage(Dictionary<int, int> ilosciWybranych, float koszykCena)
    {
        InitializeComponent();
        _databaseHelper = new DBHelper();
        _ilosciWybranych = ilosciWybranych;
        _koszykCena = koszykCena;
        
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var dania = await _databaseHelper.GetDaniaAsync();


        foreach (var kvp in _ilosciWybranych.Where(kvp => kvp.Value > 0))    //koszykWybraneProdukty
        {
            var danie = dania.FirstOrDefault(d => d.Id == kvp.Key);
            if (danie != null)
            {
                var label = new Label
                {
                    Text = $"{danie.Nazwa}       x  {kvp.Value}        {(kvp.Value * danie.Cena):F2} zł"
                };
                koszykLayout.Children.Add(label); 
            }
        }

        razem.Text = $"Razem: {_koszykCena:F2} zł"; 
    }

    private void zamButton_Clicked(object sender, EventArgs e)
    {

    }
}