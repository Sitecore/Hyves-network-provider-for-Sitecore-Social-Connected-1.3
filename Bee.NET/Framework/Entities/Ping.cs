// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents a Ping
	/// </summary>
	public sealed class Ping : HyvesEntity
	{
		private bool visibilityTransformed;
		private bool createdTransformed;

    public Ping()
		{
		}

		/// <summary>
		/// The id of the ping.
		/// </summary>
		public string PingID
		{
			get
			{
				return GetState<string>("pingid");
			}
		}

		/// <summary>
		/// The body of the ping.
		/// </summary>
		public string Body
		{
			get
			{
				return GetState<string>("body") ?? string.Empty;
			}
		}

		/// <summary>
		/// The user Id of the owner of the ping
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The user Id of the Ping
		/// </summary>
		public string TargetUserId
		{
			get
			{
				return GetState<string>("target_userid");
			}
		}

		/// <summary>
		/// The visibility of the ping.
		/// </summary>
		public HyvesVisibility Visibility
		{
			get
			{
				if (visibilityTransformed == false)
				{
					return TransformVisibility();
				}
				return (HyvesVisibility)this["visibility"];
			}
		}

		/// <summary>
		/// The date the ping was created.
		/// </summary>
		public DateTime Created
		{
			get
			{
				if (createdTransformed == false)
				{
					return TransformCreated();
				}
				return (DateTime)this["created"];
			}
		}

		/// <summary>
		/// The body of the ping.
		/// </summary>
		public string DisplayBody
		{
			get
			{
				return GetState<string>("displaybody") ?? string.Empty;
			}
		}

		private HyvesVisibility TransformVisibility()
		{
			Debug.Assert(visibilityTransformed == false);

			HyvesVisibility visibility = HyvesVisibility.NotSpecified;
			string state = GetState<string>("visibility") ?? String.Empty;

			if (state.Length != 0)
			{
				if (state.Equals("private"))
				{
					visibility = HyvesVisibility.Private;
				}
				else if (state.Equals("friend"))
				{
					visibility = HyvesVisibility.Friend;
				}
				else if (state.Equals("friends_of_friends"))
				{
					visibility = HyvesVisibility.FriendsOfFriends;
				}
				else if (state.Equals("public"))
				{
					visibility = HyvesVisibility.Public;
				}
				else if (state.Equals("superpublic"))
				{
					visibility = HyvesVisibility.SuperPublic;
				}
			}

			this["visibility"] = visibility;
			visibilityTransformed = true;

			return visibility;
		}

		private DateTime TransformCreated()
		{
			Debug.Assert(createdTransformed == false);

			int timestamp = HyvesResponse.CoerceInt32(this["created"]);

			DateTime date = new DateTime(1970, 1, 1).AddSeconds(timestamp);
			this["created"] = date;

			createdTransformed = true;

			return date;
		}
	}
}
