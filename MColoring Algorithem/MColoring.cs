using System;
using System.IO;

namespace MColoring
{
	class MColor
	{
		/*
		 * MColor is a program which simulates the M_Coloring Algorithem. 
		 * 
		 * @author Dr. Gail Lange
		 * @author Sarah Stepak
		 * @since   12/10/2020
		 */

		public const int INFINITY = 30000;
		public static int[,] adjacency;         // The adjacency matrix
		public static int numVertex;             // Number of vertices        
		private static int numsol;
		private static int[] vcolor;
		private static int numcolors;
		private static StreamWriter wtr;
		private static StreamReader rdr;


		/*
		* createGraph() parses through the user input file and initalizes/declares all the
		*  neccesary globar variables.
		* 
		* @author  Dusty Stepak
		* @param   rdr	 StreamReader	rdr contains the user input file. 
		* @return  void
		* @since   12/09/2020
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
							//adjacency = new int[numVertex][numVertex];  Console does not like this
							
							// Declares adjacency
							adjacency = new int[numVertex,numVertex];

							// Declares vcolor
							vcolor = new int[numVertex];

							// Sets adjacency to initally be 0
							for (int r = 0; r < numVertex; r++)
								for (int c = 0; c < numVertex; c++)
									adjacency[r,c] = 0;
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

								adjacency[x,y] = result;
								adjacency[y,x] = result; // Makes the matrix symmetric

								continue;
							}
						}
					}

					// Replaces 0 with oo, if there is a zero not on the diagnol
					for (int i = 0; i < numVertex; i++)
						for (int j = 0; j < numVertex; j++)
							if (i != j && adjacency[i,j] == 0)
								adjacency[i,j] = INFINITY;

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
         * m_coloring() is an algorithem which calculates m colours such that no 
		 *  two adjacent vertices of the graph are colored with the same color.
		 *  This specific algorithem follows the extend() algorithem.
         * 
         * @author  Dusty Stepak
         * @param   int     i    holds the current position in vcolor
         * @return  void
         * @since   12/09/2020
         */
		public static void m_coloring(int i) 
		{
			for (int j = 1; j <= numcolors; j++) 
			{
				vcolor[i + 1] = j;

				if (promising(i + 1) == true) 
				{
					if ((i + 1) == (numVertex - 1))
					{
						numsol++;
						printGraph();
					}
					else 
					{
						m_coloring(i + 1);
					}
				}
			}
		}


		/*
		* promising() 
		* 
		* @author  Dusty Stepak
		* @param   int     i   holds the current position in vcolor
		* @return  Boolean     returns true if xxxxxx
		*                      false otherwise.
		* @since   12/10/2020
		*/
		static Boolean promising(int i) 
		{
			// Parses through all values from 0 to i
			for (int k = 0; k < i; k++)
			{
				 
				//Console.WriteLine(i + " " + k);
				if (adjacency[i,k] == 1 &&
					vcolor[i] == vcolor[k])
				{
					return false; //Breaks out of loop                   
				}
			}
			return true;
		}

		/*
		* printGraph() prints the current row of vcolor[] to the output file.
		* 
		* @author  Dusty Stepak
		* @return  void
		* @since   12/09/2020
		*/
		public static void printGraph()
		{
			// formats the output in wtr
			foreach (int item in vcolor) { wtr.Write(item + " "); }
			wtr.WriteLine("");
		}


		/*
         * Main method.
         * 
         * @author  Dr. Gail Lange
         * @return  void
         * @since   12/09/2020
         */
		static void Main(string[] args)
		{
			String filename1 = "results.txt";   // output file
			String filename2 = "ColorA.txt";    // input file

			try
			{
				wtr = new StreamWriter(filename1);
				wtr.WriteLine("Colors are:\n\n");
				rdr = new StreamReader(filename2);

				createGraph(rdr);

				//printGraph();

				numcolors = 4;
				numsol = 0;

				m_coloring(-1);  // 0 is start vertex

				wtr.WriteLine("Number of solutions: " + numsol);
				wtr.Close();
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine("One of your files weas not found.");
			}
		}
	}
}
