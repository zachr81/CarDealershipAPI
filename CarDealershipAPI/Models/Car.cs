using System;

namespace CarDealershipAPI.Models
{
    public class Car : CarInformation
    {
        public string _id { get; set; }

        public Make Make { get; set; }

        public int year { get; set; }

        public int price { get; set; }

        
        public override bool Equals(object obj)
        {
            return obj is Car car &&
                   this._id == car._id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this._id);
        }
    }
}
