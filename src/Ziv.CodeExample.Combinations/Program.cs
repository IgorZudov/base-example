using System;
using System.Collections.Generic;
using System.Linq;

namespace Ziv.CodeExample.Combinations
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var result = Compute(
                new []
                {
                    1, 2, 3, 4, 5, 6, 23, 123, 75, 2, 9, 98, 345, 78, 33, 57, 96, 44, 22, 78, 4, 32, 34, 5, 899, 53, 2,
                    4, 3, 89, 64, 568, 343785, 43257, 83, -10, 0, -1,6,3,-43
                }, 10, 3);

            foreach (var part in result)
            {
                foreach (var i in part)
                    Console.Write($"{i} ");
                
                Console.Write("\n");
            }
        }

        private static IEnumerable<IEnumerable<int>> Compute(int[] values, int targetNumber, int length)
        {
            var permutations = GetPermutations(values, length).ToList();
            return permutations.Where(x => x.Sum() == targetNumber).ToList();
        }

        private static IEnumerable<IEnumerable<int>> GetPermutations(IEnumerable<int> items, int count)
        {
            var i = 0;
            var enumerable = items as int[] ?? items.ToArray();
            foreach (var item in enumerable)
            {
                if (count == 1)
                    yield return new[] {item};
                else
                {
                    foreach (var result in GetPermutations(enumerable.Skip(i + 1), count - 1))
                        yield return new[] {item}.Concat(result);
                }

                ++i;
            }
        }
    }
}