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
  public sealed class SearchResultItem : HyvesEntity
  {
    private string idKey;

    public SearchResultItem(Hashtable entityState, string idKey)
      : base(entityState)
    {
      this.idKey = idKey;
    }

    /// <summary>
    /// The identifier of the entity.
    /// </summary>
    public string Id
    {
      get
      {
        return GetState<string>(this.idKey);
      }
    }

    /// <summary>
    /// The title.
    /// </summary>
    public string Title
    {
      get
      {
        return GetState<string>("title");
      }
    }

    /// <summary>
    /// The sub title.
    /// </summary>
    public string SubTitle
    {
      get
      {
        return GetState<string>("subtitle");
      }
    }

    /// <summary>
    /// The link.
    /// </summary>
    public string Link
    {
      get
      {
        return GetState<string>("link");
      }
    }

    /// <summary>
    /// The mobile link.
    /// </summary>
    public string MobileLink
    {
      get
      {
        return GetState<string>("mobilelink");
      }
    }

    /// <summary>
    /// The icon url.
    /// </summary>
    public string IconUrl
    {
      get
      {
        return GetState<string>("iconurl");
      }
    }
  }
}
