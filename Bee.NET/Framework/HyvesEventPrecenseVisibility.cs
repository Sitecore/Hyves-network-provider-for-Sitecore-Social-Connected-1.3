// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// The visibility of a Hyves event presence
	/// </summary>
	public enum HyvesEventPrecenseVisibility
	{
		/// <summary>
    /// Default visibility, as presented to Hyver on website
    /// </summary>
    [Description("default")]
		Default = 0,

		/// <summary>
		/// Visible only to the user himself/herself.
		/// </summary>
    [Description("private")]
		Private = 1,

		/// <summary>
		/// Visible only to all logged in hyvers.
    /// </summary>
    [Description("public")]
		Public = 2
	}
}
