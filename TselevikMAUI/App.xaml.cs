using System.Globalization;
using TselevikMAUI.Services;
using TselevikMAUI.Views;

namespace TselevikMAUI {
	public partial class App : Application {
		public App() {
			InitializeComponent();

            //DependencyService.Register<MockDataStore>();

            //Язык
            CultureInfo.CurrentUICulture = new CultureInfo("en-EN");

            DependencyService.Register<DataStoreWebItems>();
            DependencyService.Register<DataStoreCategories>();

            if (Preferences.Get("IsFingerprintSet", false))
            {
                MainPage = new ConfirmPage();
            }
            else
            {
                MainPage = new MainPage();
            }
        }
	}
}
