﻿using BeyondNet.Ddd;

namespace BeyondNet.Ddd.valueObjects
{
    public struct AddressProps
    {
        public string Direction { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public AddressProps(string direction, string city, string country, string state, string zipCode)
        {
            Direction = direction;
            City = city;
            Country = country;
            State = state;
            ZipCode = zipCode;
        }
    }
    public class Address : ValueObject<AddressProps>
    {
        private Address(AddressProps value) : base(value)
        {
        }

        public static Address Create(string direction, string city, string country, string state, string zipCode)
        {
            return new Address(new AddressProps(direction, city, country, state, zipCode));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue().Direction;
            yield return GetValue().City;
            yield return GetValue().Country;
            yield return GetValue().State;
            yield return GetValue().ZipCode;
        }
    }
}
