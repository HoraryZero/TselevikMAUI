using TselevikMAUI.Services;
using TselevikMAUI.Themes;

namespace TselevikMAUI.Views;

public partial class ThemeSelectionPage : ContentPage, IModalPage
{
	public ThemeSelectionPage()
	{
		InitializeComponent();
	}

    void OnPickerSelectionChanged(object sender, EventArgs e)
    {
        Picker picker = sender as Picker;
        Theme theme = (Theme)picker.SelectedItem;

        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();

            switch (theme)
            {
                case Theme.Dark:
                    mergedDictionaries.Add(new DarkThemeApp());
                    break;
                case Theme.Light:
                default:
                    mergedDictionaries.Add(new LightThemeApp());
                    break;
            }
            statusLabel.Text = $"{theme.ToString()} theme loaded. Close this page.";
        }
    }

    public async Task Dismiss()
    {
        await Navigation.PopModalAsync();
    }
}