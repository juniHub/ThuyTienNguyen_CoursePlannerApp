﻿using System;
using ThuyTienNguyen_CoursePlannerApp.Models;
using ThuyTienNguyen_CoursePlannerApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThuyTienNguyen_CoursePlannerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            InitializeData();
            VMControls.startupNotifications();
        }


        private async void Start_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new TermsListPage());
        }

        private void InitializeData()
        {
            var db = new SqliteDataService();
            bool addData = db.Initialize();

            if (addData)
            {
                var thisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                db.AddTerm(new Term { Title = "Term 1", StartDate = thisMonth, EndDate = thisMonth.AddMonths(4).AddDays(-1) });
                db.AddCourse(new Course
                {
                    TermId = 1,
                    Title = "Sample Course 1",
                    StartDate = thisMonth,
                    EndDate = thisMonth.AddMonths(1).AddDays(-1),
                    Status = "In Progress",
                    InstructorName = "Thuy Tien Nguyen",
                    InstructorPhone = "123-456-0789",
                    InstructorEmail = "ttnguyen@wgu.edu",
                    Notes = "This is the sample course",
                    EnableNotifications = true
                });
                db.AddAssessment(new Assessment
                {
                    CourseId = 1,
                    Title = "Sample Assessment 1",
                    StartDate = thisMonth.AddMonths(1).AddDays(-1),
                    EndDate = thisMonth.AddMonths(1),
                    Type = "Performance",
                    EnableNotifications = true
                });
                db.AddAssessment(new Assessment
                {
                    CourseId = 1,
                    Title = "Sample Assessment 2",
                    StartDate = thisMonth.AddMonths(2).AddDays(-1),
                    EndDate = thisMonth.AddMonths(2),
                    Type = "Objective",
                    EnableNotifications = true
                });
            }
        }
    }
}