using YandexGeocoder.Raw;

namespace YandexGeocoder
{
    public class YandexResponse
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="YandexResponse" /> class.
        /// </summary>
        /// <param name="geoObject">Yandex geo object.</param>
        public YandexResponse(GeoObject geoObject)
        {
            if (geoObject != null)
            {
                Name = geoObject.name;
                Description = geoObject.description;

                Point = new LocationPoint(geoObject.Point.pos);
                PointLowerCorner = new LocationPoint(geoObject.boundedBy.Envelope.lowerCorner);
                PointUpperCorner = new LocationPoint(geoObject.boundedBy.Envelope.upperCorner);

                var metadataProperty = geoObject.metaDataProperty;

                var geocoderMetaData = metadataProperty?.GeocoderMetaData;
                if (geocoderMetaData == null) return;

                Text = geocoderMetaData.text;
                Kind = geocoderMetaData.kind;

                var addressDetails = geocoderMetaData.AddressDetails;
                if (addressDetails == null) return;

                var country = addressDetails.Country;
                AddressLine = country.AddressLine;
                CountryName = country.CountryName;
                CountryCode = country.CountryNameCode;

                var administrativeArea = country.AdministrativeArea;
                if (administrativeArea == null) return;

                AdministrativeAreaName = administrativeArea.AdministrativeAreaName;

                var subAdministrativeArea = administrativeArea.SubAdministrativeArea;
                if (subAdministrativeArea == null) return;

                SubAdministrativeArea = subAdministrativeArea.SubAdministrativeAreaName;
                LocalityName = subAdministrativeArea.Locality.LocalityName;
            }
        }

        /// <summary>
        ///     Gets the address line.
        /// </summary>
        /// <value>
        ///     The address line.
        /// </value>
        public string AddressLine { get; }

        /// <summary>
        ///     Gets the name of the administrative area.
        /// </summary>
        /// <value>
        ///     The name of the administrative area.
        /// </value>
        public string AdministrativeAreaName { get; }

        /// <summary>
        ///     Gets the sub administrative area.
        /// </summary>
        /// <value>
        ///     The sub administrative area.
        /// </value>
        public string SubAdministrativeArea { get; }

        /// <summary>
        ///     Gets the name of the locality.
        /// </summary>
        /// <value>
        ///     The name of the locality.
        /// </value>
        public string LocalityName { get; }

        /// <summary>
        ///     Gets the name of the country.
        /// </summary>
        /// <value>
        ///     The name of the country.
        /// </value>
        public string CountryName { get; }

        /// <summary>
        ///     Gets the country code.
        /// </summary>
        /// <value>
        ///     The country code.
        /// </value>
        public string CountryCode { get; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        ///     Gets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public string Text { get; }

        /// <summary>
        ///     Gets the kind.
        /// </summary>
        /// <value>
        ///     The kind.
        /// </value>
        public string Kind { get; }

        /// <summary>
        ///     Gets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; }

        /// <summary>
        ///     Gets the point.
        /// </summary>
        /// <value>
        ///     The point.
        /// </value>
        public LocationPoint Point { get; }

        /// <summary>
        ///     Gets the point lower corner.
        /// </summary>
        /// <value>
        ///     The point lower corner.
        /// </value>
        public LocationPoint PointLowerCorner { get; }

        /// <summary>
        ///     Gets the poin upper corner.
        /// </summary>
        /// <value>
        ///     The poin upper corner.
        /// </value>
        public LocationPoint PointUpperCorner { get; }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return $"{nameof(AddressLine)}: {AddressLine}\n" +
                   $"{nameof(AdministrativeAreaName)}: {AdministrativeAreaName}\n" +
                   $"{nameof(SubAdministrativeArea)}: {SubAdministrativeArea}\n" +
                   $"{nameof(LocalityName)}: {LocalityName}\n" +
                   $"{nameof(CountryName)}: {CountryName}\n" +
                   $"{nameof(CountryCode)}: {CountryCode}\n" +
                   $"{nameof(Text)}: {Text}\n" +
                   $"{nameof(Kind)}: {Kind}\n" +
                   $"{nameof(Name)}: {Name}\n" +
                   $"{nameof(Description)}: {Description}\n" +
                   $"{nameof(Point)}: {Point}\n" +
                   $"{nameof(PointLowerCorner)}: {PointLowerCorner}\n" +
                   $"{nameof(PointUpperCorner)}: {PointUpperCorner}";
        }
    }
}