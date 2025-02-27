﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TselevikMAUI.Models;
using TselevikMAUI.Views;

namespace TselevikMAUI.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command GoToUserSummaryPage { get; }
        public Command<Item> ItemTapped { get; }
        public Command MoveToUpCommand { protected set; get; }
        public Command MoveToBottomCommand { protected set; get; }
        public Command RemoveCommand { protected set; get; }
        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            GoToUserSummaryPage = new Command(OnUserSummaryPage);
            MoveToUpCommand = new Command(MoveToTop);
            MoveToBottomCommand = new Command(MoveToBottom);
            RemoveCommand = new Command(Remove);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStoreItems.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void OnUserSummaryPage(object obj)
        {
            await Shell.Current.GoToAsync(nameof(UserSummaryPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
        private void MoveToTop(object itemObj)
        {
            Item item = itemObj as Item;
            if (item == null) return;
            int oldIndex = Items.IndexOf(item);
            if (oldIndex > 0) Items.Move(oldIndex, oldIndex - 1);
        }
        private void MoveToBottom(object itemObj)
        {
            Item item = itemObj as Item;
            if (item == null) return;
            int oldIndex = Items.IndexOf(item);
            if (oldIndex < Items.Count - 1) Items.Move(oldIndex, oldIndex + 1);
        }

        private void Remove(object itemObj)
        {
            Item item = itemObj as Item;
            if (item == null) return;
            int oldIndex = Items.IndexOf(item);

            DataStoreItems.DeleteItemAsync(item.Id);
            Items.Remove(item);
        }
    }
}