using System;
using System.Text.Json.Serialization;

namespace OneListClient
{
    public class Item
    {
        // [JsonPropertyName] is used to get the actual correct name of the Http so we can name it in our class/object anything we want 
        //creating a properties with Get and Set to read the Http in out terminal
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("complete")]
        public bool Complete { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }


        // Creating a property to show us (complete) in our terminal a bit easier adn better
        public string CompletedStatus
        {
            get
            {
                if (Complete == true)
                {
                    return "completed";
                }
                else
                {
                    return "Not Completed";
                }
            }
        }






    }
}