using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minesweeper_350.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper_350.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static BoardModel gameBoard;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            gameBoard = new BoardModel(1, 8);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
