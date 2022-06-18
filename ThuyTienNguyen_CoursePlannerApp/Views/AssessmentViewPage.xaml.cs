using System;
using System.Globalization;
using ThuyTienNguyen_CoursePlannerApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThuyTienNguyen_CoursePlannerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentViewPage : ContentPage
    {
        private Assessment SelectedAssessment;
        public AssessmentViewPage(Assessment assessment)
        {
            InitializeComponent();
            SelectedAssessment = assessment;
            SetData(assessment);
        }

        public void SetData(Assessment assessment)
        {
            navTitle.Text = assessment.Title;
            AssessmentDateRange.Text = $"Start date: {assessment.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - End date: {assessment.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
            TypeSelection.Text = assessment.Type;
            notificationsEnabled.Text = assessment.EnableNotifications ? "ON" : "OFF";
        }

        private async void EditAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AssessmentFormPage(this, SelectedAssessment));
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirm Deletion", $"Are you sure you want to delete {SelectedAssessment.Title}?", "Delete", "Cancel"))
            {

                VMControls.deleteAssessmentFromAssessmentCollection(SelectedAssessment);
                await Navigation.PopAsync();
            }

          
        }
    }
}