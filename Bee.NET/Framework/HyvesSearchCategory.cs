// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
  /// Categories to search in.
	/// </summary>
  [Flags]
  public enum HyvesSearchCategory : uint
	{
		/// <summary>
		/// Search in all the categories
		/// </summary>
		All = 0,

		/// <summary>
    /// Search in friends.
		/// </summary>
    [Description("friends")]
    Friends = 1,

    /// <summary>
    /// Search in hubs.
    /// </summary>
    [Description("ownhub")]
    OwnHub = 2,

    /// <summary>
    /// Search in gadgets.
    /// </summary>
    [Description("gadgets")]
    Gadgets = 4,

    /// <summary>
    /// Search in albums.
    /// </summary>
    [Description("albums")]
    Albums = 8,

    /// <summary>
    /// Search in famous.
    /// </summary>
    [Description("famous")]
    Famous = 16
	}
}
