using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatWillWeEat.Models.Entities;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace WhatWillWeEat.Views
{
    public class MainPage : ContentPage
    {
        readonly SQLiteConnection db;
        readonly string _dbPath;
        private StackLayout firstTimeLayout;
        private StackLayout mainPageLayout;
        private Button firstTimeButtonYes;
        private Button firstTimeButtonNo;
        private Label generatedFoodLabel;
        private Food _generatedFood;
        private Button generate;
        private Button details;
        private ToolbarItem list;
        private ToolbarItem add;
        private bool isCreated = true;
        public MainPage(string dbPath)
        {
            db = new SQLiteConnection(dbPath);
            _dbPath = dbPath;
        }

        protected override void OnAppearing()
        {
            try
            {
                int count = db.Table<Food>().ToList().Count;
            }
            catch (SQLiteException)
            {
                isCreated = false;
                FirstTime();
            }
            if(isCreated)
            {
                mainPageLayout = new StackLayout
                {
                    Spacing = 2
                };
                generatedFoodLabel = new Label
                {
                    FontSize = 40,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
                mainPageLayout.Children.Add(generatedFoodLabel);
                generate = new Button
                {
                    Text = "Предложи ми",
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                mainPageLayout.Children.Add(generate);
                details = new Button
                {
                    Text = "Детайли",
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                mainPageLayout.Children.Add(details);
                Content = mainPageLayout;
                this.Title = "Какво ще ядем?";
                if(_generatedFood != null)
                {
                    generatedFoodLabel.Text = _generatedFood.Name;
                }
                generate.Clicked += Generate;
                details.Clicked += GoToDetailsPage;
                if(this.ToolbarItems.Count == 0)
                {
                    add = new ToolbarItem { IconImageSource = "plus.png" };
                    this.ToolbarItems.Add(add);
                    add.Clicked += GoToAddPage;
                    list = new ToolbarItem { IconImageSource = "list.png" };
                    this.ToolbarItems.Add(list);
                    list.Clicked += GoToListPage;
                }
                if(!db.Table<Food>().Contains(_generatedFood) && _generatedFood != null)
                {
                    generatedFoodLabel.Text = "";
                }
                if(db.Table<Food>().ToList().Count.Equals(0))
                {
                    generatedFoodLabel.Text = "";
                }
            }         
        }

        private void FirstTime()
        {
            firstTimeLayout = new StackLayout
            {
                Spacing = 2
            };
            firstTimeLayout.Children.Add(new Label
            {
                FontSize = 24,
                Text = "Искате ли да добавите готов списък от ястия?"
            });
            firstTimeButtonYes = new Button
            {
                Text = "Да",
                HorizontalOptions = LayoutOptions.Center
            };
            firstTimeButtonNo = new Button
            {
                Text = "Не",
                HorizontalOptions = LayoutOptions.Center
            };
            firstTimeLayout.Children.Add(firstTimeButtonYes);
            firstTimeLayout.Children.Add(firstTimeButtonNo);
            Content = firstTimeLayout;
            this.Title = "Първи път?";
            firstTimeButtonYes.Clicked += AddPreset;
            firstTimeButtonNo.Clicked += NoPreset;

        }

        protected void AddPreset(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            db.CreateTable<Food>();
            sb.AppendLine("Картофите се обелват и нарязват на пръчици със сечение 1x1 см. Накисват се за малко в студена вода, за да се премахне част от скорбялата, след което се отцеждат и подсушават добре върху домакинска хартия.");
            sb.AppendLine("Олиото се загрява във фритюрник до 160°С, след което се слага шепа картофи да се пържат. Не се слага по-голямо количество, за да не се понижи прекалено температурата на олиото. Пържат се няколко минути (4-8 мин.), в зависимост от сорта картофи, докато станат бледо - златисти, като се разбъркват от време на време, за да не залепнат.");
            sb.AppendLine("Изваждат се и се оставят върху кухненска хартия за около 30 мин., за да се отцедят добре. Същото се повтаря и с останалите картофи. Преди сервиране фритюрникът се нагрява до 190°C и всички картофи се слагат да се пържат за 2 мин., докато станат хрупкави и златисто - кафяви.");
            sb.AppendLine("Пържените картофи се сервират веднага, поръсени със сол и се предлага майонеза. По желание се поръсват с настъргано сирене.");
            AddFood("Пържени картофи", "1 кг картофи, олио, сол",
                sb.ToString());
            sb = new StringBuilder();
            sb.Append("Нарежете месото на кубчета по 1.5 см.");
            sb.AppendLine("Сгорещете олиото в тиган и запържете свинското. Добавете нарязаните на дребно праз, чушки и моркови. Задушете за кратко и отстранете от огъня.");
            sb.AppendLine("Сипете ги в гювеч и добавете нарязаните на по-големи кубчета картофи. Подправете със сол, черен пипер, нарязания на ситно магданоз, чубрицата и добавете доматите.");
            sb.AppendLine("Объркайте всичко добре и налейте гореща вода до 1-3 на продуктите");
            sb.AppendLine("Сложете гювеча в студена фурна и гответе за около час и половина на 180 градуса.");
            AddFood("Гювеч със свинско и картофи","700 гр. свински бут, 2 кг. картофи, 2 стръка праз, червена и зелена чушка по 1 бр., 2 бр. моркови, 1 с.л. чубрица, пресен магданоз 1/2 вр., 200 гр. домати на кубчета от консерта, 4 с.л. олио, 2 с.л. червен пипер, черен пипер, сол",
                sb.ToString());
            sb = new StringBuilder();
            sb.AppendLine("Загрявате мазнината в дълбок тиган или тенджера. В загрятата мазнина задушете измитият, изчистен и нарязан на дребно лук, докато омекне. Добавяте нарязаните на дребно половината домати и каймата, черният пипер и чубрицата, както и една чаена лъжица сол. Може да ползвате кайма, каквато имате под ръка, но най-вкусната мусаката става с кайма смес – 50% свинско и 50 % телешко месо.");
            sb.AppendLine("След като водата от доматите изври добавяте червения пипер. Измивате, почиствате, обелвате и нарязвате на дребни кубчета картофите и ги прибавяте към месото. Намазнявате тавичка на дъното на която поставяте другата половина от доматите и върху тях изсипвате сместа за мусака. Добавяте една непълна чаена чаша гореща вода и поставяте в предварително загрята на 200 градуса фурна. Печете до докато водата изври, а картофите станат златисти.");
            sb.AppendLine("Идва моментът за приготвяне на заливката за мусаката. Тя става лесно и много бързо. Разбърквате киселото мляко и яйцата, след което при постоянно бъркане добавяте и брашното, до получаване на гладка смес. Накрая добавяте и настъргания на дребно кашкавал. Заливате мусаката и печете докато порозовее.");
            AddFood("Мусака", "500 гр. кайма, 1 кг картофи, 2 бр. лук, 2 бр. домати, 400 гр. кисело мляко, 2 бр. яйца, 100 гр. кашкавал, 5 с.л. брашно, 1 кафена чаша олио, 1 ч.л. червен пипер, 1 ч.л. чубрица"
                , sb.ToString());
            sb = new StringBuilder();
            sb.AppendLine("Първо попарете зелевите листа, за да омекнат.");
            sb.AppendLine("Може да приготвите сармите в 2 варианта - със запръжка или без. Ако не искате да пържите, смесете ситно нарязания лук, настърганите домати и моркови, ориза, мазнината, червения- и черния пипер, кимиона и сол на вкус с каймата. Със сместа напълнете сармите.");
            sb.AppendLine("Вторият вариант е да запържите продуктите в тази последователност в сгорещената мазнина. Завитите зелеви сарми с кайма се подреждат в тенджера, заливат се с вода или бульон от месо, притискат се с чиния и се варят, докато останат на мазнина.");
            sb.AppendLine("Забележка: Вместо кайма може да използвате накълцано ситно месо.");
            AddFood("Зелеви сарми с кайма", "зелеви листа, 500 г кайма смес, 1/2 ч.ч. ориз, 100 мл олио, 2 глви кромид лук, 250 г домати, 1 бр. моркови, 1 ч.л. червен пипер, черен пипер, кимион, сол",
                sb.ToString());
            sb = new StringBuilder();
            sb.AppendLine("Яйцата, сиренето, киселото мляко, брашното и содата се объркват в купа до получаване на сместа за плънката.");
            sb.AppendLine("Корите за баница се вадят две по две. Първата се помазва с олио, а втората - със сместа. Навиват се и последователно се поставят в предварително помазана с олио тава до оформянето на баницата");
            sb.AppendLine("В случай че остане от сместа, се изсипва отгоре и се помазва с него, а в случай че не остане - помазва се с олио.");
            sb.AppendLine("Печете баницата до зачервяване в предваритело загрята фурна на 200 градуса.");
            AddFood("Баница с готови кори и сирене", "1 пакет кори за баница, 3 бр. яйца, 300 - 400 г сирене, 1 кофичка кисело мляко, 2 с.л. брашно, 1 к.л. сода бикарбонат, олио",
                sb.ToString());
            isCreated = true;
            this.OnAppearing();
        }
        protected void NoPreset(object sender, EventArgs e)
        {
            db.CreateTable<Food>();
            isCreated = true;
            this.OnAppearing();
        }

        protected async void Generate(object sender, EventArgs e)
        {
            Food generatedFood = null;
            int generatedFoodID = 0;
            Random rnd = new Random();
            if(db.Table<Food>().ToList().Count == 0)
            {
                await DisplayAlert("Грешка", "Нямате ястия.", "ОК");
            }
            else if(db.Table<Food>().ToList().Count == 1)
            {
                await DisplayAlert("Грешка", "Трябва да имате поне 2 ястия.", "ОК");
            }
            else
            {
                generatedFoodID = rnd.Next(1, db.Table<Food>().ToList().Count + 1);
                if (generatedFoodID == 1)
                {
                    generatedFood = db.Table<Food>().ElementAt(0);
                }
                else
                {
                    generatedFood = db.Table<Food>().ToList().ElementAt(generatedFoodID - 1);
                }
                _generatedFood = generatedFood;
                generatedFoodLabel.Text = $"{_generatedFood.Name}";
            }
        }
        protected async void GoToDetailsPage (object sender, EventArgs e)
        {
           if(String.IsNullOrEmpty(generatedFoodLabel.Text) || 
                String.IsNullOrWhiteSpace(generatedFoodLabel.Text))
            {
                await DisplayAlert("Грешка", "Не сте избрали ястие.", "OK");
            }
            else
            {
                await Navigation.PushAsync(new DetailsPage(_dbPath, _generatedFood));
            }
        }

        protected async void GoToAddPage (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPage(_dbPath));
        }

        protected async void GoToListPage (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FoodList(_dbPath));
        }

        private void AddFood(string name, string items, string recipe)
        {
            db.Insert(new Food
            {
                Name = name,
                Items = items,
                Recipe = recipe
            });
        }
    }
}