// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
  /// Represents the different response fields for an thread request.
	/// </summary>
	[Flags]
	public enum HyvesThreadResponsefield : uint
	{
		/// <summary>
    /// All the response fields.
    /// </summary>
    [Description("All the response fields.")]
		All = 0,

		/// <summary>
    /// The comments count for a thread. 
    /// </summary>
    [Description("commentscount")]
    CommentsCount = 1
	}
}
