using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SQLite;

namespace WhatWillWeEat.Models.Entities
{
    [Table("Foods")]
    public class Food
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Items { get; set; }
        public string Recipe { get; set; }

        public Food()
        {

        }
        public override string ToString()
        {
            return $"{Name}";
        }

        public override bool Equals(object obj)
        {
            var that = obj as Food;
            return this.ID == that.ID && this.Name == that.Name && this.Items == that.Items && this.Recipe == that.Recipe;
        }
    }
}
