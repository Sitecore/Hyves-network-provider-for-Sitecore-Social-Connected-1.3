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
	/// Represents a Hyves media object.
	/// </summary>
	public sealed class Media : HyvesEntity
	{
		private bool typeTransformed;
		private bool createdTransformed;
		private bool commentsCountTransformed;
		private bool respectsCountTransformed;

		public Media()
		{
		}

		/// <summary>
		/// The unique id of the media.
		/// </summary>
		public string MediaId
		{
			get
			{
				return GetState<string>("mediaid");
			}
		}

		/// <summary>
		/// The userId of the owner of the media
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}
		

		/// <summary>
		/// The title of the media.
		/// </summary>
		public string Title
		{
			get
			{
				return GetState<string>("title") ?? String.Empty;
			}
		}

		/// <summary>
		/// The description of the media.
		/// </summary>
		public string Description
		{
			get
			{
				return GetState<string>("description") ?? String.Empty;
			}
		}

		/// <summary>
		/// The type of the media
		/// </summary>
		public MediaType MediaType
		{
			get
			{
				if (typeTransformed == false)
				{
					return TransformType();
				}

				return (MediaType)this["mediatype"];
			}
		}

		/// <summary>
		/// The actual video
		/// </summary>
		public MediaItem Video
		{
			get
			{
				if (MediaType == MediaType.Image)
					return null;

        return base.TransformEntity<MediaItem>((Hashtable)this["video"]);
			}
		}

		/// <summary>
		/// The actual image
		/// </summary>
		public MediaItem Image
		{
			get
			{
				if (MediaType == MediaType.Video)
					return null;

        return base.TransformEntity<MediaItem>((Hashtable)this["image"]); 
			}
		}

		/// <summary>
		/// The fullscreen image
		/// </summary>
		public MediaItem ImageFullscreen
		{
			get
			{
				if (MediaType == MediaType.Video)
					return null;

        return base.TransformEntity<MediaItem>((Hashtable)this["image_fullscreen"]);
			}
		}

		/// <summary>
		/// The tiny version of the media
		/// </summary>
		public MediaItem IconSmall
		{
			get
      {
        return base.TransformEntity<MediaItem>((Hashtable)this["icon_small"]);
			}
		}

		/// <summary>
		/// The medium version of the media
		/// </summary>
		public MediaItem IconMedium
		{
			get
      {
        return base.TransformEntity<MediaItem>((Hashtable)this["icon_medium"]);
			}
		}

		/// <summary>
		/// The large version of the media
		/// </summary>
		public MediaItem IconLarge
		{
			get
      {
        return base.TransformEntity<MediaItem>((Hashtable)this["icon_large"]);
			}
		}

		/// <summary>
		/// The large version of the media
		/// </summary>
		public MediaItem IconExtraLarge
		{
			get
      {
        return base.TransformEntity<MediaItem>((Hashtable)this["icon_extralarge"]);
			}
		}

		/// <summary>
		/// The url of the media.
		/// </summary>
		public string Url
		{
			get
			{
				return GetState<string>("url") ?? String.Empty;
			}
		}

		/// <summary>
		/// The date the media was created.
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

		/// <summary>
		/// The number of comments of the user
		/// </summary>
		public int CommentsCount
		{
			get
			{
				if (commentsCountTransformed == false)
				{
					return TransformCommentsCount();
				}
				return (int)this["commentscount"];
			}
		}

		/// <summary>
		/// The number of respects of the user
		/// </summary>
		public int RespectsCount
		{
			get
			{
				if (respectsCountTransformed == false)
				{
					return TransformRespectsCount();
				}
				return (int)this["respectscount"];
			}
		}

		/// <summary>
		/// The tags of the blog.
		/// </summary>
		public string Tags
		{
			get
			{
				return GetState<string>("tags") ?? string.Empty;
			}
		}

		private MediaType TransformType()
		{
			Debug.Assert(typeTransformed == false);

			MediaType type = MediaType.NotSpecified;
			string state = GetState<string>("mediatype") ?? String.Empty;

			if (state.Length != 0)
			{
				if (state.Equals("image"))
				{
					type = MediaType.Image;
				}
				else if (state.Equals("video"))
				{
					type = MediaType.Video;
				}
			}

			this["mediatype"] = type;
			typeTransformed = true;

			return type;
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

		private int TransformCommentsCount()
		{
			Debug.Assert(commentsCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["commentscount"]);

			this["commentscount"] = count;

			commentsCountTransformed = true;

			return count;
		}

		private int TransformRespectsCount()
		{
			Debug.Assert(respectsCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["respectscount"]);

			this["respectscount"] = count;

			respectsCountTransformed = true;

			return count;
		}
	}
}
