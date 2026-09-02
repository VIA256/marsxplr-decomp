using System.Collections;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class MonoBehaviour : Behaviour
	{
		public const int kIgnoreRaycastLayer = 4;

		public const int kDefaultRaycastLayers = -5;

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern MonoBehaviour();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CancelInvokeAll();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_IsInvokingAll();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Invoke(string methodName, float time);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InvokeRepeating(string methodName, float time, float repeatRate);

		public void CancelInvoke()
		{
			Internal_CancelInvokeAll();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CancelInvoke(string methodName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsInvoking(string methodName);

		public bool IsInvoking()
		{
			return Internal_IsInvokingAll();
		}

		public Coroutine StartCoroutine(IEnumerator routine)
		{
			return StartCoroutine_Auto(routine);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Coroutine StartCoroutine_Auto(IEnumerator routine);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Coroutine StartCoroutine(string methodName, object value);

		public Coroutine StartCoroutine(string methodName)
		{
			object value = null;
			return StartCoroutine(methodName, value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopCoroutine(string methodName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopAllCoroutines();

		public static void print(object o)
		{
			Debug.Log(o);
		}
	}
}
