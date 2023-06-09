using ElectroCommerce.DataAccess.Repository.IRepository;
using ElectroCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectroCommerceApp.Area.Admin
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (int.TryParse(category.Name, out _))
            {
                ModelState.AddModelError("name", "The Name cannot be a number");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if(id == 0 || id == null) return NotFound();
            
            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if(category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (int.TryParse(category.Name, out _))
            {
                ModelState.AddModelError("name", "The Name cannot be a number");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if( id==0 || id==null) return NotFound();
            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == 0 || id == null) return NotFound();
            Category? category = _unitOfWork.Category.Get(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
