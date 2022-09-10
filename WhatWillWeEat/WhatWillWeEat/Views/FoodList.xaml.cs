using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatWillWeEat.Models.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatWillWeEat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodList : ContentPage
    {
        readonly SQLiteConnection db;
        readonly string _dbPath;
        public FoodList(string dbPath)
        {
            db = new SQLiteConnection(dbPath);
            _dbPath = dbPath;
            InitializeComponent();
            foodListView.ItemTapped += foodItem_Tapped;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            foodListView.ItemsSource = db.Table<Food>();      
        }

        protected async void addButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPage(_dbPath));
        }
        protected async void foodItem_Tapped(object sender, EventArgs e)
        {
            Food selectedFood = foodListView.SelectedItem as Food;
            await Navigation.PushAsync(new EditPage(_dbPath, selectedFood));
        }
    }
}