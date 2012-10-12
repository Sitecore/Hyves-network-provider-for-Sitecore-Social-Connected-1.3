// Copyright (c) 2009 - 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the different sort type for users. 
	/// </summary>
  public enum HyvesUserSortType : int
	{
		/// <summary>
		/// Unknown sort type.
		/// </summary>
		[Description("")]
    Unknown = 0,

		/// <summary>
		/// Sort the results alphabetically.
    /// </summary>
    [Description("alphabetically")]
    Alphabetically = 1,

		/// <summary>
    /// Sort the results last login.
    /// </summary>
    [Description("lastlogin")]
    LastLogin = 2
	}
}
