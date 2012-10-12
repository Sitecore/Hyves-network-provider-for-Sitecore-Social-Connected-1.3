// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
  /// Represents the different response fields for an user request.
	/// </summary>
	[Flags]
	public enum HyvesUserResponsefield : uint
	{
		/// <summary>
		/// All the response fields.
		/// </summary>
		[Description("All the response fields.")]
    All = 0,

		/// <summary>
		/// The city name
		/// </summary>
    [Description("cityname")]
    CityName = 1,

		/// <summary>
		/// The country name
    /// </summary>
    [Description("countryname")]
		CountryName = 2,

		/// <summary>
		/// Contains the data on the media that is the profilepicture for the user.
    /// </summary>
    [Description("profilepicture")]
		ProfilePicture = 4,

		/// <summary>
		/// List of all whitespaces for a user.
    /// </summary>
    [Description("whitespaces")]
		Whitespaces = 8,
				
		/// <summary>
		/// On My Mind
    /// </summary>
    [Description("onmymind")]
		OnMyMind = 16,

		/// <summary>
		/// Number of scraps
    /// </summary>
    [Description("scrapscount")]
		ScrapsCount = 32,

		/// <summary>
		/// Number of testimonials
    /// </summary>
    [Description("testimonialscount")]
		TestimonialsCount = 64,

		/// <summary>
		/// Number of respects
    /// </summary>
    [Description("respectscount")]
		RespectsCount = 128,

		/// <summary>
		/// Fancylayout tag, can be used in body fields in Hyves
    /// </summary>
    [Description("fancylayouttag")]
    FancylayoutTag = 256,

    /// <summary>
    /// Whether or not the profile is visible.
    /// </summary>
    [Description("profilevisible")]
    ProfileVisible = 512,

    /// <summary>
    /// Whether or not the scraps on an user profile are visible.
    /// </summary>
    [Description("scrapsvisible")]
    ScrapsVisible = 1024,

    /// <summary>
    /// The number of comments.
    /// </summary>
    [Description("commentscount")]
    CommentsCount = 2048,

    /// <summary>
    /// The tags.
    /// </summary>
    [Description("tags")]
    Tags = 4096,

    /// <summary>
    /// Geolocation data (latitude and longitude) of the item.
    /// </summary>
    [Description("geolocation")]
    GeoLocation = 8192,

    /// <summary>
    /// The number of views.
    /// </summary>
    [Description("viewscount")]
    ViewsCount = 16384,

    /// <summary>
    /// The type of user.
    /// </summary>
    [Description("usertypes")]
    UserTypes = 32768,

    /// <summary>
    /// Relation type of the user.
    /// </summary>
    [Description("relationtype")]
    RelationType = 65536,

    /// <summary>
    /// An about me field that is shown with the users profile information. 
    /// </summary>
    [Description("aboutme")]
    AboutMe = 131072,

    /// <summary>
    /// The link to the overview page for the item on the mobile webiste. 
    /// </summary>
    [Description("mobileurl")]
    MobileUrl = 262144,

    /// <summary>
    /// Whether the user is organ donor.
    /// </summary>
    [Description("organdonor")]
    OrganDonor = 524288
	}
}
