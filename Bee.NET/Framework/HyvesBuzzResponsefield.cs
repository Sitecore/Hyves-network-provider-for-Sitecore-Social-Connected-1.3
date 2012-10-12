// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// Represents 
	/// </summary>
	[Flags]
  public enum HyvesBuzzResponsefield : uint
	{
		/// <summary>
    /// All the response fields.
    /// </summary>
    [Description("All the response fields.")]
		All = 0,

		/// <summary>
		/// Number of comments.
		/// </summary>
		CommentsCount = 1,

		/// <summary>
		/// Number of respects.
		/// </summary>
    RespectsCount = 2,

    /// <summary>
    /// The link to the overview page for the item on the mobile webiste.
    /// </summary>
    MobileUrl = 4
	}
}
