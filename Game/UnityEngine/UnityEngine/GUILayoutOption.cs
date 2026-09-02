namespace UnityEngine
{
	public class GUILayoutOption
	{
		public enum Type
		{
			fixedWidth,
			fixedHeight,
			minWidth,
			maxWidth,
			minHeight,
			maxHeight,
			stretchWidth,
			stretchHeight,
			alignStart,
			alignMiddle,
			alignEnd,
			alignJustify,
			equalSize,
			spacing
		}

		public Type type;

		public object value;

		public GUILayoutOption(Type type, object value)
		{
			this.type = type;
			this.value = value;
		}
	}
}
