using TselevikMAUI.ViewModels;

namespace TselevikMAUI.Views;

public partial class CategoryPage : ContentPage
{
    private CategoryViewModel _categoryViewModel;
    public CategoryPage()
    {
        InitializeComponent();
        BindingContext = _categoryViewModel = new CategoryViewModel();
    }
}