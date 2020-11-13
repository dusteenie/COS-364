using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace umf.cos364
{
    /*
    * GraphPrim is a program which simulates the Prim Algorithem. 
    * 
    * @author Dr. Gail Lange
    * @author Sarah Stepak
    * @since   11.12.2020
    */
    class GraphPrim
    {
        public const int INFINITY = 30000;
        public static int[,] adjacency;         // The adjacency matrix
        public static int numVertex;            // Number of vertices

        /*
         * Prim() is a method which parses through the adjacency array starting at vertex n; 
         * It simulates the prim algorithem.
         * 
         * @author  Dr. Gail Lange
         * @author  Sarah Stepak
         * @param   int     n   Iteger value to mark the starting vertex
         * @return  void
         * @since   11.12.2020
         * 
         */
        public static void Prim(int n) 
        {

            // vnear is the vertex
            // nearest[vnear] is a vertex subscript
            // min is the minimal distance between
            int min; int vnear = n;

            //Declares 1D global arrays distance and nearest, to be an array of size numVertex
            int[] distance;
            int[] nearest;            
            distance = new int[numVertex]; 
            nearest = new int[numVertex];

            // Initializes arrays distance and nearest at v1 to be the nearest vertex and 
            // initializes the distance from the point to be the weight on the edge to v1.
            for (int i = 0; i < numVertex; i++)
            {
                distance[i] = adjacency[n,i];
                nearest[i] = n;
            }

            Console.WriteLine("\n\n");

            for (int i = n; i <= numVertex - 1; i++)
            {
                min = INFINITY; //resets min to oo
                for (int j = 1; j < numVertex; j++)
                {
                    // Sets the vertex and distance to be the shortest path
                    if (0 <= distance[j] && distance[j] < min)
                    {
                        min = distance[j];
                        vnear = j;

                    }
                }

                // writes the subscripts of the vertices as well as the edge added to the MST
                Console.WriteLine("vertex: " + vnear + "\tvertex subscript added to the MST: " + nearest[vnear]);


                // Sets distance[vnear] to -1 so vnear does not get readded to the MST
                distance[vnear] = -1;

                // Updates distance and nearest arrays to reflect the addition of vertex with subscript vnear to the MST
                for (int k = 1; k < numVertex; k++)
                {
                    if (adjacency[k, vnear] < distance[k])
                    {
                        distance[k] = adjacency[k, vnear];
                        nearest[k] = vnear;
                    }
                }


            }

        }

        /*
         * CreateGraph(param) is a method which parses through the datafile, and assigns
         * appropiate values to global variables: adjacency[,] and numVertex.
         * 
         * @author  Dr. Gail Lange
         * @author  Sarah Stepak
         * @param   rdr     StreamReader    contains data from the file
         * @return  void
         * @since   11.05.2020
         */
        public static void createGraph(StreamReader rdr)
        {
            /* Data file has format v1 v2 dist with just 1 space between
              each due to fact that split works that way. If put two spaces
              between v2 and dist, we get an error. 
           */
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
                                adjacency[y, x] = result; // Makes the matrix symmetric

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

        } 



        /*
         * Prints the number of vertices as well as the adjacency matrix.
         *
         * @author  Sarah Stepak
         * @return  void
         * @since   11.05.2020
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
                    if (adjacency[r, c] == INFINITY)
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

                Prim(0);  // 0 is the start vertex
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File {0} was not found ", filename, e.ToString());
            }

            return;
        }
    }
}
