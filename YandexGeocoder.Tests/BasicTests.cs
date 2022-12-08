using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Yandex.Geocoder.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    public class BasicTests
    {
        private const string API_KEY = "Set your key here";

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.TestLocale))]
        public async Task ValidatesResultsAsync(string query, LanguageCode locale, string result)
        {
            var geocoder = new YandexGeocoder
            {
                Apikey = API_KEY,
                SearchQuery = query,
                Results = 1,
                LanguageCode = locale
            };


            var results = geocoder.GetResultsAsync();
            Console.WriteLine((await results).First());
            StringAssert.Contains(result, (await results).First().AddressLine);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.TestLocationPoints))]
        public void ValidatesLocationPoints(double latitude, double longitude, string result)
        {
            var geocoder = new YandexGeocoder
            {
                Apikey = API_KEY,
                SearchQuery = new LocationPoint(latitude, longitude).ToString(),
                Results = 1,
                LanguageCode = LanguageCode.en_US
            };

            var results = geocoder.GetResults();
            StringAssert.Contains(result, results.First().Text);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.TestResults))]
        public void ValidatesResultsCount(int chosenResults, int expectedResults)
        {
            var geocoder = new YandexGeocoder
            {
                Apikey = API_KEY,
                SearchQuery = "тверская 1",
                Results = chosenResults,
                LanguageCode = LanguageCode.ru_RU
            };

            var results = geocoder.GetResults();
            Console.WriteLine("Results count:" + results.Count);
            Assert.AreEqual(expectedResults, results.Count);
        }

        [Test]
        public void ValidatesCultureAndReadsApiFromEnv()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.GetCultureInfo("ru-RU");
            
            Environment.SetEnvironmentVariable("YANDEX_GEOCODER_KEY", API_KEY);

            var geocoder = new YandexGeocoder
            {
                SearchQuery = "тверская 1",
                Results = 1,
                LanguageCode = LanguageCode.ru_RU
            };

            var results = geocoder.GetResults();
            Console.WriteLine("Results count:" + results.Count);
        }
        
        [Test]
        [TestCase("Зейский государственный природный заповедник")]
        public async Task QueryDoesntThrow(string query)
        {
          var geocoder = new YandexGeocoder
          {
            Apikey = API_KEY,
            SearchQuery = query,
          };


          var results = geocoder.GetResultsAsync();
          Console.WriteLine((await results).First());
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
                yield return new TestCaseData("Киев", LanguageCode.be_BY, "Кіеў");
                yield return new TestCaseData("Київ", LanguageCode.ru_RU, "Киев");
            }
        }

        public static IEnumerable TestResults
        {
            get
            {
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