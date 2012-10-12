// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service.Core
{
	/// <summary>
	/// Represents the Hyves api methods. 
	/// </summary>
  public enum HyvesMethod : uint
	{
    /// <summary>
    /// The method is unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Adds media to album. 
    /// </summary>
    [Description("albums.addMedia")]
    AlbumsAddMedia  = 10101,

    /// <summary>
    /// Creates an album for logged in user.
    /// </summary>
    [Description("albums.create")]
    AlbumsCreate = 10102,

    /// <summary>
    /// Retrieves a list of albums by albumid. 
    /// </summary>
    [Description("albums.get")]
    AlbumsGet = 10103,

    /// <summary>
    /// Retrieves one or more default (always existing) album(s) for logged in user.
    /// </summary>
    [Description("albums.getBuiltin")]
    AlbumsGetBuiltin = 10104,

    /// <summary>
    /// Retrieves all visible albums of a hub. 
    /// </summary>
    [Description("albums.getByHub")]
    AlbumsGetByHub = 10105,

    /// <summary>
    /// Retrieves all visible albums of a user. 
    /// </summary>
    [Description("albums.getByUser")]
    AlbumsGetByUser = 10106,

    /// <summary>
    /// Remove media from album. 
    /// </summary>
    [Description("albums.removeMedia")]
    AlbumsRemoveMedia = 10107,

    /// <summary>
    /// Retrieves an access token.
    /// </summary>
    [Description("auth.accesstoken")]
    AuthAccesstoken = 10201,

    /// <summary>
    /// Retrieves a request token. The oauth_token parameter should be absent. 
    /// </summary>
    [Description("auth.requesttoken")]
    AuthRequesttoken = 10202,

    /// <summary>
    /// Revokes all accesstokens for user. 
    /// </summary>
    [Description("auth.revoke")]
    AuthRevoke = 10203,

    /// <summary>
    /// Revokes all accesstokens for Consumer. 
    /// </summary>
    [Description("auth.revokeAll")]
    AuthRevokeAll = 10204,

    /// <summary>
    /// Revokes all accesstokens for Consumer. 
    /// </summary>
    [Description("auth.revokeSelf")]
    AuthRevokeSelf = 10205,

    /// <summary>
    /// Execute multiple (up to 10) API requests in 1 API-call. 
    /// </summary>
    [Description("batch.process")]
    BatchProcess = 10301,

    /// <summary>
    /// Creates a blog. 
    /// </summary>
    [Description("blogs.create")]
    BlogsCreate = 10401,

    /// <summary>
    /// Creates respect for a blogid. 
    /// </summary>
    [Description("blogs.createRespect")]
    BlogsCreateRespect = 10402,

    /// <summary>
    /// Retrieves a list of blogs by blogid. 
    /// </summary>
    [Description("blogs.get")]
    BlogsGet = 10403,

    /// <summary>
    /// Retrieves the most recent blogs of a hub. 
    /// </summary>
    [Description("blogs.getByHub")]
    BlogsGetByHub = 10404,

    /// <summary>
    /// Retrieves public blogs by tag. 
    /// </summary>
    [Description("blogs.getByTag")]
    BlogsGetByTag = 10405,

    /// <summary>
    /// Retrieves the most recent blogs of a user. 
    /// </summary>
    [Description("blogs.getByUser")]
    BlogsGetByUser = 10406,

    /// <summary>
    /// Retrieves the comments for a blogid. 
    /// </summary>
    [Description("blogs.getComments")]
    BlogsGetComments = 10407,

    /// <summary>
    /// Retrieves the most recent blogs for the friends of the loggedin user. 
    /// </summary>
    [Description("blogs.getForFriends")]
    BlogsGetForFriends = 10408,

    /// <summary>
    /// Retrieve public blogs. Sorttype defines the sort. 
    /// </summary>
    [Description("blogs.getPublic")]
    BlogsGetPublic = 10409,

    /// <summary>
    /// Retrieves the respects for a blogid. 
    /// </summary>
    [Description("blogs.getRespects")]
    BlogsGetRespects = 10410,

    /// <summary>
    /// Updates a blog. 
    /// </summary>
    [Description("blogs.update")]
    BlogsUpdate = 10411,

    /// <summary>
    /// Retrieves buzz for famous hyvers. 
    /// </summary>
    [Description("buzz.getFamous")]
    BuzzGetFamous = 10501,

    /// <summary>
    /// Get an server-ipadress and logintoken for the chat-system.  
    /// </summary>
    [Description("chat.getLoginToken")]
    ChatGetLoginToken = 10601,

    /// <summary>
    /// Retrieves cities. 
    /// </summary>
    [Description("cities.get")]
    CitiesGet = 10701,

    /// <summary>
    /// Retrieves cities. 
    /// </summary>
    [Description("countries.get")]
    CountriesGet = 10801,

    /// <summary>
    /// Adds user presence to event. 
    /// </summary>
    [Description("events.addPresence")]
    EventsAddPresence = 10901,

    /// <summary>
    /// Retrieves a list of events by eventid. 
    /// </summary>
    [Description("events.get")]
    EventsGet = 10902,

    /// <summary>
    /// Retrieves events of a hub. 
    /// </summary>
    [Description("events.getByHub")]
    EventsGetByHub = 10903,

    /// <summary>
    /// Retrieves all events created by logged in user.
    /// </summary>
    [Description("events.getByLoggedin")]
    EventsGetByLoggedin = 10904,

    /// <summary>
    /// Retrieves all public events where the given user is present. 
    /// </summary>
    [Description("events.getByUserPresent")]
    EventsGetByUserPresent = 10905,

    /// <summary>
    /// Retrieves all userid's that are present for given event.  
    /// </summary>
    [Description("events.getPresence")]
    EventsGetPresence = 10906,

    /// <summary>
    /// Retrieves all userid's for the friends that are present for given event. 
    /// </summary>
    [Description("events.getPresenceForFriends")]
    EventsGetPresenceForFriends = 10907,

    /// <summary>
    /// Converts a string to HTML format the same way that that is being done on the site, including things like smilies. See ha_fancylayout  for more information. 
    /// </summary>
    [Description("fancylayout.parse")]
    FancyLayoutParse = 11001,

    /// <summary>
    /// Retrieves the friends of user the supplied access token is for. 
    /// </summary>
    [Description("friends.get")]
    FriendsGet = 11101,

    /// <summary>
    /// Retrieves the connections between the user the access token is for and another user. 
    /// </summary>
    [Description("friends.getConnection")]
    FriendsGetConnection = 11102,

    /// <summary>
    /// Retrieves the distance between the user the access token is for, and other users. 
    /// </summary>
    [Description("friends.getDistance")]
    FriendsGetDistance = 11103,

    /// <summary>
    /// Retrieves all incoming invitations for the logged in user. 
    /// </summary>
    [Description("friends.getIncomingInvitations")]
    FriendsGetIncomingInvitations = 11104,

    /// <summary>
    /// Retrieves all outgoing invitations for the logged in user. 
    /// </summary>
    [Description("friends.getOutgoingInvitations")]
    FriendsGetOutgoingInvitations = 11105,

    /// <summary>
    /// Creates a gadget. 
    /// </summary>
    [Description("gadgets.create")]
    GadgetsCreate = 11201,

    /// <summary>
    /// Creates a gadget by XML url (OpenSocial gadgets). 
    /// </summary>
    [Description("gadgets.createByXML")]
    GadgetsCreateByXML = 11202,

    /// <summary>
    /// Creates respect for an gadgetid. 
    /// </summary>
    [Description("gadgets.createRespect")]
    GadgetsCreateRespect = 11203,

    /// <summary>
    /// Retrieves a list of gadgets by gadgetid. 
    /// </summary>
    [Description("gadgets.get")]
    GadgetsGet = 11204,

    /// <summary>
    /// Retrieves all gadgets of a hub. 
    /// </summary>
    [Description("gadgets.getByHub")]
    GadgetsGetByHub = 11205,

    /// <summary>
    /// Retrieves all visible gadgets of a user. 
    /// </summary>
    [Description("gadgets.getByUser")]
    GadgetsGetByUser = 11206,

    /// <summary>
    /// Retrieves the comments for a gadgetid. 
    /// </summary>
    [Description("gadgets.getComments")]
    GadgetsGetComments = 11207,

    /// <summary>
    /// Retrieves the respects for a gadgetid. 
    /// </summary>
    [Description("gadgets.getRespects")]
    GadgetsGetRespects = 11208,

    /// <summary>
    /// Retrieves a list of hubcategories by hubcategoryid.  
    /// </summary>
    [Description("hubcategories.get")]
    HubCategoriesGet = 11301,

    /// <summary>
    /// Retrieves all hubcategories by hubtype.  
    /// </summary>
    [Description("hubcategories.getByHubType")]
    HubCategoriesGetByHubType = 11302,

    /// <summary>
    /// Retrieves a list of children hubcategories of a parent hubcategoryid. 
    /// </summary>
    [Description("hubcategories.getChildren")]
    HubCategoriesGetChildren = 11303,

    /// <summary>
    /// Retrieves a list of hubs by hubid.  
    /// </summary>
    [Description("hubs.get")]
    HubsGet = 11401,

    /// <summary>
    /// Retrieves all hubs within given hubcategoryid.  
    /// </summary>
    [Description("hubs.getByHubCategory")]
    HubsGetByHubCategory = 11402,

    /// <summary>
    /// Retrieves a list of hubs by shortname.  
    /// </summary>
    [Description("hubs.getByShortname")]
    HubsGetByShortname = 11403,

    /// <summary>
    /// Retrieves all hubs of an user.  
    /// </summary>
    [Description("hubs.getByUser")]
    HubsGetByUser = 11404,

    /// <summary>
    /// Retrieves all the hubtypes.  
    /// </summary>
    [Description("hubs.getHubTypes")]
    HubsGetHubTypes = 11405,

    /// <summary>
    /// Retrieves the scraps for a hub.  
    /// </summary>
    [Description("hubs.getScraps")]
    HubsGetScraps = 11406,

    /// <summary>
    /// Search for hubs based on basic queries (keywords like city, name).  
    /// </summary>
    [Description("hubs.search")]
    HubsSearch = 11407,

    /// <summary>
    /// Updates the media associated with a hub.   
    /// </summary>
    [Description("hubs.updateMedia")]
    HubsUpdateMedia = 11408,

    /// <summary>
    /// Subscribes logged in user to a hub. 
    /// </summary>
    /// <remarks>Spam sensitive method.</remarks>
    [Description("hubs.subscribe")]
    HubsSubscribe = 11409,

    /// <summary>
    /// Create a listener for the ApiConsumer.
    /// </summary>
    [Description("listeners.create")]
    ListenersCreate = 11501,

    /// <summary>
    /// Delete a listener for the ApiConsumer.
    /// </summary>
    [Description("listeners.delete")]
    ListenersDelete = 11502,

    /// <summary>
    /// Retrieves listeners. 
    /// </summary>
    [Description("listeners.get")]
    ListenersGet = 11503,

    /// <summary>
    /// Retrieves all listeners. 
    /// </summary>
    [Description("listeners.getAll")]
    ListenersGetAll = 11504,

    /// <summary>
    /// Retrieves listeners by type. 
    /// </summary>
    [Description("listeners.getByType")]
    ListenersGetByType = 11505,

    /// <summary>
    /// Adds a spotted user to the media. 
    /// </summary>
    [Description("media.addSpotted")]
    MediaAddSpotted = 11601,

    /// <summary>
    /// Adds a list of coma separated tags to a media. 
    /// </summary>
    [Description("media.addTag")]
    MediaAddTag = 11602,

    /// <summary>
    /// Creates respect for an mediaid. 
    /// </summary>
    [Description("media.createRespect")]
    MediaCreateRespect = 11603,

    /// <summary>
    /// Retrieves a list of media by mediaid. 
    /// </summary>
    [Description("media.get")]
    MediaGet = 11604,

    /// <summary>
    /// Retrieves all media from an album. 
    /// </summary>
    [Description("media.getByAlbum")]
    MediaGetByAlbum = 11605,

    /// <summary>
    /// Retrieves all media by logged in user. 
    /// </summary>
    [Description("media.getByLoggedin")]
    MediaGetByLoggedin = 11606,

    /// <summary>
    /// Retrieves public media by tag. 
    /// </summary>
    [Description("media.getByTag")]
    MediaGetByTag = 11607,

    /// <summary>
    /// Retrieves the comments for a mediaid. 
    /// </summary>
    [Description("media.getComments")]
    MediaGetComments = 11608,

    /// <summary>
    /// Retrieve public media. Sorttype defines the sort. 
    /// </summary>
    [Description("media.getPublic")]
    MediaGetPublic = 11609,

    /// <summary>
    /// Retrieves the respects for a mediaid. 
    /// </summary>
    [Description("media.getRespects")]
    MediaGetRespects = 11610,

    /// <summary>
    /// Retrieve the spotted users on a media. 
    /// </summary>
    [Description("media.getSpotted")]
    MediaGetSpotted = 11611,

    /// <summary>
    /// Retrieves an upload token and ip-address to initiate the upload of media. 
    /// </summary>
    [Description("media.getUploadToken")]
    MediaGetUploadToken = 11612,

    /// <summary>
    /// Update a media. 
    /// </summary>
    [Description("media.update")]
    MediaUpdate = 11613,

    /// <summary>
    /// Update or create geolocation of a media. 
    /// </summary>
    [Description("media.updateGeolocation")]
    MediaUpdateGeolocation = 11614,

    /// <summary>
    /// Retrieves a list of pings by pingid. 
    /// </summary>
    [Description("pings.get")]
    PingsGet = 11701,

    /// <summary>
    /// Retrieves the pings of the target user. 
    /// </summary>
    [Description("pings.getByTargetUser")]
    PingsGetByTargetUser = 11702,

    /// <summary>
    /// Retrieves the pings of a user. 
    /// </summary>
    [Description("pings.getByUser")]
    PingsGetByUser = 11703,

    /// <summary>
    /// Creates a ping.
    /// </summary>
    [Description("pings.create")]
    PingsCreate = 11704,

    /// <summary>
    /// Creates a ping by a friend of logged in user. 
    /// </summary>
    /// <remarks>Hidden method.</remarks>
    [Description("pings.createByFriend")]
    PingsCreateByFriend = 11705,

    /// <summary>
    /// Retrieves the default pingtypes.
    /// </summary>
    /// <remarks>Hidden method.</remarks>
    [Description("pings.getTypes")]
    PingsGetTypes = 11706,

    /// <summary>
    /// Creates a privatespot. 
    /// </summary>
    [Description("privatespots.create")]
    PrivateSpotsCreate = 11801,

    /// <summary>
    /// Retrieves a list of privatespots by privatespotid. 
    /// </summary>
    [Description("privatespots.get")]
    PrivateSpotsGet = 11802,

    /// <summary>
    /// Retrieves all privatespots of logged in user.  
    /// </summary>
    [Description("privatespots.getByLoggedin")]
    PrivateSpotsGetByLoggedin = 11803,

    /// <summary>
    /// Retrieves regions. 
    /// </summary>
    [Description("regions.get")]
    RegionsGet = 11901,

    /// <summary>
    /// Search items in partical categories by searchterms. 
    /// </summary>
    [Description("search.find")]
    SearchFind = 12001,

    /// <summary>
    /// Retrieves a list of threads by threadid. 
    /// </summary>
    [Description("threads.get")]
    ThreadsGet = 12101,

    /// <summary>
    /// Retrieves all threads of a hub.  
    /// </summary>
    [Description("threads.getByHub")]
    ThreadsGetByHub = 12102,

    /// <summary>
    /// Retrieves the comments for a threadid.  
    /// </summary>
    [Description("threads.getComments")]
    ThreadsGetComments = 12103,

    /// <summary>
    /// Creates a tip.
    /// </summary>
    [Description("tips.create")]
    TipsCreate = 12201,

    /// <summary>
    /// Retrieves
    /// </summary>
    [Description("tips.createRespect")]
    TipsCreateRespect = 12202,

    /// <summary>
    /// Retrieves a list of tips by tipid. 
    /// </summary>
    [Description("tips.get")]
    TipsGet = 12203,

    /// <summary>
    /// Retrieves the most recent tips of a hub. 
    /// </summary>
    [Description("tips.getByHub")]
    TipsGetByHub = 12204,

    /// <summary>
    /// Retrieves the most recent tips of a user. 
    /// </summary>
    [Description("tips.getByUser")]
    TipsGetByUser = 12205,

    /// <summary>
    /// Retrieves all tip categories. 
    /// </summary>
    [Description("tips.getCategories")]
    TipsGetCategories = 12206,

    /// <summary>
    /// Retrieves the comments for a tipid. 
    /// </summary>
    [Description("tips.getComments")]
    TipsGetComments = 12207,

    /// <summary>
    /// Retrieves the most recent tips for the friends of the loggedin user. 
    /// </summary>
    [Description("tips.getForFriends")]
    TipsGetForFriends = 12208,

    /// <summary>
    /// Retrieves the respects for a tipid. 
    /// </summary>
    [Description("tips.getRespects")]
    TipsGetRespects = 12209,

    /// <summary>
    /// Creates respect for an userid. 
    /// </summary>
    [Description("users.createRespect")]
    UsersCreateRespect = 12301,

    /// <summary>
    /// Retrieves basic information for one or more users. 
    /// </summary>
    [Description("users.get")]
    UsersGet = 12302,

    /// <summary>
    /// Retrieves friends for any visible user, limited to 150 users sorted by last login. 
    /// </summary>
    [Description("users.getByFriendLastlogin")]
    UsersGetByFriendLastlogin = 12303,

    /// <summary>
    /// Retrieves users for any hub, limited to 150 users sorted by last login. 
    /// </summary>
    [Description("users.getByHubLastlogin")]
    UsersGetByHubLastlogin = 12304,

    /// <summary>
    /// Retrieves basic information for one or more users. 
    /// </summary>
    [Description("users.getByUsername")]
    UsersGetByUsername = 12305,

    /// <summary>
    /// Retrieves friends by loggedin user with different sort-options. 
    /// </summary>
    [Description("users.getFriendsByLoggedinSorted")]
    UsersGetFriendsByLoggedinSorted = 12306,

    /// <summary>
    /// Retrieves the basic information for user the supplied access token is for. 
    /// </summary>
    [Description("users.getLoggedin")]
    UsersGetLoggedin = 12307,

    /// <summary>
    /// Retrieves the respects for an userid. 
    /// </summary>
    [Description("users.getRespects")]
    UsersGetRespects = 12308,

    /// <summary>
    /// Retrieves the scraps for an user. 
    /// </summary>
    [Description("users.getScraps")]
    UsersGetScraps = 12309,

    /// <summary>
    /// Retrieves the existing smiley categories. 
    /// </summary>
    [Description("users.getSmileyCategories")]
    UsersGetSmileyCategories = 12310,

    /// <summary>
    /// Retrieves the smileys for the user the supplied access token is for. 
    /// </summary>
    [Description("users.getSmileys")]
    UsersGetSmileys = 12311,

    /// <summary>
    /// Retrieves the testimonials for an userid. 
    /// </summary>
    [Description("users.getTestimonials")]
    UsersGetTestimonials = 12312,

    /// <summary>
    /// Search for users based on basic queries (keywords like city, name).
    /// </summary>
    [Description("users.search")]
    UsersSearch = 12313,

    /// <summary>
    /// Retrieves the testimonials for an userid. 
    /// </summary>
    [Description("users.searchInFriends")]
    UsersSearchInFriends = 12314,

    /// <summary>
    /// Creates scrap for an userid. 
    /// </summary>
    /// <remarks>Spam sensitive method.</remarks>
    [Description("users.createScrap")]
    UsersCreateScrap = 12315,

    /// <summary>
    /// Creates scrap for an userid which has to be a friend of logged in user. 
    /// </summary>
    /// <remarks>Hidden method.</remarks>
    [Description("users.createScrapByFriend")]
    UsersCreateScrapByFriend = 12316,

    /// <summary>
    /// Send an email to an given userid which has to be loggedin user or a friend of logged in user. 
    /// </summary>
    /// <remarks>Spam sensitive method.</remarks>
    [Description("users.sendNotificationToFriend")]
    UsersSendNotificationToFriend= 12317,

    /// <summary>
    /// Creates a www. 
    /// </summary>
    [Description("wwws.create")]
    WwwsCreate = 12401,

    /// <summary>
    /// Creates respect for an wwwid. 
    /// </summary>
    [Description("wwws.createRespect")]
    WwwsCreateRespect = 12402,

    /// <summary>
    /// Retrieves a list of wwws by wwwid. 
    /// </summary>
    [Description("wwws.get")]
    WwwsGet = 12403,

    /// <summary>
    /// Retrieves the most recent public www(Who What Where)s of a hub. 
    /// </summary>
    [Description("wwws.getByHub")]
    WwwsGetByHub = 12404,

    /// <summary>
    /// Retrieves the most recent www(Who What Where)s of a user. 
    /// </summary>
    [Description("wwws.getByUser")]
    WwwsGetByUser = 12405,

    /// <summary>
    /// Retrieves the comments for a wwwid. 
    /// </summary>
    [Description("wwws.getComments")]
    WwwsGetComments = 12406,

    /// <summary>
    /// Retrieves the most recent www(Who What Where)s for the friends (1 www max per friend) of the loggedin user. 
    /// </summary>
    [Description("wwws.getForFriends")]
    WwwsGetForFriends = 12407,

    /// <summary>
    /// Retrieves the respects for a wwwid. 
    /// </summary>
    [Description("wwws.getRespects")]
    WwwsGetRespects = 12408,

    /// <summary>
    /// Search for public www's. 
    /// </summary>
    [Description("wwws.searchPublic")]
    WwwsSearchPublic = 12409,

    /// <summary>
    /// Delete a single message.
    /// </summary>
    /// <remarks>Hidden method.</remarks>
    [Description("messages.delete")]
    MessagesDelete = 12501,

    /// <summary>
    /// Get a single message. 
    /// </summary>
    /// <remarks>Hidden method.</remarks>
    [Description("messages.get")]
    MessagesGet = 12502,

    /// <summary>
    /// Retrieves the inbox messages for loggedin user.
    /// </summary>
    /// <remarks>Hidden method.</remarks>
    [Description("messages.getInbox")]
    MessagesGetInbox = 12503,

    /// <summary>
    /// Retrieves the number of unread messages in the inbox.
    /// </summary>
    /// <remarks>Hidden method.</remarks>
    [Description("messages.getUnreadCount")]
    MessagesGetUnreadCount = 12504,

    /// <summary>
    /// Send a private message to one or multiple users and send a private message to all 
    /// members of a hub or group (If using multiple users they will be visible for 
    /// every recipient except for hub or group messages).
    /// </summary>
    /// <remarks>Spam sensitive method.</remarks>
    [Description("messages.send")]
    MessagesSend = 12505,

    /// <summary>
    /// Send a private message to one or multiple users (If using multiple users they will 
    /// be visible for every recipient). 
    /// </summary>
    /// <remarks>Spam sensitive method.</remarks>
    [Description("messages.sendToUser")]
    MessagesSendToUser = 12506,

    /// <summary>
    /// Change the read status of a message. 
    /// </summary>
    /// <remarks>Hidden method.</remarks>
    [Description("messages.setRead")]
    MessagesSetRead = 12507,
		
    [Description("all_methods")]
		All = 99999
	}
}
