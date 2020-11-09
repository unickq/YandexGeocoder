using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Yandex.Geocoder.Model;

namespace Yandex.Geocoder
{
    public class YandexGeocoder
    {
        /// <summary>
        ///     The address you want to geocode, or the geographical coordinates. Coordinates can be set in reverse geocoding:
        /// </summary>
        public string SearchQuery { get; set; } = "null";

        /// <summary>
        ///     Locale.
        /// </summary>
        public LanguageCode LanguageCode { get; set; } = LanguageCode.ru_RU;

        /// <summary>
        ///     Maximum number of objects to return. Default value: 10. Maximum value: 100.
        /// </summary>
        public int Results { get; set; } = 10;

        /// <summary>
        ///     The number of objects to skip in the response (starting from the first one). Default value: 0.
        /// </summary>
        public int Skip { get; set; } = 0;

        /// <summary>
        ///     The key assigned in the Yandex Developer's Dashboard.
        /// </summary>
        public string Apikey { get; set; } = Environment.GetEnvironmentVariable("YANDEX_GEOCODER_KEY");


        /// <summary>
        ///     Builds the URL with all parameters
        /// </summary>
        private string BuildUrl()
        {
            var sb = new StringBuilder();
            sb.Append("http://geocode-maps.yandex.ru/1.x/?");
            sb.Append($"geocode={SearchQuery}");
            sb.Append($"&lang={LanguageCode}");
            sb.Append($"&results={Results}");
            if (Skip != 0) sb.Append($"&skip={Skip}");
            if (Apikey != null) sb.Append($"&apikey={Apikey}");
            sb.Append("&format=json");
            return sb.ToString();
        }

        public async Task<string> GetResponseAsync()
        {
            var url = BuildUrl();
            var request = (HttpWebRequest) WebRequest.Create(url);
            using (var response = await request.GetResponseAsync())
            using (var responseStream = response.GetResponseStream())
            using (var reader =
                new StreamReader(responseStream ?? throw new InvalidOperationException("Response is null")))
            {
                return reader.ReadToEnd();
            }
        }

        public string GetResponse()
        {
            return GetResponseAsync().Result;
        }

        /// <summary>
        ///     Gets the Raw data. RootObject asynchronously
        /// </summary>
        public async Task<RootObject> GetRawDataAsync()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(await GetResponseAsync());
            writer.Flush();
            stream.Position = 0;
            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            return (RootObject) serializer.ReadObject(stream);
        }

        /// <summary>
        ///     Gets the Raw data. RootObject
        /// </summary>
        public RootObject GetRawData()
        {
            return GetRawDataAsync().Result;
        }

        /// <summary>
        ///     Gets the responses list.
        /// </summary>
        public List<YandexResponse> GetResults()
        {
            return GetResultsAsync().Result;
        }

        /// <summary>
        ///     Gets the responses list asynchronously.
        /// </summary>
        public async Task<List<YandexResponse>> GetResultsAsync()
        {
            return (await GetRawDataAsync()).response.GeoObjectCollection.featureMember
                .Select(geoObject => new YandexResponse(geoObject.GeoObject)).ToList();
        }
    }
}