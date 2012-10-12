// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the service APIs that allow access to information on Hyves threads.
	/// </summary>
	public sealed class ThreadsService
	{
		private HyvesSession session;

		internal ThreadsService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}
    
    #region GetThreads
    /// <summary>
		/// Gets the desired information about the specified thread. This corresponds to the
		/// threads.get Hyves method.
		/// </summary>
		/// <param name="threadIds">The requested threadIds.</param>
		/// <returns>The information about the specified thread; null if the call fails.</returns>
		public Collection<Thread> GetThreads(Collection<string> threadIds)
		{
			return GetThreads(threadIds, HyvesThreadResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified thread. This corresponds to the
		/// threads.get Hyves method.
		/// </summary>
		/// <param name="threadIds">The requested threadIds.</param>
		/// <param name="responsefields">Get extra information from the thread.</param>
		/// <returns>The information about the specified thread; null if the call fails.</returns>
		public Collection<Thread> GetThreads(Collection<string> threadIds, HyvesThreadResponsefield responsefields)
		{
			return GetThreads(threadIds, responsefields, false);
		}

		/// <summary>
		/// Gets the desired information about the specified thread. This corresponds to the
		/// threads.get Hyves method.
		/// </summary>
		/// <param name="threadIds">The requested threadIds.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified thread; null if the call fails.</returns>
		public Collection<Thread> GetThreads(Collection<string> threadIds, bool useFancyLayout)
		{
			return GetThreads(threadIds, HyvesThreadResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified thread. This corresponds to the
		/// threads.get Hyves method.
		/// </summary>
		/// <param name="threadIds">The requested threadIds.</param>
		/// <param name="responsefields">Get extra information from the thread.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified thread; null if the call fails.</returns>
		public Collection<Thread> GetThreads(Collection<string> threadIds, HyvesThreadResponsefield responsefields, bool useFancyLayout)
		{
			if (threadIds == null || threadIds.Count == 0)
			{
				throw new ArgumentNullException("threadIds");
			}

			StringBuilder threadIdBuilder = new StringBuilder();
			if (threadIds != null)
			{
				foreach (string id in threadIds)
				{
					if (threadIdBuilder.Length != 0)
					{
						threadIdBuilder.Append(",");
					}

					threadIdBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["threadid"] = threadIdBuilder.ToString();
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.ThreadsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Thread>("thread");
			}

			return null;
		}
    #endregion
    
    #region GetThreadsByHub
    /// <summary>
		/// Gets the desired threads from the specified user. This corresponds to the
		/// threads.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested user Id.</param>
		/// <returns>The information about the specified thread; null if the call fails.</returns>
		public Collection<Thread> GetThreadsByHub(string hubId)
		{
			return GetThreadsByHub(hubId, HyvesThreadResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired threads from the specified user. This corresponds to the
		/// threads.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the thread.</param>
		/// <returns>The information about the specified thread; null if the call fails.</returns>
		public Collection<Thread> GetThreadsByHub(string hubId, HyvesThreadResponsefield responsefields)
		{
			return GetThreadsByHub(hubId, responsefields, false);
		}

		/// <summary>
		/// Gets the desired threads from the specified user. This corresponds to the
		/// threads.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested user Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified thread; null if the call fails.</returns>
		public Collection<Thread> GetThreadsByHub(string hubId, bool useFancyLayout)
		{
			return GetThreadsByHub(hubId, HyvesThreadResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired threads from the specified user. This corresponds to the
		/// threads.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the thread.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified thread; null if the call fails.</returns>
		public Collection<Thread> GetThreadsByHub(string hubId, HyvesThreadResponsefield responsefields, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(hubId))
			{
        throw new ArgumentException("hubId cannot be null or empty.", "hubId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["hubid"] = hubId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.ThreadsGetByHub, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Thread>("thread");
			}

			return null;
		}
    #endregion

    #region GetComments
    /// <summary>
    /// Gets the comments from the specified thread. This corresponds to the
    /// threads.getComments Hyves method.
    /// </summary>
    /// <param name="threadId">The requested thread Id.</param>
    /// <returns>The information about the specified thread; null if the call fails.</returns>
    public Collection<Comment> GetComments(string threadId)
    {
      return GetComments(threadId, false, -1, -1);
    }

    /// <summary>
    /// Gets the comments from the specified thread. This corresponds to the
    /// threads.getComments Hyves method.
    /// </summary>
    /// <param name="threadId">The requested thread Id.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the specified thread; null if the call fails.</returns>
    public Collection<Comment> GetComments(string threadId, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (string.IsNullOrEmpty(threadId))
      {
        throw new ArgumentException("threadId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_threadid"] = threadId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.ThreadsGetComments, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Comment>("comment");
      }

      return null;
    }
    #endregion

		#region Private methodes
		private string ConvertResponsefieldsToString(HyvesThreadResponsefield responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesThreadResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesThreadResponsefield>());
      }
      else
      {
        var responsefieldsValues = Enum.GetValues(typeof(HyvesThreadResponsefield));
        foreach (HyvesThreadResponsefield responseField in responsefieldsValues)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesThreadResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
		}
		#endregion
	}
}
