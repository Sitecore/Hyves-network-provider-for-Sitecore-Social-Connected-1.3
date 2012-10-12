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
	/// Represents a Hyves private spot.
	/// </summary>
	public class PrivateSpot : HyvesEntity
	{
    public PrivateSpot()
		{
		}

		/// <summary>
		/// The ID of the privatespot.
		/// </summary>
		public string PrivateSpotId
		{
			get
			{
				return GetState<string>("privatespotid");
			}
		}

		/// <summary>
		/// The ID of the user.
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The "Where" of the privatespot.
		/// </summary>
		public string Where
		{
			get
			{
				return GetState<string>("where") ?? String.Empty;
			}
		}

		/// <summary>
		/// The location of the privatespot
		/// </summary>
		public Geolocation Geolocation
		{
			get
			{
				return base.TransformEntity<Geolocation>((Hashtable)this["geolocation"]);
			}
		}
	}
}
