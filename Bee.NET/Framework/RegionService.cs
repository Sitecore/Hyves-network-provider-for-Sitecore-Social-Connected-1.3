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
	/// Represents the service APIs that allow access to information of a region
	/// </summary>
	public sealed class RegionService
	{
		private HyvesSession session;

		internal RegionService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		/// <summary>
		/// Gets the desired information about the specified region. This corresponds to the
		/// regions.get Hyves method.
		/// </summary>
		/// <param name="regionIDs">The requested region IDs.</param>
		/// <returns>The information about the specified region; null if the call fails.</returns>
		public Collection<Region> GetRegions(Collection<string> regionIDs)
		{
			if ((regionIDs == null) || (regionIDs.Count == 0))
			{
				throw new ArgumentNullException("regionIDs");
			}

			StringBuilder regionIDBuilder = new StringBuilder();
			if (regionIDs != null)
			{
				foreach (string id in regionIDs)
				{
					if (regionIDBuilder.Length != 0)
					{
						regionIDBuilder.Append(",");
					}
					regionIDBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["regionid"] = regionIDBuilder.ToString();

			HyvesResponse response = request.InvokeMethod(HyvesMethod.RegionsGet);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Region>("region");
			}

			return null;
		}
	}
}
