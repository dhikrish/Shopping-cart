using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping_cart.BLL;
using Shopping_cart.DAL;

namespace Shopping_cart
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Variable declaration
            string idstring = string.Empty;
            int id=0;
            int numberofitems=0;
            string userinput = string.Empty;
            string quantitystring = string.Empty;
            int quantity = 0;
            bool cartstatus = false;
            int discountcount = 0;
            int discountincremwnrt=1;
            string couponcode=string.Empty;
            string discontinput = string.Empty;
            List<string> coupencodearray = new List<string>();
            string modeofpayment = string.Empty;
            decimal grandtotal = 0;
            #endregion
            try
            {
                Console.WriteLine("Welcome to Shopping Cart\n");
                #region To Display lsit of items
                display displayitems = new display();
                numberofitems = displayitems.displayproduct();
                #endregion

                #region To Add item to the cart
                CartLineItems additem = new CartLineItems();
                while (true)
                {
                    Console.WriteLine("Please enter the id of the product to be added to the cart");
                    idstring = Console.ReadLine();
                    id=Convert.ToInt32(idstring);
                    if (id <= numberofitems)
                    {
                        Console.WriteLine("Enter the Quantity");
                        quantitystring = Console.ReadLine();
                        quantity = Convert.ToInt32(quantitystring);
                        cartstatus = additem.additems(id, quantity);
                        if (cartstatus)
                        {
                            Console.WriteLine("Item Added to Cart Successfully.Do you want to add more items to the cart? Y/N");
                        }
                        else
                        {
                            Console.WriteLine("Please try adding again");
                        }
                        userinput = Console.ReadLine();
                        if (userinput == "y" || userinput == "Y")
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Item Id does not exist. Please enter the valid id ");
                    }
                    
                }

                #endregion

                #region To Display the items in the cart and applying discount
                displayitems.displaycartlineitems();
                discountcount= displayitems.displaydiscount();
                if (discountcount > 0)
                {
                    Console.WriteLine("Please enter the coupon code of the discount you need to apply");
                    couponcode = Console.ReadLine();
                    coupencodearray.Add(couponcode);
                    displayitems.updatecart(couponcode);
                    while (discountcount > discountincremwnrt)
                    {
                        Console.WriteLine("Do you want to add another Code? If Yes enter the Offer Code else press N");
                        couponcode = Console.ReadLine();
                        if (couponcode == "n" || couponcode == "N")
                        {
                           grandtotal= displayitems.displayfinalcart();  
                        }
                        else if (coupencodearray!=null && coupencodearray.Contains(couponcode))
                        {
                            Console.WriteLine("Coupon already applied");
                        }
                        else
                        {
                            displayitems.updatecart(couponcode);
                        }
                        discountincremwnrt++;
                    }
                }
                grandtotal = displayitems.displayfinalcart();
                #endregion

                #region Payment Section
                Console.WriteLine("Please select the mode of payment");
                displayitems.displaymodeofpayments();
                modeofpayment = Console.ReadLine();
                displayitems.finalpage(modeofpayment, grandtotal);
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in program.cs,"+ex.Message);
            }


        }
    }


}
