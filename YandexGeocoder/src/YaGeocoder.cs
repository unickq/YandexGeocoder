using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace YandexGeocoder
{
    public class YaGeocoder
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
        public string Apikey { get; set; } = null;


        /// <summary>
        ///     Builds the URL with all parameters
        /// </summary>
        /// <returns>URL for webRequest</returns>
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

        /// <summary>
        ///     Gets the JSON responce from Yandex YaGeocoder.
        /// </summary>
        /// <returns></returns>
        private string GetJsonResponce()
        {
            var url = BuildUrl();
            var request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            using (var response = (HttpWebResponse) request.GetResponse())
            {
                using (var dataStream = response.GetResponseStream())
                {
                    if (dataStream != null)
                        using (var reader = new StreamReader(dataStream))
                        {
                            return reader.ReadToEnd();
                        }
                    throw new Exception("Response stream is null");
                }
            }
        }

        /// <summary>
        ///     Gets the responses list.
        /// </summary>
        /// <returns>List with YandexResponse objects</returns>
        public List<YandexResponse> GetResults()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(GetJsonResponce());
            writer.Flush();
            stream.Position = 0;
            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            var rootNode = (RootObject) serializer.ReadObject(stream);
            var ynadexResponsesList = new List<YandexResponse>();
            foreach (var geoObject in rootNode.response.GeoObjectCollection.featureMember)
            {
                var response = new YandexResponse(geoObject.GeoObject);
                ynadexResponsesList.Add(response);
            }
            return ynadexResponsesList;
        }
    }
}
