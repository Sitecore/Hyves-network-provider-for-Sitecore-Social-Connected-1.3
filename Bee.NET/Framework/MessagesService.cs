// Copyright (c) 2008-2009 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the service APIs that allow access to information on Hyves message.
	/// </summary>
	public sealed class MessagesService
	{
		private HyvesSession session;

		internal MessagesService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
    }

    /// <summary>
    /// Send a private message to multiple targets. This corresponds to the
    /// messages.send Hyves method.
    /// </summary>
    /// <param name="title">Title of the message.</param>
    /// <param name="body">Body of the message.</param>
    /// <param name="targetUserId">A single user.</param>
    /// <returns><b>true</b> if successfull; otherwise <b>false</b>.</returns>
    /// <remarks>Spam sensitive method (for trusted partners only).</remarks>
    public bool SendMessage(string title, string body, string targetUserId)
    {
      if (string.IsNullOrEmpty(title))
      {
        throw new ArgumentNullException("title");
      }
      if (string.IsNullOrEmpty(body))
      {
        throw new ArgumentNullException("body");
      }
      if (string.IsNullOrEmpty(targetUserId))
      {
        throw new ArgumentNullException("targetUserId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["title"] = title;
      request.Parameters["body"] = body;
      request.Parameters["target_userid"] = targetUserId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.MessagesSend);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return true;
      }

      return false;
    }

		/// <summary>
    /// Send a private message to multiple targets. This corresponds to the
    /// messages.send Hyves method.
		/// </summary>
    /// <param name="title">Title of the message.</param>
    /// <param name="body">Body of the message.</param>
    /// <param name="targetUserId">A list of users.</param>
    /// <param name="hubIds">A list of hubs.</param>
    /// <param name="groupIds">A list of groups.</param>
		/// <returns><b>true</b> if successfull; otherwise <b>false</b>.</returns>
    /// <remarks>Spam sensitive method (for trusted partners only).</remarks>
    public bool SendMessage(string title, string body, Collection<string> targetUserIds, Collection<string> hubIds, Collection<string> groupIds)
    {
      if (string.IsNullOrEmpty(title))
      {
        throw new ArgumentNullException("title");
      }
			if (string.IsNullOrEmpty(body))
			{
        throw new ArgumentNullException("body");
      }

      StringBuilder targetUserIdsBuilder = new StringBuilder();
      if (targetUserIds != null)
      {
        foreach (string id in targetUserIds)
        {
          if (targetUserIdsBuilder.Length != 0)
          {
            targetUserIdsBuilder.Append(",");
          }
          targetUserIdsBuilder.Append(id);
        }
      }

      StringBuilder hubIdsBuilder = new StringBuilder();
      if (hubIds != null)
      {
        foreach (string id in hubIds)
        {
          if (hubIdsBuilder.Length != 0)
          {
            hubIdsBuilder.Append(",");
          }
          hubIdsBuilder.Append(id);
        }
      }

      StringBuilder groupIdsBuilder = new StringBuilder();
      if (groupIds != null)
      {
        foreach (string id in groupIds)
        {
          if (groupIdsBuilder.Length != 0)
          {
            groupIdsBuilder.Append(",");
          }
          groupIdsBuilder.Append(id);
        }
      }

			HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["title"] = title;
      request.Parameters["body"] = body;
      request.Parameters["target_userid"] = targetUserIdsBuilder.ToString();
      request.Parameters["target_hubid"] = hubIdsBuilder.ToString();
      request.Parameters["target_groupid"] = groupIdsBuilder.ToString();

      HyvesResponse response = request.InvokeMethod(HyvesMethod.MessagesSend);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
        return true;
			}

			return false;
    }

    /// <summary>
    /// Send a private message to an user. This corresponds to the
    /// messages.sendToUser Hyves method.
    /// </summary>
    /// <param name="title">Title of the message.</param>
    /// <param name="body">Body of the message.</param>
    /// <param name="targetUserId">A single userid.</param>
    /// <returns><b>true</b> if successfull; otherwise <b>false</b>.</returns>
    /// <remarks>Spam sensitive method (for trusted partners only).</remarks>
    public bool SendMessageToUser(string title, string body, string targetUserId)
    {
      if (string.IsNullOrEmpty(title))
      {
        throw new ArgumentNullException("title");
      }

      if (string.IsNullOrEmpty(body))
      {
        throw new ArgumentNullException("body");
      }

      if (string.IsNullOrEmpty(targetUserId))
      {
        throw new ArgumentNullException("targetUserId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["title"] = title;
      request.Parameters["body"] = body;
      request.Parameters["target_userid"] = targetUserId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.MessagesSendToUser);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// Send a private message to an user. This corresponds to the
    /// messages.sendToUser Hyves method.
    /// </summary>
    /// <param name="title">Title of the message.</param>
    /// <param name="body">Body of the message.</param>
    /// <param name="targetUserId">A single userid.</param>
    /// <returns><b>true</b> if successfull; otherwise <b>false</b>.</returns>
    /// <remarks>Spam sensitive method (for trusted partners only).</remarks>
    public bool SendMessageToUser(string title, string body, Collection<string> targetUserIds)
    {
      if (string.IsNullOrEmpty(title))
      {
        throw new ArgumentNullException("title");
      }
      if (string.IsNullOrEmpty(body))
      {
        throw new ArgumentNullException("body");
      }

      StringBuilder targetUserIdsBuilder = new StringBuilder();
      if (targetUserIds != null)
      {
        foreach (string id in targetUserIds)
        {
          if (targetUserIdsBuilder.Length != 0)
          {
            targetUserIdsBuilder.Append(",");
          }
          targetUserIdsBuilder.Append(id);
        }
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["title"] = title;
      request.Parameters["body"] = body;
      request.Parameters["target_userid"] = targetUserIdsBuilder.ToString();

      HyvesResponse response = request.InvokeMethod(HyvesMethod.MessagesSendToUser);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return true;
      }

      return false;
    }
	}
}
