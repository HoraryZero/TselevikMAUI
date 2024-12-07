using TselevikMAUI.ViewModels;

namespace TselevikMAUI.Views;

public partial class ConfirmPage : ContentPage
{
    private ConfirmViewModel ViewModel => BindingContext as ConfirmViewModel;

    public ConfirmPage()
	{
		InitializeComponent();
        BindingContext = new ConfirmViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing(); 

        if (ViewModel != null)
        {
            var isAuthenticated = await ViewModel.ConfirmFingerprint();
            if (isAuthenticated)
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Ошибка", "Не удалось подтвердить отпечаток", "OK");
            }
        }
    }
}