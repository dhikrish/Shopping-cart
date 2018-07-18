using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_cart.DAL
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    class ProductList
    {
       List<Product> Products = null;

        #region Adding Items to the List
       public List<Product> products()
        {
            try
            {
                Products = new List<Product> { 
             new Product
             {
                 Id=01,
                 Name="Shirt",
                 Price=100

             },
            new Product
            {
                Id=02,
                Name="Trouser",
                Price=1500
            },
            new Product
            {
                Id=03,
                Name="T-Shirt",
                Price=300
            },
            new Product
            {
                Id=04,
                Name="Jeans",
                Price=900
            }
              };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Inventory.cs,products()," + ex.Message);
            }
            return Products;
        }
        #endregion


    }
}
