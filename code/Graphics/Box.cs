using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX // .Graphics
{
	using Win32;


	/// <summary>Defines a 3D box.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ff476089%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Source( "D3D11.h", "D3D11_BOX" )]
	//[Native( "D3D12.h", "D3D12_BOX" )]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 24 )]
	public struct Box : IEquatable<Box>
	{

		/// <summary>The x position of the left hand side of the box.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Left;

		/// <summary>The y position of the top of the box.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Top;

		/// <summary>The z position of the front of the box.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Front;

		/// <summary>The x position of the right hand side of the box.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Right;

		/// <summary>The y position of the bottom of the box.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Bottom;

		/// <summary>The z position of the back of the box.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Back;



		#region Constructors

		/// <summary>Initializes a new <see cref="Box"/>.</summary>
		/// <param name="left">The position of the left side of the box; also known as "X".</param>
		/// <param name="top">The position of the top of the box; also known as "Y".</param>
		/// <param name="front"></param>
		/// <param name="right">The position of the right side of the box.</param>
		/// <param name="bottom">The position of the bottom of the box.</param>
		/// <param name="back"></param>
		public Box( int left, int top, int front, int right, int bottom, int back )
		{
			Left = left;
			Top = top;
			Front = front;
			Right = right;
			Bottom = bottom;
			Back = back;
		}


		/// <summary>Initializes a new <see cref="Box"/>.</summary>
		/// <param name="topLeftCorner">A <see cref="Point"/> indicating the position of the upper left corner.</param>
		/// <param name="front"></param>
		/// <param name="size">A <see cref="Size"/> indicating the size of the rectangle.</param>
		/// <param name="depth"></param>
		public Box( Point topLeftCorner, int front, Size size, int depth )
		{
			Left = topLeftCorner.X;
			Top = topLeftCorner.Y;
			Front = front;
			Right = Left + size.Width;
			Bottom = Top + size.Height;
			Back = front + depth;
		}

		#endregion Constructors

		
		
		/// <summary>Gets or sets the position of the upper left corner of the box.</summary>
		public Point UpperLeftCorner
		{
			get { return new Point( Left, Top ); }
			set
			{
				Left = value.X;
				Top = value.Y;
			}
		}


		/// <summary>Gets or sets the position of the lower right corner of the box.</summary>
		public Point LowerRightCorner
		{
			get { return new Point( Right, Bottom ); }
			set
			{
				Right = value.X;
				Bottom = value.Y;
			}
		}


		/// <summary>Gets the center of the box.</summary>
		public Point Center { get { return new Point( ( Left + Right ) / 2, ( Top + Bottom ) / 2 ); } }


		/// <summary>Gets or sets the horizontal position of the left side (<see cref="Left"/>) of the box.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public int X
		{
			get { return Left; }
			set
			{
				Right += value - Left;
				Left = value;
			}
		}


		/// <summary>Gets or sets the vertical position of the upper side (<see cref="Top"/>) of the box.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public int Y
		{
			get { return Top; }
			set
			{
				Bottom += value - Top;
				Top = value;
			}
		}


		/// <summary>Gets or sets the width of the box.</summary>
		public int Width
		{
			get { return Math.Abs( Right - Left ); }
			set { Right = Left + value; }
		}


		/// <summary>Gets or sets the height of the box.</summary>
		public int Height
		{
			get { return Math.Abs( Bottom - Top ); }
			set { Bottom = Top + value; }
		}


		/// <summary>Gets or sets the depth of the box.</summary>
		public int Depth
		{
			get { return Math.Abs( Back - Front ); }
			set { Back = Front + value; }
		}


		/// <summary>Gets a value indicating whether the size of this <see cref="Box"/> is zero.</summary>
		public bool IsEmpty { get { return ( Left == Right ) && ( Top == Bottom ) && ( Front == Back ); } }

		
		/// <summary>Gets a value indicating whether the <see cref="Left"/>, <see cref="Top"/>, <see cref="Front"/>, <see cref="Right"/>, <see cref="Bottom"/> and <see cref="Back"/> members of this <see cref="Box"/> are set to zero.</summary>
		public bool IsZero { get { return ( Left == 0 ) && ( Right == 0 ) && ( Front == 0 ) && ( Top == 0 ) && ( Bottom == 0 ) && ( Back == 0 ); } }



		#region Contains

		/// <summary>Returns a value indicating whether this rectangle contains or intersects a point, given its coordinates.</summary>
		/// <param name="x">The position of the point along the horizontal axis.</param>
		/// <param name="y">The position of the point along the vertical axis.</param>
		/// <param name="z">The position of the point along the depth axis.</param>
		/// <returns>Returns a value indicating whether this rectangle contains or intersects the specified coordinates.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public ContainmentType Contains( int x, int y, int z )
		{
			if( x < Left || x > Right || y < Top || y > Bottom || z < Front || z > Back )
				return ContainmentType.Disjoint;

			if( ( x > Left && x < Right ) && ( y > Top && y < Bottom ) && ( z > Front && z < Back ) )
				return ContainmentType.Contains;

			return ContainmentType.Intersects;
		}


		/// <summary>Indicates whether this rectangle contains or intersects with another rectangle.</summary>
		/// <param name="other">A <see cref="Box"/> structure.</param>
		/// <param name="result">Receives a value indicating whether the <paramref name="other"/> <see cref="Box"/> intersects with or is contained by this <see cref="Box"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Contains( ref Box other, out ContainmentType result )
		{
			if( Left < other.Left && Right > other.Right && Top < other.Top && Bottom > other.Bottom && Front < other.Front && Back > other.Back )
				result = ContainmentType.Contains;
			else if( Left > other.Right || Right < other.Left || Top > other.Bottom || Bottom < other.Top || Front > other.Back || Back < other.Front )
				result = ContainmentType.Disjoint;
			else
				result = ContainmentType.Intersects;
		}

		/// <summary>Returns a value indicating whether this rectangle contains or intersects a rectangle.</summary>
		/// <param name="other">A <see cref="Box"/> structure.</param>
		/// <returns>Returns a value indicating whether this rectangle contains or intersects another rectangle.</returns>
		public ContainmentType Contains( Box other )
		{
			if( Left < other.Left && Right > other.Right && Top < other.Top && Bottom > other.Bottom && Front < other.Front && Back > other.Back )
				return ContainmentType.Contains;

			if( Left > other.Right || Right < other.Left || Top > other.Bottom || Bottom < other.Top || Front > other.Back || Back < other.Front )
				return ContainmentType.Disjoint;

			return ContainmentType.Intersects;
		}

		#endregion Contains


		#region Intersects

		/// <summary>Determines whether this <see cref="Box"/> intersects another <see cref="Box"/>.</summary>
		/// <param name="other">A <see cref="Box"/>.</param>
		/// <param name="result">Receives a value indicating whether the boxes intersect.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref Box other, out bool result )
		{
			result = ( Right > other.X && Left < other.Right ) && ( Bottom > other.Top && Top < other.Bottom ) && ( Front > other.Front && Back < other.Back ); 
		}

		/// <summary>Returns a value indicating whether this <see cref="Box"/> intersects another <see cref="Box"/>.</summary>
		/// <param name="other">A <see cref="Box"/>.</param>
		/// <returns>Returns a value indicating whether the boxes intersect.</returns>
		public bool Intersects( Box other )
		{
			return ( Right > other.X && Left < other.Right ) && ( Bottom > other.Top && Top < other.Bottom ) && ( Front > other.Front && Back < other.Back );
		}

		#endregion Intersects


		/// <summary>Offsets the rectangle by the specified amounts.</summary>
		/// <param name="horizontalAmount">The horizontal amount.</param>
		/// <param name="verticalAmount">The vertical amount.</param>
		/// <param name="depthAmount">The depth amount.</param>
		public void Offset( int horizontalAmount, int verticalAmount, int depthAmount )
		{
			Left += horizontalAmount;
			Top += verticalAmount;
			Front += depthAmount;
			Right += horizontalAmount;
			Bottom += verticalAmount;
			Back += depthAmount;
		}


		/// <summary>Pushes the edges of the rectangle by the specified amounts.</summary>
		/// <param name="horizontalAmount">The horizontal amount.</param>
		/// <param name="verticalAmount">The vertical amount.</param>
		/// <param name="depthAmount">The depth amount.</param>
		public void Inflate( int horizontalAmount, int verticalAmount, int depthAmount )
		{
			Left -= horizontalAmount;
			Top -= verticalAmount;
			Front -= depthAmount;
			Right += horizontalAmount;
			Bottom += verticalAmount;
			Back += depthAmount;
		}


		/// <summary>Returns a hash code for this <see cref="Box"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Box"/>.</returns>
		public override int GetHashCode()
		{
			return Left ^ Top ^ Front ^ Right ^ Bottom ^ Back;
		}


		/// <summary>Returns a value indicating whether this <see cref="Box"/> equals another <see cref="Box"/>.</summary>
		/// <param name="other">A <see cref="Box"/>.</param>
		/// <returns>Returns true if this <see cref="Box"/> and the <paramref name="other"/> <see cref="Box"/> are equal, otherwise returns false.</returns>
		public bool Equals( Box other )
		{
			return ( Left == other.Left ) && ( Top == other.Top ) && ( Front == other.Front ) && ( Right == other.Right ) && ( Bottom == other.Bottom ) && ( Back == other.Back );
		}


		/// <summary>Returns a value indicating whether this <see cref="Box"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Box"/> which equals this <see cref="Box"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is Box b && this.Equals( b );
		}


		/// <summary>Returns a string representing this <see cref="Box"/>.</summary>
		/// <returns>Returns a string representing this <see cref="Box"/>.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Left: {0}, Top: {1}, Front: {2}, Right: {3}, Bottom: {4}, Back: {5}}}", Left, Top, Front, Right, Bottom, Back );
		}


		/// <summary>The «zero» <see cref="Box"/>.</summary>
		public static readonly Box Zero;


		#region Static methods

		/// <summary>Negates a <see cref="Box"/>.</summary>
		/// <param name="box">A <see cref="Box"/>.</param>
		/// <param name="result">Receives the negated <paramref name="box"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Negate( ref Box box, out Box result )
		{
			result.Left = -box.Right;
			result.Top = -box.Bottom;
			result.Front = -box.Back;
			result.Right = -box.Left;
			result.Bottom = -box.Top;
			result.Back = -box.Front;
		}

		/// <summary>Negates a <see cref="Box"/>.</summary>
		/// <param name="box">A <see cref="Box"/>.</param>
		/// <returns>Returns the negated <paramref name="box"/>.</returns>
		public static Box Negate( Box box )
		{
			return new Box( -box.Right, -box.Bottom, -box.Back, -box.Left, -box.Top, -box.Front );
		}


		/// <summary>Adds two boxes.</summary>
		/// <param name="box">A <see cref="Box"/>.</param>
		/// <param name="other">A <see cref="Box"/>.</param>
		/// <param name="result">Receives the sum of the two specified boxes.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref Box box, ref Box other, out Box result )
		{
			result.Left = box.Left + other.Left;
			result.Top = box.Top + other.Top;
			result.Front = box.Front + other.Front;
			result.Right = box.Right + other.Right;
			result.Bottom = box.Bottom + other.Bottom;
			result.Back = box.Back + other.Back;
		}

		/// <summary>Adds two boxes.</summary>
		/// <param name="box">A <see cref="Box"/>.</param>
		/// <param name="other">A <see cref="Box"/>.</param>
		/// <returns>Returns the sum of the two specified boxes.</returns>
		public static Box Add( Box box, Box other )
		{
			box.Left += other.Left;
			box.Top += other.Top;
			box.Front += other.Front;
			box.Right += other.Right;
			box.Bottom += other.Bottom;
			box.Back += other.Back;
			return box;
		}


		/// <summary>Subtracts two boxes.</summary>
		/// <param name="box">A <see cref="Box"/>.</param>
		/// <param name="other">A <see cref="Box"/>.</param>
		/// <param name="result">Receives the difference between the two specified boxes.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref Box box, ref Box other, out Box result )
		{
			result.Left = box.Left - other.Left;
			result.Top = box.Top - other.Top;
			result.Front = box.Front - other.Front;
			result.Right = box.Right - other.Right;
			result.Bottom = box.Bottom - other.Bottom;
			result.Back = box.Back - other.Back;
		}

		/// <summary>Subtracts two boxes.</summary>
		/// <param name="box">A <see cref="Box"/>.</param>
		/// <param name="other">A <see cref="Box"/>.</param>
		/// <returns>Returns the difference between the two specified boxes.</returns>
		public static Box Subtract( Box box, Box other )
		{
			box.Left -= other.Left;
			box.Top -= other.Top;
			box.Front -= other.Front;
			box.Right -= other.Right;
			box.Bottom -= other.Bottom;
			box.Back -= other.Back;
			return box;
		}


		/// <summary>Creates a <see cref="Box"/> structure initialized with the minimum values from two boxes.</summary>
		/// <param name="box">A <see cref="Box"/> structure.</param>
		/// <param name="other">A <see cref="Box"/> structure.</param>
		/// <param name="result">Receives a <see cref="Box"/> structure initialized with the minimum values from the two specified boxes.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Min( ref Box box, ref Box other, out Box result )
		{
			result.Left = ( other.Left < box.Left ) ? other.Left : box.Left;
			result.Top = ( other.Top < box.Top ) ? other.Top : box.Top;
			result.Front = ( other.Front < box.Front ) ? other.Front : box.Front;
			result.Right = ( other.Right < box.Right ) ? other.Right : box.Right;
			result.Bottom = ( other.Bottom < box.Bottom ) ? other.Bottom : box.Bottom;
			result.Back = ( other.Back < box.Back ) ? other.Back : box.Back;
		}

		/// <summary>Returns a <see cref="Box"/> structure initialized with the minimum values from two boxes.</summary>
		/// <param name="box">A <see cref="Box"/> structure.</param>
		/// <param name="other">A <see cref="Box"/> structure.</param>
		/// <returns>Returns a <see cref="Box"/> structure initialized with the minimum values from the two specified boxes.</returns>
		public static Box Min( Box box, Box other )
		{
			if( other.Left < box.Left )
				box.Left = other.Left;

			if( other.Top < box.Top )
				box.Top = other.Top;

			if( other.Front < box.Front )
				box.Front = other.Front;

			if( other.Right < box.Right )
				box.Right = other.Right;

			if( other.Bottom < box.Bottom )
				box.Bottom = other.Bottom;

			if( other.Back < box.Back )
				box.Back = other.Back;

			return box;
		}


		/// <summary>Creates a <see cref="Box"/> structure initialized with the maximum values from two boxes.</summary>
		/// <param name="box">A <see cref="Box"/> structure.</param>
		/// <param name="other">A <see cref="Box"/> structure.</param>
		/// <param name="result">Receives a <see cref="Box"/> structure initialized with the maximum values from the two specified boxes.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Max( ref Box box, ref Box other, out Box result )
		{
			result.Left = ( other.Left > box.Left ) ? other.Left : box.Left;
			result.Top = ( other.Top > box.Top ) ? other.Top : box.Top;
			result.Front = ( other.Front > box.Front ) ? other.Front : box.Front;
			result.Right = ( other.Right > box.Right ) ? other.Right : box.Right;
			result.Bottom = ( other.Bottom > box.Bottom ) ? other.Bottom : box.Bottom;
			result.Back = ( other.Back > box.Back ) ? other.Back : box.Back;
		}

		/// <summary>Returns a <see cref="Box"/> structure initialized with the maximum values from two boxes.</summary>
		/// <param name="box">A <see cref="Box"/> structure.</param>
		/// <param name="other">A <see cref="Box"/> structure.</param>
		/// <returns>Returns a <see cref="Box"/> structure initialized with the maximum values from the two specified boxes.</returns>
		public static Box Max( Box box, Box other )
		{
			if( other.Left > box.Left )
				box.Left = other.Left;

			if( other.Top > box.Top )
				box.Top = other.Top;

			if( other.Front > box.Front )
				box.Front = other.Front;

			if( other.Right > box.Right )
				box.Right = other.Right;

			if( other.Bottom > box.Bottom )
				box.Bottom = other.Bottom;

			if( other.Back > box.Back )
				box.Back = other.Back;

			return box;
		}


		///// <summary>Creates a rectangle containing the two specified rectangles.</summary>
		///// <param name="rect">A <see cref="Rect"/>.</param>
		///// <param name="other">A <see cref="Rect"/>.</param>
		///// <param name="result">Receives the rectangle containing the two specified rectangles.</param>
		//[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		//[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		//public static void Union( ref Rect rect, ref Rect other, out Rect result )
		//{
		//	result.Left = Math.Min( rect.Left, other.Left );
		//	result.Top = Math.Min( rect.Top, other.Top );
		//	result.Right = Math.Max( rect.Right, other.Right );
		//	result.Bottom = Math.Max( rect.Bottom, other.Bottom );
		//}

		///// <summary>Returns a rectangle containing the two specified rectangles.</summary>
		///// <param name="rect">A <see cref="Rect"/>.</param>
		///// <param name="other">A <see cref="Rect"/>.</param>
		///// <returns>Returns a rectangle containing the two specified rectangles.</returns>
		//public static Rect Union( Rect rect, Rect other )
		//{
		//	rect.Left = Math.Min( rect.Left, other.Left );
		//	rect.Top = Math.Min( rect.Top, other.Top );
		//	rect.Right = Math.Max( rect.Right, other.Right );
		//	rect.Bottom = Math.Max( rect.Bottom, other.Bottom );
		//	return rect;
		//}


		///// <summary>Creates a <see cref="Rect"/> defining the area where one rectangle overlaps with another rectangle.</summary>
		///// <param name="rect">A <see cref="Rect"/>.</param>
		///// <param name="other">A <see cref="Rect"/>.</param>
		///// <param name="result">Receives the area where the two rectangles overlap.</param>
		//[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		//[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		//public static void Intersect( ref Rect rect, ref Rect other, out Rect result )
		//{
		//	var left = Math.Max( rect.Left, other.Left );
		//	var top = Math.Max( rect.Top, other.Top );
		//	var right = Math.Min( rect.Right, other.Right );
		//	var bottom = Math.Min( rect.Bottom, other.Bottom );

		//	if( right > left && bottom > top )
		//	{
		//		result.Left = left;
		//		result.Top = top;
		//		result.Right = right;
		//		result.Bottom = bottom;
		//		return;
		//	}

		//	result = Rect.Zero;
		//}

		///// <summary>Returns a <see cref="Rect"/> defining the area where one rectangle overlaps with another rectangle.</summary>
		///// <param name="rect">A <see cref="Rect"/>.</param>
		///// <param name="other">A <see cref="Rect"/>.</param>
		///// <returns>Returns the area where the two rectangles overlap.</returns>
		//public static Rect Intersect( Rect rect, Rect other )
		//{
		//	var left = Math.Max( rect.Left, other.Left );
		//	var top = Math.Max( rect.Top, other.Top );
		//	var right = Math.Min( rect.Right, other.Right );
		//	var bottom = Math.Min( rect.Bottom, other.Bottom );

		//	if( right > left && bottom > top )
		//	{
		//		rect.Left = left;
		//		rect.Top = top;
		//		rect.Right = right;
		//		rect.Bottom = bottom;
		//		return rect;
		//	}

		//	return Rect.Zero;
		//}

		#endregion Static methods


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="rect">A <see cref="Box"/>.</param>
		/// <param name="other">A <see cref="Box"/>.</param>
		/// <returns>Returns true if the boxes are equal, otherwise returns false.</returns>
		public static bool operator ==( Box rect, Box other )
		{
			return rect.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="rect">A <see cref="Box"/>.</param>
		/// <param name="other">A <see cref="Box"/>.</param>
		/// <returns>Returns true if the boxes  are not equal, otherwise returns false.</returns>
		public static bool operator !=( Box rect, Box other )
		{
			return !rect.Equals( other );
		}


		/// <summary>Negation operator.</summary>
		/// <param name="box">A <see cref="Box"/>.</param>
		/// <returns>Returns the negated <paramref name="box"/>.</returns>
		public static Box operator -( Box box )
		{
			var x = box.Left;
			var y = box.Top;
			var z = box.Front;
			box.Left = -box.Right;
			box.Top = -box.Bottom;
			box.Front -= box.Back;
			box.Right = -x;
			box.Bottom = -y;
			box.Back = -z;
			return box;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="box">A <see cref="Box"/> structure.</param>
		/// <param name="other">A <see cref="Box"/> structure.</param>
		/// <returns>Returns the sum of the two specified boxes.</returns>
		public static Box operator +( Box box, Box other )
		{
			box.Left += other.Left;
			box.Top += other.Top;
			box.Front += other.Front;
			box.Right += other.Right;
			box.Bottom += other.Bottom;
			box.Back += other.Back;
			return box;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="box">A <see cref="Box"/> structure.</param>
		/// <param name="other">A <see cref="Box"/> structure.</param>
		/// <returns>Returns the difference of the two specified boxes.</returns>
		public static Box operator -( Box box, Box other )
		{
			box.Left -= other.Left;
			box.Top -= other.Top;
			box.Front -= other.Front;
			box.Right -= other.Right;
			box.Bottom -= other.Bottom;
			box.Back -= other.Back;
			return box;
		}

		#endregion Operators

	}

}