// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// The sort type.
	/// </summary>
  public enum HyvesTimeSpan
	{
		/// <summary>
		/// Unspecified
		/// </summary>
		NotSpecified = 0,

		/// <summary>
    /// One day. 
		/// </summary>
    [Description("day")]
    Day = 1,

		/// <summary>
    /// One week. 
    /// </summary>
    [Description("week")]
    Week = 2,

    /// <summary>
    /// One month. 
    /// </summary>
    [Description("month")]
    Month = 3,

    /// <summary>
    /// Forever (alltime).
    /// </summary>
    [Description("forever")]
    Forever = 4
	}
}
