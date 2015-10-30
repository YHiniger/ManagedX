﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{

	/// <summary>Defines the (integer) coordinates of the upper-left and lower-right corners of a rectangle.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct Rect : IEquatable<Rect>
	{

		/// <summary>The position of the left side of the rectangle; also known as "X".</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance matters." )]
		public int Left;

		/// <summary>The position of the top of the rectangle; also known as "Y".</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance matters." )]
		public int Top;

		/// <summary>The position of the right side of the rectangle.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance matters." )]
		public int Right;

		/// <summary>The position of the bottom of the rectangle.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance matters." )]
		public int Bottom;


		/// <summary>Initializes a new <see cref="Rect"/> structure with the specified values.</summary>
		/// <param name="left">The position of the left side of the rectangle; also known as "X".</param>
		/// <param name="top">The position of the top of the rectangle; also known as "Y".</param>
		/// <param name="right">The position of the right side of the rectangle.</param>
		/// <param name="bottom">The position of the bottom of the rectangle.</param>
		public Rect( int left, int top, int right, int bottom )
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}


		/// <summary>Gets or sets the position of the upper left corner of the rectangle.</summary>
		public Point UpperLeftCorner
		{
			get { return new Point( Left, Top ); }
			set
			{
				Left = value.X;
				Top = value.Y;
			}
		}

		
		/// <summary>Gets or sets the position of the lower right corner of the rectangle.</summary>
		public Point LowerRightCorner
		{
			get { return new Point( Right, Bottom ); }
			set
			{
				Right = value.X;
				Bottom = value.Y;
			}
		}


		/// <summary>Gets the center of the rectangle.</summary>
		public Point Center { get { return new Point( ( Left + Right ) / 2, ( Top + Bottom ) / 2 ); } }


		/// <summary>Gets or sets the horizontal position of the rectangle.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Justification = "Uniformity." )]
		public int X
		{
			get { return Left; }
			set
			{
				Right += value - Left;
				Left = value;
			}
		}


		/// <summary>Gets or sets the vertical position of the rectangle.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Justification = "Uniformity." )]
		public int Y
		{
			get { return Top; }
			set
			{
				Bottom += value - Top;
				Top = value;
			}
		}


		/// <summary>Gets or sets the width of the rectangle.</summary>
		public int Width
		{
			get { return Math.Abs( Right - Left ); }
			set { Right = Left + value; }
		}


		/// <summary>Gets or sets the height of the rectangle.</summary>
		public int Height
		{
			get { return Math.Abs( Bottom - Top ); }
			set { Bottom = Top + value; }
		}


		/// <summary>Gets or sets the size of the rectangle.</summary>
		public Size Size
		{
			get { return new Size( Math.Abs( Right - Left ), Math.Abs( Bottom - Top ) ); }
			set
			{
				Right = Left + value.Width;
				Bottom = Top + value.Height;
			}
		}


		#region Contains

		/// <summary>Returns a value indicating whether this rectangle contains or intersects a point, given its coordinates.</summary>
		/// <param name="x">The position of the point along the horizontal axis.</param>
		/// <param name="y">The position of the point along the vertical axis.</param>
		/// <returns>Returns a value indicating whether this rectangle contains or intersects the specified coordinates.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Seriously?" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Seriously?" )]
		public ContainmentType Contains( int x, int y )
		{
			if( x < Left || x > Right || y < Top || y > Bottom )
				return ContainmentType.Disjoint;

			if( x > Left && x < Right && y > Top && y < Bottom )
				return ContainmentType.Contains;

			return ContainmentType.Intersects;
		}


		/// <summary>Returns a value indicating whether this rectangle contains or intersects a point.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <returns>Returns a value indicating whether this rectangle contains or intersects a point.</returns>
		public ContainmentType Contains( Point point )
		{
			if( Left > point.X || Right < point.X || Top > point.Y || Bottom < point.Y )
				return ContainmentType.Disjoint;

			if( Left < point.X && Right > point.X && Top < point.Y && Bottom > point.Y )
				return ContainmentType.Contains;

			return ContainmentType.Intersects;
		}

		/// <summary>Obtains a value indicating whether this rectangle contains or intersects with a <see cref="Point"/>.</summary>
		/// <param name="point">A <see cref="Point"/> structure.</param>
		/// <param name="result">Receives a value indicating whether the specified <paramref name="point"/> is contained by or intersects with this rectangle.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		public void Contains( ref Point point, out ContainmentType result )
		{
			if( Left > point.X || Right < point.X || Top > point.Y || Bottom < point.Y )
				result = ContainmentType.Disjoint;
			else if( Left < point.X && Right > point.X && Top < point.Y && Bottom > point.Y )
				result = ContainmentType.Contains;
			else
				result = ContainmentType.Intersects;
		}


		/// <summary>Returns a value indicating whether this rectangle contains or intersects a rectangle.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns a value indicating whether this rectangle contains or intersects another rectangle.</returns>
		public ContainmentType Contains( Rect rect )
		{
			if( Left < rect.Left && Right > rect.Right && Top < rect.Top && Bottom > rect.Bottom )
				return ContainmentType.Contains;

			if( Left > rect.Right || Right < rect.Left || Top > rect.Bottom || Bottom < rect.Top )
				return ContainmentType.Disjoint;

			return ContainmentType.Intersects;
		}

		/// <summary>Indicates whether this rectangle contains or intersects with another rectangle.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="result">Receives a value indicating whether the specified <paramref name="rect"/> intersects with or is contained by this rectangle.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		public void Contains( ref Rect rect, out ContainmentType result )
		{
			if( Left < rect.Left && Right > rect.Right && Top < rect.Top && Bottom > rect.Bottom )
				result = ContainmentType.Contains;
			else if( Left > rect.Right || Right < rect.Left || Top > rect.Bottom || Bottom < rect.Top )
				result = ContainmentType.Disjoint;
			else
				result = ContainmentType.Intersects;
		}

		#endregion


		/// <summary>Offsets the rectangle by the specified amounts.</summary>
		/// <param name="horizontalAmount">The horizontal amount.</param>
		/// <param name="verticalAmount">The vertical amount.</param>
		public void Offset( int horizontalAmount, int verticalAmount )
		{
			Left += horizontalAmount;
			Top += verticalAmount;
			Right += horizontalAmount;
			Bottom += verticalAmount;
		}

		/// <summary>Offsets the rectangle by the specified amount.</summary>
		/// <param name="amount">A <see cref="Point"/> structure containing the horizontal and vertical amount.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		public void Offset( ref Point amount )
		{
			Left += amount.X;
			Top += amount.Y;
			Right += amount.X;
			Bottom += amount.Y;
		}


		/// <summary>Pushes the edges of the rectangle by the specified amounts.</summary>
		/// <param name="horizontalAmount">The horizontal amount.</param>
		/// <param name="verticalAmount">The vertical amount.</param>
		public void Inflate( int horizontalAmount, int verticalAmount )
		{
			Left -= horizontalAmount;
			Top -= verticalAmount;
			Right += horizontalAmount;
			Bottom += verticalAmount;
		}

		/// <summary>Pushes the edges of the rectangle by the specified amount.</summary>
		/// <param name="amount">A <see cref="Size"/> structure containing the horizontal and vertical amount.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		public void Inflate( ref Size amount )
		{
			Left -= amount.Width;
			Top -= amount.Height;
			Right += amount.Width;
			Bottom += amount.Height;
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
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Left: {0}, Top: {1}, Right: {2}, Bottom: {3}}}", Left, Top, Right, Bottom );
		}


		#region Static

		/// <summary>The empty <see cref="Rect"/> structure.</summary>
		public static readonly Rect Empty = new Rect();


		/// <summary>Negates a <see cref="Rect"/> structure.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="result">Receives the negated <paramref name="rect"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public static void Negate( ref Rect rect, out Rect result )
		{
			result.Left = -rect.Right;
			result.Top = -rect.Bottom;
			result.Right = -rect.Left;
			result.Bottom = -rect.Top;
		}

		/// <summary>Negates a <see cref="Rect"/> structure.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns the negated <paramref name="rect"/>.</returns>
		public static Rect Negate( Rect rect )
		{
			return new Rect( -rect.Right, -rect.Bottom, -rect.Left, -rect.Top );
		}


		/// <summary>Creates a rectangle containing the two specified rectangles.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <param name="result">Receives the rectangle containing the two specified rectangles.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#" )]
		public static void Union( ref Rect rect, ref Rect other, out Rect result )
		{
			result.Left = Math.Min( rect.Left, other.Left );
			result.Top = Math.Min( rect.Top, other.Top );
			result.Right = Math.Max( rect.Right, other.Right );
			result.Bottom = Math.Max( rect.Bottom, other.Bottom );
		}

		/// <summary>Returns a rectangle containing the two specified rectangles.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns a rectangle containing the two specified rectangles.</returns>
		public static Rect Union( Rect rect, Rect other )
		{
			rect.Left = Math.Min( rect.Left, other.Left );
			rect.Top = Math.Min( rect.Top, other.Top );
			rect.Right = Math.Max( rect.Right, other.Right );
			rect.Bottom = Math.Max( rect.Bottom, other.Bottom );
			return rect;
		}


		/// <summary>Creates a <see cref="Rect"/> structure defining the area where one rectangle overlaps with another rectangle.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <param name="result">Receives the area where the two rectangles overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#" )]
		public static void Intersect( ref Rect rect, ref Rect other, out Rect result )
		{
			var left = Math.Max( rect.Left, other.Left );
			var top = Math.Max( rect.Top, other.Top );
			var right = Math.Min( rect.Right, other.Right );
			var bottom = Math.Min( rect.Bottom, other.Bottom );
			
			if( right > left && bottom > top )
			{
				result.Left = left;
				result.Top = top;
				result.Right = right;
				result.Bottom = bottom;
				return;
			}

			result = Rect.Empty;
		}

		/// <summary>Returns a <see cref="Rect"/> structure defining the area where one rectangle overlaps with another rectangle.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns the area where the two rectangles overlap.</returns>
		public static Rect Intersect( Rect rect, Rect other )
		{
			var left = Math.Max( rect.Left, other.Left );
			var top = Math.Max( rect.Top, other.Top );
			var right = Math.Min( rect.Right, other.Right );
			var bottom = Math.Min( rect.Bottom, other.Bottom );

			if( right > left && bottom > top )
			{
				rect.Left = left;
				rect.Top = top;
				rect.Right = right;
				rect.Bottom = bottom;
				return rect;
			}

			return Rect.Empty;
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


		/// <summary>Unary negation operator.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns the negated <paramref name="rect"/>.</returns>
		public static Rect operator -( Rect rect )
		{
			return new Rect( -rect.Right, -rect.Bottom, -rect.Left, -rect.Top );
		}


		/// <summary>Union operator.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns a rectangle containing the two specified rectangles.</returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Union is the alternate method." )]
		public static Rect operator +( Rect rect, Rect other )
		{
			rect.Left = Math.Min( rect.Left, other.Left );
			rect.Top = Math.Min( rect.Top, other.Top );
			rect.Right = Math.Max( rect.Right, other.Right );
			rect.Bottom = Math.Max( rect.Bottom, other.Bottom );
			return rect;
		}


		/// <summary>Intersection operator.</summary>
		/// <param name="rect">A <see cref="Rect"/> structure.</param>
		/// <param name="other">A <see cref="Rect"/> structure.</param>
		/// <returns>Returns the area where the two rectangles overlap.</returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Intersect is the alternate method." )]
		public static Rect operator -( Rect rect, Rect other )
		{
			var left = Math.Max( rect.Left, other.Left );
			var top = Math.Max( rect.Top, other.Top );
			var right = Math.Min( rect.Right, other.Right );
			var bottom = Math.Min( rect.Bottom, other.Bottom );

			if( right > left && bottom > top )
			{
				rect.Left = left;
				rect.Top = top;
				rect.Right = right;
				rect.Bottom = bottom;
				return rect;
			}

			return Rect.Empty;
		}

		#endregion // Operators

	}

}