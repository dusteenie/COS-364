using System;

namespace Stepak___COS_364_Project_SimulateQuickSort
{
    class SimulateQuickSort
    {
		private static Random rnd = new Random();
		private static long[] S;


		static void quicksort(int low, int high)
		{

			if (low < high) 
			{
				int pivotpoint = partition(low, high);
				quicksort(low, pivotpoint - 1);
				quicksort(pivotpoint + 1, high);
			}
		}

		static int partition(int low, int high)
		{
			int i, j, pivotpoint;
			long pivotitem = S[low];
			j = low;

			for(i = low; i<=high; i++) 
			{
				if(S[i] < pivotitem) 
				{
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

			return pivotpoint;
		}

		

		static void Main(string[] args)
        {
			/*
			 Sets up an array holding the above values from 10 to 500000 as follows:
				int [ ] lim = { 10, 50, 100, 500, 1000, 5000, 10000, 25000, 50000, 75000, 100000, 200000, 300000, 400000, 500000};	
			 Also declares arrays C and S
			 */
			int[] lim = { 10, 50, 100, 500, 1000, 5000, 10000, 25000, 50000, 75000, 100000, 200000, 300000, 400000, 500000 };
			long[] C;



			// Formats the title for the output
			Console.Write("SIMULATE QUICK SORT  -  PROJECT  -  COS 364\n" +
				"---- ---- ---- ---- ---- ---- \n\n\n");



			for (int i = 0; i < 1/*lim.Length*/; i++) 
			{
				// Sets S to be the size of the current number in lim
				// Sets C to be of size 50
				S = new long[lim[i]];
				C = new long[50];

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

					// Sets C at j to the number of runs it took the quicksort algorithem 
					// to sort array S


					// debug
					for (int k = 0; k < S.Length; k++)
					{
						if (k % 10 == 0) { Console.Write("\n\t"); }
						Console.Write(S[k] + " ");
					}
					Console.Write("\nSORTED\n\t");
					quicksort(0, S.Length - 1);

					// debug
					for (int k = 0; k < S.Length; k++)
					{
						if (k % 10 == 0) { Console.Write("\n\t"); }
						Console.Write(S[k] + " ");
					}

					Console.Write("\n\n\n");
					//for (int k = 0; k < S.Length; k++)
					//{
					//if (k % 10 == 0) { Console.Write("\n\t"); }
					//Console.Write(S[k] + " ");
					//}

					//C[j] = quicksort(S);


				}


				
					// Output text formatting
					Console.Write("\tNUMBER OF RUNS FROM  1-50:\n---- ---- ---- ---- ---- ----\n\t");
					for (int j = 0; j < C.Length; j++) 
					{
						if (j % 10 == 0) { Console.Write("\n\t"); }
						Console.Write(C[j] + " ");
					}
					Console.WriteLine("\n\n");

				
			}

		}
    }
}
