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
	/// Represents the service APIs that allow access to information on Hyves hubCategories.
	/// </summary>
	public sealed class HubCategoriesService
	{
		private HyvesSession session;

		internal HubCategoriesService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		#region GetHubCategories
		/// <summary>
		/// Gets the desired information about the specified hubCategory. This corresponds to the
		/// hubCategories.get Hyves method.
		/// </summary>
		/// <param name="hubCategoryId">The requested hubCategoryIds.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified hubCategory; null if the call fails.</returns>
		public Collection<HubCategory> GetHubCategories(Collection<string> hubCategoryIds, bool useFancyLayout)
		{
			if (hubCategoryIds == null || hubCategoryIds.Count == 0)
			{
				throw new ArgumentNullException("hubCategoryIds");
			}

			StringBuilder hubCategoryIdBuilder = new StringBuilder();
			if (hubCategoryIds != null)
			{
				foreach (string id in hubCategoryIds)
				{
					if (hubCategoryIdBuilder.Length != 0)
					{
						hubCategoryIdBuilder.Append(",");
					}
					hubCategoryIdBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["hubcategoryid"] = hubCategoryIdBuilder.ToString();

			HyvesResponse response = request.InvokeMethod(HyvesMethod.HubCategoriesGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<HubCategory>("hubcategory");
			}

			return null;
		}
		#endregion

    #region GetHubCategoriesByHubType
    /// <summary>
    /// Gets the hub categories by hub type. This corresponds to the
    /// hubCategories.getByHubType Hyves method.
    /// </summary>
    /// <param name="hubType">The tybe of hub to retrieve.</param>
    /// <returns>The information about the hubCategories; null if the call fails.</returns>
    public Collection<HubCategory> GetHubCategoriesByHubType(string hubType)
    {
      if (string.IsNullOrEmpty(hubType))
      {
        throw new ArgumentException("hubType cannot be null or empty.", "hubType");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["hubtype"] = hubType;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.HubCategoriesGetByHubType, false);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<HubCategory>("hubcategory");
      }

      return null;
    }
    #endregion 
    
    #region GetChildren
    /// <summary>
    /// Gets the hub categories by parent hub category. This corresponds to the
    /// hubCategories.getChildren Hyves method.
    /// </summary>
    /// <param name="hubType">The tybe of hub to retrieve.</param>
    /// <returns>The information about the hubCategories; null if the call fails.</returns>
    public Collection<HubCategory> GetChildren(string hubCategoryId)
    {
      if (string.IsNullOrEmpty(hubCategoryId))
      {
        throw new ArgumentException("hubCategoryId cannot be null or empty.", "hubCategoryId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["hubcategoryid"] = hubCategoryId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.HubCategoriesGetChildren, false);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<HubCategory>("hubcategory");
      }

      return null;
    }
    #endregion    	
	}
}
