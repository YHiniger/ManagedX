using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{

	/// <summary>Represents an HSL (hue, saturation, lightness) color.</summary>
	[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HSL" )]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct HSL : IEquatable<HSL>
	{

		/// <summary>The color hue, within the range [0,360[.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float Hue;

		/// <summary>The color saturation; should be within the range [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float Saturation;

		/// <summary>The color lightness; should be within the range [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float Lightness;

		/// <summary>The color opacity; should be within the range [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float Opacity;



		#region Constructors

		/// <summary>Initializes a new <see cref="HSL"/> color.</summary>
		/// <param name="hue">The hue of the color, within the range [0,360[.</param>
		/// <param name="saturation">The saturation of the color, within the range [0,1].</param>
		/// <param name="lightness">The lightness of the color, within the range [0,1].</param>
		/// <param name="opacity">The opacity of the color, within the range [0,1].</param>
		public HSL( float hue, float saturation, float lightness, float opacity )
		{
			Hue = hue;
			Saturation = saturation;
			Lightness = lightness;
			Opacity = opacity;

			//this.Normalize();
		}


		/// <summary>Initializes a new <see cref="HSL"/> color.</summary>
		/// <param name="hue">The hue of the color, within the range [0,360[.</param>
		/// <param name="saturation">The saturation of the color, within the range [0,1].</param>
		/// <param name="lightness">The lightness of the color, within the range [0,1].</param>
		public HSL( float hue, float saturation, float lightness )
		{
			Hue = hue;
			Saturation = saturation;
			Lightness = lightness;
			Opacity = 1.0f;

			//this.Normalize();
		}

		#endregion Constructors



		/// <summary>Normalizes this <see cref="HSL"/> color.</summary>
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

			Saturation = Saturation.Saturate();
			Lightness = Lightness.Saturate();
			Opacity = Opacity.Saturate();
		}


		/// <summary>Returns a hash code for this <see cref="HSL"/> color.</summary>
		/// <returns>Returns a hash code for this <see cref="HSL"/> color.</returns>
		public override int GetHashCode()
		{
			return Hue.GetHashCode() ^ Saturation.GetHashCode() ^ Lightness.GetHashCode() ^ Opacity.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="HSL"/> color equals another <see cref="HSL"/> color.</summary>
		/// <param name="other">An <see cref="HSL"/> color.</param>
		/// <returns>Returns true if the colors are equal, otherwise returns false.</returns>
		public bool Equals( HSL other )
		{
			return ( Hue % 360.0f == other.Hue % 360.0f ) && ( other.Saturation == Saturation ) && ( other.Lightness == Lightness ) && ( other.Opacity == Opacity );
		}


		/// <summary>Returns a value indicating whether this <see cref="HSL"/> color is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is an <see cref="HSL"/> color which equals this <see cref="HSL"/> color, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is HSL hsl && this.Equals( hsl );
		}


		/// <summary>Returns the corresponding <see cref="Color"/>.</summary>
		/// <returns>Returns the corresponding <see cref="Color"/>.</returns>
		public Color ToColor()
		{
			this.Normalize();

			int r, g, b;

			if( !float.IsNaN( this.Hue ) )
			{
				var h = this.Hue / 60.0f;
				var c = ( 1.0f - Math.Abs( this.Lightness * 2.0f - 1.0f ) ) * this.Saturation;
				var x = c * ( 1.0F - Math.Abs( h % 2.0f - 1.0f ) );

				float r1, g1, b1;
				var n = (int)( Math.Floor( h % 6.0f ) );

				if( n >= 4 )
				{
					g1 = 0.0f;
					if( n == 4 )
					{
						r1 = x;
						b1 = c;
					}
					else //if( n == 5 )
					{
						r1 = c;
						b1 = x;
					}
				}
				else if( n >= 2 )
				{
					r1 = 0.0f;
					if( n == 2 )
					{
						g1 = c;
						b1 = x;
					}
					else //if( n == 3 )
					{
						g1 = x;
						b1 = c;
					}
				}
				else
				{
					b1 = 0.0f;
					if( n == 0 )
					{
						r1 = c;
						g1 = x;
					}
					else //if( n == 1 )
					{
						r1 = x;
						g1 = c;
					}
				}


				var m = this.Lightness - c * 0.5f;
				r1 += m;
				g1 += m;
				b1 += m;

				r = (int)( r1.Clamp( 0.0f, 1.0f ) * 255.0f );
				g = (int)( g1.Clamp( 0.0f, 1.0f ) * 255.0f );
				b = (int)( b1.Clamp( 0.0f, 1.0f ) * 255.0f );
			}
			else
				r = g = b = 0;

			var a = (int)( this.Opacity * 255.0f );

			return new Color( (byte)r, (byte)g, (byte)b, (byte)a );
			// http://en.wikipedia.org/wiki/HSL_and_HSV#Converting_to_RGB
		}


		/// <summary>Returns an <see cref="RGB"/> color corresponding to this <see cref="HSL"/> color.</summary>
		/// <param name="preMultiply">Indicates whether the returned color is pre-multiplied.</param>
		/// <returns>Returns an <see cref="RGB"/> color corresponding to this <see cref="HSL"/> color.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RGB" )]
		public RGB ToRGB( bool preMultiply )
		{
			this.Normalize();

			RGB result;
			if( float.IsNaN( this.Hue ) )
				result.R = result.G = result.B = 0.0f;
			else
			{
				var h = this.Hue / 60.0f;
				var c = ( 1.0f - Math.Abs( this.Lightness * 2.0f - 1.0f ) ) * this.Saturation;
				var x = c * ( 1.0F - Math.Abs( h % 2.0f - 1.0f ) );

				var n = (int)( Math.Floor( h % 6.0f ) );

				if( n >= 4 )
				{
					result.G = 0.0f;
					if( n == 4 )
					{
						result.R = x;
						result.B = c;
					}
					else //if( n == 5 )
					{
						result.R = c;
						result.B = x;
					}
				}
				else if( n >= 2 )
				{
					result.R = 0.0f;
					if( n == 2 )
					{
						result.G = c;
						result.B = x;
					}
					else //if( n == 3 )
					{
						result.G = x;
						result.B = c;
					}
				}
				else
				{
					result.B = 0.0f;
					if( n == 0 )
					{
						result.R = c;
						result.G = x;
					}
					else //if( n == 1 )
					{
						result.R = x;
						result.G = c;
					}
				}


				var m = this.Lightness - c * 0.5f;
				result.R += m;
				result.G += m;
				result.B += m;


				if( preMultiply )
				{
					result.R *= Opacity;
					result.G *= Opacity;
					result.B *= Opacity;
				}


				result.R = result.R.Saturate();
				result.G = result.G.Saturate();
				result.B = result.B.Saturate();
			}

			return result;
		}


		/// <summary>Returns an <see cref="RGBA"/> color corresponding to this <see cref="HSL"/> color.</summary>
		/// <param name="preMultiply">Indicates whether the returned color is pre-multiplied.</param>
		/// <returns>Returns an <see cref="RGBA"/> color corresponding to this <see cref="HSL"/> color.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RGBA" )]
		public RGBA ToRGBA( bool preMultiply )
		{
			this.Normalize();

			RGBA result;
			if( float.IsNaN( this.Hue ) )
			{
				result.R = result.G = result.B = 0.0f;
				result.A = 1.0f;
			}
			else
			{
				var h = this.Hue / 60.0f;
				var c = ( 1.0f - Math.Abs( this.Lightness * 2.0f - 1.0f ) ) * this.Saturation;
				var x = c * ( 1.0F - Math.Abs( h % 2.0f - 1.0f ) );

				var n = (int)( Math.Floor( h % 6.0f ) );

				if( n >= 4 )
				{
					result.G = 0.0f;
					if( n == 4 )
					{
						result.R = x;
						result.B = c;
					}
					else //if( n == 5 )
					{
						result.R = c;
						result.B = x;
					}
				}
				else if( n >= 2 )
				{
					result.R = 0.0f;
					if( n == 2 )
					{
						result.G = c;
						result.B = x;
					}
					else //if( n == 3 )
					{
						result.G = x;
						result.B = c;
					}
				}
				else
				{
					result.B = 0.0f;
					if( n == 0 )
					{
						result.R = c;
						result.G = x;
					}
					else //if( n == 1 )
					{
						result.R = x;
						result.G = c;
					}
				}


				var m = this.Lightness - c * 0.5f;
				result.R += m;
				result.G += m;
				result.B += m;

				if( preMultiply )
				{
					result.R *= Opacity;
					result.G *= Opacity;
					result.B *= Opacity;
				}


				result.R = result.R.Saturate();
				result.G = result.G.Saturate();
				result.B = result.B.Saturate();
				result.A = Opacity.Saturate();
			}

			return result;
		}


		/// <summary>Returns a string representing this <see cref="HSL"/> color.</summary>
		/// <returns>Returns a string representing this <see cref="HSL"/> color.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Hue: {0}, Saturation: {1}, Lightness: {2}, Opacity: {3}}}", Hue, Saturation, Lightness, Opacity );
		}



		/// <summary>Calculates the sum of two <see cref="HSL"/> colors.</summary>
		/// <param name="color">An <see cref="HSL"/> color.</param>
		/// <param name="other">An <see cref="HSL"/> color.</param>
		/// <param name="result">Receives the sum of the specified colors.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref HSL color, ref HSL other, out HSL result )
		{
			result.Hue = color.Hue + other.Hue;
			result.Saturation = color.Saturation + other.Saturation;
			result.Lightness = color.Lightness + other.Lightness;
			result.Opacity = color.Opacity + other.Opacity;
			result.Normalize();
		}

		/// <summary>Returns the sum of two <see cref="HSL"/> colors.</summary>
		/// <param name="color">An <see cref="HSL"/> color.</param>
		/// <param name="other">An <see cref="HSL"/> color.</param>
		/// <returns>Returns the sum of the specified colors.</returns>
		public static HSL Add( HSL color, HSL other )
		{
			HSL result;
			result.Hue = color.Hue + other.Hue;
			result.Saturation = color.Saturation + other.Saturation;
			result.Lightness = color.Lightness + other.Lightness;
			result.Opacity = color.Opacity + other.Opacity;

			result.Normalize();
			return result;
		}


		/// <summary>Calculates the difference between two <see cref="HSL"/> colors.</summary>
		/// <param name="color">An <see cref="HSL"/> color.</param>
		/// <param name="other">An <see cref="HSL"/> color.</param>
		/// <param name="result">Receives the resulting <see cref="HSL"/> color.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref HSL color, ref HSL other, out HSL result )
		{
			result.Hue = color.Hue - other.Hue;
			result.Saturation = color.Saturation - other.Saturation;
			result.Lightness = color.Lightness - other.Lightness;
			result.Opacity = color.Opacity - other.Opacity;
			result.Normalize();
		}

		/// <summary>Returns the difference between two <see cref="HSL"/> colors.</summary>
		/// <param name="color">An <see cref="HSL"/> color.</param>
		/// <param name="other">An <see cref="HSL"/> color.</param>
		/// <returns>Returns the resulting <see cref="HSL"/> color.</returns>
		public static HSL Subtract( HSL color, HSL other )
		{
			HSL result;
			result.Hue = color.Hue - other.Hue;
			result.Saturation = color.Saturation - other.Saturation;
			result.Lightness = color.Lightness - other.Lightness;
			result.Opacity = color.Opacity - other.Opacity;
			
			result.Normalize();
			return result;
		}


		/// <summary>Performs a linear interpolation between two <see cref="HSL"/> colors.</summary>
		/// <param name="source">The source <see cref="HSL"/> color.</param>
		/// <param name="target">The target <see cref="HSL"/> color.</param>
		/// <param name="amount">The weighting factor, within the range [0,1].</param>
		/// <param name="result">Receives the interpolated <see cref="HSL"/> color.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Lerp( ref HSL source, ref HSL target, float amount, out HSL result )
		{
			result.Hue = source.Hue + ( target.Hue - source.Hue ) * amount;
			result.Saturation = source.Saturation + ( target.Saturation - source.Saturation ) * amount;
			result.Lightness = source.Lightness + ( target.Lightness - source.Lightness ) * amount;
			result.Opacity = source.Opacity + ( target.Opacity - source.Opacity ) * amount;

			result.Normalize();
		}

		/// <summary>Performs a linear interpolation between two <see cref="HSL"/> colors.</summary>
		/// <param name="source">The source <see cref="HSL"/> color.</param>
		/// <param name="target">The target <see cref="HSL"/> color.</param>
		/// <param name="amount">The weighting factor, within the range [0,1].</param>
		/// <returns>Returns the interpolated <see cref="HSL"/> color.</returns>
		public static HSL Lerp( HSL source, HSL target, float amount )
		{
			source.Hue += ( target.Hue - source.Hue ) * amount;
			source.Saturation += ( target.Saturation - source.Saturation ) * amount;
			source.Lightness += ( target.Lightness - source.Lightness ) * amount;
			source.Opacity += ( target.Opacity - source.Opacity ) * amount;
			
			source.Normalize();
			return source;
		}



		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="color">An <see cref="HSL"/> color.</param>
		/// <param name="other">An <see cref="HSL"/> color.</param>
		/// <returns>Returns true if the specified colors are equal, otherwise returns false.</returns>
		public static bool operator ==( HSL color, HSL other )
		{
			return ( color.Hue % 360.0f == other.Hue % 360.0f ) && ( color.Saturation == other.Saturation ) && ( color.Lightness == other.Lightness ) && ( color.Opacity == other.Opacity );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="color">An <see cref="HSL"/> color.</param>
		/// <param name="other">An <see cref="HSL"/> color.</param>
		/// <returns>Returns true if the specified colors are not equal, otherwise returns false.</returns>
		public static bool operator !=( HSL color, HSL other )
		{
			return ( color.Hue % 360.0f != other.Hue % 360.0f ) || ( color.Saturation != other.Saturation ) || ( color.Lightness != other.Lightness ) || ( color.Opacity != other.Opacity );
		}


		/// <summary>Addition operator.</summary>
		/// <param name="color">An <see cref="HSL"/> color.</param>
		/// <param name="other">An <see cref="HSL"/> color.</param>
		/// <returns>Returns the sum of the specified colors.</returns>
		public static HSL operator +( HSL color, HSL other )
		{
			HSL result;
			result.Hue = color.Hue + other.Hue;
			result.Saturation = color.Saturation + other.Saturation;
			result.Lightness = color.Lightness + other.Lightness;
			result.Opacity = color.Opacity + other.Opacity;

			result.Normalize();
			return result;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="color">An <see cref="HSL"/> color.</param>
		/// <param name="other">An <see cref="HSL"/> color.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static HSL operator -( HSL color, HSL other )
		{
			HSL result;
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