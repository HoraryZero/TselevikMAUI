using TselevikMAUI.ViewModels;

namespace TselevikMAUI.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        private bool SlidingPanelIsShown = false;
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();


            Task.Run(AnimateBackground);
        }


        private async void AnimateBackground()
        {
            int animationDuration = 5000;
            var forwardAnim = new Animation(x => backGradient.AnchorY = x, 0, 1, Easing.Linear);
            var backwardAnim = new Animation(x => backGradient.AnchorY = x, 1, 0, Easing.Linear);

            while (true)
            {
                forwardAnim.Commit(backGradient, "forwaedAnim", 16U, (uint)animationDuration, Easing.Linear, null, () => false);
                await Task.Delay(animationDuration);
                backwardAnim.Commit(backGradient, "forwaedAnim", 16U, (uint)animationDuration, Easing.Linear, null, () => false);
                await Task.Delay(animationDuration);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();

            HideSlidingPanel();
        }
        private async void HideSlidingPanel()
        {
            while (this.Height == -1)
            {

                await Task.Delay(200);
                SlidingPanel.TranslationY = this.Height;
                SlidingPanelBackground.BackgroundColor = Colors.Transparent;
                SlidingPanelBackground.IsEnabled = false;
            }
            SlidingPanel.TranslationY = this.Height;
            SlidingPanelBackground.BackgroundColor = Colors.Transparent;
            SlidingPanelBackground.IsEnabled = false;

            SlidingPanelIsShown = false;
        }
        private async void FloatingButton_OnClicked(object sender, EventArgs e)
        {
            SwitchSlidingPanel();

            await AnimateFloatingButton();
        }

        private void SwitchSlidingPanel()
        {
            if (SlidingPanelIsShown)
            {
                SlidingPanel.TranslateTo(0, this.Height, 250, Easing.SinIn);
                SlidingPanelBackground.BackgroundColor = Colors.Transparent;
                SlidingPanelBackground.IsEnabled = false;
            }
            else
            {
                SlidingPanel.TranslateTo(0, this.Height - Quickmenu.Height - 30, 250, Easing.SpringOut);
                SlidingPanelBackground.BackgroundColor = Color.FromRgba(55, 55, 55, 99);
                SlidingPanelBackground.IsEnabled = true;
            }
            SlidingPanelIsShown = !SlidingPanelIsShown;
        }

        private async Task AnimateFloatingButton()
        {
            FloatingButton.ScaleTo(0.9, 125);
            await FloatingButton.TranslateTo(0, -5, 125);
            FloatingButton.ScaleTo(1, 125);
            await FloatingButton.TranslateTo(0, 5, 125);
        }

        private void SlidingPanelBackground_Tapped(object sender, TappedEventArgs e)
        {
            SwitchSlidingPanel();
        }

        private void QuickMenuButton_Clicked(object sender, EventArgs e)
        {
            SwitchSlidingPanel();
        }
        private void SlidingPanel_OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {

                case SwipeDirection.Up:
                    SlidingPanel.TranslateTo(0, this.Height - Quickmenu.Height - 350, 250, Easing.SinIn);
                    break;
                case SwipeDirection.Down:
                    SlidingPanel.TranslateTo(0, this.Height - Quickmenu.Height - 30, 250, Easing.SinIn);
                    break;
            }

        }
    }
}