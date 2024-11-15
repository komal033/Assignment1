using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment.DAL;

namespace Assignment.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryDAL _repository = new CategoryDAL();

        public ActionResult Index()
        {
            var categories = _repository.GetAllCategories();
            return View(categories);
        }

        public ActionResult Details(int id)
        {
            var category = _repository.GetCategoryById(id);
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _repository.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Edit(int id)
        {
            var category = _repository.GetCategoryById(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int id)
        {
            var category = _repository.GetCategoryById(id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }

}