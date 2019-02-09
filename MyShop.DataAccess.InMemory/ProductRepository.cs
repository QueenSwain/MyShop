using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
   public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        //wher retrieving the list of product from DB.it hits to the actionresult and then returns the index view.
        //Using Cache it will get the the DB once and index view will get the data from Cache many times
        List<Product> products;



        public ProductRepository()
        {
            products = cache["products"] as List<Product>; //storing the cache product list in a products variables
            if(products==null)
            {
                products = new List<Product>(); //If any product is there then creating list object.
            }
        }



        public void Commit() 
            {
            cache["products"] = products; //keeping in cache prod list
            }


        public void Insert(Product p) 
        {
            products.Add(p);
        }


        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if(productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not updated!");
            }
        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id ==Id);

            if (product == null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
        public IQueryable<Product> Collection() 
        //While querying data from a database, IQueryable executes a "select query" on server-side with all filters.
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not deleted");
            }
        }
    }
}
