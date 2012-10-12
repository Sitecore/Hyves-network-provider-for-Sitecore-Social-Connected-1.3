// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

using Hyves.Service.Core;

namespace Hyves.Service
{
  /// <summary>
  /// Represents a target.
  /// </summary>
  public sealed class SearchResult : HyvesEntity
  {
    private bool categoryTransformed;
    private bool totalResultsTransformed;

    public SearchResult()
    {
    }

    /// <summary>
    /// The category of the result.
    /// </summary>
    public HyvesSearchCategory Category
    {
      get
      {
        if (this.categoryTransformed == false)
        {
          return TransformCategory();
        }

        return (HyvesSearchCategory)this["category"];
      }
    }

    /// <summary>
    /// The display friendly category.
    /// </summary>
    public string CategoryText
    {
      get
      {
        return GetState<string>("categoryText");
      }
    }

    /// <summary>
    /// The key of the id field.
    /// </summary>
    private string IdKey
    {
      get
      {
        return GetState<string>("idkey");
      }
    }
    
    /// <summary>
    /// The number of results.
    /// </summary>
    public int TotalResults
    {
      get
      {
        if (totalResultsTransformed == false)
        {
          return TransformTotalResults();
        }

        return (int)this["totalresults"];
      }
    }

    public Collection<SearchResultItem> SearchResultItems
    {
      get
      {
        ArrayList list = (ArrayList)this["item"];

        Collection<SearchResultItem> searchResultItems = new Collection<SearchResultItem>();
        if (list != null)
        {
          for (int i = 0; i < list.Count; i++)
          {
            SearchResultItem searchResultItem = new SearchResultItem((Hashtable)list[i], this.IdKey);
            searchResultItems.Add(searchResultItem);
          }
        }

        return searchResultItems;
      }
    }

    private HyvesSearchCategory TransformCategory()
    {
      Debug.Assert(this.categoryTransformed == false);

      HyvesSearchCategory targetType = HyvesSearchCategory.All;
      string state = GetState<string>("category") ?? String.Empty;

      if (state.Length != 0)
      {
        switch (state)
        {
          case "friends":
            targetType = HyvesSearchCategory.Friends;
            break;
          case "ownhub":
            targetType = HyvesSearchCategory.OwnHub;
            break;
          case "gadgets":
            targetType = HyvesSearchCategory.Gadgets;
            break;
          case "albums":
            targetType = HyvesSearchCategory.Albums;
            break;
          case "famous":
            targetType = HyvesSearchCategory.Famous;
            break;
        }
      }

      this["category"] = targetType;
      this.categoryTransformed = true;

      return targetType;
    }

    private int TransformTotalResults()
    {
      Debug.Assert(this.totalResultsTransformed == false);

      int count = HyvesResponse.CoerceInt32(this["totalresults"]);

      this["totalresults"] = count;

      this.totalResultsTransformed = true;

      return count;
    }
  }
}
