using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;

namespace Yandex.Geocoder.Tests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.TestLocale))]
        public void AssertThatLocaleWorks(string query, LanguageCode locale, string result)
        {
            var geocoder = new YandexGeocoder
            {
                SearchQuery = query,
                Results = 1,
                LanguageCode = locale
            };

            var results = geocoder.GetResults();
            Console.WriteLine(results.First());
            Assert.AreEqual(result, results.First().AddressLine);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.TestLocationPoints))]
        public void AssertThatLocationByPointsWorks(double latitude, double longitude, string result)
        {
            var geocoder = new YandexGeocoder
            {
                SearchQuery = new LocationPoint(latitude, longitude).ToString(),
                Results = 1,
                LanguageCode = LanguageCode.en_US
            };

            var results = geocoder.GetResults();
            StringAssert.Contains(result, results.First().Text);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.TestResults))]
        public void AssertThatResultsAreCorrect1(int choosenResults, int expectedResults)
        {
            var geocoder = new YandexGeocoder
            {
                SearchQuery = "тверская 1",
                Results = choosenResults,
                LanguageCode = LanguageCode.ru_RU
            };

            var results = geocoder.GetResults();
            Console.WriteLine("Results count:" + results.Count);
            Assert.AreEqual(expectedResults, results.Count);
        }
        [Test]
        public void AssertConvertEnCulture()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("ru-RU");
            //with this culture, convert from "1.01" will throw exception
            //Convert.ToDouble("1.01");

            var geocoder = new YandexGeocoder
            {
                SearchQuery = "тверская 1",
                Results = 1,
                LanguageCode = LanguageCode.ru_RU
            };

            var results = geocoder.GetResults();
            Console.WriteLine("Results count:" + results.Count);
        }
    }

    public class TestData
    {
        public static IEnumerable TestLocale
        {
            get
            {
                yield return new TestCaseData("Киев", LanguageCode.ru_RU, "Киев");
                yield return new TestCaseData("Киев", LanguageCode.uk_UA, "Київ");
                yield return new TestCaseData("Kyiv", LanguageCode.uk_UA, "Київ");
                yield return new TestCaseData("Киев", LanguageCode.en_US, "Kyiv");
                yield return new TestCaseData("Киев", LanguageCode.be_BY, "Киев");
                yield return new TestCaseData("Київ", LanguageCode.ru_RU, "Киев");
            }
        }

        public static IEnumerable TestResults
        {
            get
            {
                yield return new TestCaseData(0, 10);
                yield return new TestCaseData(1, 1);
                yield return new TestCaseData(10, 10);
                yield return new TestCaseData(100, 100);
                yield return new TestCaseData(150, 100);
            }
        }

        public static IEnumerable TestLocationPoints
        {
            get
            {
                yield return new TestCaseData(49.993500, 36.230383, "Kharkiv");
                yield return new TestCaseData(50.450100, 30.523400, "Kyiv");
                yield return new TestCaseData(39.933363, 32.859742, "Ankara");
                yield return new TestCaseData(59.561152, 150.830141, "Magadan");
                yield return new TestCaseData(55.184806, 30.201622, "Viciebsk");
            }
        }
    }
}