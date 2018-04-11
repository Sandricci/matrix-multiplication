using System;
using System.Threading.Tasks;

namespace MatrixMultiplication.Business
{
    public static class MatrixCalculator
    {
        public static Matrix Multiply(Matrix m1, Matrix m2)
        {
            if (m1 is null)
            {
                throw new ArgumentNullException(nameof(m1));
            }

            if (m2 is null)
            {
                throw new ArgumentNullException(nameof(m2));
            }

            if (m1.Width != m2.Height)
            {
                throw new ArgumentException(
                    "The matrix dimensions have to agree in that the width of the first one has to match the height of the second one.");
            }

            var valueStorage = new double[m1.Height, m2.Width];
            for (int i = 0; i < m1.Height; i++)
            {
                for (int j = 0; j < m2.Width; j++)
                {
                    for (int k = 0; k < m1.Width; k++)
                    {
                        valueStorage[i, j] += m1[k, i] * m2[j, k];
                    }
                }
            }

            return new Matrix(valueStorage);
        }

        public static Matrix MultiplyParallel(Matrix m1, Matrix m2)
        {
            if (m1 is null)
            {
                throw new ArgumentNullException(nameof(m1));
            }

            if (m2 is null)
            {
                throw new ArgumentNullException(nameof(m2));
            }

            if (m1.Width != m2.Height)
            {
                throw new ArgumentException(
                    "The matrix dimensions have to agree in that the width of the first one has to match the height of the second one.");
            }

            double Multiply(int i, int j, int m1Width)
            {
                double r = 0d;
                for (int k = 0; k < m1Width; k++)
                {
                    r += m1[k, i] * m2[j, k];
                }

                return r;
            }

            var valueStorage = new double[m1.Height, m2.Width];
            Parallel.For(
                0,
                m1.Height,
                i =>
                {
                    for (int j = 0; j < m2.Width; j++)
                    {
                        valueStorage[i, j] = Multiply(i, j, m1.Width);
                    }
                });

            return new Matrix(valueStorage);
        }
    }
}
