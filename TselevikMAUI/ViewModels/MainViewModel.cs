using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using TselevikMAUI.Views;

namespace TselevikMAUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateFingerprintCommand { get; }

        private bool _isFingerprintSet;
        public bool IsFingerprintSet
        {
            get => Preferences.Get("IsFingerprintSet", false);
            set
            {
                _isFingerprintSet = value;
                Preferences.Set("IsFingerprintSet", value);
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            CreateFingerprintCommand = new Command(async () => await CreateFingerprint());
        }

        private async Task CreateFingerprint()
        {
            var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Создание отпечатка", "Подтвердите ваш отпечаток пальца"));
            if (result.Authenticated)
            {
                IsFingerprintSet = true;
                Application.Current.MainPage = new ConfirmPage();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не удалось создать отпечаток", "OK");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ConfirmViewModel
    {
        public async Task<bool> ConfirmFingerprint()
        {
            var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Подтверждение отпечатка", "Подтвердите ваш отпечаток пальца"));
            return result.Authenticated;
        }
    }
}
