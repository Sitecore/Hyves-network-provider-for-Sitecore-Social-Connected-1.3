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
	/// Represents a tip category
	/// </summary>
	public sealed class TipCategory : HyvesEntity
	{
		public TipCategory()
		{
		}

		/// <summary>
		/// The id of the tip category.
		/// </summary>
		public string TipCategoryId
		{
			get
			{
				return GetState<string>("tipcategoryid");
			}
		}

		/// <summary>
		/// The name of the tip category.
		/// </summary>
		public string Name
		{
			get
			{
				return GetState<string>("name") ?? string.Empty;
			}
		}
	}
}
