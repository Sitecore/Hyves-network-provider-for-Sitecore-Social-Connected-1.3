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
	/// Represents the service APIs that allow access to information on a country
	/// </summary>
	public sealed class CountryService
	{
		private HyvesSession session;

		internal CountryService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		/// <summary>
		/// Gets the desired information about the specified country. This corresponds to the
		/// countries.get Hyves method.
		/// </summary>
		/// <param name="countryIDs">The requested country IDs.</param>
		/// <returns>The information about the specified country; null if the call fails.</returns>
		public Collection<Country> GetCountries(Collection<string> countryIDs)
    {
      if (countryIDs == null)
      {
        throw new ArgumentNullException("countryIDs");
      }

			if (countryIDs.Count == 0)
			{
        throw new ArgumentException("countryIDs must contain items.", "countryIDs");
			}

			StringBuilder countryIDBuilder = new StringBuilder();
			if (countryIDs != null)
			{
				foreach (string id in countryIDs)
				{
					if (countryIDBuilder.Length != 0)
					{
						countryIDBuilder.Append(",");
					}

					countryIDBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["countryid"] = countryIDBuilder.ToString();

			HyvesResponse response = request.InvokeMethod(HyvesMethod.CountriesGet);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Country>("country");
			}

			return null;
		}
	}
}
