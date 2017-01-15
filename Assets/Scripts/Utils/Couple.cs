using System;

namespace IAR_AdaptiveCuriosity
{
	public class Couple<T,U>
	{

		public T first;

		public U second;

		public Couple (T f, U s)
		{
			first = f;
			second = s;
		}
	}
}

