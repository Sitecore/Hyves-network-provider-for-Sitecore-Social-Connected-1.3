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
  /// Represents a smiley category.
  /// </summary>
  public sealed class SmileyCategory : HyvesEntity
  {
    public SmileyCategory()
    {
    }
    
    /// <summary>
    /// The name of the category.
    /// </summary>
    public string Name
    {
      get
      {
        return GetState<string>("name");
      }
    }

    /// <summary>
    /// The body of the category.
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
