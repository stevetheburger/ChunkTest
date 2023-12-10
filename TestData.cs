using Test.Utility;
using Godot;

namespace Test.Data
{
	public partial class TestData : Node
	{
		private Int2D Dim;
	
		public TestData()
		{
			Dim = new();
		}
		
		public void SetSize(Int2D dim)
		{
			Dim = dim;
		}
		
		public Int2D Size()
		{
			return Dim;
		}
	}
}
