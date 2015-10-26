using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{

	/// <summary>Defines the coordinates of the upper-left and lower-right corners of a rectangle.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[Serializable]
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
		public Point Location { get { return new Point( this.Left, this.Top ); } }


		/// <summary>Gets the size of the rectangle.</summary>
		public Size Size { get { return new Size( Math.Abs( this.Right - this.Left ), Math.Abs( this.Bottom - this.Top ) ); } }


		/// <summary>Gets the center of the rectangle.</summary>
		public Point Center { get { return new Point( ( this.Left + this.Right ) / 2, ( this.Top + this.Bottom ) / 2 ); } }


		/// <summary>Returns a value indicating whether this rectangle contains or intersects a point.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <returns>Returns a value indicating whether this rectangle contains or intersects a point.</returns>
		public ContainmentType Contains( Point point )
		{
			if( point.X > this.Left || point.X < this.Right || point.Y > this.Top || point.Y < this.Bottom )
				return ContainmentType.Contains;

			if( ( point.X == this.Left || point.X == this.Right ) && ( point.Y == this.Top || point.Y == this.Bottom ) )
				return ContainmentType.Intersects;
			
			return ContainmentType.Disjoint;
		}


		/// <summary>Returns a value indicating whether this rectangle contains or intersects a rectangle.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns a value indicating whether this rectangle contains or intersects another rectangle.</returns>
		public ContainmentType Contains( Rect rect )
		{
			var points = new Point[]
			{
				new Point( rect.Left, rect.Top ),
				new Point( rect.Left, rect.Bottom ),
				new Point( rect.Right, rect.Bottom ),
				new Point( rect.Right, rect.Top )
			};
			
			int contained = 0;
			for( int p = 0; p < 4; p++ )
				if( this.Contains( points[ p ] ) != ContainmentType.Disjoint )
					contained++;

			if( contained == 0 )
				return ContainmentType.Disjoint;

			if( contained == 4 )
				if( !this.Equals( rect ) )
					return ContainmentType.Contains;
			// rectangles might be the same, in which case they're intersecting each other (because they can't contain each other...)

			return ContainmentType.Intersects;
		}


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
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Left: {0}, Top: {1}, Right: {2}, Bottom: {3}}}", this.Left, this.Top, this.Right, this.Bottom );
		}


		#region Static

		/// <summary>The empty <see cref="Rect"/> structure.</summary>
		public static readonly Rect Empty = new Rect();


		/// <summary>Creates a rectangle containing the two specified rectangles.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <param name="result">Receives the rectangle containing the two specified rectangles.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#" )]
		public static void Union( ref Rect rect, ref Rect other, out Rect result )
		{
			result.Left = ( rect.Left < other.Left ) ? rect.Left : other.Left;
			result.Top = ( rect.Top < other.Top ) ? rect.Top : other.Top;
			result.Right = ( rect.Right > other.Right ) ? rect.Right : other.Right;
			result.Bottom = ( rect.Bottom > other.Bottom ) ? rect.Bottom : other.Bottom;
		}

		/// <summary>Returns a rectangle containing the two specified rectangles.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns a rectangle containing the two specified rectangles.</returns>
		public static Rect Union( Rect rect, Rect other )
		{
			Rect result;
			result.Left = ( rect.Left < other.Left ) ? rect.Left : other.Left;
			result.Top = ( rect.Top < other.Top ) ? rect.Top : other.Top;
			result.Right = ( rect.Right > other.Right ) ? rect.Right : other.Right;
			result.Bottom = ( rect.Bottom > other.Bottom ) ? rect.Bottom : other.Bottom;
			return result;
		}

		#endregion // Static


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

		// THINKABOUTME - comparison by surface, and when equal: by width, then by height.

		#endregion // Operators

	}

}