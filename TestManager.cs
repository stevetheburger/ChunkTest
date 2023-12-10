using Test.UI;
using Test.Data;
using Test.Utility;
using System.Collections.Generic;
using Godot;

namespace Test.Operations
{
	internal partial class TestManager : Node
	{
		[Export]
		private PackedScene ObjectPrefab;
		[Export]
		private TestData ObjectData;
		
		private List<TestObject> ActiveObjects;
		List<Int2D> ObjectSubs;
		private Queue<TestObject> InactiveObjects;
		
		private const double _StaleTime = 0.8;

		public override void _Ready()
		{
			base._Ready();
			ActiveObjects = new List<TestObject>();
			InactiveObjects = new Queue<TestObject>();
			ObjectSubs = new List<Int2D>();
		}

		public override void _Process(double delta)
		{
			base._Process(delta);

			//Check resources. Ensure that there is enough.
			TestObject obj;
			int count = ObjectSubs.Count - ActiveObjects.Count;
			int discrepancy = count - InactiveObjects.Count;

			while(discrepancy > 0)
			{
				GD.Print("Objects to create left: " + discrepancy);

				obj = ObjectPrefab.Instantiate<TestObject>();
				InactiveObjects.Enqueue(obj);
				--discrepancy;
			}
			while(count > 0)
			{				
				GD.Print("Objects to add to roll left: " + count);
				
				Int2D loc = ObjectSubs[count - 1];
				obj = InactiveObjects.Dequeue();
				obj.Locate(loc);
				
				ActiveObjects.Add(obj);
				AddChild(obj);
				--count;
			}

			//Make unneeded objects dormant.
			for(int i = 0; i < ActiveObjects.Count; ++i)
			{
				obj = ActiveObjects[i];

				//Check Time and recycle.
				if(obj.Time() > _StaleTime)
				{
					GD.Print("Hibernating Object at: " + i);
					ActiveObjects.Remove(obj);
					InactiveObjects.Enqueue(obj);
					RemoveChild(obj);
					continue;
				}
			}

			for(int i = 0; i < ActiveObjects.Count; ++i)
			{
				obj = ActiveObjects[i];

				GD.Print("Update object at: " + obj.Locate());

				//Update Time.
				obj.Time(delta);
			}
		}

		public void SetData(ref TestData data)
		{
			ObjectData = data;
			
			ObjectSubs.Clear();

			Int2D current = new();
			for(int i = 0; i < ObjectData.Size().Y; ++i)
			{
				current.Y = i;
				for(int e = 0; e < ObjectData.Size().X; ++e)
				{
					current.X = e;
					ObjectSubs.Add(current);
				}
			}
		}
	}
}
