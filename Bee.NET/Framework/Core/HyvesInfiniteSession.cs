// Copyright (c) 2007 - 2010, Nikhil Kothari and Beemway. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hyves.Service.Core
{
	/// <summary>
	/// Represents a Hyves "infinite" session that is used by a Hyves
	/// application that has been saved.
	/// </summary>
	internal sealed class HyvesInfiniteSession : HyvesSession
	{
		/// <summary>
		/// Creates an instance of a HyvesInfiniteSession with the
		/// specified application information and access token.
		/// </summary>
		/// <param name="consumerKey">The consumer-key used as an Consumer key.</param>
		/// <param name="consumerSecret">The consumer secret used to sign requests.</param>
		/// <param name="methods">The methods supported in this application.</param>
		/// <param name="token">The previously saved access token.</param>
		/// <param name="tokenSecret">The previously saved access token secret.</param>
		/// <param name="userId">The user Id associated with the saved access token.</param>
    public HyvesInfiniteSession(string consumerKey, string consumerSecret, List<HyvesMethod> methods, string token, string tokenSecret, string userId)
			: base(consumerKey, consumerSecret, methods)
		{
			if (string.IsNullOrEmpty(token))
			{
				throw new ArgumentException("token");
			}

			if (string.IsNullOrEmpty(tokenSecret))
			{
				throw new ArgumentException("tokenSecret");
			}

			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentException("userId");
			}

			InitializeToken(token, tokenSecret, DateTime.MinValue);
			InitializeUserId(userId);
		}
	}
}
