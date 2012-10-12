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
	/// Represents the service APIs that allow access to information on Hyves event.
	/// </summary>
	public sealed class EventsService
	{
		private HyvesSession session;

		internal EventsService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}
    
		#region GetEvents
		/// <summary>
		/// Gets the desired information about the specified event. This corresponds to the
		/// events.get Hyves method.
		/// </summary>
		/// <param name="eventIds">The requested eventIds.</param>
		/// <returns>The information about the specified event; null if the call fails.</returns>
		public Collection<Event> GetEvents(Collection<string> eventIds)
		{
			return GetEvents(eventIds, HyvesEventResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified event. This corresponds to the
		/// events.get Hyves method.
		/// </summary>
		/// <param name="eventIds">The requested eventIds.</param>
		/// <param name="responsefields">Get extra information from the requested event.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified event; null if the call fails.</returns>
		public Collection<Event> GetEvents(Collection<string> eventIds, HyvesEventResponsefield responsefields, bool useFancyLayout)
		{
			if (eventIds == null || eventIds.Count == 0)
			{
				throw new ArgumentException("eventIds");
			}

			StringBuilder eventIdBuilder = new StringBuilder();
			if (eventIds != null)
			{
				foreach (string id in eventIds)
				{
					if (eventIdBuilder.Length != 0)
					{
						eventIdBuilder.Append(",");
					}

					eventIdBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["eventid"] = eventIdBuilder.ToString();
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.EventsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Event>("event");
			}

			return null;
		}
		#endregion

		#region GetEventsByUserPresent
		/// <summary>
		/// Gets the desired events from the specified user. This corresponds to the
		/// events.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the requested event.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified event; null if the call fails.</returns>
    public Collection<Event> GetEventsByUserPresent(string userId, HyvesEventResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException("userId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["userid"] = userId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.EventsGetByUserPresent, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Event>("event");
			}

			return null;
		}
		#endregion

    #region GetEventsByHub
    /// <summary>
    /// Gets the desired events from the specified hub. This corresponds to the
    /// events.getByHub Hyves method.
    /// </summary>
    /// <param name="hubId">The requested hub Id.</param>
    /// <param name="inFuture">Select only events in the future.</param>
    /// <param name="responsefields">Get extra information from the requested event.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the specified event; null if the call fails.</returns>
    public Collection<Event> GetEventsByHub(string hubId, bool inFuture, HyvesEventResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (string.IsNullOrEmpty(hubId))
      {
        throw new ArgumentException("hubId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["hubid"] = hubId;
      request.Parameters["infuture"] = inFuture.ToString();
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.EventsGetByHub, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Event>("event");
      }

      return null;
    }
    #endregion

    #region GetEventsByLoggedin
    /// <summary>
    /// Gets the desired events from the loggedin user. This corresponds to the
    /// events.getByLoggedin Hyves method.
    /// </summary>
    /// <param name="inFuture">Select only events in the future.</param>
    /// <param name="responsefields">Get extra information from the requested event.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the specified event; null if the call fails.</returns>
    public Collection<Event> GetEventsByLoggedin(bool inFuture, HyvesEventResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
    {
      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["infuture"] = inFuture.ToString();
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.EventsGetByLoggedin, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Event>("event");
      }

      return null;
    }
    #endregion

    #region GetPresence
    /// <summary>
    /// Gets the desired information about the presence. This corresponds to the
    /// events.getPresence Hyves method.
    /// </summary>
    /// <param name="eventIds">The identifier for the event.</param>
    /// <returns>The information about the presence; null if the call fails.</returns>
    public Collection<string> GetPresence(string eventId)
    {
      if (eventId == null)
      {
        throw new ArgumentNullException("eventId");
      }

      if (eventId == string.Empty)
      {
        throw new ArgumentException("eventId cannot be an empty string.", "eventId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["eventid"] = eventId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.EventsGetPresence);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        Collection<string> collection = new Collection<string>();
        Debug.Assert(response.Result is Hashtable);
        Hashtable result = (Hashtable)response.Result;

        Debug.Assert(result["userid"] is ArrayList);
        ArrayList friendsList = (ArrayList)result["userid"];

        for (int i = 0; i < friendsList.Count; i++)
        {
          collection.Add((string)friendsList[i]);
        }

        return collection;
      }

      return null;
    }
    #endregion

    #region GetPresenceForFriends
    /// <summary>
    /// Gets the desired information about the presence. This corresponds to the
    /// events.getPresenceForFriends Hyves method.
    /// </summary>
    /// <param name="eventIds">The identifier for the event.</param>
    /// <returns>The information about the presence; null if the call fails.</returns>
    public Collection<string> GetPresenceForFriends(string eventId)
    {
      if (eventId == null)
      {
        throw new ArgumentNullException("eventId");
      }

      if (eventId == string.Empty)
      {
        throw new ArgumentException("eventId cannot be an empty string.", "eventId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["eventid"] = eventId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.EventsGetPresenceForFriends);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        Collection<string> collection = new Collection<string>();
        Debug.Assert(response.Result is Hashtable);
        Hashtable result = (Hashtable)response.Result;

        Debug.Assert(result["userid"] is ArrayList);
        ArrayList friendsList = (ArrayList)result["userid"];

        for (int i = 0; i < friendsList.Count; i++)
        {
          collection.Add((string)friendsList[i]);
        }

        return collection;
      }

      return null;
    }
    #endregion

    #region AddPresence
    /// <summary>
    /// Adds user presence to event. This corresponds to the
    /// event.addPresence Hyves method.
		/// </summary>
    /// <param name="eventId">The identifier of the event.</param>
		/// <param name="visibility">The visibility of the event presence.</param>
    /// <returns><b>True</b> if successful; otherwise <b>false</b>.</returns>
    public bool AddPresence(string eventId, HyvesEventPrecenseVisibility visibility)
		{
      if (string.IsNullOrEmpty(eventId))
			{
        throw new ArgumentException("eventId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["eventid"] = eventId;
      request.Parameters["visibility"] = EnumHelper.GetDescription(visibility);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.EventsAddPresence);
			return response.Status == HyvesResponseStatus.Succeeded;
		}
		#endregion
        
		#region Private methodes
		private string ConvertResponsefieldsToString(HyvesEventResponsefield responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesEventResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesEventResponsefield>());
      }
      else
      {
        var responsefieldsValues = Enum.GetValues(typeof(HyvesEventResponsefield));
        foreach (HyvesEventResponsefield responseField in responsefieldsValues)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesEventResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
		}
		#endregion
	}
}
