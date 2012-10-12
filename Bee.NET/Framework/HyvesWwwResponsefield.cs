// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
  /// Represents the different response fields for an www request.
	/// </summary>
	[Flags]
	public enum HyvesWwwResponsefield : uint
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
    GeoLocation = 1,

    /// <summary>
    /// Number of comments.
    /// </summary>
    [Description("commentscount")]
    CommentsCount = 2,

    /// <summary>
    /// Number of respects.
    /// </summary>
    [Description("respectscount")]
    RespectsCount = 4,

    /// <summary>
    /// Number of views.
    /// </summary>
    [Description("viewscount")]
    ViewsCount = 8,

		/// <summary>
		/// The mobile url
		/// </summary>
    [Description("mobileurl")]
    MobileUrl = 16
	}
}
