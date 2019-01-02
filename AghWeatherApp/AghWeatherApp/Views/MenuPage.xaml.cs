using AghWeatherApp.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AghWeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems, menuItemsAdmin, menuItemsUser;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Main, Title="Main" },
                new HomeMenuItem {Id = MenuItemType.Details, Title="Details" },
                new HomeMenuItem {Id = MenuItemType.Login, Title="Change User" },
                new HomeMenuItem {Id = MenuItemType.UserDetails, Title = "Manage Users"}
            };

            menuItemsAdmin = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Main, Title="Main" },
                new HomeMenuItem {Id = MenuItemType.Details, Title="Details" },
                new HomeMenuItem {Id = MenuItemType.UserDetails, Title = "Manage Users"},
                new HomeMenuItem {Id = MenuItemType.Chat, Title = "Chat"}
            };

            menuItemsUser = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Main, Title="Main" },
                new HomeMenuItem {Id = MenuItemType.Details, Title="Details" },
                new HomeMenuItem {Id = MenuItemType.Chat, Title = "Chat"}
            };

            UpdateMenuItems();

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                UpdateMenuItems();

                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);

            };
        }

        public void UpdateMenuItems()
        {
            switch (ProgramState.roleId)
            {
                case 1: //admin
                    ListViewMenu.ItemsSource = menuItemsAdmin;
                    break;
                default://user
                    ListViewMenu.ItemsSource = menuItemsUser;
                    break;
            }
        }
    }
}