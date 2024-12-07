namespace TselevikMAUI.ViewModels;

public class LanguageSettingViewModel : ContentPage
{
	public LanguageSettingViewModel()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}