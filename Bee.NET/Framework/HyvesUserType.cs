// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.ComponentModel;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the different types of users. 
	/// </summary>
  public enum HyvesUserType : int
	{
		/// <summary>
		/// A normal user.
		/// </summary>
		[Description("")]
    Normal = 0,

		/// <summary>
		/// A Goldmember.
    /// </summary>
    Goldmember = 1,

		/// <summary>
    /// A member of the Hyves team.
    /// </summary>
    HTeam = 2,

    /// <summary>
    /// A designer.
    /// </summary>
    Designer = 3,

    /// <summary>
    /// A commercial designer.
    /// </summary>
    CommercialDesigner = 4,

    /// <summary>
    /// A pionier.
    /// </summary>
    Pionier = 5,

    /// <summary>
    /// A artiest.
    /// </summary>
    Artiest = 6,

    /// <summary>
    /// A politicus.
    /// </summary>
    Politicus = 7,

    /// <summary>
    /// A sport.
    /// </summary>
    Sport = 8,

    /// <summary>
    /// A acteurs.
    /// </summary>
    Acteurs = 9,

    /// <summary>
    /// A presentatoren.
    /// </summary>
    Presentatoren = 10,

    /// <summary>
    /// A reality.
    /// </summary>
    Reality = 11,

    /// <summary>
    /// A auteurs.
    /// </summary>
    Auteurs = 12,

    /// <summary>
    /// A nep.
    /// </summary>
    Nep = 13,

    /// <summary>
    /// A betatester.
    /// </summary>
    BetaTester = 14,

    /// <summary>
    /// A barter partner.
    /// </summary>
    BarterPartner = 15,

    /// <summary>
    /// A deceased.
    /// </summary>
    Deceased = 16,

    /// <summary>
    /// A ex-crew.
    /// </summary>
    ExCrew = 17,
	}
}
