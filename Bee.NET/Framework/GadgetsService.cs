// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the service APIs that allow access to information on Hyves gadgets.
	/// </summary>
	public sealed class GadgetsService
	{
		private HyvesSession session;

		internal GadgetsService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		#region GetGadget
		/// <summary>
		/// Gets the desired information about the specified gadget. This corresponds to the
		/// gadgets.get Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadgetId.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Gadget GetGadget(string gadgetId)
		{
			return GetGadget(gadgetId, HyvesGadgetResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified gadget. This corresponds to the
		/// gadgets.get Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadgetId.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Gadget GetGadget(string gadgetId, bool useFancyLayout)
		{
			return GetGadget(gadgetId, HyvesGadgetResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified gadget. This corresponds to the
		/// gadgets.get Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadgetId.</param>
		/// <param name="responsefields">Get extra information from the gadget.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Gadget GetGadget(string gadgetId, HyvesGadgetResponsefield responsefield)
		{
			return GetGadget(gadgetId, responsefield, false);
		}

		/// <summary>
		/// Gets the desired information about the specified gadget. This corresponds to the
		/// gadgets.get Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadgetId.</param>
		/// <param name="responsefields">Get extra information from the gadget.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Gadget GetGadget(string gadgetId, HyvesGadgetResponsefield responsefields, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(gadgetId))
			{
				throw new ArgumentNullException("gadgetId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["gadgetid"] = gadgetId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.GadgetsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessSingleItemResponse<Gadget>("gadget");
      }

			return null;
		}
		#endregion

		#region GetGadgets
		/// <summary>
		/// Gets the desired information about the specified gadget. This corresponds to the
		/// gadgets.get Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadgetIds.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgets(Collection<string> gadgetIds)
		{
			return GetGadgets(gadgetIds, HyvesGadgetResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired information about the specified gadget. This corresponds to the
		/// gadgets.get Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadgetIds.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgets(Collection<string> gadgetIds, bool useFancyLayout)
		{
			return GetGadgets(gadgetIds, HyvesGadgetResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired information about the specified gadget. This corresponds to the
		/// gadgets.get Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadgetIds.</param>
		/// <param name="responsefields">Get extra information from the gadget.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgets(Collection<string> gadgetIds, HyvesGadgetResponsefield responsefield)
		{
			return GetGadgets(gadgetIds, responsefield, false);
		}

		/// <summary>
		/// Gets the desired information about the specified gadget. This corresponds to the
		/// gadgets.get Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadgetIds.</param>
		/// <param name="responsefields">Get extra information from the gadget.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgets(Collection<string> gadgetIds, HyvesGadgetResponsefield responsefields, bool useFancyLayout)
		{
			if (gadgetIds == null || gadgetIds.Count == 0)
			{
				throw new ArgumentNullException("gadgetIds");
			}

			StringBuilder gadgetIdBuilder = new StringBuilder();
			if (gadgetIds != null)
			{
				foreach (string id in gadgetIds)
				{
					if (gadgetIdBuilder.Length != 0)
					{
						gadgetIdBuilder.Append(",");
					}
					gadgetIdBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["gadgetid"] = gadgetIdBuilder.ToString();
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.GadgetsGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Gadget>("gadget");
			}

			return null;
		}
		#endregion

		#region GetGadgetsByUser
		/// <summary>
		/// Gets the desired gadgets from the specified user. This corresponds to the
		/// gadgets.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgetsByUser(string userId)
		{
			return GetGadgetsByUser(userId, HyvesGadgetResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired gadgets from the specified user. This corresponds to the
		/// gadgets.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgetsByUser(string userId, bool useFancyLayout)
		{
			return GetGadgetsByUser(userId, HyvesGadgetResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired gadgets from the specified user. This corresponds to the
		/// gadgets.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgetsByUser(string userId, HyvesGadgetResponsefield responsefield)
		{
			return GetGadgetsByUser(userId, responsefield, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired gadgets from the specified user. This corresponds to the
		/// gadgets.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgetsByUser(string userId, HyvesGadgetResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException("userId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["userid"] = userId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.GadgetsGetByUser, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Gadget>("gadget");
			}

			return null;
		}
		#endregion

		#region GetGadgetsByHub
		/// <summary>
		/// Gets the desired gadgets from the specified hub. This corresponds to the
		/// gadgets.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgetsByHub(string hubId)
		{
			return GetGadgetsByHub(hubId, HyvesGadgetResponsefield.All, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired gadgets from the specified hub. This corresponds to the
		/// gadgets.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgetsByHub(string hubId, bool useFancyLayout)
		{
			return GetGadgetsByHub(hubId, HyvesGadgetResponsefield.All, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets the desired gadgets from the specified hub. This corresponds to the
		/// gadgets.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgetsByHub(string hubId, HyvesGadgetResponsefield responsefield)
		{
			return GetGadgetsByHub(hubId, responsefield, false, -1, -1);
		}

		/// <summary>
		/// Gets the desired gadgets from the specified hub. This corresponds to the
		/// gadgets.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Gadget> GetGadgetsByHub(string hubId, HyvesGadgetResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(hubId))
			{
				throw new ArgumentNullException("hubId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["hubid"] = hubId;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.GadgetsGetByHub, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Gadget>("gadget");
			}

			return null;
		}
		#endregion

		#region GetComments
		/// <summary>
		/// Gets the comments from the specified gadget. This corresponds to the
		/// gadgets.getComments Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadget ID.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Comment> GetComments(string gadgetId)
		{
			return GetComments(gadgetId, false);
		}

		/// <summary>
		/// Gets the comments from the specified gadget. This corresponds to the
		/// gadgets.getComments Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadget ID.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Comment> GetComments(string gadgetId, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(gadgetId))
			{
				throw new ArgumentException("gadgetId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_gadgetid"] = gadgetId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.GadgetsGetComments, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessResponse<Comment>("comment");
			}

			return null;
		}
		#endregion

		#region GetRespects
		/// <summary>
		/// Gets the respects from the specified gadget. This corresponds to the
		/// gadgets.getRespects Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadget ID.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Respect> GetRespects(string gadgetId)
		{
			return GetRespects(gadgetId, false);
		}

		/// <summary>
		/// Gets the respects from the specified gadget. This corresponds to the
		/// gadgets.getRespects Hyves method.
		/// </summary>
		/// <param name="gadgetId">The requested gadget ID.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified gadget; null if the call fails.</returns>
		public Collection<Respect> GetRespects(string gadgetId, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(gadgetId))
			{
				throw new ArgumentException("gadgetId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_gadgetid"] = gadgetId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.GadgetsGetRespects, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessResponse<Respect>("respect");
			}

			return null;
		}
		#endregion

		#region CreateGadget
		/// <summary>
		/// Create a gadget for the current user. This corresponds to the
		/// gadgets.create Hyves method.
		/// </summary>
		/// <param name="title">The title of the gadget.</param>
		/// <param name="html">The html of the gadget.</param>
		/// <param name="visibility">The visibility of the gadget.</param>
		/// <param name="mayCopy">Allow to copy this gadget.</param>
		/// <returns>The new gadget; null if the call fails.</returns>
		public Gadget CreateGadget(string title, string html, HyvesVisibility visibility, bool mayCopy)
		{
			if (string.IsNullOrEmpty(title))
			{
				throw new ArgumentNullException("title");
			}
			if (string.IsNullOrEmpty(html))
			{
				throw new ArgumentNullException("html");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["title"] = title;
			request.Parameters["html"] = html;
      request.Parameters["visibility"] = EnumHelper.GetDescription(visibility);
			request.Parameters["maycopy"] = mayCopy.ToString();

			HyvesResponse response = request.InvokeMethod(HyvesMethod.GadgetsCreate);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Gadget>("gadget");
			}

			return null;
		}
    
    /// <summary>
    /// Create a gadget by XML (OpenSocial) for the current user. This corresponds to the
    /// gadgets.createByXML Hyves method.
    /// </summary>
    /// <param name="specUrl">Url where the gadget spec for the gadget can be found.</param>
    /// <param name="visibility">The visibility of the gadget.</param>
    /// <param name="onProfilePage">Show gadget on profilepage.</param>
    /// <param name="onHomepage">Show gadget on the homepage.</param>
    /// <returns>The new gadget; null if the call fails.</returns>
    public Gadget CreateGadget(string specUrl, HyvesVisibility visibility, bool onProfilePage, bool onHomepage)
    {
      if (string.IsNullOrEmpty(specUrl))
      {
        throw new ArgumentNullException("specUrl");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["specurl"] = specUrl;
      request.Parameters["visibility"] = EnumHelper.GetDescription(visibility);
      request.Parameters["onprofilepage"] = onProfilePage.ToString();
      request.Parameters["onhomepage"] = onHomepage.ToString();

      HyvesResponse response = request.InvokeMethod(HyvesMethod.GadgetsCreateByXML);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Gadget>("gadget");
      }

      return null;
    }
		#endregion

		#region CreateRespect
		/// <summary>
		/// Creates respect for an gadget. This corresponds to the
		/// gadgets.createRespect Hyves method.
		/// </summary>
		/// <param name="targetGadgetID">A single gadgetid.</param>
		/// <param name="respectType">The type of the respect.</param>
		/// <returns>True if the call succeeds, false if the call fails.</returns>
		public bool CreateRespect(string targetGadgetID, HyvesRespectType respectType)
		{
			if (string.IsNullOrEmpty(targetGadgetID))
			{
				throw new ArgumentNullException("targetGadgetID");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_gadgetid"] = targetGadgetID;
      request.Parameters["respecttype"] = EnumHelper.GetDescription(respectType);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.GadgetsCreateRespect);
			return response.Status == HyvesResponseStatus.Succeeded;
		}
		#endregion

		#region Private methodes
		private string ConvertResponsefieldsToString(HyvesGadgetResponsefield responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesGadgetResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesGadgetResponsefield>());
      }
      else
      {
        var userResponsefields = Enum.GetValues(typeof(HyvesGadgetResponsefield));
        foreach (HyvesGadgetResponsefield responseField in userResponsefields)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesGadgetResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
		}
		#endregion
	}
}
