using Assignment.DAL;
using Assignment.Models;
using System.Web.Mvc;
using Assignment.Models;
using Assignment.ProductDAL;

public class ProductController : Controller
{
    private readonly ProductDAL _repository = new ProductDAL();

    public ActionResult Index(int pageNumber = 1, int pageSize = 10)
    {
        // Fetch paginated products
        var products = _repository.GetProducts(pageNumber, pageSize);

        // Store page info in ViewBag for pagination
        ViewBag.PageNumber = pageNumber;
        ViewBag.PageSize = pageSize;

        return View(products);
    }


    public ActionResult Details(int id)
    {
        var product = _repository.GetProductById(id);
        if (product == null)
        {
            return HttpNotFound();
        }
        return View(product);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _repository.AddProduct(product);
            return RedirectToAction("Index");
        }
        return View(product);
    }

    public ActionResult Edit(int id)
    {
        var product = _repository.GetProductById(id);
        if (product == null)
        {
            return HttpNotFound();
        }
        return View(product);
    }

    [HttpPost]
    public ActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _repository.UpdateProduct(product);
            return RedirectToAction("Index");
        }
        return View(product);
    }

    public ActionResult Delete(int id)
    {
        var product = _repository.GetProductById(id);
        if (product == null)
        {
            return HttpNotFound();
        }
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        _repository.DeleteProduct(id);
        return RedirectToAction("Index");
    }
}
