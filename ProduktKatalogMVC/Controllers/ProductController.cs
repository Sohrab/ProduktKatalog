using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProduktKatalogMVC.Models;

namespace ProduktKatalogMVC.Controllers
{
    public class ProductController : Controller
    {

        

        private List<Product> localProductList;
        public List<Product> JsonRepository { 
            get
            {
                localProductList = GetJsonProducts();
                return localProductList;
            }
             
            set
            {
                localProductList = value;
                
            }
        }




        // GET: Product
        public ActionResult Index()
        {

            //WriteToJson();
            
            return View(JsonRepository);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            Product product = JsonRepository.Find(x => x.id == id);
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                UpdateModel(product, collection);
                
                //Check if product with similar id already exist in Json file
                if (JsonRepository.Find(x => x.id == product.id) == null)
                {
                    List<Product> productlist = JsonRepository;
                    productlist.Add(product);
                    SaveJsonProducts(productlist);
                }
                else
                {
                    ViewBag.ErrorMessage = "Id already exist, please select another";
                    return View();
                }

                return RedirectToAction("Index");
            }

            return View();    
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = JsonRepository.Find(x => x.id == id);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                //Remove existing product
                List<Product> productlist = JsonRepository;
                productlist.RemoveAll(x => x.id == id);
                //Get product info from form
                Product product = new Product();
                UpdateModel(product, collection);
                //Add the edited product as new
                productlist.Add(product);
                SaveJsonProducts(productlist);
                return RedirectToAction("Index");
            }

                return View();

        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {

            Product product = JsonRepository.Find(x => x.id == id);
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                List<Product> productlist = JsonRepository;
                productlist.RemoveAll(x => x.id == id);
                SaveJsonProducts(productlist);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        //
        //Json Methods below
        //

        private void SaveJsonProducts(List<Product> localProductList)
        {
            //Set json file info
            string filename = "products.json";
            string filePath = Server.MapPath("/Content/" + filename);

            
            string products = JsonConvert.SerializeObject(localProductList);

            System.IO.File.WriteAllText(filePath, products);
        }

        private List<Product> GetJsonProducts()
        {
            //Set json file info
            string filename = "products.json";
            string filePath = Server.MapPath("/Content/" + filename);

            string jsonString = System.IO.File.ReadAllText(filePath);

            List<Product> jsonProducts = JsonConvert.DeserializeObject<List<Product>>(jsonString);

            return jsonProducts;
        }

        private bool ProductAlreadyExist(int id)
        {
            //Set json file info
            string filename = "products.json";
            string filePath = Server.MapPath("/Content/" + filename);

            string jsonString = System.IO.File.ReadAllText(filePath);

            List<Product> jsonProducts = JsonConvert.DeserializeObject<List<Product>>(jsonString);


            foreach (var item in jsonProducts)
            {
                if (item.id == id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
