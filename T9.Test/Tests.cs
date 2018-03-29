using System;
using System.Net.NetworkInformation;
using NUnit.Framework;
using T9Parser;

namespace T9.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestSimpleConversion()
        {
            Assert.AreEqual("44", ParseT9.Parse("h"));
        }
        
        [Test]
        public void TestSameDigitConversion()
        {
            Assert.AreEqual("44 444", ParseT9.Parse("hi"));
        }

        
    }
}