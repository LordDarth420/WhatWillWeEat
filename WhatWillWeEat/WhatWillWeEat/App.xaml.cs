using System;
using WhatWillWeEat.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatWillWeEat
{
    public partial class App : Application
    {
        public App(string dbPath)
        {
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
