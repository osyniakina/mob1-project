using System;
using Microsoft.Maui.Controls;
using mob1_project;

namespace mob1_project.Pages
{
    public partial class OrderConfirmationPage : ContentPage
    {
        private Restauracja _restauracja;

        public OrderConfirmationPage(Restauracja restauracja)
        {
            InitializeComponent();
            _restauracja = restauracja;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            int czasDostawyMinuty = 30; // domyœlny czas

            if (int.TryParse(_restauracja.CzasDostawy, out int parsed))
            {
                czasDostawyMinuty = parsed;
            }

            var godzinaDostawy = DateTime.Now.AddMinutes(czasDostawyMinuty).ToString("HH:mm");

            czasLabel.Text = $"Przybli¿ony czas dostawy: {godzinaDostawy}";
        }
    }
}
