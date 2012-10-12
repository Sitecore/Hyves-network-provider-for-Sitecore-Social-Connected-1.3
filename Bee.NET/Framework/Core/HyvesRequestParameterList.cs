// Copyright (c) 2007, Nikhil Kothari and Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Hyves.Service.Core
{
	/// <summary>
	/// Represents a collection of name/value pairs, where each pair corresponds to a parameter
	/// within requests sent to the Hyves service.
	/// </summary>
	public sealed class HyvesRequestParameterList : Dictionary<string, string>
	{
		private OAuthBase oauth = new OAuthBase();

		private void InitializeFormRequest(HttpWebRequest request)
		{			
			StringBuilder requestBuilder = new StringBuilder(512);
			foreach (KeyValuePair<string, string> param in this)
			{
				if (requestBuilder.Length != 0)
				{
					requestBuilder.Append("&");
				}

				requestBuilder.Append(param.Key);
				requestBuilder.Append("=");
        requestBuilder.Append(this.oauth.UrlEncode(param.Value));
			}

			byte[] requestBytes = Encoding.UTF8.GetBytes(requestBuilder.ToString());

			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = requestBytes.Length;

			using (Stream requestStream = request.GetRequestStream())
			{
				requestStream.Write(requestBytes, 0, requestBytes.Length);
			}
		}

    internal void InitializeRequest(HttpWebRequest request, HyvesMethod method, HyvesSession session)
		{
			InitializeRequest(request, method, false, -1, -1, session);
		}

    internal void InitializeRequest(HttpWebRequest request, HyvesMethod method, int page, int resultsPerPage, HyvesSession session)
		{
			InitializeRequest(request, method, false, page, resultsPerPage, session);
		}

    internal void InitializeRequest(HttpWebRequest request, HyvesMethod method, bool useFancyLayout, HyvesSession session)
		{
			InitializeRequest(request, method, useFancyLayout, -1, -1, session);
		}

    internal void InitializeRequest(HttpWebRequest request, HyvesMethod method, bool useFancyLayout, int page, int resultsPerPage, HyvesSession session)
		{
			request.Method = "POST";

			SetRequestMetadata(request, method, useFancyLayout, page, resultsPerPage, session);
						
			InitializeFormRequest(request);
		}

    private void SetRequestMetadata(HttpWebRequest request, HyvesMethod method, bool useFancyLayout, int page, int resultsPerPage, HyvesSession session)
		{
			string url = request.RequestUri.ToString();

			this["ha_method"] = EnumHelper.GetDescription(method);
			this["ha_version"] = session.Version;
			this["ha_format"] = "json";
			this["ha_fancylayout"] = useFancyLayout.ToString().ToLower();
			if (page > 0)
			{
				this["ha_page"] = page.ToString();
			}

			if (resultsPerPage > 0)
			{
				this["ha_resultsperpage"] = resultsPerPage.ToString();
			}

			StringBuilder requestBuilder = new StringBuilder(512);
			foreach (KeyValuePair<string, string> param in this)
			{
				if (requestBuilder.Length != 0)
				{
					requestBuilder.Append("&");
				}

				requestBuilder.Append(param.Key);
				requestBuilder.Append("=");
				requestBuilder.Append(param.Value);
			}

			string timeStamp = oauth.GenerateTimeStamp();
			string nonce = this.oauth.GenerateNonce();

			if (string.IsNullOrEmpty(session.Token) == false)
			{
				this["oauth_token"] = session.Token;
			}
			
			this["oauth_consumer_key"] = session.ConsumerKey;
			this["oauth_timestamp"] = timeStamp;
			this["oauth_nonce"] = nonce;
			this["oauth_version"] = "1.0";
			this["oauth_signature_method"] = "HMAC-SHA1";

      this["oauth_signature"] = this.oauth.GenerateSignature(new Uri(url), this, session.ConsumerKey, session.ConsumerSecret, session.Token, session.TokenSecret, request.Method, timeStamp, nonce);	
		}
	}
}
