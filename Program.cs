using System;
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
            int? identityElem = FindIdentity(table);
            bool hasIdentity = (identityElem != null);
            bool associative = IsAssociative(table);
            bool commutative = IsCommutative(table);
            bool allElementsHasInverse = AreAllElementsHasInverse(table, identityElem);

            if (!associative)
            {
                Console.WriteLine("Magma");
            }
            else if (!hasIdentity)
            {
                Console.WriteLine("Semigroup");
            }
            else if (!allElementsHasInverse)
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

        static int? FindIdentity(int[][] table)
        {
            for (int i = 0; i < table.Length; ++i)
            {
                bool hasIdentity = true;
                for (int j = 0; j < table.Length; ++j)
                {
                    hasIdentity = hasIdentity && ((table[i][j] == j + 1) && (table[j][i] == j + 1));
                }
                if (hasIdentity)
                {
                    return table[i][i];
                }
            }
            return null;
        }

        static bool AreAllElementsHasInverse(int[][] table, int? identityElem)
        {
            if (identityElem == null)
            {
                return false;
            }
            bool result = true;
            for (int i = 0; i < table.Length; ++i)
            {
                bool hasInverse = false;
                for (int j = 0; j < table.Length; ++j)
                {
                    hasInverse = hasInverse || (table[i][j] == identityElem && table[j][i] == identityElem);
                }
                result = result && hasInverse;
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
