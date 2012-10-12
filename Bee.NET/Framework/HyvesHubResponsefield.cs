// Copyright (c) 2009 - 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
  /// Represents the different response fields for an hub request.
	/// </summary>
	[Flags]
  public enum HyvesHubResponsefield : uint
	{
		/// <summary>
    /// All the response fields.
    /// </summary>
    [Description("All the response fields.")]
		All = 0,

		/// <summary>
		/// The profile picture.
    /// </summary>
    [Description("profilepicture")]
    ProfilePicture = 1,

		/// <summary>
		/// Number of scraps.
    /// </summary>
    [Description("scrapscount")]
    ScrapsCount = 2,

    /// <summary>
    /// The geo location.
    /// </summary>
    [Description("geolocation")]
    GeoLocation = 4,

    /// <summary>
    /// The hub address.
    /// </summary>
    [Description("hubaddress")]
    HubAddress = 8,

    /// <summary>
    /// The hub opening hours.
    /// </summary>
    [Description("hubopeninghours")]
    HubOpeningHours = 16,

    /// <summary>
    /// The moderators.
    /// </summary>
    [Description("moderators")]
    Moderators = 32,

    /// <summary>
    /// The mobile url.
    /// </summary>
    [Description("mobileurl")]
    MobileUrl = 64,

    /// <summary>
    /// 
    /// </summary>
    [Description("ismemberof")]
    IsMemberOf = 128,

    /// <summary>
    /// The hub categories.
    /// </summary>
    [Description("hubcategories")]
    HubCategories = 256,

    /// <summary>
    /// The postable items.
    /// </summary>
    [Description("postableitems")]
    PostableItems = 512
	}
}
