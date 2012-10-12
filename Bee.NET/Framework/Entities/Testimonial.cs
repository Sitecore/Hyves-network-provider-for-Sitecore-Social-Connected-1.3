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
	/// Represents a Testimonial
	/// </summary>
	public sealed class Testimonial : HyvesEntity
	{
		private bool createdTransformed;

    public Testimonial()
		{
		}

		/// <summary>
		/// The id of the testimonial.
		/// </summary>
		public string TestimonialID
		{
			get
			{
				return GetState<string>("testimonialid");
			}
		}

		/// <summary>
		/// The user Id of the owner of the testimonial
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The user Id of the target user
		/// </summary>
		public string TargetUserId
		{
			get
			{
				return GetState<string>("target_userid");
			}
		}

		/// <summary>
		/// The body of the testimonial.
		/// </summary>
		public string Body
		{
			get
			{
				return GetState<string>("body") ?? string.Empty;
			}
		}

		/// <summary>
		/// The date the testimonial was created.
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
			Debug.Assert(createdTransformed == false);

			int timestamp = HyvesResponse.CoerceInt32(this["created"]);

			DateTime date = new DateTime(1970, 1, 1).AddSeconds(timestamp);
			this["created"] = date;

			createdTransformed = true;

			return date;
		}
	}
}
