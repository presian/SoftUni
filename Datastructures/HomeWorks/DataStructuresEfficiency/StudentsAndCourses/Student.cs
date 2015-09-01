namespace StudentsAndCourses
{
    using System;

    class Student : IComparable<Student>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CompareTo(Student other)
        {
            var lastNamesComare = string.CompareOrdinal(this.LastName, other.LastName);
            if (lastNamesComare == 0)
            {
                return string.CompareOrdinal(this.FirstName, other.FirstName);
            }
            else
            {
                return lastNamesComare;
            }
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
