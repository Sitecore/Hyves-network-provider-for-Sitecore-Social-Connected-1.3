// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;

namespace Hyves.Service
{
	/// <summary>
	/// The layout type
	/// </summary>
	public enum HyvesLayoutType
	{
		/// <summary>
		/// Unspecified
		/// </summary>
		NotSpecified = 0,

		/// <summary>
		/// Layout for natural names, such as city or personnames. No smilies or enters are converted
		/// </summary>
		NaturalName = 1,

		/// <summary>
		/// Layout for nicknames that may contain smilies
		/// </summary>
		Nickname= 2,

		/// <summary>
		/// Layout for titles that need to be clickable
		/// </summary>
		Title = 3,

		/// <summary>
		/// Layout for single lines of text. Being used on the emotion of the www on the site
		/// </summary>
		Oneliner = 4,

		/// <summary>
		/// Layout for blocks of text. Includes allmost all options, and is the most used method on the site
		/// </summary>
		Body = 5
	}
}
