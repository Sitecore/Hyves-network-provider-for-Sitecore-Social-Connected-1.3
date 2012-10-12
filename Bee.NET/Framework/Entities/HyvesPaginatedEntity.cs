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
	/// Represents a paginated data object returned from the Hyves service.
	/// </summary>
	public class HyvesPaginatedEntity : HyvesEntity
	{
		internal HyvesPaginatedEntity()
		{
		}

		/// <summary>
		/// The total number of results
		/// </summary>
		public int TotalResults
		{
			get
			{
				return GetState<int>("totalresults");
			}
		}

		/// <summary>
		/// The total number of pages
		/// </summary>
		public int TotalPages
		{
			get
			{
				return GetState<int>("totalpages");
			}
		}

		/// <summary>
		/// The number of results per page
		/// </summary>
		public int ResultsPerPage
		{
			get
			{
				return GetState<int>("resultsperpage");
			}
		}

		/// <summary>
		/// The current page
		/// </summary>
		public int CurrentPage
		{
			get
			{
				return GetState<int>("currentpage");
			}
		}
	}
}
