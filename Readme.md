# Yandex Geocoder .NET wrapper
.NET API for converting geographical coordinates to an address and back.
### Build:
[![Build status](https://ci.appveyor.com/api/projects/status/42jdei8626b4ie1h?svg=true)](https://ci.appveyor.com/project/unickq/yandexgeocoder)
[![Test status](http://flauschig.ch/batch.php?type=tests&account=unickq&slug=yandexgeocoder&branch=master)](https://ci.appveyor.com/project/unickq/yandexgeocoder/branch/master)
### Download (NuGet):
[![NuGet YandexGeocoder](http://flauschig.ch/nubadge.php?id=YandexGeocoder)](https://www.nuget.org/packages/YandexGeocoder)
### About:
[Yandex Geocoder](https://tech.yandex.com/maps/geocoder/) service can get the coordinates and other information about an object using its name or address, as well as the opposite, using the coordinates of an object to get its address (reverse geocoding).

For example, the geocoder receives the request “Türkiye, İstanbul, Kartal, Esentepe, Aydos Sokak, 32” and returns the geographical coordinates of this building: “29.198184 40.900640” (longitude, latitude). Conversely, if the request contains the geographical coordinates of the building “29.198184 40.900640”, the geocoder will return the address.

Note: Free version supports 25000 requests per day. Also not  all countries supported - read [geocoder documentation](https://tech.yandex.com/maps/doc/geocoder/desc/concepts/About-docpage/).

### Example:
```csharp
var geocoder = new YandexGeocoder {
    SearchQuery = "Kyiv, Ukraine, Maydan Nezalezhnosti",
    Results = 1,
    LanguageCode = LanguageCode.en_US
};
Console.WriteLine(geocoder.GetResults().First());
```    

##### Output:

    AddressLine: Kyiv, Nezalezhnosti Square
    AdministrativeAreaName: Kyiv
    CountryName: Ukraine
    CountryCode: UA
    Text: Ukraine, Kyiv, Nezalezhnosti Square
    Kind: street
    Name: Nezalezhnosti Square
    Description: Kyiv, Ukraine
    Point: 30.523846,50.450131
    PointLowerCorner: 30.521681,50.448904
    PointUpperCorner: 30.526056,50.451514
