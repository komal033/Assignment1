using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment.DAL;

namespace Assignment.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDAL productRepository = new ProductDAL();

        // Display all products
        public ActionResult Index()
        {
            var products = productRepository.GetAllProducts();
            return View(products);
        }

        // Display details of a specific product
        public ActionResult Details(int id)
        {
            var product = productRepository.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // Show the form to create a new product
        public ActionResult Create()
        {
            return View();
        }

        // Handle the form submission to create a new product
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Show the form to edit an existing product
        public ActionResult Edit(int id)
        {
            var product = productRepository.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // Handle the form submission to edit an existing product
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Handle deleting a product
        public ActionResult Delete(int id)
        {
            var product = productRepository.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // Confirm deletion of the product
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            productRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}