using static StudentGradeBook.StudentBase;

namespace StudentGradeBook
{
    public interface IStudent
    {
        string Name { get; }
        string Surname { get; }
        char Gender { get; }
        int Age { get; }
        string GenderInString { get; }

        void AddGrade(float grade);
        void AddGrade(int grade);
        void AddGrade(string grade);
        void AddGrade(char grade);
        
        event GradeAddedDelegate GradeAdded;
        
        List<string> GetGrades();
        Statistics GetStatistics();
    }
}
