namespace SubsetOfStringArray
{
    using System;

    static class SubsetOfStringArrayMain
    {
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
                    subset[index] = s[i];
                    MakeSubsets(s, k, subset, i + 1);
                }
            }
        }
    }
}
