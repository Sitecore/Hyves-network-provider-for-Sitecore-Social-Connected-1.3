// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the service APIs that allow access to information on Hyves www (Who What Where).
	/// </summary>
	public sealed class WwwsService
	{
		private HyvesSession session;

		internal WwwsService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		#region GetWww
		/// <summary>
		/// Gets the desired information about the specified www. This corresponds to the
		/// wwws.get Hyves method.
		/// </summary>
		/// <param name="wwwID">The requested wwwID.</param>
		/// <returns>The information about the specified www; null if the call fails.</returns>
		public Www GetWww(string wwwID)
		{
			return GetWww(wwwID, HyvesWwwResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified www. This corresponds to the
		/// wwws.get Hyves method.
		/// </summary>
		/// <param name="wwwID">The requested wwwID.</param>
		/// <param name="responsefields">Get extra information from the requested www.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified www; null if the call fails.</returns>
		public Www GetWww(string wwwID, HyvesWwwResponsefield responsefields, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(wwwID))
			{
				throw new ArgumentNullException("wwwID");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["wwwid"] = wwwID;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.WwwsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Www>("www");
			}

			return null;
		}
		#endregion

		#region GetWwws
		/// <summary>
		/// Gets the desired information about the specified www. This corresponds to the
		/// wwws.get Hyves method.
		/// </summary>
		/// <param name="wwwIDs">The requested wwwIDs.</param>
		/// <returns>The information about the specified www; null if the call fails.</returns>
		public Collection<Www> GetWwws(Collection<string> wwwIDs)
		{
			return GetWwws(wwwIDs, HyvesWwwResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified www. This corresponds to the
		/// wwws.get Hyves method.
		/// </summary>
		/// <param name="wwwIDs">The requested wwwIDs.</param>
		/// <param name="responsefields">Get extra information from the requested www.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified www; null if the call fails.</returns>
		public Collection<Www> GetWwws(Collection<string> wwwIDs, HyvesWwwResponsefield responsefields, bool useFancyLayout)
		{
			if (wwwIDs == null || wwwIDs.Count == 0)
			{
				throw new ArgumentNullException("wwwIDs");
			}

			StringBuilder wwwIDBuilder = new StringBuilder();
			if (wwwIDs != null)
			{
				foreach (string id in wwwIDs)
				{
					if (wwwIDBuilder.Length != 0)
					{
						wwwIDBuilder.Append(",");
					}
					wwwIDBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["wwwid"] = wwwIDBuilder.ToString();
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.WwwsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Www>("www");
			}

			return null;
		}
		#endregion

		#region GetWwwsByUser
		/// <summary>
		/// Gets the desired wwws from the specified user. This corresponds to the
		/// wwws.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <returns>The information about the specified www; null if the call fails.</returns>
		public Collection<Www> GetWwwsByUser(string userId)
		{
			return GetWwwsByUser(userId, HyvesWwwResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired wwws from the specified user. This corresponds to the
		/// wwws.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the requested www.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified www; null if the call fails.</returns>
		public Collection<Www> GetWwwsByUser(string userId, HyvesWwwResponsefield responsefields, bool useFancyLayout)
		{
			return GetWwwsByUser(userId, responsefields, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired wwws from the specified user. This corresponds to the
		/// wwws.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the requested www.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified www; null if the call fails.</returns>
		public Collection<Www> GetWwwsByUser(string userId, HyvesWwwResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException("userId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["userid"] = userId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.WwwsGetByUser, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Www>("www");
			}

			return null;
		}
		#endregion

    #region GetWwwsByHub
    /// <summary>
    /// Gets the desired wwws from the specified user. This corresponds to the
    /// wwws.getByUser Hyves method.
    /// </summary>
    /// <param name="hubId">The requested hub Id.</param>
    /// <param name="responsefields">Get extra information from the requested www.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the specified www; null if the call fails.</returns>
    public Collection<Www> GetWwwsByHub(string hubId, HyvesWwwResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (string.IsNullOrEmpty(hubId))
      {
        throw new ArgumentException("hubId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["hubid"] = hubId;
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.WwwsGetByHub, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Www>("www");
      }

      return null;
    }
    #endregion

    #region GetWwwsForFriends
    /// <summary>
		/// Retrieves the most recent wwws for the friends of the loggedin user. 
		/// This corresponds to the wwws.getForFriends Hyves method.
		/// </summary>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the wwws; null if the call fails.</returns>
		public Collection<Www> GetWwwsForFriends(bool useFancyLayout)
		{
			return GetWwwsForFriends(HyvesWwwResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Retrieves the most recent wwws for the friends of the loggedin user. 
		/// This corresponds to the wwws.getForFriends Hyves method.
		/// </summary>
		/// <param name="responsefields">Get extra information from the requested www.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the wwws; null if the call fails.</returns>
		public Collection<Www> GetWwwsForFriends(HyvesWwwResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.WwwsGetForFriends, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Www>("www");
			}

			return null;
		}
		#endregion

    #region GetComments
    /// <summary>
    /// Gets the comments from the specified www. This corresponds to the
    /// wwws.getComments Hyves method.
    /// </summary>
    /// <param name="wwwId">The requested www ID.</param>
    /// <returns>The information about the specified www; null if the call fails.</returns>
    public Collection<Comment> GetComments(string wwwId)
    {
      return GetComments(wwwId, false, -1, -1);
    }

    /// <summary>
    /// Gets the comments from the specified www. This corresponds to the
    /// wwws.getComments Hyves method.
    /// </summary>
    /// <param name="wwwId">The requested www ID.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the specified www; null if the call fails.</returns>
    public Collection<Comment> GetComments(string wwwId, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (string.IsNullOrEmpty(wwwId))
      {
        throw new ArgumentNullException("wwwId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_wwwid"] = wwwId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.WwwsGetComments, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Comment>("comment");
      }

      return null;
    }
    #endregion

    #region GetRespects
    /// <summary>
    /// Gets the respects from the specified www. This corresponds to the
    /// wwws.getRespects Hyves method.
    /// </summary>
    /// <param name="wwwId">The requested www ID.</param>
    /// <returns>The information about the specified www; null if the call fails.</returns>
    public Collection<Respect> GetRespects(string wwwId)
    {
      return GetRespects(wwwId, false, -1, -1);
    }

    /// <summary>
    /// Gets the respects from the specified www. This corresponds to the
    /// wwws.getRespects Hyves method.
    /// </summary>
    /// <param name="wwwId">The requested www ID.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the specified www; null if the call fails.</returns>
    public Collection<Respect> GetRespects(string wwwId, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (string.IsNullOrEmpty(wwwId))
      {
        throw new ArgumentException("wwwId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_wwwid"] = wwwId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.WwwsGetRespects, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Respect>("respect");
      }

      return null;
    }
    #endregion

    #region SearchPublic
    /// <summary>
    /// Search in public wwws. This corresponds to the
    /// wwws.searchPublic Hyves method.
    /// </summary>
    /// <param name="searchterms">The search terms.</param>
    /// <param name="scopeMinId">Only www's posted after this one will be received.</param>
    /// <param name="scopeMaxId">Only www's posted before this one will be received.</param>
    /// <param name="limit">The maximum number of results desired.</param>
    /// <param name="responsefields">Get extra information from the requested www.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified www; null if the call fails.</returns>
    public Collection<Www> SearchPublic(string searchterms, string scopeMinId, string scopeMaxId, int limit, HyvesWwwResponsefield responsefields, bool useFancyLayout)
    {
      HyvesRequest request = new HyvesRequest(this.session);
      if (string.IsNullOrEmpty(searchterms) == false)
      {
        request.Parameters["searchterms"] = searchterms;
      }

      if (string.IsNullOrEmpty(scopeMinId) == false)
      {
        request.Parameters["scope_minid"] = scopeMinId;
      }

      if (string.IsNullOrEmpty(scopeMaxId) == false)
      {
        request.Parameters["scope_maxid"] = scopeMaxId;
      }

      if (limit > 0)
      {
        request.Parameters["limit"] = limit.ToString();
      }

      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.WwwsSearchPublic, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Www>("www");
      }

      return null;
    }
    #endregion

		#region CreateWww
		/// <summary>
		/// Create a new www for the current user. This corresponds to the
		/// www.create Hyves method.
		/// </summary>
		/// <param name="emotion">The emotion of the www.</param>
		/// <param name="visibility">The visibility of the www.</param>
		/// <param name="where">The where of the www.</param>
		/// <returns>The new www; null if the call fails.</returns>
		public Www CreateWww(string emotion, HyvesVisibility visibility, string hubId, string privateSpotId, string where, float? latitude, float? longitude)
		{
			if (string.IsNullOrEmpty(emotion))
			{
				throw new ArgumentException("emotion");
			}

			HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["emotion"] = emotion;
      request.Parameters["visibility"] = EnumHelper.GetDescription(visibility);

      if (string.IsNullOrEmpty(hubId) == false)
      {
        request.Parameters["hubid"] = hubId;
      }
      else if (string.IsNullOrEmpty(privateSpotId) == false)
      {
        request.Parameters["privatespotid"] = privateSpotId;
      }

      request.Parameters["where"] = where;

      if (latitude.HasValue)
      {
        request.Parameters["latitude"] = latitude.Value.ToString("F");
      }

      if (longitude.HasValue)
      {
        request.Parameters["longitude"] = longitude.Value.ToString("F");
      }

			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(HyvesWwwResponsefield.All);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.WwwsCreate);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Www>("www");
			}

			return null;
		}
		#endregion

    #region CreateRespect
    /// <summary>
    /// Creates respect for an www. This corresponds to the
    /// www.createRespect Hyves method.
    /// </summary>
    /// <param name="targetWwwId">A single www id.</param>
    /// <param name="respectType">The type of the respect.</param>
    /// <returns>True if the call succeeds, false if the call fails.</returns>
    public bool CreateRespect(string targetWwwId, HyvesRespectType respectType)
    {
      if (string.IsNullOrEmpty(targetWwwId))
      {
        throw new ArgumentException("targetWwwId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_wwwid"] = targetWwwId;
      request.Parameters["respecttype"] = EnumHelper.GetDescription(respectType);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.WwwsCreateRespect);
      return response.Status == HyvesResponseStatus.Succeeded;
    }
    #endregion
    
		#region Private methodes
		private string ConvertResponsefieldsToString(HyvesWwwResponsefield responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesWwwResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesWwwResponsefield>());
      }
      else
      {
        var responsefieldsValues = Enum.GetValues(typeof(HyvesWwwResponsefield));
        foreach (HyvesWwwResponsefield responseField in responsefieldsValues)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesWwwResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
		}
		#endregion
	}
}
