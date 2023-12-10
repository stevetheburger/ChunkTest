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
		
		private Dictionary<int, TestObject> ActiveObjects;
		List<Int2D> ObjectSubs;
		private Queue<TestObject> InactiveObjects;
		
		private const double _StaleTime = 0.8;

		public override void _Ready()
		{
			base._Ready();
			ActiveObjects = new Dictionary<int, TestObject>();
			InactiveObjects = new Queue<TestObject>();
			ObjectSubs = new List<Int2D>();
		}

		public override void _Process(double delta)
		{
			base._Process(delta);

			//Check resources. Ensure that there is enough.
			TestObject obj;
			int key;
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
				
				ActiveObjects.Add(loc.ToIndex(ObjectData.Size()), obj);
				AddChild(obj);
				--count;
			}

			//Make unneeded objects dormant.
			foreach(KeyValuePair<int, TestObject> obj_key_pair in ActiveObjects)
			{
				obj = obj_key_pair.Value;
				key = obj_key_pair.Key;

				//Check Time and recycle.
				if(obj.Time() > _StaleTime)
				{
					GD.Print("Hibernating Object at: " + key);
					ActiveObjects.Remove(key);
					InactiveObjects.Enqueue(obj);
					RemoveChild(obj);
					continue;
				}
			}

			foreach(KeyValuePair<int, TestObject> obj_key_pair in ActiveObjects)
			{
				//Update Time.
				obj_key_pair.Value.Time(delta);
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
