using PurchaseOrderSystem.Domain.Orders.DataPackage;
using System;

namespace PurchaseOrderSystem.Domain.Orders
{
    public record Address
    {
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        protected Address(string street, string zipCode, string city, string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country must be specified.");

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City must be specified.");

            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentException("Zip code must be specified.");

            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street must be specified.");


            Street = street;
            ZipCode = zipCode;
            City = city;
            Country = country;
        }

        public static Address Create(AddressData data)
        {
            return new Address(data.Street, data.ZipCode, data.City, data.Country);
        }
    }
}
