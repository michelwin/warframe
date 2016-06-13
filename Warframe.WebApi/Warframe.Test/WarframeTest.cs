namespace Warframe.Test
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json;

    using Warframe.Lib;

    [TestClass]
    public class WarframeTest
    {
        [TestMethod]
        public void ShouldBeReturnTheDucatList()
        {
            var ducatList = new List<Ducat>();

            ducatList.Load("http://warframe.wikia.com/wiki/Ducats/Prices");

            Assert.IsTrue(ducatList.Any());
            var json = JsonConvert.SerializeObject(ducatList);

            Assert.IsNotNull(json);
        }
    }
}
