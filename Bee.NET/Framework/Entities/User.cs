// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents a Hyves user.
	/// </summary>
	public class User : HyvesEntity
	{
		private bool profileVisibleTransformed;
		private bool genderTransformed;
		private bool birthdayTransformed;
		private bool createdTransformed;
		private bool scrapsCountTransformed;
		private bool testimonialsCountTransformed;
    private bool respectsCountTransformed;
    private bool viewsCountTransformed;

    public User()
		{
		}

		/// <summary>
		/// The ID of the user.
		/// </summary>
		public string UserId
		{
			get
			{
				return GetState<string>("userid");
			}
		}

		/// <summary>
		/// Indicate if a profile is visible
		/// </summary>
		public bool ProfileVisible
		{
			get
			{
				if (profileVisibleTransformed == false)
				{
					return TransformProfileVisible();
				}
				return (bool)this["profilevisible"];
			}
		}

		/// <summary>
		/// The "Nickname" of the user.
		/// </summary>
		public string Nickname
		{
			get
			{
				return GetState<string>("nickname") ?? String.Empty;
			}
		}

		/// <summary>
		/// The firstname of the user.
		/// </summary>
		public string Firstname
		{
			get
			{
				return GetState<string>("firstname") ?? String.Empty;
			}
		}

		/// <summary>
		/// The lastname of the user.
		/// </summary>
		public string Lastname
		{
			get
			{
				return GetState<string>("lastname") ?? String.Empty;
			}
		}

		/// <summary>
		/// The user's gender as shared on the profile.
		/// </summary>
		public UserGender Gender
		{
			get
			{
				if (genderTransformed == false)
				{
					return TransformGender();
				}

				return (UserGender)this["gender"];
			}
		}

		/// <summary>
		/// The 'on my mind' as shared on the profile.
		/// </summary>
		public string OnMyMind
		{
			get
			{
				return GetState<string>("onmymind") ?? String.Empty;
			}
		}

		/// <summary>
		/// The user's birthday information as shared on the profile.
		/// </summary>
		public DateTime Birthday
		{
			get
			{
				if (birthdayTransformed == false)
				{
					return TransformBirthday();
				}

				return (DateTime)this["birthday"];
			}
		}

		/// <summary>
		/// The age of the user
		/// </summary>
		public int Age
		{
			get
			{
				DateTime birthday = Birthday;
				int years = DateTime.Now.Year - birthday.Year;
        // subtract another year if we're before the
        // birth day in the current year
        if (DateTime.Now.Month < birthday.Month || (DateTime.Now.Month == birthday.Month && DateTime.Now.Day < birthday.Day))
        {
          years--;
        }

        return years;
			}
		}

		/// <summary>
		/// The number of friends this user has
		/// </summary>
		public int FriendsCount
		{
			get
			{
				return GetState<int>("friendscount");
			}
		}

		/// <summary>
		/// the profile url of the user
		/// </summary>
		public string Url
		{
			get
			{
				return GetState<string>("url") ?? String.Empty;
			}
		}
		
		/// <summary>
		/// The country of the user.
		/// </summary>
		public string Country
		{
			get
			{
				return GetState<string>("countryname");
			}
		}

		/// <summary>
		/// The city of the user.
		/// </summary>
		public string City
		{
			get
			{
				return GetState<string>("cityname") ?? String.Empty;
			}
		}

		/// <summary>
		/// The profile picture of the user.
		/// </summary>
		public Media ProfilePicture
		{
			get
			{
        return TransformEntity<Media>((Hashtable)this["profilepicture"]);
			}
		}

		/// <summary>
		/// The whitespaces of the user.
		/// </summary>
		public ArrayList Whitespaces
		{
			get
			{
				if (this["whitespaces"] != null)
				{
					ArrayList list = (ArrayList)((Hashtable)this["whitespaces"])["whitespace"];
					for (int i = 0; i < list.Count; i++)
					{
						list[i] = (Hashtable)list[i];
					}

					return list;
				}
				else
				{
					return new ArrayList();
				}
			}
		}

		/// <summary>
		/// Comma seperated list of brands
		/// </summary>
		public string Brands
		{
			get
			{
				return GetState<string>("brands") ?? String.Empty;
			}
		}

		/// <summary>
		/// Comma seperated list of interests
		/// </summary>
		public string Interest
		{
			get
			{
				return GetState<string>("interest") ?? String.Empty;
			}
		}

		/// <summary>
		/// The date the user was created.
		/// </summary>
		public DateTime Created
		{
			get
			{
				if (createdTransformed == false)
				{
					return TransformCreated();
				}

				return (DateTime)this["created"];
			}
		}

		/// <summary>
		/// The number of scraps of the user
		/// </summary>
		public int ScrapsCount
		{
			get
			{
				if (scrapsCountTransformed == false)
				{
					return TransformScrapsCount();
				}

				return (int)this["scrapscount"];
			}
		}

		/// <summary>
		/// The number of testimonials of the user
		/// </summary>
		public int TestimonialsCount
		{
			get
			{
				if (testimonialsCountTransformed == false)
				{
					return TransformTestimonialsCount();
				}

				return (int)this["testimonialscount"];
			}
		}

		/// <summary>
		/// The number of respects of the user
		/// </summary>
		public int RespectsCount
		{
			get
			{
				if (respectsCountTransformed == false)
				{
					return TransformRespectsCount();
				}

				return (int)this["respectscount"];
			}
    }

    public string AboutMe
    {
      get
      {
        return GetState<string>("aboutme") ?? String.Empty;
      }
    }

    public string FancyLayoutTag
    {
      get
      {
        return GetState<string>("fancylayouttag") ?? String.Empty;
      }
    }

    public int ViewsCount
    {
      get
      {
        if (viewsCountTransformed == false)
        {
          return TransformViewsCount();
        }
        return (int)this["viewscount"];
      }
    }

    public string RelationType
    {
      get
      {
        return GetState<string>("relationtype") ?? String.Empty;
      }
    }

    public string MobileUrl
    {
      get
      {
        return GetState<string>("mobileurl") ?? String.Empty;
      }
    }
    
    /// <summary>
    /// The location of the user.
    /// </summary>
    public Geolocation Geolocation
    {
      get
      {
        return TransformEntity<Geolocation>((Hashtable)this["geolocation"]);
      }
    }

    #region Private methods
    private bool TransformProfileVisible()
		{
			Debug.Assert(profileVisibleTransformed == false);

			bool visible = HyvesResponse.CoerceBoolean(this["profilevisible"]);

			this["profilevisible"] = visible;

			profileVisibleTransformed = true;

			return visible;
		}

		private UserGender TransformGender()
		{
			Debug.Assert(genderTransformed == false);

			UserGender gender = UserGender.NotSpecified;
			string state = GetState<string>("gender") ?? String.Empty;

			if (state.Length != 0)
			{
				if (state.Equals("male"))
				{
					gender = UserGender.Male;
				}
				else if (state.Equals("female"))
				{
					gender = UserGender.Female;
				}
			}

			this["gender"] = gender;
			genderTransformed = true;

			return gender;
		}

		private DateTime TransformBirthday()
		{
			Debug.Assert(birthdayTransformed == false);

			Hashtable table = (Hashtable)this["birthday"];

			int year = HyvesResponse.CoerceInt32(table["year"]);
			if (year == -1) year = DateTime.Now.Year;
			int month = HyvesResponse.CoerceInt32(table["month"]);
			int day = HyvesResponse.CoerceInt32(table["day"]);

			DateTime date = DateTime.MinValue;
			if (month > 0 && day > 0)
			{
				date = new DateTime(year, month, day);
			}
			this["birthday"] = date;

			birthdayTransformed = true;

			return date;
		}
    		
		private DateTime TransformCreated()
		{
			Debug.Assert(createdTransformed == false);

			int timestamp = HyvesResponse.CoerceInt32(this["created"]);

			DateTime date = new DateTime(1970, 1, 1).AddSeconds(timestamp);
			this["created"] = date;

			createdTransformed = true;

			return date;
		}

		private int TransformScrapsCount()
		{
			Debug.Assert(scrapsCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["scrapscount"]);

			this["scrapscount"] = count;

			scrapsCountTransformed = true;

			return count;
		}

		private int TransformTestimonialsCount()
		{
			Debug.Assert(testimonialsCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["testimonialscount"]);

			this["testimonialscount"] = count;

			testimonialsCountTransformed = true;

			return count;
		}

		private int TransformRespectsCount()
		{
			Debug.Assert(respectsCountTransformed == false);

			int count = HyvesResponse.CoerceInt32(this["respectscount"]);

			this["respectscount"] = count;

			respectsCountTransformed = true;

			return count;
    }

    private int TransformViewsCount()
    {
      Debug.Assert(this.viewsCountTransformed == false);

      int count = HyvesResponse.CoerceInt32(this["viewscount"]);

      this["viewscount"] = count;

      this.viewsCountTransformed = true;

      return count;
    }
    #endregion
  }
}
