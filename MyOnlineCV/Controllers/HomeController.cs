using Microsoft.AspNetCore.Mvc;
using MyOnlineCV.Models;

namespace MyOnlineCV.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Projects()
        {
            return View();
        }

        public IActionResult Photo()
        {
            return View();
        }


        // This action will handle both GET (load the page) and POST (form submission)
        [HttpGet]
        public IActionResult TaxCalculator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TaxCalculator(string income)
        {
            if (string.IsNullOrWhiteSpace(income) || !decimal.TryParse(income, out decimal incomeValue) || incomeValue < 0)
            {
                ViewBag.Error = "Please enter a valid, non-negative number.";
                return View();
            }

            var model = new TaxCalculatorModel { Income = incomeValue };
            var tax = CalculateTax(model); // Use the model for tax calculation
            ViewBag.Tax = tax.ToString("N2"); // Format the tax with commas and two decimal places

            return View();
        }

        private decimal CalculateTax(TaxCalculatorModel model)
        {
            // Simple tax calculation logic for Nigeria
            if (model.Income <= 300000)
            {
                return model.Income * 0.07m; // 7% tax for low-income
            }
            else if (model.Income <= 600000)
            {
                return model.Income * 0.11m; // 11% tax for middle-income
            }
            else
            {
                return model.Income * 0.24m; // 24% tax for high-income
            }
        }
    }
}
