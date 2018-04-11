using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MatrixMultiplication.Business;
using Microsoft.AspNetCore.Mvc;
using MatrixMultiplication.Models;

namespace MatrixMultiplication.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new MatrixModel());
        }

        [HttpGet]
        public IActionResult IndexSerial()
        {
            return View(new MatrixModel());
        }

        [HttpGet]
        public IActionResult Random()
        {
            return View(new MatrixRandomModel());
        }

        [HttpPost]
        public IActionResult CalculateRandom(MatrixRandomModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Random", model);
            }

            var matrixA = Matrix.Random(model.MatrixAWidth, model.MatrixAHeight, model.Scalar);
            var matrixB = Matrix.Random(model.MatrixBWidth, model.MatrixBHeight, model.Scalar);
            model.Matrix1 = matrixA.ToString();
            model.Matrix2 = matrixB.ToString();

            var sw = Stopwatch.StartNew();
            var result = MatrixCalculator.MultiplyParallel(matrixA, matrixB);
            sw.Stop();

            var mdl = new MatrixModel
            {
                MatrixA = matrixA.ToString(';'),
                MatrixB = matrixB.ToString(';'),
                Result = result.ToString(),
                Duration = sw.Elapsed
            };

            return View("Index", mdl);
        }

        [HttpPost]
        public IActionResult Calculate(MatrixModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            if (!Matrix.TryParse(model.MatrixA, out var matrixA)
                || !Matrix.TryParse(model.MatrixB, out var matrixB))
            {
                return View("Index", model);
            }

            var sw = Stopwatch.StartNew();
            var result = MatrixCalculator.MultiplyParallel(matrixA, matrixB);
            sw.Stop();

            model.Result = result.ToString();
            model.Duration = sw.Elapsed;

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult CalculateSerial(MatrixModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("IndexSerial", model);
            }

            if (!Matrix.TryParse(model.MatrixA, out var matrixA)
                || !Matrix.TryParse(model.MatrixB, out var matrixB))
            {
                return View("IndexSerial", model);
            }

            var sw = Stopwatch.StartNew();
            var result = MatrixCalculator.Multiply(matrixA, matrixB);
            sw.Stop();

            model.Result = result.ToString();
            model.Duration = sw.Elapsed;

            return View("IndexSerial", model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
