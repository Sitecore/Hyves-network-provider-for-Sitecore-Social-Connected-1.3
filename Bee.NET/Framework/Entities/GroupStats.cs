// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents a Hyves hub stats.
	/// </summary>
	public sealed class HubStats : HyvesEntity
	{
    private bool subscribeCountTransformed;
    private bool awaitingApprovalTransformed;

    internal HubStats(Hashtable entityState)
		{
      base.Initialize(entityState);
		}

    /// <summary>
    /// The number of users of the hub.
    /// </summary>
    public int UsersCount
    {
      get
      {
        if (this.subscribeCountTransformed == false)
        {
          return TransformSubscribeCount();
        }

        return (int)this["subscribecount"];
      }
    }

		/// <summary>
		/// The number of scraps of the hub.
		/// </summary>
		public int ScrapsCount
		{
			get
			{
        if (this.awaitingApprovalTransformed == false)
				{
          return TransformAwaitingApproval();
				}

        return (int)this["awaitingapproval"];
			}
		}

    private int TransformSubscribeCount()
		{
      Debug.Assert(this.subscribeCountTransformed == false);

      int count = HyvesResponse.CoerceInt32(this["subscribecount"]);

      this["subscribecount"] = count;

      this.subscribeCountTransformed = true;

			return count;
		}

    private int TransformAwaitingApproval()
		{
      Debug.Assert(this.awaitingApprovalTransformed == false);

      int count = HyvesResponse.CoerceInt32(this["awaitingapproval"]);

      this["awaitingapproval"] = count;

      this.awaitingApprovalTransformed = true;

			return count;
		}
	}
}
