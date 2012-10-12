// Copyright (c) Nikhil Kothari, 2007. All Rights Reserved.

using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace Hyves.Service.Core
{
	internal sealed class JsonObject : Hashtable, ICustomTypeDescriptor
	{

		private PropertyDescriptorCollection _propDescs;

		private void BuildPropertyDescriptors()
		{
			ArrayList items = new ArrayList();

			foreach (DictionaryEntry entry in this)
			{
				Type valueType = (entry.Value == null) ? typeof(string) : entry.Value.GetType();
				items.Add(new JsonPropertyDescriptor((string)entry.Key, valueType));
			}

			PropertyDescriptor[] propDescs = (PropertyDescriptor[])items.ToArray(typeof(PropertyDescriptor));
			_propDescs = new PropertyDescriptorCollection(propDescs, /* readOnly */ true);
		}

		#region Implementation of ICustomTypeDescriptor
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return null;
		}

		string ICustomTypeDescriptor.GetClassName()
		{
			return String.Empty;
		}

		string ICustomTypeDescriptor.GetComponentName()
		{
			return String.Empty;
		}

		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return null;
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return null;
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return ((ICustomTypeDescriptor)this).GetProperties();
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			if (_propDescs == null)
			{
				BuildPropertyDescriptors();
			}
			return _propDescs;
		}

		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
		#endregion

		private sealed class JsonPropertyDescriptor : PropertyDescriptor
		{

			private Type _type;

			public JsonPropertyDescriptor(string propertyName, Type propertyType)
				: base(propertyName, null)
			{
				_type = propertyType;
			}

			public override bool CanResetValue(object component)
			{
				return false;
			}

			public override Type ComponentType
			{
				get
				{
					return typeof(JsonObject);
				}
			}

			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			public override Type PropertyType
			{
				get
				{
					return _type;
				}
			}

			public override object GetValue(object component)
			{
				if (!(component is JsonObject))
				{
					throw new ArgumentException("Unexpected component type.", "component");
				}

				return ((JsonObject)component)[Name];
			}

			public override void ResetValue(object component)
			{
				throw new NotSupportedException();
			}

			public override void SetValue(object component, object value)
			{
				if (!(component is JsonObject))
				{
					throw new ArgumentException("Unexpected component type.", "component");
				}

				((JsonObject)component)[Name] = value;
			}

			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}
		}
	}
}
