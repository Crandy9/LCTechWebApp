﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCWebApp.Controllers
{
    public class WebGLController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
