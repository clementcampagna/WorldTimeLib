using System;
using System.Json;
using System.Net;

namespace WorldTimeLib
{
	// A short library written by Clément Campagna to retrieve the current date and time of any city or country in the world based on their name or latitude and longitude coordinates.
	// It relies on the OpenCageData and Geonames APIs to gather the information.
	public class WorldTime
	{
		// Method used to get latitude, longitude and location found based on a location's name supplied
		public string[] GetLatLongBasedOnLoc(string location, string openCageApiKey)
		{
			double latitudeFound, longitudeFound;
			string locationFound;

			using (WebClient wc = new WebClient())
			{
				var jsonString = wc.DownloadString("https://api.opencagedata.com/geocode/v1/json?q=" + location + "&key=" + openCageApiKey + "&language=en&pretty=1");

				// Verify your JSON if you get any errors here
				var jObj = (JsonObject)JsonValue.Parse(jsonString);
				
				latitudeFound = jObj["results"][0]["geometry"]["lat"];
				longitudeFound = jObj["results"][0]["geometry"]["lng"];
				locationFound = jObj["results"][0]["formatted"];
			}

			var latLongLocFound = new string[3];
			latLongLocFound[0] = latitudeFound.ToString();
			latLongLocFound[1] = longitudeFound.ToString();
			latLongLocFound[2] = locationFound;

			return latLongLocFound;
		}

		// Method used to get the current date and time of a location based on its latitude and longitude
		public DateTime GetCurrentDateTimeBasedOnLatLong(double latitude, double longitude, string geonamesUsername)
		{
			string dateTimeString = "";

			using (WebClient wc = new WebClient())
			{
				var jsonString = wc.DownloadString("http://api.geonames.org/timezoneJSON?lat=" + latitude + "&lng=" + longitude + "&username=" + geonamesUsername);

				// Verify your JSON if you get any errors here
				JsonValue json = JsonValue.Parse(jsonString);

				dateTimeString = json["time"];
			}

			return DateTime.Parse(dateTimeString);
		}
	}
}
