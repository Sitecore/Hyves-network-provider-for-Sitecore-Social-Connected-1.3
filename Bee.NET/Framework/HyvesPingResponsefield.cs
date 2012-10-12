// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
  /// Represents the different response fields for an ping request.
	/// </summary>
	[Flags]
	public enum HyvesPingResponsefield : uint
	{
		/// <summary>
    /// All the response fields.
    /// </summary>
    [Description("All the response fields.")]
		All = 0,

		/// <summary>
		/// Show the boddy of the ping
    /// </summary>
    [Description("displaybody")]
		DisplayBody = 1
	}
}
