namespace Infrastructure.Dtos.Twitter
{

    public class TwitterUserInfo
    {
        public long id { get; set; }
        public string id_str { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string screen_name { get; set; } = string.Empty;
        public string location { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
        public Entities entities { get; set; }
        public bool _protected { get; set; }
        public long followers_count { get; set; }
        public long friends_count { get; set; }
       public long listed_count { get; set; }
        public string created_at { get; set; } = string.Empty;
        public long favourites_count { get; set; }
        public object utc_offset { get; set; } = string.Empty;
        public object time_zone { get; set; } = string.Empty;
        public bool geo_enabled { get; set; }
        public bool verified { get; set; }
        public long statuses_count { get; set; }
        public object lang { get; set; }
        public Status status { get; set; }
        public bool contributors_enabled { get; set; }
        public bool is_translator { get; set; }
        public bool is_translation_enabled { get; set; }
        public string profile_background_color { get; set; } = string.Empty;
        public object profile_background_image_url { get; set; } = string.Empty;
        public object profile_background_image_url_https { get; set; } = string.Empty;
        public bool profile_background_tile { get; set; }
        public string profile_image_url { get; set; } = string.Empty;
        public string profile_image_url_https { get; set; } = string.Empty;
        public string profile_banner_url { get; set; } = string.Empty;
        public string profile_link_color { get; set; } = string.Empty;
        public string profile_sidebar_border_color { get; set; } = string.Empty;
        public string profile_sidebar_fill_color { get; set; } = string.Empty;
        public string profile_text_color { get; set; } = string.Empty;
        public bool profile_use_background_image { get; set; }
        public bool has_extended_profile { get; set; }
        public bool default_profile { get; set; }
        public bool default_profile_image { get; set; }
        public bool following { get; set; }
        public bool follow_request_sent { get; set; }
        public bool notifications { get; set; }
        public string translator_type { get; set; } = string.Empty;
        public object[]? withheld_in_countries { get; set; } 
        public bool suspended { get; set; }
        public bool needs_phone_verification { get; set; }
        public string email { get; set; } = string.Empty;
    }

    public class Entities
    {
        public Url url { get; set; }
        public Description description { get; set; }
    }

    public class Url
    {
        public Url1[] urls { get; set; }
    }

    public class Url1
    {
        public string url { get; set; } = string.Empty;
        public string expanded_url { get; set; } = string.Empty;
        public string display_url { get; set; } = string.Empty;
        public int[] indices { get; set; }
    }

    public class Description
    {
        public object[] urls { get; set; }
    }

    public class Status
    {
        public string created_at { get; set; } = string.Empty;
        public long id { get; set; }
        public string id_str { get; set; } = string.Empty;
        public string text { get; set; } = string.Empty;
        public bool truncated { get; set; }
        public Entities1 entities { get; set; }
        public string source { get; set; } = string.Empty;
        public long in_reply_to_status_id { get; set; }
        public string in_reply_to_status_id_str { get; set; } = string.Empty;
        public long in_reply_to_user_id { get; set; }
        public string in_reply_to_user_id_str { get; set; } = string.Empty;
        public string in_reply_to_screen_name { get; set; } = string.Empty;
        public object geo { get; set; }
        public object coordinates { get; set; }
        public object place { get; set; }
        public object contributors { get; set; }
        public bool is_quote_status { get; set; }
        public long retweet_count { get; set; }
        public long favorite_count { get; set; }
        public bool favorited { get; set; }
        public bool retweeted { get; set; }
        public string lang { get; set; } = string.Empty;
    }

    public class Entities1
    {
        public Hashtag[] hashtags { get; set; }
        public object[] symbols { get; set; }
        public User_Mentions[] user_mentions { get; set; }
        public object[] urls { get; set; }
    }

    public class Hashtag
    {
        public string text { get; set; } = string.Empty;
        public int[] indices { get; set; }
    }

    public class User_Mentions
    {
        public string screen_name { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public long id { get; set; }
        public string id_str { get; set; } = string.Empty;
        public int[] indices { get; set; }
    }

}
