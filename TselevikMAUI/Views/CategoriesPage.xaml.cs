using TselevikMAUI.Models;
using TselevikMAUI.ViewModels;

namespace TselevikMAUI.Views
{
    public partial class CategoriesPage : ContentPage
    {
        CategoriesViewModel _viewModel;
        public CategoriesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CategoriesViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
        async void OnItemSelected(Category category)
        {
            if (category == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CategoryPage)}?{nameof(CategoryViewModel.ItemId)}={category.Id}");
        }
    }
}

