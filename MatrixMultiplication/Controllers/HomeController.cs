using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MatrixMultiplication.Models;

namespace MatrixMultiplication.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MatrixModel model)
        {
            // get rows and cols from user input instead of init here
            double[][] ma = MatrixModel.CreateEmptyMatrix(10, 20);
            double[][] mb = MatrixModel.CreateEmptyMatrix(20, 10);
            ma = MatrixModel.ParseMatrix(ma);
            mb = MatrixModel.ParseMatrix(mb);

            double[][] matrixR = MatrixModel.MultiplyMatrix(ma, mb);
            string result = MatrixModel.PrintMatrix(matrixR);

            return Content($"{result}");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
