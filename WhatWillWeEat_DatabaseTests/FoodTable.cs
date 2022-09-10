using NUnit.Framework;
using SQLite;
using System.Runtime.CompilerServices;
using WhatWillWeEat.Models.Entities;
using WhatWillWeEat.Services;

namespace WhatWillWeEat_DatabaseTests
{
    public class Tests
    {
        private SQLiteConnection db;
        private string _dbPath;
        [SetUp]
        public void Setup()
        {
            _dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal), "foodTestDB.db");
            db = new SQLiteConnection(_dbPath);
        }

        [Test]
        public void IsFoodAddedCorrectly()
        {
            try
            {
                db.DeleteAll<Food>();
            }
            catch (SQLiteException)
            {
                db.CreateTable<Food>();
            }
            Food food = CreateTestFood();
            db.Insert(food);

            Assert.AreEqual(food, db.Table<Food>().ElementAt(0));
        }

        [Test]
        public void IsFoodUpdatedCorrectly()
        {
            db.DeleteAll<Food>();
            Food oldfood = CreateTestFood();
            Food food = CreateTestFood();
            db.Insert(food);
            food.Recipe = "Намачкай и тогава го мий.";
            db.Update(food);

            Assert.AreNotEqual(oldfood, db.Table<Food>().ElementAt(0));
        }

        [Test]
        public void IsFoodRemovedCorrectly()
        {
            db.DeleteAll<Food>();
            Food food = CreateTestFood();
            Food food2 = CreateTestFood2();
            db.Insert(food);
            db.Insert(food2);
            db.Delete(food);
            Assert.AreNotEqual(food, db.Table<Food>().ElementAt(0));

        }



        private Food CreateTestFood()
        {
            Food food1 = new Food
            {
                Name = "Мусака",
                Recipe = "Бъркаш вътре и става.",
                Items = "яйца, гъз, бъз"
            };
            return food1;
        }
        private Food CreateTestFood2()
        {
            Food food2 = new Food
            {
                Name = "Мусака",
                Recipe = "Бъркаш вътре и не става.",
                Items = "да е"
            };
            return food2;
        }
    }
}