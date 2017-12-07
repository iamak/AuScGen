using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.SeleniumFixtureTest
{
    public class AddProductTest : TestBase
    {
        //[TestCategory(TestType.regression)]
        //[TestCategory(TestType.functional)]
        //[Test(Description = "Test to perform login")]
        //public void TC01_Login()
        //{
        //    Runner.DoStep("Enter Username", () =>
        //    {
        //        Page.Login.Username.Highlight();
        //        Page.Login.Username.SendKeys("smacha@allianceglobalservices.com");
        //    });

        //    Runner.DoStep("Enter Password", () =>
        //    {
        //        Page.Login.Password.Highlight();
        //        Page.Login.Password.SendKeys("alliance@123$");
        //    });

        //    Runner.DoStep("Click on login button", () =>
        //    {
        //        Page.Login.LoginButton.Highlight();
        //        Page.Login.LoginButton.Click();
        //    });

        //    Runner.DoStep("Verify Login is successfull", () =>
        //    {
        //        Page.Login.ITChoice.Highlight();
        //        if (!Page.Login.IsChoicePresent)
        //        {
        //            Assert.Fail("Login is not successful - Choice Frame is not found");
        //        }
        //    });
        //}

        //[TestCategory(TestType.regression)]
        //[TestCategory(TestType.functional)]
        //[Test(Description = "Test to create new product")]
        //public void TC02_CreateNewProduct()
        //{
        //    Runner.DoStep("Select the Personalization role", () =>
        //    {
        //        Page.Login.ITChoice.Click();
        //    });
        //    if (Page.Home.IsPopupPresent)
        //    {
        //        Runner.DoStep("Close the popUp", () =>
        //        {
        //            Page.Home.PopupHandleClose.Click();
        //        });
        //    }

        //    Runner.DoStep("Close the side Tab", () =>
        //    {
        //        Page.Home.SideTabHandle.Highlight();
        //        Page.Home.SideTabHandle.Click();
        //    });


        //    Runner.DoStep("Click on the Product Tab", () =>
        //    {
        //        Page.Home.ProductsTab.Highlight();
        //        Page.Home.ProductsTab.Click();
        //    });

        //    Runner.DoStep("Click on new Products Button", () =>
        //    {
        //        Page.Products.NewProducts.Highlight();
        //        Page.Products.NewProducts.Click();
        //    });

        //    Runner.DoStep("Enter the product Name", () =>
        //    {
        //        Page.NewProduct.Productname.Highlight();
        //        Page.NewProduct.Productname.SendKeys("AuScGen");
        //    });

        //    Runner.DoStep("Enter the product Code", () =>
        //    {
        //        Page.NewProduct.ProductCode.Highlight();
        //        Page.NewProduct.ProductCode.SendKeys("123456");
        //    });

        //    Runner.DoStep("Enter the product Description", () =>
        //    {
        //        Page.NewProduct.ProductDescription.Highlight();
        //        Page.NewProduct.ProductDescription.SendKeys("The Automation Tool");
        //    });

        //    Runner.DoStep("Select the product family", () =>
        //    {
        //        Page.NewProduct.ProductFamily.Highlight();
        //        Page.NewProduct.ProductFamily.SelectByText("None");
        //    });

        //    Runner.DoStep("Check the Active", () =>
        //    {
        //        Page.NewProduct.Active.Highlight();
        //        Page.NewProduct.Active.Check();
        //    });

        //    Runner.DoStep("Click on Save & Click", () =>
        //    {
        //        Page.NewProduct.SaveAndAddPrice.Highlight();
        //        Page.NewProduct.SaveAndAddPrice.Click();
        //    });

        //}

        //[TestCategory(TestType.functional)]
        //[Test(Description = "Test to add price to the product")]
        //public void TC03_AddProductPrice()
        //{
        //    Runner.DoStep("Add Price to the Product", () =>
        //    {
        //        Page.Price.ProductPrice.SendKeys("500");
        //    });

        //    Runner.DoStep("Click on Save Button", () =>
        //    {
        //        Page.Price.SavePrice.Click();
        //    });
        //}

        //[Test(Description = "Test to verify the product")]
        //public void TC04_VerifyProductDetails()
        //{
        //    Runner.DoStep("Verify the product", () =>
        //    {
        //        if (Page.ProductDetails.ProductName.Text != "AuScGen")
        //        {
        //            Assert.Fail("Product name is not Correct");
        //        }
        //    });


        //}

        //[Test(Description = "Test for logout")]
        //public void TC05_LogoutApplication()
        //{
        //    Runner.DoStep("Click on the UserMenu", () =>
        //    {
        //        Page.Home.UserMenu.Highlight();
        //        Page.Home.UserMenu.Click();
        //    });

        //    Runner.DoStep("Click on the Logout", () =>
        //    {
        //        Page.Home.Logout.Highlight();
        //        Page.Home.Logout.Click();
        //    });
        //}


        // Modified site


        [TestCategory(TestType.functional)]
        [Test(Description = "Test to perform login")]
        public void TC01_Login()
        {
            Runner.DoStep("Click on login link", () =>
            {
                //Page.Login.LogInPageLink.Highlight();
                Page.Login.LogInPageLink.Click();
            });

            Runner.DoStep("Enter Username", () =>
            {
                Page.Login.Username.Highlight();
                Page.Login.Username.SendKeys("sreenivasparimi95@gmail.com");
            });

            Runner.DoStep("Enter Password", () =>
            {
                Page.Login.Password.Highlight();
                Page.Login.Password.SendKeys("Sreenivas007.");
            });

            Runner.DoStep("Click on login button", () =>
            {
                Page.Login.LogInButton.Highlight();
                Page.Login.LogInButton.Click();
            });

            Runner.DoStep("Verify Login is successfull", () =>
            {
                Page.Login.ITChoice.Highlight();
                if (!Page.Login.IsChoicePresent)
                {
                    Assert.Fail("Login is not successful - Choice Frame is not found");
                }
            });
        }


    }
}
