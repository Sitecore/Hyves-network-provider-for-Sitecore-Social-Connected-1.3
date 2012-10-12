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
	/// Represents the service APIs that allow access to information on Hyves ping.
	/// </summary>
	public sealed class PingsService
	{
		private HyvesSession session;

		internal PingsService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

    #region GetPing
    /// <summary>
		/// Gets the desired information about the specified ping. This corresponds to the
		/// pings.get Hyves method.
		/// </summary>
		/// <param name="pingId">The requested pingId.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Ping GetPing(string pingId)
		{
			return GetPing(pingId, HyvesPingResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified ping. This corresponds to the
		/// pings.get Hyves method.
		/// </summary>
		/// <param name="pingId">The requested pingId.</param>
		/// <param name="responsefields">Get extra information from the ping.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Ping GetPing(string pingId, HyvesPingResponsefield responsefields)
		{
			return GetPing(pingId, responsefields, false);
		}

		/// <summary>
		/// Gets the desired information about the specified ping. This corresponds to the
		/// pings.get Hyves method.
		/// </summary>
		/// <param name="pingId">The requested pingId.</param>
		/// <param name="responsefields">Get extra information from the ping.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Ping GetPing(string pingId, bool useFancyLayout)
		{
			return GetPing(pingId, HyvesPingResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified ping. This corresponds to the
		/// pings.get Hyves method.
		/// </summary>
		/// <param name="pingId">The requested pingId.</param>
		/// <param name="responsefields">Get extra information from the ping.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Ping GetPing(string pingId, HyvesPingResponsefield responsefields, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(pingId))
			{
				throw new ArgumentNullException("pingId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["pingid"] = pingId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.PingsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Ping>("ping");
			}

			return null;
		}
    #endregion

    #region GetPings
    /// <summary>
		/// Gets the desired information about the specified ping. This corresponds to the
		/// pings.get Hyves method.
		/// </summary>
		/// <param name="pingIds">The requested pingIds.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPings(Collection<string> pingIds)
		{
			return GetPings(pingIds, HyvesPingResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified ping. This corresponds to the
		/// pings.get Hyves method.
		/// </summary>
		/// <param name="pingIds">The requested pingIds.</param>
		/// <param name="responsefields">Get extra information from the ping.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPings(Collection<string> pingIds, HyvesPingResponsefield responsefields)
		{
			return GetPings(pingIds, responsefields, false);
		}

		/// <summary>
		/// Gets the desired information about the specified ping. This corresponds to the
		/// pings.get Hyves method.
		/// </summary>
		/// <param name="pingIds">The requested pingIds.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPings(Collection<string> pingIds, bool useFancyLayout)
		{
			return GetPings(pingIds, HyvesPingResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified ping. This corresponds to the
		/// pings.get Hyves method.
		/// </summary>
		/// <param name="pingIds">The requested pingIds.</param>
		/// <param name="responsefields">Get extra information from the ping.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPings(Collection<string> pingIds, HyvesPingResponsefield responsefields, bool useFancyLayout)
		{
			if (pingIds == null || pingIds.Count == 0)
			{
				throw new ArgumentNullException("pingIds");
			}

			StringBuilder pingIdBuilder = new StringBuilder();
			if (pingIds != null)
			{
				foreach (string id in pingIds)
				{
					if (pingIdBuilder.Length != 0)
					{
						pingIdBuilder.Append(",");
					}
					pingIdBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["pingid"] = pingIdBuilder.ToString();
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.PingsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Ping>("ping");
			}

			return null;
		}
    #endregion

    #region GetPingsByTargetUser
    /// <summary>
		/// Gets the desired pings from the specified user. This corresponds to the
		/// pings.getByTargetUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPingsByTargetUser(string userId)
		{
      return GetPingsByTargetUser(userId, HyvesPingResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired pings from the specified user. This corresponds to the
		/// pings.getByTargetUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the ping.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPingsByTargetUser(string userId, HyvesPingResponsefield responsefields)
		{
      return GetPingsByTargetUser(userId, responsefields, false);
		}

		/// <summary>
		/// Gets the desired pings from the specified user. This corresponds to the
		/// pings.getByTargetUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPingsByTargetUser(string userId, bool useFancyLayout)
		{
      return GetPingsByTargetUser(userId, HyvesPingResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired pings from the specified user. This corresponds to the
		/// pings.getByTargetUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the ping.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPingsByTargetUser(string userId, HyvesPingResponsefield responsefields, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentException("userId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_userid"] = userId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.PingsGetByTargetUser, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Ping>("ping");
			}

			return null;
		}
    #endregion

    #region GetPingsByUser
    /// <summary>
		/// Gets the desired pings from the specified user. This corresponds to the
		/// pings.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPingsByUser(string userId)
		{
			return GetPingsByUser(userId, HyvesPingResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired pings from the specified user. This corresponds to the
		/// pings.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the ping.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPingsByUser(string userId, HyvesPingResponsefield responsefields)
		{
			return GetPingsByUser(userId, responsefields, false);
		}

		/// <summary>
		/// Gets the desired pings from the specified user. This corresponds to the
		/// pings.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPingsByUser(string userId, bool useFancyLayout)
		{
			return GetPingsByUser(userId, HyvesPingResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired pings from the specified user. This corresponds to the
		/// pings.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the ping.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified ping; null if the call fails.</returns>
		public Collection<Ping> GetPingsByUser(string userId, HyvesPingResponsefield responsefields, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(userId))
			{
        throw new ArgumentException("userId cannot be null or empty.", "userId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["userid"] = userId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.PingsGetByUser, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Ping>("ping");
			}

			return null;
		}
    #endregion

    #region GetPingTypes
    /// <summary>
    /// Gets the default ping types. This corresponds to the
    /// pings.getTypes Hyves method.
    /// </summary>
    /// <returns>The information about the ping types; null if the call fails.</returns>
    /// <remarks>Hidden method (for any partner).</remarks>
    public Collection<PingType> GetPingTypes()
    {
      HyvesRequest request = new HyvesRequest(this.session);
      
      HyvesResponse response = request.InvokeMethod(HyvesMethod.PingsGetTypes, false);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<PingType>("pingtype");
      }

      return null;
    }
    #endregion

    #region CreatePing
    /// <summary>
    /// Creates a ping. This corresponds to the
    /// pings.create Hyves method.
    /// </summary>
    /// <param name="targetUserId">The title of the ping.</param>
    /// <param name="pingTypeId"></param>
    /// <param name="body">The body of the ping.</param>
    /// <param name="visibility">The visibility of the ping.</param>
    /// <returns>The new ping; null if the call fails.</returns>
    public Ping CreatePing(string targetUserId, string pingTypeId, string body, HyvesVisibility visibility)
    {
      if (string.IsNullOrEmpty(targetUserId))
      {
        throw new ArgumentException("targetUserId cannot be null or empty.", "targetUserId");
      }

      if (string.IsNullOrEmpty(pingTypeId) && string.IsNullOrEmpty(body))
      {
        throw new ArgumentException("Please enter a ping type or a body.");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_userid"] = targetUserId;
      if (string.IsNullOrEmpty(body))
      {
        request.Parameters["pingtypeid"] = pingTypeId;
      }
      else
      {
        request.Parameters["body"] = body;
      }

      request.Parameters["visibility"] = EnumHelper.GetDescription(visibility);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.PingsCreate);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Ping>("ping");
      }

      return null;
    }
    #endregion

    #region CreatePingByFriend
    /// <summary>
    /// Creates a ping. This corresponds to the
    /// pings.createByFriend Hyves method.
    /// </summary>
    /// <param name="targetUserId">The title of the ping.</param>
    /// <param name="pingTypeId">The identifier for a ping type.</param>
    /// <param name="body">The html of the ping.</param>
    /// <param name="visibility">The visibility of the ping.</param>
    /// <returns>The new ping; null if the call fails.</returns>
    /// <remarks>Hidden method (for any partner).</remarks>
    public Ping CreatePingByFriend(string targetUserId, string pingTypeId, string body, HyvesVisibility visibility)
    {
      if (string.IsNullOrEmpty(targetUserId))
      {
        throw new ArgumentException("targetUserId cannot be null or empty.", "targetUserId");
      }

      if (string.IsNullOrEmpty(pingTypeId) && string.IsNullOrEmpty(body))
      {
        throw new ArgumentException("Please enter a ping type or a body.");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_userid"] = targetUserId;
      if (string.IsNullOrEmpty(body))
      {
        request.Parameters["pingtypeid"] = pingTypeId;
      }
      else
      {
        request.Parameters["body"] = body;
      }
      
      request.Parameters["visibility"] = EnumHelper.GetDescription(visibility);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.PingsCreateByFriend);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Ping>("ping");
      }

      return null;
    }
    #endregion

		#region Private methodes
		private string ConvertResponsefieldsToString(HyvesPingResponsefield responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesPingResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesPingResponsefield>());
      }
      else
      {
        var responsefieldsValues = Enum.GetValues(typeof(HyvesPingResponsefield));
        foreach (HyvesPingResponsefield responseField in responsefieldsValues)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesPingResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
		}
		#endregion
	}
}
