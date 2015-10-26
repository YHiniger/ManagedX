using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	
	/// <summary>Represents a point in 2D space.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Point : IEquatable<Point>
	{

		/// <summary>The X component of the point.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Justification = "Seriously?" )]
		public int X;
		
		/// <summary>The Y component of the point.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Justification = "Seriously?" )]
		public int Y;



		/// <summary>Initializes a new <see cref="Point"/> structure.</summary>
		/// <param name="x">The X component of the point.</param>
		/// <param name="y">The Y component of the point.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Seriously?" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Seriously?" )]
		public Point( int x, int y )
		{
			this.X = x;
			this.Y = y;
		}


		
		/// <summary>Offsets this <see cref="Point"/> by the specified <see cref="Point"/>.</summary>
		/// <param name="value">A <see cref="Point"/> structure.</param>
		public void Offset( Point value )
		{
			X += value.X;
			Y += value.Y;
		}


		/// <summary>Returns a hash code for this <see cref="Point"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Point"/> structure.</returns>
		public override int GetHashCode()
		{
			return X ^ Y;
		}


		/// <summary>Returns a value indicating whether this <see cref="Point"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Point other )
		{
			return
				( X == other.X ) &&
				( Y == other.Y );
		}


		/// <summary>Returns a value indicating whether this <see cref="Point"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Point"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Point ) && this.Equals( (Point)obj );
		}


		/// <summary>Returns a string representing this <see cref="Point"/> structure, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>)</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="Point"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "({0},{1})", X, Y );
		}


		#region Static

		/// <summary>The zero (or empty) <see cref="Point"/> structure.</summary>
		public static readonly Point Zero;


		/// <summary>Negates a <see cref="Point"/>.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="result">Receives the negated point.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		public static void Negate( ref Point point, out Point result )
		{
			result.X = -point.X;
			result.Y = -point.Y;
		}

		/// <summary>Returns a <see cref="Point"/> initialized with coordinates opposite to the specified <see cref="Point"/>.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <returns>Returns a <see cref="Point"/> initialized with coordinates opposite to the specified <see cref="Point"/>.</returns>
		public static Point Negate( Point point )
		{
			point.X = -point.X;
			point.Y = -point.Y;
			return point;
		}



		/// <summary>Adds two <see cref="Point"/> values.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <param name="result">Receives the sum of the two specified points.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Add( ref Point point, ref Point other, out Point result )
		{
			result.X = point.X + other.X;
			result.Y = point.Y + other.Y;
		}

		/// <summary>Adds two <see cref="Point"/> values.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="point"/> + <paramref name="other"/> ).</returns>
		public static Point Add( Point point, Point other )
		{
			point.X += other.X;
			point.Y += other.Y;
			return point;
		}


		/// <summary>Calculates the difference between two points.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <param name="result">Receives the difference between <paramref name="point"/> and <paramref name="other"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Subtract( ref Point point, ref Point other, out Point result )
		{
			result.X = point.X - other.X;
			result.Y = point.Y - other.Y;
		}

		/// <summary>Subtracts a <see cref="Point"/> value (<paramref name="other"/>) from another <see cref="Point"/> (<paramref name="point"/>).</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="point"/> - <paramref name="other"/> ).</returns>
		public static Point Subtract( Point point, Point other )
		{
			point.X -= other.X;
			point.Y -= other.Y;
			return point;
		}

		#endregion // Static


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Point point, Point other )
		{
			return point.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Point point, Point other )
		{
			return !point.Equals( other );
		}


		/// <summary>Inferiority comparer.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <( Point point, Point other )
		{
			return ( point.X < other.X ) && ( point.Y < other.Y );
		}


		/// <summary>Inferiority or equality comparer.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <=( Point point, Point other )
		{
			return ( point.X <= other.X ) && ( point.Y <= other.Y );
		}


		/// <summary>Superiority comparer.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >( Point point, Point other )
		{
			return ( point.X > other.X ) && ( point.Y > other.Y );
		}


		/// <summary>Superiority or equality comparer.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >=( Point point, Point other )
		{
			return ( point.X >= other.X ) && ( point.Y >= other.Y );
		}

	
		/// <summary>Negation operator.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <returns>Returns a <see cref="Point"/> structure initialized with values opposite to the specified <paramref name="point"/>.</returns>
		public static Point operator -( Point point )
		{
			point.X = -point.X;
			point.Y = -point.Y;
			return point;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="point"/> + <paramref name="other"/> ).</returns>
		public static Point operator +( Point point, Point other )
		{
			point.X += other.X;
			point.Y += other.Y;
			return point;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="point"/> - <paramref name="other"/> ).</returns>
		public static Point operator -( Point point, Point other )
		{
			point.X -= other.X;
			point.Y -= other.Y;
			return point;
		}

		#endregion // Operators

	}

}