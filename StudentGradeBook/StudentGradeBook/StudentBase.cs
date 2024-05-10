
namespace StudentGradeBook
{
    public abstract class StudentBase : IStudent
    {
        public delegate void GradeAddedDelegate(object sender, EventArgs args);

        public abstract event GradeAddedDelegate GradeAdded;

        public StudentBase(string name, string surname, char gender, int age)
        {
            this.Name = name;
            this.Surname = surname;
            this.Gender = GenderValidation(gender);
            this.Age = AgeValidation(age);
        }
        public StudentBase(string fullName, char gender, int age)
            : this(GetFirstname(fullName), GetLastname(fullName), gender, age)
        {
        }
        public StudentBase(string fullName)
            : this(fullName, 'N', 0)
        {
        }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public char Gender { get; private set; }
        public int Age { get; private set; }
        public string GenderInString
        {
            get
            {
                switch (this.Gender)
                {
                    case 'M':
                        return "Mezczyzna";
                    case 'F':
                        return "Kobieta";
                    default:
                        return "Nie okreslono";
                }
            }
        }

        private static char GenderValidation(char gender)
        {
            switch (gender)
            {
                case 'M': // Male
                case 'm':
                    return 'M';
                case 'F': // Female
                case 'f':
                    return 'F';
                case 'N': // Non-specified
                case 'n':
                    return 'N';
                default:
                    throw new Exception($"\"{gender}\" is wrong gender value!");
            }
        }
        private static int AgeValidation(int age)
        {
            if (age < 0)
            {
                throw new Exception($"Age value \"{age}\" cannot be negative!");
            }

            return age;
        }
        private static bool IsPlusMinusGrade(string grade)
        {
            grade = grade.Trim();
            return (grade.Length == 2
               && short.TryParse(grade[0].ToString(), out short value)
               && value >= 1
               && value <= 6
               && (grade[1] == '-' || grade[1] == '+'));
        }
        private static float PlusMinusGradeConversion(string grade)
        {
            grade = grade.Trim();
            float value = float.Parse(grade[0].ToString());
            switch (grade[1])
            {
                case '+':
                    return value + 0.25f;
                case '-':
                    return value - 0.25f;
                default:
                    throw new Exception("Bad grade value!");
            }
        }
        private static string GetFirstname(string fullName)
        {
            var names = fullName.Trim().Split(' ');
            var firstname = names.First();

            return firstname;
        }
        private static string GetLastname(string fullName)
        {
            var names = fullName.Trim().Split(' ');
            var lastname = names.Last();

            if (names.First() == lastname) //no lastname case
            {
                lastname = "";
            }

            return lastname;
        }

        protected static List<string> ConvertToGrade(List<float> grades)
        {
            List<string> ret = new List<string>();

            foreach (var grade in grades)
            {
                ret.Add(ConvertToGrade(grade));
            }
            return ret;
        }
        protected static string ConvertToGrade(float grade)
        {
            switch (grade)
            {
                case var value when value >= 5.90f:
                    return "6";
                case var value when value >= 5.75f:
                    return "6-";
                case var value when value >= 5.25f:
                    return "5+";
                case var value when value >= 4.90f:
                    return "5";
                case var value when value >= 4.75f:
                    return "5-";
                case var value when value >= 4.25f:
                    return "4+";
                case var value when value >= 3.90f:
                    return "4";
                case var value when value >= 3.75f:
                    return "4-";
                case var value when value >= 3.25f:
                    return "3+";
                case var value when value >= 2.90f:
                    return "3";
                case var value when value >= 2.75f:
                    return "3-";
                case var value when value >= 2.25f:
                    return "2+";
                case var value when value >= 1.90f:
                    return "2";
                case var value when value >= 1.75:
                    return "2-";
                case var value when value >= 1.25:
                    return "1+";
                default:
                    return "1";
            }
        }

        public abstract void AddGrade(float grade);

        public void AddGrade(int grade)
        {
            float value = (float)grade;
            this.AddGrade(value);
        }
        public void AddGrade(string grade)
        {
            if (float.TryParse(grade, out float resultFloat))
            {
                this.AddGrade(resultFloat);
            }
            else if (char.TryParse(grade, out char resultChar))
            {
                this.AddGrade(resultChar);
            }
            else if (IsPlusMinusGrade(grade))
            {
                this.AddGrade(PlusMinusGradeConversion(grade));
            }
            else
            {
                throw new Exception($"Value \"{grade}\" is not a school grade or float value!");
            }
        }
        public void AddGrade(char grade)
        {
            var buffer = grade.ToString();
            buffer = buffer.ToUpper();
            grade = char.Parse(buffer);

            switch (grade)
            {
                case 'A':
                    this.AddGrade(6.00f);
                    break;
                case 'B':
                    this.AddGrade(5.00f);
                    break;
                case 'C':
                    this.AddGrade(4.00f);
                    break;
                case 'D':
                    this.AddGrade(3.00f);
                    break;
                case 'E':
                    this.AddGrade(2.00f);
                    break;
                case 'F':
                    this.AddGrade(1.00f);
                    break;
                default:
                    throw new Exception($"\"{grade}\" is wrong grade letter!");
            }
        }

        public abstract List<string> GetGrades();
        public abstract Statistics GetStatistics();
    }
}