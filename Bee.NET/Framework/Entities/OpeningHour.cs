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
  /// Represents a opening hour.
  /// </summary>
  public sealed class OpeningHour : HyvesEntity
  {
    public OpeningHour()
    {
    }

    /// <summary>
    /// The day of the week.
    /// </summary>
    public string DayOfWeek
    {
      get
      {
        return GetState<string>("dayofweek");
      }
    }

    /// <summary>
    /// The opening time.
    /// </summary>
    public string OpeningTime
    {
      get
      {
        return GetState<string>("openingtime");
      }
    }

    /// <summary>
    /// The close time.
    /// </summary>
    public string CloseTime
    {
      get
      {
        return GetState<string>("closetime");
      }
    }
  }
}
