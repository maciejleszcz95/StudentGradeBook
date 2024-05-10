

namespace StudentGradeBook
{
    public class StudentInMemory : StudentBase
    {
        public override event GradeAddedDelegate GradeAdded;

        private List<float> grades = new List<float>();

        public StudentInMemory(string name, string surname, char gender, int age)
            : base(name, surname, gender, age)
        {
        }
        public StudentInMemory(string fullName, char gender, int age)
            : base(fullName, gender, age)
        {
        }
        public StudentInMemory(string fullName)
            : base(fullName)
        {
        }

        public override void AddGrade(float grade)
        {
            if (grade >= 1 && grade <= 6)
            {
                this.grades.Add(grade);

                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new Exception($"Grade value \"{grade}\" is out of range!");
            }
        }
        public override List<string> GetGrades()
        {
            return ConvertToGrade(this.grades);
        }
        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            statistics.ImportGrades(this.grades);
            return statistics;
        }
    }
}
