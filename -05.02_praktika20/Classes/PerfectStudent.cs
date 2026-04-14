namespace praktika20.Classes
{
    public class PerfectStudent
    {
        public StudentContext Student {  get; set; }
        public int Row { get; set; }
        public int PracticeCount { get; set; }
        public int TheoryCount { get; set; }
        public int AbsenteeismCount { get; set; }
        public int LateCount { get; set; }
        public int TotalIssues {  get; set; }

        public PerfectStudent(StudentContext student, int row, int practiceCount, int theoryCount, int absenteeismCount, int lateCount)
        {
            Student = student;
            Row = row;
            PracticeCount = practiceCount;
            TheoryCount = theoryCount;
            AbsenteeismCount = absenteeismCount;
            LateCount = lateCount;
            TotalIssues = practiceCount + theoryCount + absenteeismCount + lateCount;
        }
    }
}
