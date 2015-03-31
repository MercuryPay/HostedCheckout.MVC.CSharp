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

        public ActionResult CSS ()
        {
            HCService.CssUploadRequest request = new HCService.CssUploadRequest();
            request.MerchantID = "013163015566916";
            request.Password = "KR568r@1spyC13,T";
            request.Css = ".divTotalDefault{color: #000000;background-color: red;}";

            //Call the web service to initialize the UploadCSS request.
            HCService.HCServiceSoapClient client = new HCService.HCServiceSoapClient();
            HCService.CssAdminResponse response = new HCService.CssAdminResponse();
                response = client.UploadCSS(request);
                if (response != null)
                {
                    var blah = response.ResponseCode.ToString();
                    var blah2 = HttpUtility.HtmlDecode(response.Message);
                }

                else
                {
                    //something seriously wrong. probably couldn't connect to the web service at all
                    var blah3 = "Response from UploadCss was Null";

                }
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