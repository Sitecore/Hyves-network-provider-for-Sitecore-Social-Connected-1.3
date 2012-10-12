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
	/// Represents a region
	/// </summary>
	public sealed class Region : HyvesEntity
	{
    public Region()
		{
		}

		/// <summary>
		/// The id of the region
		/// </summary>
		public string RegionId
		{
			get
			{
				return GetState<string>("regionid");
			}
		}

		/// <summary>
		/// The unique ID of the region
		/// </summary>
		public string CountryID
		{
			get
			{
				return GetState<string>("countryid");
			}
		}

		/// <summary>
		/// The name of the region
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
