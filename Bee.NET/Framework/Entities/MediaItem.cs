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
	/// Represents a Hyves image.
	/// </summary>
	public sealed class MediaItem : HyvesEntity
	{
		private bool _widthTransformed;
		private bool _heightTransformed;

    public MediaItem()
		{
		}

		/// <summary>
		/// The width of the image.
		/// </summary>
		public int Width
		{
			get
			{
				if (_widthTransformed == false)
				{
					return TransformWidth();
				}
				return GetState<int>("width");
			}
		}

		/// <summary>
		/// The height of the image
		/// </summary>
		public int Height
		{
			get
			{
				if (_heightTransformed == false)
				{
					return TransformHeight();
				}
				return GetState<int>("height");
			}
		}

		/// <summary>
		/// The source of the image
		/// </summary>
		public string Src
		{
			get
			{
				return GetState<string>("src") ?? String.Empty;
			}
		}

		private int TransformWidth()
		{
			Debug.Assert(_widthTransformed == false);

			int width = HyvesResponse.CoerceInt32(this["width"]);

			this["width"] = width;
			_widthTransformed = true;

			return width;
		}

		private int TransformHeight()
		{
			Debug.Assert(_heightTransformed == false);

			int height = HyvesResponse.CoerceInt32(this["height"]);

			this["height"] = height;
			_heightTransformed = true;

			return height;
		}
	}
}
