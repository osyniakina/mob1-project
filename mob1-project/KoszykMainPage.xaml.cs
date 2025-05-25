using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using mob1_project.Pages;

namespace mob1_project
{
    public partial class KoszykMainPage : ContentPage
    {
        private DBHelper _databaseHelper;
        private Dictionary<int, int> _ilosciWybranych;
        private float _koszykCena;
        private Restauracja _restauracja;

        public KoszykMainPage(Dictionary<int, int> ilosciWybranych, float koszykCena, Restauracja restauracja)
        {
            InitializeComponent();
            _databaseHelper = new DBHelper();
            _ilosciWybranych = ilosciWybranych;
            _koszykCena = koszykCena;
            _restauracja = restauracja;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var dania = await _databaseHelper.GetDaniaAsync();

            koszykLayout.Children.Clear();

            foreach (var kvp in _ilosciWybranych.Where(kvp => kvp.Value > 0).ToList())
            {
                var danie = dania.FirstOrDefault(d => d.Id == kvp.Key);
                if (danie != null)
                {
                    var itemLabel = new Label
                    {
                        Text = $"{danie.Nazwa}    x{kvp.Value}    {(kvp.Value * danie.Cena):F2} zł",
                        FontSize = 18,
                        TextColor = Colors.Black,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };

                    var removeBtn = new Button
                    {
                        Text = "Usuń",
                        BackgroundColor = Colors.Transparent,
                        TextColor = Colors.Red,
                        FontAttributes = FontAttributes.Bold,
                        WidthRequest = 70,
                        HorizontalOptions = LayoutOptions.End
                    };

                    removeBtn.Clicked += (sender, e) =>
                    {
                        // Remove item from cart
                        _ilosciWybranych[kvp.Key] = 0;

                        // Update total price
                        _koszykCena -= kvp.Value * danie.Cena;

                        // Refresh UI
                        OnAppearing();
                    };

                    var horizontalLayout = new HorizontalStackLayout
                    {
                        Spacing = 15,
                        Children = { itemLabel, removeBtn }
                    };

                    var frame = new Frame
                    {
                        BackgroundColor = Colors.White,
                        CornerRadius = 20,
                        Padding = 15,
                        Margin = new Thickness(0, 6),
                        HasShadow = true,
                        Content = horizontalLayout
                    };

                    koszykLayout.Children.Add(frame);
                }
            }

            if (!koszykLayout.Children.Any())
            {
                koszykLayout.Children.Add(new Label
                {
                    Text = "Koszyk jest pusty.",
                    FontSize = 18,
                    TextColor = Colors.Gray,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 20)
                });
                razem.Text = "Razem: 0.00 zł";
            }
            else
            {
                razem.Text = $"Razem: {_koszykCena:F2} zł";
            }
        }

        private async void zamButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderConfirmationPage(_restauracja));
        }

        private async void BackToMenu_Clicked(object sender, EventArgs e)
        {
            // Go back in navigation stack to menu page
            await Navigation.PopAsync();
        }
    }
}
