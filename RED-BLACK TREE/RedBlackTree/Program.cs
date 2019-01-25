using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {


            REDBLACKTREE b1 = new REDBLACKTREE();

            for (int i = 1; i <= 10; i++)
            {
                b1.Add(i);
            }
            //printing black height for testing purpose...
            b1.BlackHeight();
            b1.inorder();
            Console.Write(">>>>>TOTAL SIZE OF TREE = ");
            Console.WriteLine(b1.Count());
            Console.Write(">>>>>TOTAL HEIGHT OF TREE = ");
            Console.WriteLine(b1.Height());
            Console.WriteLine("=============================");
            Console.WriteLine("AFTER DELETION");
            Console.WriteLine("=============================");
            b1.delete(5);
            b1.inorder();
            Console.Write(">>>>>TOTAL SIZE OF TREE = ");
            Console.WriteLine(b1.Count());
            Console.Write(">>>>>TOTAL HEIGHT OF TREE = ");
            Console.WriteLine(b1.Height());
            //printing black height for testing purpose...
            b1.BlackHeight();




        }
    }
}
