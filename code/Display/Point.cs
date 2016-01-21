using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX // .Graphics
{

	/// <summary>Defines the (integer) coordinates of a point in 2D space.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Point : IEquatable<Point>
	{

		/// <summary>The X component of the point.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public int X;
		
		/// <summary>The Y component of the point.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public int Y;



		/// <summary>Initializes a new <see cref="Point"/>.</summary>
		/// <param name="x">The X component of the point.</param>
		/// <param name="y">The Y component of the point.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public Point( int x, int y )
		{
			this.X = x;
			this.Y = y;
		}



		/// <summary>Gets a value indicating whether the components of this <see cref="Point"/> are set to zero.</summary>
		public bool IsZero { get { return ( X == 0 ) && ( Y == 0 ); } }

		
		/// <summary>Offsets this <see cref="Point"/> by the specified <see cref="Point"/>.</summary>
		/// <param name="value">A <see cref="Point"/>.</param>
		public void Offset( Point value )
		{
			X += value.X;
			Y += value.Y;
		}

		/// <summary>Offsets this <see cref="Point"/> by the specified <see cref="Size"/>.</summary>
		/// <param name="value">A <see cref="Size"/>.</param>
		public void Offset( Size value )
		{
			X += value.Width;
			Y += value.Height;
		}


		/// <summary>Returns a hash code for this <see cref="Point"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Point"/>.</returns>
		public override int GetHashCode()
		{
			return X ^ Y;
		}


		/// <summary>Returns a value indicating whether this <see cref="Point"/> equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns>Returns true if the points are equal, otherwise returns false.</returns>
		public bool Equals( Point other )
		{
			return ( X == other.X ) && ( Y == other.Y );
		}


		/// <summary>Returns a value indicating whether this <see cref="Point"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Point"/> which equals this <see cref="Point"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Point ) && this.Equals( (Point)obj );
		}


		/// <summary>Returns a string representing this <see cref="Point"/>, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>)</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="Point"/>.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "({0},{1})", X, Y );
		}


		/// <summary>The «zero» (or empty) <see cref="Point"/>.</summary>
		public static readonly Point Zero;


		#region Static methods

		/// <summary>Negates a <see cref="Point"/>.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="result">Receives the negated point.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Negate( ref Point point, out Point result )
		{
			result.X = -point.X;
			result.Y = -point.Y;
		}

		/// <summary>Returns a <see cref="Point"/> initialized with coordinates opposite to the specified <see cref="Point"/>.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <returns>Returns a <see cref="Point"/> initialized with coordinates opposite to the specified <see cref="Point"/>.</returns>
		public static Point Negate( Point point )
		{
			point.X = -point.X;
			point.Y = -point.Y;
			return point;
		}



		/// <summary>Adds two <see cref="Point"/> values.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <param name="result">Receives the sum of the two specified points.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref Point point, ref Point other, out Point result )
		{
			result.X = point.X + other.X;
			result.Y = point.Y + other.Y;
		}

		/// <summary>Adds two <see cref="Point"/> values.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns>Returns the result of ( <paramref name="point"/> + <paramref name="other"/> ).</returns>
		public static Point Add( Point point, Point other )
		{
			point.X += other.X;
			point.Y += other.Y;
			return point;
		}


		/// <summary>Calculates the difference between two points.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <param name="result">Receives the difference between <paramref name="point"/> and <paramref name="other"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref Point point, ref Point other, out Point result )
		{
			result.X = point.X - other.X;
			result.Y = point.Y - other.Y;
		}

		/// <summary>Subtracts a <see cref="Point"/> value (<paramref name="other"/>) from another <see cref="Point"/> (<paramref name="point"/>).</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns>Returns the result of ( <paramref name="point"/> - <paramref name="other"/> ).</returns>
		public static Point Subtract( Point point, Point other )
		{
			point.X -= other.X;
			point.Y -= other.Y;
			return point;
		}


		/// <summary>Multiplies two <see cref="Point"/> values.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <param name="result">Receives the product of the two specified points.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Multiply( ref Point point, ref Point other, out Point result )
		{
			result.X = point.X * other.X;
			result.Y = point.Y * other.Y;
		}

		/// <summary>Multiplies two <see cref="Point"/> values.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns>Returns the product of the two specified points.</returns>
		public static Point Multiply( Point point, Point other )
		{
			point.X *= other.X;
			point.Y *= other.Y;
			return point;
		}


		/// <summary>Divides two <see cref="Point"/> values.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <param name="result">Receives the result of the division.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( ref Point point, ref Point other, out Point result )
		{
			result.X = point.X / other.X;
			result.Y = point.Y / other.Y;
		}

		/// <summary>Divides two <see cref="Point"/> values.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns>Returns the result of the division.</returns>
		public static Point Divide( Point point, Point other )
		{
			point.X /= other.X;
			point.Y /= other.Y;
			return point;
		}


		/// <summary>Retrieves the minimum values between two points.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Min( ref Point point, ref Point other, out Point result )
		{
			if( point.X < other.X )
				result.X = point.X;
			else
				result.X = other.X;

			if( point.Y < other.Y )
				result.Y = point.Y;
			else
				result.Y = other.Y;
		}

		/// <summary>Returns a <see cref="Point"/> initialized with the minimum values between two points.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns></returns>
		public static Point Min( Point point, Point other )
		{
			if( point.X > other.X )
				point.X = other.X;

			if( point.Y > other.Y )
				point.Y = other.Y;

			return point;
		}


		/// <summary>Retrieves the maximum values between two points.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Max( ref Point point, ref Point other, out Point result )
		{
			if( point.X > other.X )
				result.X = point.X;
			else
				result.X = other.X;

			if( point.Y > other.Y )
				result.Y = point.Y;
			else
				result.Y = other.Y;
		}

		/// <summary>Returns a <see cref="Point"/> initialized with the maximum values between two points.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns></returns>
		public static Point Max( Point point, Point other )
		{
			if( point.X < other.X )
				point.X = other.X;

			if( point.Y < other.Y )
				point.Y = other.Y;

			return point;
		}

		#endregion Static methods


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Point point, Point other )
		{
			return point.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Point point, Point other )
		{
			return !point.Equals( other );
		}


		/// <summary>Negation operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <returns>Returns a <see cref="Point"/> structure initialized with values opposite to the specified <paramref name="point"/>.</returns>
		public static Point operator -( Point point )
		{
			point.X = -point.X;
			point.Y = -point.Y;
			return point;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns>Returns the result of ( <paramref name="point"/> + <paramref name="other"/> ).</returns>
		public static Point operator +( Point point, Point other )
		{
			point.X += other.X;
			point.Y += other.Y;
			return point;
		}

		/// <summary>Addition operator.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="value">An integer value.</param>
		/// <returns>Returns the result of the addition.</returns>
		public static Point operator +( Point point, int value )
		{
			point.X += value;
			point.Y += value;
			return point;
		}

		/// <summary>Addition operator.</summary>
		/// <param name="value">An integer value.</param>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <returns>Returns the result of the addition.</returns>
		public static Point operator +( int value, Point point )
		{
			point.X += value;
			point.Y += value;
			return point;
		}

		/// <summary>Addition operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="size">A <see cref="Size"/>.</param>
		/// <returns>Returns the result of the addition.</returns>
		public static Point operator +( Point point, Size size )
		{
			point.X += size.Width;
			point.Y += size.Height;
			return point;
		}

	
		/// <summary>Subtraction operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns>Returns the result of ( <paramref name="point"/> - <paramref name="other"/> ).</returns>
		public static Point operator -( Point point, Point other )
		{
			point.X -= other.X;
			point.Y -= other.Y;
			return point;
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="value">An integer value.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static Point operator -( Point point, int value )
		{
			point.X -= value;
			point.Y -= value;
			return point;
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="value">An integer value.</param>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static Point operator -( int value, Point point)
		{
			point.X = value - point.X;
			point.Y = value - point.Y;
			return point;
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="size">A <see cref="Size"/>.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static Point operator -( Point point, Size size )
		{
			point.X -= size.Width;
			point.Y -= size.Height;
			return point;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns>Returns the product of the two specified points.</returns>
		public static Point operator *( Point point, Point other )
		{
			point.X *= other.X;
			point.Y *= other.Y;
			return point;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="value">An integer value.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static Point operator *( Point point, int value )
		{
			point.X *= value;
			point.Y *= value;
			return point;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">An integer value.</param>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static Point operator *( int value, Point point )
		{
			point.X *= value;
			point.Y *= value;
			return point;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="size">A <see cref="Size"/>.</param>
		/// <returns>Returns the product of <paramref name="point"/> by <paramref name="size"/>.</returns>
		public static Point operator *( Point point, Size size )
		{
			point.X *= size.Width;
			point.Y *= size.Height;
			return point;
		}


		/// <summary>Division operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="other">A <see cref="Point"/>.</param>
		/// <returns>Returns the result of the division.</returns>
		public static Point operator /( Point point, Point other )
		{
			point.X /= other.X;
			point.Y /= other.Y;
			return point;
		}

		/// <summary>Division operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="value">An integer value.</param>
		/// <returns>Returns the result of the division.</returns>
		public static Point operator /( Point point, int value )
		{
			point.X /= value;
			point.Y /= value;
			return point;
		}

		/// <summary>Division operator.</summary>
		/// <param name="value">An integer value.</param>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <returns>Returns the result of the division.</returns>
		public static Point operator /( int value, Point point )
		{
			point.X = value / point.X;
			point.Y = value / point.Y;
			return point;
		}

		/// <summary>Division operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <param name="size">A <see cref="Size"/>.</param>
		/// <returns>Returns the quotient of <paramref name="point"/> over <paramref name="size"/>.</returns>
		public static Point operator /( Point point, Size size )
		{
			point.X /= size.Width;
			point.Y /= size.Height;
			return point;
		}


		/// <summary><see cref="Point"/> to <see cref="Size"/> conversion operator.</summary>
		/// <param name="point">A <see cref="Point"/>.</param>
		/// <returns>Returns a <see cref="Size"/> initialized with the specified <paramref name="point"/>.</returns>
		public static explicit operator Size( Point point )
		{
			return new Size( point.X, point.Y );
		}

		/// <summary><see cref="Size"/> to <see cref="Point"/> conversion operator.</summary>
		/// <param name="size">A <see cref="Size"/>.</param>
		/// <returns>Returns a <see cref="Point"/> initialized from the specified <paramref name="size"/>.</returns>
		public static explicit operator Point( Size size )
		{
			return new Point( size.Width, size.Height );
		}

		#endregion Operators

	}

}