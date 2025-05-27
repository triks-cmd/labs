namespace PeopleApp
{
    /// <summary>
    /// Application-wide constants to avoid magic literals.
    /// </summary>
    public  class Constants
    {
        /// <summary>Threshold age for highlighting schoolchildren.</summary>
        public const int SchoolchildAgeThreshold = 12;

        /// <summary>Grade value considered failing.</summary>
        public const int FailingGrade = 2;

        /// <summary>Minimum tokens required to parse a learner record.</summary>
        public const int MinTokensForLearner = 6;

        public const int LastNameIndex = 0;
        public const int BirthYearIndex = 1;
        public const int StatusIndex = 2;
        public const int InstitutionIndex = 3;
        public const int GroupOrClassIndex = 4;
        public const int GradesStartIndex = 5;

        public const int WorkplaceIndex = 3;
        public const int PositionIndex = 4;
        public const int SalariesStartIndex = 5;
    }
}