// Copyright (c) 2008 - 2010, Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

using Hyves.Service.Core;

namespace Hyves.Service
{
	/// <summary>
	/// Represents the service APIs that allow access to information on Hyves listener (Who What Where).
	/// </summary>
	public sealed class ListenersService
	{
		private HyvesSession session;

		internal ListenersService(HyvesSession session)
		{
			Debug.Assert(session != null);
			this.session = session;
		}

		/// <summary>
		/// Gets the desired information about the specified listener. This corresponds to the
		/// listeners.get Hyves method.
		/// </summary>
		/// <param name="listenerId">The requested listenerId.</param>
		/// <returns>The information about the specified listener; null if the call fails.</returns>
		public Listener GetListener(string listenerId)
		{
			if (string.IsNullOrEmpty(listenerId))
			{
				throw new ArgumentNullException("listenerId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["listenerid"] = listenerId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.ListenersGet, false);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Listener>("listener");
			}

			return null;
		}

		/// <summary>
		/// Gets the desired information about the specified listener. This corresponds to the
		/// listeners.get Hyves method.
		/// </summary>
		/// <param name="listenerId">The requested listenerIds.</param>
		/// <param name="useFancyLayout">Display information the same way that that is being done on the site, including things like smilies.</param>
		/// <returns>The information about the specified listener; null if the call fails.</returns>
		public Collection<Listener> GetListeners(Collection<string> listenerIds)
		{
			if (listenerIds == null || listenerIds.Count == 0)
			{
				throw new ArgumentNullException("listenerIds");
			}

			StringBuilder listenerIdBuilder = new StringBuilder();
			if (listenerIds != null)
			{
				foreach (string id in listenerIds)
				{
					if (listenerIdBuilder.Length != 0)
					{
						listenerIdBuilder.Append(",");
					}
					listenerIdBuilder.Append(id);
				}
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["listenerid"] = listenerIdBuilder.ToString();

			HyvesResponse response = request.InvokeMethod(HyvesMethod.ListenersGet, false);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Listener>("listener");
			}

			return null;
		}

		/// <summary>
		/// Gets the desired listeners from the specified type. This corresponds to the
		/// listeners.getByType Hyves method.
		/// </summary>
		/// <param name="type">Type of listeners to retrieve.</param>
		/// <returns>The information about the specified listener; null if the call fails.</returns>
		public Collection<Listener> GetListenersByType(HyvesListenerType type)
		{			
			if (type == HyvesListenerType.NotSpecified)
			{
				throw new ArgumentNullException("type");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			switch (type)
			{
				case HyvesListenerType.AccesstokenRevoke:
					request.Parameters["type"] = "accesstoken_revoke";
					break;
			}

			HyvesResponse response = request.InvokeMethod(HyvesMethod.ListenersGetByType, false);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Listener>("listener");
			}

			return null;
		}

		/// <summary>
		/// Gets the desired listeners from the specified type. This corresponds to the
		/// listeners.getAll Hyves method.
		/// </summary>
		/// <returns>The information about the specified listener; null if the call fails.</returns>
		public Collection<Listener> GetAllListeners()
		{
			HyvesRequest request = new HyvesRequest(this.session);
			
			HyvesResponse response = request.InvokeMethod(HyvesMethod.ListenersGetAll, false);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessResponse<Listener>("listener");
			}

			return null;
		}

		/// <summary>
		/// Create a new listener for the ApiConsumer. This corresponds to the
		/// listener.create Hyves method.
		/// </summary>
		/// <param name="type">Type of listener to create.</param>
		/// <param name="callback">Url to do the callback to.</param>
		/// <returns>The new listener; null if the call fails.</returns>
		public Listener CreateListener(HyvesListenerType type, string callback)
		{
			if (type == HyvesListenerType.NotSpecified)
			{
				throw new ArgumentException("type");
			}

			if (string.IsNullOrEmpty(callback))
			{
				throw new ArgumentException("callback");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			switch (type)
			{
				case HyvesListenerType.AccesstokenRevoke:
					request.Parameters["type"] = "accesstoken_revoke";
					break;
			}

			request.Parameters["callback"] = callback;
			
			HyvesResponse response = request.InvokeMethod(HyvesMethod.ListenersCreate);
			if (response.Status == HyvesResponseStatus.Succeeded)
      {
        return response.ProcessSingleItemResponse<Listener>("listener");
			}

			return null;
		}

		/// <summary>
		/// Deletes a listener for the ApiConsumer. This corresponds to the
		/// listener.create Hyves method.
		/// </summary>
		/// <param name="listenerId">The requested listenerId.</param>
		/// <returns>The new listener; null if the call fails.</returns>
		public bool DeleteListener(string listenerId)
		{
			if (string.IsNullOrEmpty(listenerId))
			{
        throw new ArgumentException("listenerId");
			}

			HyvesRequest request = new HyvesRequest(this.session);
			request.Parameters["listenerid"] = listenerId;

			HyvesResponse response = request.InvokeMethod(HyvesMethod.ListenersCreate);
			if (response.Status == HyvesResponseStatus.Succeeded)
			{
				Debug.Assert(response.Result is Hashtable);
				Hashtable result = (Hashtable)response.Result;

				Debug.Assert(result["success"] is bool);

				return (bool)result["success"];
			}

			return false;
		}
	}
}
