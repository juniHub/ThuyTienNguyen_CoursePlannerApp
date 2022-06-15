using System.Collections.Generic;
using ThuyTienNguyen_CoursePlannerApp.Models;

namespace ThuyTienNguyen_CoursePlannerApp.Services
{
    public interface ILocalDataService
    {
        bool Initialize();
        void Close();

        List<Term> GetAllTerms();
        void AddTerm(Term term);
        int UpdateTerm(Term term);
        int DeleteTerm(Term term);

        List<Course> GetAllCourses();
        void AddCourse(Course course);
        List<Course> GetCoursesByTermId(int termId);
        int UpdateCourse(Course course);
        int DeleteCourse(Course course);

        List<Assessment> GetAllAssessments();
        void AddAssessment(Assessment assessment);
        List<Assessment> GetAssessmentsByCourseId(int courseId);
        int UpdateAssessment(Assessment assessment);
        int DeleteAssessment(Assessment assessment);
    }
}
