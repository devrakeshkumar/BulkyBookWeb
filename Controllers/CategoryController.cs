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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            ValidateCategory(category);
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["Success"] = "Category Created.";
                return RedirectToAction("Index");
            }
            return View(category);

        }
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue || id == 0)
            {
                return NotFound();
            }
            var dbCategory = _db.Categories.FirstOrDefault(c => c.Id == id.Value);
            if (dbCategory == null)
            {
                return NotFound();
            }
            return View(dbCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            ValidateCategory(category);
            if (ModelState.IsValid)
            {
                var dbCategory = _db.Categories.FirstOrDefault(c => c.Id == category.Id);
                if (dbCategory == null)
                {
                    return NotFound();
                }
                _db.Categories.Attach(dbCategory);
                dbCategory.Name = category.Name;
                dbCategory.DisplayOrder = category.DisplayOrder;
                _db.SaveChanges();
                TempData["Success"] = "Category Updated.";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Remove(int? id)
        {
            if (!id.HasValue || id == 0)
            {
                return NotFound();
            }
            var dbCategory = _db.Categories.FirstOrDefault(c => c.Id == id.Value);
            if (dbCategory == null)
            {
                return NotFound();
            }
            return View(dbCategory);
        }

        [HttpPost]   
        [ValidateAntiForgeryToken]
        public IActionResult Remove(Category category)
        {
            var dbCategory = _db.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(dbCategory);
            _db.SaveChanges();
            TempData["Success"] = "Category Removed.";
            return RedirectToAction("Index");
        }
        #region Custom Validators
        private void ValidateCategory(Category category)
        {
            if (category.Name == category.DisplayOrder)
            {
                ModelState.AddModelError("displayorder", "Diaplay Order can not have same value as of Name.");
            }
        }
        #endregion
    }
}
