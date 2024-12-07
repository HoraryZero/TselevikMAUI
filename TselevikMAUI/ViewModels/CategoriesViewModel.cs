﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using TselevikMAUI.Models;
using TselevikMAUI.Views;

namespace TselevikMAUI.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        private Category _selectedItem;

        public ObservableCollection<Category> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Category> ItemTapped { get; }

        public Command MoveToUpCommand { protected set; get; }
        public Command MoveToBottomCommand { protected set; get; }
        public Command RemoveCommand { protected set; get; }

        public CategoriesViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Category>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Category>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

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
                var items = await DataStoreCategories.GetItemsAsync(true);
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

        public Category SelectedItem
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
            await Shell.Current.GoToAsync(nameof(CategoryPage));
        }

        async void OnItemSelected(Category item)
        {
            if (item == null)
                return;

            // This will push the CatrgoryPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CategoryPage)}?{nameof(CategoryViewModel.ItemId)}={item.Id}");
        }
        private void MoveToTop(object categoryObj)
        {
            Category category = categoryObj as Category;
            if (category == null) return;
            int oldIndex = Items.IndexOf(category);
            if (oldIndex > 0) Items.Move(oldIndex, oldIndex - 1);
        }
        private void MoveToBottom(object categoryObj)
        {
            Category category = categoryObj as Category;
            if (category == null) return;
            int oldIndex = Items.IndexOf(category);
            if (oldIndex < Items.Count - 1) Items.Move(oldIndex, oldIndex + 1);
        }

        private void Remove(object categoryObj)
        {
            Category category = categoryObj as Category;
            if (category == null) return;
            int oldIndex = Items.IndexOf(category);

            DataStoreCategories.DeleteItemAsync(category.Id);
            Items.Remove(category);
        }

    }
}
