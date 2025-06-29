using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public class PhysicMaterial : Object
	{
		public extern float dynamicFriction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float staticFriction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float bouncyness
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3 frictionDirection2
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float dynamicFriction2
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float staticFriction2
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern PhysicMaterialCombine frictionCombine
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern PhysicMaterialCombine bounceCombine
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 frictionDirection
		{
			get
			{
				return frictionDirection2;
			}
			set
			{
				frictionDirection2 = value;
			}
		}

		public PhysicMaterial()
		{
			Internal_CreateDynamicsMaterial(null);
		}

		public PhysicMaterial(string name)
		{
			Internal_CreateDynamicsMaterial(name);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CreateDynamicsMaterial(string name);
	}
}
