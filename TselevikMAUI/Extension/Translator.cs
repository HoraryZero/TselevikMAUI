﻿using TselevikMAUI.Resources.Strings;
using System.ComponentModel;
using System.Globalization;

namespace TselevikMAUI.Extension
{
    public class Translator : INotifyPropertyChanged
    {
        public string this[string key]
        {
            get => AppResources.ResourceManager.GetString(key, CultureInfo);
        }
        public CultureInfo CultureInfo { get; set; }
        public static Translator Instance { get; set; } = new Translator();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
