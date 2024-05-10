
namespace StudentGradeBook
{
    public class StudentInFile : StudentBase
    {
        public override event GradeAddedDelegate GradeAdded;

        private const string fileName = "grades.txt";

        public StudentInFile(string name, string surname, char gender, int age)
            : base(name, surname, gender, age)
        {
        }
        public StudentInFile(string fullName, char gender, int age)
            : base(fullName, gender, age)
        {
        }
        public StudentInFile(string fullName)
            : base(fullName)
        {
        }
        public override void AddGrade(float grade)
        {
            if (grade >= 1 && grade <= 6)
            {
                using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine(grade);
                }

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

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();

            statistics.ImportGrades(this.ReadGradesFromFile());

            return statistics;
        }

        public override List<string> GetGrades()
        {
            return ConvertToGrade(this.ReadGradesFromFile());
        }

        private List<float> ReadGradesFromFile()
        {
            List<float> result = new List<float>();

            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        if (float.TryParse(line, out float number))
                        {
                            result.Add(number);
                            line = reader.ReadLine();
                        }
                        else
                        {
                            throw new Exception($"Value \"{line}\" is not a float value!");
                        }
                    }
                }
            }
            else
            {
                throw new Exception($"File \"{fileName}\" does not exist!");
            }

            return result;
        }
    }
}
