using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Shapes; // Dodano dla RoundRectangle i Border
// Dodaj tutaj inne usingi, które były w Twoim oryginalnym kodzie, np. dla DBHelper i modeli danych (Restauracja, Danie)
// Upewnij się, że przestrzenie nazw dla Twoich modeli i pomocników są tutaj uwzględnione
// using TwojaNazwaProjektu.Models;
// using TwojaNazwaProjektu.Helpers;

namespace mob1_project
{
    public partial class MenuRestPage : ContentPage
    {
        private DBHelper _databaseHelper;
        private Restauracja _rest;
        private Dictionary<int, int> ilosciWybranych = new Dictionary<int, int>();   // przechowuje: id_dania, ilość
        public float koszykCena = 0f;
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

            if (nazwaLabel != null)
            {
                nazwaLabel.Text = _rest.Nazwa + " - " + _rest.CzasPracy;
            }
            if (koszykButton != null)
            {
                koszykButton.Text = "Przejdź do koszyka: " + koszykCena.ToString("F2") + " zł";
            }

            LoadMenu();
        }

        private async void LoadMenu()
        {
            if (menu == null)
            {
                System.Diagnostics.Debug.WriteLine("Kontener 'menu' nie został znaleziony w XAML! Upewnij się, że masz element z x:Name=\"menu\".");
                return;
            }

            var daniaWszystkie = await _databaseHelper.GetDaniaAsync();
            var dania = daniaWszystkie
                .Where(d => d.Restauracja == _rest.Nazwa)
                .ToList();
            var pogrupowane = dania
                .GroupBy(d => d.Kategoria)
                .OrderBy(g => g.Key);

            menu.Children.Clear();

            foreach (var grupa in pogrupowane)
            {
                if (!nazwyKategorii.TryGetValue(grupa.Key, out string nazwaKat))
                    continue;

                menu.Children.Add(new Label
                {
                    Text = nazwaKat,
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.Black,
                    Margin = new Thickness(15, 10, 5, 5)
                });

                foreach (var danie in grupa)
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

                    var itemGrid = new Grid
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto }, // Kolumna na obrazek
                            new ColumnDefinition { Width = GridLength.Star }  // Kolumna na szczegóły
                        }
                    };

                    var itemImage = new Image
                    {
                        Source = "rest.jpg",
                        WidthRequest = 80,
                        HeightRequest = 80,
                        Aspect = Aspect.AspectFill,
                        VerticalOptions = LayoutOptions.Center
                    };

                    var imageBorder = new Border
                    {
                        StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(8) },
                        Stroke = Colors.Transparent,
                        Padding = 0,
                        Content = itemImage,
                        WidthRequest = 80,
                        HeightRequest = 80,
                        Margin = new Thickness(0, 0, 10, 0)
                    };
                    Grid.SetColumn(imageBorder, 0);
                    itemGrid.Children.Add(imageBorder);

                    var detailsLayout = new Grid
                    {
                        VerticalOptions = LayoutOptions.Fill,
                        RowDefinitions =
                        {
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Star }
                        }
                    };
                    Grid.SetColumn(detailsLayout, 1);
                    itemGrid.Children.Add(detailsLayout);

                    var labelNazwa = new Label
                    {
                        Text = danie.Nazwa,
                        FontSize = 16,
                        TextColor = Colors.Black,
                        FontAttributes = FontAttributes.Bold,
                        LineBreakMode = LineBreakMode.WordWrap
                    };
                    Grid.SetRow(labelNazwa, 0);
                    detailsLayout.Children.Add(labelNazwa);

                    var priceQuantityLayout = new Grid
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Star },
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Auto }
                        },
                        VerticalOptions = LayoutOptions.Center
                    };
                    Grid.SetRow(priceQuantityLayout, 1);
                    detailsLayout.Children.Add(priceQuantityLayout);

                    var labelCena = new Label
                    {
                        Text = $"{danie.Cena:F2} zł",
                        FontSize = 16,
                        TextColor = Colors.Green,
                        VerticalOptions = LayoutOptions.Center
                    };
                    Grid.SetColumn(labelCena, 0);
                    priceQuantityLayout.Children.Add(labelCena);

                    if (!ilosciWybranych.ContainsKey(danie.Id))
                    {
                        ilosciWybranych[danie.Id] = 0;
                    }

                    var labelIlosc = new Label
                    {
                        Text = ilosciWybranych[danie.Id].ToString(),
                        FontSize = 16,
                        TextColor = Colors.Black,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = 30,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(0, 0, 0, 20) // <-- margines dolny 15
                    };
                    Grid.SetColumn(labelIlosc, 3);
                    priceQuantityLayout.Children.Add(labelIlosc);

                    var buttonMinus = new Button
                    {
                        Text = "-",
                        BackgroundColor = Color.FromHex("#FFA500"),
                        TextColor = Colors.White,
                        Padding = new Thickness(0),
                        WidthRequest = 30,
                        HeightRequest = 30,
                        CornerRadius = 15,
                        FontSize = 16,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(5, 0, 0, 20)
                    };
                    buttonMinus.Clicked += (s, e) =>
                    {
                        if (ilosciWybranych.ContainsKey(danie.Id) && ilosciWybranych[danie.Id] > 0)
                        {
                            ilosciWybranych[danie.Id]--;
                            koszykCena -= (float)danie.Cena;
                            if (koszykButton != null)
                            {
                                koszykButton.Text = "Przejdź do koszyka: " + koszykCena.ToString("F2") + " zł";
                            }
                            labelIlosc.Text = ilosciWybranych[danie.Id].ToString();
                        }
                    };
                    Grid.SetColumn(buttonMinus, 2);
                    priceQuantityLayout.Children.Add(buttonMinus);

                    var buttonPlus = new Button
                    {
                        Text = "+",
                        BackgroundColor = Color.FromHex("#FFA500"),
                        TextColor = Colors.White,
                        Padding = new Thickness(0),
                        WidthRequest = 30,
                        HeightRequest = 30,
                        CornerRadius = 15,
                        FontSize = 16,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(5, 0, 0, 20)
                    };
                    buttonPlus.Clicked += (s, e) =>
                    {
                        if (!ilosciWybranych.ContainsKey(danie.Id))
                        {
                            ilosciWybranych[danie.Id] = 0;
                        }
                        ilosciWybranych[danie.Id]++;
                        koszykCena += (float)danie.Cena;
                        if (koszykButton != null)
                        {
                            koszykButton.Text = "Przejdź do koszyka: " + koszykCena.ToString("F2") + " zł";
                        }
                        labelIlosc.Text = ilosciWybranych[danie.Id].ToString();
                    };
                    Grid.SetColumn(buttonPlus, 4);
                    priceQuantityLayout.Children.Add(buttonPlus);

                    itemBorder.Content = itemGrid;
                    menu.Children.Add(itemBorder);
                }
            }
        }

        private async void BtnWrocMenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void koszykButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KoszykMainPage());
        }
    }

    // Przykładowe klasy (dopasuj do projektu):
    // public class DBHelper { public Task<List<Danie>> GetDaniaAsync() => Task.FromResult(new List<Danie>()); }
    // public class Restauracja { public string Nazwa { get; set; } public string CzasPracy { get; set; } }
    // public class Danie { public int Id { get; set; } public string Nazwa { get; set; } public double Cena { get; set; } public int Kategoria { get; set; } public string Restauracja { get; set; } }
    // public class KoszykMainPage : ContentPage { /* ... */ }
}
