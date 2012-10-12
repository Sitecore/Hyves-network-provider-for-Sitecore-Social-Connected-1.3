// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the different sort orders. 
	/// </summary>
  public enum HyvesSortOrder : int
	{
		/// <summary>
    /// Sort the results in ascending order
		/// </summary>
		[Description("asc")]
    Ascending = 0,

		/// <summary>
    /// Sort the results in descending order.
    /// </summary>
    [Description("desc")]
    Descending = 1
	}
}
