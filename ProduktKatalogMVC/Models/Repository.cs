using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ProduktKatalogMVC.Models
{
    public class Repository
    {

        List<Product> productList = new List<Product>();

        public List<Product> ProductList { 
            get
            {
                return productList;
            }
            set
            {
                productList = value;
            } 
        
        }

        public Repository()
        {

            ProductList = GetProducts();
            

        }


        private List<Product> GetProducts()
        {
            return new List<Product>{ new Product(){ id = 1, Name ="DammSugare", Price = 54m, Description = "suger damm!"},
                new Product() { id = 2, Name ="Kikare", Price = 32m, Description="ser långt"}};
        }
    }
}