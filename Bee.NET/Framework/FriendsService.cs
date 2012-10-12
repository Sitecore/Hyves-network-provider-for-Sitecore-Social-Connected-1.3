// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the service APIs that allow access to information on Hyves friends.
	/// </summary>
	public sealed class FriendsService
	{
		private HyvesSession session;

		internal FriendsService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		/// <summary>
		/// Gets the friends of the current user. This corresponds to the
		/// friends.get Hyves method.
		/// </summary>
		/// <returns>The ids of the friends of the current user; null if the call fails.</returns>
		public Collection<string> GetFriends()
		{
			HyvesRequest request = new HyvesRequest(this.session);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.FriendsGet);
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

    /// <summary>
    /// Retrieves the connections between the user the access token is for and 
    /// another user. This corresponds to the friends.getConnection Hyves method.
    /// </summary>
    /// <param name="userId">The requested userId.</param>
    /// <returns>A list of connections; null if the call fails.</returns>
    public Collection<string> GetConnection(string userId)
    {
      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["userid"] = userId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.FriendsGetConnection);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        Collection<string> collection = new Collection<string>();
        Debug.Assert(response.Result is Hashtable);
        Hashtable result = (Hashtable)response.Result;

        Debug.Assert(result["connection"] is ArrayList);
        ArrayList list = (ArrayList)result["connection"];

        Debug.Assert(response.Result is Hashtable);
        Hashtable userIds = (Hashtable)list[0];

        Debug.Assert(result["connection"] is ArrayList);
        list = (ArrayList)userIds["userid"];
        
        for (int i = 0; i < list.Count; i++)
        {
          collection.Add((string)list[i]);
        }

        return collection;
      }

      return null;
    }

		/// <summary>
		/// Gets the distance for given friend ids with the current user. This corresponds to the
		/// friends.getDistance Hyves method.
		/// </summary>
		/// <param name="userIds">The list of requested user Ids.</param>
		/// <returns>The information about the specified users; null if the call fails.</returns>
		public Collection<Distance> GetDistance(Collection<string> userIds)
		{
			if ((userIds == null) || (userIds.Count == 0))
			{
				throw new ArgumentNullException("userIds");
			}

			StringBuilder userIdBuilder = new StringBuilder();
			foreach (string id in userIds)
			{
				if (userIdBuilder.Length != 0)
				{
					userIdBuilder.Append(",");
				}
				userIdBuilder.Append(id);
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["userid"] = userIdBuilder.ToString();

			HyvesResponse response = request.InvokeMethod(HyvesMethod.FriendsGetDistance);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Distance>("distance");
			}

			return null;
		}
		
		/// <summary>
		/// Gets all incoming invitations for the logged in user. This corresponds to the
		/// friends.getIncomingInvitations Hyves method.
		/// </summary>
		/// <returns>The information about the specified users; null if the call fails.</returns>
		public Collection<Friendinvitation> GetIncomingInvitations()
		{
			HyvesRequest request = new HyvesRequest(this.session);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.FriendsGetIncomingInvitations);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Friendinvitation>("friendinvitation");
			}

			return null;
		}

		/// <summary>
		/// Gets all outgoing invitations for the logged in user. This corresponds to the
		/// friends.getOutgoingInvitations Hyves method.
		/// </summary>
		/// <returns>The information about the specified users; null if the call fails.</returns>
		public Collection<Friendinvitation> GetOutgoingInvitations()
		{
			HyvesRequest request = new HyvesRequest(this.session);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.FriendsGetOutgoingInvitations);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Friendinvitation>("friendinvitation");
			}

			return null;
		}
	}
}
