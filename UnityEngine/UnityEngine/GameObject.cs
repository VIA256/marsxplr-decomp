using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Boo.Lang;

namespace UnityEngine
{
	public class GameObject : Object
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

		public extern int layer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

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

		public GameObject gameObject
		{
			get
			{
				return this;
			}
		}

		public GameObject(string name)
		{
			Internal_CreateGameObject(this, name);
		}

		public GameObject()
		{
			Internal_CreateGameObject(this, null);
		}

		public GameObject(string name, params Type[] components)
		{
			Internal_CreateGameObject(this, name);
			int num = components.Length;
			for (int i = 0; i < num; i++)
			{
				Type componentType = components[i];
				AddComponent(componentType);
			}
		}

		public T GetComponent<T>() where T : Component
		{
			return GetComponent(typeof(T)) as T;
		}

		public T GetComponentInChildren<T>() where T : Component
		{
			return GetComponentInChildren(typeof(T)) as T;
		}

		public T[] GetComponents<T>() where T : Component
		{
			return (T[])GetComponentsWithCorrectReturnType(typeof(T));
		}

		public T[] GetComponentsInChildren<T>(bool includeInactive) where T : Component
		{
			ArrayList arrayList = new ArrayList();
			GetComponentsInChildrenRecurse(typeof(T), arrayList, includeInactive);
			return (T[])arrayList.ToArray(typeof(T));
		}

		public T[] GetComponentsInChildren<T>() where T : Component
		{
			return GetComponentsInChildren<T>(false);
		}

		public T AddComponent<T>() where T : Component
		{
			return AddComponent(typeof(T).ToString()) as T;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject CreatePrimitive(PrimitiveType type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DuckTyped]
		public extern Component GetComponent(Type type);

		[DuckTyped]
		public Component GetComponent(string type)
		{
			return GetComponentByName(type);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Component GetComponentByName(string type);

		[DuckTyped]
		public Component GetComponentInChildren(Type type)
		{
			if (active)
			{
				Component component = GetComponent(type);
				if (component != null)
				{
					return component;
				}
			}
			Transform transform = this.transform;
			if (transform != null)
			{
				foreach (Transform item in transform)
				{
					Component componentInChildren = item.gameObject.GetComponentInChildren(type);
					if (componentInChildren != null)
					{
						return componentInChildren;
					}
				}
			}
			return null;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DuckTyped]
		public extern Component[] GetComponents(Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Component[] GetComponentsWithCorrectReturnType(Type type);

		private void GetComponentsInChildrenRecurse(Type type, ArrayList array, bool includeInactive)
		{
			if (includeInactive || active)
			{
				array.AddRange(GetComponents(type));
			}
			Transform transform = this.transform;
			if (!(transform != null))
			{
				return;
			}
			foreach (Transform item in transform)
			{
				item.gameObject.GetComponentsInChildrenRecurse(type, array, includeInactive);
			}
		}

		[DuckTyped]
		public Component[] GetComponentsInChildren(Type type)
		{
			bool includeInactive = false;
			return GetComponentsInChildren(type, includeInactive);
		}

		public Component[] GetComponentsInChildren(Type type, bool includeInactive)
		{
			ArrayList arrayList = new ArrayList();
			GetComponentsInChildrenRecurse(type, arrayList, includeInactive);
			return (Component[])arrayList.ToArray(typeof(Component));
		}

		public void SetActiveRecursively(bool state)
		{
			foreach (Transform item in this.transform)
			{
				item.gameObject.SetActiveRecursively(state);
			}
			active = state;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CompareTag(string tag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject FindGameObjectWithTag(string tag);

		public static GameObject FindWithTag(string tag)
		{
			return FindGameObjectWithTag(tag);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject[] FindGameObjectsWithTag(string tag);

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

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DuckTyped]
		public extern Component AddComponent(string className);

		[DuckTyped]
		public Component AddComponent(Type componentType)
		{
			return AddComponent(componentType.ToString());
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateGameObject(GameObject mono, string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SampleAnimation(AnimationClip animation, float time);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("gameObject.PlayAnimation is not supported anymore. Use animation.Play")]
		public extern void PlayAnimation(AnimationClip animation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("gameObject.StopAnimation is not supported anymore. Use animation.Stop")]
		public extern void StopAnimation();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject Find(string name);
	}
}
