using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Boo.Lang.Runtime
{
	internal class PromotionEmitter : DispatcherEmitter
	{
		private Type _toType;

		public PromotionEmitter(Type toType)
			: base(toType, "NumericPromotion")
		{
			_toType = toType;
		}

		protected override void EmitMethodBody()
		{
			_il.Emit(OpCodes.Ldarg_0);
			MethodInfo methodInfo = EmitPromotion(_toType);
			EmitReturn(methodInfo.ReturnType);
		}
	}
}
