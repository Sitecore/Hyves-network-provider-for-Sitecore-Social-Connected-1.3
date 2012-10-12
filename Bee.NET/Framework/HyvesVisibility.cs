// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// The visibility of a Hyves entity
	/// </summary>
	public enum HyvesVisibility
	{
		/// <summary>
		/// Unspecified
    /// </summary>
    [Description("")]
		NotSpecified = 0,

		/// <summary>
		/// Visible only to the user himself/herself.
		/// </summary>
    [Description("private")]
		Private = 1,

		/// <summary>
		/// Visible only to the friends of the user.
    /// </summary>
    [Description("friend")]
		Friend = 2,

		/// <summary>
		/// Visible only to the friends of the user and their friends.
    /// </summary>
    [Description("friends_of_friends")]
		FriendsOfFriends = 3,

		/// <summary>
		/// Visible only to all logged in hyvers.
    /// </summary>
    [Description("public")]
		Public = 4,

		/// <summary>
		/// Visible only to all (whether logged in or not).
    /// </summary>
    [Description("superpublic")]
		SuperPublic = 5
	}
}
