using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace mob1_project;

public partial class LocPage : ContentPage
{
    public string miasto = "";
    public LocPage()
    {
        InitializeComponent();
    }

    public async void LocMiasto_Clicked(object sender, EventArgs e)
    {
        string wybraneMiasto = null;

        if (sender is Frame frame && frame.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer recognizer)
        {
            wybraneMiasto = recognizer.CommandParameter as string;
        }

        if (!string.IsNullOrEmpty(wybraneMiasto))
        {
            Preferences.Set("Miasto", wybraneMiasto); 
            await Navigation.PushAsync(new RestMainPage(wybraneMiasto));
        }
    }
    // Metoda LocMiasto_Focused nie jest ju¿ wywo³ywana przez Frame z TapGestureRecognizer,
    // wiêc mo¿esz j¹ usun¹æ, chyba ¿e masz inne elementy, które jej u¿ywaj¹.
    // private void LocMiasto_Focused(object sender, EventArgs e)
    // {
    //     if (sender is Button focusedButton)
    //     {
    //         focusedButton.TextColor = Colors.Black;
    //         focusedButton.BackgroundColor = Colors.White;
    //     }
    // }
}