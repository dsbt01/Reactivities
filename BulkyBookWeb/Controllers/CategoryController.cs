using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategory = _db.GetAll().OrderBy(r => r.DisplayOrder);

            return View(objCategory);
        }

        //Get
        public IActionResult Create()
        {

            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order and Name must be different.");
            }

            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save();

                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _db.GetFirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order and Name must be different.");
            }

            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();

                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _db.GetFirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Remove(obj);
            _db.Save();

            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
