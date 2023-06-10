using ElectroCommerce.DataAccess.Repository.IRepository;
using ElectroCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ElectroCommerce.Models.ViewModels;
using ElectroCommerce.DataAccess.Repository;
using ElectroCommerce.Utility;
using Microsoft.AspNetCore.Authorization;

namespace ElectroCommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
                ),
                Product = new Product()
            };

            if(id == 0 || id == null)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u=> u.Id == id, includeProperties:"ProductImages");
                return View(productVM);
            }

            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, List<IFormFile> files)
        {
            if (int.TryParse(productVM.Product.Title, out _))
            {
                ModelState.AddModelError("name", "The Name cannot be a number");
            }
            if(ModelState.IsValid)
            {
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save();

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = @"images\products\product-" + productVM.Product.Id;
                        string finalPath = Path.Combine(wwwRootPath, productPath);

                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        ProductImage productImage = new()
                        {
                            ImageUrl = @"\" + productPath + @"\" + fileName,
                            ProductId = productVM.Product.Id,
                        };

                        if (productVM.Product.ProductImages == null)
                        {
                            productVM.Product.ProductImages = new List<ProductImage>();
                        }
                        productVM.Product.ProductImages.Add(productImage);
                        //_unitOfWork.ProductImage.Add(productImage);
                    }

                    _unitOfWork.Product.Update(productVM.Product);
                    _unitOfWork.Save();
                }
                if (productVM.Product.Id == 0) TempData["success"] = "Product created successfully";
                else TempData["success"] = "Product updated successfully";

                return RedirectToAction("Index", "Product");

            }
            return View();
        }


        public IActionResult DeleteImage(int imageId)
        {
            var imageForDelete = _unitOfWork.ProductImage.Get(u => u.Id == imageId);
            int productId = imageForDelete.ProductId;
            if (imageForDelete != null)
            {
                if (!string.IsNullOrEmpty(imageForDelete.ImageUrl))
                {
                    var oldImagePath =
                                    Path.Combine(_webHostEnvironment.WebRootPath, imageForDelete.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _unitOfWork.ProductImage.Remove(imageForDelete);
                _unitOfWork.Save();

                TempData["success"] = "Deleted Successful";
            }
            return RedirectToAction(nameof(Upsert), new { id = productId });
        }



        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> ProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = ProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Product product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }
            _unitOfWork.Product.Remove(product); _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion


    }
}
