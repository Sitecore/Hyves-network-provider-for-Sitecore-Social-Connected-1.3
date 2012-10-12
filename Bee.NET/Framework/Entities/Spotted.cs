// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

using Hyves.Service.Core;

namespace Hyves.Service
{
  /// <summary>
  /// Represents a target.
  /// </summary>
  public sealed class Spotted : HyvesEntity
  {
    public Spotted()
    {
    }

    /// <summary>
    /// The identifier of the spotted.
    /// </summary>
    public string SpottedId
    {
      get
      {
        return GetState<string>("spottedid");
      }
    }

    /// <summary>
    /// The identifier of the media.
    /// </summary>
    public string MediaId
    {
      get
      {
        return GetState<string>("mediaid");
      }
    }

    /// <summary>
    /// The identifier of the target user.
    /// </summary>
    public string TargetUserId
    {
      get
      {
        return GetState<string>("target_userid");
      }
    }

    /// <summary>
    /// The identifier of the user.
    /// </summary>
    public string UserId
    {
      get
      {
        return GetState<string>("userid");
      }
    }

    /// <summary>
    /// The coördinates of the area where the user is spotted.
    /// </summary>
    public string Rectangle
    {
      get
      {
        return GetState<string>("rectangle");
      }
    }
  }
}
