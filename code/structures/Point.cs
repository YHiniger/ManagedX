using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	
	/// <summary>Represents a point in 2D space.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[Serializable]
	[ComVisible( true )]
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
		/// <exception cref="ArgumentOutOfRangeException"/>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Seriously?" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Seriously?" )]
		public Point( int x, int y )
		{
			if( x < 0 )
				throw new ArgumentOutOfRangeException( "x" );
			if( y < 0 )
				throw new ArgumentOutOfRangeException( "y" );
			
			this.X = x;
			this.Y = y;
		}

		
		/// <summary>Offsets this <see cref="Point"/> by the specified <see cref="Point"/>.</summary>
		/// <param name="value">A <see cref="Point"/> structure.</param>
		public void Offset( Point value )
		{
			this.X += value.X;
			this.Y += value.Y;
		}


		/// <summary>Returns a hash code for this <see cref="Point"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Point"/> structure.</returns>
		public override int GetHashCode()
		{
			return this.X ^ this.Y;
		}


		/// <summary>Returns a value indicating whether this <see cref="Point"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Point other )
		{
			return ( this.X == other.X ) && ( this.Y == other.Y );
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
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "({0},{1})", this.X, this.Y );
		}


		/// <summary>The zero (or empty) <see cref="Point"/> structure.</summary>
		public static readonly Point Zero = new Point();


		/// <summary></summary>
		/// <param name="point"></param>
		/// <returns></returns>
		[ComVisible( false )]
		public static Point Negate( Point point )
		{
			return -point;
		}


		/// <summary></summary>
		/// <param name="point"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		[ComVisible( false )]
		public static Point Add( Point point, Point other )
		{
			point.Offset( other );
			return point;
		}


		/// <summary></summary>
		/// <param name="point"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		[ComVisible( false )]
		public static Point Subtract( Point point, Point other )
		{
			point.Offset( -other );
			return point;
		}


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
			point.Offset( other );
			return point;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="point"/> - <paramref name="other"/> ).</returns>
		public static Point operator -( Point point, Point other )
		{
			point.Offset( -other );
			return point;
		}

		#endregion

	}

}