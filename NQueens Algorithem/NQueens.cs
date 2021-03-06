﻿using System;
using System.IO;

namespace NQueens
{
    public class NQueens
    {

        /*0
         * NQueens is a program which takes an n by n matrix, 
         * and finds where to place n queens on the matrix.
         * Outputs the total number of solutions 
         * 
         * @author  Dr. Gail Lange
         * @author  Sarah Stepak
         * @since   12/2/2020
         */


        public static StreamWriter wtr;
        private static int numSolutions; // Contains total number of solutions
        private static int[] col;
        private static int n;  // Holds the total number of queens


        /*
         * playQueens()
         * 
         * @author  Sarah Stepak
         * @param   int     i    holds the current position in col
         * @return  void
         * @since   12/2/2020
         */
        static void playQueens(int i) 
        {

            for (int j = 0; j < n; j++) // for each child u of v
            {
                // assigns the i+1 value to be j
                col[i + 1] = j;

                // Checks to see if there is a possible solution at i+1.
                if (promising(i+1))
                {
                    // Increments numSolutions when i+1 is equal to 
                    // the total number of possible solutions (n)
                    if (i + 1 == n-1) { 
                        numSolutions++; 

                        // formats the output in wtr
                        foreach (int item in col) { wtr.Write( item + " "); }
                        wtr.WriteLine(""); 
                    }
                
                    else {
                        // recursive call to playQueens at i+1
                        playQueens(i+1);
                    }
                }
            } // End method


            /*
             * PREVIOUS SOLUTION:
             *

            int j;

            // checks to see if there is a possible solution at i
            if (promising(i)) {

                // Checks to see if i+1 is equal to the total number of solutions
                if (i+1 == n) {

                    //increments the total number of solutions up by one
                    numSolutions++;

                    // formats the output in wtr
                    foreach (int item in col) { wtr.Write( item + " "); }
                    wtr.WriteLine(""); 
                }


                else {
                    // assigns the i+1 value to be j
                    // recursive call to playQueens at i+1
                    for (j = 0; j < n; j++) {
                        col[(i+1)] = j;
                        playQueens(i + 1);
                    }
                } 
            } // End method
            */
        }

        /*
         * promising() is a boolean function which checks to see if a
         * queen can be placed at a location. Return values are true/ false.
         * 
         * @author  Sarah Stepak
         * @param   int     i   holds the current position in col
         * @return  Boolean     returns true if a position can have a queen placed
         *                      false otherwise.
         * @since   11/30/2020
         */
        static Boolean promising(int i) 
        {
            // Parses through all values from 0 to i
            for(int k = 0; k < i; k++)
            {
                // Finds any discreptency such that a queen cannot be placed at 
                // the current position within col
                if (col[i] == col[k]  || 
                    (Math.Abs(col[i] - col[k])) == Math.Abs(i - k)) 
                {
                    return false; //Breaks out of loop                   
                }
            }
            // Function did not find any discreptencys, and returns true.
            return true;
        }


        /*
         * Main method.
         * 
         * @author  Dr. Gail Lange
         * @param   args[0]   contains a filename, if a file is used for set-up
         * @return  void
         * @since   11/19/2020
         */
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
