// Copyright (c) 2007, Nikhil Kothari. All Rights Reserved.

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace Hyves.Service
{
	/// <summary>
	/// Represents a data object returned from the Hyves service.
	/// </summary>
	public abstract class HyvesEntity
	{
		private Hashtable _entityState;

		/// <summary>
		/// Initializes a Hyves entity with the specified data.
		/// </summary>
		internal HyvesEntity()
		{
		}

    internal HyvesEntity(Hashtable entityState)
    {
      if (entityState == null)
      {
        entityState = new Hashtable();
      }

      _entityState = entityState;
    }

    /// <summary></summary>
    /// <param name="entityState">A set of name/value pairs representing the entity.</param>
    internal void Initialize(Hashtable entityState)
    {
      if (entityState == null)
      {
        entityState = new Hashtable();
      }

      _entityState = entityState;
    }

		/// <summary>
		/// The state of the entity represented as name/value pairs.
		/// </summary>
		protected Hashtable EntityState
		{
			get
			{
				return _entityState;
			}
		}

		/// <summary>
		/// Gets or sets the specified name value from the entity's state.
		/// </summary>
		/// <param name="key">The name of the value.</param>
		/// <returns>The current value associated with the specified name.</returns>
		protected object this[string key]
		{
			get
			{
				return _entityState[key];
			}
			set
			{
				_entityState[key] = value;
			}
		}

		/// <summary>
		/// Gets the specified name value from the entity's state.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="key">The name of the value.</param>
		/// <returns>The current value associated with the specified name.</returns>
		protected T GetState<T>(string key)
		{
			object value = _entityState[key];
			if (value == null)
			{
				return default(T);
			}

			Debug.Assert(typeof(T).IsAssignableFrom(value.GetType()));
			return (T)value;
		}

    protected T TransformEntity<T>(Hashtable entityState)
      where T : HyvesEntity, new()
    {
      T t = new T();
      t.Initialize(entityState);
      return t;
    }
	}
}
