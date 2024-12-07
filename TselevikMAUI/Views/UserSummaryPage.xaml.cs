using TselevikMAUI.Extension;

namespace TselevikMAUI.Views;

public partial class UserSummaryPage : ContentPage
{
    int count = 0;
    private bool isInitialized = false;

    public UserSummaryPage()
	{
		InitializeComponent();

        // ”становите RadioButton выбор после полной инициализации
        InitializeRadioButton();

        isInitialized = true;
    }

    private void InitializeRadioButton()
    {
        // ”становим английский €зык по умолчанию
        rbEnglish.IsChecked = true;
    }

    //private void OnCounterClicked(object sender, EventArgs e)
    //{
    //    count++;

    //    if (count == 1)
    //        CounterBtn.Text = $"Clicked {count} time";
    //    else
    //        CounterBtn.Text = $"Clicked {count} times";

    //    SemanticScreenReader.Announce(CounterBtn.Text);
    //}

    private void rbEnglish_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (isInitialized && e.Value) // ѕровер€ем, что страница загружена и RadioButton был действительно изменен
        {
            Translator.Instance.CultureInfo = new System.Globalization.CultureInfo("hi-IN");
            Translator.Instance.OnPropertyChanged();
        }
    }

    private void rbRussian_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (isInitialized && e.Value)
        {
            Translator.Instance.CultureInfo = new System.Globalization.CultureInfo("ru-RU");
            Translator.Instance.OnPropertyChanged();
        }
    }

    private void rbFrench_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (isInitialized && e.Value)
        {
            Translator.Instance.CultureInfo = new System.Globalization.CultureInfo("fr-FR");
            Translator.Instance.OnPropertyChanged();
        }
    }
    private void rbSpanish_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (isInitialized && e.Value)
        {
            Translator.Instance.CultureInfo = new System.Globalization.CultureInfo("sp-SP");
            Translator.Instance.OnPropertyChanged();
        }
    }
    private void rbGerman_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (isInitialized && e.Value)
        {
            Translator.Instance.CultureInfo = new System.Globalization.CultureInfo("de-DE");
            Translator.Instance.OnPropertyChanged();
        }
    }
}