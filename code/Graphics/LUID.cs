using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{

	/// <summary>A locally unique identifier (LUID).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/aa379261%28v=vs.85%29.aspx</remarks>
	[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Luid" )]
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Native( "WinNT.h", "LUID" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Luid : IEquatable<Luid>
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


		/// <summary>Returns a string representing this <see cref="Luid"/> in a hexadecimal form.</summary>
		/// <returns>Returns a string representing this <see cref="Luid"/> in a hexadecimal form.</returns>
		public override string ToString()
		{
			return string.Format( CultureInfo.InvariantCulture, "0x{0:X16}", this.ToInt64() );
		}


		/// <summary>The «zero» <see cref="Luid"/>.</summary>
		public static readonly Luid Zero;


		#region Operators

		/// <summary><see cref="Luid"/> to <see cref="long"/> conversion operator.</summary>
		/// <param name="identifier">An <see cref="Luid"/>.</param>
		/// <returns>Returns a <see cref="long"/> representing the <paramref name="identifier"/>.</returns>
		public static implicit operator long( Luid identifier )
		{
			return identifier.ToInt64();
		}


		/// <summary>Equality comparer.</summary>
		/// <param name="identifier">A <see cref="Luid"/>.</param>
		/// <param name="other">A <see cref="Luid"/>.</param>
		/// <returns>Returns true if the <see cref="Luid"/> values are equal, otherwise returns false.</returns>
		public static bool operator ==( Luid identifier, Luid other )
		{
			return identifier.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="identifier">A <see cref="Luid"/>.</param>
		/// <param name="other">A <see cref="Luid"/>.</param>
		/// <returns>Returns true if the <see cref="Luid"/> values are not equal, otherwise returns false.</returns>
		public static bool operator !=( Luid identifier, Luid other )
		{
			return !identifier.Equals( other );
		}

		#endregion Operators

	}

}