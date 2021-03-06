using System;

namespace simQuickSort
{
    class simQuickSort
	{
		private static Random rnd = new Random();
		private static long[] S;
		private static long[] C;
		private static int numHits;


		private static void quicksort(long low, long high)
		{
			if (low < high)
			{
				long pivotpoint;
				partition(low, high, out pivotpoint);
				quicksort(low, pivotpoint - 1);
				quicksort(pivotpoint + 1, high);
			}
		}


		static void partition(long low, long high, out long pivotpoint)
		{
			long i, j;
			long pivotitem = S[low];
			j = low;

			for (i = low; i <= high; i++)
			{
				if (S[i] < pivotitem)
				{
					numHits++;
					j++;
					long temp1 = S[i];
					S[i] = S[j];
					S[j] = temp1;
				}
			}

			pivotpoint = j;
			long temp = S[low];
			S[low] = S[pivotpoint];
			S[pivotpoint] = temp;


		}



		static void Main(string[] args)
		{
			/*
			 Sets up an array holding the above values from 10 to 500000 as follows:
				int [ ] lim = { 10, 50, 100, 500, 1000, 5000, 10000, 25000, 50000, 75000, 100000, 200000, 300000, 400000, 500000};	
			 Also declares arrays C and S
			 */
			int[] lim = { 10, 50, 100, 500, 1000, 5000, 10000, 25000, 50000, 75000, 100000, 200000, 300000, 400000, 500000 };
			



			// Formats the title for the output
			Console.Write("SIMULATE QUICK SORT  -  PROJECT OUTPUT  -  COS 364\n\n\n\n");



			for (int i = 0; i < lim.Length; i++)
			{
				// Sets S to be the size of the current number in lim
				// Sets C to be of size 50
				S = new long[lim[i]];
				C = new long[50];
				numHits = 0;

				// Output text formatting
				Console.Write("ARRAY SIZE:  " + lim[i] + "\n---- ---- ---- ---- ---- ----\n");

				// Calls quicksort 50 times per item in lim.
				// Fills array S with new random ints per parse
				for (int j = 0; j < 50; j++)
				{
					// Fills array S using a random number generator, with numbers ranging from:
					// 0 to the current number in lim
					for (int k = 0; k < lim[i]; k++)
					{
						S[k] = rnd.Next(lim[i]);
					}

					quicksort(0, S.Length - 1);
					C[j] = numHits;
					numHits = 0;


					/*
					  	DEBUG - OUTPUTS SORTED S ARRAY
					 
						Console.WriteLine("");
						for (int k = 0; k < S.Length; k++)
						{
							if (k % 10 == 0) { Console.Write("\n\t"); }
							Console.Write(S[k] + " ");
						}
						Console.Write("\nSORTED ARRAY\n\t");
						quicksort(0, S.Length - 1);

						for (int k = 0; k < S.Length; k++)
						{
							if (k % 10 == 0) { Console.Write("\n\t"); }
							Console.Write(S[k] + " ");
						}

						Console.Write("\n\n\n");
					*/


				}



				// Output text formatting
				double average = 0;
				long n = lim[i];
				for (int j = 0; j < C.Length; j++) { average += C[j]; }
				average = average / 50;
				average = average / lim[i];
				Console.WriteLine("\tN: " + n);
				Console.WriteLine("\tAVERAGE NUMBER OF EXCHANGES: " + average);
				Console.WriteLine("\tPOINT: \t(" + n + ", " + average + ")\n\n");


			}

		}
	}
}

