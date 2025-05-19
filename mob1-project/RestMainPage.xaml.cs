using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using System;
using System.Linq;

namespace mob1_project;

public partial class RestMainPage : ContentPage
{
    private DBHelper _databaseHelper;
    private string miasto;
    private string kategoria;

    public class CategoryToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string categoryName)
            {
                return $"Images/Kategorie/{categoryName}.jpg";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public RestMainPage(string Wybmiasto) : this(Wybmiasto, null)
    {
    }

    public RestMainPage(string Wybmiasto, string wybranaKategoria)
    {
        InitializeComponent();
        _databaseHelper = new DBHelper();

        miasto = Wybmiasto;
        kategoria = wybranaKategoria;

        _ = _databaseHelper.AddSampleRestauracjeAsync();
        _ = _databaseHelper.AddRestauracje2Async();
        _ = _databaseHelper.AddDaniaAsync();

        LoadRestaurants();
    }

    private async void LoadRestaurants()
    {
        if (lista == null)
        {
            System.Diagnostics.Debug.WriteLine("Kontener 'lista' nie został znaleziony w XAML! Upewnij się, że masz element z x:Name=\"lista\".");
            return;
        }

        var restaurants = await _databaseHelper.GetRestAsync();

        var filtrowane = restaurants
    .Where(r => string.Equals(r.Miasto, miasto, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(kategoria))
        {
            filtrowane = filtrowane.Where(r => string.Equals(r.Kategoria, kategoria, StringComparison.OrdinalIgnoreCase));
        }

        var listaFinalna = filtrowane.ToList();

        lista.Children.Clear();

        foreach (var rest in filtrowane)
        {
            var itemBorder = new Border
            {
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(10) },
                Stroke = Colors.Transparent,
                BackgroundColor = Colors.White,
                Padding = new Thickness(10),
                Margin = new Thickness(10, 5),
                Shadow = new Shadow
                {
                    Brush = Colors.Black,
                    Offset = new Point(2, 2),
                    Radius = 5,
                    Opacity = 0.3F
                }
            };

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += async (s, e) =>
            {
                await Navigation.PushAsync(new MenuRestPage(rest));
            };
            itemBorder.GestureRecognizers.Add(tapGesture);

            var itemGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                },
                ColumnSpacing = 10
            };

            // Obrazek restauracji z użyciem konwertera
            var itemImage = new Image
            {
                WidthRequest = 80,
                HeightRequest = 80,
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.Center,
                BindingContext = rest // Ważne: ustaw kontekst powiązania PRZED bindingiem
            };
            itemImage.SetBinding(Image.SourceProperty, new Binding(
                "Kategoria", // Ścieżka do właściwości (nazwa kategorii)
                converter: new CategoryToImageConverter() // Instancja Twojego konwertera
            ));

            var imageBorder = new Border
            {
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(8) },
                Stroke = Colors.Transparent,
                Padding = 0,
                Content = itemImage,
                WidthRequest = 80,
                HeightRequest = 80,
                Margin = new Thickness(0)
            };
            Grid.SetColumn(imageBorder, 0);
            Grid.SetRowSpan(imageBorder, 3);
            itemGrid.Children.Add(imageBorder);

            var detailsStack = new VerticalStackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Spacing = 2
            };
            Grid.SetColumn(detailsStack, 1);
            Grid.SetRowSpan(detailsStack, 3);
            itemGrid.Children.Add(detailsStack);

            var labelTitle = new Label
            {
                Text = rest.Nazwa,
                FontSize = 16,
                TextColor = Colors.Black,
                FontAttributes = FontAttributes.Bold,
                LineBreakMode = LineBreakMode.WordWrap
            };
            detailsStack.Children.Add(labelTitle);

            var labelCategory = new Label
            {
                Text = rest.Kategoria,
                FontSize = 14,
                TextColor = Colors.Gray,
                LineBreakMode = LineBreakMode.WordWrap
            };
            detailsStack.Children.Add(labelCategory);

            var labelTimeDelivery = new Label
            {
                Text = rest.CzasDostawy,
                TextColor = Colors.Black,
                FontSize = 14
            };
            detailsStack.Children.Add(labelTimeDelivery);

            var labelDelivery = new Label
            {
                Text = (rest.KosztDostawy.ToLower().Contains("darmowa") || rest.KosztDostawy == "0" || rest.KosztDostawy == "0 zł")
                            ? "Darmowa"
                            : $"Dostawa {rest.KosztDostawy}",
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = (rest.KosztDostawy.ToLower().Contains("darmowa") || rest.KosztDostawy == "0" || rest.KosztDostawy == "0 zł") ? Colors.Green : Colors.OrangeRed,
                HorizontalTextAlignment = TextAlignment.End,
                VerticalOptions = LayoutOptions.End
            };
            Grid.SetColumn(labelDelivery, 2);
            Grid.SetRow(labelDelivery, 2);
            itemGrid.Children.Add(labelDelivery);

            itemBorder.Content = itemGrid;
            lista.Children.Add(itemBorder);
        }
    }

    private void BtnSzukajRest_Clicked(object sender, EventArgs e)
    {
        // Obsługa wyszukiwania
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
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}