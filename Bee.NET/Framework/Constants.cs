//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="Beemway">
//     Copyright (c) Beemway. All rights reserved.
// </copyright>
// <license>
//     Microsoft Public License (Ms-PL http://opensource.org/licenses/ms-pl.html).
//     Contributors may add their own copyright notice above.
// </license>
//-----------------------------------------------------------------------

using System;

namespace LinkedIn
{
  /// <summary>
  /// A Constants class.
  /// </summary>
  internal static class Constants
  {
    /// <summary>
    /// The base url for all the OAuth calls.
    /// </summary>
    public static readonly string ApiOAuthBaseUrl = "https://api.linkedin.com/uas/oauth/";

    /// <summary>
    /// The base url for all the API calls.
    /// </summary>
    public static readonly string ApiBaseUrl = "http://api.linkedin.com/v1/people";

    /// <summary>
    /// The method name of the Request Token method.
    /// </summary>
    public static readonly string RequestTokenMethod = "requestToken";

    /// <summary>
    /// The method name of the Authorize method.
    /// </summary>
    public static readonly string AuthorizeTokenMethod = "authorize";

    /// <summary>
    /// The method name of the Access Token method.
    /// </summary>
    public static readonly string AccessTokenMethod = "accessToken";  
    
  }
}
