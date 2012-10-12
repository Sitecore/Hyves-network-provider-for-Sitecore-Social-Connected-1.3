// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Hyves.Service.Core;

namespace Hyves.Service
{
  /// <summary>
  /// Represents the service APIs that allow access to information on Hyves users.
  /// </summary>
  //TODO: Implement usertypes
  public sealed class UsersService
  {
    private HyvesSession session;

    internal UsersService(HyvesSession session)
    {
      Debug.Assert(session != null);
      this.session = session;
    }

    #region GetUser (users.get)
    /// <summary>
    /// Gets the desired information about the specified user. 
    /// </summary>
    /// <param name="userId">The requested user Id</param>
    /// <returns>The information about the specific user; null if the call fails.</returns>
    public User GetUser(string userId)
    {
      return GetUser(userId, HyvesUserResponsefield.All, false);
    }

    /// <summary>
    /// Gets the desired information about the specified user. 
    /// </summary>
    /// <param name="userId">The requested user Id</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <returns>The information about the specific user; null if the call fails.</returns>
    public User GetUser(string userId, HyvesUserResponsefield responsefields)
    {
      return GetUser(userId, responsefields, false);
    }

    /// <summary>
    /// Gets the desired information about the specified user. 
    /// </summary>
    /// <param name="userId">The requested user Id</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specific user; null if the call fails.</returns>
    public User GetUser(string userId, bool useFancyLayout)
    {
      return GetUser(userId, HyvesUserResponsefield.All, useFancyLayout);
    }

    /// <summary>
    /// Gets the desired information about the specified user. 
    /// </summary>
    /// <param name="userId">The requested user Id</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specific user; null if the call fails.</returns>
    public User GetUser(string userId, HyvesUserResponsefield responsefields, bool useFancyLayout)
    {
      if (string.IsNullOrEmpty(userId))
      {
        throw new ArgumentException("userId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["userid"] = userId;
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields, HyvesUserPrivateResponsefield.None);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGet, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<User>("user");
      }

      return null;
    }
    #endregion

    #region GetUsers (users.get)
    /// <summary>
    /// Gets the desired information about the specified users. This corresponds to the
    /// users.get Hyves method.
    /// </summary>
    /// <param name="userIds">The list of requested user Ids.</param>
    /// <returns>The information about the specified users; null if the call fails.</returns>
    public Collection<User> GetUsers(Collection<string> userIds)
    {
      return GetUsers(userIds, HyvesUserResponsefield.All, false);
    }

    /// <summary>
    /// Gets the desired information about the specified users. This corresponds to the
    /// users.get Hyves method.
    /// </summary>
    /// <param name="userIds">The list of requested user Ids.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <returns>The information about the specified users; null if the call fails.</returns>
    public Collection<User> GetUsers(Collection<string> userIds, HyvesUserResponsefield responsefields)
    {
      return GetUsers(userIds, responsefields, false);
    }

    /// <summary>
    /// Gets the desired information about the specified users. This corresponds to the
    /// users.get Hyves method.
    /// </summary>
    /// <param name="userIds">The list of requested user Ids.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified users; null if the call fails.</returns>
    public Collection<User> GetUsers(Collection<string> userIds, bool useFancyLayout)
    {
      return GetUsers(userIds, HyvesUserResponsefield.All, useFancyLayout);
    }

    /// <summary>
    /// Gets the desired information about the specified users. This corresponds to the
    /// users.get Hyves method.
    /// </summary>
    /// <param name="userIds">The list of requested user Ids.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified users; null if the call fails.</returns>
    public Collection<User> GetUsers(Collection<string> userIds, HyvesUserResponsefield responsefields, bool useFancyLayout)
    {
      return GetUsers(userIds, responsefields, HyvesUserPrivateResponsefield.None, useFancyLayout);
    }

    /// <summary>
    /// Gets the desired information about the specified users. This corresponds to the
    /// users.get Hyves method.
    /// </summary>
    /// <param name="userIds">The list of requested user Ids.</param>
    /// <param name="responsefields">Get extra information from the requested user.</param>
    /// <param name="responsefields">Get extra (private) information from the requested user.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified users; null if the call fails.</returns>
    public Collection<User> GetUsers(Collection<string> userIds, HyvesUserResponsefield responsefields, HyvesUserPrivateResponsefield privateResponseFields, bool useFancyLayout)
    {
      if ((userIds == null) || (userIds.Count == 0))
      {
        throw new ArgumentNullException("userIds");
      }

      StringBuilder userIdBuilder = new StringBuilder();
      if (userIds != null)
      {
        foreach (string id in userIds)
        {
          if (userIdBuilder.Length != 0)
          {
            userIdBuilder.Append(",");
          }
          userIdBuilder.Append(id);
        }
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["userid"] = userIdBuilder.ToString();
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields, privateResponseFields);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGet, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<User>("user");
      }

      return null;
    }
    #endregion

    #region GetUserByUserName (users.getByUsername)
    /// <summary>
    /// Gets the desired information about the specified user. 
    /// </summary>
    /// <param name="username">The username of the requested user.</param>
    /// <returns>The information about the specific user; null if the call fails.</returns>
    public User GetUserByUserName(string username)
    {
      return GetUserByUserName(username, HyvesUserResponsefield.All, false);
    }

    /// <summary>
    /// Gets the desired information about the specified user. 
    /// </summary>
    /// <param name="username">The username of the requested user.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <returns>The information about the specific user; null if the call fails.</returns>
    public User GetUserByUserName(string username, HyvesUserResponsefield responsefields)
    {
      return GetUserByUserName(username, responsefields, false);
    }

    /// <summary>
    /// Gets the desired information about the specified user. 
    /// </summary>
    /// <param name="username">The username of the requested user.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specific user; null if the call fails.</returns>
    public User GetUserByUserName(string username, bool useFancyLayout)
    {
      return GetUserByUserName(username, HyvesUserResponsefield.All, useFancyLayout);
    }

    /// <summary>
    /// Gets the desired information about the specified user. 
    /// </summary>
    /// <param name="username">The username of the requested user.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specific user; null if the call fails.</returns>
    public User GetUserByUserName(string username, HyvesUserResponsefield responsefields, bool useFancyLayout)
    {
      if (string.IsNullOrEmpty(username))
      {
        throw new ArgumentNullException("username");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["username"] = username;
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields, HyvesUserPrivateResponsefield.None);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGetByUsername, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<User>("user");
      }

      return null;
    }
    #endregion

    #region GetUsersByUserName (users.getByUsername)
    /// <summary>
    /// Gets the desired information about the specified users. This corresponds to the
    /// users.get Hyves method.
    /// </summary>
    /// <param name="usernames">The list of requested usernames.</param>
    /// <returns>The information about the specified users; null if the call fails.</returns>
    public Collection<User> GetUsersByUserName(Collection<string> usernames)
    {
      return GetUsersByUserName(usernames, HyvesUserResponsefield.All, false);
    }

    /// <summary>
    /// Gets the desired information about the specified users. This corresponds to the
    /// users.get Hyves method.
    /// </summary>
    /// <param name="usernames">The list of requested usernames.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <returns>The information about the specified users; null if the call fails.</returns>
    public Collection<User> GetUsersByUserName(Collection<string> usernames, HyvesUserResponsefield responsefields)
    {
      return GetUsersByUserName(usernames, responsefields, false);
    }

    /// <summary>
    /// Gets the desired information about the specified users. This corresponds to the
    /// users.get Hyves method.
    /// </summary>
    /// <param name="usernames">The list of requested usernames.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified users; null if the call fails.</returns>
    public Collection<User> GetUsersByUserName(Collection<string> usernames, bool useFancyLayout)
    {
      return GetUsersByUserName(usernames, HyvesUserResponsefield.All, useFancyLayout);
    }

    /// <summary>
    /// Gets the desired information about the specified users. This corresponds to the
    /// users.get Hyves method.
    /// </summary>
    /// <param name="usernames">The list of requested usernames.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified users; null if the call fails.</returns>
    public Collection<User> GetUsersByUserName(Collection<string> usernames, HyvesUserResponsefield responsefields, bool useFancyLayout)
    {
      if ((usernames == null) || (usernames.Count == 0))
      {
        throw new ArgumentNullException("usernames");
      }

      StringBuilder usernameBuilder = new StringBuilder();
      if (usernames != null)
      {
        foreach (string name in usernames)
        {
          if (usernameBuilder.Length != 0)
          {
            usernameBuilder.Append(",");
          }
          usernameBuilder.Append(name);
        }
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["username"] = usernameBuilder.ToString();
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields, HyvesUserPrivateResponsefield.None);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGetByUsername, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<User>("user");
      }

      return null;
    }
    #endregion

    #region GetLoggedinUser (users.getLoggedin)
    /// <summary>
    /// Gets the desired information about the user the supplied access token is for. This corresponds to the
    /// users.getLoggedin Hyves method.
    /// </summary>
    /// <returns>The information about the user; null if the call fails.</returns>
    public User GetLoggedinUser()
    {
      return GetLoggedinUser(false);
    }

    /// <summary>
    /// Gets the desired information about the user the supplied access token is for. This corresponds to the
    /// users.getLoggedin Hyves method.
    /// </summary>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <returns>The information about the user; null if the call fails.</returns>
    public User GetLoggedinUser(HyvesUserResponsefield responsefields)
    {
      return GetLoggedinUser(responsefields, false);
    }

    /// <summary>
    /// Gets the desired information about the user the supplied access token is for. This corresponds to the
    /// users.getLoggedin Hyves method.
    /// </summary>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the user; null if the call fails.</returns>
    public User GetLoggedinUser(bool useFancyLayout)
    {
      return GetLoggedinUser(HyvesUserResponsefield.All, useFancyLayout);
    }

    /// <summary>
    /// Gets the desired information about the user the supplied access token is for. This corresponds to the
    /// users.getLoggedin Hyves method.
    /// </summary>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the user; null if the call fails.</returns>
    public User GetLoggedinUser(HyvesUserResponsefield responsefields, bool useFancyLayout)
    {
      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields, HyvesUserPrivateResponsefield.None);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGetLoggedin, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<User>("user");
      }

      return null;
    }
    #endregion

    #region GetByFriendLastlogin (users.getByFriendLastlogin)
    /// <summary>
    /// Gets the desired information about the friends. 
    /// </summary>
    /// <param name="userId">The requested user Id</param>
    /// <returns>The information about the friends; null if the call fails.</returns>
    public Collection<User> GetByFriendLastlogin(string userId)
    {
      return GetByFriendLastlogin(userId, HyvesUserResponsefield.All, false, 1, 100);
    }

    /// <summary>
    /// Gets the desired information about the friends. 
    /// </summary>
    /// <param name="userId">The requested user Id</param>
    /// <param name="responsefields">Get extra information from the requested users</param>
    /// <returns>The information about the friends; null if the call fails.</returns>
    public Collection<User> GetByFriendLastlogin(string userId, HyvesUserResponsefield responsefields)
    {
      return GetByFriendLastlogin(userId, responsefields, false, 1, 100);
    }

    /// <summary>
    /// Gets the desired information about the friends. 
    /// </summary>
    /// <param name="userId">The requested user Id</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the friends; null if the call fails.</returns>
    public Collection<User> GetByFriendLastlogin(string userId, bool useFancyLayout)
    {
      return GetByFriendLastlogin(userId, HyvesUserResponsefield.All, useFancyLayout, 1, 100);
    }

    /// <summary>
    /// Gets the desired information about the friends. 
    /// </summary>
    /// <param name="userId">The requested user Id</param>
    /// <param name="responsefields">Get extra information from the requested users</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the friends; null if the call fails.</returns>
    public Collection<User> GetByFriendLastlogin(string userId, HyvesUserResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (string.IsNullOrEmpty(userId))
      {
        throw new ArgumentException("userId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["userid"] = userId;
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields, HyvesUserPrivateResponsefield.None);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGetByFriendLastlogin, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<User>("user");
      }

      return null;
    }
    #endregion

    #region GetFriendsByLoggedinSorted (users.getFriendsByLoggedinSorted)
    /// <summary>
    /// Gets the desired information about the friends for the current loggedin user with different sort-options.
    /// </summary>
    /// <param name="sortType">The sort type.</param>
    /// <returns>The information about the friends; null if the call fails.</returns>
    public Collection<User> GetFriendsByLoggedinSorted(HyvesUserSortType sortType)
    {
      return GetFriendsByLoggedinSorted(sortType, HyvesUserResponsefield.All, false, -1, -1);
    }

    /// <summary>
    /// Gets the desired information about the friends for the current loggedin user with different sort-options.
    /// </summary>
    /// <param name="sortType">The sort type.</param>
    /// <param name="responsefields">Get extra information from the requested users</param>
    /// <returns>The information about the friends; null if the call fails.</returns>
    public Collection<User> GetFriendsByLoggedinSorted(HyvesUserSortType sortType, HyvesUserResponsefield responsefields)
    {
      return GetFriendsByLoggedinSorted(sortType, responsefields, false, -1, -1);
    }

    /// <summary>
    /// Gets the desired information about the friends for the current loggedin user with different sort-options.
    /// </summary>
    /// <param name="sortType">The sort type.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the friends; null if the call fails.</returns>
    public Collection<User> GetFriendsByLoggedinSorted(HyvesUserSortType sortType, bool useFancyLayout)
    {
      return GetFriendsByLoggedinSorted(sortType, HyvesUserResponsefield.All, useFancyLayout, -1, -1);
    }

    /// <summary>
    /// Gets the desired information about the friends for the current loggedin user with different sort-options.
    /// </summary>
    /// <param name="sortType">The sort type.</param>
    /// <param name="responsefields">Get extra information from the requested users</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the friends; null if the call fails.</returns>
    public Collection<User> GetFriendsByLoggedinSorted(HyvesUserSortType sortType, HyvesUserResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (sortType == HyvesUserSortType.Unknown)
      {
        throw new ArgumentNullException("sortType");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["sorttype"] = EnumHelper.GetDescription(sortType);
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields, HyvesUserPrivateResponsefield.None);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGetFriendsByLoggedinSorted, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<User>("user");
      }

      return null;
    }
    #endregion

    #region GetByHubLastlogin
    /// <summary>
    /// Gets the users from the specified hub. This corresponds to the
    /// users.getByHubLastlogin Hyves method.
    /// </summary>
    /// <param name="hubId">The requested hub Id.</param>
    /// <returns>The information about the users sorted by lastlogin; null if the call fails.</returns>
    public Collection<User> GetByHubLastlogin(string hubId)
    {
      return GetHub(hubId, HyvesUserResponsefield.All, false, -1, -1);
    }

    /// <summary>
    /// Gets the users from the specified hub. This corresponds to the
    /// users.getByHubLastlogin Hyves method.
    /// </summary>
    /// <param name="hubId">The requested hub Id.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the users sorted by lastlogin; null if the call fails.</returns>
    public Collection<User> GetHub(string hubId, bool useFancyLayout)
    {
      return GetHub(hubId, HyvesUserResponsefield.All, useFancyLayout, -1, -1);
    }

    /// <summary>
    /// Gets the users from the specified hub. This corresponds to the
    /// users.getByHubLastlogin Hyves method.
    /// </summary>
    /// <param name="hubId">The requested hub Id.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the users sorted by lastlogin; null if the call fails.</returns>
    public Collection<User> GetHub(string hubId, HyvesUserResponsefield responsefields, bool useFancyLayout)
    {
      return GetHub(hubId, responsefields, useFancyLayout, -1, -1);
    }

    /// <summary>
    /// Gets the users from the specified hub. This corresponds to the
    /// users.getByHubLastlogin Hyves method.
    /// </summary>
    /// <param name="hubId">The requested hub Id.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the users sorted by lastlogin; null if the call fails.</returns>
    public Collection<User> GetHub(string hubId, HyvesUserResponsefield responsefields, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (string.IsNullOrEmpty(hubId))
      {
        throw new ArgumentNullException("hubId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["hubid"] = hubId;
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields, HyvesUserPrivateResponsefield.None);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGetByHubLastlogin, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<User>("user");
      }

      return null;
    }
    #endregion

    #region Search
    /// <summary>
    /// Search for users based on basic queries (keywords like city, name). This corresponds to the
    /// users.search Hyves method.
    /// </summary>
    /// <param name="searchterms">The searchterms to search for.</param>
    /// <returns>The information about the users; null if the call fails.</returns>
    public Collection<User> Search(string searchterms)
    {
      return Search(searchterms, HyvesUserResponsefield.All, false);
    }

    /// <summary>
    /// Search for users based on basic queries (keywords like city, name). This corresponds to the
    /// users.search Hyves method.
    /// </summary>
    /// <param name="searchterms">The searchterms to search for.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <returns>The information about the users; null if the call fails.</returns>
    public Collection<User> Search(string searchterms, HyvesUserResponsefield responsefields)
    {
      return Search(searchterms, responsefields, false);
    }

    /// <summary>
    /// Search for users based on basic queries (keywords like city, name). This corresponds to the
    /// users.search Hyves method.
    /// </summary>
    /// <param name="searchterms">The searchterms to search for.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the users; null if the call fails.</returns>
    public Collection<User> Search(string searchterms, bool useFancyLayout)
    {
      return Search(searchterms, HyvesUserResponsefield.All, useFancyLayout);
    }

    /// <summary>
    /// Search for users based on basic queries (keywords like city, name). This corresponds to the
    /// users.search Hyves method.
    /// </summary>
    /// <param name="searchterms">The searchterms to search for.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the users; null if the call fails.</returns>
    public Collection<User> Search(string searchterms, HyvesUserResponsefield responsefields, bool useFancyLayout)
    {
      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["searchterms"] = searchterms;
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields, HyvesUserPrivateResponsefield.None);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersSearch, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<User>("user");
      }

      return null;
    }

    /// <summary>
    /// Search for friends based on basic queries (keywords like city, name). This corresponds to the
    /// users.searchInFriends Hyves method.
    /// </summary>
    /// <param name="searchterms">The searchterms to search for.</param>
    /// <returns>The information about the users; null if the call fails.</returns>
    public Collection<User> SearchInFriends(string searchterms)
    {
      return SearchInFriends(searchterms, HyvesUserResponsefield.All, false);
    }

    /// <summary>
    /// Search for friends based on basic queries (keywords like city, name). This corresponds to the
    /// users.searchInFriends Hyves method.
    /// </summary>
    /// <param name="searchterms">The searchterms to search for.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <returns>The information about the users; null if the call fails.</returns>
    public Collection<User> SearchInFriends(string searchterms, HyvesUserResponsefield responsefields)
    {
      return SearchInFriends(searchterms, responsefields, false);
    }

    /// <summary>
    /// Search for friends based on basic queries (keywords like city, name). This corresponds to the
    /// users.searchInFriends Hyves method.
    /// </summary>
    /// <param name="searchterms">The searchterms to search for.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the users; null if the call fails.</returns>
    public Collection<User> SearchInFriends(string searchterms, bool useFancyLayout)
    {
      return SearchInFriends(searchterms, HyvesUserResponsefield.All, useFancyLayout);
    }

    /// <summary>
    /// Search for friends based on basic queries (keywords like city, name). 
    /// This corresponds to the users.searchInFriends Hyves method.
    /// </summary>
    /// <param name="searchterms">The searchterms to search for.</param>
    /// <param name="responsefields">Get extra information from the requested user</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the users; null if the call fails.</returns>
    public Collection<User> SearchInFriends(string searchterms, HyvesUserResponsefield responsefields, bool useFancyLayout)
    {
      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["searchterms"] = searchterms;
      request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields, HyvesUserPrivateResponsefield.None);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersSearchInFriends, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<User>("user");
      }

      return null;
    }
    #endregion

    #region Respects
    /// <summary>
    /// Gets the respects from the specified user. This corresponds to the
    /// users.getRespects Hyves method.
    /// </summary>
    /// <param name="userId">The requested user Id.</param>
    /// <returns>The information about the specified user; null if the call fails.</returns>
    public Collection<Respect> GetRespects(string userId)
    {
      return GetRespects(userId, false);
    }

    /// <summary>
    /// Gets the respects from the specified user. This corresponds to the
    /// users.getRespects Hyves method.
    /// </summary>
    /// <param name="userId">The requested user Id.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified user; null if the call fails.</returns>
    public Collection<Respect> GetRespects(string userId, bool useFancyLayout)
    {
      if (string.IsNullOrEmpty(userId))
      {
        throw new ArgumentNullException("userId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_userid"] = userId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGetRespects, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Respect>("respect");
      }

      return null;
    }

    /// <summary>
    /// Creates respect for a user. This corresponds to the
    /// user.createRespect Hyves method.
    /// </summary>
    /// <param name="targetUserId">A single userid.</param>
    /// <param name="respectType">The type of the respect.</param>
    /// <returns>True if the call succeeds, false if the call fails.</returns>
    public bool CreateRespect(string targetUserId, HyvesRespectType respectType)
    {
      if (string.IsNullOrEmpty(targetUserId))
      {
        throw new ArgumentNullException("targetUserId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_userid"] = targetUserId;
      request.Parameters["respecttype"] = EnumHelper.GetDescription(respectType);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersCreateRespect);
      return response.Status == HyvesResponseStatus.Succeeded;
    }
    #endregion

    #region Scraps
    /// <summary>
    /// Gets the scraps from the specified user. This corresponds to the
    /// users.getScraps Hyves method.
    /// </summary>
    /// <param name="userId">The requested user Id.</param>
    /// <returns>The information about the specified user; null if the call fails.</returns>
    public Collection<Scrap> GetScraps(string userId)
    {
      return GetScraps(userId, false);
    }

    /// <summary>
    /// Gets the scraps from the specified user. This corresponds to the
    /// users.getScraps Hyves method.
    /// </summary>
    /// <param name="userId">The requested user Id.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified user; null if the call fails.</returns>
    public Collection<Scrap> GetScraps(string userId, bool useFancyLayout)
    {
      return GetScraps(userId, useFancyLayout, -1, -1);
    }

    /// <summary>
    /// Gets the scraps from the specified user. This corresponds to the
    /// users.getScraps Hyves method.
    /// </summary>
    /// <param name="userId">The requested user Id.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <param name="page">The requested page.</param>
    /// <param name="resultsPerPage">The number of results per page.</param>
    /// <returns>The information about the specified user; null if the call fails.</returns>
    public Collection<Scrap> GetScraps(string userId, bool useFancyLayout, int page, int resultsPerPage)
    {
      if (string.IsNullOrEmpty(userId))
      {
        throw new ArgumentNullException("userId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_userid"] = userId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGetScraps, useFancyLayout, page, resultsPerPage);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Scrap>("scrap");
      }

      return null;
    }

    /// <summary>
    /// Creates scrap for a user. This corresponds to the
    /// user.createScrap Hyves method.
    /// </summary>
    /// <param name="targetUserId">A single userid.</param>
    /// <param name="body">The body of the Scrap.</param>
    /// <returns>True if the call succeeds, otherwise false.</returns>
    /// <remarks>Spam sensitive method (for trusted partners only).</remarks>
    public bool CreateScrap(string targetUserId, string body)
    {
      if (string.IsNullOrEmpty(targetUserId))
      {
        throw new ArgumentException("targetUserId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_userid"] = targetUserId;
      request.Parameters["body"] = body;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersCreateScrap);
      return response.Status == HyvesResponseStatus.Succeeded;
    }

    /// <summary>
    /// Creates scrap for an userid which has to be a friend of 
    /// logged in user. This corresponds to the
    /// user.createScrapByFriend Hyves method.
    /// </summary>
    /// <param name="targetUserId">A single userid.</param>
    /// <param name="body">The body of the Scrap.</param>
    /// <returns>True if the call succeeds, otherwise false.</returns>
    /// <remarks>Hidden method.</remarks>
    public bool CreateScrapByFriend(string targetUserId, string body)
    {
      if (string.IsNullOrEmpty(targetUserId))
      {
        throw new ArgumentException("targetUserId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_userid"] = targetUserId;
      request.Parameters["body"] = body;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersCreateScrapByFriend);
      return response.Status == HyvesResponseStatus.Succeeded;
    }
    #endregion

    #region Testimonials
    /// <summary>
    /// Gets the testimonials from the specified user. This corresponds to the
    /// users.getTestimonials Hyves method.
    /// </summary>
    /// <param name="userId">The requested user Id.</param>
    /// <returns>The information about the specified user; null if the call fails.</returns>
    public Collection<Testimonial> GetTestimonials(string userId)
    {
      return GetTestimonials(userId, false);
    }

    /// <summary>
    /// Gets the testimonials from the specified user. This corresponds to the
    /// users.getTestimonials Hyves method.
    /// </summary>
    /// <param name="userId">The requested user Id.</param>
    /// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
    /// <returns>The information about the specified user; null if the call fails.</returns>
    public Collection<Testimonial> GetTestimonials(string userId, bool useFancyLayout)
    {
      if (string.IsNullOrEmpty(userId))
      {
        throw new ArgumentNullException("userId");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["target_userid"] = userId;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGetTestimonials, useFancyLayout);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Testimonial>("testimonial");
      }

      return null;
    }
    #endregion

    #region Notifications
    /// <summary>
    /// Send an email to an given userid which has to be loggedin user 
    /// or a friend of logged in user. This corresponds to the
    /// users.sendNotificationToFriend Hyves method.
    /// </summary>
    /// <param name="userid">A single userid.</param>
    /// <param name="subject">Subject of the email.</param>
    /// <param name="body">Body of the email. </param>
    /// <param name="senderName">Sender name. </param>
    /// <param name="senderEmail">Sender email address.</param>
    /// <returns><b>True</b> if the call succeeds, otherwise <b>false</b>.</returns>
    /// <remarks>Spam sensitive method (for trusted partners only).</remarks>
    public bool SendNotificationToFriend(string userId, string subject, string body, string senderName, string senderEmail)
    {
      if (string.IsNullOrEmpty(userId))
      {
        throw new ArgumentNullException("userId");
      }
      if (string.IsNullOrEmpty(subject))
      {
        throw new ArgumentNullException("subject");
      }
      if (string.IsNullOrEmpty(body))
      {
        throw new ArgumentNullException("body");
      }
      if (string.IsNullOrEmpty(senderName))
      {
        throw new ArgumentNullException("senderName");
      }
      if (string.IsNullOrEmpty(senderEmail))
      {
        throw new ArgumentNullException("senderEmail");
      }

      HyvesRequest request = new HyvesRequest(this.session);
      request.Parameters["userid"] = userId;
      request.Parameters["subject"] = subject;
      request.Parameters["body"] = body;
      request.Parameters["sender_name"] = senderName;
      request.Parameters["sender_email"] = senderEmail;

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersSendNotificationToFriend);
      return response.Status == HyvesResponseStatus.Succeeded;
    }
    #endregion

    #region GetSmileyCategories
    /// <summary>
    /// Gets the smiley categories. This corresponds to the
    /// users.getSmileyCategories Hyves method.
    /// </summary>
    /// <returns>The information about all the categories; null if the call fails.</returns>
    public Collection<SmileyCategory> GetSmileyCategories()
    {
      HyvesRequest request = new HyvesRequest(this.session);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGetSmileyCategories);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<SmileyCategory>("smileycategory");
      }

      return null;
    }
    #endregion

    #region GetSmileys
    /// <summary>
    /// Gets the smileys. This corresponds to the
    /// users.getSmileys Hyves method.
    /// </summary>
    /// <returns>The information about all the smileys; null if the call fails.</returns>
    public Collection<Smiley> GetSmileys()
    {
      HyvesRequest request = new HyvesRequest(this.session);

      HyvesResponse response = request.InvokeMethod(HyvesMethod.UsersGetSmileys);
      if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Smiley>("smiley");
      }

      return null;
    }
    #endregion

    #region Private methodes
    private string ConvertResponsefieldsToString(HyvesUserResponsefield responsefields, HyvesUserPrivateResponsefield privateResponseFields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesUserResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesUserResponsefield>());
      }
      else
      {
        var userResponsefields = Enum.GetValues(typeof(HyvesUserResponsefield));
        foreach (HyvesUserResponsefield userResponseField in userResponsefields)
        {
          if (EnumHelper.HasFlag(responsefields, userResponseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(userResponseField)));
          }
        }
      }

      if (privateResponseFields == HyvesUserPrivateResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesUserPrivateResponsefield>());
      }
      else if (privateResponseFields != HyvesUserPrivateResponsefield.None)
      {
        var userPrivateResponsefields = Enum.GetValues(typeof(HyvesUserPrivateResponsefield));
        foreach (HyvesUserPrivateResponsefield userPrivateResponseField in userPrivateResponsefields)
        {
          if (EnumHelper.HasFlag(privateResponseFields, userPrivateResponseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(privateResponseFields)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesUserResponsefield.All)), string.Empty);
      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesUserPrivateResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
    }
    #endregion
  }
}
