﻿using Plugin.LocalNotifications;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using ThuyTienNguyen_CoursePlannerApp.Services;
using ThuyTienNguyen_CoursePlannerApp.Models;

namespace ThuyTienNguyen_CoursePlannerApp
{
    public static class VMControls
    {
        public static ObservableCollection<Term> Terms = new ObservableCollection<Term>();
        public static ObservableCollection<Course> Courses = new ObservableCollection<Course>();
        public static ObservableCollection<Assessment> Assessments = new ObservableCollection<Assessment>();

        //Notification Controls
        public static void startupNotifications()
        {
            var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var database = new SqliteDataService();
            database.Initialize();
            var courses = database.GetAllCourses();
            var assessments = database.GetAllAssessments();
            courses.ForEach(course =>
            {
                if (course.EnableNotifications && new DateTime(course.StartDate.Year, course.StartDate.Month, course.StartDate.Day) == today)
                {
                    CrossLocalNotifications.Current.Show("Course Start", $"{course.Title} is starting today");
                }
                if (course.EnableNotifications && new DateTime(course.EndDate.Year, course.EndDate.Month, course.EndDate.Day) == today)
                {
                    CrossLocalNotifications.Current.Show("Course End", $"{course.Title} is ending today");
                }
            });
            assessments.ForEach(test =>
            {
                if (test.EnableNotifications && new DateTime(test.StartDate.Year, test.StartDate.Month, test.StartDate.Day) == today)
                {
                    CrossLocalNotifications.Current.Show("Assessment Start", $"{test.Title} due date start is today");
                }
                if (test.EnableNotifications && new DateTime(test.EndDate.Year, test.EndDate.Month, test.EndDate.Day) == today)
                {
                    CrossLocalNotifications.Current.Show("Assessment End", $"{test.Title} due date end is today");
                }
            });
            database.Close();
        }


        //VM Term Controls
        public static void initializeTermsCollection()
        {
            Terms.Clear();
            var database = new SqliteDataService();
            database.Initialize();
            var terms = database.GetAllTerms();
            terms.ForEach(term => Terms.Add(term));
            database.Close();
        }

        public static void addTermToTermCollection(Term term)
        {
            var database = new SqliteDataService();
            database.Initialize();
            database.AddTerm(term);
            Terms.Add(term);
            database.Close();
        }

        public static void updateTermInTermCollection(Term oldTerm, Term newTerm)
        {
            var termList = Terms.ToList();
            Terms.Clear();

            var database = new SqliteDataService();
            database.Initialize();
            database.UpdateTerm(newTerm);

            int indexFound = termList.IndexOf(oldTerm);
            termList.RemoveAt(indexFound);
            termList.Insert(indexFound, newTerm);
            termList.ForEach(term => Terms.Add(term));

            database.Close();
        }

        public static void deleteTermFromTermCollection(Term term)
        {
            var database = new SqliteDataService();
            database.Initialize();
            database.DeleteTerm(term);
            Terms.Remove(term);
            database.Close();
        }

        //VM Course Controls
        public static void initializeCoursesCollection(int termId)
        {
            Courses.Clear();
            var database = new SqliteDataService();
            database.Initialize();
            var courses = database.GetCoursesByTermId(termId);
            courses.ForEach(course => Courses.Add(course));
            database.Close();
        }

        public static void addCourseToCourseCollection(Course course)
        {
            var database = new SqliteDataService();
            database.Initialize();
            database.AddCourse(course);
            Courses.Add(course);
            database.Close();
        }

        public static void updateCourseInCourseCollection(Course oldCourse, Course newCourse)
        {
            var courseList = Courses.ToList();
            Courses.Clear();

            var database = new SqliteDataService();
            database.Initialize();
            database.UpdateCourse(newCourse);

            int indexFound = courseList.IndexOf(oldCourse);
            courseList.RemoveAt(indexFound);
            courseList.Insert(indexFound, newCourse);
            courseList.ForEach(course => Courses.Add(course));

            database.Close();
        }

        public static void deleteCourseFromCourseCollection(Course course)
        {
            var database = new SqliteDataService();
            database.Initialize();
            database.DeleteCourse(course);
            Courses.Remove(course);
            database.Close();
        }

        //VM Assessment Controls
        public static void initializeAssessmentCollection(int courseId)
        {
            Assessments.Clear();
            var database = new SqliteDataService();
            database.Initialize();
            var courses = database.GetAssessmentsByCourseId(courseId);
            courses.ForEach(assessment => Assessments.Add(assessment));
            database.Close();
        }

        public static void addAssessmentToAssessmentCollection(Assessment assessment)
        {
            var database = new SqliteDataService();
            database.Initialize();
            database.AddAssessment(assessment);
            Assessments.Add(assessment);
            database.Close();
        }

        public static void updateAssessmentInAssessmentCollection(Assessment oldAssessment, Assessment newAssessment)
        {
            var assessmentList = Assessments.ToList();
            Assessments.Clear();

            var database = new SqliteDataService();
            database.Initialize();
            database.UpdateAssessment(newAssessment);

            int indexFound = assessmentList.IndexOf(oldAssessment);
            assessmentList.RemoveAt(indexFound);
            assessmentList.Insert(indexFound, newAssessment);
            assessmentList.ForEach(assessment => Assessments.Add(assessment));

            database.Close();
        }

        public static void deleteAssessmentFromAssessmentCollection(Assessment assessment)
        {
            var database = new SqliteDataService();
            database.Initialize();
            database.DeleteAssessment(assessment);
            Assessments.Remove(assessment);
            database.Close();
        }
    }
}
