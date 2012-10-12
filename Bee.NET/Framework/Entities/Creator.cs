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
  /// Represents a creator.
  /// </summary>
  public sealed class Creator : HyvesEntity
  {
    private bool creatorTypeTransformed;

    public Creator()
    {
    }

    /// <summary>
    /// The type of entity.
    /// </summary>
    public HyvesEntityType CreatorType
    {
      get
      {
        if (this.creatorTypeTransformed == false)
        {
          return TransformCreatorType();
        }

        return (HyvesEntityType)this["creatortype"];
      }
    }

    /// <summary>
    /// The identifier of the creator entity.
    /// </summary>
    public string CreatorId
    {
      get
      {
        return GetState<string>("creatorid");
      }
    }

    private HyvesEntityType TransformCreatorType()
    {
      Debug.Assert(this.creatorTypeTransformed == false);

      HyvesEntityType creatorType = HyvesEntityType.NotSpecified;
      string state = GetState<string>("creatortype") ?? String.Empty;

      if (state.Length != 0)
      {
        switch (state)
        {
          case "gadget":
            creatorType = HyvesEntityType.Gadget;
            break;
          case "user":
            creatorType = HyvesEntityType.User;
            break;
        }
      }

      this["creatortype"] = creatorType;
      this.creatorTypeTransformed = true;

      return creatorType;
    }
  }
}
