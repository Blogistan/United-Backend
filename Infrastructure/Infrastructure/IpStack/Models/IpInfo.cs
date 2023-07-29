namespace Infrastructure.IpStack.Models
{
    public class IPInfo
    {
        public string IP { get; set; }
        public string Hostname { get; set; }
        public string Type { get; set; }
        public string Continent_Code { get; set; }
        public string Continen_Name { get; set; }
        public string Country_Code { get; set; }
        public string Country_Name { get; set; }
        public string Region_Code { get; set; }
        public string Region_Name { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Location Location { get; set; }
        public TimeZone TimeZone { get; set; }
        public Currency Currency { get; set; }
        public Connection Connection { get; set; }
        public Security Security { get; set; }


    }
    public class Location
    {
        public int Geoname_Id { get; set; }
        public string Capital { get; set; }
        public List<Language> Languages { get; set; }
        public string Country_Flag { get; set; }
        public string Country_Flag_Emoji { get; set; }
        public string Country_Flag_Emoji_Unicode { get; set; }
        public int CallingCode { get; set; }
        public bool IsEU { get; set; }
    }
    public class Language
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Native { get; set; }
    }

    public class TimeZone
    {
        public string ID { get; set; }
        public string Current_Time { get; set; }
        public int GMTOffset { get; set; }
        public string Code { get; set; }
        public bool IsDaylightSaving { get; set; }
    }

    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Plural { get; set; }
        public string Symbol { get; set; }
        public string SymbolNative { get; set; }
    }

    public class Connection
    {
        public int ASN { get; set; }
        public string ISP { get; set; }
    }

    public class Security
    {
        public bool IsProxy { get; set; }
        public string ProxyType { get; set; }
        public bool IsCrawler { get; set; }
        public string CrawlerName { get; set; }
        public string CrawlerType { get; set; }
        public bool IsTor { get; set; }
        public string ThreatLevel { get; set; }
        public List<string> ThreatTypes { get; set; }
    }
}
