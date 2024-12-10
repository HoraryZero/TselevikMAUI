using TselevikMAUI.Views;

namespace TselevikMAUI {
	public partial class AppShell : Shell {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(UserSummaryPage), typeof(UserSummaryPage));
            Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
            //My way
            Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
