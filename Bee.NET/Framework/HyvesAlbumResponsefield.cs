// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the different response fields for an album request.
	/// </summary>
	[Flags]
	public enum HyvesAlbumResponsefield : uint
	{
		/// <summary>
		/// All the response fields.
		/// </summary>
		[Description("All the response fields.")]
    All = 0,

		/// <summary>
		/// The mobile url
		/// </summary>
    [Description("mobileurl")]
    MobileUrl = 1
	}
}
