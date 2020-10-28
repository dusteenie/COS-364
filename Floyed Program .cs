using System;
using System.IO;

namespace GraphFloyd
{
    /*
     * GraphFloyd is a program which simulates the Floyd Algorithem. 
     * 
     * @author Dr. Gail Lange
     * @author Sarah Stepak
     * @see https://drive.google.com/drive/u/2/folders/1kL54gaSDDU5xCsQjjwcAYdrp8pAQZX0d
     *     The following link contains the data for the program
     * @since   10/28/2020
     */
    class GraphFloyd
    {
        public const int INFINITY = 30000;
        public static int[,] adjacency;         // The adjacency matrix
        public static int numVertex;             // Number of vertices
        private static int[,] D;
        private static int[,] P;


        /*
         * Floyd() is a method that parses through arrays D and P to simulate
         * the Floyd algorithem
         * 
         * @author  Sarah Stepak
         * @return  void
         */
        public static void Floyd() 
        {
            /*
             * Declares and initalizes variables D,P,len. 
             * 
             * @Var D   int[,]    D[i,j] contains the length of the shortest path
             * @Var P   int[,]    P[i,j] contains the highest index on the shortest path from vi to vj; -1 otherwise.
             *          
             *                    Both D and P are the same length as the adjacency array. 
             *          
             * @Var len   int     Contains the length of the adjacency array
             */
            int[,] D = new int[adjacency.GetLength(0), adjacency.GetLength(1)];
            int[,] P = new int[adjacency.GetLength(0), adjacency.GetLength(1)];

            // Initalizes D and P
            for (int r = 0; r < adjacency.GetLength(0); r++) 
            {
                for(int c = 0; c < adjacency.GetLength(1); c++) 
                {
                    // Sets D's values equal to the the adjacency array
                    // initalizes P to -1
                    D[r, c] = adjacency[r, c];
                    P[r, c] = -1;
                }
            }

            // Floyd algorithem
            for (int k = 0; k < numVertex; k++) 
            {
                for (int i = 0; i < numVertex; i++) 
                {
                    for(int j = 0; j < numVertex; j++)
                    {
                        D[i, j] = Math.Min(D[i, j], (D[i, k] + D[k, j]));  // Sets D[i,j] to the shortest path
                    }
                }
            }


            // TO-DO
            /* 
                Lastly, do your outputs. 
                (1) Output the final D matrix which will give you the lengths of the short est paths between each pair of vertices. 
                (2) Output the path matrix P. (3) Output all the shortest paths, utilizing Algorithm 3.5 on page 109 (as modified). 
                    Your function will call the path function. 
             */

        }

        /*
         *  If at least one intermediate vertex exists:
         *      Takes the P array and sets the highest index of an intermediate 
         *      vertex on the shortest path from p to r. 
         *  P is kept as -1 otherwise.
         * 
         * @author      Sarah Stepak
         * @param q     int     Used as vertex
         * @param r     int     Used as vertex
         * @return      void
         */
        public static void path(int q, int r) 
        {
            if(P[q,r] != -1) 
            {
                path(q, P[q, r]);
                path(P[q, r], r);
            }
        }


        /*
         * Prints the number of vertices as well as the adjacency matrix.
         *
         * @author  Sarah Stepak
         * @return  void
         */
        public static void printGraph() 
        {
            // Prints the adjacency array
            for (int r = 0; r < adjacency.GetLength(0); r++) 
            {
                for(int c = 0; c < adjacency.GetLength(1); c++) 
                {
                    Console.Write(" "+ adjacency[r,c] + ",");
                }
                Console.Write("\n");
            }

            // Prints the number of verticies 
            Console.WriteLine("Number of Verticies: " + numVertex);

        }


        /*
         * CreateGraph(param) is a method which parses through the datafile, and assigns
         * appropiate values to global variables: adjacency[,] and numVertex.
         * 
         * @author  Dr. Gail Lange
         * @author  Sarah Stepak
         * @param   rdr     StreamReader    contains data from the file
         * @return  void
         * 
         * @see https://drive.google.com/drive/u/2/folders/13HRrYGaF3roY23DmgDKvrarMGzrW8lcA
         * 
         */
        public static void createGraph(StreamReader rdr)
        {
            string line;
            string[] s; // for split  
            int i, j;

            try
            {
                // get the data from the file and put it into variables
                int k = 0;
                i = 0; j = 0;

                using(rdr)
                { 
                    // parses through the data file
                    while(rdr.EndOfStream == false) 
                    {
                        // Reads and splits the current line in the file
                        line = rdr.ReadLine();
                        s = line.Split(' ');                        


                        //Ignores line 1
                        if (s[0] == "D" || s[0] == "U") { k++; continue; } 

                        // Converts string to int, and initalizes numVertex to the given number of vertices
                        else if (k == 1)
                        {  
                            numVertex = Int32.Parse(s[0]);
                            k++; continue; }

                        //Ignores line 3
                        else if (k == 2) { k++; continue;}

                        else
                        {
                            // Checks to see if the next line is the last line. If so, sets j to length n
                            if (rdr.Peek() == -1) { j = s.Length; continue; }

                            // Counts total number of rows
                            else { i++; continue; }
                        }
                    }

                    // Declares adjacency array length
                    adjacency = new int[i, j];

                    // resets rdr's stream back to the top of the file
                    rdr.DiscardBufferedData();
                    rdr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                    i = 0;  j = 0;  k = 0; // resets i,j,k

                    // parses through the data file
                    while (rdr.EndOfStream == false)
                    {
                        // Reads and splits the current line in the file
                        line = rdr.ReadLine();
                        s = line.Split(' ');

                        // ignores the first 3 lines
                        if (k < 3) { k++; continue; }
                        
                        else
                        {
                            // checks to make sure we are not on the last line of the file
                            if (rdr.Peek() != -1)
                            {
                                // fills the adjacency array
                                for (j = 0; j < adjacency.GetLength(1); j++)
                                {
                                    adjacency[i, j] = Convert.ToInt32(s[j]);
                                }

                                i++; continue; // goes to the next line
                            }
                        }
                    }

                }
                rdr.Close();
                return;
            } // end try

            catch (IOException e)
            {
                Console.WriteLine("Some I/O problem", e.ToString());
            }

        } // end CreateGraph	




        /*
         * Main method.
         * 
         * @author  Dr. Gail Lange
         * @param   args[0]     Contains the file name for the dataset
         * @return  void
         */
        static void Main(string[] args)
        {
            // args[0] is the first command line argument you supply.            
            StreamReader rdr;

            // assign filename when define or get error in catch block
            String filename = null;

            try
            {
                filename = args[0];
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("You must enter the filename on the command line",
                    e.ToString());
            }

            try
            {
                rdr = new StreamReader(filename);

                createGraph(rdr);

                printGraph();

                Floyd();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File {0} was not found ", filename, e.ToString());
            }

            return;
        }
    }
}
