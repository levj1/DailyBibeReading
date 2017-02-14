using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBReading.Models;

namespace DBReadingTests
{
    [TestClass]
    public class ReaderTest
    {
        [TestMethod]
        public void ValidEmailTest()
        {
            // actual
            Reader reader = new Reader("james", "", "leveille", "levcom");

            // expectation 
            bool expected = false;

            // Attest
            Assert.AreEqual(expected, reader.ValidEmail());
        }
    }
}
