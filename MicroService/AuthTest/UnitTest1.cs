using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Quotation.Service.Query;
using Quotation.Service.Query.DTOs;
using System.Net.Sockets;

namespace AuthTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            var mockService = new Mock<IQuotationQueryService>();


            string result = Quotation.Service.Query.QuotationQueryService.Validate();
            Assert.AreEqual("Validando Unit Test",result);
        }
    }
}
