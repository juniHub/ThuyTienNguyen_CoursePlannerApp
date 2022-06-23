using System;
using ThuyTienNguyen_CoursePlannerApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;

namespace ThuyTienNguyen_CoursePlannerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseFormPage : ContentPage
    {
        private int TermID;
        private CourseViewPage CoursePage;

        Regex PhoneRegex = new Regex(@"\d{3}-\d{3}-\d{4}");
        Regex EmailRegex = new Regex(@"\w+@wgu\.edu");

        public CourseFormPage(int termId)
        {
            InitializeComponent();
            TermID = termId;
            SaveEditButton.IsVisible = false;
        }

        public CourseFormPage(CourseViewPage coursePage)
        {
            InitializeComponent();
            CoursePage = coursePage;
            Course course = coursePage.SelectedCourse;
            TermID = course.TermId;
            courseTitle.Text = course.Title;
            startDateSelected.Date = course.StartDate;
            endDateSelected.Date = course.EndDate;
            statusPicker.SelectedItem = course.Status;
            instructorName.Text = course.InstructorName;
            instructorPhone.Text = course.InstructorPhone;
            instructorEmail.Text = course.InstructorEmail;
            courseNotes.Text = course.Notes;
            notificationSwitch.IsToggled = course.EnableNotifications;
            SaveButton.IsVisible = false;
        }

      

    private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
   
                if ( courseTitle.Text == null || courseTitle.Text == "")
                {
                    throw new Exception("You must have Course Title");
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception("The start date cannot be after the end date");
                }

                if (statusPicker.SelectedItem == null)
                {
                    throw new Exception("You must pick a course status");
                }

                if (
                        instructorName.Text == null || instructorName.Text == "" ||
                        instructorPhone.Text == null || instructorPhone.Text == "" ||
                        instructorEmail.Text == null || instructorEmail.Text == ""
                    )
                {
                    throw new Exception("You must provide all of the course instructor's info (Name, Phone, Email)");
                }



                if (!PhoneRegex.IsMatch(instructorPhone.Text))
                {
                    throw new Exception(
                        "Instructor's phone is invalid.\n" +
                        "Phone must match this pattern:\n" +
                        "123-456-7890");

                }


                if (!EmailRegex.IsMatch(instructorEmail.Text))
                {
                    throw new Exception(
                        "Instructor's email is invalid.\n" +
                        "Email must match this pattern:\n" +
                        "name@wgu.edu");


                }




                if (courseNotes.Text == null)
                {
                    courseNotes.Text = "";
                }

                var newCourse = new Course
                {
                    TermId = TermID,
                    Title = courseTitle.Text,
                    StartDate = startDateSelected.Date,
                    EndDate = endDateSelected.Date,
                    Status = statusPicker.SelectedItem.ToString(),
                    InstructorName = instructorName.Text,
                    InstructorPhone = instructorPhone.Text,
                    InstructorEmail = instructorEmail.Text,
                    Notes = courseNotes.Text,
                    EnableNotifications = notificationSwitch.IsToggled
                };
                VMControls.addCourseToCourseCollection(newCourse);
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
               

                if (!PhoneRegex.IsMatch(instructorPhone.Text))
                {
                    throw new Exception(
                        "Instructor's phone is invalid.\n" +
                        "Phone must match this pattern:\n" +
                        "123-456-7890");

                }


                if (!EmailRegex.IsMatch(instructorEmail.Text))
                {
                    throw new Exception(
                        "Instructor's email is invalid.\n" +
                        "Email must match this pattern:\n" +
                        "name@wgu.edu");


                }


                if (courseTitle.Text == "")
                {
                    throw new Exception("You must have Course Title");
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception("The start date cannot be after the end date");
                }

                if (instructorName.Text == "" || instructorPhone.Text == "" || instructorEmail.Text == "")
                {
                    throw new Exception("You must provide all of the course instructor's info (Name, Phone, Email)");
                }

              

                var coursePage = CoursePage;
                var newCourse = new Course
                {
                    Id = coursePage.SelectedCourse.Id,
                    TermId = TermID,
                    Title = courseTitle.Text,
                    StartDate = startDateSelected.Date,
                    EndDate = endDateSelected.Date,
                    Status = statusPicker.SelectedItem.ToString(),
                    InstructorName = instructorName.Text,
                    InstructorPhone = instructorPhone.Text,
                    InstructorEmail = instructorEmail.Text,
                    Notes = courseNotes.Text,
                    EnableNotifications = notificationSwitch.IsToggled
                };
                VMControls.updateCourseInCourseCollection(coursePage.SelectedCourse, newCourse);
                coursePage.SetData(newCourse);
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