using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestStore.Models;

namespace TestStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository _productRepository;
        private IImageRepository _imageRepository;
        public AdminController(IProductRepository repository, IImageRepository imageRepository)
        {
            _productRepository = repository;
            _imageRepository = imageRepository;
        }
        public IActionResult Index()
        {
            return View(_productRepository.Products);
        }
        public IActionResult Edit(int productId)
        {
            return View(_productRepository.Products.Where(p=>p.ProductId==productId).FirstOrDefault());
        }
        public IActionResult ImageEdit()
        {
            return View(_imageRepository.Images);
        }
        [HttpPost]
        public IActionResult ImageDelete(int imageId)
        {
            Image img = _imageRepository.Remove(imageId);
            if(img!= null)
            {
                TempData["message"] = $"{img.ImageId}: {img.ImageTitle} was deleted";
            }
            return RedirectToAction("ImageEdit");
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                 foreach (var file in Request.Form.Files)
                {
                    Image image = new Image();

                    if (file.FileName.EndsWith(".jpg") || file.FileName.EndsWith(".png") || file.FileName.EndsWith(".jpeg") || file.FileName.EndsWith(".webp"))
                    {
                        image.ImageTitle = file.FileName;

                        MemoryStream ms = new MemoryStream();
                        using (ms)
                        {
                            file.CopyTo(ms);
                            image.ImageData = ms.ToArray();
                        }
                        _imageRepository.Add(image);
                        
                        TempData["message"] = $"{image.ImageId}: {image.ImageTitle} was successfully uploaded";
                        product.Images.Add(image);
                    }
                    else
                    {
                        TempData["message"] = $"{image.ImageId}: {image.ImageTitle} cannot be uploaded (the file extension must be .png/.jpg)";
                        return View(product);
                    }
                }                        
                _productRepository.SaveProduct(product);
                TempData["message"] = $"{product.ProductId}: {product.ProductName} has been saved";
                return RedirectToAction("Index");

            }
            else
            {
                return View(product);
            }
        }

        public IActionResult Create()=>View("Edit",new Product());
        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product product = _productRepository.DeleteProduct(productId);
            if (product!=null)
            {
                TempData["message"] = $"{product.ProductId}: {product.ProductName} was deleted";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UploadImage(int productId)
        {
            Product dbEntry = _productRepository.Products.FirstOrDefault(x => x.ProductId == productId);
            foreach(var file in Request.Form.Files)
            {
                Image image = new Image();

                if (file.FileName.EndsWith(".jpg") || file.FileName.EndsWith(".png") && dbEntry != null)
                {


                    image.ImageTitle = file.FileName;
                    
                    MemoryStream ms = new MemoryStream();
                    using (ms) 
                    { 
                        file.CopyTo(ms);
                        image.ImageData = ms.ToArray();
                    }
                    _imageRepository.Add(image);
                    dbEntry.Images.Add(image);
                    _productRepository.SaveProduct(dbEntry);
                    TempData["message"] = $"{image.ImageId}: {image.ImageTitle} was successfully uploaded";
                }
                else
                {
                    TempData["message"] = $"{image.ImageId}: {image.ImageTitle} cannot be uploaded (the file extension must be .png/.jpg)";
                }
            }
                            

            return RedirectToAction("ImageEdit");
        }
    }
}
