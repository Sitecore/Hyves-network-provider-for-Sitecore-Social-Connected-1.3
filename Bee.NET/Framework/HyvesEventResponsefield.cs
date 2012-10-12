// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// Represents 
	/// </summary>
	[Flags]
	public enum HyvesEventResponsefield : uint
	{
		/// <summary>
    /// All the response fields.
    /// </summary>
    [Description("All the response fields.")]
		All = 0,

		/// <summary>
		/// Number of views.
    /// </summary>
    [Description("viewscount")]
		ViewsCount = 1
	}
}
