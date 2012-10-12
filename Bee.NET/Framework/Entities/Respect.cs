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
	/// Represents a Respect
	/// </summary>
	public sealed class Respect : HyvesEntity
	{
		private bool typeTransformed;
		private bool createdTransformed;

		public Respect()
		{
		}

		/// <summary>
		/// The id of the respect.
		/// </summary>
		public string RespectID
		{
			get
			{
				return GetState<string>("respectid");
			}
		}

		/// <summary>
		/// The type of respect.
		/// </summary>
		public HyvesRespectType Visibility
		{
			get
			{
				if (typeTransformed == false)
				{
					return TransformRespectType();
				}
				return (HyvesRespectType)this["respecttype"];
			}
		}

		/// <summary>
		/// The user Id of the owner of the respect
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The date the respect was created.
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

		private HyvesRespectType TransformRespectType()
		{
			Debug.Assert(typeTransformed == false);

			HyvesRespectType respectType = HyvesRespectType.NotSpecified;
			string state = GetState<string>("respectType") ?? String.Empty;

			if (state.Length != 0)
			{
				if (state.Equals("withprofile"))
				{
					respectType = HyvesRespectType.WithProfile;
				}
				else if (state.Equals("anonymous"))
				{
					respectType = HyvesRespectType.Anonymous;
				}
			}

			this["respectType"] = respectType;
			typeTransformed = true;

			return respectType;
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
