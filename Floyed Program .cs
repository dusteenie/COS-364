using System;

namespace GraphFloyd
{
    class GraphFloyd
    {
        public const int INFINITY = 30000;
        public static int[,] adjacency;         // The adjacency matrix
        public static int numVertex;             // Number of vertices

        private static int[,] D,P;


        private static void Floyd() 
        {
            int[,] D = new int[adjacency.GetLength(0), adjacency.GetLength(1];
            int[,] P = new int[adjacency.GetLength(0), adjacency.GetLength(1];

            int rowLen = adjacency.GetLength(0);
            int colLen = adjacency.GetLength(1);

            // The following initalizes arrays D and P.
            for(int r = 0; r < rowLen; r++) 
            {
                for(int c =0; c < colLen; c++) 
                {
                    D[r, c] = adjacency[r, c];
                    P[r, c] = -1;
                }
            }

            // Floyd algorithem
            // rowLen needs to be replaced with the number of verticies
            for (int i = 0; i < rowLen; i++) 
            {
                for (int j = 0; j < rowLen; j++) 
                {
                    for(int k = 0; k < rowLen; k++)
                    {
                        // D[j,k] = minimum(D[j,k], (D[j,i]+D[i,k]))
                    }
                }
            }

            // Refrence path function on page 109; Algorithem 3.5

            // Ask about create graph


        }

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
