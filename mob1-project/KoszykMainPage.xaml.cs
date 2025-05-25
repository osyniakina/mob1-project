using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mob1_project;
using mob1_project.Pages;
using Microsoft.Maui.Controls;

namespace mob1_project
{
    public partial class KoszykMainPage : ContentPage
    {
        private DBHelper _databaseHelper;
        private Dictionary<int, int> _ilosciWybranych;
        private float _koszykCena;
        private Restauracja _restauracja;   // dodajemy obiekt restauracji

        // Dodajemy parametr Restauracja do konstruktora
        public KoszykMainPage(Dictionary<int, int> ilosciWybranych, float koszykCena, Restauracja restauracja)
        {
            InitializeComponent();
            _databaseHelper = new DBHelper();
            _ilosciWybranych = ilosciWybranych;
            _koszykCena = koszykCena;
            _restauracja = restauracja;  // przypisujemy restaurację
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

        private async void zamButton_Clicked(object sender, EventArgs e)
        {
            // Teraz przekazujemy obiekt restauracji do strony podsumowania
            await Navigation.PushAsync(new OrderConfirmationPage(_restauracja));
        }
    }
}
