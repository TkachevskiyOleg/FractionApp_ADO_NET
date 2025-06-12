using Microsoft.AspNetCore.Mvc;
using _24._04._2024_Lab.Models;

namespace _24._04._2024_Lab.Controllers
{
    public class FractionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(Fraction f1, Fraction f2, string operation)
        {
            Fraction result = null;

            switch (operation)
            {
                case "Add":
                    result = f1.Add(f2);
                    break;
                case "Subtract":
                    result = f1.Subtract(f2);
                    break;
                case "Multiply":
                    result = f1.Multiply(f2);
                    break;
                case "Divide":
                    result = f1.Divide(f2);
                    break;
            }

            ViewBag.F1 = f1;
            ViewBag.F2 = f2;
            ViewBag.Operation = operation;
            ViewBag.Result = result;
            ViewBag.Decimal = result?.ToDecimal();

            return View("Index");
        }
    }
}
