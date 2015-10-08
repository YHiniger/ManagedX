using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// Comments checked (2015/09/23)


namespace ManagedX
{
	
	/// <summary>A pair of integer coordinates.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Point : IEquatable<Point>
	{

		private int x;
		private int y;


		/// <summary>Initializes a new <see cref="Point"/> structure.</summary>
		/// <param name="x">The X component of the point.</param>
		/// <param name="y">The Y component of the point.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y" )]
		public Point( int x, int y )
		{
			if( x < 0 )
				throw new ArgumentOutOfRangeException( "x" );
			if( y < 0 )
				throw new ArgumentOutOfRangeException( "y" );
			
			this.x = x;
			this.y = y;
		}


		/// <summary>Gets the horizontal position of the point.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		public int X { get { return x; } }

		/// <summary>Gets the vertical position of the point.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		public int Y { get { return y; } }


		/// <summary>Returns a hash code for this <see cref="Point"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Point"/> structure.</returns>
		public override int GetHashCode()
		{
			return x ^ y;
		}


		/// <summary>Returns a value indicating whether this <see cref="Point"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Point"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Point other )
		{
			return ( x == other.x ) && ( y == other.y );
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
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "({0},{1})", x, y );
		}
		


		/// <summary>The zero (or empty) <see cref="Point"/> structure.</summary>
		public static readonly Point Zero = new Point();


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

		#endregion

	}

}