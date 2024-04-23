using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatWillWeEat.Models.Entities;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace WhatWillWeEat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {

        readonly SQLiteConnection db;
        readonly Food _food;
        public DetailsPage(string dbPath, Food food)
        {
            db = new SQLiteConnection(dbPath);
            _food = food;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            nameEntry.Text = _food.Name;
            itemsEditor.Text = _food.Items;
            recipeEditor.Text = _food.Recipe;
        }
    }
}