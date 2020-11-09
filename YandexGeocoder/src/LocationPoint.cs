using System;

namespace Yandex.Geocoder
{
    public class LocationPoint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LocationPoint" /> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public LocationPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LocationPoint" /> class.
        /// </summary>
        /// <param name="str">The string.</param>
        public LocationPoint(string str)
        {
            var elArray = str.Split(' ');
            Latitude = Convert.ToDouble(elArray[1], System.Globalization.CultureInfo.InvariantCulture);
            Longitude = Convert.ToDouble(elArray[0], System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Gets the latitude.
        /// </summary>
        /// <value>
        ///     The latitude.
        /// </value>
        public double Latitude { get; }

        /// <summary>
        ///     Gets the longitude.
        /// </summary>
        /// <value>
        ///     The longitude.
        /// </value>
        public double Longitude { get; }

        /// <summary>
        ///     Returns reversed coordinates: Longitude,Latitude
        /// </summary>
        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0},{1}", Longitude, Latitude);
        }
    }
}