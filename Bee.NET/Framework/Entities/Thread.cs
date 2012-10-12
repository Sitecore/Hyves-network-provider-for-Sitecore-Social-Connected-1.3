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
	/// Represents a Thread
	/// </summary>
	public sealed class Thread : HyvesEntity
	{
		private bool commentsCountTransformed;
    private bool lastCommentCreatedTransformed;
    private bool createdTransformed;

    public Thread()
		{
		}

		/// <summary>
		/// The id of the thread.
		/// </summary>
    public string ThreadId
		{
			get
			{
				return GetState<string>("threadid");
			}
		}

		/// <summary>
		/// The identifier of the hub.
		/// </summary>
		public string HubId
		{
			get
			{
        return GetState<string>("hubid");
			}
		}

		/// <summary>
		/// The title of the tip.
		/// </summary>
		public string Title
		{
			get
			{
				return GetState<string>("title") ?? string.Empty;
			}
		}

		/// <summary>
		/// The user Id of the creator of the thread.
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

    /// <summary>
    /// The date the last comment was created.
    /// </summary>
    public DateTime LastCommentCreated
    {
      get
      {
        if (this.lastCommentCreatedTransformed == false)
        {
          return TransformLastCommentCreated();
        }

        return (DateTime)this["last_commentcreated"];
      }
    }

		/// <summary>
		/// The date the thread was created.
		/// </summary>
		public DateTime Created
		{
			get
			{
				if (createdTransformed == false)
				{
					return TransformCreated();
				}
				return (DateTime)this["created"];
			}
		}

		/// <summary>
		/// The number of comments of the tip
		/// </summary>
		public int CommentsCount
		{
			get
			{
				if (commentsCountTransformed == false)
				{
					return TransformCommentsCount();
				}
				return (int)this["commentscount"];
			}
		}

    private DateTime TransformLastCommentCreated()
    {
      Debug.Assert(this.lastCommentCreatedTransformed == false);

      int timestamp = HyvesResponse.CoerceInt32(this["last_commentcreated"]);

      DateTime date = new DateTime(1970, 1, 1).AddSeconds(timestamp);
      this["last_commentcreated"] = date;

      this.lastCommentCreatedTransformed = true;

      return date;
    }

		private DateTime TransformCreated()
		{
			Debug.Assert(createdTransformed == false);

			int timestamp = HyvesResponse.CoerceInt32(this["created"]);

			DateTime date = new DateTime(1970, 1, 1).AddSeconds(timestamp);
			this["created"] = date;

			createdTransformed = true;

			return date;
		}

		private int TransformCommentsCount()
		{
			Debug.Assert(commentsCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["commentscount"]);

			this["commentscount"] = count;

			commentsCountTransformed = true;

			return count;
		}
	}
}
