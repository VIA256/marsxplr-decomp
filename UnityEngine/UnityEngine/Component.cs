using System;
using System.Runtime.CompilerServices;
using Boo.Lang;

namespace UnityEngine
{
	public class Component : Object
	{
		public extern Transform transform
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Rigidbody rigidbody
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Camera camera
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Light light
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Animation animation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ConstantForce constantForce
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Renderer renderer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AudioSource audio
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern GUIText guiText
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern NetworkView networkView
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[DuckTyped]
		[Obsolete("Please use guiTexture instead")]
		public extern GUIElement guiElement
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern GUITexture guiTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[DuckTyped]
		public extern Collider collider
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern HingeJoint hingeJoint
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ParticleEmitter particleEmitter
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern GameObject gameObject
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("the active property is deprecated on components. Please use gameObject.active instead. If you meant to enable / disable a single component use enabled instead.")]
		public extern bool active
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern string tag
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public T GetComponent<T>() where T : Component
		{
			return GetComponent(typeof(T)) as T;
		}

		public T GetComponentInChildren<T>() where T : Component
		{
			return (T)GetComponentInChildren(typeof(T));
		}

		public T[] GetComponentsInChildren<T>(bool includeInactive) where T : Component
		{
			return gameObject.GetComponentsInChildren<T>(includeInactive);
		}

		public T[] GetComponentsInChildren<T>() where T : Component
		{
			return GetComponentsInChildren<T>(false);
		}

		public T[] GetComponents<T>() where T : Component
		{
			return (T[])GetComponentsWithCorrectReturnType(typeof(T));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DuckTyped]
		public extern Component GetComponent(Type type);

		[DuckTyped]
		public Component GetComponent(string type)
		{
			return gameObject.GetComponent(type);
		}

		[DuckTyped]
		public Component GetComponentInChildren(Type t)
		{
			return gameObject.GetComponentInChildren(t);
		}

		[DuckTyped]
		public Component[] GetComponentsInChildren(Type t)
		{
			bool includeInactive = false;
			return GetComponentsInChildren(t, includeInactive);
		}

		public Component[] GetComponentsInChildren(Type t, bool includeInactive)
		{
			return gameObject.GetComponentsInChildren(t, includeInactive);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DuckTyped]
		public extern Component[] GetComponents(Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Component[] GetComponentsWithCorrectReturnType(Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CompareTag(string tag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessageUpwards(string methodName, object value, SendMessageOptions options);

		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			SendMessageUpwards(methodName, value, options);
		}

		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			SendMessageUpwards(methodName, value, options);
		}

		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			SendMessageUpwards(methodName, null, options);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessage(string methodName, object value, SendMessageOptions options);

		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			SendMessage(methodName, value, options);
		}

		public void SendMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			SendMessage(methodName, value, options);
		}

		public void SendMessage(string methodName, SendMessageOptions options)
		{
			SendMessage(methodName, null, options);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BroadcastMessage(string methodName, object parameter, SendMessageOptions options);

		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			BroadcastMessage(methodName, parameter, options);
		}

		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object parameter = null;
			BroadcastMessage(methodName, parameter, options);
		}

		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			BroadcastMessage(methodName, null, options);
		}
	}
}
