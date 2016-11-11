using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YandexGeocoder.Raw
{
    [DataContract]
    public class Envelope
    {
        [DataMember]
        public string lowerCorner { get; set; }

        [DataMember]
        public string upperCorner { get; set; }
    }

    [DataContract]
    public class BoundedBy
    {
        [DataMember]
        public Envelope Envelope { get; set; }
    }

    [DataContract]
    public class GeocoderResponseMetaData
    {
        [DataMember]
        public string request { get; set; }

        [DataMember]
        public string found { get; set; }

        [DataMember]
        public string results { get; set; }

        [DataMember]
        public BoundedBy boundedBy { get; set; }
    }

    [DataContract]
    public class MetaDataProperty
    {
        [DataMember]
        public GeocoderResponseMetaData GeocoderResponseMetaData { get; set; }
    }

    [DataContract]
    public class DependentLocality2
    {
        public string DependentLocalityName { get; set; }
    }

    [DataContract]
    public class DependentLocality
    {
        [DataMember]
        public string DependentLocalityName { get; set; }

        [DataMember]
        public DependentLocality2 DependentLocality2 { get; set; }
    }

    [DataContract]
    public class Locality
    {
        [DataMember]
        public string LocalityName { get; set; }

        [DataMember]
        public DependentLocality DependentLocality { get; set; }
    }

    [DataContract]
    public class DependentLocality3
    {
        [DataMember]
        public string DependentLocalityName { get; set; }
    }

    [DataContract]
    public class Locality2
    {
        [DataMember]
        public string LocalityName { get; set; }

        [DataMember]
        public DependentLocality3 DependentLocality { get; set; }
    }

    [DataContract]
    public class SubAdministrativeArea
    {
        [DataMember]
        public string SubAdministrativeAreaName { get; set; }

        [DataMember]
        public Locality2 Locality { get; set; }
    }

    [DataContract]
    public class AdministrativeArea
    {
        [DataMember]
        public string AdministrativeAreaName { get; set; }

        [DataMember]
        public Locality Locality { get; set; }

        [DataMember]
        public SubAdministrativeArea SubAdministrativeArea { get; set; }
    }

    [DataContract]
    public class Country
    {
        [DataMember]
        public string AddressLine { get; set; }

        [DataMember]
        public string CountryNameCode { get; set; }

        [DataMember]
        public string CountryName { get; set; }

        [DataMember]
        public AdministrativeArea AdministrativeArea { get; set; }
    }

    [DataContract]
    public class AddressDetails
    {
        [DataMember]
        public Country Country { get; set; }
    }

    [DataContract]
    public class GeocoderMetaData
    {
        [DataMember]
        public string kind { get; set; }

        [DataMember]
        public string text { get; set; }

        [DataMember]
        public string precision { get; set; }

        [DataMember]
        public AddressDetails AddressDetails { get; set; }
    }

    [DataContract]
    public class MetaDataProperty2
    {
        [DataMember]
        public GeocoderMetaData GeocoderMetaData { get; set; }
    }

    [DataContract]
    public class Envelope2
    {
        [DataMember]
        public string lowerCorner { get; set; }

        [DataMember]
        public string upperCorner { get; set; }
    }

    [DataContract]
    public class BoundedBy2
    {
        [DataMember]
        public Envelope2 Envelope { get; set; }
    }

    [DataContract]
    public class Point
    {
        [DataMember]
        public string pos { get; set; }
    }

    [DataContract]
    public class GeoObject
    {
        [DataMember]
        public MetaDataProperty2 metaDataProperty { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public BoundedBy2 boundedBy { get; set; }

        [DataMember]
        public Point Point { get; set; }
    }

    public class FeatureMember
    {
        [DataMember]
        public GeoObject GeoObject { get; set; }
    }

    [DataContract]
    public class GeoObjectCollection
    {
        [DataMember]
        public MetaDataProperty metaDataProperty { get; set; }

        [DataMember]
        public List<FeatureMember> featureMember { get; set; }
    }

    [DataContract]
    public class ApiResponse
    {
        [DataMember]
        public GeoObjectCollection GeoObjectCollection { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public ApiResponse response { get; set; }
    }
}