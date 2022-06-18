using System;
using System.Globalization;
using ThuyTienNguyen_CoursePlannerApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThuyTienNguyen_CoursePlannerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermViewPage : ContentPage
    {
        public Term SelectedTerm { get; set; }
        public TermViewPage(Term term)
        {
            VMControls.initializeCoursesCollection(term.Id);
            InitializeComponent();
            SelectedTerm = term;
            SetData(term);
        }

        public void SetData(Term term)
        {
            navTitle.Text = term.Title;
            TermDateRange.Text = $"Start date: {term.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - End date: {term.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
        }

        private async void AddCourse_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (VMControls.Courses.Count >= 6)
                {
                    throw new Exception("You cannot add more than 6 courses to a term");
                }

                await Navigation.PushModalAsync(new CourseFormPage(SelectedTerm.Id));
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void EditTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermFormPage(this));
        }

        private async void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirm Deletion", $"Are you sure you want to delete {SelectedTerm.Title}?", "Delete", "Cancel"))
            {
                VMControls.deleteTermFromTermCollection(SelectedTerm);
                await Navigation.PopAsync();
            }
        }

        private async void Course_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var course = (Course)layout.BindingContext;
            await Navigation.PushAsync(new CourseViewPage(course));
        }
    }
}