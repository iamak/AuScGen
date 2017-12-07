using AuScGen.SeleniumTestPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.SeleniumFixtureTest
{
    public class Page
    {
        private List<object> utilsList = new List<object>();

        public Page(TestBase testBase)
        {
            //Telerik = testBase.Telerik;

            utilsList.Add(testBase.WebDriver);
            utilsList.Add(testBase.KeyBoardSimulator);
            //utilsList.Add(testBase.DialogHandler);
            utilsList.Add(testBase.DBValidation);
        }

        private LoginPage login;
        public LoginPage Login
        {
            get
            {
                if (null == login)
                {
                    login = new LoginPage(utilsList);
                }
                return login;
            }
        }

        private HomePage home;
        public HomePage Home
        {
            get
            {
                if (null == home)
                {
                    home = new HomePage(utilsList);
                }
                return home;
            }
        }

        private AddPricePage price;
        public AddPricePage Price
        {
            get
            {
                if (null == price)
                {
                    price = new AddPricePage(utilsList);
                }
                return price;
            }
        }

        private NewProductsPage newProduct;
        public NewProductsPage NewProduct
        {
            get
            {
                if (null == newProduct)
                {
                    newProduct = new NewProductsPage(utilsList);
                }
                return newProduct;
            }
        }

        private ProductDetailsPage productDetails;
        public ProductDetailsPage ProductDetails
        {
            get
            {
                if (null == productDetails)
                {
                    productDetails = new ProductDetailsPage(utilsList);
                }
                return productDetails;
            }
        }

        private ProductsPage products;
        public ProductsPage Products
        {
            get
            {
                if (null == products)
                {
                    products = new ProductsPage(utilsList);
                }
                return products;
            }
        }

    }
}
