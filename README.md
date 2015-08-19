# HostedCheckout.MVC.CSharp

* More documentation?  http://developer.mercurypay.com
* Questions?  integrationteam@mercurypay.com
* **Feature request?** Open an issue.
* Feel like **contributing**?  Submit a pull request.

# Overview

Visual Studio MVC website application that shows how to integrate to Mercury's Hosted Checkout platform using an iFrame and performs styling of the hosted site using CSS.

![HostedCheckout.MVC.CSharp](https://github.com/mercurypay/HostedCheckout.MVC.CSharp/blob/master/screenshot1.PNG)


![HostedCheckout.MVC.CSharp](https://github.com/mercurypay/HostedCheckout.MVC.CSharp/blob/master/screenshot2.PNG)

# Payment Processing

##Step 1: Initialize Payment

Note:  req.DisplayStyle = "custom" -- this allows for custom styling attributes via a CSS upload mechanism.

```
HCService.HCServiceSoapClient client = new HCService.HCServiceSoapClient();
HCService.InitPaymentRequest req = new HCService.InitPaymentRequest();
req.MerchantID = merchantId;
req.Password = password;
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
```


##Step 2: Display HostedCheckout

In the Home Controller's Buy action we are setting the ViewBag.URL property to the URL of the hosted checkout page and appending the PaymentID returned in the response.  We will use this ViewBag property when rendering the page in the View.

```
ViewBag.URL = "https://hc.mercurydev.net/CheckoutIFrame.aspx?ReturnMethod=get&pid=" + resp.PaymentID;
```

This code is in the Buy.cshtml View.  Notice we are setting the src property of the iframe to the ViewBag.URL that we set above.  When we set this src property and the iframe renders the iframe will be redirected to the Hosted Checkout page where the customer will enter their card data.

```
<div class="row">
  <iframe id='ifrm' src="@ViewBag.URL" height="550" width="550" scrolling="auto" frameborder="0" style="text-align: center;">
    Your browser does not support iFrames. To view this content, please download and use the latest version of one of the following browsers: Internet Explorer, Firefox, Google Chrome or Safari.
  </iframe>
</div>

```

##Step 3: Verify Payment

After the customer enters their card data and presses submit the payment is processed.  When payment processing is complete the browser is redirected to the ProcessCompleteUrl sent during InitializePayment.  At that point the VerifyPayment method is called to gain access to all of the properties of the response.

```
HCService.HCServiceSoapClient client = new HCService.HCServiceSoapClient();
HCService.PaymentInfoRequest req = new HCService.PaymentInfoRequest();
req.MerchantID = merchantId;
req.Password = password;
req.PaymentID = PaymentID;
var resp = client.VerifyPayment(req);
```

#CSS Styling

Make sure to set DisplayStyle = "custom" in the InitPayment call to allow CSS customization.

There are two webmethods that will help you perform CSS styling of the hosted page and are shown below.  After uploading the initial CSS you can modify the CSS by calling UploadCSS again which will overwrite the original.  The only reason to call RemoveCSS is if you no longer require CSS customization or to test/validate that your CSS customizations are taking effect.

## UploadCSS

```
HCService.CssUploadRequest request = new HCService.CssUploadRequest();
request.MerchantID = merchantId;
request.Password = password;
request.Css = "{Include Valid CSS here}";

HCService.HCServiceSoapClient client = new HCService.HCServiceSoapClient();
HCService.CssAdminResponse response = new HCService.CssAdminResponse();
response = client.UploadCSS(request);
```

## RemoveCSS

```
var request = new HCService.CssRemoveRequest();
request.MerchantID = merchantId;
request.Password = password;

var client = new HCService.HCServiceSoapClient();

var response = client.RemoveCSS(request);
```


###©2015 Mercury Payment Systems, LLC - all rights reserved.

Disclaimer:
This software and all specifications and documentation contained herein or provided to you hereunder (the "Software") are provided free of charge strictly on an "AS IS" basis. No representations or warranties are expressed or implied, including, but not limited to, warranties of suitability, quality, merchantability, or fitness for a particular purpose (irrespective of any course of dealing, custom or usage of trade), and all such warranties are expressly and specifically disclaimed. Mercury Payment Systems shall have no liability or responsibility to you nor any other person or entity with respect to any liability, loss, or damage, including lost profits whether foreseeable or not, or other obligation for any cause whatsoever, caused or alleged to be caused directly or indirectly by the Software. Use of the Software signifies agreement with this disclaimer notice.

![Analytics](https://ga-beacon.appspot.com/UA-60858025-43/HostedCheckout.MVC.CSharp/readme?pixel)
