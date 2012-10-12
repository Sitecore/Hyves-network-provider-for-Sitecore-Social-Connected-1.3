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
	/// Represents the service APIs that allow searches.
	/// </summary>
	public sealed class SearchService
	{
		private HyvesSession session;

    internal SearchService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

    /// <summary>
    /// Search items in partical categories by searchterms. This corresponds to the
    /// search.find Hyves method.
    /// </summary>
    /// <param name="searchTerms">The search terms.</param>
    /// <param name="numberOfResults">The number of results to return.</param>
    /// <returns>The search results; null if the call fails.</returns>
    public Collection<SearchResult> Find(string searchTerms, int numberOfResults)
    {
      return Find(searchTerms, numberOfResults, string.Empty, HyvesSearchCategory.All);
    }

		/// <summary>
    /// Search items in partical categories by searchterms. This corresponds to the
    /// search.find Hyves method.
		/// </summary>
    /// <param name="searchTerms">The search terms.</param>
    /// <param name="numberOfResults">The number of results to return.</param>
    /// <param name="userId">The identifier of a user to filter the results.</param>
    /// <param name="searchCategories">The categories to search in.</param>
		/// <returns>The search results; null if the call fails.</returns>
    public Collection<SearchResult> Find(string searchTerms, int numberOfResults, string userId, HyvesSearchCategory searchCategories)
		{
      if (string.IsNullOrEmpty(searchTerms))
			{
        throw new ArgumentException("searchTerms");
			}

      if (numberOfResults < 1)
      {
        throw new ArgumentOutOfRangeException("numberOfResults");
      }

      if (searchCategories == HyvesSearchCategory.Friends && string.IsNullOrEmpty(userId))
      {
        throw new ArgumentException("userId cannot be null or empty when searching for friends.", "userId");
      }

			HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["searchterms"] = searchTerms;
      request.Parameters["nrresults"] = numberOfResults.ToString();

      if (string.IsNullOrEmpty(userId) == false)
      {
        request.Parameters["userid"] = searchTerms;
      }

      if (searchCategories != HyvesSearchCategory.All)
      {
        request.Parameters["categories"] = ConvertSearchCategoriesToString(searchCategories);
      }

			HyvesResponse response = request.InvokeMethod(HyvesMethod.SearchFind);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<SearchResult>("result");
			}

			return null;
		}

    #region Private methodes
    private string ConvertSearchCategoriesToString(HyvesSearchCategory responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesSearchCategory.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesSearchCategory>());
      }
      else
      {
        var userResponsefields = Enum.GetValues(typeof(HyvesSearchCategory));
        foreach (HyvesSearchCategory responseField in userResponsefields)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesSearchCategory.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
    }
    #endregion
	}
}
