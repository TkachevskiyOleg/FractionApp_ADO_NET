using Microsoft.AspNetCore.Mvc;
using _24._04._2024_Lab.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _24._04._2024_Lab.Controllers
{
    public class FractionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Обробка операцій з двома дробами (перше завдання)
        [HttpPost]
        public IActionResult Calculate(Fraction f1, Fraction f2, string operation)
        {
            Fraction result = null;

            try
            {
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
                ViewBag.Decimal = result.ToDecimal();
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View("Index");
        }

        // Додатковий GET-метод для сторінки завантаження файлу
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        // POST-метод для завантаження файлу з дробами (друге завдання)
        [HttpPost]
        public async Task<IActionResult> UploadFile(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Error = "Будь ласка, виберіть файл для завантаження.";
                return View("Upload");
            }

            List<Fraction> fractions = new List<Fraction>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var lines = new List<string>();

                while (!reader.EndOfStream)
                {
                    lines.Add(await reader.ReadLineAsync());
                }

                // Знайдемо індекси рядків, які складаються з однакових символів (наприклад +++ або ^^^)
                // Вони будуть роздільниками блоків дробів
                var separatorIndices = new List<int>();
                for (int i = 0; i < lines.Count; i++)
                {
                    string line = lines[i].Trim();
                    if (!string.IsNullOrEmpty(line) && line.Distinct().Count() == 1) // всі символи однакові
                    {
                        separatorIndices.Add(i);
                    }
                }

                // Додамо в кінець ліній індекс для полегшення ітерування
                separatorIndices.Add(lines.Count);

                int start = 0;
                foreach (var sepIndex in separatorIndices)
                {
                    var blockLines = lines.Skip(start).Take(sepIndex - start).Where(l => !string.IsNullOrWhiteSpace(l));
                    foreach (var line in blockLines)
                    {
                        try
                        {
                            fractions.Add(Fraction.Parse(line));
                        }
                        catch
                        {
                            // пропускаємо рядки, які не є дробами у форматі "чисельник/знаменник"
                        }
                    }
                    start = sepIndex + 1;
                }
            }

            if (fractions.Count == 0)
            {
                ViewBag.Error = "У файлі не знайдено дробів у форматі 'чисельник/знаменник'.";
                return View("Upload");
            }

            // Наприклад, тут обчислимо суму усіх дробів із файлу
            Fraction sum = fractions[0];
            for (int i = 1; i < fractions.Count; i++)
            {
                sum = sum.Add(fractions[i]);
            }

            ViewBag.Fractions = fractions;
            ViewBag.Sum = sum;
            ViewBag.SumDecimal = sum.ToDecimal();

            return View("UploadResult");
        }
    }
}
