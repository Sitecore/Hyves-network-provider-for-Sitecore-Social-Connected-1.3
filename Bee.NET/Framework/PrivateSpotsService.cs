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
	/// Represents the service APIs that allow access to information on Hyves privatespots.
	/// </summary>
	public sealed class PrivateSpotsService
	{
		private HyvesSession session;

		internal PrivateSpotsService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		#region GetPrivateSpot
		/// <summary>
		/// Gets the desired information about the specified privatespot. 
		/// </summary>
		/// <param name="privatespotID">The requested privatespot ID</param>
		/// <returns>The information about the specific privatespot; null if the call fails.</returns>
		public PrivateSpot GetPrivateSpot(string privatespotID)
		{
			return GetPrivateSpot(privatespotID, HyvesPrivateSpotResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified privatespot. 
		/// </summary>
		/// <param name="privatespotID">The requested privatespot ID</param>
		/// <param name="responsefields">Get extra information from the requested privatespot</param>
		/// <returns>The information about the specific privatespot; null if the call fails.</returns>
		public PrivateSpot GetPrivateSpot(string privatespotID, HyvesPrivateSpotResponsefield responsefields)
		{
			return GetPrivateSpot(privatespotID, responsefields, false);
		}

		/// <summary>
		/// Gets the desired information about the specified privatespot. 
		/// </summary>
		/// <param name="privatespotID">The requested privatespot ID</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specific privatespot; null if the call fails.</returns>
		public PrivateSpot GetPrivateSpot(string privatespotID, bool useFancyLayout)
		{
			return GetPrivateSpot(privatespotID, HyvesPrivateSpotResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified privatespot. 
		/// </summary>
		/// <param name="privatespotID">The requested privatespot ID</param>
		/// <param name="responsefields">Get extra information from the requested privatespot</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specific privatespot; null if the call fails.</returns>
		public PrivateSpot GetPrivateSpot(string privatespotID, HyvesPrivateSpotResponsefield responsefields, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(privatespotID))
			{
				throw new ArgumentNullException("privatespotID");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["privatespotid"] = privatespotID;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.PrivateSpotsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<PrivateSpot>("privatespot");
			}

			return null;
		}
		#endregion

		#region GetPrivateSpots
		/// <summary>
		/// Gets the desired information about the specified privatespots. This corresponds to the
		/// privatespots.get Hyves method.
		/// </summary>
		/// <param name="privatespotIds">The list of requested privatespot IDs.</param>
		/// <returns>The information about the specified privatespots; null if the call fails.</returns>
		public Collection<PrivateSpot> GetPrivateSpots(Collection<string> privatespotIds)
		{
			return GetPrivateSpots(privatespotIds, HyvesPrivateSpotResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified privatespots. This corresponds to the
		/// privatespots.get Hyves method.
		/// </summary>
		/// <param name="privatespotIds">The list of requested privatespot IDs.</param>
		/// <param name="responsefields">Get extra information from the requested privatespot</param>
		/// <returns>The information about the specified privatespots; null if the call fails.</returns>
		public Collection<PrivateSpot> GetPrivateSpots(Collection<string> privatespotIds, HyvesPrivateSpotResponsefield responsefields)
		{
			return GetPrivateSpots(privatespotIds, responsefields, false);
		}

		/// <summary>
		/// Gets the desired information about the specified privatespots. This corresponds to the
		/// privatespots.get Hyves method.
		/// </summary>
		/// <param name="privatespotIds">The list of requested privatespot IDs.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified privatespots; null if the call fails.</returns>
		public Collection<PrivateSpot> GetPrivateSpots(Collection<string> privatespotIds, bool useFancyLayout)
		{
			return GetPrivateSpots(privatespotIds, HyvesPrivateSpotResponsefield.All, useFancyLayout);
		}
		
		/// <summary>
		/// Gets the desired information about the specified privatespots. This corresponds to the
		/// privatespots.get Hyves method.
		/// </summary>
		/// <param name="privatespotIds">The list of requested privatespot IDs.</param>
		/// <param name="responsefields">Get extra information from the requested privatespot</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified privatespots; null if the call fails.</returns>
		public Collection<PrivateSpot> GetPrivateSpots(Collection<string> privatespotIds, HyvesPrivateSpotResponsefield responsefields, bool useFancyLayout)
		{
			if ((privatespotIds == null) || (privatespotIds.Count == 0))
			{
				throw new ArgumentException("privatespotIds");
			}

			StringBuilder privatespotIdBuilder = new StringBuilder();
			if (privatespotIds != null)
			{
				foreach (string id in privatespotIds)
				{
					if (privatespotIdBuilder.Length != 0)
					{
						privatespotIdBuilder.Append(",");
					}
					privatespotIdBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["privatespotid"] = privatespotIdBuilder.ToString();
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.PrivateSpotsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<PrivateSpot>("privatespot");
			}

			return null;
		}
		#endregion

		#region GetByLoggedin
		/// <summary>
		/// Gets the desired information about the privatespots of logged in user. 
		/// </summary>
		/// <returns>The information about the specific privatespot; null if the call fails.</returns>
		public Collection<PrivateSpot> GetByLoggedin()
		{
			return GetByLoggedin(HyvesPrivateSpotResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired information about the privatespots of logged in user.  
		/// </summary>
		/// <param name="responsefields">Get extra information from the requested privatespot</param>
		/// <returns>The information about the specific privatespot; null if the call fails.</returns>
		public Collection<PrivateSpot> GetByLoggedin(HyvesPrivateSpotResponsefield responsefields)
		{
			return GetByLoggedin(responsefields, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired information about the privatespots of logged in user. 
		/// </summary>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specific privatespot; null if the call fails.</returns>
		public Collection<PrivateSpot> GetByLoggedin(bool useFancyLayout)
		{
			return GetByLoggedin(HyvesPrivateSpotResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired information about the specified privatespot. 
		/// </summary>
		/// <param name="responsefields">Get extra information from the requested privatespot</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specific privatespot; null if the call fails.</returns>
		public Collection<PrivateSpot> GetByLoggedin(HyvesPrivateSpotResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.PrivateSpotsGetByLoggedin, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<PrivateSpot>("privatespot");
			}

			return null;
		}
		#endregion

		#region CreatePrivateSpot
		/// <summary>
		/// Create a new privatespot for the current user. This corresponds to the
		/// privatespots.create Hyves method.
		/// </summary>
		/// <param name="where">The where of the privatespot.</param>
		/// <param name="latitude">The latitude of the privatespot.</param>
		/// <param name="longitude">The longitude of the privatespot.</param>
		/// <returns>The new privatespot; null if the call fails.</returns>
		public PrivateSpot CreatePrivateSpot(string where, float latitude, float longitude)
		{
			return CreatePrivateSpot(where, latitude, longitude, HyvesPrivateSpotResponsefield.All);
		}

		/// <summary>
		/// Create a new privatespot for the current user. This corresponds to the
		/// privatespots.create Hyves method.
		/// </summary>
		/// <param name="where">The where of the privatespot.</param>
		/// <param name="latitude">The latitude of the privatespot.</param>
		/// <param name="longitude">The longitude of the privatespot.</param>
		/// <param name="responsefields">Get extra information from the privatespot.</param>
		/// <returns>The new privatespot; null if the call fails.</returns>
		public PrivateSpot CreatePrivateSpot(string where, float latitude, float longitude, HyvesPrivateSpotResponsefield responsefields)
		{
			if (string.IsNullOrEmpty(where))
			{
				throw new ArgumentException("where");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["where"] = where;
			request.Parameters["latitude"] = latitude.ToString();
			request.Parameters["longitude"] = longitude.ToString();
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.PrivateSpotsCreate);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<PrivateSpot>("privatespot");
			}

			return null;
		}
		#endregion

		#region Private methodes
		private string ConvertResponsefieldsToString(HyvesPrivateSpotResponsefield responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesPrivateSpotResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesPrivateSpotResponsefield>());
      }
      else
      {
        var responsefieldsValues = Enum.GetValues(typeof(HyvesPrivateSpotResponsefield));
        foreach (HyvesPrivateSpotResponsefield responseField in responsefieldsValues)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesPrivateSpotResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
		}
		#endregion
	}
}
