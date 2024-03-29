﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using ThuyTienNguyen_CoursePlannerApp.Models;

namespace ThuyTienNguyen_CoursePlannerApp.Services
{
    public class SqliteDataService : ILocalDataService
    {
        private SQLiteConnection database;
        public bool Initialize()
        {
            bool dbTablesCreated = false;

            //create database
            if (database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CoursePlannerDB.db");
                database = new SQLiteConnection(dbPath);
            }

            var create = database.CreateTables<Term, Course, Assessment>();

            foreach (var result in create.Results)
            {
                dbTablesCreated = dbTablesCreated || result.Value == CreateTableResult.Created;
            }

            return dbTablesCreated;
        }

        //close database
        public void Close()
        {
            database.Close();
        }


        //DB Term Controls
        public List<Term> GetAllTerms()
        {
            return database.Table<Term>().ToList();
        }
        public void AddTerm(Term term)
        {
            database.Insert(term);
        }
        public int UpdateTerm(Term term)
        {
            return database.Update(term);
        }
        public int DeleteTerm(Term term)
        {
            return database.Delete(term);
        }

        //DB Course Controls
        public List<Course> GetAllCourses()
        {
            return database.Table<Course>().ToList();
        }
        public void AddCourse(Course course)
        {
            database.Insert(course);
        }
        public List<Course> GetCoursesByTermId(int termId)
        {
            string query = $"SELECT * FROM course WHERE course.TermId={termId}";
            return database.Query<Course>(query);
        }
        public int UpdateCourse(Course course)
        {
            return database.Update(course);
        }
        public int DeleteCourse(Course course)
        {
            return database.Delete(course);
        }

        //DB Assessment Controls
        public List<Assessment> GetAllAssessments()
        {
            return database.Table<Assessment>().ToList();
        }
        public void AddAssessment(Assessment assessment)
        {
            database.Insert(assessment);
        }
        public List<Assessment> GetAssessmentsByCourseId(int courseId)
        {
            string query = $"SELECT * FROM assessment WHERE assessment.CourseId={courseId}";
            return database.Query<Assessment>(query);
        }
        public int UpdateAssessment(Assessment assessment)
        {
            return database.Update(assessment);
        }
        public int DeleteAssessment(Assessment assessment)
        {
            return database.Delete(assessment);
        }

    }
}
