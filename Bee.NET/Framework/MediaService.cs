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
	/// Represents the service APIs that allow access to information on Hyves media.
	/// </summary>
	public sealed class MediaService
	{
		private const string AllResponsefields = "commentscount,respectscount,tags,fancylayouttag,";
		private HyvesSession session;

		internal MediaService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		#region GetMedia
		/// <summary>
		/// Gets the desired media. This corresponds to the media.get Hyves method.
		/// </summary>
		/// <param name="mediaIds">The list of requested media IDs.</param>
		/// <returns>The information about the desired media; null if the call fails.</returns>
		public Collection<Media> GetMedia(Collection<string> mediaIds)
		{
			return GetMedia(mediaIds, HyvesMediaResponsefield.All, false);
		}

		/// <summary>
		/// Gets the desired media. This corresponds to the media.get Hyves method.
		/// </summary>
		/// <param name="mediaIds">The list of requested media IDs.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the desired media; null if the call fails.</returns>
    public Collection<Media> GetMedia(Collection<string> mediaIds, bool useFancyLayout)
		{
			return GetMedia(mediaIds, HyvesMediaResponsefield.All, useFancyLayout);
		}

		/// <summary>
		/// Gets the desired media. This corresponds to the media.get Hyves method.
		/// </summary>
		/// <param name="mediaIds">The list of requested media IDs.</param>
		/// <param name="responsefields">Get extra information from the media.</param>
		/// <returns>The information about the desired media; null if the call fails.</returns>
    public Collection<Media> GetMedia(Collection<string> mediaIds, HyvesMediaResponsefield responsefields)
		{
			return GetMedia(mediaIds, responsefields, false);
		}

		/// <summary>
		/// Gets the desired media. This corresponds to the media.get Hyves method.
		/// </summary>
		/// <param name="mediaIds">The list of requested media IDs.</param>
		/// <param name="responsefields">Get extra information from the media.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the desired media; null if the call fails.</returns>
		public Collection<Media> GetMedia(Collection<string> mediaIds, HyvesMediaResponsefield responsefields, bool useFancyLayout)
		{
			if (mediaIds == null || mediaIds.Count == 0)
			{
				throw new ArgumentNullException("mediaIds");
			}
			
			StringBuilder mediaIdBuilder = new StringBuilder();
			if (mediaIds != null)
			{
				foreach (string id in mediaIds)
				{
					if (mediaIdBuilder.Length != 0)
					{
						mediaIdBuilder.Append(",");
					}
					mediaIdBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["mediaid"] = mediaIdBuilder.ToString();
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaGet, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessResponse<Media>("media");
			}

			return null;
		}
		#endregion
    
    #region AddSpotted
    /// <summary>
    /// Adds a spotted user to the media. This corresponds to the
    /// media.addSpotted Hyves method.
    /// </summary>
    /// <param name="mediaId">The identifier for the media. </param>
    /// <param name="targetUserId">The user that is spotted.</param>
    /// <returns>True if the call succeeds, false if the call fails.</returns>
    public bool AddSpotted(string mediaId, string targetUserId)
    {
      if (string.IsNullOrEmpty(mediaId))
      {
        throw new ArgumentException("mediaId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["mediaid"] = mediaId;
      request.Parameters["target_userid"] = targetUserId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaAddSpotted);
      return response.Status == HyvesResponseStatus.Succeeded;
    }
    #endregion
    
		#region GetMediaByAlbum
		/// <summary>
		/// Gets the desired media from an album. This corresponds to the media.getByAlbum Hyves method.
		/// </summary>
		/// <param name="albumId">The requested album ID.</param>
		/// <returns>The information about the desired media; null if the call fails.</returns>
		public Collection<Media> GetMediaByAlbum(string albumId)
		{
			return GetMediaByAlbum(albumId, HyvesMediaResponsefield.All, false, 1, 100);
		}

		/// <summary>
		/// Gets the desired media from an album. This corresponds to the media.getByAlbum Hyves method.
		/// </summary>
		/// <param name="albumId">The requested album ID.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the desired media; null if the call fails.</returns>
		public Collection<Media> GetMediaByAlbum(string albumId, bool useFancyLayout)
		{
			return GetMediaByAlbum(albumId, HyvesMediaResponsefield.All, useFancyLayout, 1, 100);
		}

		/// <summary>
		/// Gets the desired media from an album. This corresponds to the media.getByAlbum Hyves method.
		/// </summary>
		/// <param name="albumId">The requested album ID.</param>
		/// <param name="responsefields">Get extra information from the media.</param>
		/// <returns>The information about the desired media; null if the call fails.</returns>
		public Collection<Media> GetMediaByAlbum(string albumId, HyvesMediaResponsefield responsefields)
		{
			return GetMediaByAlbum(albumId, responsefields, false, 1, 100);
		}

		/// <summary>
		/// Gets the desired media from an album. This corresponds to the media.getByAlbum Hyves method.
		/// </summary>
		/// <param name="albumId">The requested album ID.</param>
		/// <param name="responsefields">Get extra information from the media.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the desired media; null if the call fails.</returns>
    public Collection<Media> GetMediaByAlbum(string albumId, HyvesMediaResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(albumId))
			{
				throw new ArgumentNullException("albumId");
			}
						
			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["albumid"] = albumId;
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaGetByAlbum, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessResponse<Media>("media");
			}

			return null;
		}
		#endregion

    #region GetMediaByLoggedin
    /// <summary>
    /// Gets the desired media from the loggedin user. This corresponds to the media.getByLoggedin Hyves method.
    /// </summary>
    /// <param name="responsefields">Get extra information from the media.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the desired media; null if the call fails.</returns>
    public Collection<Media> GetMediaByLoggedin(HyvesMediaResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
    {
      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaGetByLoggedin, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Media>("media");
      }

      return null;
    }
    #endregion

		#region GetMediaByTag
		/// <summary>
		/// Gets the desired media from the specified user by tag. This corresponds to the
		/// media.getByTag Hyves method.
		/// </summary>
		/// <param name="tag">The requested tag.</param>
		/// <returns>The information about the specified media; null if the call fails.</returns>
		public Collection<Media> GetMediaByTag(string tag)
		{
			return GetMediaByTag(tag, HyvesMediaResponsefield.All, false, 1, 100);
		}

		/// <summary>
		/// Gets the desired media from the specified user by tag. This corresponds to the
		/// media.getByTag Hyves method.
		/// </summary>
		/// <param name="tag">The requested tag.</param>
		/// <param name="responsefields">Get extra information from the media.</param>
		/// <returns>The information about the specified media; null if the call fails.</returns>
		public Collection<Media> GetMediaByTag(string tag, HyvesMediaResponsefield responsefields)
		{
			return GetMediaByTag(tag, responsefields, false, 1, 100);
		}

		/// <summary>
		/// Gets the desired media from the specified user by tag. This corresponds to the
		/// media.getByTag Hyves method.
		/// </summary>
		/// <param name="tag">The requested tag.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified media; null if the call fails.</returns>
		public Collection<Media> GetMediaByTag(string tag, bool useFancyLayout)
		{
			return GetMediaByTag(tag, HyvesMediaResponsefield.All, useFancyLayout, 1, 100);
		}

		/// <summary>
		/// Gets the desired media from the specified user by tag. This corresponds to the
		/// media.getByTag Hyves method.
		/// </summary>
		/// <param name="userId">The requested user Id.</param>
		/// <param name="responsefields">Get extra information from the media.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
		/// <returns>The information about the specified media; null if the call fails.</returns>
    public Collection<Media> GetMediaByTag(string tag, HyvesMediaResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
		{
			if (string.IsNullOrEmpty(tag))
			{
				throw new ArgumentNullException("tag");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["tag"] = tag;
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaGetByTag, useFancyLayout, page, resultsPerPage);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessResponse<Media>("media");
			}

			return null;
		}
		#endregion

    #region GetPublic
    /// <summary>
    /// Gets the public media for the loggedin user. This corresponds to the
    /// media.getPublic Hyves method.
    /// </summary>
    /// <param name="sortType">The sorttype</param>
    /// <param name="mediaType">The media type of the results.</param>
    /// <param name="timeSpan">The timespan to select from.</param>
    /// <param name="responsefields">Get extra information from the media.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the specified media; null if the call fails.</returns>
    public Collection<Media> GetPublic(HyvesSortType sortType, HyvesMediaType mediaType, HyvesTimeSpan timeSpan, HyvesMediaResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (sortType == HyvesSortType.NotSpecified)
      {
        throw new ArgumentOutOfRangeException("sortType");
      }

      if (mediaType == HyvesMediaType.Unknown)
      {
        throw new ArgumentOutOfRangeException("mediaType");
      }
            
      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["sorttype"] = EnumHelper.GetDescription(sortType);
      request.Parameters["mediatype"] = EnumHelper.GetDescription(mediaType);
      if (timeSpan == HyvesTimeSpan.NotSpecified)
      {
        request.Parameters["timespan"] = EnumHelper.GetDescription(timeSpan);
      }

      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaGetPublic, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Media>("media");
      }

      return null;
    }
    #endregion

    #region GetSpotted
    /// <summary>
    /// Gets the spotted users of a media. This corresponds to the
    /// media.getSpotted Hyves method.
    /// </summary>
    /// <param name="responsefields">Get extra information from the media.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the spotted users; null if the call fails.</returns>
    public Collection<Spotted> GetSpotted(string mediaId, HyvesMediaResponsefield responsefields, bool useFancyLayout)
    {
      if (string.IsNullOrEmpty(mediaId))
      {
        throw new ArgumentException("mediaId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["mediaid"] = mediaId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaGetSpotted, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Spotted>("spotted");
      }

      return null;
    }
    #endregion

    #region AddTag
    /// <summary>
    /// Adds a list of coma separated tags to a media. This corresponds to the
    /// media.addTag Hyves method.
    /// </summary>
    /// <param name="mediaId">The identifier for the media.</param>
    /// <param name="tags">A list of tags.</param>
    /// <returns>True if the call succeeds, false if the call fails.</returns>
    public bool AddTag(string mediaId, Collection<string> tags)
    {
      if (string.IsNullOrEmpty(mediaId))
      {
        throw new ArgumentException("mediaId");
      }

      StringBuilder tagsStringBuilder = new StringBuilder();
      foreach (string tag in tags)
      {
        tagsStringBuilder.Append(tag);
        tagsStringBuilder.Append(",");
      }

      tagsStringBuilder.Remove(tagsStringBuilder.Length - 1, 1);

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["mediaid"] = mediaId;
      request.Parameters["tag"] = tagsStringBuilder.ToString();

      HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaAddTag);
      return response.Status == HyvesResponseStatus.Succeeded;
    }
    #endregion

		#region GetComments
		/// <summary>
		/// Gets the comments from the specified media. This corresponds to the
		/// media.getComments Hyves method.
		/// </summary>
		/// <param name="mediaId">The requested media ID.</param>
		/// <returns>The information about the specified media; null if the call fails.</returns>
		public Collection<Comment> GetComments(string mediaId)
		{
			return GetComments(mediaId, false);
		}

		/// <summary>
		/// Gets the comments from the specified media. This corresponds to the
		/// media.getComments Hyves method.
		/// </summary>
		/// <param name="mediaId">The requested media ID.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified media; null if the call fails.</returns>
		public Collection<Comment> GetComments(string mediaId, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(mediaId))
			{
				throw new ArgumentNullException("mediaId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_mediaid"] = mediaId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaGetComments, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Comment>("comment");
			}

			return null;
		}
		#endregion

		#region GetRespects
		/// <summary>
		/// Gets the respects from the specified media. This corresponds to the
		/// media.getRespects Hyves method.
		/// </summary>
		/// <param name="mediaId">The requested media ID.</param>
		/// <returns>The information about the specified media; null if the call fails.</returns>
		public Collection<Respect> GetRespects(string mediaId)
		{
			return GetRespects(mediaId, false);
		}

		/// <summary>
		/// Gets the respects from the specified media. This corresponds to the
		/// media.getRespects Hyves method.
		/// </summary>
		/// <param name="mediaId">The requested media ID.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified media; null if the call fails.</returns>
		public Collection<Respect> GetRespects(string mediaId, bool useFancyLayout)
		{
			if (string.IsNullOrEmpty(mediaId))
			{
				throw new ArgumentNullException("mediaId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_mediaid"] = mediaId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaGetRespects, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return response.ProcessResponse<Respect>("respect");
			}

			return null;
		}
		#endregion

		#region CreateRespect
		/// <summary>
		/// Creates respect for media. This corresponds to the
		/// media.createRespect Hyves method.
		/// </summary>
		/// <param name="targetMediaId">A single gadgetid.</param>
		/// <param name="respectType">The type of the respect.</param>
		/// <returns>True if the call succeeds, false if the call fails.</returns>
		public bool CreateRespect(string targetMediaId, HyvesRespectType respectType)
		{
			if (string.IsNullOrEmpty(targetMediaId))
			{
				throw new ArgumentNullException("targetMediaId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["target_mediaid"] = targetMediaId;
      request.Parameters["respecttype"] = EnumHelper.GetDescription(respectType);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaCreateRespect);
			return response.Status == HyvesResponseStatus.Succeeded;
		}
		#endregion

    #region UpdateGeolocation
    /// <summary>
    /// Update the geolocation of a media. This corresponds to the
    /// media.updateGeolocation Hyves method.
    /// </summary>
    /// <param name="targetMediaId">A single gadgetid.</param>
    /// <param name="respectType">The type of the respect.</param>
    /// <returns>True if the call succeeds, false if the call fails.</returns>
    public bool UpdateGeolocation(string mediaId, HyvesVisibility visibility, float latitude, float longitude)
    {
      if (string.IsNullOrEmpty(mediaId))
      {
        throw new ArgumentNullException("mediaId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["mediaid"] = mediaId;
      request.Parameters["visibility"] = EnumHelper.GetDescription(visibility);
      request.Parameters["latitude"] = latitude.ToString("F");
      request.Parameters["longitude"] = longitude.ToString("F");

      HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaCreateRespect);
      return response.Status == HyvesResponseStatus.Succeeded;
    }
    #endregion
        
    #region GetUploadToken
    /// <summary>
    /// Retrieves an upload token and ip-address to initiate the upload of media.
    /// </summary>
    /// <remarks>This corresponds to the media.getUploadToken Hyves method.</remarks>
    public UploadToken GetUploadToken()
    {
      HyvesRequest request = new HyvesRequest(this.session);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.MediaGetUploadToken);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        Debug.Assert(response.Result is Hashtable);
        Hashtable result = (Hashtable)response.Result;

        return new UploadToken((Hashtable)result);
      }

      return null;
    }
    #endregion

    #region UploadMedia
    // TODO: A lot of Refactoring...
    public void UploadMedia(UploadToken token, string fileName, byte[] fileData, string title, string description)
    {
      string NEWLINE = "\r\n";
      string PREFIX = "--";
      string boundary = DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
      string url = string.Format("http://{0}/upload?token={1}", token.Ip, HttpUtility.UrlEncode(token.Token));

      HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
      webRequest.Method = "POST";
      StringBuilder sb = new StringBuilder();

      sb.Append(PREFIX).Append(boundary).Append(NEWLINE);
      sb.Append("Content-Disposition: form-data; name=\"title\"");
      sb.Append(NEWLINE);
      sb.Append(NEWLINE);
      sb.Append(HttpUtility.UrlEncode(title));
      sb.Append(NEWLINE);

      sb.Append(PREFIX).Append(boundary).Append(NEWLINE);
      sb.Append("Content-Disposition: form-data; name=\"description\"");
      sb.Append(NEWLINE);
      sb.Append(NEWLINE);
      sb.Append(HttpUtility.UrlEncode(description));
      sb.Append(NEWLINE);

      sb.Append(PREFIX).Append(boundary).Append(NEWLINE);
      sb.Append("Content-Disposition: form-data; name=\"file\";");
      sb.Append(" filename=\"");
      sb.Append(fileName);
      sb.Append("\"").Append(NEWLINE);
      sb.Append("Content-Type: image/pjpeg");
      sb.Append(NEWLINE);
      sb.Append(NEWLINE);

      byte[] parameterBytes = Encoding.UTF8.GetBytes(sb.ToString());

      byte[] boundaryBytes = Encoding.UTF8.GetBytes(String.Concat(NEWLINE, PREFIX, boundary, PREFIX, NEWLINE));

      webRequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
      webRequest.ContentLength = parameterBytes.Length + fileData.Length + + boundaryBytes.Length;

      using (Stream requestStream = webRequest.GetRequestStream())
      {
        requestStream.Write(parameterBytes, 0, parameterBytes.Length);
        requestStream.Write(fileData, 0, fileData.Length);
        requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
      }
      
      HttpWebResponse webResponse = null;
      try
      {
        webResponse = (HttpWebResponse)webRequest.GetResponse();
      }
      catch (WebException we)
      {
        webResponse = (HttpWebResponse)we.Response;
      }      
    }

    public string GetUploadMediaStatus(UploadToken token)
    {
      string url = string.Format("http://{0}/status?token={1}", token.Ip, HttpUtility.UrlEncode(token.Token));

      HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
      webRequest.Method = "GET";
      
      HttpWebResponse webResponse = null;
      try
      {
        webResponse = (HttpWebResponse)webRequest.GetResponse();
      }
      catch (WebException we)
      {
        webResponse = (HttpWebResponse)we.Response;
      }

      HyvesResponse response = new HyvesResponse(webResponse.GetResponseStream(), HyvesMethod.Unknown);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.RawResponse;
      }

      return string.Empty;
    }
    #endregion

    #region Private methodes
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
