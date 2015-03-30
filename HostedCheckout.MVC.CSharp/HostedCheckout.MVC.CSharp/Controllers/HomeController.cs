using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HostedCheckout.MVC.CSharp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buy()
        {
            HCService.HCServiceSoapClient client = new HCService.HCServiceSoapClient();
            HCService.InitPaymentRequest req = new HCService.InitPaymentRequest();
            req.MerchantID = "013163015566916";
            req.Password = "ypBj@f@zt3fJRX,k";
            req.Invoice = "1234";
            req.TotalAmount = 1.23;
            req.TaxAmount = 0;
            req.TranType = "Sale";
            req.Frequency = "OneTime";
            req.Memo = "dano test";
            req.ProcessCompleteUrl = "http://localhost:51619/Home/Complete";
            req.ReturnUrl = "http://localhost:51619/Home/Return";
            req.OperatorID = "test";
            var resp = client.InitializePayment(req);

            ViewBag.URL = "https://hc.mercurydev.net/CheckoutIFrame.aspx?ReturnMethod=get&pid=" + resp.PaymentID;
            return View();
        }

        public ActionResult Complete(string PaymentID, string ReturnCode, string ReturnMessage)
        {
            HCService.HCServiceSoapClient client = new HCService.HCServiceSoapClient();
            HCService.PaymentInfoRequest req = new HCService.PaymentInfoRequest();
            req.MerchantID = "013163015566916";
            req.Password = "ypBj@f@zt3fJRX,k";
            req.PaymentID = PaymentID;
            var resp = client.VerifyPayment(req);

            return View();
        }
    }
}