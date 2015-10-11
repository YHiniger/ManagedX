using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	
	/// <summary>Represents a size (integer) in 2D space.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[Serializable]
	[ComVisible( true )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Size : IEquatable<Size>
	{

		/// <summary>The width component of this <see cref="Size"/> structure.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Width;
		
		/// <summary>The height component of this <see cref="Size"/> structure.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Height;


		/// <summary>Initializes a new <see cref="Size"/> structure.</summary>
		/// <param name="width">The width component of the size; must be greater than or equal to zero.</param>
		/// <param name="height">The height component of the size; must be greater than or equal to zero.</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public Size( int width, int height )
		{
			if( width < 0 )
				throw new ArgumentOutOfRangeException( "width" );
			
			if( height < 0 )
				throw new ArgumentOutOfRangeException( "height" );
			
			this.Width = width;
			this.Height = height;
		}


		/// <summary>Returns a hash code for this <see cref="Size"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Size"/> structure.</returns>
		public override int GetHashCode()
		{
			return Width ^ Height;
		}


		/// <summary>Returns a value indicating whether this <see cref="Size"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Size other )
		{
			return ( Width == other.Width ) && ( Height == other.Height );
		}

		/// <summary>Returns a value indicating whether this <see cref="Size"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Size"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Size ) && this.Equals( (Size)obj );
		}


		/// <summary>Returns a string representing this <see cref="Size"/> structure, in the form:
		/// <para><see cref="Width"/>×<see cref="Height"/></para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="Size"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}×{1}", Width, Height );
		}
		


		/// <summary>The empty (or zero) <see cref="Size"/> structure.</summary>
		public static readonly Size Empty = new Size();


		/// <summary>Adds two <see cref="Size"/> values.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns></returns>
		[ComVisible( false )]
		public static Size Add( Size size, Size other )
		{
			return new Size( size.Width + other.Width, size.Height + other.Height );
		}


		/// <summary>Subtracts a <see cref="Size"/> (<paramref name="other"/>) from another <see cref="Size"/> (<paramref name="size"/>).
		/// <para>Beware that the returned value is a <see cref="Point"/> structure. This happens because the result might contain negative values, which is invalid for a size.</para>
		/// </summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns a <see cref="Point"/> structure initialized with the result of ( <paramref name="size"/> - <paramref name="other"/> ).</returns>
		[ComVisible( false )]
		public static Point Subtract( Size size, Size other )
		{
			return new Point( size.Width - other.Width, size.Height - other.Height );
		}


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Size size, Size other )
		{
			return size.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Size size, Size other )
		{
			return !size.Equals( other );
		}



		/// <summary>Addition operator.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns the sum of the two specified <see cref="Size"/> values.</returns>
		public static Size operator +( Size size, Size other )
		{
			return new Size( size.Width + other.Width, size.Height + other.Height );
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns the difference between the two specified <see cref="Size"/> values.</returns>
		public static Point operator -( Size size, Size other )
		{
			return new Point( size.Width - other.Width, size.Height - other.Height );
		}

		#endregion

	}

}