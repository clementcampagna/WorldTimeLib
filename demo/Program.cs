using System;
using WorldTimeLib;

namespace demo
{
	class Program
	{
		// Get your OpenCageData API key for free by registering at https://opencagedata.com/users/sign_up
		private const string OPENCAGE_API_KEY = "YOUR_OPENCAGE_API_KEY";

		// Get your Geonames username for free by registering at https://www.geonames.org/login
		private const string GEONAMES_USERNAME = "YOUR_GEONAMES_USERNAME";

		static void Main(string[] args)
		{
			string location = "";

			while (true)
			{
				Console.WriteLine("Please enter a location (e.g. Paris, France) or 'q' to quit:");
				location = Console.ReadLine();

				if (location != "q")
				{
					if (!String.IsNullOrEmpty(location))
					{
						try
						{
							// Create a new object of type WorldTime (notice the use of WorldTimeLib at the top)
							WorldTime cityTime = new WorldTime();

							// Declare and initialize a string array that will hold the latitude, longitude and location found based on the location's string supplied above
							var latLongLocFound = new string[3];

							// Call WorldTimeLib's GetLatLongBasedOnLoc function to gather the data
							latLongLocFound = cityTime.GetLatLongBasedOnLoc(location, OPENCAGE_API_KEY);

							// The current date and time info will be returned by WorldTimeLib's GetCurrentDateTimeBasedOnLatLong function
							Console.WriteLine("Current date and time in " + latLongLocFound[2] + ": " + cityTime.GetCurrentDateTimeBasedOnLatLong(double.Parse(latLongLocFound[0]), double.Parse(latLongLocFound[1]), GEONAMES_USERNAME) + "\n");
						}
						catch (Exception ex)
						{
							Console.WriteLine($"The following exception occurred: {ex.Message}\n");
						}
					}
				}
				else
					break;
			}
		}
	}
}
