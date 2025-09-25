using System;
using System.Reflection;

namespace Boo.Lang.Runtime
{
	public class CandidateMethod
	{
		public const int ExactMatchScore = 7;

		public const int UpCastScore = 6;

		public const int WideningPromotion = 5;

		public const int ImplicitConversionScore = 4;

		public const int NarrowingPromotion = 3;

		public const int DowncastScore = 2;

		private readonly MethodInfo _method;

		private readonly int[] _argumentScores;

		private readonly bool _varArgs;

		public MethodInfo Method
		{
			get
			{
				return _method;
			}
		}

		public int[] ArgumentScores
		{
			get
			{
				return _argumentScores;
			}
		}

		public bool VarArgs
		{
			get
			{
				return _varArgs;
			}
		}

		public int MinimumArgumentCount
		{
			get
			{
				if (!_varArgs)
				{
					return Parameters.Length;
				}
				return Parameters.Length - 1;
			}
		}

		public ParameterInfo[] Parameters
		{
			get
			{
				return _method.GetParameters();
			}
		}

		public Type VarArgsParameterType
		{
			get
			{
				return GetParameterType(Parameters.Length - 1).GetElementType();
			}
		}

		public bool DoesNotRequireConversions
		{
			get
			{
				return !Array.Exists(_argumentScores, RequiresConversion);
			}
		}

		public static int CalculateArgumentScore(Type paramType, Type argType)
		{
			if (argType == null)
			{
				if (paramType.IsValueType)
				{
					return -1;
				}
				return 7;
			}
			if (paramType == argType)
			{
				return 7;
			}
			if (paramType.IsAssignableFrom(argType))
			{
				return 6;
			}
			if (argType.IsAssignableFrom(paramType))
			{
				return 2;
			}
			if (IsNumericPromotion(paramType, argType))
			{
				if (NumericTypes.IsWideningPromotion(paramType, argType))
				{
					return 5;
				}
				return 3;
			}
			MethodInfo methodInfo = RuntimeServices.FindImplicitConversionOperator(argType, paramType);
			if (methodInfo != null)
			{
				return 4;
			}
			return -1;
		}

		public CandidateMethod(MethodInfo method, int argumentCount, bool varArgs)
		{
			_method = method;
			_argumentScores = new int[argumentCount];
			_varArgs = varArgs;
		}

		private static bool RequiresConversion(int score)
		{
			return score < 5;
		}

		public Type GetParameterType(int i)
		{
			return Parameters[i].ParameterType;
		}

		public static bool IsNumericPromotion(Type paramType, Type argType)
		{
			if (RuntimeServices.IsPromotableNumeric(Type.GetTypeCode(paramType)))
			{
				return RuntimeServices.IsPromotableNumeric(Type.GetTypeCode(argType));
			}
			return false;
		}
	}
}
