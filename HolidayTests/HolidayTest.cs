using System;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;

namespace HolidayTests
{
    [TestFixture]
    public class HolidayTest
    {
        private FakeHoliday _holiday;

        [SetUp]
        public void SetUp()
        {
            
            _holiday = new FakeHoliday();
        }

        [Test]
        public void when_today_is_xmas()
        {
            GivenToday(12, 25);
            SayHelloShouldBe("Merry Xmas");
        }

        [Test]
        public void when_today_is_not_xmas()
        {
            GivenToday(12, 27);
            SayHelloShouldBe("Today is not Xmas");
        }
        private void SayHelloShouldBe(string message)
        {
            Assert.True(_holiday.SayHello().Equals(message));
        }

        private void GivenToday(int month, int day)
        {
            _holiday.Today = new DateTime(2021, month, day);
        }
    }

    public class Holiday
    {
        public string SayHello()
        {
            if (GetToday() == "1225")
                return "Merry Xmas";
            return "Today is not Xmas";
        }

        protected virtual string GetToday()
        {
            return DateTime.Today.ToString("MMdd");
        }
    }

    class FakeHoliday : Holiday
    {
        private DateTime _today;

        public DateTime Today
        {
            set => _today = value;
        }

        protected override string GetToday()
        {
            return _today.ToString("MMdd");
        }
    }
}