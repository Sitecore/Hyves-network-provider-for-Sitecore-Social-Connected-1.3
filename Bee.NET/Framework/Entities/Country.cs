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
	/// Represents a country
	/// </summary>
	public sealed class Country : HyvesEntity
	{
    public Country()
		{
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
		/// The name of the country
		/// </summary>
		public string Name
		{
			get
			{
				return GetState<string>("name") ?? string.Empty;
			}
		}
	}
}
