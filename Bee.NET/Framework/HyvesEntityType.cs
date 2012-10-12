// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;

// TODO: Sort
namespace Hyves.Service
{
	/// <summary>
	/// The type of the entity.
	/// </summary>
  public enum HyvesEntityType
	{
		/// <summary>
		/// Unspecified
		/// </summary>
		NotSpecified = 0,

		/// <summary>
		/// A gadget.
		/// </summary>
		Gadget = 1,

		/// <summary>
		/// A blog.
		/// </summary>
    Blog = 2,

    /// <summary>
    /// A www.
    /// </summary>
    Www = 3,

    /// <summary>
    /// A ping.
    /// </summary>
    Ping = 4,

    /// <summary>
    /// A song.
    /// </summary>
    Song = 5,

    /// <summary>
    /// A song.
    /// </summary>
    User = 6,

    /// <summary>
    /// A group.
    /// </summary>
    Group = 7
	}
}
