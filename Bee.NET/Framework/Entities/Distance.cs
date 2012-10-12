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
	/// Represents a distance between two users.
	/// </summary>
	public sealed class Distance : HyvesEntity
	{
		private bool _degreeTransformed;

    public Distance()
		{
		}

		/// <summary>
		/// The unique ID of the user
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// The distance for given friend with the logged in user. (when degree > 3; degree will be empty.)
		/// </summary>
		public int Degree
		{
			get
			{
				if (_degreeTransformed == false)
				{
					return TransformDegree();
				}
				return GetState<int>("degree");
			}
		}

		private int TransformDegree()
		{
			Debug.Assert(_degreeTransformed == false);

			int id = HyvesResponse.CoerceInt32(this["degree"]);

			this["degree"] = id;
			_degreeTransformed = true;

			return id;
		}
	}
}
