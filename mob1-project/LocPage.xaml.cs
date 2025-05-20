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
}