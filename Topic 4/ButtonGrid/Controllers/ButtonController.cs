using ButtonGrid.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ButtonGrid.Controllers
{
    public class ButtonController : Controller
    {
        //create a list of buttons
        static List<ButtonModel> buttons = new List<ButtonModel>();
        Random random = new Random();
        const int GRID_SIZE = 25;
        public IActionResult Index()
        {
            buttons = new List<ButtonModel>();
          //  buttons.Add(new ButtonModel(0, 0));
          //  buttons.Add(new ButtonModel(1, 3));
          //  buttons.Add(new ButtonModel(2, 0));
          //  buttons.Add(new ButtonModel(3, 1));
          //  buttons.Add(new ButtonModel(4, 2));

            for (int i = 0; i < GRID_SIZE; i++)
            {
                buttons.Add(new ButtonModel(i, random.Next(4)));
            }

            return View("Index", buttons);
        }

        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bN = int.Parse(buttonNumber);

            buttons.ElementAt(bN).ButtonState = (buttons.ElementAt(bN).ButtonState + 1) % 4;

            return View("Index", buttons);
        }
        public IActionResult ShowOneButton(int buttonNumber)
        {
            buttons.ElementAt(buttonNumber).ButtonState = (buttons.ElementAt(buttonNumber).ButtonState + 1) % 4;

            return PartialView(buttons.ElementAt(buttonNumber));
        }
    }
}
