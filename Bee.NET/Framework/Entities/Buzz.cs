// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents a Buzz
	/// </summary>  
  //TODO: Implement BuzzExtra
	public sealed class Buzz : HyvesEntity
  {
    private bool buzzActionTransformed;
    private bool buzzTypeTransformed;
		private bool createdTransformed;

    public Buzz()
		{
		}

		/// <summary>
		/// The action that took place.
		/// </summary>
		public HyvesBuzzAction Action
		{
			get
      {
        if (this.buzzActionTransformed == false)
        {
          return TransformBuzzAction();
        }

        return (HyvesBuzzAction)this["buzzaction"];
			}
		}

		/// <summary>
		/// The type of the entry.
		/// </summary>
    public HyvesEntityType Type
		{
			get
      {
        if (this.buzzTypeTransformed == false)
        {
          return TransformBuzzType();
        }

        return (HyvesEntityType)this["buzztype"];
			}
		}

		/// <summary>
		/// The target of the entry.
		/// </summary>
    public BuzzTarget Target
    {
      get
      {
        return TransformEntity<BuzzTarget>((Hashtable)this["target"]);
      }
		}

    /// <summary>
    /// The creator of the entry.
    /// </summary>
    public Creator Creator
    {
      get
      {
        return TransformEntity<Creator>((Hashtable)this["creator"]);
      }
    }

		/// <summary>
		/// The title of the entry.
		/// </summary>
    public string Title
		{
			get
			{
        return GetState<string>("title");
			}
		}

    /// <summary>
    /// The url of the entry.
    /// </summary>
    public string Url
    {
      get
      {
        return GetState<string>("title") ?? string.Empty;
      }
    }

    /// <summary>
    /// The date the entry was created.
    /// </summary>
    public DateTime Created
    {
      get
      {
        if (this.createdTransformed == false)
        {
          return TransformCreated();
        }

        return (DateTime)this["created"];
      }
    }

    /// <summary>
    /// The body of the entry.
    /// </summary>
    public string Body
    {
      get
      {
        return GetState<string>("body") ?? string.Empty;
      }
    }

    private HyvesBuzzAction TransformBuzzAction()
    {
      Debug.Assert(this.buzzActionTransformed == false);

      HyvesBuzzAction targetType = HyvesBuzzAction.NotSpecified;
      string state = GetState<string>("buzzaction") ?? String.Empty;

      if (state.Length != 0)
      {
        switch (state)
        {
          case "create":
            targetType = HyvesBuzzAction.Create;
            break;
          case "subscribe":
            targetType = HyvesBuzzAction.Subscribe;
            break;
        }
      }

      this["buzzaction"] = targetType;
      this.buzzActionTransformed = true;

      return targetType;
    }

    private HyvesEntityType TransformBuzzType()
    {
      Debug.Assert(this.buzzTypeTransformed == false);

      HyvesEntityType targetType = HyvesEntityType.NotSpecified;
      string state = GetState<string>("buzztype") ?? String.Empty;

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

      this["buzztype"] = targetType;
      this.buzzTypeTransformed = true;

      return targetType;
    }

		private DateTime TransformCreated()
		{
			Debug.Assert(this.createdTransformed == false);

			int timestamp = HyvesResponse.CoerceInt32(this["created"]);

			DateTime date = new DateTime(1970, 1, 1).AddSeconds(timestamp);
			this["created"] = date;

      this.createdTransformed = true;

			return date;
		}
	}
}
