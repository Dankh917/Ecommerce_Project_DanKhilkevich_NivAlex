using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class Address
    {
        private string street_name;
        private int number_of_building;
        private string city_name;
        private string country_name;

        public Address(string street_name, int number_of_building, string city_name, string country_name) // address constructor
        {
            StreetName = street_name;
            NumberOfBuilding = number_of_building;
            CityName = city_name;
            CountryName = country_name;
        }

        public Address(Address other) // copy constructor
        {
            StreetName = other.StreetName;
            NumberOfBuilding = other.NumberOfBuilding;
            CityName = other.CityName;
            CountryName = other.CountryName;
        }

        public string StreetName
        {
            get { return street_name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Street name cannot be null or empty.", nameof(StreetName));
                }

                if (value.Any(char.IsDigit))
                {
                    throw new ArgumentException("Street name cannot contain digits.", nameof(StreetName));
                }

                street_name = value;
            }
        }

        public int NumberOfBuilding
        {
            get { return number_of_building; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Number of building must be greater than zero.", nameof(NumberOfBuilding));
                }

                // Check if the value is a string
                if (value.GetType() == typeof(string))
                {
                    throw new ArgumentException("Number of building cannot be a string.", nameof(NumberOfBuilding));
                }

                number_of_building = value;
            }
        }

        public string CityName
        {
            get { return city_name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[a-zA-Z\s]+$"))
                    throw new ArgumentException("City name can not be null and only contain letters and spaces.");
                city_name = value;
            }
        }

        public string CountryName
        {
            get { return country_name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[a-zA-Z\s]+$"))
                    throw new ArgumentException("Country name can not be null and only contain letters and spaces.");
                country_name = value;
            }
        }

        public override bool Equals(object obj)
        {
            // Check if the object is null or not of the correct type
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // Casting the object to an Address
            Address other = (Address)obj;

            // Compare the address's street name, number of building, city name, and country name
            return StreetName == other.StreetName &&
                   NumberOfBuilding == other.NumberOfBuilding &&
                   CityName == other.CityName &&
                   CountryName == other.CountryName;
        }

        public override string ToString()
        {
            return $"Street Name: {StreetName}, Number of Building: {NumberOfBuilding}, City: {CityName}, Country: {CountryName}";
        }


    }
    

}
