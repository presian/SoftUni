namespace SubsetOfStringArray
{
    using System;

    static class SubsetOfStringArrayMain
    {
        private static int greatest = 0;
        static void Main()
        {
            var s = new string[]
            {
                "test",
                "rock",
                "fun"
            };

            var k = 2;
            var subset = new string[k];
            MakeSubsets(s, k, subset, 0);
        }

        private static void MakeSubsets(string[] s, int k, string[] subset, int index)
        {
            if (index >= k)
            {
                Console.WriteLine($"({string.Join(" ", subset)})");
            }
            else 
            {
                for (int i = index; i < s.Length; i++)
                {
                    if (i == 0)
                    {
                        subset[index] = s[i];
                        MakeSubsets(s, k, subset, i + 1);
                    }
                    else if (i > greatest)
                    {
                        subset[index] = s[i];
                        greatest = i;
                        MakeSubsets(s, k, subset, i + 1);
                    }
                }
            }
        }
    }
}
