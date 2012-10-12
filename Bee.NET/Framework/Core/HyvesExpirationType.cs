// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service.Core
{
	/// <summary>
	/// The expiration types
	/// </summary>
	public enum HyvesExpirationType
	{
		/// <summary>
		/// Default expiration time: 1 hour
		/// </summary>
		[Description("default")]
    Default = 0,

		/// <summary>
		/// 'Infinite' expiration time: 2 years
    /// </summary>
    [Description("infinite")]
		Infinite= 1,

		/// <summary>
		/// User can select expiration date during authorization
    /// </summary>
    [Description("user")]
		User = 2
	}
}
