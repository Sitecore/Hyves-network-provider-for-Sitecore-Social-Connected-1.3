// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
  /// Represents the different response fields for a private spot request.
	/// </summary>
	[Flags]
  public enum HyvesPrivateSpotResponsefield : uint
	{
		/// <summary>
    /// All the response fields.
    /// </summary>
    [Description("All the response fields.")]
		All = 0,

		/// <summary>
    /// Geolocation data (latitude and longitude) of the item. 
    /// </summary>
    [Description("geolocation")]
		Geolocation = 1
	}
}
