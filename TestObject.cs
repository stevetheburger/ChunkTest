using Test.Utility;
using Godot;

namespace Test.UI
{
	internal partial class TestObject : TileMap
	{
		private Int2D Location;
		private double TimeStamp;
		
		public void Locate(Int2D val)
		{
			Location = val;
		}
		public Int2D Locate()
		{
			return Location;
		}
		
		public void Time(double delta)
		{
			TimeStamp += delta;
		}
		public double Time()
		{
			return TimeStamp;
		}
	}
}
