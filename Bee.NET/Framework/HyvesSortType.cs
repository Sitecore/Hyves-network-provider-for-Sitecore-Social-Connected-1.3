// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// The sort type.
	/// </summary>
	public enum HyvesSortType
	{
		/// <summary>
		/// Unspecified
		/// </summary>
		NotSpecified = 0,

		/// <summary>
    /// Sort by featured.
		/// </summary>
    [Description("featured")]
    Featured = 1,

		/// <summary>
    /// Sort by most views.
		/// </summary>
    [Description("mostview")]
    MostView = 2,

    /// <summary>
    /// Sort by most recent. 
    /// </summary>
    [Description("mostrecent")]
    MostRecent = 3,

    /// <summary>
    /// Sort by most reactions.
    /// </summary>
    [Description("mostreaction")]
    MostReaction = 4,

    /// <summary>
    /// Sort by most respects. 
    /// </summary>
    [Description("mostrespect")]
    MostRespect = 5
	}
}
