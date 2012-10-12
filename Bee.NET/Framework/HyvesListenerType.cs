// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;

namespace Hyves.Service
{
	/// <summary>
	/// The visibility of a Hyves entity
	/// </summary>
	public enum HyvesListenerType
	{
		/// <summary>
		/// Unspecified
		/// </summary>
		NotSpecified = 0,

		/// <summary>
		/// When User revokes the accesstoken at Hyves for the ApiConsumer.
		/// </summary>
		AccesstokenRevoke = 1
	}
}
