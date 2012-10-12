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
	/// Represents a Tip
	/// </summary>
	public sealed class Tip : HyvesEntity
	{
		private bool ratingTransformed;
		private bool commentsCountTransformed;
		private bool respectsCountTransformed;
		private bool createdTransformed;

    public Tip()
		{
		}

		/// <summary>
		/// The id of the tip.
		/// </summary>
		public string TipId
		{
			get
			{
				return GetState<string>("tipid");
			}
		}

		/// <summary>
		/// The category id of the tip.
		/// </summary>
		public string TipCategoryId
		{
			get
			{
				return GetState<string>("tipcategoryid");
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
		/// The body of the tip.
		/// </summary>
		public string Body
		{
			get
			{
				return GetState<string>("body") ?? string.Empty;
			}
		}

		/// <summary>
		/// The rating of the tip
		/// </summary>
		public int Rating
		{
			get
			{
				if (ratingTransformed == false)
				{
					return TransformRating();
				}
				return (int)this["rating"];
			}
		}

		/// <summary>
		/// The user Id of the owner of the tip
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The date the tip was created.
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

		/// <summary>
		/// The number of respects of the tip
		/// </summary>
		public int RespectsCount
		{
			get
			{
				if (respectsCountTransformed == false)
				{
					return TransformRespectsCount();
				}
				return (int)this["respectscount"];
			}
		}

		private int TransformRating()
		{
			Debug.Assert(ratingTransformed == false);

			int rating = HyvesResponse.CoerceInt32(this["rating"]);

			this["rating"] = rating;

			ratingTransformed = true;

			return rating;
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

		private int TransformRespectsCount()
		{
			Debug.Assert(respectsCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["respectscount"]);

			this["respectscount"] = count;

			respectsCountTransformed = true;

			return count;
		}
	}
}
