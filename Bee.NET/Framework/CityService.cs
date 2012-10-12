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
	/// Represents the service APIs that allow access to information on a city
	/// </summary>
	public sealed class CityService
	{
		private HyvesSession session;

		internal CityService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		/// <summary>
		/// Gets the desired information about the specified city. This corresponds to the
		/// cities.get Hyves method.
		/// </summary>
		/// <param name="cityIDs">The requested city IDs.</param>
		/// <returns>The information about the specified city; null if the call fails.</returns>
		public Collection<City> GetCities(Collection<string> cityIDs)
		{
			if ((cityIDs == null) || (cityIDs.Count == 0))
			{
				throw new ArgumentNullException("cityIDs");
			}

			StringBuilder cityIDBuilder = new StringBuilder();
			if (cityIDs != null)
			{
				foreach (string id in cityIDs)
				{
					if (cityIDBuilder.Length != 0)
					{
						cityIDBuilder.Append(",");
					}

					cityIDBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["cityid"] = cityIDBuilder.ToString();

			HyvesResponse response = request.InvokeMethod(HyvesMethod.CitiesGet);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<City>("city");
			}

			return null;
		}
	}
}
