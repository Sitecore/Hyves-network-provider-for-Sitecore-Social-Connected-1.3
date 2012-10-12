// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents a Event.
	/// </summary>
	public sealed class Event : HyvesEntity
	{
    private bool startDateTransformed;
    private bool endDateTransformed;
    private bool inFutureTransformed;
    private bool usersCountTransformed;
    private bool visibilityTransformed;
    private bool createdTransformed;
    private bool viewsCountTransformed;

    public Event()
		{
		}

		/// <summary>
		/// The id of the event.
		/// </summary>
		public string EventId
		{
			get
			{
				return GetState<string>("eventid");
			}
		}

		/// <summary>
		/// The title of the event.
		/// </summary>
		public string Title
		{
			get
			{
				return GetState<string>("title") ?? string.Empty;
			}
		}

		/// <summary>
		/// The body of the event.
		/// </summary>
		public string Body
		{
			get
			{
				return GetState<string>("body") ?? string.Empty;
			}
		}

		/// <summary>
		/// The user Id of the owner of the event
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The url of the event.
		/// </summary>
		public string Url
		{
			get
			{
				return GetState<string>("url") ?? string.Empty;
			}
		}

    /// <summary>
    /// The start date of the event.
    /// </summary>
    public DateTime StartDate
    {
      get
      {
        if (this.startDateTransformed == false)
        {
          return TransformStartDate();
        }

        return (DateTime)this["startdate"];
      }
    }

    /// <summary>
    /// The end date of the event.
    /// </summary>
    public DateTime EndDate
    {
      get
      {
        if (this.endDateTransformed == false)
        {
          return TransformEndDate();
        }

        return (DateTime)this["enddate"];
      }
    }

    /// <summary>
    /// Whether this event is in the future.
    /// </summary>
    public bool InFuture
    {
      get
      {
        if (this.inFutureTransformed == false)
        {
          return TransformInFuture();
        }
        return (bool)this["infuture"];
      }
    }

    /// <summary>
    /// The number of user attending the event.
    /// </summary>
    public int UsersCount
    {
      get
      {
        if (this.usersCountTransformed == false)
        {
          return TransformUsersCount();
        }

        return (int)this["usercount"];
      }
    }

    /// <summary>
    /// The identifier of the citey where the event takes place.
    /// </summary>
    public string CityId
    {
      get
      {
        return GetState<string>("cityid") ?? string.Empty;
      }
    }

    /// <summary>
    /// The city tab identifier.
    /// </summary>
    public string CityTabId
    {
      get
      {
        return GetState<string>("citytabid") ?? string.Empty;
      }
    }

    /// <summary>
    /// The visibility of the event.
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
    /// The date the event was created.
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
    /// The number of view of the event.
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

    private DateTime TransformStartDate()
    {
      Debug.Assert(this.startDateTransformed == false);

      int timestamp = HyvesResponse.CoerceInt32(this["startdate"]);

      DateTime date = new DateTime(1970, 1, 1).AddSeconds(timestamp);
      this["startdate"] = date;

      this.startDateTransformed = true;

      return date;
    }

    private DateTime TransformEndDate()
    {
      Debug.Assert(this.endDateTransformed == false);

      int timestamp = HyvesResponse.CoerceInt32(this["enddate"]);

      DateTime date = new DateTime(1970, 1, 1).AddSeconds(timestamp);
      this["enddate"] = date;

      this.endDateTransformed = true;

      return date;
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

		private bool TransformInFuture()
		{
			Debug.Assert(this.inFutureTransformed == false);

      bool inFuture = HyvesResponse.CoerceBoolean(this["infuture"]);

      this["infuture"] = inFuture;

      this.inFutureTransformed = true;

      return inFuture;
		}

		private int TransformUsersCount()
		{
			Debug.Assert(this.usersCountTransformed == false);

      int count = HyvesResponse.CoerceInt32(this["userscount"]);

			this["userscount"] = count;

      this.usersCountTransformed = true;

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
