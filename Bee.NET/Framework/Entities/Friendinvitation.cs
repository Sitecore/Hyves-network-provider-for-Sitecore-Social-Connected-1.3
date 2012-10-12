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
	/// Represents a Friendinvitation
	/// </summary>
	public sealed class Friendinvitation : HyvesEntity
	{
		private bool createdTransformed;

		public Friendinvitation()
		{
		}

		/// <summary>
		/// The body of the friendinvitation.
		/// </summary>
		public string Body
		{
			get
			{
				return GetState<string>("body") ?? string.Empty;
			}
		}

		/// <summary>
		/// The user Id of the owner of the friendinvitation
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The user Id of the invitation
		/// </summary>
		public string TargetUserId
		{
			get
			{
				return GetState<string>("target_userid");
			}
		}

		/// <summary>
		/// The date the friendinvitation was created.
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
