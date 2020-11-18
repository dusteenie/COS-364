using System;
using System.IO;

namespace NQueens
{
    public class NQueens
    {
        public static StreamWriter wtr;
        // Reserve other class variables
        // Write the methods the Main calls
        private static int numSolutions;
        private static int[] col;
        private static int n;

        static void playQueens(int variable) { }
        static void checkNode(int variable) { }


        static void Main(string[] args)
        {
            string filename = null;

            Console.WriteLine("Enter the number of queens you want\n");
            n = Convert.ToInt32(Console.ReadLine());

            try
            {
                filename = args[0];
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("You must enter a filename on the command line.");
                e.ToString();
            }

            try
            {
                wtr = new StreamWriter(filename);
                col = new int[n];
                playQueens(-1);
                wtr.WriteLine("The number of solutions is " + numSolutions);
                wtr.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File {0} was not found ", filename);
                e.ToString();
            }

            return;
        }  // end Main

    } // end class NQueens
}
