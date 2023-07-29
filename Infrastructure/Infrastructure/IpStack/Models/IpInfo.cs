namespace Infrastructure.IpStack.Models
{
    public class IPInfo
    {
        public string IP { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string ContinentCode { get; set; } = string.Empty;
        public string ContinentName { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string RegionCode { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Languages { get; set; } = string.Empty;
        public string CountryFlag { get; set; } = string.Empty;
        public string CountryFlagEmoji { get; set; } = string.Empty;
        public string CountryFlagEmojiUnicode { get; set; } = string.Empty;
        public int CallingCode { get; set; }
        public bool IsEU { get; set; }
    }
}
