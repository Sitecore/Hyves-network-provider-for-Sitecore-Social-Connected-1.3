// Copyright (c) 2009 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents a upload token.
	/// </summary>
	public sealed class UploadToken : HyvesEntity
	{
    internal UploadToken(Hashtable entityState)
		{
      base.Initialize(entityState);
		}

		/// <summary>
    /// The body of the upload token.
		/// </summary>
		public string Token
		{
			get
			{
				return GetState<string>("token") ?? string.Empty;
			}
    }

    /// <summary>
    /// The ip address of the upload token.
    /// </summary>
    public string Ip
    {
      get
      {
        return GetState<string>("ip") ?? string.Empty;
      }
    }
	}
}
