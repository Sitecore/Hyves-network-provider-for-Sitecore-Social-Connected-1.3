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
	/// Represents a Hyves hub address.
	/// </summary>
	public sealed class HubAddress : HyvesEntity
	{
    public HubAddress()
		{
		}

    /// <summary>
    /// The identifier of the city.
    /// </summary>
    public string CityID
    {
      get
      {
        return GetState<string>("cityid") ?? string.Empty;
      }
    }

    /// <summary>
    /// The identifier of the country.
    /// </summary>
    public string CountryID
    {
      get
      {
        return GetState<string>("countryid") ?? string.Empty;
      }
    }

    /// <summary>
    /// The phone number.
    /// </summary>
    public string PhoneNumber
    {
      get
      {
        return GetState<string>("phonenumber") ?? string.Empty;
      }
    }

    /// <summary>
    /// The street.
    /// </summary>
    public string Street
    {
      get
      {
        return GetState<string>("street") ?? string.Empty;
      }
    }

    /// <summary>
    /// The street number.
    /// </summary>
    public string StreetNumber
    {
      get
      {
        return GetState<string>("streetnumber") ?? string.Empty;
      }
    }

    /// <summary>
    /// The zip code.
    /// </summary>
    public string ZipCode
    {
      get
      {
        return GetState<string>("zipcode") ?? string.Empty;
      }
    }

    /// <summary>
    /// The url.
    /// </summary>
    public string Url
    {
      get
      {
        return GetState<string>("url") ?? string.Empty;
      }
    }
	}
}
