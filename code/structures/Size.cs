using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	
	/// <summary>Represents a size (integer) in 2D space.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Size : IEquatable<Size>
	{

		/// <summary>The width component of this <see cref="Size"/> structure; should be greater than or equal to zero.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Width;

		/// <summary>The height component of this <see cref="Size"/> structure; should be greater than or equal to zero.</summary>
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


		#region Static

		/// <summary>The empty (or zero) <see cref="Size"/> structure.</summary>
		public static readonly Size Empty = new Size();


		/// <summary>Adds two <see cref="Size"/> structures.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <param name="result">Receives the sum of the specified sizes.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#" )]
		public static void Add( ref Size size, ref Size other, out Size result )
		{
			result.Width = size.Width + other.Width;
			result.Height = size.Height + other.Height;
		}

		/// <summary>Adds two <see cref="Size"/> values.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns></returns>
		public static Size Add( Size size, Size other )
		{
			size.Width += other.Width;
			size.Height += other.Height;
			return size;
		}


		/// <summary>Subtracts a <see cref="Size"/> (<paramref name="other"/>) from another <see cref="Size"/> (<paramref name="size"/>).</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <param name="result">Receives the difference between the specified sizes.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#" )]
		public static void Subtract( ref Size size, ref Size other, out Size result )
		{
			result.Width = size.Width - other.Width;
			result.Height = size.Height - other.Height;
		}

		/// <summary>Subtracts a <see cref="Size"/> (<paramref name="other"/>) from another <see cref="Size"/> (<paramref name="size"/>).</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns a <see cref="Size"/> structure initialized with the result of ( <paramref name="size"/> - <paramref name="other"/> ).</returns>
		public static Size Subtract( Size size, Size other )
		{
			size.Width -= other.Width;
			size.Height -= other.Height;
			return size;
		}


		/// <summary>Multiplies two <see cref="Size"/> structures.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <param name="result">Receives the result of the product.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#" )]
		public static void Multiply( ref Size size, ref Size other, out Size result )
		{
			result.Width = size.Width * other.Width;
			result.Height = size.Height * other.Height;
		}

		/// <summary>Returns the product of two <see cref="Size"/> structures.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns the product of the two specified <see cref="Size"/> structures.</returns>
		public static Size Multiply( Size size, Size other )
		{
			size.Width *= other.Width;
			size.Height *= other.Height;
			return size;
		}


		/// <summary>Multiplies a <see cref="Size"/> by an integer.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="value">An integer value.</param>
		/// <param name="result">Receives the result of the product.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#" )]
		public static void Multiply( ref Size size, int value, out Size result )
		{
			result.Width = size.Width * value;
			result.Height = size.Height * value;
		}

		/// <summary>Returns the product of a <see cref="Size"/> by an integer.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="value">An integer value.</param>
		/// <returns>Returns the product of <paramref name="size"/> x <paramref name="value"/>.</returns>
		public static Size Multiply( Size size, int value )
		{
			size.Width *= value;
			size.Height *= value;
			return size;
		}

		#endregion // Static


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


		/// <summary>Inferiority comparer.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <( Size size, Size other )
		{
			return ( size.Width < other.Width ) && ( size.Height < other.Height );
		}


		/// <summary>Inferiority or equality comparer.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <=( Size size, Size other )
		{
			return ( size.Width <= other.Width ) && ( size.Height <= other.Height );
		}


		/// <summary>Superiority comparer.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >( Size size, Size other )
		{
			return ( size.Width > other.Width ) && ( size.Height > other.Height );
		}


		/// <summary>Superiority or equality comparer.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >=( Size size, Size other )
		{
			return ( size.Width >= other.Width ) && ( size.Height >= other.Height );
		}



		/// <summary>Addition operator.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns the sum of the two specified <see cref="Size"/> values.</returns>
		public static Size operator +( Size size, Size other )
		{
			size.Width += other.Width;
			size.Height += other.Height;
			return size;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns the difference between the two specified <see cref="Size"/> values.</returns>
		public static Size operator -( Size size, Size other )
		{
			size.Width -= other.Width;
			size.Height -= other.Height;
			return size;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="other">A <see cref="Size"/> structure.</param>
		/// <returns>Returns the product of the specified values.</returns>
		public static Size operator *( Size size, Size other )
		{
			size.Width *= other.Width;
			size.Height *= other.Height;
			return size;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <param name="value">An integer value.</param>
		/// <returns>Returns the product of the specified values.</returns>
		public static Size operator *( Size size, int value )
		{
			size.Width *= value;
			size.Height *= value;
			return size;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">An integer value.</param>
		/// <param name="size">A <see cref="Size"/> structure.</param>
		/// <returns>Returns the product of the specified values.</returns>
		public static Size operator *( int value, Size size )
		{
			size.Width *= value;
			size.Height *= value;
			return size;
		}

		#endregion // Operators

	}

}