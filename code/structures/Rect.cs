using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

// Comments checked (2015/09/23)


namespace ManagedX
{

	/// <summary>Defines the coordinates of the upper-left and lower-right corners of a rectangle.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct Rect : IEquatable<Rect>
	{

		/// <summary>The position of the left side of the rectangle; also known as "X".</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Left;

		/// <summary>The position of the top of the rectangle; also known as "Y".</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Top;

		/// <summary>The position of the right side of the rectangle.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Right;

		/// <summary>The position of the bottom of the rectangle.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Bottom;


		/// <summary>Gets the position of the upper left corner of the rectangle.</summary>
		public Point Position { get { return new Point( this.Left, this.Top ); } }

		/// <summary>Gets the size of the rectangle.</summary>
		public Size Size { get { return new Size( Math.Abs( this.Right - this.Left ), Math.Abs( this.Bottom - this.Top ) ); } }


		/// <summary>Returns a hash code for this <see cref="Rect"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Rect"/> structure.</returns>
		public override int GetHashCode()
		{
			return Left ^ Top ^ Right ^ Bottom;
		}


		/// <summary>Returns a value indicating whether this <see cref="Rect"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns true if this structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( Rect other )
		{
			return ( Left == other.Left ) && ( Top == other.Top ) && ( Right == other.Right ) && ( Bottom == other.Bottom );
		}


		/// <summary>Returns a value indicating whether this <see cref="Rect"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Rect"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Rect ) && this.Equals( (Rect)obj );
		}


		/// <summary>Returns a string representing this <see cref="Rect"/> structure.</summary>
		/// <returns>Returns a string representing this <see cref="Rect"/> structure.</returns>
		public override string ToString()
		{
			return "{" + string.Format( System.Globalization.CultureInfo.InvariantCulture, "Left: {0}, Top: {1}, Right: {2}, Bottom: {3}", this.Left, this.Top, this.Right, this.Bottom ) + "}";
		}


		/// <summary>The empty <see cref="Rect"/> structure.</summary>
		public static readonly Rect Empty = new Rect();


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Rect rect, Rect other )
		{
			return rect.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Rect rect, Rect other )
		{
			return !rect.Equals( other );
		}

		#endregion

	}

}