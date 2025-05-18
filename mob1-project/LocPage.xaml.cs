using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace mob1_project;

public partial class LocPage : ContentPage
{
    public string miasto = "";
    public int miastoid = 0;
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
            miasto = wybraneMiasto; // Ustawiamy w�a�ciwo�� miasto

            if (miasto == "Warszawa")
            {
                miastoid = 1;
                await Navigation.PushAsync(new RestMainPage(miasto));
            }
            else if (miasto == "Bialystok")
            {
                miastoid = 2;
                await Navigation.PushAsync(new RestMainPage(miasto));
            }
            else if (miasto == "Lublin")
            {
                miastoid = 3;
                await Navigation.PushAsync(new RestMainPage(miasto));
            }
            else
            {
                miastoid = 0;
            }
        }
    }

    // Metoda LocMiasto_Focused nie jest ju� wywo�ywana przez Frame z TapGestureRecognizer,
    // wi�c mo�esz j� usun��, chyba �e masz inne elementy, kt�re jej u�ywaj�.
    // private void LocMiasto_Focused(object sender, EventArgs e)
    // {
    //     if (sender is Button focusedButton)
    //     {
    //         focusedButton.TextColor = Colors.Black;
    //         focusedButton.BackgroundColor = Colors.White;
    //     }
    // }
}