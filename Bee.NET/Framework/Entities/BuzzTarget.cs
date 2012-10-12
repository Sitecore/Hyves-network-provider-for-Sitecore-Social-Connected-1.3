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
  public sealed class BuzzTarget : HyvesEntity
  {
    private bool targetTypeTransformed;

    public BuzzTarget()
    {
    }

    /// <summary>
    /// The type of entity.
    /// </summary>
    public HyvesEntityType TargetType
    {
      get
      {
        if (this.targetTypeTransformed == false)
        {
          return TransformTargetType();
        }

        return (HyvesEntityType)this["targettype"];
      }
    }

    /// <summary>
    /// The identifier of the target entity.
    /// </summary>
    public string TargetId
    {
      get
      {
        return GetState<string>("targetid");
      }
    }

    private HyvesEntityType TransformTargetType()
    {
      Debug.Assert(this.targetTypeTransformed == false);

      HyvesEntityType targetType = HyvesEntityType.NotSpecified;
      string state = GetState<string>("targettype") ?? String.Empty;

      if (state.Length != 0)
      {
        switch (state)
        {
          case "blog":
            targetType = HyvesEntityType.Blog;
            break;
          case "gadget":
            targetType = HyvesEntityType.Gadget;
            break;
          case "group":
            targetType = HyvesEntityType.Group;
            break;
          case "ping":
            targetType = HyvesEntityType.Ping;
            break;
          case "song":
            targetType = HyvesEntityType.Song;
            break;
          case "www":
            targetType = HyvesEntityType.Www;
            break;
        }
      }

      this["targettype"] = targetType;
      this.targetTypeTransformed = true;

      return targetType;
    }
  }
}
