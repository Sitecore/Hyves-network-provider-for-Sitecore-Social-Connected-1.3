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
	/// Represents a Hyves album.
	/// </summary>
	public sealed class Album : HyvesEntity
	{
		private bool mediaCountTransformed;
    private bool visibilityTransformed;
    private bool printabilityTransformed;

		public Album()
		{
		}

		/// <summary>
		/// The unique ID of the album.
		/// </summary>
		public string AlbumID
		{
			get
			{
				return GetState<string>("albumid");
			}
		}

		/// <summary>
		/// The title of the album.
		/// </summary>
		public string Title
		{
			get
			{
				return GetState<string>("title") ?? string.Empty;
			}
		}

		/// <summary>
		/// The number of media elements in the album
		/// </summary>
		public int MediaCount
		{
			get
			{
				if (mediaCountTransformed == false)
				{
					return TransformMediaCount();
				}
				return GetState<int>("mediacount");
			}
		}

		/// <summary>
		/// The user Id of the owner of the album
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid"); 
			}
		}

    /// <summary>
    /// The hub Id of the hub containing this album.
    /// </summary>
    public string HubId
    {
      get
      {
        return GetState<string>("hubid") ?? string.Empty;
      }
    }

		/// <summary>
		/// The url of the album.
		/// </summary>
		public string Url
		{
			get
			{
				return GetState<string>("url") ?? string.Empty;
			}
    }

    /// <summary>
    /// The mobile url of the album.
    /// </summary>
    public string MobileUrl
    {
      get
      {
        return GetState<string>("mobileurl") ?? string.Empty;
      }
    }

		/// <summary>
		/// The visibility of the album
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
    /// The visibility of the album
    /// </summary>
    public HyvesVisibility Printability
    {
      get
      {
        if (visibilityTransformed == false)
        {
          return TransformPrintability();
        }

        return (HyvesVisibility)this["printability"];
      }
    }

		private int TransformMediaCount()
		{
			Debug.Assert(mediaCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["mediacount"]);

			this["mediacount"] = count;
			mediaCountTransformed = true;

			return count;
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

    private HyvesVisibility TransformPrintability()
    {
      Debug.Assert(printabilityTransformed == false);

      HyvesVisibility printability = HyvesVisibility.NotSpecified;
      string state = GetState<string>("printability") ?? String.Empty;

      if (state.Length != 0)
      {
        if (state.Equals("private"))
        {
          printability = HyvesVisibility.Private;
        }
        else if (state.Equals("friend"))
        {
          printability = HyvesVisibility.Friend;
        }
        else if (state.Equals("friends_of_friends"))
        {
          printability = HyvesVisibility.FriendsOfFriends;
        }
        else if (state.Equals("public"))
        {
          printability = HyvesVisibility.Public;
        }
        else if (state.Equals("superpublic"))
        {
          printability = HyvesVisibility.SuperPublic;
        }
      }

      this["printability"] = printability;
      printabilityTransformed = true;

      return printability;
    }
	}
}
