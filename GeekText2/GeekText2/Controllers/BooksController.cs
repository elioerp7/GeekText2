using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeekText2.Data;

namespace GeekText2.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            ApplicationDbContext context = HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            return View(context.GetAllBooks());
        }
    }
}