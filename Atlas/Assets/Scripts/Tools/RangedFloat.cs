using System;

[Serializable]
public struct RangedFloat
{
	public float minimum;
	public float maximum;
}

namespace Tools
{
	[Serializable]
	public class Range<TNumericType>
	{
		public TNumericType Min;
		public TNumericType Max;
	}
	
	[Serializable]
	public class RangeFloat : Range<float>
	{
		//public float Min;
		//public float Max;
	}
}
