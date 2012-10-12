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
	/// Represents a Comment
	/// </summary>
	public sealed class Comment : HyvesEntity
	{
		private bool createdTransformed;

    public Comment()
		{
		}

		/// <summary>
		/// The id of the comment.
		/// </summary>
		public string CommentId
		{
			get
			{
				return GetState<string>("commentid");
			}
		}

		/// <summary>
		/// The user Id of the owner of the comment
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The body of the comment.
		/// </summary>
		public string Body
		{
			get
			{
				return GetState<string>("body") ?? string.Empty;
			}
		}

		/// <summary>
		/// The date the comment was created.
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
