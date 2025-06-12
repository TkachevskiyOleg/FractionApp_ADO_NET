using Microsoft.AspNetCore.Mvc;
using _24._04._2024_Lab.Models;
using System.Text;

namespace _24._04._2024_Lab.Controllers
{
    public class CarController : Controller
    {
        private static List<Car> cars = new();

        public IActionResult Index()
        {
            return View(cars);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Please select a file.");
                return View("Upload");
            }

            var lines = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(await reader.ReadLineAsync());
                }
            }

            
            var groups = SplitIntoGroups(lines);

            cars.Clear();

            foreach (var group in groups)
            {
                foreach (var line in group)
                {
                    try
                    {
                        var car = Car.FromFileString(line);
                        cars.Add(car);
                    }
                    catch
                    {
                        
                    }
                }
            }

            return RedirectToAction("Index");
        }

        private List<List<string>> SplitIntoGroups(List<string> lines)
        {
            var groups = new List<List<string>>();
            var currentGroup = new List<string>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentGroup.Count > 0)
                    {
                        groups.Add(new List<string>(currentGroup));
                        currentGroup.Clear();
                    }
                }
                else
                {
                    currentGroup.Add(line.Trim());
                }
            }

            if (currentGroup.Count > 0)
                groups.Add(currentGroup);

            return groups;
        }
    }
}
