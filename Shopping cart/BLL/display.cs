using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping_cart.DAL;
using System.Diagnostics;

namespace Shopping_cart.BLL
{
    class display : CartLineItems
    {
        public int displayproduct()
        {
            int count = 0;
            try
            {

                ProductList p = new ProductList();
                List<Product> list = p.products();
                count = list.Count();
                Console.WriteLine(list.ToStringTable(
    new[] { "Id", "Item Name", "Price" },
    a => a.Id, a => a.Name, a => a.Price));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in displayproducts,display()," + ex.Message);
            }
            return count;
        }

        public void displaycartlineitems()
        {

            Console.WriteLine(items.ToStringTable(
new[] { "Sno", "Item Name", "Item Id", "Quantity", "Price", "Total Price" },
a => a.SreialNumber, a => a.ItemName, a => a.ItemId, a => a.Quantity, a => a.Price, a => a.TotalPrice));

        }

        public int displaydiscount()
        {
            int count = 0;
            try
            {
                Discount discountview = new Discount();
                List<Discountdata> discountdata = discountview.eligiblediscount();
                count = discountdata.Count();
                foreach (Discountdata discount in discountdata)
                {
                    Console.WriteLine(discount.discountpercent != 0 ? "Get " + discount.discountpercent + "% Off. Use Code: "+discount.id+"." : discount.buyx != 0 ? "Buy " + discount.buyx + "and Get " + discount.gety + " Free. Use Code: "+discount.id+"." : "You dont have any offers");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errorin display,displaydiscount()," + ex.Message);
            }
            return count;
        }

        public void updatecart(string idstring)
        {
            int couponid = 0;
            try
            {
                couponid = Convert.ToInt32(idstring);
                updateitemwithdiscount(couponid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in display,updatecart()," + ex.Message);
            }
        }

        public decimal displayfinalcart()
        {
            decimal grandtotal = 0;
            foreach (CartDetails item in items)
            {
                grandtotal += item.TotalPrice;
            }
            Console.WriteLine(items.ToStringTable(
new[] { "Sno", "Item Name", "Item Id", "Quantity", "Price", "Total Price" },
a => a.SreialNumber, a => a.ItemName, a => a.ItemId, a => a.Quantity, a => a.Price, a => a.TotalPrice));
            Console.WriteLine("                                     Grand Total: " + grandtotal);
            return grandtotal;
        }

        public void displaymodeofpayments()
        {
            Paymentcompletion pay = new Paymentcompletion();
            List<PaymentModes> modes=pay.listofmodes();
            Console.WriteLine(modes.ToStringTable(
new[] { "Id", "Mode of payment" },
a => a.id, a => a.modeofpayments));
        }

        public void finalpage(string modeofpayment,decimal grandtotal)
        {
            string amountstring = string.Empty;
            if (modeofpayment == "1")
            {
                Console.WriteLine("Please enter the total amount recieved");
                amountstring = Console.ReadLine();
            }
            else
            {
                amountstring = grandtotal.ToString();
            }
                Paymentcompletion pay = new Paymentcompletion();
                Payment finallist = pay.buytheitems(modeofpayment, grandtotal, amountstring);
                Console.WriteLine("Mode of Payment: {0}   Grand Total: {1}  Amount Recieved: {2} Due Amount: {3}\n Thank You for Shopping with us!!!", finallist.modeofpayment, finallist.totalamount, finallist.amountrecieved, finallist.dueamount);
             
        }
    }

    public static class TableParser
    {
        public static string ToStringTable<T>(
          this IEnumerable<T> values,
          string[] columnHeaders,
          params Func<T, object>[] valueSelectors)
        {
            return ToStringTable(values.ToArray(), columnHeaders, valueSelectors);
        }

        public static string ToStringTable<T>(
          this T[] values,
          string[] columnHeaders,
          params Func<T, object>[] valueSelectors)
        {
            Debug.Assert(columnHeaders.Length == valueSelectors.Length);

            var arrValues = new string[values.Length + 1, valueSelectors.Length];

            // Fill headers
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                arrValues[0, colIndex] = columnHeaders[colIndex];
            }

            // Fill table rows
            for (int rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    arrValues[rowIndex, colIndex] = valueSelectors[colIndex]
                      .Invoke(values[rowIndex - 1]).ToString();
                }
            }

            return ToStringTable(arrValues);
        }

        public static string ToStringTable(this string[,] arrValues)
        {
            int[] maxColumnsWidth = GetMaxColumnsWidth(arrValues);
            var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

            var sb = new StringBuilder();
            for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    // Print cell
                    string cell = arrValues[rowIndex, colIndex];
                    cell = cell.PadRight(maxColumnsWidth[colIndex]);
                    sb.Append(" | ");
                    sb.Append(cell);
                }

                // Print end of line
                sb.Append(" | ");
                sb.AppendLine();

                // Print splitter
                if (rowIndex == 0)
                {
                    sb.AppendFormat(" |{0}| ", headerSpliter);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private static int[] GetMaxColumnsWidth(string[,] arrValues)
        {
            var maxColumnsWidth = new int[arrValues.GetLength(1)];
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
                {
                    int newLength = arrValues[rowIndex, colIndex].Length;
                    int oldLength = maxColumnsWidth[colIndex];

                    if (newLength > oldLength)
                    {
                        maxColumnsWidth[colIndex] = newLength;
                    }
                }
            }

            return maxColumnsWidth;
        }
    }
}
