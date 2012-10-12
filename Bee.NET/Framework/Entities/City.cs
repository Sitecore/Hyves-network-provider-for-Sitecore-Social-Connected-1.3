// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents a city
	/// </summary>
	public sealed class City : HyvesEntity
	{
    public City()
		{
		}

		/// <summary>
		/// The unique ID of the city
		/// </summary>
		public string CityID
		{
			get
			{
				return GetState<string>("cityid");
			}
		}

		/// <summary>
		/// The unique ID of the region
		/// </summary>
		public string RegionID
		{
			get
			{
				return GetState<string>("regionid");
			}
		}

		/// <summary>
		/// The unique ID of the country
		/// </summary>
		public string CountryID
		{
			get
			{
				return GetState<string>("countryid");
			}
		}

		/// <summary>
		/// The name of the city
		/// </summary>
		public string Name
		{
			get
			{
				return GetState<string>("name") ?? string.Empty;
			}
		}

		/// <summary>
		/// The id of the nearest city that has a city tab. If this is equal 
		/// to the cityid, the city is a city-tab. Only citytabs have urls.
		/// </summary>
		public string CitytabID
		{
			get
			{
				return GetState<string>("citytabid");
			}
		}

		/// <summary>
		/// The url of the city
		/// </summary>
		public string Url
		{
			get
			{
				return GetState<string>("url") ?? string.Empty;
			}
		}
	}
}
