using TselevikMAUI.Models;
using TselevikMAUI.ViewModels;
using System.Diagnostics;

namespace TselevikMAUI.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        private NewItemViewModel _viewModel;
        private IEnumerable<Category> categories;
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewItemViewModel();
            LoadCategories();

            buttonPlay.IsEnabled = false;
            buttonPlay.BackgroundColor = Colors.Silver;

            _viewModel.FinishPlaying += StopPlaying;

        }
        async Task LoadCategories()
        {
            try
            {
                categories = await _viewModel.DataStoreCategories.GetItemsAsync(true);
                List<string> categoriesTitles = new List<string>();
                foreach (var category in categories)
                {
                    categoriesTitles.Add(category.Title);
                }

                PickerCategory.ItemsSource = categoriesTitles;
                if (categoriesTitles.Count > 0)
                {
                    PickerCategory.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DatePickerDate_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            _viewModel.Date = e.NewDate.ToShortDateString();
        }

        private void SwitchRecording(object sender, EventArgs e)
        {
            if (_viewModel.IsRecording)
            {
                StartRecording(sender, e);
            }
            else
            {
                StopRecording(sender, e);
            }
        }
        private void StartRecording(object sender, EventArgs e)
        {
            buttonRecord.Text = "Stop Recording";
            buttonRecord.IsEnabled = true;
            buttonRecord.BackgroundColor = Color.FromHex("#7cbb45");
            buttonPlay.IsEnabled = false;
            buttonPlay.BackgroundColor = Colors.Silver;
        }

        private void StopRecording(object sender, EventArgs e)
        {
            buttonRecord.Text = "Record";
            buttonRecord.IsEnabled = true;
            buttonRecord.BackgroundColor = Color.FromHex("#7cbb45");
            buttonPlay.IsEnabled = true;
            buttonPlay.BackgroundColor = Color.FromHex("#7cbb45");
        }

        private void SwitchPlaying(object sender, EventArgs e)
        {
            if (_viewModel.IsPlaying)
            {
                StartPlaying(sender, e);
            }
            else
            {
                StopPlaying(sender, e);
            }
        }

        private void StartPlaying(object sender, EventArgs e)
        {
            buttonPlay.Text = "Stop Playing";
            buttonRecord.IsEnabled = false;
            buttonRecord.BackgroundColor = Colors.Silver;
            buttonPlay.IsEnabled = true;
            buttonPlay.BackgroundColor = Color.FromHex("#7cbb45");
        }

        void StopPlaying(object sender, EventArgs e)
        {
            buttonPlay.Text = "Play";
            buttonRecord.IsEnabled = true;
            buttonRecord.BackgroundColor = Color.FromHex("#7cbb45");
            buttonPlay.IsEnabled = true;
            buttonPlay.BackgroundColor = Color.FromHex("#7cbb45");

        }
    }
}