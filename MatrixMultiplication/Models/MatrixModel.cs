using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatrixMultiplication.Models
{
    public class MatrixModel
    {
        [Required]
        public string MatrixA { get; set; }

        [Required]
        public string MatrixB { get; set; }

        public string Result { get; set; }

        public TimeSpan? Duration { get; set; }
    }
}
