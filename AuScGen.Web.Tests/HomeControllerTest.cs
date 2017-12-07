using NUnit.Framework;
using AuScGen.Web.Controllers;

namespace AuScGen.Web.Tests
{
    [TestFixture]
    public class HomeControllerTest
    {
        HomeController objHome = null;
        [Test]
        public void GetMethodsFromTheAssembly_Test_ValidPath()
        {
            objHome = new HomeController();
            string filePath_Test = @"D:\AuScGenLatest\AuScGen.Web.Tests\bin\Debug\AuScGen.Web.Tests.dll";
            var data = objHome.GetMethodsFromTheAssembly(filePath_Test);
            Assert.IsTrue(true);
            Assert.NotNull(data);
        }
        [Test]
        public void GetMethodsFromTheAssembly_Test_InvalidPath()
        {
            objHome = new HomeController();
            string filePath_Test = @"D:\Mozart-Git\MozartV2\Verisk.Mozart.Web.Tests\bin\Debug\Verisk.ISO.Mozart.Web.Tests.dll";
            var data = objHome.GetMethodsFromTheAssembly(filePath_Test);
            Assert.IsTrue(true);
            Assert.NotNull(data);
        }
    }
}
