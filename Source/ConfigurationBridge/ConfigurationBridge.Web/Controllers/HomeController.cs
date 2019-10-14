using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConfigurationBridge.Web.Models;
using ConfigurationBridge.Configuration.Core;

namespace ConfigurationBridge.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppSettingsResolved _settings;
        public HomeController(IAppSettingsResolved settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public IActionResult Index()
        {
            return View(_settings);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View(_settings);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View(_settings);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
