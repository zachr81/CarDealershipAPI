using System;

namespace CarDealershipAPI.Models
{
    public class CarException : Exception
    {
        public string errorMessage { get; set; }

        public CarException(string message) : base()
        {
            errorMessage = message;
        }
    }
}
