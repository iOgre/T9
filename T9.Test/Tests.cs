using System;
using System.Net;
using System.Net.NetworkInformation;
using NUnit.Framework;
using T9Parser;

namespace T9.Test
{
    
    
    [TestFixture]
    public class Tests
    {
        private ParseT9 parser;
        [OneTimeSetUp]
        public void Setup()
        {
            parser = new ParseT9();
        }
        
        [Test]
        public void TestSimpleConversion()
        {
            Assert.AreEqual("44", parser.Parse("h"));
        }
        
        [Test]
        public void TestSameDigitConversion()
        {
            Assert.AreEqual("44 444", parser.Parse("hi"));
        }
        
        [Test]
        public void TestSentence()
        {
            Assert.AreEqual("333666 6660 022 2777", parser.Parse("foo  bar"));
        }

        [Test]
        public void TestSentenceNoSameDigit()
        {
            Assert.AreEqual("4433555 555666096667775553", parser.Parse("hello world"));
        }
        
        [Test]
        public void TestNonconvertible()
        {
            Assert.AreEqual("Я", parser.Parse("Я"));
        }
        
        [Test]
        public void TestMultipleFalls()
        {
            Assert.Catch<AggregateException>(() =>
                parser.Parse("ЯZZZZZAAAAZЯЯЯЯЯЯЯЯЯЯЯЯЯЯяяяяяяяяяяяяяяяяяяяяяяяяяяяяыыыыыыыыыыыыыяяяяяяяяяяяяяяяя", ConversionSettings.FailOnUnconvertable));
        }
    }
}