namespace mob1_project;

public partial class MenuRestPage : ContentPage
{
    private DBHelper _databaseHelper;
    private Restauracja _rest;
    private Dictionary<int, int> ilosciWybranych = new Dictionary<int, int>();   // przechowuje: id_dania, ilość
    private readonly Dictionary<int, string> nazwyKategorii = new()
    {
        { 1, "Dania główne" },
        { 2, "Zupy" },
        { 3, "Burgery" },
        { 4, "Pizza" },
        { 5, "Zakąski" },
        { 6, "Desery" },
        { 7, "Napoje i dodatki" }
    };

    public MenuRestPage(Restauracja rest)
    {
        InitializeComponent();
        _rest = rest;
        _databaseHelper = new DBHelper();
        _ = _databaseHelper.AddSampleRestauracjeAsync();

        nazwaLabel.Text = _rest.Nazwa+"  "+_rest.CzasPracy;
        LoadMenu();
    }


    private async void LoadMenu()
    {
        var daniaWszystkie = await _databaseHelper.GetDaniaAsync(); // pobiera wszystkie dania
        var dania = daniaWszystkie
            .Where(d => d.Restauracja == _rest.Nazwa) // porównanie nazw restauracji
            .ToList();
        var pogrupowane = dania
            .GroupBy(d => d.Kategoria)
            .OrderBy(g => g.Key); // kolejność wg kategorii

        foreach (var grupa in pogrupowane)
        {
            if (!nazwyKategorii.TryGetValue(grupa.Key, out string nazwaKat))
                continue;

            // Nagłówek kategorii
            menu.Children.Add(new Label
            {
                Text = nazwaKat,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.White,
                Margin = new Thickness(5, 10, 5, 5)
            });

            foreach (var danie in grupa)
            {
                var layout = new Microsoft.Maui.Controls.Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Star },   // Nazwa
                        new ColumnDefinition { Width = GridLength.Auto },   // Cena
                        new ColumnDefinition { Width = GridLength.Auto },   // -
                        new ColumnDefinition { Width = GridLength.Auto },   // Ilość
                        new ColumnDefinition { Width = GridLength.Auto }    // +
                    },
                    Margin = new Thickness(10, 5),
                    BackgroundColor = Colors.White
                };


                var labelNazwa = new Label
                {
                    Text = danie.Nazwa,
                    FontSize = 16,
                    TextColor = Colors.Black
                };
                var labelCena = new Label
                {
                    Text = $"{danie.Cena} zł",
                    FontSize = 16,
                    TextColor = Colors.Black,
                    Margin = new Thickness(10, 0)
                };

                if (!ilosciWybranych.ContainsKey(danie.Id))
                    ilosciWybranych[danie.Id] = 0;

                var labelIlosc = new Label
                {
                    Text = ilosciWybranych[danie.Id].ToString(),
                    FontSize = 16,
                    TextColor = Colors.Black,
                    HorizontalTextAlignment = TextAlignment.Center,
                    WidthRequest = 30,
                    HorizontalOptions = LayoutOptions.Center
                };

                var buttonMinus = new Button
                {
                    Text = "-",
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Orange,
                    Padding = new Thickness(5, 0),
                    WidthRequest = 30,
                    HeightRequest = 30,
                    CornerRadius = 15,
                    FontSize = 16
                };
                buttonMinus.Clicked += (s, e) =>
                {
                    if (ilosciWybranych[danie.Id] > 0)
                    {
                        ilosciWybranych[danie.Id]--;
                        labelIlosc.Text = ilosciWybranych[danie.Id].ToString();
                    }
                };

                var buttonPlus = new Button
                {
                    Text = "+",
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Orange,
                    Padding = new Thickness(5, 0),
                    WidthRequest = 30,
                    HeightRequest = 30,
                    CornerRadius = 15,
                    FontSize = 16
                };
                buttonPlus.Clicked += (s, e) =>
                {
                    ilosciWybranych[danie.Id]++;
                    labelIlosc.Text = ilosciWybranych[danie.Id].ToString();
                };

                // Dodawanie elementów do layoutu
                Grid.SetColumn(labelNazwa, 0);
                Grid.SetRow(labelNazwa, 0);
                layout.Children.Add(labelNazwa);
                Grid.SetColumn(labelCena, 1);
                Grid.SetRow(labelCena, 0);
                layout.Children.Add(labelCena);
                Grid.SetColumn(buttonMinus, 2);
                Grid.SetRow(buttonMinus, 0);
                layout.Children.Add(buttonMinus);
                Grid.SetColumn(labelIlosc, 3);
                Grid.SetRow(labelIlosc, 0);
                layout.Children.Add(labelIlosc);
                Grid.SetColumn(buttonPlus, 4);
                Grid.SetRow(buttonPlus, 0);
                layout.Children.Add(buttonPlus);

                menu.Children.Add(layout);
            }
        }
    }

    private async void BtnWrocMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}