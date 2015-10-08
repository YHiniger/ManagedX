using System;
using System.Runtime.InteropServices;

// Comments checked (2015/09/23)


namespace ManagedX
{
	
	/// <summary>Contains a width and a height.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Size : IEquatable<Size>
	{

		private int width;
		private int height;


		/// <summary>Initializes a new <see cref="Size"/> structure.</summary>
		/// <param name="width">The width component of the size.</param>
		/// <param name="height">The height component of the size.</param>
		public Size( int width, int height )
		{
			if( width < 0 )
				throw new ArgumentOutOfRangeException( "width" );
			if( height < 0 )
				throw new ArgumentOutOfRangeException( "height" );
			
			this.width = width;
			this.height = height;
		}


		/// <summary>Gets the width component of this <see cref="Size"/> structure.</summary>
		public int Width { get { return width; } }

		/// <summary>Gets the height component of this <see cref="Size"/> structure.</summary>
		public int Height { get { return height; } }


		/// <summary>Returns a hash code for this <see cref="Size"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Size"/> structure.</returns>
		public override int GetHashCode()
		{
			return width ^ height;
		}


		/// <summary>Returns a value indicating whether this <see cref="Size"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Size other )
		{
			return ( width == other.width ) && ( height == other.height );
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
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}×{1}", width, height );
		}
		


		/// <summary>The empty (or zero) <see cref="Size"/> structure.</summary>
		public static readonly Size Empty = new Size();


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

		#endregion

	}

}