using System;
using NUnit.Framework;

namespace Nelson.Tests
{
    public class EventParserTests
    {
        private EventParser _sut;
        
        [SetUp]
        public void Setup()
        {
            _sut = new EventParser();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void ParseEventTime()
        {
            var stringToParse = "12.07. 20:00";

            var correctDateTime = new DateTime(2020, 07, 12, 20, 00,00);

            var parsedTime = EventParser.ParseDateTime(stringToParse, 2020);

            Assert.AreEqual(correctDateTime, parsedTime);
        }
    }
}