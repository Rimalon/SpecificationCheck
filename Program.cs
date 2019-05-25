﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Specifications
{
    class Program
    {
        static void Main(string[] args)
        {
            var table = GetTable(args[0]);
            int? neutral = FindNeutral(table);
            bool hasNeutral = (neutral != null);
            bool associative = IsAssociative(table);
            bool commutative = IsCommutative(table);
            bool allElementsHasInvert = AreAllElementsHasInvert(table, neutral);

            if (!associative)
            {
                Console.WriteLine("Magma");
            }
            else if (!hasNeutral)
            {
                Console.WriteLine("Semigroup");
            }
            else if (!allElementsHasInvert)
            {
                if (!commutative)
                {
                    Console.WriteLine("Monoid");
                }
                else
                {
                    Console.WriteLine("Commutative Monoid");
                }
            }
            else if (!commutative)
            {
                Console.WriteLine("Group");
            }
            else
            {
                Console.WriteLine("Abelian Group");
            }
        }

        static int[][] GetTable(string file)
        {
            var rowsList = new List<String>();
            using (var reader = new StreamReader(file))
            {
                var row = reader.ReadLine();
                while(row != null)
                {
                    rowsList.Add(row);
                    row = reader.ReadLine();
                }
            }
            int[][] result = new int[rowsList.Count][];
            for (int i = 0; i < rowsList.Count; ++i)
            {
                result[i] = rowsList[i].Split(' ').Select(x => int.Parse(x)).ToArray();
            }
            return result;
        }

        static bool IsCommutative(int[][] table)
        {
            bool result = true;
            for (int i = 0; i < table.Length; ++i)
            {
                for (int j = i + 1; j < table.Length; ++j)
                {
                    result = result && (table[i][j] == table[j][i]);
                }
            }
            return result;
        }

        static int? FindNeutral(int[][] table)
        {
            for (int i = 0; i < table.Length; ++i)
            {
                bool hasNeutral = true;
                for (int j = i + 1; j < table.Length; ++j)
                {
                    hasNeutral = hasNeutral && (table[i][j] == j + 1 && table[j][i] == j + 1);
                }
                if (hasNeutral)
                {
                    return table[i][i];
                }
            }
            return null;
        }

        static bool AreAllElementsHasInvert(int[][] table, int? neutral)
        {
            if (neutral == null)
            {
                return false;
            }
            bool result = true;
            for (int i = 0; i < table.Length; ++i)
            {
                bool hasInvert = false;
                for (int j = 0; j < table.Length; ++j)
                {
                    hasInvert = hasInvert || (table[i][j] == neutral && table[j][i] == neutral);
                }
                result = result && hasInvert;
            }
            return result;
        }

        static bool IsAssociative(int[][] table)
        {
            bool result = true;
            for (int i = 0; i < table.Length; ++i)
            {
                for (int j = 0; j < table.Length; ++j)
                {
                    for (int k = 0; k < table.Length; ++k)
                    {
                        result = result && (table[table[i][j] - 1][k] == table[i][table[j][k] - 1]);
                    }
                }
            }
            return result;
        }

    }
}