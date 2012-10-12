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
	/// Represents a Hyves gadget.
	/// </summary>
	public sealed class Gadget : HyvesEntity
	{
		private bool mayCopyTransformed;
		private bool visibilityTransformed;
		private bool createdTransformed;
		private bool commentsCountTransformed;
		private bool respectsCountTransformed;

    public Gadget()
		{
		}

		/// <summary>
		/// The unique ID of the gadget.
		/// </summary>
		public string GadgetID
		{
			get
			{
				return GetState<string>("gadgetid");
			}
		}

		/// <summary>
		/// The user Id of the owner of the gadget
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The title of the gadget.
		/// </summary>
		public string Title
		{
			get
			{
				return GetState<string>("title") ?? string.Empty;
			}
		}

		/// <summary>
		/// The html of the gadget.
		/// </summary>
		public string Html
		{
			get
			{
				return GetState<string>("html") ?? string.Empty;
			}
    }

    /// <summary>
    /// The url where the gadget spec for the gadget can be found.
    /// </summary>
    public string SpecUrl
    {
      get
      {
        return GetState<string>("specurl") ?? string.Empty;
      }
    }

		/// <summary>
		/// Allow to copy this gadget
		/// </summary>
		public bool MayCopy
		{
			get
			{
				if (mayCopyTransformed == false)
				{
					return TransformMayCopy();
				}
				return (bool)this["maycopy"];
			}
		}

		/// <summary>
		/// The url of the gadget.
		/// </summary>
		public string Url
		{
			get
			{
				return GetState<string>("url") ?? string.Empty;
			}
		}

		/// <summary>
		/// The visibility of the gadget
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
		/// The date the gadget was created.
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
		/// The number of comments of the user
		/// </summary>
		public int CommentsCount
		{
			get
			{
				if (commentsCountTransformed == false)
				{
					return TransformCommentsCount();
				}
				return (int)this["commentscount"];
			}
		}

		/// <summary>
		/// The number of respects of the user
		/// </summary>
		public int RespectsCount
		{
			get
			{
				if (respectsCountTransformed == false)
				{
					return TransformRespectsCount();
				}
				return (int)this["respectscount"];
			}
		}

		private bool TransformMayCopy()
		{
			Debug.Assert(mayCopyTransformed == false);

			bool mayCopy = HyvesResponse.CoerceBoolean(this["maycopy"]);

			this["maycopy"] = mayCopy;

			mayCopyTransformed = true;

			return mayCopy;
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

		private int TransformCommentsCount()
		{
			Debug.Assert(commentsCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["commentscount"]);

			this["commentscount"] = count;

			commentsCountTransformed = true;

			return count;
		}

		private int TransformRespectsCount()
		{
			Debug.Assert(respectsCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["respectscount"]);

			this["respectscount"] = count;

			respectsCountTransformed = true;

			return count;
		}
	}
}
