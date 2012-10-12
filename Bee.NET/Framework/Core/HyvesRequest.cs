// Parts copyright (c) 2007, Nikhil Kothari. All Rights Reserved.
// Parts copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace Hyves.Service.Core
{
  /// <summary>
  /// Represents a request used to invoke a Hyves API method.
  /// </summary>
  public sealed class HyvesRequest
  {
    private const string HyvesHttpUri = "http://data.hyves-api.nl/";

    private HyvesSession session;
    private HyvesRequestParameterList parameters;
    private WebRequest asyncRequest;
    private HyvesMethod asyncMethod;

    /// <summary>
    /// Creates an new instance of a request with the specified session.
    /// </summary>
    /// <param name="session">The session to use to issue the request.</param>
    public HyvesRequest(HyvesSession session)
    {
      if (session == null)
      {
        throw new ArgumentNullException("session");
      }

      this.session = session;
      parameters = new HyvesRequestParameterList();
    }

    /// <summary>
    /// Gets the collection of parameters associated with the request.
    /// </summary>
    public HyvesRequestParameterList Parameters
    {
      get
      {
        return this.parameters;
      }
    }

    /// <summary>
    /// Starts an asynchronous call to create an authorization token. This corresponds to the
    /// Hyves auth.createToken method.
    /// </summary>
    /// <param name="callback">The async callback that is invoked when the request completes.</param>
    /// <param name="asyncState">The state to associate with the asynchronous call.</param>
    /// <returns>An async result that represents the asynchronous call.</returns>
    public IAsyncResult BeginCreateRequestToken(HyvesExpirationType expirationType, AsyncCallback callback, object asyncState)
    {
      if (this.asyncRequest != null)
      {
        throw new InvalidOperationException("A method is currently being invoked using this request.");
      }

      this.asyncRequest = CreateRequest(null, null, expirationType, this.session);
      return this.asyncRequest.BeginGetResponse(callback, asyncState);
    }

    /// <summary>
    /// Starts an asynchronous call to create a session. This corresponds to the
    /// Hyves auth.accesstoken method.
    /// </summary>
    /// <param name="requestToken">The authorization token to use to create the session.</param>
    /// <param name="requestTokenSecret"></param>
    /// <param name="callback">The async callback that is invoked when the request completes.</param>
    /// <param name="asyncState">The state to associate with the asynchronous call.</param>
    /// <returns>An async result that represents the asynchronous call.</returns>
    public IAsyncResult BeginCreateAccessToken(string requestToken, string requestTokenSecret, AsyncCallback callback, object asyncState)
    {
      if (string.IsNullOrEmpty(requestToken))
      {
        throw new ArgumentException("requestToken");
      }

      if (string.IsNullOrEmpty(requestTokenSecret))
      {
        throw new ArgumentException("requestTokenSecret");
      }

      if (this.asyncRequest != null)
      {
        throw new InvalidOperationException("A method is currently being invoked using this request.");
      }

      this.asyncRequest = CreateRequest(requestToken, requestTokenSecret, HyvesExpirationType.Default, this.session);
      return this.asyncRequest.BeginGetResponse(callback, asyncState);
    }

    /// <summary>
    /// Starts an asynchronous call to invoke the specified method.
    /// </summary>
    /// <param name="method">The name of the API method to invoke.</param>
    /// <param name="callback">The async callback that is invoked when the request completes.</param>
    /// <param name="asyncState">The state to associate with the asynchronous call.</param>
    /// <returns>An async result that represents the asynchronous call.</returns>
    public IAsyncResult BeginInvokeMethod(HyvesMethod method, AsyncCallback callback, object asyncState)
    {
      if (this.asyncRequest != null)
      {
        throw new InvalidOperationException("A method is currently being invoked using this request.");
      }

      this.asyncMethod = method;
      this.asyncRequest = CreateRequest(method, this.session, parameters);
      return this.asyncRequest.BeginGetResponse(callback, asyncState);
    }

    private static HttpWebRequest CreateRequest(string requestToken, string requestTokenSecret, HyvesExpirationType expirationType, HyvesSession session)
    {
      HyvesRequestParameterList parameters = new HyvesRequestParameterList();
      HyvesMethod method;

      if (string.IsNullOrEmpty(requestToken))
      {
        method = HyvesMethod.AuthRequesttoken;

        StringBuilder methodsStringBuilder = new StringBuilder();

        if (session.Methods.Contains(HyvesMethod.All))
        {
          Array hyvesMethodValues = Enum.GetValues(typeof(HyvesMethod));
          foreach (HyvesMethod hyvesMethod in hyvesMethodValues)
          {
            if (hyvesMethod != HyvesMethod.Unknown)
            {
              methodsStringBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(hyvesMethod)));
            }
          }

          methodsStringBuilder=methodsStringBuilder.Replace(
            string.Format("{0},", EnumHelper.GetDescription(HyvesMethod.All)),
            string.Empty);
        }
        else
        {
          foreach (HyvesMethod hyvesMethod in session.Methods)
          {
            methodsStringBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(hyvesMethod)));
          }
        }

        string methods = methodsStringBuilder.ToString();
        parameters["methods"] = methods.Substring(0, methods.Length - 1);

        parameters["expirationtype"] = EnumHelper.GetDescription(expirationType);
      }
      else
      {
        session.InitializeToken(requestToken, requestTokenSecret, DateTime.MinValue);
        method = HyvesMethod.AuthAccesstoken;
      }

      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HyvesHttpUri);
      parameters.InitializeRequest(request, method, session);

      return request;
    }

    private static HttpWebRequest CreateRequest(HyvesMethod method, HyvesSession session, HyvesRequestParameterList parameters)
    {
      return CreateRequest(method, session, parameters);
    }

    private static HttpWebRequest CreateRequest(HyvesMethod method, bool useFancyLayout, HyvesSession session, HyvesRequestParameterList parameters)
    {
      return CreateRequest(method, useFancyLayout, session, parameters);
    }

    private static HttpWebRequest CreateRequest(HyvesMethod method, bool useFancyLayout, int page, int resultsPerPage, HyvesSession session, HyvesRequestParameterList parameters)
    {
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HyvesHttpUri);
      parameters.InitializeRequest(request, method, useFancyLayout, page, resultsPerPage, session);

      return request;
    }

    /// <summary>
    /// Creates a request token. This corresponds to the Hyves auth.requesttoken method.
    /// </summary>
    /// <param name="requestTokenSecret">The request token secret to use to sign the request.</param>
    /// <returns>A new requesttoken on success.</returns>
    public string CreateRequestToken(out string tokenSecret, HyvesExpirationType expirationType)
    {
      tokenSecret = string.Empty;
      string token = null;
      HttpWebResponse webResponse = null;

      HttpWebRequest webRequest = CreateRequest(null, null, expirationType, session);
      try
      {
        webResponse = (HttpWebResponse)webRequest.GetResponse();
      }
      catch (WebException we)
      {
        webResponse = (HttpWebResponse)we.Response;
      }

      HyvesResponse requestTokenResponse = new HyvesResponse(webResponse.GetResponseStream(), HyvesMethod.Unknown);

      if (requestTokenResponse.IsError == false)
      {
        Hashtable result = requestTokenResponse.Result as Hashtable;
        if (result != null)
        {
          token = (string)result["oauth_token"];

          tokenSecret = (string)result["oauth_token_secret"];
        }
      }

      return token;
    }

    /// <summary>
    /// Creates a new access token. This corresponds to the Hyves auth.accesstoken method.
    /// </summary>
    /// <param name="requestToken">The request token to use to create the access token.</param>
    /// <param name="requestTokenSecret">The request token secret to use to sign the request.</param>
    /// <param name="tokenSecret"></param>
    /// <param name="userId">The user Id associated with the session.</param>
    /// <param name="expireDate">Date when the created access token expireDate.</param>
    /// <returns>A access token.</returns>
    public string CreateAccessToken(string requestToken, string requestTokenSecret, out string tokenSecret, out string userId, out DateTime expireDate)
    {
      tokenSecret = string.Empty;
      userId = string.Empty;
      expireDate = DateTime.Now;

      if (string.IsNullOrEmpty(requestToken))
      {
        throw new ArgumentException("requestToken");
      }

      if (string.IsNullOrEmpty(requestTokenSecret))
      {
        throw new ArgumentException("requestTokenSecret");
      }

      string token = null;

      HttpWebRequest webRequest = CreateRequest(requestToken, requestTokenSecret, HyvesExpirationType.Default, session);
      HttpWebResponse webResponse = null;

      try
      {
        webResponse = (HttpWebResponse)webRequest.GetResponse();
      }
      catch (WebException we)
      {
        webResponse = (HttpWebResponse)we.Response;
      }

      if (webResponse.StatusCode == HttpStatusCode.OK)
      {
        HyvesResponse sessionResponse = new HyvesResponse(webResponse.GetResponseStream(), HyvesMethod.Unknown);
        if (sessionResponse.IsError == false)
        {
          Hashtable result = sessionResponse.Result as Hashtable;
          if (result != null)
          {
            token = (string)result["oauth_token"];
            tokenSecret = (string)result["oauth_token_secret"];
            userId = (string)result["userid"];
            expireDate = HyvesResponse.CoerceDateTime(result["expiredate"]);
          }
        }
      }

      return token;
    }

    /// <summary>
    /// Completes an asynchronous call to create an request token.
    /// </summary>
    /// <param name="asyncResult">The async result from the corresponding BeginCreateRequestToken call.</param>
    /// <param name="requestTokenSecret"></param>
    /// <returns>A new request token on success.</returns>
    public string EndCreateRequestToken(IAsyncResult asyncResult, out string requestTokenSecret)
    {
      requestTokenSecret = string.Empty;

      if (asyncResult == null)
      {
        throw new ArgumentNullException("asyncResult");
      }
      if (asyncRequest == null)
      {
        throw new InvalidOperationException("No method is currently being invoked using this request.");
      }

      string requestToken = null;

      try
      {
        HttpWebResponse webResponse = (HttpWebResponse)asyncRequest.EndGetResponse(asyncResult);

        if (webResponse.StatusCode == HttpStatusCode.OK)
        {
          HyvesResponse requestTokenResponse = new HyvesResponse(webResponse.GetResponseStream(), HyvesMethod.Unknown);
          if (requestTokenResponse.IsError == false)
          {
            Hashtable result = (Hashtable)requestTokenResponse.Result;

            requestToken = (string)result["oauth_token"];
            requestTokenSecret = (string)result["oauth_token_secret"];
          }
        }
      }
      finally
      {
        asyncRequest = null;
      }

      return requestToken;
    }

    /// <summary>
    /// Completes an asynchronous call to create an new access token.
    /// </summary>
    /// <param name="asyncResult">The async result from the corresponding BeginCreateAccessToken call.</param>
    /// <param name="tokenSecret"></param>
    /// <param name="userId">The user Id associated with the access token.</param>
    /// <param name="expireDate">Date when the created access token expireDate.</param>
    /// <returns>A new access token.</returns>
    public string EndCreateAccessToken(IAsyncResult asyncResult, out string tokenSecret, out string userId, out DateTime expireDate)
    {
      tokenSecret = string.Empty;
      userId = string.Empty;
      expireDate = DateTime.Now;

      if (asyncResult == null)
      {
        throw new ArgumentNullException("asyncResult");
      }

      if (asyncRequest == null)
      {
        throw new InvalidOperationException("No method is currently being invoked using this request.");
      }

      string token = null;

      try
      {
        HttpWebResponse webResponse = (HttpWebResponse)asyncRequest.EndGetResponse(asyncResult);

        if (webResponse.StatusCode == HttpStatusCode.OK)
        {
          HyvesResponse accessResponse = new HyvesResponse(webResponse.GetResponseStream(), HyvesMethod.Unknown);
          if (accessResponse.IsError == false)
          {
            Hashtable result = accessResponse.Result as Hashtable;
            if (result != null)
            {
              token = (string)result["oauth_token"];
              tokenSecret = (string)result["oauth_token_secret"];
              userId = (string)result["userid"];
              expireDate = HyvesResponse.CoerceDateTime(result["expiredate"]);
            }
          }
        }
      }
      finally
      {
        asyncRequest = null;
      }

      return token;
    }

    /// <summary>
    /// Completes an asynchronous call to invoke an API method.
    /// </summary>
    /// <param name="asyncResult">The async result from the corresponding BeginCreateAccessToken call.</param>
    /// <returns>The resulting response.</returns>
    public HyvesResponse EndInvokeMethod(IAsyncResult asyncResult)
    {
      if (asyncRequest == null)
      {
        throw new InvalidOperationException("No method is currently being invoked using this request.");
      }

      HyvesResponse response = null;

      try
      {
        HttpWebResponse webResponse = (HttpWebResponse)asyncRequest.EndGetResponse(asyncResult);
        if (webResponse.StatusCode != HttpStatusCode.OK)
        {
          response = new HyvesResponse(webResponse.StatusCode, asyncMethod);
        }
        else
        {
          Stream responseStream = webResponse.GetResponseStream();
          response = new HyvesResponse(responseStream, asyncMethod);
        }
      }
      finally
      {
        asyncRequest = null;
        asyncMethod = HyvesMethod.Unknown;
      }

      session.LogResponse(response);
      return response;
    }

    /// <summary>
    /// Invokes the specified API method.
    /// </summary>
    /// <param name="method">The name of the API method to invoke.</param>
    /// <returns>The resulting response.</returns>
    public HyvesResponse InvokeMethod(HyvesMethod method)
    {
      return InvokeMethod(method, false);
    }

    /// <summary>
    /// Invokes the specified API method.
    /// </summary>
    /// <param name="method">The name of the API method to invoke.</param>
    /// <param name="method">Indicate if the response must be converted to fancy layout (smilies etc.).</param>
    /// <returns>The resulting response.</returns>
    public HyvesResponse InvokeMethod(HyvesMethod method, bool useFancyLayout)
    {
      return InvokeMethod(method, useFancyLayout, -1, -1);
    }

    /// <summary>
    /// Invokes the specified API method.
    /// </summary>
    /// <param name="method">The name of the API method to invoke.</param>
    /// <param name="method">Indicate if the response must be converted to fancy layout (smilies etc.).</param>
    /// <param name="page">The .</param>
    /// <param name="resultsPerPage">Number of results in the resultset</param>
    /// <returns>The resulting response.</returns>
    public HyvesResponse InvokeMethod(HyvesMethod method, bool useFancyLayout, int page, int resultsPerPage)
    {
      HttpWebRequest webRequest = CreateRequest(method, useFancyLayout, page, resultsPerPage, session, parameters);

      HyvesResponse response = null;

      HttpWebResponse webResponse = null;
      try
      {
        webResponse = (HttpWebResponse)webRequest.GetResponse();
      }
      catch (WebException we)
      {
        webResponse = (HttpWebResponse)we.Response;
      }

      Stream responseStream = webResponse.GetResponseStream();
      response = new HyvesResponse(responseStream, method);

      session.LogResponse(response);
      return response;
    }
  }
}
