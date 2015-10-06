namespace PermutationWithRepetition
{
    using System;

    class PermutationWithRepetitionMain
    {
        static void Main()
        {
//            var set = new int[] {1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5};
            var set = new int[] { 1, 3, 5, 5 };
            Permute(set, 0, set.Length);
        }


        public static void Permute(int[] numbers, int start, int resultSetCount)
        {
            PrintResult(numbers);
            int tmp = 0;

            if (start < resultSetCount)
            {
                for (int i = resultSetCount - 2; i >= start; i--)
                {
                    for (int j = i + 1; j < resultSetCount; j++)
                    {
                        if (numbers[i] != numbers[j])
                        {
                            // swap ps[i] <--> ps[j]
                            tmp = numbers[i];
                            numbers[i] = numbers[j];
                            numbers[j] = tmp;

                            Permute(numbers, i + 1, resultSetCount);
                        }
                    }

                    // Undo all modifications done by
                    // recursive calls and swapping
                    tmp = numbers[i];
                    for (int k = i; k < resultSetCount - 1;)
                        numbers[k] = numbers[++k];
                    numbers[resultSetCount - 1] = tmp;
                }
            }
        }

        private static void PrintResult(int[] resultSet)
        {
            Console.WriteLine($"{{{string.Join(",", resultSet)}}}");
        }
    }
}
