using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixMultiplication.Business
{
    public class Matrix
    {
        private static readonly Random RandomNumberGenerator = new Random();
        private readonly double[,] _matrix;

        public Matrix(int width, int height)
        {
            Width = width < 0 ? throw new ArgumentOutOfRangeException(nameof(width)) : width;
            Height = height < 0 ? throw new ArgumentOutOfRangeException(nameof(height)) : height;
            _matrix = new double[width, height];
        }

        public Matrix(double[,] matrix)
        {
            _matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
            Width = matrix.GetLength(0);
            Height = matrix.GetLength(1);
        }

        public int Width { get; }

        public int Height { get; }

        public double this[int x, int y]
            => x < 0 || x >= Width ? throw new ArgumentOutOfRangeException(nameof(x))
            : y < 0 || y >= Height ? throw new ArgumentOutOfRangeException(nameof(y))
            : _matrix[x, y];

        public static bool TryParse(string source, out Matrix value)
        {
            var stringRows = source.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            int rowCount = stringRows.Length;
            int colCount = 0;

            var matrixLines = new string[rowCount][];

            for (int y = 0; y < rowCount; y++)
            {
                string row = stringRows[y];
                var cells = row.Split(';').Select(e => e.Trim()).ToArray();
                matrixLines[y] = cells;
                colCount = cells.Length;
            }

            if (matrixLines.Any(l => l.Length != colCount))
            {
                value = default(Matrix);
                return false;
            }

            var valueStorage = new double[colCount, rowCount];

            for (int y = 0; y < rowCount; y++)
            {
                for (int x = 0; x < colCount; x++)
                {
                    if (!double.TryParse(matrixLines[y][x], out double r))
                    {
                        value = default(Matrix);
                        return false;
                    }

                    valueStorage[x, y] = r;
                }
            }

            value = new Matrix(valueStorage);
            return true;
        }

        public static Matrix Random(int width, int height, double scalar)
        {
            var valueStorage = new double[width, height];

            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    valueStorage[x,y] = RandomNumberGenerator.NextDouble() * scalar;
                }
            }

            return new Matrix(valueStorage);
        }

        public override string ToString() => ToString('\t');

        public virtual string ToString(char delimiter)
        {
            IEnumerable<double> GetRow(int row)
            {
                for (int x = 0; x < Width; x++)
                {
                    yield return _matrix[x, row];
                }
            }

            var sb = new StringBuilder();
            for (int y = 0; y < Height; y++)
            {
                sb.AppendLine(string.Join(delimiter, GetRow(y)));
            }

            return sb.ToString();
        }
    }
}
