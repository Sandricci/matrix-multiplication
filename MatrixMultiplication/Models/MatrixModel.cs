using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatrixMultiplication.Models
{
    public class MatrixModel
    {
        public double[][] MatrixA { get; set; }
        public double[][] MatrixB { get; set; }

        public static double[][] CreateEmptyMatrix(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        public static double[][] ParseMatrix(double[][] matrix)
        {
            double[][] result = CreateEmptyMatrix(matrix.Length, matrix[0].Length);
            Random r = new Random();
            int rInt;

            // TODO:
            // parse input to double[][]
            // get rows and cols based on user input separated by ;
            // detailed error handling if cols matrixA != rows matrixB
            // measure execution time

            for (int i = 0; i < result.Length; ++i)
            {
                for (int j = 0; j < result[0].Length; ++j)
                {
                    rInt = r.Next(0, 100); // TODO: random value for now
                    result[i][j] += rInt;
                }
            }
            return result;
        }

        public static double[][] MultiplyMatrix(double[][] matrixA, double[][] matrixB)
        {
            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bRows = matrixB.Length;
            int bCols = matrixB[0].Length;

            if (aCols != bRows)
                throw new Exception("Matrices don't match!");

            double[][] result = CreateEmptyMatrix(aRows, bCols);

            Parallel.For(0, aRows, i =>
            {
                for (int j = 0; j < bCols; ++j)
                    for (int k = 0; k < aCols; ++k)
                        result[i][j] += matrixA[i][k] * matrixB[k][j];
            }
            );

            return result;
        }

        public static string PrintMatrix(double[][] matrix)
        {
            string result = "Result:";

            for (int i = 0; i < matrix.Length; ++i)
            {
                result += "\n";

                for (int j = 0; j < matrix[0].Length; ++j)
                {
                    result += " " + matrix[i][j];
                }
            }

            return result;
        }
    }
}
