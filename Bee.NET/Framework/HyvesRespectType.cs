// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// The type of the respect.
	/// </summary>
	public enum HyvesRespectType
	{
		/// <summary>
		/// Unspecified
		/// </summary>
		NotSpecified = 0,

		/// <summary>
		/// Respect with reference to the creator.
		/// </summary>
    [Description("withprofile")]
    WithProfile = 1,

		/// <summary>
		/// Anonymous respect.
		/// </summary>
    [Description("anonymous")]
    Anonymous = 2
	}
}
