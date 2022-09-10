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
    public partial class AddPage : ContentPage
    {
        readonly SQLiteConnection db;
        readonly string _dbPath;

        public AddPage(string dbPath)
        {
            db = new SQLiteConnection(dbPath);
            _dbPath = dbPath;
            InitializeComponent();
        }

        protected async void addButton_Clicked(object sender, EventArgs e)
        {
            if(nameEntry.Text.Length == 0)
            {
                await DisplayAlert("Грешка", "Името не може да бъде празно.", "OK");
            }
            else
            {
                Food food = new Food
                {
                    Name = nameEntry.Text,
                    Items = itemsEditor.Text,
                    Recipe = recipeEditor.Text
                };
                db.Insert(food);
                await Navigation.PopAsync();
            }
        }
    }
}