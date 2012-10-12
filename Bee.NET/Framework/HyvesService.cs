// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using Hyves.Service.Core;

namespace Hyves.Service
{
	public class HyvesService
	{
		private HyvesSession session;

    private AlbumsService albumsService;
    private AuthenticationService authenticationService;
    private BlogsService blogsService;
    private BuzzService buzzService;
    private CityService cityService;
    private CountryService countryService;
    private EventsService eventsService;
    private FancyLayoutService fancyLayoutService;
		private FriendsService friendsService;
    private GadgetsService gadgetsService;
    private HubCategoriesService hubCategoriesService;
    private HubsService hubsService;
    private MediaService mediaService;
    private MessagesService messagesService;
		private ListenersService listenersService;
		private PingsService pingsService;
    private PrivateSpotsService privateSpotsService;
    private RegionService regionService;
    private SearchService searchService;
    private ThreadsService threadsService;
    private TipsService tipsService;
    private UsersService usersService;
    private WwwsService wwwService;

		#region Properties
		/// <summary>
		/// Gets the service APIs that allow accessing user information.
		/// </summary>
		public UsersService Users
		{
			get
			{
        if (this.usersService == null)
				{
          this.usersService = new UsersService(session);
				}

        return this.usersService;
			}
		}

    /// <summary>
    /// Gets the service APIs that allow accessing and creating album information.
    /// </summary>
    public AlbumsService Albums
    {
      get
      {
        if (this.albumsService == null)
        {
          this.albumsService = new AlbumsService(session);
        }

        return this.albumsService;
      }
    }

		/// <summary>
		/// Gets the service APIs that allow accessing and creating blog information.
		/// </summary>
		public BlogsService Blogs
		{
			get
			{
				if (this.blogsService == null)
				{
          this.blogsService = new BlogsService(session);
				}

        return this.blogsService;
			}
		}

    /// <summary>
    /// Gets the service APIs that allow accessing buzz information.
    /// </summary>
    public BuzzService Buzz
    {
      get
      {
        if (this.buzzService == null)
        {
          this.buzzService = new BuzzService(session);
        }

        return this.buzzService;
      }
    }

    /// <summary>
    /// Gets the service APIs that allow accessing event information.
    /// </summary>
    public EventsService Events
    {
      get
      {
        if (this.eventsService == null)
        {
          this.eventsService = new EventsService(session);
        }

        return this.eventsService;
      }
    }

		/// <summary>
		/// Gets the service APIs that allow accessing friends information.
		/// </summary>
		public FriendsService Friends
		{
			get
			{
        if (this.friendsService == null)
				{
          this.friendsService = new FriendsService(session);
				}

        return this.friendsService;
			}
		}

		/// <summary>
		/// Gets the service APIs that allow accessing media information.
		/// </summary>
		public MediaService Media
		{
			get
			{
        if (this.mediaService == null)
				{
          this.mediaService = new MediaService(session);
				}

        return this.mediaService;
			}
		}

		/// <summary>
		/// Gets the service APIs that allow accessing and creating gadget information.
		/// </summary>
		public GadgetsService Gadgets
		{
			get
			{
        if (this.gadgetsService == null)
				{
          this.gadgetsService = new GadgetsService(session);
				}

        return this.gadgetsService;
			}
    }

    /// <summary>
    /// Gets the service APIs that allow accessing hub category information.
    /// </summary>
    public HubCategoriesService HubCategories
    {
      get
      {
        if (this.hubCategoriesService == null)
        {
          this.hubCategoriesService = new HubCategoriesService(session);
        }

        return this.hubCategoriesService;
      }
    }

    /// <summary>
    /// Gets the service APIs that allow accessing hub information.
    /// </summary>
    public HubsService Hubs
    {
      get
      {
        if (this.hubsService == null)
        {
          this.hubsService = new HubsService(session);
        }

        return this.hubsService;
      }
    }

		/// <summary>
		/// Gets the service APIs that allow accessing country information.
		/// </summary>
		public CountryService Country
		{
			get
			{
        if (this.countryService == null)
				{
          this.countryService = new CountryService(session);
				}

        return this.countryService;
			}
		}

		/// <summary>
		/// Gets the service APIs that allow accessing city information.
		/// </summary>
		public CityService City
		{
			get
			{
        if (this.cityService == null)
				{
          this.cityService = new CityService(session);
				}

        return this.cityService;
			}
		}

		/// <summary>
		/// Gets the service APIs that allow parsing strings to fancy layout.
		/// </summary>
		public FancyLayoutService FancyLayout
		{
			get
			{
        if (this.fancyLayoutService == null)
				{
          this.fancyLayoutService = new FancyLayoutService(session);
				}

        return this.fancyLayoutService;
			}
		}

		/// <summary>
		/// Gets the service APIs that allow revoking accesstokens
		/// </summary>
		public AuthenticationService Authentication
		{
			get
			{
        if (this.authenticationService == null)
				{
          this.authenticationService = new AuthenticationService(session);
				}

        return this.authenticationService;
			}
		}

		/// <summary>
		/// Gets the service APIs that allow accessing and creating listener information.
		/// </summary>
		public ListenersService Listeners
		{
			get
			{
        if (this.listenersService == null)
				{
          this.listenersService = new ListenersService(session);
				}

        return this.listenersService;
			}
		}

    /// <summary>
    /// Gets the service APIs that allow accessing region information.
    /// </summary>
    public RegionService Region
    {
      get
      {
        if (this.regionService == null)
        {
          this.regionService = new RegionService(session);
        }

        return this.regionService;
      }
    }

		/// <summary>
		/// Gets the service APIs that allow accessing ping information.
		/// </summary>
		public PingsService Pings
		{
			get
			{
        if (this.pingsService == null)
				{
          this.pingsService = new PingsService(session);
				}

        return this.pingsService;
			}
		}

    /// <summary>
    /// Gets the service APIs that allow accessing private spot information.
    /// </summary>
    public PrivateSpotsService PrivateSpots
    {
      get
      {
        if (this.privateSpotsService == null)
        {
          this.privateSpotsService = new PrivateSpotsService(session);
        }

        return this.privateSpotsService;
      }
    }

		/// <summary>
		/// Gets the service APIs that allow searching.
		/// </summary>
    public SearchService Search
		{
			get
			{
        if (this.searchService == null)
				{
          this.searchService = new SearchService(session);
				}

        return this.searchService;
			}
    }

    /// <summary>
    /// Gets the service APIs that allow accessing thread information.
    /// </summary>
    public ThreadsService Threads
    {
      get
      {
        if (this.threadsService == null)
        {
          this.threadsService = new ThreadsService(session);
        }

        return this.threadsService;
      }
    }

    /// <summary>
    /// Gets the service APIs that allow accessing tip information.
    /// </summary>
    public TipsService Tips
    {
      get
      {
        if (this.tipsService == null)
        {
          this.tipsService = new TipsService(session);
        }

        return this.tipsService;
      }
    }

    /// <summary>
    /// Gets the service APIs that allow accessing and creating www information.
    /// </summary>
    public WwwsService Wwws
    {
      get
      {
        if (this.wwwService == null)
        {
          this.wwwService = new WwwsService(session);
        }

        return this.wwwService;
      }
    }

    /// <summary>
    /// Gets the service APIs that allow accessing message information.
    /// </summary>
    public MessagesService Messages
    {
      get
      {
        if (this.messagesService == null)
        {
          this.messagesService = new MessagesService(session);
        }

        return this.messagesService;
      }
    }
		
		/// <summary>
		/// The current Hyves session associated with this service.
		/// </summary>
		public HyvesSession Session
		{
			get
			{
        return this.session;
			}
		}

		/// <summary>
		/// Gets the ID of the user associated with the current session.
		/// </summary>
		public string UserId
		{
			get
			{
				return this.session.UserId;
			}
		}
		#endregion

		/// <summary>
		/// Initializes an instance of a HyvesService with the specified
		/// session.
		/// </summary>
		/// <param name="session">The session associated with requests issued to the service.</param>
		public HyvesService(HyvesSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}

      this.session = session;
		}

		/// <summary>
		/// Creates an instance of a HyvesService with the specified application
		/// and infinite session information.
		/// </summary>
		/// <param name="consumerKey">The consumer-key used as an Consumer key.</param>
		/// <param name="consumerSecret">The consumer secret used to sign requests.</param>
		/// <param name="methods">The methods supported in this application.</param>
    public HyvesService(string consumerKey, string consumerSecret, List<HyvesMethod> methods)
			: this(new HyvesSession(consumerKey, consumerSecret, methods))
		{
		}

		/// <summary>
		/// Creates an instance of a HyvesService with the specified application
		/// and infinite session information.
		/// </summary>
		/// <param name="consumerKey">The consumer-key used as an Consumer key.</param>
		/// <param name="consumerSecret">The consumer secret used to sign requests.</param>
		/// <param name="methods">The methods supported in this application.</param>
		/// <param name="token">The previously saved access token.</param>
		/// <param name="tokenSecret">The previously saved access token secret.</param>
		/// <param name="userId">The user Id associated with the access token.</param>
    public HyvesService(string consumerKey, string consumerSecret, List<HyvesMethod> methods, string token, string tokenSecret, string userId)
			: this(new HyvesInfiniteSession(consumerKey, consumerSecret, methods, token, tokenSecret, userId))
		{
		}
	}
}
