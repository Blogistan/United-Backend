namespace Infrastructure.IpStack.Models
{
    public class IPInfo
    {
        public string IP { get; set; } = String.Empty;
        public string Hostname { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string Continent_Code { get; set; } = String.Empty;
        public string Continen_Name { get; set; } = String.Empty;
        public string Country_Code { get; set; } = String.Empty;
        public string Country_Name { get; set; } = String.Empty;
        public string Region_Code { get; set; } = String.Empty;
        public string Region_Name { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Zip { get; set; } = String.Empty;
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
        public string Capital { get; set; } = String.Empty;
        public List<Language> Languages { get; set; }
        public string Country_Flag { get; set; } = String.Empty;
        public string Country_Flag_Emoji { get; set; } = String.Empty;
        public string Country_Flag_Emoji_Unicode { get; set; } = String.Empty;
        public int CallingCode { get; set; }
        public bool IsEU { get; set; }
    }
    public class Language
    {
        public string Code { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Native { get; set; } = String.Empty;
    }

    public class TimeZone
    {
        public string ID { get; set; } = String.Empty;
        public string Current_Time { get; set; } = String.Empty;
        public int GMTOffset { get; set; }
        public string Code { get; set; } = String.Empty;
        public bool IsDaylightSaving { get; set; }
    }

    public class Currency
    {
        public string Code { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Plural { get; set; } = String.Empty;
        public string Symbol { get; set; } = String.Empty;
        public string SymbolNative { get; set; } = String.Empty;
    }

    public class Connection
    {
        public int ASN { get; set; }
        public string ISP { get; set; } = String.Empty;
    }

    public class Security
    {
        public bool IsProxy { get; set; }
        public string ProxyType { get; set; } = String.Empty;
        public bool IsCrawler { get; set; }
        public string CrawlerName { get; set; } = String.Empty;
        public string CrawlerType { get; set; } = String.Empty;
        public bool IsTor { get; set; }
        public string ThreatLevel { get; set; } = String.Empty;
        public List<string> ThreatTypes { get; set; }
    }
}
