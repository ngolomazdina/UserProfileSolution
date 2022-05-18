using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UserProfile.Application.BL;
using UserProfile.Infrastructure;
using UserProfile.Infrastructure.DBModel;

namespace UserProfile.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        static IProfileService _service;

        [ClassInitialize]
        public static void BuildContainer(TestContext context)
        {
            var container = AutofacContainer.ConfigureContainer();
            container.Resolve<IRepository<DbProfileTextFile>>();

            _service = container.Resolve<IProfileService>();
        }

        [TestMethod]
        public void TestMethodGetStatistics()
        {
            var statistics = _service.GetStatistic();

            Assert.AreEqual(Common.CodeResult.Success, statistics.Code);
            Assert.AreEqual(32, statistics.Data.AverageAge);
            Assert.AreEqual("C#", statistics.Data.MostPopularLang);
            Assert.AreEqual("Петров Петр Петрович", statistics.Data.Senior);

        }
    }
}
