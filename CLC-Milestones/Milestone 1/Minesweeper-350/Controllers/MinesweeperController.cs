using Microsoft.AspNetCore.Mvc;

namespace Minesweeper_350.Controllers
{
    public class MinesweeperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
