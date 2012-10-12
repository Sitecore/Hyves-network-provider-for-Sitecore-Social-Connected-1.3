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
	/// Represents a Blog
	/// </summary>
	public sealed class Blog : HyvesEntity
	{
		private bool visibilityTransformed;
		private bool createdTransformed;
		private bool commentsCountTransformed;
    private bool respectsCountTransformed;
    private bool viewsCountTransformed;

    public Blog()
		{
		}

		/// <summary>
		/// The id of the blog.
		/// </summary>
		public string BlogId
		{
			get
			{
				return GetState<string>("blogid");
			}
		}

		/// <summary>
		/// The title of the blog.
		/// </summary>
		public string Title
		{
			get
			{
				return GetState<string>("title") ?? string.Empty;
			}
		}

		/// <summary>
		/// The body of the blog.
		/// </summary>
		public string Body
		{
			get
			{
				return GetState<string>("body") ?? string.Empty;
			}
		}

		/// <summary>
		/// The user Id of the owner of the blog
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The visibility of the blog.
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
		/// The url of the blog.
		/// </summary>
		public string Url
		{
			get
			{
				return GetState<string>("url") ?? string.Empty;
			}
		}

		/// <summary>
		/// The date the blog was created.
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
		/// The number of comments of the blog.
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
		/// The number of respects of the blog
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

		/// <summary>
		/// The tags of the blog.
		/// </summary>
		public string Tags
		{
			get
			{
				return GetState<string>("tags") ?? string.Empty;
			}
		}

    /// <summary>
    /// The location of the blog.
    /// </summary>
    public Geolocation Geolocation
    {
      get
      {
        return TransformEntity<Geolocation>((Hashtable)this["geolocation"]);
      }
    }

    /// <summary>
    /// The number of view of the blog.
    /// </summary>
    public int ViewsCount
    {
      get
      {
        if (viewsCountTransformed == false)
        {
          return TransformViewsCount();
        }
        return (int)this["viewscount"];
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

    private int TransformViewsCount()
    {
      Debug.Assert(viewsCountTransformed == false);

      int count = HyvesResponse.CoerceInt32(this["viewscount"]);

      this["viewscount"] = count;

      viewsCountTransformed = true;

      return count;
    }
	}
}
