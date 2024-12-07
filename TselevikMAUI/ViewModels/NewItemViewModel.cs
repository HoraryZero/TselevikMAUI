using TselevikMAUI.Models;
using Plugin.AudioRecorder;

////PushNotification
//#if ANDROID
//using TselevikMAUI.Platforms.Android;
//#endif

namespace TselevikMAUI.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string id;
        private string text;
        private string description;
        private string date = DateTime.Now.ToShortDateString();
        private int importants = 50;
        private string category;
        private AudioRecorderService recorder;
        private AudioPlayer player;
        public bool IsPlaying;
        public bool IsRecording => recorder.IsRecording;
        private string filename;
        public EventHandler FinishPlaying;

        ////PushNotification
        //INotificationManagerService notificationManager;
        //int notificationNumber = 0;

        public NewItemViewModel()
        {
            id = Guid.NewGuid().ToString();
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            SwitchRecordingCommand = new Command(SwitchRecording);
            SwitchPlayingCommand = new Command(SwitchPlaying);
            AuduiInitialilise();
        }

        private async void AuduiInitialilise()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Microphone>();
            }


            recorder = new AudioRecorderService();
            player = new AudioPlayer();
            player.FinishedPlaying += StopPlaying;
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public int Importance
        {
            get => importants;
            set => SetProperty(ref importants, value);
        }
        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command SwitchRecordingCommand { get; }
        public Command SwitchPlayingCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        //Add Push Notification
        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = id,
                Text = Text,
                Description = Description,
                Date = Date,
                Importance = Importance,
                Category = Category
            };

            await DataStoreItems.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
            //Push
        }

        private void SwitchRecording()
        {
            if (!recorder.IsRecording)
            {
                StartRecordingAsync();
            }
            else
            {
                StopRecordingAsync();
            }
        }
        private async Task StartRecordingAsync()
        {

            filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "audio_" + id);
            recorder.FilePath = filename;
            await recorder.StartRecording();

        }

        private async Task StopRecordingAsync()
        {

            await recorder?.StopRecording();
        }



        private void SwitchPlaying()
        {
            if (!IsPlaying)
            {
                StartPlaying();
            }
            else
            {
                StopPlaying();
            }
        }

        private void StartPlaying()
        {
            IsPlaying = true;
            player.Play(filename);
        }
        void StopPlaying()
        {
            IsPlaying = false;
            player.Pause();

        }

        void StopPlaying(object sender, EventArgs e)
        {
            IsPlaying = false;
            FinishPlaying.Invoke(sender, e);
        }
    }
}
