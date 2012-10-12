// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the service APIs that allow access to information on Hyves blog.
	/// </summary>
	public sealed class BlogsService
	{
		private HyvesSession session;

		internal BlogsService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		#region GetBlog
		/// <summary>
		/// Gets the desired information about the specified blog. This corresponds to the
		/// blogs.get Hyves method.
		/// </summary>
		/// <param name="blogId">The requested blogId.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Blog GetBlog(string blogId)
		{
			return GetBlog(blogId, HyvesBlogResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified blog. This corresponds to the
		/// blogs.get Hyves method.
		/// </summary>
		/// <param name="blogId">The requested blogId.</param>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Blog GetBlog(string blogId, HyvesBlogResponsefield responsefields)
		{
			return GetBlog(blogId, responsefields, false);
		}

		/// <summary>
		/// Gets the desired information about the specified blog. This corresponds to the
		/// blogs.get Hyves method.
		/// </summary>
		/// <param name="blogId">The requested blogId.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Blog GetBlog(string blogId, bool useFancyLayout)
		{
			return GetBlog(blogId, HyvesBlogResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified blog. This corresponds to the
		/// blogs.get Hyves method.
		/// </summary>
		/// <param name="blogId">The requested blogId.</param>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Blog GetBlog(string blogId, HyvesBlogResponsefield responsefields, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(blogId))
			{
				throw new ArgumentNullException("blogId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["blogid"] = blogId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessSingleItemResponse<Blog>("blog");
			}

			return null;
		}
		#endregion

		#region GetBlogs
		/// <summary>
		/// Gets the desired information about the specified blog. This corresponds to the
		/// blogs.get Hyves method.
		/// </summary>
		/// <param name="blogIds">The requested blogIds.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogs(Collection<string> blogIds)
		{
			return GetBlogs(blogIds, HyvesBlogResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified blog. This corresponds to the
		/// blogs.get Hyves method.
		/// </summary>
		/// <param name="blogIds">The requested blogIds.</param>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogs(Collection<string> blogIds, HyvesBlogResponsefield responsefields)
		{
			return GetBlogs(blogIds, responsefields, false);
		}

		/// <summary>
		/// Gets the desired information about the specified blog. This corresponds to the
		/// blogs.get Hyves method.
		/// </summary>
		/// <param name="blogIds">The requested blogIds.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogs(Collection<string> blogIds, bool useFancyLayout)
		{
			return GetBlogs(blogIds, HyvesBlogResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified blog. This corresponds to the
		/// blogs.get Hyves method.
		/// </summary>
		/// <param name="blogIds">The requested blogIds.</param>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogs(Collection<string> blogIds, HyvesBlogResponsefield responsefields, bool useFancyLayout)
		{
			if (blogIds == null || blogIds.Count == 0)
			{
				throw new ArgumentException("blogIds");
			}

			StringBuilder blogIdBuilder = new StringBuilder();
			if (blogIds != null)
			{
				foreach (string id in blogIds)
				{
					if (blogIdBuilder.Length != 0)
					{
						blogIdBuilder.Append(",");
					}
					blogIdBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["blogid"] = blogIdBuilder.ToString();
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Blog>("blog");
			}

			return null;
		}
		#endregion

		#region GetBlogsByTag
		/// <summary>
		/// Gets the desired blogs from the specified user by tag. This corresponds to the
		/// blogs.getByTag Hyves method.
		/// </summary>
		/// <param name="tag">The requested tag.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByTag(string tag)
		{
			return GetBlogsByTag(tag, HyvesBlogResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired blogs from the specified user by tag. This corresponds to the
		/// blogs.getByTag Hyves method.
		/// </summary>
		/// <param name="tag">The requested tag.</param>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByTag(string tag, HyvesBlogResponsefield responsefields)
		{
			return GetBlogsByTag(tag, responsefields, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired blogs from the specified user by tag. This corresponds to the
		/// blogs.getByTag Hyves method.
		/// </summary>
		/// <param name="tag">The requested tag.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByTag(string tag, bool useFancyLayout)
		{
			return GetBlogsByTag(tag, HyvesBlogResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired blogs from the specified user by tag. This corresponds to the
		/// blogs.getByTag Hyves method.
		/// </summary>
		/// <param name="tag">The requested tag.</param>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByTag(string tag, HyvesBlogResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(tag))
			{
				throw new ArgumentException("tag");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["tag"] = tag;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsGetByTag, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Blog>("blog");
			}

			return null;
		}
		#endregion

		#region GetBlogsByUser
		/// <summary>
		/// Gets the desired blogs from the specified user. This corresponds to the
		/// blogs.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByUser(string userId)
		{
			return GetBlogsByUser(userId, HyvesBlogResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired blogs from the specified user. This corresponds to the
		/// blogs.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByUser(string userId, HyvesBlogResponsefield responsefields)
		{
			return GetBlogsByUser(userId, responsefields, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired blogs from the specified user. This corresponds to the
		/// blogs.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByUser(string userId, bool useFancyLayout)
		{
			return GetBlogsByUser(userId, HyvesBlogResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired blogs from the specified user. This corresponds to the
		/// blogs.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByUser(string userId, HyvesBlogResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException("userId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["userid"] = userId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsGetByUser, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Blog>("blog");
			}

			return null;
		}
		#endregion

		#region GetBlogsForFriends
		/// <summary>
		/// Retrieves the most recent blogs for the friends of the loggedin user. 
		/// This corresponds to the blogs.getForFriends Hyves method.
		/// </summary>
		/// <returns>The information about the blogs; null if the call fails.</returns>
		public Collection<Blog> GetBlogsForFriends()
		{
			return GetBlogsForFriends(HyvesBlogResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Retrieves the most recent blogs for the friends of the loggedin user. 
		/// This corresponds to the blogs.getForFriends Hyves method.
		/// </summary>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <returns>The information about the blogs; null if the call fails.</returns>
		public Collection<Blog> GetBlogsForFriends(HyvesBlogResponsefield responsefields)
		{
			return GetBlogsForFriends(responsefields, false, -1, -1);
		}

		/// <summary>
		/// Retrieves the most recent blogs for the friends of the loggedin user. 
		/// This corresponds to the blogs.getForFriends Hyves method.
		/// </summary>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the blogs; null if the call fails.</returns>
		public Collection<Blog> GetBlogsForFriends(HyvesBlogResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsGetForFriends, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Blog>("blog");
			}

			return null;
		}
		#endregion

		#region GetBlogsByHub
		/// <summary>
		/// Gets the desired blogs from the specified hub. This corresponds to the
		/// blogs.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByHub(string hubId)
		{
			return GetBlogsByHub(hubId, HyvesBlogResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired blogs from the specified hub. This corresponds to the
		/// blogs.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByHub(string hubId, HyvesBlogResponsefield responsefields)
		{
			return GetBlogsByHub(hubId, responsefields, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired blogs from the specified hub. This corresponds to the
		/// blogs.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByHub(string hubId, bool useFancyLayout)
		{
			return GetBlogsByHub(hubId, HyvesBlogResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired blogs from the specified hub. This corresponds to the
		/// blogs.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Blog> GetBlogsByHub(string hubId, HyvesBlogResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(hubId))
			{
				throw new ArgumentNullException("hubId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["hubid"] = hubId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsGetByHub, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Blog>("blog");
			}

			return null;
		}
		#endregion

    #region GetBlogsForFriends
    /// <summary>
    /// Retrieve public blogs. This corresponds to the blogs.getPublic Hyves method.
    /// </summary>
    /// <param name="sortType">The sort type.</param>
    /// <returns>The information about the blogs; null if the call fails.</returns>
    public Collection<Blog> GetPublicBlogs(HyvesSortType sortType)
    {
      return GetPublicBlogs(sortType, HyvesTimeSpan.Forever, HyvesBlogResponsefield.All, false);
    }

    /// <summary>
    /// Retrieve public blogs. This corresponds to the blogs.getPublic Hyves method.
    /// </summary>
    /// <param name="sortType">The sort type.</param>
    /// <param name="timeSpan">The timespan to select from.</param>
    /// <returns>The information about the blogs; null if the call fails.</returns>
    public Collection<Blog> GetPublicBlogs(HyvesSortType sortType, HyvesTimeSpan timeSpan, HyvesBlogResponsefield responsefields, bool useFancyLayout)
    {
      if (sortType == HyvesSortType.NotSpecified)
      {
        throw new ArgumentOutOfRangeException("sortType");
      }

      if (timeSpan == HyvesTimeSpan.NotSpecified)
      {
        throw new ArgumentOutOfRangeException("timeSpan");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["sorttype"] = EnumHelper.GetDescription(sortType);
      request.Parameters["timespan"] = EnumHelper.GetDescription(timeSpan);
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsGetForFriends, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Blog>("blog");
      }

      return null;
    }
    #endregion

		#region GetComments
		/// <summary>
		/// Gets the comments from the specified blog. This corresponds to the
		/// blogs.getComments Hyves method.
		/// </summary>
		/// <param name="blogId">The requested blog ID.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Comment> GetComments(string blogId)
		{
			return GetComments(blogId, false, -1, -1);
		}

		/// <summary>
		/// Gets the comments from the specified blog. This corresponds to the
		/// blogs.getComments Hyves method.
		/// </summary>
		/// <param name="blogId">The requested blog ID.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Comment> GetComments(string blogId, bool useFancyLayout, int page, int resultsPerPage)
		{			
			if (string.IsNullOrEmpty(blogId))
			{
				throw new ArgumentNullException("blogId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_blogid"] = blogId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsGetComments, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessResponse<Comment>("comment");
			}

			return null;
		}
		#endregion

		#region GetRespects
		/// <summary>
		/// Gets the respects from the specified blog. This corresponds to the
		/// blogs.getRespects Hyves method.
		/// </summary>
		/// <param name="blogId">The requested blog ID.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Respect> GetRespects(string blogId)
		{
			return GetRespects(blogId, false, -1, -1);
		}

		/// <summary>
		/// Gets the respects from the specified blog. This corresponds to the
		/// blogs.getRespects Hyves method.
		/// </summary>
		/// <param name="blogId">The requested blog ID.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified blog; null if the call fails.</returns>
		public Collection<Respect> GetRespects(string blogId, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(blogId))
			{
				throw new ArgumentNullException("blogId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_blogid"] = blogId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsGetRespects, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        response.ProcessResponse<Respect>("respect");
			}

			return null;
		}
		#endregion

		#region CreateBlog
		/// <summary>
		/// Create a new blog for the current user. This corresponds to the
		/// blog.create Hyves method.
		/// </summary>
		/// <param name="title">The title of the blog.</param>
		/// <param name="body">The body of the blog.</param>
		/// <param name="visibility">The visibility of the blog.</param>
		/// <returns>The new blog; null if the call fails.</returns>
		public Blog CreateBlog(string title, string body, HyvesVisibility visibility)
		{
			return CreateBlog(title, body, visibility, HyvesBlogResponsefield.All);
		}

		/// <summary>
		/// Create a new blog for the current user. This corresponds to the
		/// blog.create Hyves method.
		/// </summary>
		/// <param name="title">The title of the blog.</param>
		/// <param name="body">The body of the blog.</param>
		/// <param name="visibility">The visibility of the blog.</param>
		/// <param name="responsefields">Get extra information from the blog.</param>
		/// <returns>The new blog; null if the call fails.</returns>
		public Blog CreateBlog(string title, string body, HyvesVisibility visibility, HyvesBlogResponsefield responsefields)
		{
      return CreateBlog(title, body, visibility, null, null, null, responsefields);
    }

    /// <summary>
    /// Create a new blog for the current user. This corresponds to the
    /// blog.create Hyves method.
    /// </summary>
    /// <param name="title">The title of the blog.</param>
    /// <param name="body">The body of the blog.</param>
    /// <param name="visibility">The visibility of the blog.</param>
    /// <param name="latitude">Latitude of the geolocation.</param>
    /// <param name="longitude">Longitude of the geolocation. </param>
    /// <param name="hubIds">List of hubIds.</param>
    /// <param name="responsefields">Get extra information from the blog.</param>
    /// <returns>The new blog; null if the call fails.</returns>
    public Blog CreateBlog(string title, string body, HyvesVisibility visibility, float? latitude, float? longitude, Collection<string> hubIds, HyvesBlogResponsefield responsefields)
    {
			if (string.IsNullOrEmpty(title))
			{
				throw new ArgumentException("title");
			}

			if (string.IsNullOrEmpty(body))
			{
				throw new ArgumentException("body");
			}

			if (visibility == HyvesVisibility.Private)
			{
				throw new ArgumentOutOfRangeException("visibility");
			}

      StringBuilder hubIdBuilder = new StringBuilder();
      if (hubIds != null)
      {
        foreach (string id in hubIds)
        {
          if (hubIdBuilder.Length != 0)
          {
            hubIdBuilder.Append(",");
          }

          hubIdBuilder.Append(id);
        }
      }

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["title"] = title;
			request.Parameters["body"] = body;
      request.Parameters["visibility"] = EnumHelper.GetDescription(visibility);
      if (latitude.HasValue)
      {
        request.Parameters["latitude"] = latitude.Value.ToString("F");
      }

      if (longitude.HasValue)
      {
        request.Parameters["longitude"] = longitude.Value.ToString("F");
      }

      if (hubIdBuilder.Length > 0)
      {
        request.Parameters["hubid"] = hubIdBuilder.ToString();
      }

			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsCreate);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Blog>("blog");
			}

			return null;
		}
		#endregion

    #region UpdateBlog

    /// <summary>
    /// Update a blog for the current user. This corresponds to the
    /// blog.update Hyves method.
    /// </summary>
    /// <param name="title">The title of the blog.</param>
    /// <param name="body">The body of the blog.</param>
    /// <param name="visibility">The visibility of the blog.</param>
    /// <param name="latitude">Latitude of the geolocation.</param>
    /// <param name="longitude">Longitude of the geolocation. </param>
    /// <param name="responsefields">Get extra information from the blog.</param>
    /// <returns>The new blog; null if the call fails.</returns>
    public Blog UpdateBlog(string blogId, string title, string body, HyvesVisibility visibility, float? latitude, float? longitude, HyvesBlogResponsefield responsefields)
    {
      if (string.IsNullOrEmpty(blogId))
      {
        throw new ArgumentException("blogId");
      }

      if (string.IsNullOrEmpty(title))
      {
        throw new ArgumentException("title");
      }

      if (string.IsNullOrEmpty(body))
      {
        throw new ArgumentException("body");
      }

      if (visibility == HyvesVisibility.Private)
      {
        throw new ArgumentOutOfRangeException("visibility");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["blogid"] = blogId;
      request.Parameters["title"] = title;
      request.Parameters["body"] = body;
      request.Parameters["visibility"] = EnumHelper.GetDescription(visibility);
      if (latitude.HasValue)
      {
        request.Parameters["latitude"] = latitude.Value.ToString("F");
      }

      if (longitude.HasValue)
      {
        request.Parameters["longitude"] = longitude.Value.ToString("F");
      }

      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsUpdate);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Blog>("blog");
      }

      return null;
    }
    #endregion

		#region CreateRespect
		/// <summary>
		/// Creates respect for an blog. This corresponds to the
		/// blog.createRespect Hyves method.
		/// </summary>
		/// <param name="targetBlogId">A single blogid.</param>
		/// <param name="respectType">The type of the respect.</param>
		/// <returns>True if the call succeeds, false if the call fails.</returns>
		public bool CreateRespect(string targetBlogId, HyvesRespectType respectType)
		{
			if (string.IsNullOrEmpty(targetBlogId))
			{
				throw new ArgumentException("targetBlogId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_blogid"] = targetBlogId;
      request.Parameters["respecttype"] = EnumHelper.GetDescription(respectType);
			
			HyvesResponse response = request.InvokeMethod(HyvesMethod.BlogsCreateRespect);
			return response.Status == HyvesResponseStatus.Succeeded;
		}
		#endregion

		#region Private methodes
    private string ConvertResponsefieldsToString(HyvesBlogResponsefield responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesBlogResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesBlogResponsefield>());
      }
      else
      {
        var userResponsefields = Enum.GetValues(typeof(HyvesBlogResponsefield));
        foreach (HyvesBlogResponsefield responseField in userResponsefields)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesBlogResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
    }
		#endregion
	}
}
