namespace StudentGradeBook.Tests
{
    public class StudentTests
    {
        [Test]
        public void SettingNameAndSurname_ShouldReturnNameAndSurname()
        {
            // Arrange and Act
            var student = new StudentInMemory("Adam Kamizelich");

            // Assert
            Assert.That($"{student.Name} {student.Surname}", Is.EqualTo("Adam Kamizelich"));
        }

        [Test]
        public void GettingStatistics_ShouldReturnCorrectAverage()
        {
            // Arrange
            var student = new StudentInMemory("Adam Kamizelich");
            student.AddGrade(1);
            student.AddGrade(2);
            student.AddGrade(3);
            student.AddGrade(6);

            // Act
            var statistics = student.GetStatistics();

            // Assert
            Assert.That(statistics.Average, Is.EqualTo(3));
        }

        [Test]
        public void GettingStatistics_ShouldReturnCorrectMin()
        {
            // Arrange
            var student = new StudentInMemory("Adam Kamizelich");
            student.AddGrade(1);
            student.AddGrade(2);
            student.AddGrade(3);
            student.AddGrade(6);

            // Act
            var statistics = student.GetStatistics();

            // Assert
            Assert.That(statistics.Min, Is.EqualTo(1));
        }

        [Test]
        public void GettingStatistics_ShouldReturnCorrectMax()
        {
            // Arrange
            var student = new StudentInMemory("Adam Kamizelich");
            student.AddGrade(1);
            student.AddGrade(2);
            student.AddGrade(3);
            student.AddGrade(6);

            // Act
            var statistics = student.GetStatistics();

            // Assert
            Assert.That(statistics.Max, Is.EqualTo(6));
        }

        [Test]
        public void WhenLetterIsProvided_GradeShouldBeRepresentedAsNumericValue()
        {
            // Arrange
            var employee = new StudentInMemory("Adam Kamizelich");
            employee.AddGrade('a');

            // Act
            var statistics = employee.GetStatistics();

            // Assert
            Assert.That(statistics.Average, Is.EqualTo(6));
        }

        [Test]
        public void AddingPlusMinusGrade_ShouldReturnTheSameGrade()
        {
            // Arrange
            var student = new StudentInMemory("Adam Kamizelich");

            // Act
            student.AddGrade("3+");

            // Assert
            Assert.That(student.GetGrades().First(), Is.EqualTo("3+"));
        }

        [Test]
        public void AddingGradesToFile_ShouldReadTheSameGradesFromFile()
        {
            // Arrange
            var fileName = "grades.txt";
            if (File.Exists(fileName))
            {
                File.Delete("grades.txt");
            }
            var student = new StudentInFile("Adam Kamizelich");

            // Act
            student.AddGrade("3+");
            student.AddGrade("5");
            student.AddGrade("1+");
            student.AddGrade("5+");
            student.AddGrade("6-");

            // Assert
            Assert.That(string.Join(' ', student.GetGrades().ToArray()), Is.EqualTo("3+ 5 1+ 5+ 6-"));
        }
    }
}