// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Net;
using System.Web;

using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the service APIs that allow access to information on Hyves albums.
	/// </summary>
	public sealed class AlbumsService
	{
		private HyvesSession session;

    internal AlbumsService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}
    
    #region GetAlbums
    /// <summary>
    /// Gets the desired albums from the specified albums. This corresponds to the albums.get Hyves method.
    /// </summary>
    /// <param name="albumId">The requested album ID.</param>
    /// <param name="responsefields">Get extra information from the requested album.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the desired albums; null if the call fails.</returns>
    public Collection<Album> GetAlbums(string albumId, HyvesAlbumResponsefield responseFields, bool useFancyLayout)
    {
      if (string.IsNullOrEmpty(albumId))
      {
        throw new ArgumentException("userId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["albumid"] = albumId;
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responseFields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.AlbumsGet, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Album>("album");
      }

      return null;
    }

		/// <summary>
		/// Gets the desired albums from a user. This corresponds to the albums.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <returns>The information about the desired albums; null if the call fails.</returns>
		public Collection<Album> GetAlbums(string userId)
		{
			return GetAlbums(userId, false);
		}

		/// <summary>
		/// Gets the desired albums from a user. This corresponds to the albums.getByUser Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the desired albums; null if the call fails.</returns>
		public Collection<Album> GetAlbums(string userId, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException("userId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["userid"] = userId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.AlbumsGetByUser, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessResponse<Album>("album");
			}

			return null;
		}
		#endregion

    #region GetBuiltinAlbums
    /// <summary>
    /// Gets the builtin albums from a user. This corresponds to the albums.getBuiltin Hyves method.
    /// </summary>
    /// <param name="visibility">The visibility of the albums.</param>
    /// <param name="responsefields">Get extra information from the requested album.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the desired albums; null if the call fails.</returns>
    public Collection<Album> GetBuiltinAlbums(HyvesVisibility visibility, HyvesAlbumResponsefield responseFields, bool useFancyLayout)
    {
      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["visibility"] = EnumHelper.GetDescription(visibility);
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responseFields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.AlbumsGetBuiltin, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Album>("album");
      }

      return null;
    }
    #endregion

    #region GetAlbumsByHub
    /// <summary>
		/// Gets all visible albums of the specified hub. This corresponds to the
		/// albums.getByHub Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <returns>The information about the specified user; null if the call fails.</returns>
    public Collection<Album> GetAlbumsByHub(string hubId)
		{
			return GetAlbumsByHub(hubId, false);
		}

		/// <summary>
		/// Gets all visible albums of the specified hub. This corresponds to the
		/// albums.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified album; null if the call fails.</returns>
		public Collection<Album> GetAlbumsByHub(string hubId, bool useFancyLayout)
		{
			return GetAlbumsByHub(hubId, useFancyLayout, -1, -1);
		}

		/// <summary>
		/// Gets all visible albums of the specified hub. This corresponds to the
		/// albums.getByHub Hyves method.
		/// </summary>
		/// <param name="hubId">The requested hub Id.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <param name="page">The requested page.</param>
		/// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the albums; null if the call fails.</returns>
    public Collection<Album> GetAlbumsByHub(string hubId, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(hubId))
			{
				throw new ArgumentNullException("hubId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["hubid"] = hubId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.AlbumsGetByHub, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessResponse<Album>("album");
			}

			return null;
		}
		#endregion

    #region GetAlbumsByUser
    /// <summary>
    /// Gets all visible albums of the specified user. This corresponds to the
    /// albums.getByUser Hyves method.
    /// </summary>
    /// <param name="userId">The identifier for the requested user.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the albums; null if the call fails.</returns>
    public Collection<Album> GetAlbumsByUser(string userId, bool useFancyLayout)
    {
      return GetAlbumsByUser(userId, useFancyLayout, -1, -1);
    }

    /// <summary>
    /// Gets all visible albums of the specified user. This corresponds to the
    /// albums.getByUser Hyves method.
    /// </summary>
    /// <param name="userId">The identifier for the requested user.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the albums; null if the call fails.</returns>
    public Collection<Album> GetAlbumsByUser(string userId, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (string.IsNullOrEmpty(userId))
      {
        throw new ArgumentException("userId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["userid"] = userId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.AlbumsGetByUser, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Album>("album");
      }

      return null;
    }
    #endregion

    #region CreateAlbum
    /// <summary>
    /// Creates a new album. This corresponds to the
    /// albums.create Hyves method.
    /// </summary>
    /// <param name="title">Title of the album. </param>
    /// <param name="visibility">The visibility of the album.</param>
    /// <param name="printability">The printability of the album.</param>
    /// <param name="responseFields">Get extra information from the created album.</param>
    /// <returns>True if the call succeeds, false if the call fails.</returns>
    public bool CreateAlbum(string title, HyvesVisibility visibility, HyvesVisibility printability, HyvesAlbumResponsefield responseFields)
    {
      if (string.IsNullOrEmpty(title))
      {
        throw new ArgumentNullException("title");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["title"] = title;
      request.Parameters["visibility"] = EnumHelper.GetDescription(visibility);
      request.Parameters["printability"] = EnumHelper.GetDescription(printability);
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responseFields);
      
      HyvesResponse response = request.InvokeMethod(HyvesMethod.AlbumsCreate);
      return response.Status == HyvesResponseStatus.Succeeded;
    }
    #endregion
    
    #region AddMediaToAlbum
    public bool AddMediaToAlbum(string albumId, Collection<string> mediaIds)
    {
      if (string.IsNullOrEmpty(albumId))
      {
        throw new ArgumentNullException("albumId");
      }

      if (mediaIds == null)
      {
        throw new ArgumentNullException("mediaIds");
      }

      StringBuilder mediaIdBuilder = new StringBuilder();
      foreach (string id in mediaIds)
      {
        if (mediaIdBuilder.Length != 0)
        {
          mediaIdBuilder.Append(",");
        }

        mediaIdBuilder.Append(id);
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["albumid"] = albumId;
      request.Parameters["mediaid"] = mediaIdBuilder.ToString();

      HyvesResponse response = request.InvokeMethod(HyvesMethod.AlbumsAddMedia);
      return response.Status == HyvesResponseStatus.Succeeded;
    }
    #endregion

    #region RemoveMediaFromAlbum
    public bool RemoveMediaFromAlbum(string albumId, Collection<string> mediaIds)
    {
      if (string.IsNullOrEmpty(albumId))
      {
        throw new ArgumentException("albumId");
      }

      if (mediaIds == null)
      {
        throw new ArgumentNullException("mediaIds");
      }

      StringBuilder mediaIdBuilder = new StringBuilder();
      foreach (string id in mediaIds)
      {
        if (mediaIdBuilder.Length != 0)
        {
          mediaIdBuilder.Append(",");
        }

        mediaIdBuilder.Append(id);
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["albumid"] = albumId;
      request.Parameters["mediaid"] = mediaIdBuilder.ToString();

      HyvesResponse response = request.InvokeMethod(HyvesMethod.AlbumsRemoveMedia);
      return response.Status == HyvesResponseStatus.Succeeded;
    }
    #endregion
    
    #region Private methodes
    private string ConvertResponsefieldsToString(HyvesAlbumResponsefield responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesAlbumResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesAlbumResponsefield>());
      }
      else
      {
        var userResponsefields = Enum.GetValues(typeof(HyvesAlbumResponsefield));
        foreach (HyvesAlbumResponsefield responseField in userResponsefields)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesAlbumResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
    }

    private string ConvertResponsefieldsToString(HyvesMediaResponsefield responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesMediaResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesMediaResponsefield>());
      }
      else
      {
        var userResponsefields = Enum.GetValues(typeof(HyvesMediaResponsefield));
        foreach (HyvesMediaResponsefield responseField in userResponsefields)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesMediaResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
		}    
		#endregion
	}
}
