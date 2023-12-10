using Test.Data;
using Godot;

namespace Test.Operations
{
	public partial class TestScene : Node2D
	{
		[Export]
		private TestManager TestMgr = null;
		[Export]
		private TestData ObjectData = null;
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();
			ObjectData.SetSize(new(10, 10));
			if(TestMgr != null && ObjectData != null)
				TestMgr.SetData(ref ObjectData);
		}
	}
}
