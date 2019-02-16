using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {

        SQLRepository<ProductCategory> context;


        public ProductCategoryManagerController()
        { }
        public ProductCategoryManagerController(SQLRepository<ProductCategory> context)
        {
            this.context = context;
        }

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            if (context == null)
            {
                return View(new List<ProductCategory>());
            }
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);


    }

        public ActionResult Create()
        {
            if (context == null)
            {
                return View(new ProductCategory());
            }

            return View(context.Collection().ToList());
        }


        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                if (context == null)
                {
                    return View(productCategory);
                }
                context.Insert(productCategory);
                context.Commit();

                return RedirectToAction("Index");
            };
            
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }


        [HttpPost]
        public ActionResult Edit(ProductCategory product, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productCategoryToEdit.Category = product.Category;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        
       
    }
}