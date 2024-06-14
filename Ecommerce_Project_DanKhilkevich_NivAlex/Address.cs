﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class Address
    {
        private string street_name;
        private int number_of_building;
        private string city_name;
        private string country_name;

        public Address(string street_name, int number_of_building, string city_name, string country_name) //address constructor
        {
            while (true)
            {
                if (SetStreetName(street_name) == true) { break; }
                Console.WriteLine("please reenter street name: ");
                street_name = Console.ReadLine();
                
            }

            while (true)
            {
                if (SetNumberOfBuilding(number_of_building) == true) { break; }
                Console.WriteLine("please reenter number of building: ");
                number_of_building = int.Parse(Console.ReadLine());

            }

            while (true)
            {
                if (SetCityName(city_name) == true) { break; }
                Console.WriteLine("please reenter city name: ");
                city_name = Console.ReadLine();

            }

            while (true)
            {
                if (SetCountryName(country_name) == true) { break; }
                Console.WriteLine("please reenter country name: ");
                country_name = Console.ReadLine();

            }

        }

        public Address(Address other) //copy constructor
        {
            street_name=other.street_name;
            number_of_building=other.number_of_building;
            city_name=other.city_name;
            country_name=other.country_name;
        }

        public bool SetStreetName(string street_name)
        {
            this.street_name = street_name;
            return true;
        }

        public bool SetNumberOfBuilding(int number_of_building)
        {
            this.number_of_building = number_of_building; 
            return true;
        }

        public bool SetCityName(string city_name)
        {
            this.city_name = city_name; 
            return true;
        }

        public bool SetCountryName(string country_name)
        {
            this.country_name = country_name;
            return true;
        }

        public string GetStreetName()
        {
            return this.street_name;
        }

        public int GetNumberOfBuilding()
        {
            return this.number_of_building;
        }

        public string GetCountryName()
        {
            return this.country_name;
        }

        public override bool Equals(object obj)
        {
            // check if the object is null or not of the correct type
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // casting the object to an Address
            Address other = (Address)obj;

            // compare the address's street name, number of building, city name, and country name
            return street_name == other.street_name &&
                   number_of_building == other.number_of_building &&
                   city_name == other.city_name &&
                   country_name == other.country_name;
        }

        public override string ToString()
        {
            return $"Street Name: {street_name}, Number of Building: {number_of_building}, City: {city_name}, Country: {country_name}";
        }


    }
    

}
