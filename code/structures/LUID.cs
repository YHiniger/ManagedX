using System;
using System.Globalization;
using System.Runtime.InteropServices;

// Comments checked (2015/09/23)


namespace ManagedX
{

	/// <summary>A locally unique identifier (LUID).</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Luid : IEquatable<Luid>, IComparable<Luid>
	{

		private uint lowPart;
		private int highPart;



		/// <summary>Converts this <see cref="Luid"/> structure to an <see cref="Int64"/> (<seealso cref="long"/>) value.</summary>
		/// <returns>Returns an <see cref="Int64"/> (<seealso cref="long"/>) value representing this <see cref="Luid"/> structure.</returns>
		public long ToInt64()
		{
			return (long)lowPart | ( ( (long)highPart ) << 32 );
		}


		/// <summary>Returns a hash code for this this <see cref="Luid"/> structure.</summary>
		/// <returns>Returns a hash code for this this <see cref="Luid"/> structure.</returns>
		public override int GetHashCode()
		{
			return lowPart.GetHashCode() ^ highPart;
		}


		/// <summary>Returns a value indicating whether this <see cref="Luid"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Luid"/> structure.</param>
		/// <returns>Returns true if this <see cref="Luid"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( Luid other )
		{
			return ( lowPart == other.lowPart ) && ( highPart == other.highPart );
		}


		/// <summary>Returns a value indicating whether this <see cref="Luid"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Luid"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Luid ) && this.Equals( (Luid)obj );
		}


		/// <summary>Returns a value indicating whether this <see cref="Luid"/> structure is equal(0), smaller(-1) or greater(+1) than another structure of the same type.</summary>
		/// <param name="other">A <see cref="Luid"/> structure.</param>
		/// <returns>Returns -1 if this <see cref="Luid"/> structure is smaller than the <paramref name="other"/> structure, +1 if it's greater or 0 if they're equal.</returns>
		public int CompareTo( Luid other )
		{
			if( highPart < other.highPart )
				return -1;

			if( highPart > other.highPart )
				return +1;

			return lowPart.CompareTo( other.lowPart );
		}


		/// <summary>Returns a string representing this <see cref="Luid"/> structure in a hexadecimal form.</summary>
		/// <returns>Returns a string representing this <see cref="Luid"/> structure in a hexadecimal form.</returns>
		public override string ToString()
		{
			return string.Format( CultureInfo.InvariantCulture, "0x{0:X16}", this.ToInt64() );
		}


		/// <summary>The empty <see cref="Luid"/> structure.</summary>
		public static readonly Luid Empty = new Luid();


		#region Operators

		/// <summary>Implicit conversion operator.</summary>
		/// <param name="luid">A <see cref="Luid"/> structure.</param>
		/// <returns>Returns a <see cref="long"/> representing the <paramref name="luid"/>.</returns>
		public static implicit operator long( Luid luid )
		{
			return luid.ToInt64();
		}


		/// <summary>Equality comparer.</summary>
		/// <param name="luid">A <see cref="Luid"/> structure.</param>
		/// <param name="other">A <see cref="Luid"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Luid luid, Luid other )
		{
			return luid.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="luid">A <see cref="Luid"/> structure.</param>
		/// <param name="other">A <see cref="Luid"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Luid luid, Luid other )
		{
			return !luid.Equals( other );
		}


		/// <summary>Inferiority comparer.</summary>
		/// <param name="luid">A <see cref="Luid"/> structure.</param>
		/// <param name="other">A <see cref="Luid"/> structure.</param>
		/// <returns>Returns true if the <paramref name="luid"/> is lower than the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator <( Luid luid, Luid other )
		{
			return luid.CompareTo( other ) < 0;
		}


		/// <summary>Inferiority or equality comparer.</summary>
		/// <param name="luid">A <see cref="Luid"/> structure.</param>
		/// <param name="other">A <see cref="Luid"/> structure.</param>
		/// <returns>Returns true if the <paramref name="luid"/> is lower than or equal to the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator <=( Luid luid, Luid other )
		{
			return luid.CompareTo( other ) <= 0;
		}


		/// <summary>Superiority comparer.</summary>
		/// <param name="luid">A <see cref="Luid"/> structure.</param>
		/// <param name="other">A <see cref="Luid"/> structure.</param>
		/// <returns>Returns true if the <paramref name="luid"/> is greater than the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator >( Luid luid, Luid other )
		{
			return luid.CompareTo( other ) > 0;
		}


		/// <summary>Superiority or equality comparer.</summary>
		/// <param name="luid">A <see cref="Luid"/> structure.</param>
		/// <param name="other">A <see cref="Luid"/> structure.</param>
		/// <returns>Returns true if the <paramref name="luid"/> is greater than or equal to the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator >=( Luid luid, Luid other )
		{
			return luid.CompareTo( other ) >= 0;
		}

		#endregion

	}

}