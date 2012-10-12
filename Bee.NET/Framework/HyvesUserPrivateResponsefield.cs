// Copyright (c) 2009 - 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
  /// Represents the different response fields for an private user request.
	/// </summary>
	[Flags]
  public enum HyvesUserPrivateResponsefield : uint
	{
		/// <summary>
    /// All the response fields.
		/// </summary>
    All = 0,

    /// <summary>
    /// None of the response fields.
    /// </summary>
    None = 1,

		/// <summary>
		/// The original image.
		/// </summary>
    [Description("image_original")]
    ImageOriginal = 2,

		/// <summary>
		/// Whether the user is 18+.
    /// </summary>
    [Description("eighteenplus")]
    EighteenPlus = 4,

		/// <summary>
		/// The mobile number.
    /// </summary>
    [Description("mobilenumber")]
    MobileNumber = 8,

		/// <summary>
		/// Whether the mobile number is verified.
    /// </summary>
    [Description("mobilenumberverified")]
    MobileNumberVerified = 16,
				
		/// <summary>
    /// The e-mail address.
    /// </summary>
    [Description("emailaddress")]
    EmailAddress = 32,

		/// <summary>
		/// The user name.
    /// </summary>
    [Description("username")]
    UserName = 64,

		/// <summary>
		/// The e-mail hashes.
    /// </summary>
    [Description("emailhashes")]
    EmailHashes = 128,

		/// <summary>
		/// The full display name.
    /// </summary>
    [Description("fulldisplayname")]
    FullDisplayName = 256,

    [Description("blackberry_pin")]
    BlackberryPin = 512
	}
}
