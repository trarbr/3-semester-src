using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bt05
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree bst = new BinarySearchTree();
            Random rnd = new Random();

            bst.Insert(20);
            bst.Insert(10);
            bst.Insert(40);
            bst.Insert(30);
            bst.Insert(60);
            bst.Insert(5);
            bst.Insert(12);
            bst.Insert(8);

            int[] inOrder = bst.GetInOrder();
            int[] preOrder = bst.GetPreOrder();
            int[] postOrder = bst.GetPostOrder();
            printArray(inOrder);
            printArray(preOrder);
            printArray(postOrder);

            Console.WriteLine("=== POST DELETION ===");
            bst.Delete(10);
            bst.Delete(20);
            bst.Delete(40);
            bst.Delete(30);
            bst.Delete(60);
            bst.Delete(5);
            bst.Delete(8);
            bst.Delete(12);

            inOrder = bst.GetInOrder();
            preOrder = bst.GetPreOrder();
            postOrder = bst.GetPostOrder();
            printArray(inOrder);
            printArray(preOrder);
            printArray(postOrder);

            Console.ReadLine();
        }

        private static void printArray(int[] array)
        {
            foreach (var value in array)
            {
                Console.WriteLine(value);
            }
            Console.WriteLine("");
        }
    }
}
