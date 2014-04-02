using System;
using System.Collections.Generic;

namespace Tree {

    class Program {

        static void Main() {
            var tree = new BinaryTree<int, int>();
            var rnd = new Random();
            for (int i = 0; i < 1030; ++i) {
                tree.Insert(rnd.Next(0,1030), i);
            }
            for (int i = 0; i < 1000; ++i) {
                tree.Remove(i);
            }

            if (!tree.IsCorrect()) throw new Exception();

            Console.WriteLine("---BFS---");
            IEnumerator<int> a = tree.GetBFSEnumerator();
            while (a.MoveNext()) {
                Console.WriteLine(a.Current);
            }

            Console.WriteLine();
            Console.WriteLine("---DFS---");
            IEnumerator<int> b = tree.GetDFSEnumerator();
            while (b.MoveNext()) {
                Console.WriteLine(b.Current);
            }

            Console.ReadKey();

        }

    }
}
