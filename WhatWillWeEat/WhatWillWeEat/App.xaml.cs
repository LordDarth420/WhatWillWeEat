using System;
using WhatWillWeEat.Views;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using SQLite;

namespace WhatWillWeEat
{
    
    public partial class App : Application
    {
        private string applicationFolderPath = FileSystem.Current.AppDataDirectory;
        readonly SQLiteConnection db;

        public App()
        {
            Directory.CreateDirectory(applicationFolderPath);

            string dbPath = System.IO.Path.Combine(applicationFolderPath, "foodDB.db");
            var db = new SQLiteConnection(dbPath);
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage(dbPath));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
