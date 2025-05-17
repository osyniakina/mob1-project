using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics; // Upewnij się, że masz ten using
// using static System.Net.Mime.MediaTypeNames; // To nie jest potrzebne do UI, można usunąć

namespace mob1_project;

public partial class RestMainPage : ContentPage
{
    private DBHelper _databaseHelper;
    private string miasto;

    public RestMainPage(string Wybmiasto)
    {
        InitializeComponent();
        _databaseHelper = new DBHelper();

        // Te linijki mogą być problematyczne, jeśli dane są dodawane za każdym uruchomieniem.
        // Upewnij się, że dane są dodawane tylko raz (np. przy pierwszym uruchomieniu aplikacji)
        // lub że logika Add...Async obsługuje duplikaty.
        _ = _databaseHelper.AddSampleRestauracjeAsync();
        _ = _databaseHelper.AddRestauracje2Async();
        _ = _databaseHelper.AddDaniaAsync();

        miasto = Wybmiasto;
        LoadRestaurants();
    }

    private async void LoadRestaurants()
    {
        // Upewnij się, że kontener 'lista' istnieje w XAML i ma x:Name="lista"
        if (lista == null)
        {
            System.Diagnostics.Debug.WriteLine("Kontener 'lista' nie został znaleziony w XAML! Upewnij się, że masz element z x:Name=\"lista\".");
            return;
        }

        var restaurants = await _databaseHelper.GetRestAsync();

        var filtrowane = restaurants
           .Where(r => string.Equals(r.Miasto, miasto, StringComparison.OrdinalIgnoreCase))
           .ToList();

        lista.Children.Clear(); // Wyczyść istniejące elementy przed dodaniem nowych

        foreach (var rest in filtrowane)
        {
            var itemBorder = new Border // Zmieniono nazwę zmiennej na itemBorder dla spójności
            {
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(10) },
                Stroke = Colors.Transparent, // Brak widocznego obramowania
                BackgroundColor = Colors.White,
                Padding = new Thickness(10), // Padding wewnątrz Border
                Margin = new Thickness(10, 5), // Marginesy między elementami i od krawędzi
                Shadow = new Shadow // Dodajemy cień!
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
            itemBorder.GestureRecognizers.Add(tapGesture); // Przypisujemy gest do itemBorder

            var itemGrid = new Grid // Zmieniono nazwę zmiennej na itemGrid dla spójności
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto }, // Kolumna na obrazek
                    new ColumnDefinition { Width = GridLength.Star }, // Kolumna na szczegóły
                    new ColumnDefinition { Width = GridLength.Auto }  // Kolumna na koszt dostawy (jeśli jest)
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto }, // Nazwa restauracji
                    new RowDefinition { Height = GridLength.Auto }, // Czas dostawy
                    new RowDefinition { Height = GridLength.Auto }  // Czas pracy / Koszt dostawy
                },
                ColumnSpacing = 10 // Odstęp między kolumnami dla lepszego wyglądu
            };

            // Obrazek restauracji
            var itemImage = new Image
            {
                Source = "rest.jpg", // Tutaj prawdopodobnie będziesz chciał użyć rest.ImageSource lub podobnie
                WidthRequest = 80,
                HeightRequest = 80,
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.Center
            };

            // Obrazek w Border dla zaokrąglonych rogów, tak jak na stronie menu
            var imageBorder = new Border
            {
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(8) }, // Zaokrąglone rogi obrazka
                Stroke = Colors.Transparent, // Brak obramowania obrazka
                Padding = 0,
                Content = itemImage,
                WidthRequest = 80,
                HeightRequest = 80,
                Margin = new Thickness(0) // Margines 0 wewnątrz itemGrid
            };
            Grid.SetColumn(imageBorder, 0);
            Grid.SetRowSpan(imageBorder, 3); // Rozciągnij obrazek na 3 rzędy
            itemGrid.Children.Add(imageBorder);


            // Szczegóły restauracji (Nazwa, Kategoria, Czas Dostawy, Czas Pracy)
            var detailsStack = new VerticalStackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Spacing = 2 // Mały odstęp między elementami w StackLayout
            };
            Grid.SetColumn(detailsStack, 1);
            Grid.SetRowSpan(detailsStack, 3); // Rozciągnij na wszystkie 3 rzędy w drugiej kolumnie
            itemGrid.Children.Add(detailsStack);

            var labelTitle = new Label
            {
                Text = rest.Nazwa, // "McDonald's"
                FontSize = 16,
                TextColor = Colors.Black,
                FontAttributes = FontAttributes.Bold,
                LineBreakMode = LineBreakMode.WordWrap // Dodałem dla długich nazw
            };
            detailsStack.Children.Add(labelTitle);

            var labelCategory = new Label
            {
                Text = rest.Kategoria, // "Burgery"
                FontSize = 14,
                TextColor = Colors.Gray,
                LineBreakMode = LineBreakMode.WordWrap
            };
            detailsStack.Children.Add(labelCategory);

            var labelTimeDelivery = new Label
            {
                Text = rest.CzasDostawy, // "30-45 min"
                TextColor = Colors.Black,
                FontSize = 14
            };
            detailsStack.Children.Add(labelTimeDelivery);

            // Koszt dostawy - osobna kolumna, na prawo, wyrównana do dołu
            var labelDelivery = new Label
            {
                Text = (rest.KosztDostawy.ToLower().Contains("darmowa") || rest.KosztDostawy == "0" || rest.KosztDostawy == "0 zł")
                       ? "Darmowa" // Zmieniono na "Darmowa" dla spójności
                       : $"Dostawa {rest.KosztDostawy}",
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = (rest.KosztDostawy.ToLower().Contains("darmowa") || rest.KosztDostawy == "0" || rest.KosztDostawy == "0 zł") ? Colors.Green : Colors.OrangeRed, // Zielony dla darmowej, pomarańczowy dla płatnej
                HorizontalTextAlignment = TextAlignment.End, // Wyrównanie do prawej
                VerticalOptions = LayoutOptions.End // Wyrównanie do dołu
            };
            Grid.SetColumn(labelDelivery, 2);
            Grid.SetRow(labelDelivery, 2); // Umieść w ostatnim rzędzie, ostatniej kolumnie
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
        Navigation.PushAsync(new KategoriePage());
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
            // Pamiętaj, że PushAsync dodaje stronę na stos. Jeśli chcesz usunąć bieżący stos
            // i wrócić do strony logowania, lepiej użyć Application.Current.MainPage = new MainPage();
            // lub ClearNavigationStack (jeśli to możliwe, zależy od struktury nawigacji).
            // W tym przypadku (wylogowanie) zazwyczaj czyści się stos.
            // await Navigation.PushAsync(new MainPage());
            Application.Current.MainPage = new NavigationPage(new MainPage()); // Tworzy nowy stos nawigacji
        }
    }
}

// Upewnij się, że masz odpowiednie klasy:
// public class DBHelper { /* ... */ }
// public class Restauracja { public string Nazwa { get; set; } public string Kategoria { get; set; } public string CzasDostawy { get; set; } public string CzasPracy { get; set; } public string KosztDostawy { get; set; } /* Dodaj ImageSource jeśli masz */ }
// public class MenuRestPage : ContentPage { /* ... */ }
// public class LocPage : ContentPage { /* ... */ }
// public class MainPage : ContentPage { /* ... */ }