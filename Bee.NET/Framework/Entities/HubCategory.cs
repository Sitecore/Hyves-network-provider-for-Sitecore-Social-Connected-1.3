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
	/// Represents a Hyves hub category.
	/// </summary>
	public sealed class HubCategory : HyvesEntity
  {
    #region Private members
    private bool hubsCountTransformed;
    #endregion

    #region Contructors
    public HubCategory()
		{
    }
    #endregion

    #region Public properties
    /// <summary>
		/// The unique ID of the hub.
		/// </summary>
    public string HubId { get { return GetState<string>("hubcategoryid"); } }

    /// <summary>
    /// The name of the hub category.
    /// </summary>
    public string Name
    {
      get
      {
        return GetState<string>("name") ?? string.Empty;
      }
    }


		/// <summary>
		/// The hub type.
		/// </summary>
    public string HubType { get { return GetState<string>("hubtype") ?? string.Empty; } }
    
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
    /// The number of hub in this hub category.
    /// </summary>
    public int HubsCount
    {
      get
      {
        if (this.hubsCountTransformed == false)
        {
          return TransformHubsCount();
        }

        return (int)this["hubscount"];
      }
    }
    #endregion

    #region Private methods
		private int TransformHubsCount()
		{
			Debug.Assert(this.hubsCountTransformed == false);

      int count = HyvesResponse.CoerceInt32(this["hubscount"]);

      this["hubscount"] = count;

      this.hubsCountTransformed = true;

			return count;
		}
    #endregion
  }
}
