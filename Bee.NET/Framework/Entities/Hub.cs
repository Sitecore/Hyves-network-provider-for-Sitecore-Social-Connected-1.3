// Copyright (c) 2010, Beemway. All Rights Reserved.

using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents a Hyves hub.
	/// </summary>
	public sealed class Hub : HyvesEntity
  {
    #region Private members
    private bool hubVisibleTransformed;
    private bool usersCountTransformed;
    private bool scrapsCountTransformed;
    private bool isMemberOfTransformed;
    #endregion

    #region Contructors
    public Hub()
		{
    }
    #endregion

    #region Public properties
    /// <summary>
		/// The unique ID of the hub.
		/// </summary>
		public string HubId { get { return GetState<string>("hubid"); } }

    /// <summary>
    /// Whether the hub is visible or not.
    /// </summary>
    public bool HubVisible
    {
      get
      {
        if (hubVisibleTransformed == false)
        {
          return TransformHubVisible();
        }

        return (bool)this["hubvisible"];
      }
    }    

		/// <summary>
		/// The hub type.
		/// </summary>
    public string HubType { get { return GetState<string>("hubtype") ?? string.Empty; } }

		/// <summary>
		/// The title of the hub.
		/// </summary>
		public string Title
		{
			get
			{
				return GetState<string>("title") ?? string.Empty;
			}
		}

		/// <summary>
    /// The description of the hub.
		/// </summary>
    public string Description
		{
			get
			{
        return GetState<string>("description") ?? string.Empty;
			}
    }

    /// <summary>
    /// The media ID of the profile picture.
    /// </summary>
    public string MediaId
    {
      get
      {
        return GetState<string>("mediaid") ?? string.Empty;
      }
    }

    /// <summary>
    /// The ID of the hub category.
    /// </summary>
    public string HubCategoryID
    {
      get
      {
        return GetState<string>("hubcategoryid") ?? string.Empty;
      }
    }

    /// <summary>
    /// The number of users of the hub.
    /// </summary>
    public int UsersCount
    {
      get
      {
        if (usersCountTransformed == false)
        {
          return TransformUsersCount();
        }

        return (int)this["userscount"];
      }
    }

    /// <summary>
    /// The url of the hub.
    /// </summary>
    public string Url
    {
      get
      {
        return GetState<string>("url") ?? string.Empty;
      }
    }

    /// <summary>
    /// The mobile url of the hub.
    /// </summary>
    public string MobileUrl
    {
      get
      {
        return GetState<string>("mobileurl") ?? string.Empty;
      }
    }

    /// <summary>
    /// The profile picture of the hub.
    /// </summary>
    public Media ProfilePicture
    {
      get
      {
        return TransformEntity<Media>((Hashtable)this["profilepicture"]);
      }
    }

		/// <summary>
		/// The number of scraps of the hub.
		/// </summary>
		public int ScrapsCount
		{
			get
			{
				if (scrapsCountTransformed == false)
				{
					return TransformScrapsCount();
				}

        return (int)this["scrapscount"];
			}
    }

    /// <summary>
    /// The geolocation of the hub.
    /// </summary>
    public Geolocation Geolocation
    {
      get
      {
        return TransformEntity<Geolocation>((Hashtable)this["geolocation"]);
      }
    }

    /// <summary>
    /// The address of the hub.
    /// </summary>
    public HubAddress HubAddress
    {
      get
      {
        return TransformEntity<HubAddress>((Hashtable)this["hubaddress"]);
      }
    }

    /// <summary>
    /// The opening hours of the hub.
    /// </summary>
    public Collection<OpeningHour> HubOpeningHours
    {
      get
      {
        ArrayList list = (ArrayList)this["hubopeninghours"];

        Collection<OpeningHour> openingHours = new Collection<OpeningHour>();
        if (list != null)
        {
          for (int i = 0; i < list.Count; i++)
          {
            openingHours.Add(TransformEntity<OpeningHour>((Hashtable)list[i]));
          }
        }

        return openingHours;
      }
    }

    /// <summary>
    /// The moderators of the hub.
    /// </summary>
    //TODO: Implement
    public string Moderators
    {
      get
      {
        return string.Empty;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsMemberOf
    {
      get
      {
        if (this.isMemberOfTransformed == false)
        {
          return TransformIsMemberOf();
        }

        return (bool)this["ismemberof"];
      }
    }
#endregion

    #region Private methods
    private bool TransformHubVisible()
		{
			Debug.Assert(hubVisibleTransformed == false);

			bool hubVisible = HyvesResponse.CoerceBoolean(this["hubvisible"]);

			this["hubvisible"] = hubVisible;

			hubVisibleTransformed = true;

			return hubVisible;
    }

		private int TransformUsersCount()
		{
			Debug.Assert(usersCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["userscount"]);

			this["userscount"] = count;

			usersCountTransformed = true;

			return count;
		}

		private int TransformScrapsCount()
		{
			Debug.Assert(scrapsCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["scrapscount"]);

			this["scrapscount"] = count;

			scrapsCountTransformed = true;

			return count;
    }

    private bool TransformIsMemberOf()
    {
      Debug.Assert(this.isMemberOfTransformed == false);

      bool value = HyvesResponse.CoerceBoolean(this["ismemberof"]);

      this["ismemberof"] = value;

      this.isMemberOfTransformed = true;

      return value;
    }
    #endregion
  }
}
