// Copyright (c) 2007 - 2010, Nikhil Kothari and Beemway. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hyves.Service.Core
{
	/// <summary>
	/// A Hyves session represents a logical user login, and
	/// allows an application to access Hyves services in the context
	/// of the specific user.
	/// </summary>
	public class HyvesSession
	{
    private const string CurrentVersion = "2.0";
		private string consumerKey;
		private string consumerSecret;
		private List<HyvesMethod> methods;

		private string token;
		private string tokenSecret;
		private DateTime expireDate;
		private string userId;

		private HyvesResponse lastResponse;

		/// <summary>
		/// Initializes an instance of a <see cref="HyvesSession" />.
		/// </summary>
		/// <param name="consumerKey">The consumer key representing the application.</param>
		/// <param name="secret">The consumer secret used to authenticate requests.</param>
		/// <param name="methods">The methods supported in this application.</param>
		public HyvesSession(string consumerKey, string consumerSecret, List<HyvesMethod> methods)
		{
			Debug.Assert(string.IsNullOrEmpty(consumerKey) == false);
			Debug.Assert(string.IsNullOrEmpty(consumerSecret) == false);

			this.consumerKey = consumerKey;
      this.consumerSecret = consumerSecret;
      this.methods = methods;
		}

		#region Properties
		/// <summary>
		/// Gets the consumer key representing the application.
		/// </summary>
		public string ConsumerKey
		{
			get
			{
        return this.consumerKey;
			}
		}

		/// <summary>
		/// Gets the shared secret used to authenticate requests.
		/// </summary>
		public string ConsumerSecret
		{
			get
			{
        return this.consumerSecret;
			}
		}

		/// <summary>
		/// The methods supported by this session.
		/// </summary>
		public List<HyvesMethod> Methods
		{
			get
			{
        return this.methods;
			}
		}

		/// <summary>
		/// Date when the session expires
		/// </summary>
		public DateTime ExpireDate
		{
			get
			{
        return this.expireDate;
			}
		}

		/// <summary>
		/// The token of the session.
		/// </summary>
		public string Token
		{
			get
			{
        return this.token;
			}
		}

		/// <summary>
		/// The token secret of the session.
		/// </summary>
		public string TokenSecret
		{
			get
			{
        return this.tokenSecret;
			}
		}

		/// <summary>
		/// The ID of the user associated with the session.
		/// </summary>
		public string UserId
		{
			get
			{
        return this.userId;
			}
		}

		/// <summary>
		/// The version of the Hyves API accessible with this session.
		/// </summary>
		public string Version
		{
			get
			{
				return CurrentVersion;
			}
		}

		/// <summary>
		/// Gets the last response returned from the Hyves service for
		/// access to the raw response, and status code for diagnostics purposes.
		/// This may be null.
		/// </summary>
		/// <returns>The last response if available.</returns>
		public HyvesResponse GetLastResponse()
		{
      return this.lastResponse;
		}
		#endregion

		/// <summary>
		/// Initializes the session information once a session has been created.
		/// </summary>
		/// <param name="token">The unique identifier of the session.</param>
		/// <param name="tokenSecret">The secret of the session.</param>
		/// <param name="ExpireDate">Date when the session expires.</param>
		public void InitializeToken(string token, string tokenSecret, DateTime expireDate)
		{
			if (string.IsNullOrEmpty(token))
			{
				throw new ArgumentException("token");
			}

			if (string.IsNullOrEmpty(tokenSecret))
			{
				throw new ArgumentException("tokenSecret");
			}

      this.token = token;
      this.tokenSecret = tokenSecret;
      this.expireDate = expireDate;
		}

		/// <summary>
		/// Initializes the user associated with this session.
		/// </summary>
		/// <param name="userId">The identifier of the user associated with the session.</param>
		public void InitializeUserId(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentException("userId");
			}

      this.userId = userId;
		}

		internal void LogResponse(HyvesResponse response)
		{
      this.lastResponse = response;
		}
	}
}
