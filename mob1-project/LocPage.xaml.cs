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
        if (sender is Button clickedButton)
        {
            miasto = clickedButton.StyleId;

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

    private void LocMiasto_Focused(object sender, EventArgs e)
    {
        if (sender is Button focusedButton)
        {
            focusedButton.TextColor = Colors.Black;
            focusedButton.BackgroundColor = Colors.White;
        }
    }
}