// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;

namespace Hyves.Service.Core
{
	/// <summary>
	/// Represents the status of a response resulting from invoking a
	/// Hyves API method.
	/// </summary>
	public enum HyvesResponseStatus : uint
	{
		/// <summary>
		/// The call succeeded.
		/// </summary>
		Succeeded = 0,

		/// <summary>
		/// Unknown error occurred.
		/// </summary>
		UnknownError = 1,

		/// <summary>
		/// Hyves Api method does not exist.
		/// </summary>
		UnknownMethod = 2,

		/// <summary>
		/// Hyves Api version does not exist.
		/// </summary>
		UnknownVersion = 3,
				
		/// <summary>
		/// OAuth version does not exist.
		/// </summary>
		UnknownOAuthVersion = 4,

		/// <summary>
		/// Hyves Api temporary unavailable.
		/// </summary>
		TemporaryUnavailable = 5,

		/// <summary>
		/// Hyves Api IP range restriction.
		/// </summary>
		IPRangeRestriction = 6,

		/// <summary>
		/// An illegal character was encountered.
		/// </summary>
		IncorrectCharacter = 7,

		/// <summary>
		/// Request-limit is exceeded for this IP address.
		/// </summary>
		IPAddressRequestLimitExceeded = 8,

		/// <summary>
		/// Invalid OAuth Consumer key.
		/// </summary>
		InvalidOAuthConsumerKey = 10,

		/// <summary>
		/// OAuth signature method is unsupported.
		/// </summary>
		UnsupportedOAuthSignatureMethod = 11,

		/// <summary>
		/// OAuth signature is invalid.
		/// </summary>
		IncorrectOAuthSignature = 12,

		/// <summary>
		/// Required parameter(s) are missing.
		/// </summary>
		RequiredParameterMissing = 13,
		
		/// <summary>
		/// Unknown parameter(s) given. 
		/// </summary>
		UnknownParameter = 14,

		/// <summary>
		/// Hyves Api format is not available.
		/// </summary>
		InvalidFormat = 15,

		/// <summary>
		/// OAuth timestamp is invalid. 
		/// </summary>
		InvalidOAuthTimestamp = 16,

		/// <summary>
		/// OAuth token is invalid.
		/// </summary>
		InvalidOAuthToken = 17,

		/// <summary>
		/// OAuth Consumer request-limit is exceeded.
		/// </summary>
		ConsumerRequestLimitExceeded = 18,

		/// <summary>
		/// OAuth token doesn't have permission for this method.
		/// </summary>
		NoPermission = 19,

		/// <summary>
		/// Hyves Api method temporary unavailable.
		/// </summary>
		MethodTemporaryUnavailable = 20,

		/// <summary>
		/// This error is for internal use only.
		/// </summary>
		InternalError21 = 21,

		/// <summary>
		/// This error is for internal use only.
		/// </summary>
		InternalError22 = 22,

		/// <summary>
		/// This error is for internal use only.
		/// </summary>
		InternalError23 = 23,

		/// <summary>
		/// Request replay: a request with the provided timestamp/nonce combination was made before for this oauth_consumer_key.
		/// </summary>
		RequestReplay = 24,

		/// <summary>
		/// The supplied token has expired.
		/// </summary>
		TokenExpired = 25,

		/// <summary>
		/// The ha_callback variable has an illegal format.
		/// </summary>
		InvalidCallback = 26,

		/// <summary>
		/// A requesttoken can only be used with the method auth.accesstoken.
		/// </summary>
		InvalidRequestTokenUse = 27,

		/// <summary>
		/// An accesstoken cannot be used with the methods auth.requesttoken and auth.accesstoken.
		/// </summary>
		InvalidAccessTokenUse = 28,

		/// <summary>
		/// The authorization header has an illegal format.
		/// </summary>
		IncorrectAuthorizationHeader = 29,

		/// <summary>
		/// A parameter was present more than once.
		/// </summary>
		DoubleParameter = 30,

		/// <summary>
		/// The requesttoken used to obtain an accesstoken was not authorized.
		/// </summary>
		UnauthorizedRequestToken = 31,

		/// <summary>
		/// The requesttoken used to obtain an accesstoken was declined by the user.
		/// </summary>
		DeclinedRequestToken = 32,

		/// <summary>
		/// The requested page doesn't exist.
		/// </summary>
		UnknownPage = 33,

		/// <summary>
		/// The requested results per page exceeds limits.
		/// </summary>
		ResultsPerPageExceeded = 34,

		/// <summary>
		/// The method you used does not support pagination.
		/// </summary>
		PaginationNotSupported = 35,

		/// <summary>
		/// Object does not exist.
		/// </summary>
		UnknownObject = 100,

		/// <summary>
		/// No access, OAuth token required.
		/// </summary>
		AccessDenied = 101,

		/// <summary>
		/// You are not allowed to retrieve so many objects in one request.
		/// </summary>
		ObjectLimitExceeded = 102,

		/// <summary>
		/// Item not visible for current OAuth token.
		/// </summary>
		ItemInvisible = 103,

		/// <summary>
		/// Invalid data given.
		/// </summary>
		InvalidData = 110,

		/// <summary>
		/// No title given.
		/// </summary>
		TitleMissing = 1000,

		/// <summary>
		/// Unknown visibility given.
		/// </summary>
		UnknownVisibility = 1001,

		/// <summary>
		/// Illegal gadget html given.
		/// </summary>
		InvalidGadgetHtml = 1002,

		/// <summary>
		/// No where given.
		/// </summary>
		WhereMissing = 1003,

		/// <summary>
		/// Unknown listener type given.
		/// </summary>
		UnknownListener = 1004,

		/// <summary>
		/// No listener callback given.
		/// </summary>
		CallbackMissing = 1005,

		/// <summary>
		/// There was an HTTP error in issuing the request.
		/// </summary>
		HttpError = 0xFFFF0000
	}
}
