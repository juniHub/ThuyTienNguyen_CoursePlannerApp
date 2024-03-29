﻿using System;
using ThuyTienNguyen_CoursePlannerApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThuyTienNguyen_CoursePlannerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentsListPage : ContentPage
    {
        public Course SelectedCourse { get; set; }
        public AssessmentsListPage(Course course)
        {
            VMControls.initializeAssessmentCollection(course.Id);
            InitializeComponent();
            SelectedCourse = course;
            navTitle.Text = $"{course.Title} Assessments";
        }

        private async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (VMControls.Assessments.Count >= 2)
                {
                    throw new Exception("You cannot add more than 2 assessments to a course");
                }
                await Navigation.PushModalAsync(new AssessmentFormPage(SelectedCourse.Id));
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void Assessment_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var assessment = (Assessment)layout.BindingContext;
            await Navigation.PushAsync(new AssessmentViewPage(assessment));
        }
    }
}