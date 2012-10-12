// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the service APIs that allow access to information on a 
	/// </summary>
	public sealed class AuthenticationService
	{
		private HyvesSession session;

		internal AuthenticationService(HyvesSession session)
		{
			Debug.Assert(session != null);
      this.session = session;
		}

		/// <summary>
    /// Revokes current accesstoken. This corresponds to the
    /// auth.revokeSelf Hyves method.
		/// </summary>
		/// <returns>The number of tokens that were revoked.</returns>
		public int RevokeSelf()
		{
			HyvesRequest request = new HyvesRequest(this.session);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.AuthRevokeSelf);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
				Debug.Assert(response.Result is Hashtable);
				Hashtable result = (Hashtable)response.Result;

        Debug.Assert(result["deletecount"] is int);

        return (int)result["deletecount"];
			}

			return -1;
		}

		/// <summary>
    /// Revokes all accesstokens for user. This corresponds to the
    /// auth.revoke Hyves method.
		/// </summary>
		/// <param name="userId">The identifier of the user.</param>
		/// <returns>The number of tokens that were rovoked.</returns>
		public int Revoke(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentException("userId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["userid"] = userId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.AuthRevoke);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
				Debug.Assert(response.Result is Hashtable);
				Hashtable result = (Hashtable)response.Result;

				Debug.Assert(result["deletecount"] is int);

				return (int)result["deletecount"];
			}

			return -1;
		}

		/// <summary>
    /// Revokes all accesstokens for Consumer. This corresponds to the
    /// auth.revokeAll Hyves method.
		/// </summary>
		/// <returns>The number of tokens that were rovoked.</returns>
		public int RevokeAll()
		{
			HyvesRequest request = new HyvesRequest(this.session);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.AuthRevokeAll);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
				Debug.Assert(response.Result is Hashtable);
				Hashtable result = (Hashtable)response.Result;

				Debug.Assert(result["deletecount"] is int);

				return (int)result["deletecount"];
			}

			return -1;
		}
	}
}
