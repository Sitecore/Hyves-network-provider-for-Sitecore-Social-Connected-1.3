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
  /// Represents a ping type.
  /// </summary>
  public sealed class PingType : HyvesEntity
  {
    public PingType()
    {
    }

    /// <summary>
    /// The identifier of the ping type.
    /// </summary>
    public string PingTypeId
    {
      get
      {
        return GetState<string>("pingtypeid");
      }
    }

    /// <summary>
    /// The body of the ping type.
    /// </summary>
    public string Body
    {
      get
      {
        return GetState<string>("body");
      }
    }
  }
}
