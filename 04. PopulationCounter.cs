using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class PopulationCounter
{
    static void Main()
    {
        Regex splitPattern = new Regex(@"\|");
        Dictionary<string, Dictionary<string, long>> countries = new Dictionary<string, Dictionary<string, long>>();
        string command = Console.ReadLine();
        while (command != "report")
        {
            string[] tokens = splitPattern.Split(command);
            string city = tokens[0];
            string country = tokens[1];
            int population = int.Parse(tokens[2]);

            if (!countries.ContainsKey(country))
            {
                countries.Add(country, new Dictionary<string, long>());
            }

            countries[country].Add(city, population);

            command = Console.ReadLine();
        }

        var orderCountriesData = countries
            .OrderByDescending(x => x.Value.Values.Sum());

        foreach (var country in orderCountriesData)
        {
            Console.WriteLine("{0} (total population: {1})", country.Key, country.Value.Values.Sum());
            var orderCountryData = country.Value
                .OrderByDescending(x => x.Value);
            foreach (var city in orderCountryData)
            {
                Console.WriteLine("=>{0}: {1}", city.Key, city.Value);
            }
        }
    }
}
