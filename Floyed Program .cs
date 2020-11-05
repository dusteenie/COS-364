using System;
using System.IO;
using System.Reflection;

namespace GraphFloyd
{
    /*
     * GraphFloyd is a program which simulates the Floyd Algorithem. 
     * 
     * @author Dr. Gail Lange
     * @author Dusty Stepak
     * @since   11/05/2020
     */
    class GraphFloyd
    {
        public const int INFINITY = 30000;
        public static int[,] adjacency;         // The adjacency matrix
        public static int numVertex;             // Number of vertices
        public static int[,] D;
        public static int[,] P;


        /*
         * Floyd() is a method that parses through arrays D and P to simulate
         * the Floyd algorithem
         * 
         * @author  Dusty Stepak
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

            // Gail added the following two lines
            // initalizes D and P to be a square matrix of size numVertex
            D = new int[numVertex, numVertex];
            P = new int[numVertex, numVertex];



            // P is initalized to -1 and D is initalized to the adjacency array
            for (int r = 0; r < numVertex; r++)
                for (int c = 0; c < numVertex; c++)
                {
                    P[r, c] = -1;
                    D[r, c] = adjacency[r, c];
                }


            // Floyd algorithem
            for (int k = 0; k < numVertex; k++)
            {
                for (int i = 0; i < numVertex; i++)
                {
                    for (int j = 0; j < numVertex; j++)
                    {
                        if ((D[i, k] + D[k, j]) < D[i, j])
                        {
                            D[i, j] = D[i, k] + D[k, j];
                            P[i, j] = k;
                        }
                    }
                }
            }

            // Outputs the final D matrix
            Console.WriteLine("\nFinal D Matrix: ");
            for (int r = 0; r < D.GetLength(0); r++)
            {
                for (int c = 0; c < D.GetLength(1); c++)
                {
                    if (D[r, c] == INFINITY)
                        Console.Write(" oo");
                    else
                        Console.Write(" " + D[r, c]);
                }
                Console.Write("\n");
            }

            // Outputs the P matrix
            Console.WriteLine("\nP Matrix: ");
            for (int r = 0; r < numVertex; r++)
            {
                for (int c = 0; c < numVertex; c++)
                {
                    Console.Write(" " + P[r, c]);
                }
                Console.Write("\n");
            }

            // Calls path() for every value within the P matrix 
            Console.Write("\nPath: ");
            for (int r = 0; r < numVertex; r++)
            {
                for (int c = 0; c < numVertex; c++)
                {
                    path(r, c);
                }
            }

        }

        /*
         *  If at least one intermediate vertex exists:
         *      Takes the P array and sets the highest index of an intermediate 
         *      vertex on the shortest path from p to r. 
         *  P is kept as -1 otherwise.
         * 
         * @author      Dusty Stepak
         * @param q     int     Used as vertex
         * @param r     int     Used as vertex
         * @return      void
         */
        public static void path(int q, int r)
        {
            if (P[q, r] != -1)
            {
                Console.Write("\nPath [" + q + ", " + r + "]\t");
                path(q, P[q, r]);
                Console.Write("v" + P[q, r] + " ");
                path(P[q, r], r);
            }

        }


        /*
         * Prints the number of vertices as well as the adjacency matrix.
         *
         * @author  Dusty Stepak
         * @return  void
         */
        public static void printGraph()
        {
            // Prints the number of Vertices 
            Console.WriteLine("Number of Vertices: " + numVertex);

            // Prints the adjacency rray
            Console.WriteLine("\nThe adjacency array:");
            for (int r = 0; r < numVertex; r++)
            {
                for (int c = 0; c < numVertex; c++)
                {
                    if(adjacency[r, c] == INFINITY)
                        Console.Write(" oo");
                    else
                        Console.Write(" " + adjacency[r, c]);
                }
                Console.Write("\n");
            }

            // Prints the Key
            Console.WriteLine("Where oo is equal to: " + INFINITY);

        }


        /*
         * CreateGraph(param) is a method which parses through the datafile, and assigns
         * appropiate values to global variables: adjacency[,] and numVertex.
         * 
         * @author  Dr. Gail Lange
         * @author  Dusty Stepak
         * @param   rdr     StreamReader    contains data from the file
         * @return  void
         */
        public static void createGraph(StreamReader rdr)
        {
            string line;
            string[] s; // for split  

            try
            {
                // get the data from the file and put it into variables
                int k = 0;

                using (rdr)
                {
                    // parses through the data file
                    while (rdr.EndOfStream == false)
                    {
                        // Reads and splits the current line in the file
                        line = rdr.ReadLine();
                        s = line.Split(' ');


                        //Ignores line 1
                        if (s[0] == "D" || s[0] == "U") { k++; continue; }

                        // Converts string to int, and initalizes numVertex to the given number of vertices
                        // Initalizes adjacency array to the size of numVertex as well.
                        // Sets non declared numbers in the adjacency array to infinity.
                        else if (k == 1)
                        {
                            numVertex = Int32.Parse(s[0]);
                            adjacency = new int[numVertex, numVertex];
                            // Sets adjacency to initally be 0
                            for (int r = 0; r < numVertex; r++)
                                for (int c = 0; c < numVertex; c++)
                                    adjacency[r, c] = 0;
                            k++; continue; //next line
                        }

                        //Ignores line 3
                        else if (k == 2) { k++; continue; }

                        else
                        {
                            // Checks to see if the current line is not the last line. If this is the case, then
                            // the values in the adjacency array are updated to those in the data file.
                            if (rdr.Peek() != -1) 
                            {
                                int x, y, result;
                                x = Int32.Parse(s[0]); y = Int32.Parse(s[1]); result = Int32.Parse(s[2]);
                                adjacency[x, y] = result;
                                continue; 
                            }
                        }
                    }

                    // Replaces 0 with oo, if there is a zero not on the diagnol
                    for (int i = 0; i < numVertex; i++)
                        for (int j = 0; j < numVertex; j++)
                            if (i != j && adjacency[i, j] == 0)
                                adjacency[i, j] = INFINITY;

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
