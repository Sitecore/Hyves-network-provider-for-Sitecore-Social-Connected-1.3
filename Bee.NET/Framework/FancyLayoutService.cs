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
	/// Represents the service APIs to convert a string to a fancy layout
	/// </summary>
	public sealed class FancyLayoutService
	{
		private HyvesSession session;

		internal FancyLayoutService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		/// <summary>
		/// Converts a string to HTML format the same way that that is being done on the site, 
		/// including things like smilies. This corresponds to the fancylayout.parse Hyves method.
		/// </summary>
		/// <param name="layoutString">The string to convert.</param>
		/// <param name="type">An extra option how to do the conversion.</param>
		/// <returns>The converted string; null if the call fails.</returns>
		public string Parse(string layoutString, HyvesLayoutType type)
		{
			if (layoutString == null)
			{
				throw new ArgumentNullException("layoutString");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["string"] = layoutString;
			switch (type)
			{
				case HyvesLayoutType.NaturalName:
					request.Parameters["fancylayouttype"] = "naturalname";
					break;
				case HyvesLayoutType.Nickname:
					request.Parameters["fancylayouttype"] = "nickname";
					break;
				case HyvesLayoutType.Title:
					request.Parameters["fancylayouttype"] = "title";
					break;
				case HyvesLayoutType.Oneliner:
					request.Parameters["fancylayouttype"] = "oneliner";
					break;
				default:
					request.Parameters["fancylayouttype"] = "body";
					break;
			}

			HyvesResponse response = request.InvokeMethod(HyvesMethod.FancyLayoutParse, true);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
				Debug.Assert(response.Result is Hashtable);
				Hashtable result = (Hashtable)response.Result;

				Debug.Assert(result["fl_string"] is string);
				return (string)result["fl_string"];
			}

			return null;
		}
	}
}
