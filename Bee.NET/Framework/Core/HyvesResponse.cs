// Copyright (c) 2007 - 2010, Nikhil Kothari and Beemway. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace Hyves.Service.Core
{
	/// <summary>
	/// Represents a response resulting from invoking a Hyves API method.
	/// </summary>
	public sealed class HyvesResponse
	{
		private HyvesResponseStatus status;
		private string message;
		private HyvesMethod method;
		private int timestampDifference;
		private int runningMilliseconds;
		private HyvesPaginateInformation paginateInformation;

		private string rawResponse;
		private object result;

		internal HyvesResponse(HttpStatusCode errorStatusCode, HyvesMethod method)
		{
			status = (HyvesResponseStatus)((uint)HyvesResponseStatus.HttpError | (uint)errorStatusCode);
			this.method = method;
		}

		internal HyvesResponse(Stream responseStream, HyvesMethod method)
		{
      this.method = method;
			paginateInformation = null;

			StreamReader streamReader = new StreamReader(responseStream);
      this.rawResponse = streamReader.ReadToEnd();
      
      try
      {
        JsonReader jsonReader = new JsonReader(new StringReader(this.rawResponse));
				object result = jsonReader.ReadValue();
				Hashtable jsonObject = result as Hashtable;
				if (jsonObject != null)
				{
					object errorCode = jsonObject["error_code"];
					if (errorCode != null)
					{
            this.status = (HyvesResponseStatus)(int)errorCode;
						message = (string)jsonObject["error_message"];
					}
          
					Hashtable info = jsonObject["info"] as Hashtable;
					if (info != null)
					{
						this.timestampDifference = (int)info["timestamp_difference"];
            this.runningMilliseconds = (int)info["running_milliseconds"];
            this.paginateInformation = new HyvesPaginateInformation(info);
          }
				}

        if (this.status == HyvesResponseStatus.Succeeded)
				{
          this.result = result;
				}
      }
			catch
			{
        this.status = HyvesResponseStatus.UnknownError;
			}
    }

    #region Properties
    /// <summary>
		/// Gets the error message if the response represents an error.
		/// </summary>
		public string ErrorMessage
		{
			get
			{
        if (this.message == null)
				{
					return String.Empty;
				}

        return this.message;
			}
		}

		/// <summary>
		/// Indicates whether the response represents an error.
		/// </summary>
		public bool IsError
		{
			get
			{
        return (this.status != HyvesResponseStatus.Succeeded);
			}
		}

		/// <summary>
		/// Gets the raw, textual response content.
		/// </summary>
		public string RawResponse
		{
			get
			{
        return this.rawResponse;
			}
		}

		/// <summary>
		/// Gets the result contained within the response.
		/// </summary>
		public object Result
		{
			get
			{
        return this.result;
			}
		}

		/// <summary>
		/// Gets more specific success or failure status of the response.
		/// </summary>
		public HyvesResponseStatus Status
		{
			get
			{
				return status;
			}
		}

		/// <summary>
		/// Gets the signed difference between the timestamp sent in the request, and the timestamp on the server.
		/// </summary>
		public int TimestampDifference
		{
			get
			{
        return this.timestampDifference;
			}
		}

		/// <summary>
		/// Gets the number of milliseconds that the call took to complete.
		/// </summary>
		public int RunningMilliseconds
		{
			get
			{
        return this.runningMilliseconds;
			}
		}

		/// <summary>
		/// Gets the paginate information
		/// </summary>
		public HyvesPaginateInformation PaginateInformation
		{
			get
			{
        return this.paginateInformation;
			}
    }
    #endregion

    internal static bool CoerceBoolean(object o)
		{
			if (o is bool)
			{
				return (bool)o;
			}
			if (o is int)
			{
				return ((int)o) == 1;
			}
			if (o is string)
			{
				return (String.CompareOrdinal((string)o, "1") == 0);
			}
			if (o is ArrayList)
			{
				Debug.Assert(((ArrayList)o).Count == 1);
				Debug.Assert(((ArrayList)o)[0] is bool);
				return (bool)((ArrayList)o)[0];
			}
			return false;
		}

		internal static int CoerceInt32(object o)
		{
			if (o is int)
			{
				return (int)o;
			}

			if (o is string)
			{
				int result = -1;
				int.TryParse((string)o, out result);
				return result;
			}

			return -1;
		}

		internal static float CoerceFloat(object o)
		{
			if (o is float)
			{
				return (float)o;
			}

			if (o is string)
			{
				float result = -1;
				float.TryParse((string)o, out result);
				return result;
			}

			return -1;
		}

		internal static DateTime CoerceDateTime(object o)
		{
			if (o is int)
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((int)o);
			}

			if (o is long)
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((long)o);
			}

			return DateTime.MinValue;
		}

		internal static string CoerceString(object o)
		{
			if (o is string)
			{
				return (string)o;
			}

			if (o == null)
			{
				return String.Empty;
			}

			return o.ToString();
    }

    internal Collection<T> ProcessResponse<T>(string fieldName)
      where T : HyvesEntity, new()
    {
      Collection<T> collection = new Collection<T>();

      Debug.Assert(this.Result is Hashtable);
      Hashtable result = (Hashtable)this.Result;

      Debug.Assert(result[fieldName] is ArrayList);
      ArrayList list = (ArrayList)result[fieldName];

      for (int i = 0; i < list.Count; i++)
      {
        Debug.Assert(list[i] is Hashtable);
        T t = new T();
        t.Initialize((Hashtable)list[i]);      
        collection.Add(t);
      }

      return collection;
    }

    internal T ProcessSingleItemResponse<T>(string fieldName)
      where T : HyvesEntity, new()
    {
      Debug.Assert(this.Result is Hashtable);
      Hashtable result = (Hashtable)this.Result;

      Debug.Assert(result[fieldName] is ArrayList);
      ArrayList list = (ArrayList)result[fieldName];

      if (list != null && list.Count > 0)
      {
        T t = new T();
        t.Initialize((Hashtable)list[0]);
        return t;
      }

      return null;
    }
	}
}
