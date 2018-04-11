using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatrixMultiplication.Models
{
    public class MatrixRandomModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int MatrixAWidth { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MatrixAHeight { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MatrixBWidth { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MatrixBHeight { get; set; }

        [Required]
        public double Scalar { get; set; }

        public string Matrix1 { get; set; }

        public string Matrix2 { get; set; }

        public string Result { get; set; }

        public TimeSpan? Duration { get; set; }
    }
}
