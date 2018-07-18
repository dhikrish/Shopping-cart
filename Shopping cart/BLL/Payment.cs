using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_cart.BLL
{
    class Payment
    {
        public decimal amountrecieved {get;set;}
        public decimal totalamount { get; set; }
        public decimal dueamount { get; set; }
        public string modeofpayment { get; set; }
    }
    class PaymentModes
    {
        public string modeofpayments { get; set; }
        public int id { get; set; }
    }
    class Paymentcompletion
    {
        static List<PaymentModes> list = null;
        public Payment buytheitems(string modeofpayment,decimal grandtotal,string amountrecieved)
        {
            int modeid = Convert.ToInt32(modeofpayment);
            decimal amountrecieve = Convert.ToDecimal(amountrecieved);
             Payment finalcalculation = new Payment();
            try
            {
                finalcalculation.amountrecieved = amountrecieve;
                finalcalculation.dueamount = amountrecieve - grandtotal;
                finalcalculation.modeofpayment = list.Where(p => p.id == modeid).FirstOrDefault().modeofpayments;
                finalcalculation.totalamount = grandtotal;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in payment.cs,buytheitems()," + ex.Message);
            }
            return finalcalculation;
        }
        public List<PaymentModes> listofmodes()
        {
            try
            {
                list = new List<PaymentModes>{
                    new PaymentModes{
                        id=1,
                        modeofpayments="Cash"
                    },
                    new PaymentModes{
                        id=2,
                        modeofpayments="Cheque"
                    }
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in payment.cs listofmodes()," + ex.Message);
            }
            return list;
        }
    }
}
