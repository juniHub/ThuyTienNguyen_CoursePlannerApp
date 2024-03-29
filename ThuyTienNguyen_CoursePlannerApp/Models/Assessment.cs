﻿using SQLite;
using System;

namespace ThuyTienNguyen_CoursePlannerApp.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public bool EnableNotifications { get; set; }
    }
}
