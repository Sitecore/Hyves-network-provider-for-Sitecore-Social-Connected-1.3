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
	/// Represents the Who What Where of a user
	/// </summary>
	public sealed class Www : HyvesEntity
	{
		private bool visibilityTransformed;
		private bool createdTransformed;

    public Www()
		{
		}

		/// <summary>
		/// The id of the www.
		/// </summary>
		public string WwwId
		{
			get
			{
				return GetState<string>("wwwid");
			}
		}

		/// <summary>
		/// The "what" part of the www.
		/// </summary>
		public string Emotion
		{
			get
			{
				return GetState<string>("emotion") ?? string.Empty;
			}
		}

		/// <summary>
		/// The "where" part of the www.
		/// </summary>
		public string Where
		{
			get
			{
				return GetState<string>("where") ?? string.Empty;
			}
		}

		/// <summary>
		/// The user Id of the owner of the www
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The visibility of the www.
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
		/// The date the www was created.
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

		private HyvesVisibility TransformVisibility()
		{
      Debug.Assert(this.visibilityTransformed == false);

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
      this.visibilityTransformed = true;

			return visibility;
		}

		private DateTime TransformCreated()
		{
			Debug.Assert(createdTransformed == false);

			int timestamp = HyvesResponse.CoerceInt32(this["created"]);

			DateTime date = new DateTime(1970, 1, 1).AddSeconds(timestamp);
			this["created"] = date;

			this.createdTransformed = true;

			return date;
		}
	}
}
