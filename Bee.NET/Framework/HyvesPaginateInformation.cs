// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents paginate information
	/// </summary>
	public sealed class HyvesPaginateInformation : HyvesEntity
	{
		internal HyvesPaginateInformation(Hashtable paginateInformationState)
			: base(paginateInformationState)
		{
		}

		/// <summary>
		/// The total results of the method call.
		/// </summary>
		public int TotalResults
		{
			get
			{
				return Convert.ToInt32(GetState<string>("totalresults"));
			}
		}

		/// <summary>
		/// The total pages of the method call.
		/// </summary>
		public int TotalPages
		{
			get
			{
				return Convert.ToInt32(GetState<string>("totalpages"));
			}
		}

		/// <summary>
		/// The results per page of the method call.
		/// </summary>
		public int ResultsPerPage
		{
			get
			{
				return Convert.ToInt32(GetState<string>("resultsperpage"));
			}
		}

		/// <summary>
		/// The current page of the method call.
		/// </summary>
		public int CurrentPage
		{
			get
			{
				return Convert.ToInt32(GetState<string>("currentpage"));
			}
		}
	}
}
