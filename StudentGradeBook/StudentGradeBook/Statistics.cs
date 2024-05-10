namespace StudentGradeBook
{
    public class Statistics
    {
        public float Min { get; private set; }
        public float Max { get; private set; }
        public float Sum { get; private set; }
        public float Count { get; private set; }
        public float Average
        {
            get
            {
                return (float)Math.Round(this.Sum / this.Count, 2);
            }
        }
        public string AverageGrade
        {
            get
            {
                switch (this.Average)
                {
                    case var average when average >= 5.90f:
                        return "6";
                    case var average when average >= 5.75f:
                        return "6-";
                    case var average when average >= 5.25f:
                        return "5+";
                    case var average when average >= 4.90f:
                        return "5";
                    case var average when average >= 4.75f:
                        return "5-";
                    case var average when average >= 4.25f:
                        return "4+";
                    case var average when average >= 3.90f:
                        return "4";
                    case var average when average >= 3.75f:
                        return "4-";
                    case var average when average >= 3.25f:
                        return "3+";
                    case var average when average >= 2.90f:
                        return "3";
                    case var average when average >= 2.75f:
                        return "3-";
                    case var average when average >= 2.25f:
                        return "2+";
                    case var average when average >= 1.90f:
                        return "2";
                    case var average when average >= 1.75:
                        return "2-";
                    case var average when average >= 1.25:
                        return "1+";
                    default:
                        return "1";
                }
            }
        }
        public Statistics()
        {
            this.Count = 0;
            this.Sum = 0;
            this.Max = float.MinValue;
            this.Min = float.MaxValue;
        }
        public void AddGrade(float grade)
        {
            this.Count++;
            this.Sum += grade;
            this.Min = Math.Min(this.Min, grade);
            this.Max = Math.Max(this.Max, grade);
        }
        public void ClearGrades()
        {
            this.Count = 0;
            this.Sum = 0;
            this.Max = float.MinValue;
            this.Min = float.MaxValue;
        }
        public void ImportGrades(in List<float> grades)
        {
            foreach (var grade in grades)
            {
                AddGrade(grade);
            }
        }
    }
}
