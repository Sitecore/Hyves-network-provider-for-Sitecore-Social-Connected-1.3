// Copyright (c) 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the service APIs that allow access to information on Hyves buzz.
	/// </summary>
	public sealed class BuzzService
	{
		private HyvesSession session;

		internal BuzzService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}
    
		#region GetBuzz
		/// <summary>
		/// Gets the buzz of famous hyvers. This corresponds to the
    /// buzz.getFamous Hyves method.
    /// </summary>
    /// <param name="responsefields">Get extra information from the buzz.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the buzz; null if the call fails.</returns>
    public Collection<Buzz> GetFamous(HyvesBuzzResponsefield responsefields, bool useFancyLayout)
		{
			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["ha_responsefields"] = ConvertResponsefieldsToString(responsefields);

			HyvesResponse response = request.InvokeMethod(HyvesMethod.BuzzGetFamous, useFancyLayout);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Buzz>("buzz");
			}

			return null;
		}
		#endregion
    
		#region Private methodes
    private string ConvertResponsefieldsToString(HyvesBuzzResponsefield responsefields)
    {
      StringBuilder responsefieldsBuilder = new StringBuilder();
      if (responsefields == HyvesBuzzResponsefield.All)
      {
        responsefieldsBuilder.Append(EnumHelper.GetAllValuesAsString<HyvesBuzzResponsefield>());
      }
      else
      {
        var userResponsefields = Enum.GetValues(typeof(HyvesBuzzResponsefield));
        foreach (HyvesBuzzResponsefield responseField in userResponsefields)
        {
          if (EnumHelper.HasFlag(responsefields, responseField))
          {
            responsefieldsBuilder.Append(string.Format("{0},", EnumHelper.GetDescription(responseField)));
          }
        }
      }

      responsefieldsBuilder = responsefieldsBuilder.Replace(
        string.Format("{0},", EnumHelper.GetDescription(HyvesBuzzResponsefield.All)), string.Empty);
      string returnValue = responsefieldsBuilder.ToString();
      return returnValue.Substring(0, returnValue.Length - 1);
    }
		#endregion
	}
}
