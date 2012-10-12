// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the service APIs that allow access to information on Hyves hubs.
	/// </summary>
	public sealed class HubsService
	{
		private HyvesSession session;

		internal HubsService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		#region GetHub
		/// <summary>
		/// Gets the desired information about the specified hub. This corresponds to the
		/// hubs.get Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hubId.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Hub GetHub(string hubId)
		{
			return GetHub(hubId, HyvesHubResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified hub. This corresponds to the
		/// hubs.get Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hubId.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Hub GetHub(string hubId, bool useFancyLayout)
		{
			return GetHub(hubId, HyvesHubResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified hub. This corresponds to the
		/// hubs.get Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hubId.</param>
		/// <param name="responsefields">Get extra information from the hub.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Hub GetHub(string hubId, HyvesHubResponsefield responsefield)
		{
			return GetHub(hubId, responsefield, false);
		}

		/// <summary>
		/// Gets the desired information about the specified hub. This corresponds to the
		/// hubs.get Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hubId.</param>
		/// <param name="responsefields">Get extra information from the hub.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Hub GetHub(string hubId, HyvesHubResponsefield responsefields, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(hubId))
			{
				throw new ArgumentNullException("hubId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["hubid"] = hubId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.HubsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Hub>("hub");
			}

			return null;
		}
		#endregion

		#region GetHubs
		/// <summary>
		/// Gets the desired information about the specified hub. This corresponds to the
		/// hubs.get Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hubIds.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Collection<Hub> GetHubs(Collection<string> hubIds)
		{
			return GetHubs(hubIds, HyvesHubResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified hub. This corresponds to the
		/// hubs.get Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hubIds.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Collection<Hub> GetHubs(Collection<string> hubIds, bool useFancyLayout)
		{
			return GetHubs(hubIds, HyvesHubResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified hub. This corresponds to the
		/// hubs.get Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hubIds.</param>
		/// <param name="responsefields">Get extra information from the hub.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Collection<Hub> GetHubs(Collection<string> hubIds, HyvesHubResponsefield responsefield)
		{
			return GetHubs(hubIds, responsefield, false);
		}

		/// <summary>
		/// Gets the desired information about the specified hub. This corresponds to the
		/// hubs.get Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hubIds.</param>
		/// <param name="responsefields">Get extra information from the hub.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Collection<Hub> GetHubs(Collection<string> hubIds, HyvesHubResponsefield responsefields, bool useFancyLayout)
		{
			if (hubIds == null || hubIds.Count == 0)
			{
				throw new ArgumentNullException("hubIds");
			}

			StringBuilder hubIdBuilder = new StringBuilder();
			if (hubIds != null)
			{
				foreach (string id in hubIds)
				{
					if (hubIdBuilder.Length != 0)
					{
						hubIdBuilder.Append(",");
					}
					hubIdBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["hubid"] = hubIdBuilder.ToString();
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.HubsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Hub>("hub");
			}

			return null;
		}
		#endregion

		#region GetHubsByUser
		/// <summary>
		/// Gets the desired hubs from the specified user. This corresponds to the
		/// hubs.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Collection<Hub> GetHubsByUser(string userId)
		{
			return GetHubsByUser(userId, HyvesHubResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired hubs from the specified user. This corresponds to the
		/// hubs.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Collection<Hub> GetHubsByUser(string userId, bool useFancyLayout)
		{
			return GetHubsByUser(userId, HyvesHubResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired hubs from the specified user. This corresponds to the
		/// hubs.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Collection<Hub> GetHubsByUser(string userId, HyvesHubResponsefield responsefield)
		{
			return GetHubsByUser(userId, responsefield, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired hubs from the specified user. This corresponds to the
		/// hubs.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified hub; null if the call fails.</returns>
		public Collection<Hub> GetHubsByUser(string userId, HyvesHubResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException("userId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["userid"] = userId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.HubsGetByUser, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Hub>("hub");
			}

			return null;
		}
		#endregion

    #region GetHubsByCategory
    /// <summary>
    /// Gets the desired hubs from the specified category. This corresponds to the
    /// hubs.getByHubCategory Hyves method.
    /// </summary>
    /// <param name="hubCategoryId">The identifier for the hub category.</param>
    /// <param name="responsefields">Get extra information from the requested hub.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified hubs; null if the call fails.</returns>
    public Collection<Hub> GetHubsByCategory(string hubCategoryId, HyvesHubResponsefield responsefields, bool useFancyLayout)
    {
      if (string.IsNullOrEmpty(hubCategoryId))
      {
        throw new ArgumentException("hubCategoryId cannot be null or empty.", "hubCategoryId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["hubcategoryid"] = hubCategoryId;
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.HubsGetByHubCategory, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Hub>("hub");
      }

      return null;
    }
    #endregion

		#region GetHubByShortName
		/// <summary>
		/// Gets the desired hub from the specified short name. This corresponds to the
		/// hubs.getByShortname Hyves method.
    /// </summary>
    /// <param name="shortName">The short name of the hub.</param>
		/// <returns>The information about the specified hubs; null if the call fails.</returns>
		public Hub GetHubByShortName(string shortName)
		{
      return GetHubByShortName(shortName, string.Empty, HyvesHubResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired hub from the specified short name. This corresponds to the
		/// hubs.getByShortname Hyves method.
    /// </summary>
    /// <param name="shortName">The short name of the hub.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified hubs; null if the call fails.</returns>
    public Hub GetHubByShortName(string shortName, bool useFancyLayout)
		{
			return GetHubByShortName(shortName, string.Empty, HyvesHubResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired hub from the specified short name. This corresponds to the
		/// hubs.getByShortname Hyves method.
		/// </summary>
    /// <param name="shortName">The short name of the hub.</param>
    /// <param name="hubType">The tybe of hub to retrieve (leave empty for all hub types).</param>
    /// <param name="responsefields">Get extra information from the requested hub.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified hubs; null if the call fails.</returns>
    public Hub GetHubByShortName(string shortName, string hubType, HyvesHubResponsefield responsefields, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(shortName))
			{
				throw new ArgumentException("shortName cannot be null or empty.", "shortName");
			}

			HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["shortname"] = shortName;
      if (string.IsNullOrEmpty(hubType) == false)
      {
        request.Parameters["hubtype"] = hubType;
      }

			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.HubsGetByShortname, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Hub>("hub");
			}

			return null;
		}
		#endregion

    #region GetHubsByShortName
    /// <summary>
    /// Gets the desired hubs from the specified short names. This corresponds to the
    /// hubs.getByShortname Hyves method.
    /// </summary>
    /// <param name="shortName">A list of short names of the hubs.</param>
    /// <returns>The information about the specified hubs; null if the call fails.</returns>
    public Collection<Hub> GetHubsByShortName(Collection<string> shortNames)
    {
      return GetHubsByShortName(shortNames, string.Empty, HyvesHubResponsefield.All, false);
    }

    /// <summary>
    /// Gets the desired hubs from the specified short names. This corresponds to the
    /// hubs.getByShortname Hyves method.
    /// </summary>
    /// <param name="shortName">A list of short names of the hubs.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified hubs; null if the call fails.</returns>
    public Collection<Hub> GetHubsByShortName(Collection<string> shortNames, bool useFancyLayout)
    {
      return GetHubsByShortName(shortNames, string.Empty, HyvesHubResponsefield.All, useFancyLayout);
    }

    /// <summary>
    /// Gets the desired hubs from the specified short names. This corresponds to the
    /// hubs.getByShortname Hyves method.
    /// </summary>
    /// <param name="shortName">A list of short names of the hubs.</param>
    /// <param name="hubType">The tybe of hub to retrieve (leave empty for all hub types).</param>
    /// <param name="responsefields">Get extra information from the requested hub.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified hubs; null if the call fails.</returns>
    public Collection<Hub> GetHubsByShortName(Collection<string> shortNames, string hubType, HyvesHubResponsefield responsefields, bool useFancyLayout)
    {
      if (shortNames == null || shortNames.Count == 0)
      {
        throw new ArgumentNullException("shortNames");
      }

      StringBuilder shortNamesBuilder = new StringBuilder();
      if (shortNames != null)
      {
        foreach (string shortName in shortNames)
        {
          if (shortNamesBuilder.Length != 0)
          {
            shortNamesBuilder.Append(",");
          }

          shortNamesBuilder.Append(shortName);
        }
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["shortname"] = shortNamesBuilder.ToString();
      if (string.IsNullOrEmpty(hubType) == false)
      {
        request.Parameters["hubtype"] = hubType;
      }
      
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.HubsGetByShortname, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Hub>("hub");
      }

      return null;
    }
    #endregion

    #region GetHubsByUser
    /// <summary>
    /// Gets the hubs of an user. This corresponds to the
    /// hubs.getByUser Hyves method.
    /// </summary>
    /// <param name="userId">The userId of the user.</param>
    /// <param name="hubType">The tybe of hub to retrieve (leave empty for all hub types).</param>
    /// <param name="hubCategoryIds">A list of identifiers for hub categories (leave empty for all hub categories).</param>
    /// <param name="responsefields">Get extra information from the requested hubs.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the hubs; null if the call fails.</returns>
    public Collection<Hub> GetHubsByUser(string userId, string hubType, Collection<string> hubCategoryIds, HyvesHubResponsefield responsefields, bool useFancyLayout)
    {
      if (string.IsNullOrEmpty(userId))
      {
        throw new ArgumentException("userId cannot be null or empty.", "userId");
      }

      StringBuilder hubCategoryIdsBuilder = new StringBuilder();
      if (hubCategoryIds != null)
      {
        foreach (string hubCategoryId in hubCategoryIds)
        {
          if (hubCategoryIdsBuilder.Length != 0)
          {
            hubCategoryIdsBuilder.Append(",");
          }

          hubCategoryIdsBuilder.Append(hubCategoryId);
        }
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["userid"] = userId;
      if (string.IsNullOrEmpty(hubType) == false)
      {
        request.Parameters["hubtype"] = hubType;
      }

      if (hubCategoryIdsBuilder.Length > 0)
      {
        request.Parameters["hubcategoryid"] = hubCategoryIdsBuilder.ToString();
      }

      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.HubsGetByUser, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Hub>("hub");
      }

      return null;
    }
    #endregion

    #region GetHubTypes
    /// <summary>
    /// Gets the hub types. This corresponds to the
    /// hubs.getHubTypes Hyves method.
    /// </summary>
    /// <returns>The information about the hub types; null if the call fails.</returns>
    public Collection<string> GetHubTypes()
    {
      HyvesRequest request = new HyvesRequest(this.session);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.HubsGetHubTypes);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        Collection<string> collection = new Collection<string>();
        Debug.Assert(response.Result is Hashtable);
        Hashtable result = (Hashtable)response.Result;

        Debug.Assert(result["hubtype"] is ArrayList);
        ArrayList resultList = (ArrayList)result["hubtype"];

        for (int i = 0; i < resultList.Count; i++)
        {
          collection.Add((string)resultList[i]);
        }

        return collection;
      }

      return null;
    }
    #endregion

    #region Scraps
    /// <summary>
    /// Gets the scraps from the specified hub. This corresponds to the
    /// hubs.getScraps Hyves method.
    /// </summary>
    /// <param name="targetHubId">The requested hub Id.</param>
    /// <param name="sortOrder">The sort order of the results.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the specified hub; null if the call fails.</returns>
    public Collection<Scrap> GetScraps(string targetHubId, HyvesSortOrder sortOrder, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (string.IsNullOrEmpty(targetHubId))
      {
        throw new ArgumentNullException("targetHubId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_hubid"] = targetHubId;
      request.Parameters["sortorder"] = EnumHelper.GetDescription(sortOrder);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.HubsGetScraps, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Scrap>("scrap");
      }

      return null;
    }
    #endregion

    #region SubscribeHub
    /// <summary>
		/// Subscribe the current user to a hub. This corresponds to the
    /// hubs.subscribe Hyves method.
		/// </summary>
    /// <param name="hubId">The identifier for the hub.</param>
		/// <returns>The new hub; null if the call fails.</returns>
    /// <remarks>Spam sensitive method (for trusted partners only).</remarks>
    public HubStats SubscripeHub(string hubId)
		{
      if (string.IsNullOrEmpty(hubId))
			{
        throw new ArgumentException("hubId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["hubid"] = hubId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.HubsSubscribe);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
				Debug.Assert(response.Result is Hashtable);
				Hashtable result = (Hashtable)response.Result;

        Debug.Assert(result is Hashtable);
        return new HubStats((Hashtable)result);
			}

			return null;
		}
		#endregion

    #region Search
    /// <summary>
    /// Search for hubs based on basic queries (keywords like city, name). This corresponds to the
    /// hubs.search Hyves method.
    /// </summary>
    /// <param name="searchterms">The searchterms to search for.</param>
    /// <param name="hubType">The tybe of hub to retrieve (leave empty for all hub types).</param>
    /// <param name="responsefields">Get extra information from the requested hubs.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the hubs; null if the call fails.</returns>
    public Collection<Hub> Search(string searchterms, string hubType, HyvesHubResponsefield responsefields, bool useFancyLayout)
    {
      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["searchterms"] = searchterms;
      if (string.IsNullOrEmpty(hubType) == false)
      {
        request.Parameters["hubtype"] = hubType;
      }

      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.HubsSearch, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Hub>("hub");
      }

      return null;
    }
    #endregion

    #region UpdateHubMedia
    /// <summary>
    /// Update the media associated with a hub. This corresponds to the
    /// hubs.updateMedia Hyves method.
    /// </summary>
    /// <param name="hubId">The identifier for the hub.</param>
    /// <param name="mediaId">The identifier for the media.</param>
    public void UpdateHubMedia(string hubId, string mediaId)
    {
      if (string.IsNullOrEmpty(hubId))
      {
        throw new ArgumentException("hubId cannot be null or empty.", "hubId");
      }

      if (string.IsNullOrEmpty(mediaId))
      {
        throw new ArgumentException("mediaId cannot be null or empty.", "mediaId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["hubid"] = hubId;
      request.Parameters["mediaid"] = mediaId;

      request.InvokeMethod(HyvesMethod.HubsUpdateMedia);
    }
    #endregion

		#region Private methodes
		private string ConvertResponsefieldsToString(HyvesHubResponsefield responsefields)
		{
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesHubResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesHubResponsefield>());
      }
      else
      {
        var responsefieldsValues = Enum.GetValues(typeof(HyvesHubResponsefield));
        foreach (HyvesHubResponsefield responseField in responsefieldsValues)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesHubResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
		}
		#endregion
	}
}
