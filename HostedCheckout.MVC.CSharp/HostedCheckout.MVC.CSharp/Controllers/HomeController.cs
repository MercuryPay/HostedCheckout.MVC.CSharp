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
            req.MerchantID = "912127036979183";
            req.Password = "e%MH0HJs3$3wk+ob";
            req.Invoice = "1234";
            req.TotalAmount = 1.23;
            req.TaxAmount = 0;
            req.TranType = "Sale";
            req.Frequency = "OneTime";
            req.Memo = "dano test";
            req.ProcessCompleteUrl = "http://localhost:51619/Home/Complete";
            req.ReturnUrl = "http://localhost:51619/Home/Return";
            req.OperatorID = "test";
            req.DisplayStyle = "custom";
            req.CancelButton = "on";
            var resp = client.InitializePayment(req);

            ViewBag.URL = "https://hc.mercurydev.net/CheckoutIFrame.aspx?ReturnMethod=get&pid=" + resp.PaymentID;
            return View();
        }

        public ActionResult Complete(string PaymentID, string ReturnCode, string ReturnMessage)
        {
            HCService.HCServiceSoapClient client = new HCService.HCServiceSoapClient();
            HCService.PaymentInfoRequest req = new HCService.PaymentInfoRequest();
            req.MerchantID = "912127036979183";
            req.Password = "e%MH0HJs3$3wk+ob";
            req.PaymentID = PaymentID;
            var resp = client.VerifyPayment(req);

            return View(resp);
        }

        public ActionResult RemoveCSS()
        {
            var request = new HCService.CssRemoveRequest();
            request.MerchantID = "018847445761734";
            request.Password = "Y6@Mepyn!r0LsMNq";

            var client = new HCService.HCServiceSoapClient();

            var response = client.RemoveCSS(request);
            if (response != null)
            {
                var blah = response.ResponseCode.ToString();
                var blah2 = HttpUtility.HtmlDecode(response.Message);
            }
            else
            {
                var blah3 = "Response from RemoveCss was Null";
            }
            return View();

        }

        public ActionResult CSS()
        {

            HCService.CssUploadRequest request = new HCService.CssUploadRequest();
            request.MerchantID = "912127036979183";
            request.Password = "e%MH0HJs3$3wk+ob";
            request.Css = ".btnDefaultIFrame {color: #fff; background-color: #f7901e; border-color: transparent; border: 1px solid transparent; border-radius: 2px;} .btnDefaultIFrame:hover {color: #fff; background-color: #f6801a; border-color: transparent;} .divTotalIFrameDefault {display: none;} .divSecurityLogoIFrame {display: none;} .divLogo {display: none;} .watermarkCustom {color: #aaaaaa; font-style: normal; vertical-align: middle; font-size: large; padding: 3px;} #divMainIFrame{width: 450px; font-family: arial; font-size: large; height:100%; clear: left; border: none;}";

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
                var blah3 = "Response from UploadCss was Null";

            }
            return View();
        }

    }
}