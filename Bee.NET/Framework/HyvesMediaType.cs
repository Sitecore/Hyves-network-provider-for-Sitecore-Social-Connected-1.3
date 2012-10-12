// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the different sort of media. 
	/// </summary>
  public enum HyvesMediaType : int
	{
		/// <summary>
		/// Unknown sort type.
		/// </summary>
		[Description("")]
    Unknown = 0,

		/// <summary>
		/// A image.
    /// </summary>
    [Description("image")]
    Image = 1,

		/// <summary>
    /// A video.
    /// </summary>
    [Description("video")]
    Video = 2,

    /// <summary>
    /// A document.
    /// </summary>
    [Description("document")]
    Document = 3
	}
}
