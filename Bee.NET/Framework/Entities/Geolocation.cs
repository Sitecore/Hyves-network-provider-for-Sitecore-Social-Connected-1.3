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
	/// Represents a Hyves geolocation.
	/// </summary>
	public sealed class Geolocation : HyvesEntity
	{
		private bool latitudeTransformed;
    private bool longitudeTransformed;
    private bool visibilityTransformed;

    public Geolocation()
		{
		}

		/// <summary>
		/// The latitude of the geolocation.
		/// </summary>
		public float Latitude
		{
			get
			{
				if (this.latitudeTransformed == false)
				{
					return TransformLatitude();
				}

				return GetState<float>("latitude");
			}
		}

		/// <summary>
		/// The longitude of the geolocation
		/// </summary>
		public float Longitude
		{
			get
			{
				if (this.longitudeTransformed == false)
				{
					return TransformLongitude();
				}

				return GetState<float>("longitude");
			}
    }

    /// <summary>
    /// The visibility of the geo location.
    /// </summary>
    public bool Visibility
    {
      get
      {
        if (this.visibilityTransformed == false)
        {
          return TransformVisibility();
        }

        return (bool)this["visibility"];
      }
    }

		private float TransformLatitude()
		{
			Debug.Assert(latitudeTransformed == false);

			float latitude = HyvesResponse.CoerceFloat(this["latitude"]);

			this["latitude"] = latitude;
			latitudeTransformed = true;

			return latitude;
		}

		private float TransformLongitude()
		{
			Debug.Assert(longitudeTransformed == false);

			float longitude = HyvesResponse.CoerceFloat(this["longitude"]);

			this["longitude"] = longitude;
			longitudeTransformed = true;

			return longitude;
    }

    private bool TransformVisibility()
    {
      Debug.Assert(this.visibilityTransformed == false);

      bool visibility = HyvesResponse.CoerceBoolean(this["visibility"]);

      this["visibility"] = visibility;

      this.visibilityTransformed = true;

      return visibility;
    }
	}
}
