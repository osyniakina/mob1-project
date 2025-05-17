using Microsoft.Maui.Controls.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace mob1_project;

public partial class RestMainPage : ContentPage
{
    private DBHelper _databaseHelper;
    private string miasto;
    public RestMainPage(string Wybmiasto)
	{
		InitializeComponent();
        _databaseHelper = new DBHelper();
        _ = _databaseHelper.AddSampleRestauracjeAsync();
        _ = _databaseHelper.AddRestauracje2Async();
        _ = _databaseHelper.AddDaniaAsync();
        miasto = Wybmiasto;
        LoadRestaurants();

    }


    private async void LoadRestaurants()
    {
        var restaurants = await _databaseHelper.GetRestAsync();

        var filtrowane = restaurants
       .Where(r => string.Equals(r.Miasto, miasto, StringComparison.OrdinalIgnoreCase))
       .ToList();

        foreach (var rest in filtrowane)
        {
            var frame = new Border
            {
                Stroke = Colors.Orange,
                StrokeThickness = 1,
                Background = Colors.White,
                Margin = new Thickness(0, 10),
                Padding = 0,
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(20) }
            };

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += async (s, e) =>
            {
                await Navigation.PushAsync(new MenuRestPage(rest));
            };
            frame.GestureRecognizers.Add(tapGesture);



            var grid = new Grid
            {
                ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(100) },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition {}
            },
                RowDefinitions =
            {
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition()
            }
            };

            var image = new Microsoft.Maui.Controls.Image
            {
                Source = "rest.jpg",
                WidthRequest = 80,
                HeightRequest = 80,
                Margin = 10
            };
            Grid.SetRowSpan(image, 3);
            grid.Children.Add(image);

            var labelTitle = new Label
            {
                Text = rest.Nazwa + "   •  " + rest.Kategoria,
                FontSize = 16,
                TextColor = Colors.Black,
                FontAttributes = FontAttributes.Bold
            };
            Grid.SetColumn(labelTitle, 1);
            Grid.SetRow(labelTitle, 0);
            grid.Children.Add(labelTitle);

            var labelTime = new Label
            {
                Text = rest.CzasDostawy,
                TextColor = Colors.Black,
                FontSize = 14
            };
            Grid.SetColumn(labelTime, 1);
            Grid.SetRow(labelTime, 1);
            grid.Children.Add(labelTime);

            var labelFromTo = new Label
            {
                Text = rest.CzasPracy,
                TextColor = Colors.Black,
                FontSize = 14
            };
            Grid.SetColumn(labelFromTo, 1);
            Grid.SetRow(labelFromTo, 2);
            grid.Children.Add(labelFromTo);


            bool isFreeDelivery = rest.KosztDostawy.ToLower().Contains("darmowa") || rest.KosztDostawy == "0" || rest.KosztDostawy == "0 zł";

            string kosztText = (rest.KosztDostawy == "5 zł" || rest.KosztDostawy == "6 zł")
                ? $"Dostawa {rest.KosztDostawy}"
                : rest.KosztDostawy;

            var labelDelivery = new Label
            {
                Text = kosztText,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = isFreeDelivery ? Colors.OrangeRed : Colors.Gray
            };
            Grid.SetColumn(labelDelivery, 2);
            Grid.SetRow(labelDelivery, 2);
            grid.Children.Add(labelDelivery);

            frame.Content = grid;

            lista.Children.Add(frame);
        }
    }




    private void BtnSzukajRest_Clicked(object sender, EventArgs e)
    {

    }

    private async void BtnKategorie_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KategoriePage());
    }

    private void BtnZmienMiasto_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LocPage());

    }

    private async void BtnWylog_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Wylogowanie", "Czy na pewno chcesz się wylogować?", "Tak", "Nie");
        if (confirm)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }

}