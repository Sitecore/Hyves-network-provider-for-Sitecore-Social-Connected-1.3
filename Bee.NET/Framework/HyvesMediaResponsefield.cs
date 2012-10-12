// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
  /// Represents the different response fields for an media request.
	/// </summary>
	[Flags]
	public enum HyvesMediaResponsefield : uint
	{
		/// <summary>
    /// All the response fields.
		/// </summary>
		All = 0,

		/// <summary>
		/// Number of comments
		/// </summary>
		[Description("commentscount")]
    CommentsCount = 1,

		/// <summary>
		/// Number of respects
    /// </summary>
    [Description("respectscount")]
		RespectsCount = 2,

		/// <summary>
		/// Tags of the post
    /// </summary>
    [Description("tags")]
		Tags = 4,

		/// <summary>
		/// Fancylayout tag, can be used in body fields in Hyves
    /// </summary>
    [Description("fancylayouttag")]
    FancylayoutTag = 8,

    /// <summary>
    /// The geo location.
    /// </summary>
    [Description("geolocation")]
    GeoLocation = 16,

    /// <summary>
    /// The number of views.
    /// </summary>
    [Description("viewscount")]
    ViewsCount = 32,

    /// <summary>
    /// The link to the overview page for the item on the mobile webiste.
    /// </summary>
    [Description("mobileurl")]
    MobileUrl = 64
	}
}
