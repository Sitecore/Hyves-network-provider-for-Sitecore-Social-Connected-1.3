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
	/// Represents the listener
	/// </summary>
	public sealed class Listener : HyvesEntity
	{
    public Listener()
		{
		}

		/// <summary>
		/// The id of the listener.
		/// </summary>
		public string ListenerID
		{
			get
			{
				return GetState<string>("listenerid");
			}
		}

		/// <summary>
		/// The type of the listener.
		/// </summary>
		public string Type
		{
			get
			{
				return GetState<string>("type") ?? string.Empty;
			}
		}

		/// <summary>
		/// The URL the listener will do the callback to.
		/// </summary>
		public string Callback
		{
			get
			{
				return GetState<string>("callback") ?? string.Empty;
			}
		}
	}
}
