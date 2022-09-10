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
    public partial class EditPage : ContentPage
    {
        readonly SQLiteConnection db;
        readonly string _dbPath;
        readonly Food foodToEdit;
        public EditPage(string dbPath, Food food)
        {
            db = new SQLiteConnection(dbPath);
            _dbPath = dbPath;
            foodToEdit = food;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            nameEntry.Text = foodToEdit.Name;
            itemsEditor.Text = foodToEdit.Items;
            recipeEditor.Text = foodToEdit.Recipe;
        }

        protected async void okButton_Clicked(object sender, EventArgs e)
        {
            if (nameEntry.Text.Length == 0)
            {
                await DisplayAlert("Грешка", "Името не може да бъде празно.", "OK");
            }
            else
            {
                foodToEdit.Name = nameEntry.Text;
                foodToEdit.Items = itemsEditor.Text;
                foodToEdit.Recipe = recipeEditor.Text;
                db.Update(foodToEdit);
                await Navigation.PopAsync();
            }
        }

        protected async void deleteButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Сигурни ли сте?", "Сигурни ли сте, че искате да изтриете ястието?", "Да", "Не");
            if(answer)
            {
                db.Delete(foodToEdit);
                await Navigation.PopAsync();
            }
        }
    }
}