using System;

namespace GraphFloyd
{
    /*
     * TO-Do: Explain this program
     * 
     * @author Dr. Gail Lange
     * @author Sarah Stepak
     * @see https://drive.google.com/drive/u/2/folders/1kL54gaSDDU5xCsQjjwcAYdrp8pAQZX0d
     *     The following link contains the data for the program
     * @since   10/22/2020
     */
    class GraphFloyd
    {
        public const int INFINITY = 30000;
        public static int[,] adjacency;         // The adjacency matrix
        public static int numVertex;             // Number of vertices
        private static int[,] D,P;


        /*
         * TO-DO    Explain Floyd()
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
             *          Both D and P are the same length as the adjacency array. 
             *          
             * @Var len   int     Contains the length of the adjacency array
             */
            int[,] D = new int[adjacency.GetLength(0), adjacency.GetLength(1)];
            int[,] P = new int[adjacency.GetLength(0), adjacency.GetLength(1)];
            int len = adjacency.GetLength(0);

            for(int r = 0; r < len; r++) 
            {
                for(int c =0; c < len; c++) 
                {
                    // Sets D's values equal to the the adjacency array
                    // initalizes P to -1
                    D[r, c] = adjacency[r, c];
                    P[r, c] = -1;
                }
            }

            // Floyd algorithem
            for (int k = 0; k < len; k++) 
            {
                for (int i = 0; i < len; i++) 
                {
                    for(int j = 0; j < len; j++)
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
        }


        /*
         * TO-DO:   Explain createGraph()
         * 
         * @author  Dr. Gail Lange
         * @author  Sarah Stepak
         * @param   rdr     StreamReader    //explain what it is
         * @return  void
         * 
         * @see https://drive.google.com/drive/u/2/folders/13HRrYGaF3roY23DmgDKvrarMGzrW8lcA
         * 
         */
        public static void createGraph(StreamReader rdr)
        {
            /* Data file: Line 1 has either a D or a U
                          Line 2 has an integer representing the number of vertices
                                then spaces then a comment
                          Line 3 has a comment # List of edges
                          all the other lines have the following charactristic:
              Data file has format v1 v2 dist with just 1 space between
              each due to fact that split works that way. If put two spaces
              between v2 and dist, we get an error. 
              The last line has -1's (n of them) each separated by one space
              Put your dataset into the Debug folder of the Visual Studio directory
              created for your project.
              Set up command line argument for the dataset. Go to 
              Project | Properties and then Debug and for Command Line Arguments
              fill in the name of your data file.
           */
            string line;
            string[] s; // for split            
            int i, j;

            try
            {
                // get the data from the file and put it into variables







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

