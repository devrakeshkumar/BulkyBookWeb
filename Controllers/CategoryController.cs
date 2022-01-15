using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Retruns the Index page for categories
        /// </summary>
        /// <returns>View(objCategoryList)</returns>
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        /// <summary>
        /// Retrun the view to create the categories
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        
        /// <summary>
        /// Creates the category if modal is valid or returns the view with errors
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
              return  RedirectToAction("Index");
            }
            return View(category);

        }
    }
}
