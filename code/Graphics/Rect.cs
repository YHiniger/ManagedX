using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX // .Graphics
{
	using Win32;


	/// <summary>Defines the (integer) coordinates of the upper-left and lower-right corners of a rectangle.
	/// <para>This structure is equivalent to the native structure <code>RECT</code> (defined in WinDef.h),
	/// and its aliases <code>D3D11_RECT</code> (defined in D3D11.h) and <code>D3D12_RECT</code> (defined in D3D12.h).
	/// </para>
	/// </summary>
	[System.Diagnostics.DebuggerStepThrough]
	[Source( "WinDef.h", "RECT" )]
	[Source( "D3D11.h", "D3D11_RECT" )]
	[Source( "D3D12.h", "D3D12_RECT" )]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct Rect : IEquatable<Rect>
	{

		/// <summary>The position of the left side of the rectangle; also known as "X".</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Left;

		/// <summary>The position of the top of the rectangle; also known as "Y".</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Top;

		/// <summary>The position of the right side of the rectangle; must be greater than or equal to <see cref="Left"/>.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Right;

		/// <summary>The position of the bottom of the rectangle; must be greater than or equal to <see cref="Top"/>.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Bottom;



		#region Constructors

		/// <summary>Initializes a new <see cref="Rect"/>.</summary>
		/// <param name="left">The position of the left side of the rectangle; also known as "X".</param>
		/// <param name="top">The position of the top of the rectangle; also known as "Y".</param>
		/// <param name="right">The position of the right side of the rectangle.</param>
		/// <param name="bottom">The position of the bottom of the rectangle.</param>
		public Rect( int left, int top, int right, int bottom )
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}


		/// <summary>Initializes a new <see cref="Rect"/>.</summary>
		/// <param name="topLeftCorner">A <see cref="Point"/> indicating the position of the upper left corner.</param>
		/// <param name="size">A <see cref="Size"/> indicating the size of the rectangle.</param>
		public Rect( Point topLeftCorner, Size size )
		{
			Left = topLeftCorner.X;
			Top = topLeftCorner.Y;
			Right = Left + size.Width;
			Bottom = Top + size.Height;
		}

		#endregion Constructors

		
		
		/// <summary>Gets or sets the position of the upper left corner of the rectangle.</summary>
		public Point UpperLeftCorner
		{
			get { return new Point( Left, Top ); }
			set
			{
				Left = value.X;
				Top = value.Y;
			}
		}

		
		/// <summary>Gets or sets the position of the lower right corner of the rectangle.</summary>
		public Point LowerRightCorner
		{
			get { return new Point( Right, Bottom ); }
			set
			{
				Right = value.X;
				Bottom = value.Y;
			}
		}


		/// <summary>Gets the center of the rectangle.</summary>
		public Point Center { get { return new Point( ( Left + Right ) / 2, ( Top + Bottom ) / 2 ); } }


		/// <summary>Gets or sets the horizontal position of the left side (<see cref="Left"/>) of the rectangle.</summary>
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


		/// <summary>Gets or sets the vertical position of the upper side (<see cref="Top"/>) of the rectangle.</summary>
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


		/// <summary>Gets or sets the width of the rectangle.</summary>
		public int Width
		{
			get { return Math.Abs( Right - Left ); }
			set { Right = Left + value; }
		}


		/// <summary>Gets or sets the height of the rectangle.</summary>
		public int Height
		{
			get { return Math.Abs( Bottom - Top ); }
			set { Bottom = Top + value; }
		}


		/// <summary>Gets or sets the size of the rectangle.</summary>
		public Size Size
		{
			get { return new Size( Math.Abs( Right - Left ), Math.Abs( Bottom - Top ) ); }
			set
			{
				Right = Left + value.Width;
				Bottom = Top + value.Height;
			}
		}


		/// <summary>Gets a value indicating whether the <see cref="Size"/> of this <see cref="Rect"/> is empty (see <see cref="ManagedX.Size.Empty"/>).</summary>
		public bool IsEmpty { get { return ( Left == Right ) && ( Top == Bottom ); } }

		
		/// <summary>Gets a value indicating whether the <see cref="Left"/>, <see cref="Top"/>, <see cref="Right"/> and <see cref="Bottom"/> of this <see cref="Rect"/> are set to zero.</summary>
		public bool IsZero { get { return ( Left == 0 ) && ( Right == 0 ) && ( Top == 0 ) && ( Bottom == 0 ); } }


		/// <summary>Returns an array containing the corners of this <see cref="Rect"/>.</summary>
		/// <returns>Returns an array containing the corners of this <see cref="Rect"/>.</returns>
		public Point[] GetCorners()
		{
			return new Point[]
			{
				new Point( Left, Top ),
				new Point( Right, Top ),
				new Point( Right, Bottom ),
				new Point( Left, Bottom )
			};
		}

		/// <summary>Copies the corners of this <see cref="Rect"/> to an array.</summary>
		/// <param name="corners">An array to receive the corners of this <see cref="Rect"/>; must not be null, and must contain at least 4 + <paramref name="startIndex"/> elements.</param>
		/// <param name="startIndex">The zero-based index the copy should start at.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void GetCorners( Point[] corners, int startIndex )
		{
			if( corners == null )
				throw new ArgumentNullException( "corners" );

			if( corners.Length < 4 )
				throw new ArgumentException( "Not enough corners.", "corners" );

			if( startIndex < 0 || startIndex + 4 > corners.Length )
				throw new ArgumentOutOfRangeException( "startIndex" );

			corners[ startIndex + 0 ] = new Point( Left, Top );
			corners[ startIndex + 1 ] = new Point( Right, Top );
			corners[ startIndex + 2 ] = new Point( Right, Bottom );
			corners[ startIndex + 3 ] = new Point( Left, Bottom );
		}


		#region Contains

		/// <summary>Returns a value indicating whether this rectangle contains or intersects a point, given its coordinates.</summary>
		/// <param name="x">The position of the point along the horizontal axis.</param>
		/// <param name="y">The position of the point along the vertical axis.</param>
		/// <returns>Returns a value indicating whether this rectangle contains or intersects the specified coordinates.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public ContainmentType Contains( int x, int y )
		{
			if( x < Left || x > Right || y < Top || y > Bottom )
				return ContainmentType.Disjoint;

			if( x > Left && x < Right && y > Top && y < Bottom )
				return ContainmentType.Contains;

			return ContainmentType.Intersects;
		}


		/// <summary>Obtains a value indicating whether this rectangle contains or intersects with a <see cref="Point"/>.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="result">Receives a value indicating whether the specified <paramref name="point"/> is contained by or intersects with this rectangle.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Contains( ref Point point, out ContainmentType result )
		{
			if( Left > point.X || Right < point.X || Top > point.Y || Bottom < point.Y )
				result = ContainmentType.Disjoint;
			else if( Left < point.X && Right > point.X && Top < point.Y && Bottom > point.Y )
				result = ContainmentType.Contains;
			else
				result = ContainmentType.Intersects;
		}

		/// <summary>Returns a value indicating whether this rectangle contains or intersects a point.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <returns>Returns a value indicating whether this rectangle contains or intersects a point.</returns>
		public ContainmentType Contains( Point point )
		{
			if( Left > point.X || Right < point.X || Top > point.Y || Bottom < point.Y )
				return ContainmentType.Disjoint;

			if( Left < point.X && Right > point.X && Top < point.Y && Bottom > point.Y )
				return ContainmentType.Contains;

			return ContainmentType.Intersects;
		}


		/// <summary>Indicates whether this rectangle contains or intersects with another rectangle.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="result">Receives a value indicating whether the specified <paramref name="rect"/> intersects with or is contained by this rectangle.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Contains( ref Rect rect, out ContainmentType result )
		{
			if( Left < rect.Left && Right > rect.Right && Top < rect.Top && Bottom > rect.Bottom )
				result = ContainmentType.Contains;
			else if( Left > rect.Right || Right < rect.Left || Top > rect.Bottom || Bottom < rect.Top )
				result = ContainmentType.Disjoint;
			else
				result = ContainmentType.Intersects;
		}

		/// <summary>Returns a value indicating whether this rectangle contains or intersects a rectangle.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns a value indicating whether this rectangle contains or intersects another rectangle.</returns>
		public ContainmentType Contains( Rect rect )
		{
			if( Left < rect.Left && Right > rect.Right && Top < rect.Top && Bottom > rect.Bottom )
				return ContainmentType.Contains;

			if( Left > rect.Right || Right < rect.Left || Top > rect.Bottom || Bottom < rect.Top )
				return ContainmentType.Disjoint;

			return ContainmentType.Intersects;
		}

		#endregion Contains


		#region Intersects

		/// <summary>Determines whether this <see cref="Rect"/> intersects another <see cref="Rect"/>.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="result">Receives a value indicating whether the rectangles intersect.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref Rect rect, out bool result )
		{
			result = ( Right > rect.X && Left < rect.Right && Bottom > rect.Top && Top < rect.Bottom ); 
		}

		/// <summary>Returns a value indicating whether this <see cref="Rect"/> intersects another <see cref="Rect"/>.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <returns>Returns a value indicating whether the rectangles intersect.</returns>
		public bool Intersects( Rect rect )
		{
			return ( Right > rect.X && Left < rect.Right && Bottom > rect.Top && Top < rect.Bottom );
		}

		#endregion Intersects


		/// <summary>Offsets the rectangle by the specified amounts.</summary>
		/// <param name="horizontalAmount">The horizontal amount.</param>
		/// <param name="verticalAmount">The vertical amount.</param>
		public void Offset( int horizontalAmount, int verticalAmount )
		{
			Left += horizontalAmount;
			Top += verticalAmount;
			Right += horizontalAmount;
			Bottom += verticalAmount;
		}

		/// <summary>Offsets the rectangle by the specified amount.</summary>
		/// <param name="amount">A <see cref="Point"/> indicating the horizontal and vertical amounts.</param>
		public void Offset( Point amount )
		{
			Left += amount.X;
			Top += amount.Y;
			Right += amount.X;
			Bottom += amount.Y;
		}

		/// <summary>Offsets the rectangle by the specified amount.</summary>
		/// <param name="amount">A <see cref="Size"/> indicating the horizontal and vertical amounts.</param>
		public void Offset( Size amount )
		{
			Left += amount.Width;
			Top += amount.Height;
			Right += amount.Width;
			Bottom += amount.Height;
		}


		/// <summary>Pushes the edges of the rectangle by the specified amounts.</summary>
		/// <param name="horizontalAmount">The horizontal amount.</param>
		/// <param name="verticalAmount">The vertical amount.</param>
		public void Inflate( int horizontalAmount, int verticalAmount )
		{
			Left -= horizontalAmount;
			Top -= verticalAmount;
			Right += horizontalAmount;
			Bottom += verticalAmount;
		}

		/// <summary>Pushes the edges of the rectangle by the specified amount.</summary>
		/// <param name="amount">A <see cref="Size"/> indicating the horizontal and vertical amounts.</param>
		public void Inflate( Size amount )
		{
			Left -= amount.Width;
			Top -= amount.Height;
			Right += amount.Width;
			Bottom += amount.Height;
		}

		/// <summary>Pushes the edges of the rectangle by the specified amount.</summary>
		/// <param name="amount">A <see cref="Rect"/> indicating the left, top, right and bottom amounts.</param>
		public void Inflate( Rect amount )
		{
			Left -= amount.Left;
			Top -= amount.Top;
			Right += amount.Right;
			Bottom += amount.Bottom;
		}


		/// <summary>Returns a hash code for this <see cref="Rect"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Rect"/>.</returns>
		public override int GetHashCode()
		{
			return Left ^ Top ^ Right ^ Bottom;
		}


		/// <summary>Returns a value indicating whether this <see cref="Rect"/> equals another <see cref="Rect"/>.</summary>
		/// <param name="other">A <see cref="Rect"/>.</param>
		/// <returns>Returns true if this <see cref="Rect"/> and the <paramref name="other"/> <see cref="Rect"/> are equal, otherwise returns false.</returns>
		public bool Equals( Rect other )
		{
			return ( Left == other.Left ) && ( Top == other.Top ) && ( Right == other.Right ) && ( Bottom == other.Bottom );
		}


		/// <summary>Returns a value indicating whether this <see cref="Rect"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Rect"/> which equals this <see cref="Rect"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is Rect r && this.Equals( r );
		}


		/// <summary>Returns a string representing this <see cref="Rect"/>.</summary>
		/// <returns>Returns a string representing this <see cref="Rect"/>.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Left: {0}, Top: {1}, Right: {2}, Bottom: {3}}}", Left, Top, Right, Bottom );
		}


		/// <summary>The «zero» <see cref="Rect"/>.</summary>
		public static readonly Rect Zero;


		#region Static methods

		/// <summary>Negates a <see cref="Rect"/>.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="result">Receives the negated <paramref name="rect"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Negate( ref Rect rect, out Rect result )
		{
			result.Left = -rect.Right;
			result.Top = -rect.Bottom;
			result.Right = -rect.Left;
			result.Bottom = -rect.Top;
		}

		/// <summary>Negates a <see cref="Rect"/>.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <returns>Returns the negated <paramref name="rect"/>.</returns>
		public static Rect Negate( Rect rect )
		{
			return new Rect( -rect.Right, -rect.Bottom, -rect.Left, -rect.Top );
		}


		/// <summary>Adds two rectangles.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="other">A <see cref="Rect"/>.</param>
		/// <param name="result">Receives the sum of the two specified rectangles.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref Rect rect, ref Rect other, out Rect result )
		{
			result.Left = rect.Left + other.Left;
			result.Top = rect.Top + other.Top;
			result.Right = rect.Right + other.Right;
			result.Bottom = rect.Bottom + other.Bottom;
		}

		/// <summary>Adds two rectangles.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="other">A <see cref="Rect"/>.</param>
		/// <returns>Returns the sum of the two specified rectangles.</returns>
		public static Rect Add( Rect rect, Rect other )
		{
			rect.Left += other.Left;
			rect.Top += other.Top;
			rect.Right += other.Right;
			rect.Bottom += other.Bottom;
			return rect;
		}


		/// <summary>Subtracts two rectangles.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="other">A <see cref="Rect"/>.</param>
		/// <param name="result">Receives the difference between the two specified rectangles.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref Rect rect, ref Rect other, out Rect result )
		{
			result.Left = rect.Left - other.Left;
			result.Top = rect.Top - other.Top;
			result.Right = rect.Right - other.Right;
			result.Bottom = rect.Bottom - other.Bottom;
		}

		/// <summary>Subtracts two rectangles.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="other">A <see cref="Rect"/>.</param>
		/// <returns>Returns the difference between the two specified rectangles.</returns>
		public static Rect Subtract( Rect rect, Rect other )
		{
			rect.Left -= other.Left;
			rect.Top -= other.Top;
			rect.Right -= other.Right;
			rect.Bottom -= other.Bottom;
			return rect;
		}


		/// <summary>Creates a <see cref="Rect"/> structure initialized with the minimum values from two <see cref="Rect"/> structures.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <param name="result">Receives a <see cref="Rect"/> structure initialized with the minimum values from the two specified <see cref="Rect"/> structures.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Min( ref Rect rect, ref Rect other, out Rect result )
		{
			if( other.Left < rect.Left )
				result.Left = other.Left;
			else
				result.Left = rect.Left;

			if( other.Top < rect.Top )
				result.Top = other.Top;
			else
				result.Top = rect.Top;

			if( other.Right < rect.Right )
				result.Right = other.Right;
			else
				result.Right = rect.Right;

			if( other.Bottom < rect.Bottom )
				result.Bottom = other.Bottom;
			else
				result.Bottom = rect.Bottom;
		}

		/// <summary>Returns a <see cref="Rect"/> structure initialized with the minimum values from two <see cref="Rect"/> structures.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns a <see cref="Rect"/> structure initialized with the minimum values from two <see cref="Rect"/> structures.</returns>
		public static Rect Min( Rect rect, Rect other )
		{
			if( other.Left < rect.Left )
				rect.Left = other.Left;
			
			if( other.Top < rect.Top )
				rect.Top = other.Top;

			if( other.Right < rect.Right )
				rect.Right = other.Right;

			if( other.Bottom < rect.Bottom )
				rect.Bottom = other.Bottom;

			return rect;
		}


		/// <summary>Creates a <see cref="Rect"/> structure initialized with the maximum values from two <see cref="Rect"/> structures.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <param name="result">Receives a <see cref="Rect"/> structure initialized with the maximum values from the two specified <see cref="Rect"/> structures.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Max( ref Rect rect, ref Rect other, out Rect result )
		{
			if( other.Left > rect.Left )
				result.Left = other.Left;
			else
				result.Left = rect.Left;

			if( other.Top > rect.Top )
				result.Top = other.Top;
			else
				result.Top = rect.Top;

			if( other.Right > rect.Right )
				result.Right = other.Right;
			else
				result.Right = rect.Right;

			if( other.Bottom > rect.Bottom )
				result.Bottom = other.Bottom;
			else
				result.Bottom = rect.Bottom;
		}

		/// <summary>Returns a <see cref="Rect"/> structure initialized with the maximum values from two <see cref="Rect"/> structures.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns a <see cref="Rect"/> structure initialized with the maximum values from two <see cref="Rect"/> structures.</returns>
		public static Rect Max( Rect rect, Rect other )
		{
			if( other.Left > rect.Left )
				rect.Left = other.Left;

			if( other.Top > rect.Top )
				rect.Top = other.Top;

			if( other.Right > rect.Right )
				rect.Right = other.Right;

			if( other.Bottom > rect.Bottom )
				rect.Bottom = other.Bottom;

			return rect;
		}


		/// <summary>Creates a rectangle containing the two specified rectangles.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="other">A <see cref="Rect"/>.</param>
		/// <param name="result">Receives the rectangle containing the two specified rectangles.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Union( ref Rect rect, ref Rect other, out Rect result )
		{
			result.Left = Math.Min( rect.Left, other.Left );
			result.Top = Math.Min( rect.Top, other.Top );
			result.Right = Math.Max( rect.Right, other.Right );
			result.Bottom = Math.Max( rect.Bottom, other.Bottom );
		}

		/// <summary>Returns a rectangle containing the two specified rectangles.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="other">A <see cref="Rect"/>.</param>
		/// <returns>Returns a rectangle containing the two specified rectangles.</returns>
		public static Rect Union( Rect rect, Rect other )
		{
			rect.Left = Math.Min( rect.Left, other.Left );
			rect.Top = Math.Min( rect.Top, other.Top );
			rect.Right = Math.Max( rect.Right, other.Right );
			rect.Bottom = Math.Max( rect.Bottom, other.Bottom );
			return rect;
		}


		/// <summary>Creates a <see cref="Rect"/> defining the area where one rectangle overlaps with another rectangle.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="other">A <see cref="Rect"/>.</param>
		/// <param name="result">Receives the area where the two rectangles overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Intersect( ref Rect rect, ref Rect other, out Rect result )
		{
			var left = Math.Max( rect.Left, other.Left );
			var top = Math.Max( rect.Top, other.Top );
			var right = Math.Min( rect.Right, other.Right );
			var bottom = Math.Min( rect.Bottom, other.Bottom );
			
			if( right > left && bottom > top )
			{
				result.Left = left;
				result.Top = top;
				result.Right = right;
				result.Bottom = bottom;
				return;
			}

			result = Rect.Zero;
		}

		/// <summary>Returns a <see cref="Rect"/> defining the area where one rectangle overlaps with another rectangle.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="other">A <see cref="Rect"/>.</param>
		/// <returns>Returns the area where the two rectangles overlap.</returns>
		public static Rect Intersect( Rect rect, Rect other )
		{
			var left = Math.Max( rect.Left, other.Left );
			var top = Math.Max( rect.Top, other.Top );
			var right = Math.Min( rect.Right, other.Right );
			var bottom = Math.Min( rect.Bottom, other.Bottom );

			if( right > left && bottom > top )
			{
				rect.Left = left;
				rect.Top = top;
				rect.Right = right;
				rect.Bottom = bottom;
				return rect;
			}

			return Rect.Zero;
		}

		#endregion Static methods


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="other">A <see cref="Rect"/>.</param>
		/// <returns>Returns true if the rectangles are equal, otherwise returns false.</returns>
		public static bool operator ==( Rect rect, Rect other )
		{
			return ( rect.Left == other.Left ) && ( rect.Top == other.Top ) && ( rect.Right == other.Right ) && ( rect.Bottom == other.Bottom );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <param name="other">A <see cref="Rect"/>.</param>
		/// <returns>Returns true if the rectangles are not equal, otherwise returns false.</returns>
		public static bool operator !=( Rect rect, Rect other )
		{
			return ( rect.Left != other.Left ) || ( rect.Top != other.Top ) || ( rect.Right != other.Right ) || ( rect.Bottom != other.Bottom );
		}


		/// <summary>Negation operator.</summary>
		/// <param name="rect">A <see cref="Rect"/>.</param>
		/// <returns>Returns the negated <paramref name="rect"/>.</returns>
		public static Rect operator -( Rect rect )
		{
			var x = rect.Left;
			var y = rect.Top;
			rect.Left = -rect.Right;
			rect.Top = -rect.Bottom;
			rect.Right = -x;
			rect.Bottom = -y;
			return rect;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns the sum of the two specified rectangles.</returns>
		public static Rect operator +( Rect rect, Rect other )
		{
			rect.Left += other.Left;
			rect.Top += other.Top;
			rect.Right += other.Right;
			rect.Bottom += other.Bottom;
			return rect;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns the difference of the two specified rectangles.</returns>
		public static Rect operator -( Rect rect, Rect other )
		{
			rect.Left -= other.Left;
			rect.Top -= other.Top;
			rect.Right -= other.Right;
			rect.Bottom -= other.Bottom;
			return rect;
		}

		#endregion Operators

	}

}