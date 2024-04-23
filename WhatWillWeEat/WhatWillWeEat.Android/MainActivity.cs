using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using Microsoft.Maui;

namespace WhatWillWeEat.Droid
{
    [Activity(Label = "Какво ще ядем?", Icon = "@drawable/logo2", Theme = "@style/Theme.MaterialComponents", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Microsoft.Maui.MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
    }
}