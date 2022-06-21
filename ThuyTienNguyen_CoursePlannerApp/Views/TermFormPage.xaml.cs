using System;
using ThuyTienNguyen_CoursePlannerApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThuyTienNguyen_CoursePlannerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermFormPage : ContentPage
    {
        private TermViewPage TermPage;
        public TermFormPage()
        {
            InitializeComponent();
            SaveEditButton.IsVisible = false;
        }

        public TermFormPage(TermViewPage termPage)
        {
            InitializeComponent();
            TermPage = termPage;
         
            termTitle.Text = termPage.SelectedTerm.Title;
            startDateSelected.Date = termPage.SelectedTerm.StartDate;
            endDateSelected.Date = termPage.SelectedTerm.EndDate;
            SaveButton.IsVisible = false;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (termTitle.Text == null || termTitle.Text == "")
                {
                    throw new Exception("You must have Term Title");
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception("The start date cannot be after the end date");
                }

                var newTerm = new Term { Title = termTitle.Text, StartDate = startDateSelected.Date, EndDate = endDateSelected.Date };
                VMControls.addTermToTermCollection(newTerm);
                await Navigation.PopModalAsync();
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void SaveEditButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (termTitle.Text == "")
                {
                    throw new Exception("You must have Term Title");
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception("The start date cannot be after the end date");
                }

                var termPage = TermPage;
                var newTerm = new Term { Id = termPage.SelectedTerm.Id, Title = termTitle.Text, StartDate = startDateSelected.Date, EndDate = endDateSelected.Date };
                VMControls.updateTermInTermCollection(termPage.SelectedTerm, newTerm);
                termPage.SetData(newTerm);
                await Navigation.PopModalAsync();
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}