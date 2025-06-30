using System;
using System.Collections;
using Boo.Lang;
using Boo.Lang.Runtime;

namespace UnityScript.Lang
{
	[Serializable]
	public class UnityRuntimeServices
	{
		[Serializable]
		public abstract class ValueTypeChange
		{
			public object Target;

			public object Value;

			public bool IsValid
			{
				get
				{
					return (Value is ValueType) ? true : false;
				}
			}

			public ValueTypeChange(object target, object value)
			{
				Target = target;
				Value = value;
			}

			//FUCK public abstract virtual bool Propagate();
			public abstract bool Propagate();
		}

		[Serializable]
		public class MemberValueTypeChange : ValueTypeChange
		{
			public string Member;

			public MemberValueTypeChange(object target, string member, object value)
				: base(target, value)
			{
				Member = member;
			}

			public override bool Propagate()
			{
				if (!IsValid)
				{
					return false;
				}
				try
				{
					RuntimeServices.SetProperty(Target, Member, Value);
				}
				catch (MissingFieldException)
				{
					return false;
				}
				return true;
			}
		}

		[Serializable]
		public class SliceValueTypeChange : ValueTypeChange
		{
			public object Index;

			public SliceValueTypeChange(object target, object index, object value)
				: base(target, value)
			{
				Index = index;
			}

			public override bool Propagate()
			{
				if (!IsValid)
				{
					return false;
				}
				IList list = Target as IList;
				if (list != null)
				{
					list[RuntimeServices.UnboxInt32(Index)] = Value;
					return true;
				}
				try
				{
					RuntimeServices.SetSlice(Target, string.Empty, new object[2] { Index, Value });
				}
				catch (MissingFieldException)
				{
					return false;
				}
				return true;
			}
		}

		protected static Type EnumeratorType;

		public static readonly bool Initialized;

		static UnityRuntimeServices()
		{
			_0024static_initializer_0024();
			RuntimeServices.RegisterExtensions(typeof(Extensions));
			Initialized = true;
		}

		public static object Invoke(object target, string name, object[] args, Type scriptBaseType)
		{
			if (!Initialized)
			{
				throw new AssertionFailedException("Initialized");
			}
			object obj = RuntimeServices.Invoke(target, name, args);
			if (obj == null)
			{
				return null;
			}
			if (!IsGenerator(obj))
			{
				return obj;
			}
			if (!target.GetType().IsSubclassOf(scriptBaseType))
			{
				return obj;
			}
			if (IsStaticMethod(target.GetType(), name, args))
			{
				return obj;
			}
			return RuntimeServices.Invoke(target, "StartCoroutine_Auto", new object[1] { obj });
		}

		public static object GetProperty(object target, string name)
		{
			if (!Initialized)
			{
				throw new AssertionFailedException("Initialized");
			}
			try
			{
				return RuntimeServices.GetProperty(target, name);
			}
			catch (MissingMemberException)
			{
				if (target.GetType().IsValueType)
				{
					throw;
				}
				return ExpandoServices.GetExpandoProperty(target, name);
			}
		}

		public static object SetProperty(object target, string name, object value)
		{
			if (!Initialized)
			{
				throw new AssertionFailedException("Initialized");
			}
			try
			{
				return RuntimeServices.SetProperty(target, name, value);
			}
			catch (MissingMemberException)
			{
				if (target.GetType().IsValueType)
				{
					throw;
				}
				return ExpandoServices.SetExpandoProperty(target, name, value);
			}
		}

		public static Type GetTypeOf(object o)
		{
			if (o == null)
			{
				return null;
			}
			return o.GetType();
		}

		public static bool IsGenerator(object obj)
		{
			Type type = obj.GetType();
			if (type == EnumeratorType)
			{
				return true;
			}
			if (EnumeratorType.IsAssignableFrom(type))
			{
				return true;
			}
			return typeof(AbstractGenerator).IsAssignableFrom(type);
		}

		public static bool IsStaticMethod(Type type, string name, object[] args)
		{
			try
			{
				return type.GetMethod(name).IsStatic;
			}
			catch (Exception)
			{
				return true;
			}
		}

		public static IEnumerator GetEnumerator(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (IsValueTypeArray(obj) || obj is Array)
			{
				return new ListUpdateableEnumerator((IList)obj);
			}
			IEnumerator enumerator = obj as IEnumerator;
			if (enumerator != null)
			{
				return enumerator;
			}
			return RuntimeServices.GetEnumerable(obj).GetEnumerator();
		}

		public static void Update(IEnumerator e, object newValue)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (e is ListUpdateableEnumerator)
			{
				((ListUpdateableEnumerator)e).Update(newValue);
			}
		}

		public static bool IsValueTypeArray(object obj)
		{
			if (!(obj is System.Array))
			{
				return false;
			}
			return obj.GetType().GetElementType().IsValueType;
		}

		public static void PropagateValueTypeChanges(ValueTypeChange[] changes)
		{
			int i = 0;
			for (int length = changes.Length; i < length && changes[i].Propagate(); i = checked(i + 1))
			{
			}
		}

		static void _0024static_initializer_0024()
		{
			EnumeratorType = typeof(IEnumerator);
		}
	}
}
