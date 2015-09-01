namespace StudentsAndCourses
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class StudentsAndCoursesMain
    {
        static void Main()
        {
            var dict = new SortedDictionary<string, SortedSet<Student>>();
            StreamReader streamReader = new StreamReader(@"../../students.txt");
            string line = streamReader.ReadLine();
            while (line != null)
            {
                var inputParts = line.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                var dictKey = inputParts[2].Trim();
                if (!dict.ContainsKey(dictKey))
                {
                    dict.Add(dictKey, new SortedSet<Student>
                    {
                        new Student
                        {
                            FirstName = inputParts[0].Trim(),
                            LastName = inputParts[1].Trim()
                        }
                    });
                }
                else
                {
                    dict[dictKey].Add(new Student
                    {
                        FirstName = inputParts[0].Trim(),
                        LastName = inputParts[1].Trim()
                    });
                }

                line = streamReader.ReadLine();
            }

            PirntResult(dict);
        }

        private static void PirntResult(SortedDictionary<string, SortedSet<Student>> dict)
        {
            foreach (var item in dict)
            {
                Console.WriteLine($"{item.Key}: {string.Join(", ", item.Value.Select(s=>s.ToString()))}");
            }
        }
    }
}
