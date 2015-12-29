using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX // .Display
{

	/// <summary>An HSL (hue, saturation, lightness) color.</summary>
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct HslColor : IEquatable<HslColor>
	{

		/// <summary>The hue, within the range [0,360).</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float Hue;

		/// <summary>The saturation, within the range [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float Saturation;

		/// <summary>The lightness, within the range [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float Lightness;

		/// <summary>The opacity, within the range [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float Opacity;



		#region Constructors

		/// <summary>Initializes a new <see cref="HslColor"/>.</summary>
		/// <param name="hue">The hue of the color, within the range [0,360).</param>
		/// <param name="saturation">The saturation of the color, within the range [0,1].</param>
		/// <param name="lightness">The lightness of the color, within the range [0,1].</param>
		/// <param name="opacity">The opacity of the color, within the range [0,1].</param>
		public HslColor( float hue, float saturation, float lightness, float opacity )
		{
			Hue = hue;
			Saturation = saturation;
			Lightness = lightness;
			Opacity = opacity;

			this.Normalize();
		}


		/// <summary>Initializes a new <see cref="HslColor"/>.</summary>
		/// <param name="hue">The hue of the color, within the range [0,360).</param>
		/// <param name="saturation">The saturation of the color, within the range [0,1].</param>
		/// <param name="lightness">The lightness of the color, within the range [0,1].</param>
		public HslColor( float hue, float saturation, float lightness )
		{
			Hue = hue;
			Saturation = saturation;
			Lightness = lightness;
			Opacity = 1.0f;

			this.Normalize();
		}

		#endregion Constructors



		/// <summary>Normalizes this <see cref="HslColor"/>.</summary>
		public void Normalize()
		{
			if( float.IsInfinity( Hue ) )
				Hue = float.NaN;
			else if( !float.IsNaN( Hue ) )
			{
				Hue %= 360.0f;
				if( Hue < 0.0f )
					Hue += 360.0f;
			}

			Saturation.Saturate();
			Lightness.Saturate();
			Opacity.Saturate();
		}


		///// <summary>Returns the corresponding ARGB color.</summary>
		///// <returns></returns>
		//public Color ToColor()
		//{
		//	this.Normalize();

		//	int a = (int)( this.Opacity * 255.0f );
		//	int r, g, b;

		//	if( !float.IsNaN( this.Hue ) && !float.IsInfinity( this.Hue ) )
		//	{
		//		this.Hue %= 360.0f;
		//		if( this.Hue < 0.0f )
		//			this.Hue += 360.0f;

		//		float h = this.Hue / 60.0f;
		//		float c = ( 1.0f - Math.Abs( this.Lightness * 2.0f - 1.0f ) ) * this.Saturation;
		//		float x = c * ( 1.0F - Math.Abs( h % 2.0f - 1.0f ) );

		//		float r1, g1, b1;
		//		switch( (int)( Math.Floor( h % 6.0f ) ) )
		//		{
		//			case 0:
		//				r1 = c;
		//				g1 = x;
		//				b1 = 0f;
		//				break;

		//			case 1:
		//				r1 = x;
		//				g1 = c;
		//				b1 = 0f;
		//				break;

		//			case 2:
		//				r1 = 0f;
		//				g1 = c;
		//				b1 = x;
		//				break;

		//			case 3:
		//				r1 = 0f;
		//				g1 = x;
		//				b1 = c;
		//				break;

		//			case 4:
		//				r1 = x;
		//				g1 = 0f;
		//				b1 = c;
		//				break;

		//			default:
		//				r1 = c;
		//				g1 = 0f;
		//				b1 = x;
		//				break;
		//		}

		//		float m = this.Lightness - c * 0.5f;
		//		r1 += m;
		//		g1 += m;
		//		b1 += m;

		//		r = (int)( r1.Clamp( 0.0f, 1.0f ) * 255.0f );
		//		g = (int)( g1.Clamp( 0.0f, 1.0f ) * 255.0f );
		//		b = (int)( b1.Clamp( 0.0f, 1.0f ) * 255.0f );
		//	}
		//	else
		//	{
		//		r = g = b = 0;
		//	}

		//	return Color.FromArgb( a, r, g, b );
		//	// http://en.wikipedia.org/wiki/HSL_and_HSV#Converting_to_RGB
		//}


		/// <summary>Returns a hash code for this <see cref="HslColor"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="HslColor"/>.</returns>
		public override int GetHashCode()
		{
			return Hue.GetHashCode() ^ Saturation.GetHashCode() ^ Lightness.GetHashCode() ^ Opacity.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="HslColor"/> equals another <see cref="HslColor"/>.</summary>
		/// <param name="other">An <see cref="HslColor"/>.</param>
		/// <returns>Returns true if the colors are equal, otherwise returns false.</returns>
		public bool Equals( HslColor other )
		{
			return ( Hue % 360.0f == other.Hue % 360.0f ) && ( other.Saturation == Saturation ) && ( other.Lightness == Lightness ) && ( other.Opacity == Opacity );
		}

		
		/// <summary>Returns a value indicating whether this <see cref="HslColor"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is an <see cref="HslColor"/> which equals this <see cref="HslColor"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is HslColor ) && this.Equals( (HslColor)obj );
		}


		/// <summary>Returns a string representing this <see cref="HslColor"/>.</summary>
		/// <returns>Returns a string representing this <see cref="HslColor"/>.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Hue: {0}, Saturation: {1}, Lightness: {2}, Opacity: {3}}}", Hue, Saturation, Lightness, Opacity );
		}



		/// <summary>Performs a linear interpolation between two <see cref="HslColor"/> structures.</summary>
		/// <param name="source">The source <see cref="HslColor"/>.</param>
		/// <param name="target">The target <see cref="HslColor"/>.</param>
		/// <param name="amount">The weighting factor.</param>
		/// <param name="result">Receives the interpolated <see cref="HslColor"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Lerp( ref HslColor source, ref HslColor target, float amount, out HslColor result )
		{
			result.Hue = source.Hue + ( target.Hue - source.Hue ) * amount;
			result.Saturation = source.Saturation + ( target.Saturation - source.Saturation ) * amount;
			result.Lightness = source.Lightness + ( target.Lightness - source.Lightness ) * amount;
			result.Opacity = source.Opacity + ( target.Opacity - source.Opacity ) * amount;

			result.Normalize();
		}

		/// <summary>Performs a linear interpolation between two <see cref="HslColor"/> structures.</summary>
		/// <param name="source">The source <see cref="HslColor"/>.</param>
		/// <param name="target">The target <see cref="HslColor"/>.</param>
		/// <param name="amount">The weighting factor.</param>
		/// <returns>Returns the interpolated <see cref="HslColor"/>.</returns>
		public static HslColor Lerp( HslColor source, HslColor target, float amount )
		{
			source.Hue += ( target.Hue - source.Hue ) * amount;
			source.Saturation += ( target.Saturation - source.Saturation ) * amount;
			source.Lightness += ( target.Lightness - source.Lightness ) * amount;
			source.Opacity += ( target.Opacity - source.Opacity ) * amount;
			
			source.Normalize();
			return source;
		}


		/// <summary></summary>
		/// <param name="color"></param>
		/// <param name="other"></param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref HslColor color, ref HslColor other, out HslColor result )
		{
			result.Hue = color.Hue + other.Hue;
			result.Saturation = color.Saturation + other.Saturation;
			result.Lightness = color.Lightness + other.Lightness;
			result.Opacity = color.Opacity + other.Opacity;
			result.Normalize();
		}

		/// <summary></summary>
		/// <param name="color"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static HslColor Add( HslColor color, HslColor other )
		{
			HslColor result;
			result.Hue = color.Hue + other.Hue;
			result.Saturation = color.Saturation + other.Saturation;
			result.Lightness = color.Lightness + other.Lightness;
			result.Opacity = color.Opacity + other.Opacity;

			result.Normalize();
			return result;
		}


		/// <summary></summary>
		/// <param name="color"></param>
		/// <param name="other"></param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref HslColor color, ref HslColor other, out HslColor result )
		{
			result.Hue = color.Hue - other.Hue;
			result.Saturation = color.Saturation - other.Saturation;
			result.Lightness = color.Lightness - other.Lightness;
			result.Opacity = color.Opacity - other.Opacity;
			result.Normalize();
		}

		/// <summary></summary>
		/// <param name="color"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static HslColor Subtract( HslColor color, HslColor other )
		{
			HslColor result;
			result.Hue = color.Hue - other.Hue;
			result.Saturation = color.Saturation - other.Saturation;
			result.Lightness = color.Lightness - other.Lightness;
			result.Opacity = color.Opacity - other.Opacity;
			
			result.Normalize();
			return result;
		}



		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="color">An <see cref="HslColor"/>.</param>
		/// <param name="other">An <see cref="HslColor"/>.</param>
		/// <returns>Returns true if the specified colors are equal, otherwise returns false.</returns>
		public static bool operator ==( HslColor color, HslColor other )
		{
			return ( color.Hue % 360.0f == other.Hue % 360.0f ) && ( color.Saturation == other.Saturation ) && ( color.Lightness == other.Lightness ) && ( color.Opacity == other.Opacity );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="color">An <see cref="HslColor"/>.</param>
		/// <param name="other">An <see cref="HslColor"/>.</param>
		/// <returns>Returns true if the specified colors are not equal, otherwise returns false.</returns>
		public static bool operator !=( HslColor color, HslColor other )
		{
			return ( color.Hue % 360.0f != other.Hue % 360.0f ) || ( color.Saturation != other.Saturation ) || ( color.Lightness != other.Lightness ) || ( color.Opacity != other.Opacity );
		}


		/// <summary>Addition operator.</summary>
		/// <param name="color"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static HslColor operator +( HslColor color, HslColor other )
		{
			HslColor result;
			result.Hue = color.Hue + other.Hue;
			result.Saturation = color.Saturation + other.Saturation;
			result.Lightness = color.Lightness + other.Lightness;
			result.Opacity = color.Opacity + other.Opacity;

			result.Normalize();
			return result;
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="color"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static HslColor operator -( HslColor color, HslColor other )
		{
			HslColor result;
			result.Hue = color.Hue - other.Hue;
			result.Saturation = color.Saturation - other.Saturation;
			result.Lightness = color.Lightness - other.Lightness;
			result.Opacity = color.Opacity - other.Opacity;

			result.Normalize();
			return result;
		}

		#endregion Operators

	}

}