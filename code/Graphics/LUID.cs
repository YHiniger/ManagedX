using System;
using System.Globalization;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{

    /// <summary>A locally unique identifier (LUID).</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Luid")]
    [System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Luid : IEquatable<Luid>, IComparable<Luid>
	{

		private uint lowPart;
		private int highPart;



		/// <summary>Returns an <see cref="Int64"/> (or <see cref="long"/>) representing this <see cref="Luid"/>.</summary>
		/// <returns>Returns an <see cref="Int64"/> (or <see cref="long"/>) representing this <see cref="Luid"/>.</returns>
		public long ToInt64()
		{
			return (long)lowPart | ( ( (long)highPart ) << 32 );
		}


		/// <summary>Returns a hash code for this <see cref="Luid"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Luid"/>.</returns>
		public override int GetHashCode()
		{
			return lowPart.GetHashCode() ^ highPart;
		}


		/// <summary>Returns a value indicating whether this <see cref="Luid"/> equals another <see cref="Luid"/>.</summary>
		/// <param name="other">An <see cref="Luid"/>.</param>
		/// <returns>Returns true if this <see cref="Luid"/> and the <paramref name="other"/> <see cref="Luid"/> are equal, otherwise returns false.</returns>
		public bool Equals( Luid other )
		{
			return ( lowPart == other.lowPart ) && ( highPart == other.highPart );
		}


		/// <summary>Returns a value indicating whether this <see cref="Luid"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Luid"/> which equals this <see cref="Luid"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Luid ) && this.Equals( (Luid)obj );
		}


		/// <summary>Returns a value indicating whether this <see cref="Luid"/> is equal(0), smaller(-1) or greater(+1) than another <see cref="Luid"/>.</summary>
		/// <param name="other">A <see cref="Luid"/>.</param>
		/// <returns>Returns -1 if this <see cref="Luid"/> is smaller than the <paramref name="other"/> <see cref="Luid"/>, +1 if it's greater or 0 if they're equal.</returns>
		public int CompareTo( Luid other )
		{
			if( highPart < other.highPart )
				return -1;

			if( highPart > other.highPart )
				return +1;

			return lowPart.CompareTo( other.lowPart );
		}


		/// <summary>Returns a string representing this <see cref="Luid"/> in a hexadecimal form.</summary>
		/// <returns>Returns a string representing this <see cref="Luid"/> in a hexadecimal form.</returns>
		public override string ToString()
		{
			return string.Format( CultureInfo.InvariantCulture, "0x{0:X16}", this.ToInt64() );
		}


		/// <summary>The «zero» <see cref="Luid"/>.</summary>
		public static readonly Luid Zero = new Luid();


        #region Operators

        /// <summary>Implicit conversion operator.</summary>
        /// <param name="luid">A <see cref="Luid"/>.</param>
        /// <returns>Returns a <see cref="long"/> representing the <paramref name="luid"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "luid")]
        public static implicit operator long( Luid luid )
		{
			return luid.ToInt64();
		}


        /// <summary>Equality comparer.</summary>
        /// <param name="luid">A <see cref="Luid"/>.</param>
        /// <param name="other">A <see cref="Luid"/>.</param>
        /// <returns>Returns true if the <see cref="Luid"/> values are equal, otherwise returns false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "luid")]
        public static bool operator ==( Luid luid, Luid other )
		{
			return luid.Equals( other );
		}


        /// <summary>Inequality comparer.</summary>
        /// <param name="luid">A <see cref="Luid"/>.</param>
        /// <param name="other">A <see cref="Luid"/>.</param>
        /// <returns>Returns true if the <see cref="Luid"/> values are not equal, otherwise returns false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "luid")]
        public static bool operator !=( Luid luid, Luid other )
		{
			return !luid.Equals( other );
		}


        /// <summary>Inferiority comparer.</summary>
        /// <param name="luid">A <see cref="Luid"/>.</param>
        /// <param name="other">A <see cref="Luid"/>.</param>
        /// <returns>Returns true if the <paramref name="luid"/> is lower than the <paramref name="other"/> <see cref="Luid"/>, otherwise returns false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "luid")]
        public static bool operator <( Luid luid, Luid other )
		{
			return luid.CompareTo( other ) < 0;
		}


        /// <summary>Inferiority or equality comparer.</summary>
        /// <param name="luid">A <see cref="Luid"/>.</param>
        /// <param name="other">A <see cref="Luid"/>.</param>
        /// <returns>Returns true if the <paramref name="luid"/> is lower than or equal to the <paramref name="other"/> <see cref="Luid"/>, otherwise returns false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "luid")]
        public static bool operator <=( Luid luid, Luid other )
		{
			return luid.CompareTo( other ) <= 0;
		}


        /// <summary>Superiority comparer.</summary>
        /// <param name="luid">A <see cref="Luid"/>.</param>
        /// <param name="other">A <see cref="Luid"/>.</param>
        /// <returns>Returns true if the <paramref name="luid"/> is greater than the <paramref name="other"/> <see cref="Luid"/>, otherwise returns false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "luid")]
        public static bool operator >( Luid luid, Luid other )
		{
			return luid.CompareTo( other ) > 0;
		}


        /// <summary>Superiority or equality comparer.</summary>
        /// <param name="luid">A <see cref="Luid"/>.</param>
        /// <param name="other">A <see cref="Luid"/>.</param>
        /// <returns>Returns true if the <paramref name="luid"/> is greater than or equal to the <paramref name="other"/> <see cref="Luid"/>, otherwise returns false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "luid")]
        public static bool operator >=( Luid luid, Luid other )
		{
			return luid.CompareTo( other ) >= 0;
		}

		#endregion

	}

}