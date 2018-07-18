using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_cart.BLL
{
    class Discount : CartLineItems
    {
        public static List<Discountdata> Discounts = null;
        public List<Discountdata> eligiblediscount()
        {
            List<Discountdata> applicablediscount = new List<Discountdata>();
            try
            {
                numberofdiscount();
                for (int i = 0; i < items.Count(); i++)
                {
                    for (int j = 0; j < Discounts.Count(); j++)
                    {
                        for (int k = 0; k < Discounts[j].applicableproductid.Count(); k++)
                        {
                            if (items[i].ItemId == Discounts[j].applicableproductid[k])
                            {
                                if (items[i].Quantity >= Discounts[j].buyx)
                                {
                                    if (applicablediscount.Where(li => li.id == Discounts[j].id).Count() == 0)
                                    {
                                        applicablediscount.Add(
new Discountdata
{
    applicableproductid = Discounts[j].applicableproductid,
    buyx = Discounts[j].buyx,
    gety = Discounts[j].gety,
    id = Discounts[j].id,
    name = Discounts[j].name,
    discountpercent = Discounts[j].discountpercent
}
);
                                    }
                                }
                                else if (Discounts[j].discountpercent != 0)
                                {
                                    applicablediscount.Add(
new Discountdata
{
    applicableproductid = Discounts[j].applicableproductid,
    buyx = Discounts[j].buyx,
    gety = Discounts[j].gety,
    id = Discounts[j].id,
    name = Discounts[j].name,
    discountpercent = Discounts[j].discountpercent
}
);
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in applicablediscount()," + ex.Message);
            }
            return applicablediscount;
        }

        public void numberofdiscount()
        {

            Discounts = new List<Discountdata> { 
                new Discountdata
            {
                id=1,
                name="Buygetoff",
                buyx=2,
                gety=1,
                applicableproductid=new int[] {1,2},
                discountpercent=0
       },
       new Discountdata
       {
           id=2,
           name="Percentage",
           discountpercent=20,
           applicableproductid=new int[]{3,4},
       }
       };

        }
    }

    class Discountdata
    {
        public int id { get; set; }
        public string name { get; set; }
        public int buyx { get; set; }
        public int gety { get; set; }
        public int[] applicableproductid { get; set; }
        public int discountpercent { get; set; }
    }
}
