using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping_cart.DAL;

namespace Shopping_cart.BLL
{
    class CartLineItems 
    {
       protected static List<CartDetails> items = new List<CartDetails>();
        int itemnumber = 1;
        public bool additems(int id, int quantity)
        {
            #region Variable declaration
            bool status = false;
            string itemname = string.Empty;
            decimal price = 0;
            #endregion

            try
            {
                ProductList productlist = new ProductList();
                List<Product> list = productlist.products();
                var itemdetails = list.Where(l => l.Id == id);
                itemname = itemdetails.First().Name;
                price = itemdetails.First().Price;
                items.Add(
                    new CartDetails
                    {
                        SreialNumber = itemnumber,
                        ItemName = itemname,
                        ItemId=id,
                        Quantity = quantity,
                        Price = price,
                        TotalPrice = price * quantity
                    }
                );
                status = true;
            }
            catch (Exception ex)
            {

            }
            itemnumber++;
            return status;

        }

        public void updateitemwithdiscount(int couponid)
        {
            for (int i = 0; i < Discount.Discounts.Count; i++)
            {
                if (couponid == Discount.Discounts[i].id)
                {
                    for (int j = 0; j < Discount.Discounts[i].applicableproductid.Count(); j++)
                    {
                        for (int k = 0; k < items.Count(); k++)
                        {
                            if (items[k].ItemId == Discount.Discounts[i].applicableproductid[j])
                            {
                                if (Discount.Discounts[i].buyx != 0)
                                {
                                    items[k].Quantity += Discount.Discounts[i].gety;
                                }
                                else if (Discount.Discounts[i].discountpercent != 0)
                                {
                                    items[k].TotalPrice =items[k].TotalPrice - ((items[k].TotalPrice *(Discount.Discounts[i].discountpercent) / 100));
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    class CartDetails
    {
        public int SreialNumber { get; set; }
        public string ItemName { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
