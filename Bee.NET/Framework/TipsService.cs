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
	/// Represents the service APIs that allow access to information on Hyves tip.
	/// </summary>
	public sealed class TipsService
	{
		private HyvesSession session;

		internal TipsService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		#region GetTip
		/// <summary>
		/// Gets the desired information about the specified tip. This corresponds to the
		/// tips.get Hyves method.
		/// </summary>
		/// <param name="tipId">The requested tipId.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Tip GetTip(string tipId)
		{
			return GetTip(tipId, HyvesTipResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified tip. This corresponds to the
		/// tips.get Hyves method.
		/// </summary>
		/// <param name="tipId">The requested tipId.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Tip GetTip(string tipId, HyvesTipResponsefield responsefields)
		{
			return GetTip(tipId, responsefields, false);
		}

		/// <summary>
		/// Gets the desired information about the specified tip. This corresponds to the
		/// tips.get Hyves method.
		/// </summary>
		/// <param name="tipId">The requested tipId.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Tip GetTip(string tipId, bool useFancyLayout)
		{
			return GetTip(tipId, HyvesTipResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified tip. This corresponds to the
		/// tips.get Hyves method.
		/// </summary>
		/// <param name="tipId">The requested tipId.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Tip GetTip(string tipId, HyvesTipResponsefield responsefields, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(tipId))
			{
				throw new ArgumentNullException("tipId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["tipid"] = tipId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.TipsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Tip>("tip");
			}

			return null;
		}
		#endregion

		#region GetTips
		/// <summary>
		/// Gets the desired information about the specified tip. This corresponds to the
		/// tips.get Hyves method.
		/// </summary>
		/// <param name="tipIds">The requested tipIds.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTips(Collection<string> tipIds)
		{
			return GetTips(tipIds, HyvesTipResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified tip. This corresponds to the
		/// tips.get Hyves method.
		/// </summary>
		/// <param name="tipIds">The requested tipIds.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTips(Collection<string> tipIds, HyvesTipResponsefield responsefields)
		{
			return GetTips(tipIds, responsefields, false);
		}

		/// <summary>
		/// Gets the desired information about the specified tip. This corresponds to the
		/// tips.get Hyves method.
		/// </summary>
		/// <param name="tipIds">The requested tipIds.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTips(Collection<string> tipIds, bool useFancyLayout)
		{
			return GetTips(tipIds, HyvesTipResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified tip. This corresponds to the
		/// tips.get Hyves method.
		/// </summary>
		/// <param name="tipIds">The requested tipIds.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTips(Collection<string> tipIds, HyvesTipResponsefield responsefields, bool useFancyLayout)
		{
			if (tipIds == null || tipIds.Count == 0)
			{
				throw new ArgumentNullException("tipIds");
			}

			StringBuilder tipIdBuilder = new StringBuilder();
			if (tipIds != null)
			{
				foreach (string id in tipIds)
				{
					if (tipIdBuilder.Length != 0)
					{
						tipIdBuilder.Append(",");
					}
					tipIdBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["tipid"] = tipIdBuilder.ToString();
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.TipsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Tip>("tip");
			}

			return null;
		}
		#endregion

		#region GetTipsByUser
		/// <summary>
		/// Gets the desired tips from the specified user by user. This corresponds to the
		/// tips.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByUser(string userId)
		{
			return GetTipsByUser(userId, string.Empty, HyvesTipResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired tips from the specified user by user. This corresponds to the
		/// tips.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="tipCategoryId">Filter selecting tips by tip category ID.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByUser(string userId, string tipCategoryId)
		{
			return GetTipsByUser(userId, tipCategoryId, HyvesTipResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired tips from the specified user by user. This corresponds to the
		/// tips.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByUser(string userId, HyvesTipResponsefield responsefields)
		{
			return GetTipsByUser(userId, string.Empty, responsefields, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired tips from the specified user by user. This corresponds to the
		/// tips.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="tipCategoryId">Filter selecting tips by tip category ID.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByUser(string userId, string tipCategoryId, HyvesTipResponsefield responsefields)
		{
			return GetTipsByUser(userId, tipCategoryId, responsefields, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired tips from the specified user by user. This corresponds to the
		/// tips.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByUser(string userId, bool useFancyLayout)
		{
			return GetTipsByUser(userId, string.Empty, HyvesTipResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired tips from the specified user by user. This corresponds to the
		/// tips.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="tipCategoryId">Filter selecting tips by tip category ID.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByUser(string userId, string tipCategoryId, bool useFancyLayout)
		{
			return GetTipsByUser(userId, tipCategoryId, HyvesTipResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired tips from the specified user by user. This corresponds to the
		/// tips.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByUser(string userId, HyvesTipResponsefield responsefields, bool useFancyLayout)
		{
			return GetTipsByUser(userId, string.Empty, responsefields, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired tips from the specified user by user. This corresponds to the
		/// tips.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="tipCategoryId">Filter selecting tips by tip category ID.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByUser(string userId, string tipCategoryId, HyvesTipResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentException("userId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["userid"] = userId;
      if (string.IsNullOrEmpty(tipCategoryId) == false)
      {
        request.Parameters["tipcategoryid"] = tipCategoryId;
      }

			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.TipsGetByUser, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Tip>("tip");
			}

			return null;
		}
		#endregion

		#region GetCategories
		/// <summary>
		/// Gets the tip categories. This corresponds to the
		/// tips.getCategories Hyves method.
		/// </summary>
		/// <returns>The information about the tips; null if the call fails.</returns>
		public Collection<TipCategory> GetCategories()
		{
			return GetCategories(false);
		}

		/// <summary>
		/// Gets the tip categories. This corresponds to the
		/// tips.getCategories Hyves method.
		/// </summary>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the tips; null if the call fails.</returns>
		public Collection<TipCategory> GetCategories(bool useFancyLayout)
		{
			HyvesRequest request = new HyvesRequest(this.session);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.TipsGetCategories, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<TipCategory>("tipcategory");
			}

			return null;
		}
		#endregion

		#region GetTipsForFriends
		/// <summary>
		/// Retrieves the most recent tips for the friends of the loggedin user. 
		/// This corresponds to the tips.getForFriends Hyves method.
		/// </summary>
		/// <returns>The information about the tips; null if the call fails.</returns>
		public Collection<Tip> GetTipsForFriends()
		{
			return GetTipsForFriends(string.Empty, HyvesTipResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Retrieves the most recent tips for the friends of the loggedin user. 
		/// This corresponds to the tips.getForFriends Hyves method.
		/// </summary>
		/// <param name="tipCategoryId">Filter selecting tips by tip category ID.</param>
		/// <returns>The information about the tips; null if the call fails.</returns>
		public Collection<Tip> GetTipsForFriends(string tipCategoryId)
		{
			return GetTipsForFriends(tipCategoryId, HyvesTipResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Retrieves the most recent tips for the friends of the loggedin user. 
		/// This corresponds to the tips.getForFriends Hyves method.
		/// </summary>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <returns>The information about the tips; null if the call fails.</returns>
		public Collection<Tip> GetTipsForFriends(HyvesTipResponsefield responsefields)
		{
			return GetTipsForFriends(string.Empty, responsefields, false, -1, -1);
		}

		/// <summary>
		/// Retrieves the most recent tips for the friends of the loggedin user. 
		/// This corresponds to the tips.getForFriends Hyves method.
		/// </summary>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the tips; null if the call fails.</returns>
		public Collection<Tip> GetTipsForFriends(bool useFancyLayout)
		{
			return GetTipsForFriends(string.Empty, HyvesTipResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Retrieves the most recent tips for the friends of the loggedin user. 
		/// This corresponds to the tips.getForFriends Hyves method.
		/// </summary>
		/// <param name="tipCategoryId">Filter selecting tips by tip category ID.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <returns>The information about the tips; null if the call fails.</returns>
		public Collection<Tip> GetTipsForFriends(string tipCategoryId, HyvesTipResponsefield responsefields)
		{
			return GetTipsForFriends(tipCategoryId, responsefields, false, -1, -1);
		}

		/// <summary>
		/// Retrieves the most recent tips for the friends of the loggedin user. 
		/// This corresponds to the tips.getForFriends Hyves method.
		/// </summary>
		/// <param name="tipCategoryId">Filter selecting tips by tip category ID.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the tips; null if the call fails.</returns>
		public Collection<Tip> GetTipsForFriends(string tipCategoryId, bool useFancyLayout)
		{
			return GetTipsForFriends(tipCategoryId, HyvesTipResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Retrieves the most recent tips for the friends of the loggedin user. 
		/// This corresponds to the tips.getForFriends Hyves method.
		/// </summary>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the tips; null if the call fails.</returns>
		public Collection<Tip> GetTipsForFriends(HyvesTipResponsefield responsefields, bool useFancyLayout)
		{
			return GetTipsForFriends(string.Empty, responsefields, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Retrieves the most recent tips for the friends of the loggedin user. 
		/// This corresponds to the tips.getForFriends Hyves method.
		/// </summary>
		/// <param name="tipCategoryId">Filter selecting tips by tip category ID.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the tips; null if the call fails.</returns>
		public Collection<Tip> GetTipsForFriends(string tipCategoryId, HyvesTipResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			HyvesRequest request = new HyvesRequest(this.session);
      if (string.IsNullOrEmpty(tipCategoryId))
      {
        request.Parameters["tipcategoryid"] = tipCategoryId;
      }
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.TipsGetForFriends, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Tip>("tip");
			}

			return null;
		}
		#endregion

		#region GetTipsByHub
		/// <summary>
		/// Gets the desired tips from the specified hub by hub. This corresponds to the
		/// tips.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByHub(string hubId)
		{
			return GetTipsByHub(hubId, HyvesTipResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired tips from the specified hub by hub. This corresponds to the
		/// tips.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByHub(string hubId, HyvesTipResponsefield responsefields)
		{
			return GetTipsByHub(hubId, responsefields, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired tips from the specified hub by hub. This corresponds to the
		/// tips.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByHub(string hubId, bool useFancyLayout)
		{
			return GetTipsByHub(hubId, HyvesTipResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired tips from the specified hub by hub. This corresponds to the
		/// tips.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <param name="tipCategoryId">Filter selecting tips by tip category ID.</param>
		/// <param name="responsefields">Get extra information from the tip.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Tip> GetTipsByHub(string hubId, HyvesTipResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(hubId))
			{
				throw new ArgumentException("hubId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["hubid"] = hubId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.TipsGetByHub, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Tip>("tip");
			}

			return null;
		}
		#endregion

		#region GetComments
		/// <summary>
		/// Gets the comments from the specified tip. This corresponds to the
		/// tips.getComments Hyves method.
		/// </summary>
		/// <param name="tipId">The requested tip ID.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Comment> GetComments(string tipId)
		{
			return GetComments(tipId, false, -1, -1);
		}

		/// <summary>
		/// Gets the comments from the specified tip. This corresponds to the
		/// tips.getComments Hyves method.
		/// </summary>
		/// <param name="tipId">The requested tip ID.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Comment> GetComments(string tipId, bool useFancyLayout, int page, int resultsPerPage)
		{			
			if (string.IsNullOrEmpty(tipId))
			{
				throw new ArgumentException("tipId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_tipid"] = tipId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.TipsGetComments, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Comment>("comment");
			}

			return null;
		}
		#endregion

		#region GetRespects
		/// <summary>
		/// Gets the respects from the specified tip. This corresponds to the
		/// tips.getRespects Hyves method.
		/// </summary>
		/// <param name="tipId">The requested tip ID.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Respect> GetRespects(string tipId)
		{
			return GetRespects(tipId, false, -1, -1);
		}

		/// <summary>
		/// Gets the respects from the specified tip. This corresponds to the
		/// tips.getRespects Hyves method.
		/// </summary>
		/// <param name="tipId">The requested tip ID.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified tip; null if the call fails.</returns>
		public Collection<Respect> GetRespects(string tipId, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(tipId))
			{
				throw new ArgumentException("tipId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_tipid"] = tipId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.TipsGetRespects, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessResponse<Respect>("respect");
			}

			return null;
		}
		#endregion

    #region CreateTip
    /// <summary>
    /// Creates respect for an tip. This corresponds to the
    /// tip.createRespect Hyves method.
    /// </summary>
    /// <param name="title">The title of the tip.</param>
    /// <param name="body">The body of the tip.</param>
    /// <param name="tipCategoryId">The identifier of a tip category.</param>
    /// <param name="rating">The rating of the tip.</param>
    /// <returns>True if the call succeeds, false if the call fails.</returns>
    public bool CreateTip(string title, string body, string tipCategoryId, int rating)
    {
      if (string.IsNullOrEmpty(title))
      {
        throw new ArgumentException("title");
      }

      if (string.IsNullOrEmpty(body))
      {
        throw new ArgumentException("body");
      }

      if (string.IsNullOrEmpty(tipCategoryId))
      {
        throw new ArgumentException("tipCategoryId");
      }

      if (rating < 1 || rating > 5)
      {
        throw new ArgumentOutOfRangeException("rating", "The rating must be between 1 and 5.");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["title"] = title;
      request.Parameters["body"] = body;
      request.Parameters["tipcategoryid"] = tipCategoryId;
      request.Parameters["rating"] = rating.ToString();
      
      HyvesResponse response = request.InvokeMethod(HyvesMethod.TipsCreate);
      return response.Status == HyvesResponseStatus.Succeeded;
    }
    #endregion

		#region CreateRespect
		/// <summary>
		/// Creates respect for an tip. This corresponds to the
		/// tip.createRespect Hyves method.
		/// </summary>
		/// <param name="targetTipId">A single tipid.</param>
		/// <param name="respectType">The type of the respect.</param>
		/// <returns>True if the call succeeds, false if the call fails.</returns>
		public bool CreateRespect(string targetTipId, HyvesRespectType respectType)
		{
			if (string.IsNullOrEmpty(targetTipId))
			{
				throw new ArgumentException("targetTipId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_tipid"] = targetTipId;
      request.Parameters["respecttype"] = EnumHelper.GetDescription(respectType);
			
			HyvesResponse response = request.InvokeMethod(HyvesMethod.TipsCreateRespect);
			return response.Status == HyvesResponseStatus.Succeeded;
		}
		#endregion

		#region Private methodes
		private string ConvertResponsefieldsToString(HyvesTipResponsefield responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesTipResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesTipResponsefield>());
      }
      else
      {
        var userResponsefields = Enum.GetValues(typeof(HyvesTipResponsefield));
        foreach (HyvesTipResponsefield responseField in userResponsefields)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesTipResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
		}
		#endregion
	}
}
