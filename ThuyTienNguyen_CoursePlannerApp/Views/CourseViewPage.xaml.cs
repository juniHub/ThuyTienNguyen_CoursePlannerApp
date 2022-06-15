using System;
using System.Globalization;
using ThuyTienNguyen_CoursePlannerApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThuyTienNguyen_CoursePlannerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseViewPage : ContentPage
    {
        public Course SelectedCourse { get; set; }
        public CourseViewPage(Course course)
        {
            InitializeComponent();
            SetData(course);
        }

        public void SetData(Course course)
        {
            SelectedCourse = course;
            navTitle.Text = course.Title;
            CourseDateRange.Text = $"{course.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - {course.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
            statusSelection.Text = course.Status;
            instructorsName.Text = course.InstructorName;
            instructorsPhone.Text = course.InstructorPhone;
            instructorsEmail.Text = course.InstructorEmail;
            notes.Text = course.Notes;
            notificationsEnabled.Text = course.EnableNotifications ? "ON" : "OFF";
        }

        private async void ShareNotes_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync($"Notes from {navTitle.Text}:\n{notes.Text}");
        }

        private async void CourseAssessments_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentsListPage(SelectedCourse));
        }

        private async void EditCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CourseFormPage(this));
        }

        private async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            VMControls.deleteCourseFromCourseCollection(SelectedCourse);
            await Navigation.PopAsync();
        }
    }
}