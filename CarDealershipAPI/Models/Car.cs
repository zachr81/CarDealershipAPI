using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace CarDealershipAPI.Models
{
    public class Car
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("make")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Make Make { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("hasSunroof")]
        public bool HasSunroof { get; set; }

        [JsonProperty("isFourWheelDrive")]
        public bool IsFourWheelDrive { get; set; }

        [JsonProperty("hasLowMiles")]
        public bool HasLowMiles { get; set; }

        [JsonProperty("hasPowerWindows")]
        public bool HasPowerWindows { get; set; }

        [JsonProperty("hasNavigation")]
        public bool HasNavigation { get; set; }

        [JsonProperty("hasHeatedSeats")]
        public bool HasHeatedSeats { get; set; }

        [JsonProperty("color")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Color Color { get; set; }


        public override bool Equals(object obj)
        {
            return obj is Car car &&
                   this.Id == car.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }
    }
}
