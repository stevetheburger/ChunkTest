using Godot;

namespace Test.Utility
{
	public class Int2D
	{
		public int X, Y;
		public Int2D()
		{
			X = 0;
			Y = 0;
		}
		public Int2D(int x, int y)
		{
			X = x;
			Y = y;
		}
		public Int2D(Int2D orig)
		{
			X = orig.X;
			Y = orig.Y;
		}
		public static Int2D operator +(Int2D l, Int2D r)
		{
			return new Int2D(l.X + r.X, l.Y + r.Y);
		}
		public static Int2D operator -(Int2D l, Int2D r)
		{
			return new Int2D(l.X - r.X, l.Y - r.Y);
		}
		public static bool operator >(Int2D l, Int2D r)
		{
			return l.Y > r.Y || l.Y == r.Y && l.X > r.X;
		}
		public static bool operator <(Int2D l, Int2D r)
		{
			return l.Y < r.Y || l.Y == r.Y && l.X < r.X;
		}
		public static bool operator ==(Int2D l, Int2D r)
		{
			return l.Y == r.Y && l.X == r.X;
		}
		public static bool operator !=(Int2D l, Int2D r)
		{
			return l.Y != r.Y && l.X != r.X;
		}
		public static bool operator >=(Int2D l, Int2D r)
		{
			return l.Y > r.Y || l.Y == r.Y && l.X >= r.X;
		}
		public static bool operator <=(Int2D l, Int2D r)
		{
			return l.Y < r.Y || l.Y == r.Y && l.X <= r.X;
		}

		public static Int2D Zero
		{
			get
			{
				return new Int2D(0, 0);
			}

			private set { }
		}

		public override bool Equals(object obj)
		{
			if (obj is Int2D int2)
			{
				return int2.X == X && int2.Y == Y;
			}
			else return false;
		}

		public int ToIndex(Int2D dimension)
		{
			return dimension.X * Y + X;
		}

		public Vector2I ToGodot()
		{
			Vector2I ret = new Vector2I();
			ret.X = X;
			ret.Y = Y;
			return ret;
		}

		public override string ToString()
		{
			return "" + X + ", " + Y;
		}
	}
}
