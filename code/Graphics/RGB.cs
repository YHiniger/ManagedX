using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{

	/// <summary>Represents an RGB color.
	/// <para>This structure is equivalent to the native structure <code>DXGI_RGB</code>.</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/bb173062%28v=vs.85%29.aspx</remarks>
	[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RGB" )]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 12 )]
	public struct RGB : IEquatable<RGB>
	{

		/// <summary>The red component of the color; should be within the range [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public float R;

		/// <summary>The green component of the color; should be within the range [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public float G;

		/// <summary>The blue component of the color; should be within the range [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public float B;



		#region Constructors

		/// <summary>Initializes a new <see cref="RGB"/> structure.</summary>
		/// <param name="red">The value of the red component.</param>
		/// <param name="green">The value of the green component.</param>
		/// <param name="blue">The value of the blue component.</param>
		public RGB( float red, float green, float blue )
		{
			R = red;
			G = green;
			B = blue;
		}


		/// <summary>Initializes a new <see cref="RGB"/> structure from a <see cref="Vector3"/>.</summary>
		/// <param name="rgb">A <see cref="Vector3"/> representing the RGB color.</param>
		public RGB( Vector3 rgb )
		{
			R = rgb.X;
			G = rgb.Y;
			B = rgb.Z;
		}

		#endregion Constructors



		/// <summary>Normalizes this <see cref="RGB"/> color so that its components are within the range [0,1], while preserving R:G:B ratios.</summary>
		public void Normalize()
		{
			if( R < 0.0f )
				R = 0.0f;

			if( G < 0.0f )
				G = 0.0f;

			if( B < 0.0f )
				B = 0.0f;

			var max = Math.Max( Math.Max( R, G ), B );
			if( max > 1.0f )
			{
				R /= max;
				G /= max;
				B /= max;
			}
		}


		/// <summary>Forces the component of this <see cref="RGB"/> color within the range [0,1].</summary>
		public void Saturate()
		{
			if( R < 0.0f )
				R = 0.0f;
			else if( R > 1.0f )
				R = 1.0f;

			if( G < 0.0f )
				G = 0.0f;
			else if( G > 1.0f )
				G = 1.0f;

			if( B < 0.0f )
				B = 0.0f;
			else if( B > 1.0f )
				B = 1.0f;
		}


		/// <summary>Returns a hash code for this <see cref="RGB"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RGB"/> structure.</returns>
		public override int GetHashCode()
		{
			return R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="RGB"/> structure equals another structure of the same type.</summary>
		/// <param name="other">An <see cref="RGB"/> structure.</param>
		/// <returns>Returns true if this <see cref="RGB"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( RGB other )
		{
			return ( R == other.R ) && ( G == other.G ) && ( B == other.B );
		}


		/// <summary>Returns a value indicating whether this <see cref="RGB"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is an <see cref="RGB"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RGB ) && this.Equals( (RGB)obj );
		}

		
		/// <summary>Returns a <see cref="Color"/> corresponding to this <see cref="RGB"/> color.</summary>
		/// <returns>Returns a <see cref="Color"/> corresponding to this <see cref="RGB"/> color.</returns>
		public Color ToColor()
		{
			var clamped = this;
			clamped.Saturate();
			
			return new Color( (byte)( clamped.R * 255.0f ), (byte)( clamped.G * 255.0f ), (byte)( clamped.B * 255.0f ), 255 );
		}

	
		/// <summary>Returns a <see cref="Vector3"/> corresponding to this <see cref="RGB"/> color.</summary>
		/// <returns>Returns a <see cref="Vector3"/> corresponding to this <see cref="RGB"/> color.</returns>
		public Vector3 ToVector3()
		{
			Vector3 result;
			result.X = R;
			result.Y = G;
			result.Z = B;
			return result;
		}


		/// <summary>Returns an array filled with the <see cref="R"/>, <see cref="G"/>, and <see cref="B"/> components of this <see cref="RGB"/> color.</summary>
		/// <returns>Returns an array filled with the <see cref="R"/>, <see cref="G"/>, and <see cref="B"/> components of this <see cref="RGB"/> color.</returns>
		public float[] ToArray()
		{
			return new float[] { R, G, B };
		}


		/// <summary>Returns a string representing this <see cref="RGB"/> color.</summary>
		/// <returns>Returns a string representing this <see cref="RGB"/> color.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{R:{0}, G:{1}, B:{2}}}", this.R, this.G, this.B );
		}



		/// <summary></summary>
		public static readonly RGB Black = new RGB( 0.0f, 0.0f, 0.0f );
		
		/// <summary></summary>
		public static readonly RGB White = new RGB( 1.0f, 1.0f, 1.0f );


		#region Static methods

		/// <summary>Calculates the opposite of an <see cref="RGB"/> color.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="result">Receives the opposite of the specified <see cref="RGB"/> <paramref name="color"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Negate( ref RGB color, out RGB result )
		{
			result.R = -color.R;
			result.G = -color.G;
			result.B = -color.B;
		}

		/// <summary>Returns the opposite of an <see cref="RGB"/> color.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <returns>Returns the opposite of the specified <see cref="RGB"/> <paramref name="color"/>.</returns>
		public static RGB Negate( RGB color )
		{
			color.R = -color.R;
			color.G = -color.G;
			color.B = -color.B;
			return color;
		}


		/// <summary>Calculates the sum of two <see cref="RGB"/> colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <param name="result">Receives the result of the addition.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref RGB color, ref RGB other, out RGB result )
		{
			result.R = color.R + other.R;
			result.G = color.G + other.G;
			result.B = color.B + other.B;
		}

		/// <summary>Returns the sum of two <see cref="RGB"/> colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <returns>Returns the result of the addition.</returns>
		public static RGB Add( RGB color, RGB other )
		{
			color.R += other.R;
			color.G += other.G;
			color.B += other.B;
			return color;
		}

		/// <summary>Calculates the sum of an <see cref="RGB"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the addition.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref RGB color, float value, out RGB result )
		{
			result.R = color.R + value;
			result.G = color.G + value;
			result.B = color.B + value;
		}

		/// <summary>Returns the sum of an <see cref="RGB"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the addition.</returns>
		public static RGB Add( RGB color, float value )
		{
			color.R += value;
			color.G += value;
			color.B += value;
			return color;
		}


		/// <summary>Calculates the difference between two <see cref="RGB"/> colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <param name="result">Receives the result of the subtraction.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref RGB color, ref RGB other, out RGB result )
		{
			result.R = color.R - other.R;
			result.G = color.G - other.G;
			result.B = color.B - other.B;
		}

		/// <summary>Returns the difference between two <see cref="RGB"/> colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static RGB Subtract( RGB color, RGB other )
		{
			color.R -= other.R;
			color.G -= other.G;
			color.B -= other.B;
			return color;
		}

		/// <summary>Calculates the difference between an <see cref="RGB"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the subtraction.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref RGB color, float value, out RGB result )
		{
			result.R = color.R - value;
			result.G = color.G - value;
			result.B = color.B - value;
		}

		/// <summary>Returns the difference between an <see cref="RGB"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static RGB Subtract( RGB color, float value )
		{
			color.R -= value;
			color.G -= value;
			color.B -= value;
			return color;
		}

		/// <summary>Calculates the difference between a value and an <see cref="RGB"/> color.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="result">Receives the result of the subtraction.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( float value, ref RGB color, out RGB result )
		{
			result.R = value - color.R;
			result.G = value - color.G;
			result.B = value - color.B;
		}

		/// <summary>Returns the difference between a value and an <see cref="RGB"/> color.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static RGB Subtract( float value, RGB color )
		{
			color.R = value - color.R;
			color.G = value - color.G;
			color.B = value - color.B;
			return color;
		}


		/// <summary>Calculates the product of two <see cref="RGB"/> colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <param name="result">Receives the result of the multiplication.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Multiply( ref RGB color, ref RGB other, out RGB result )
		{
			result.R = color.R * other.R;
			result.G = color.G * other.G;
			result.B = color.B * other.B;
		}

		/// <summary>Returns the product of two <see cref="RGB"/> colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static RGB Multiply( RGB color, RGB other )
		{
			color.R *= other.R;
			color.G *= other.G;
			color.B *= other.B;
			return color;
		}

		/// <summary>Calculates the product of an <see cref="RGB"/> color with a value.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the multiplication.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Multiply( ref RGB color, float value, out RGB result )
		{
			result.R = color.R * value;
			result.G = color.G * value;
			result.B = color.B * value;
		}

		/// <summary>Returns the product of an <see cref="RGB"/> color with a value.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static RGB Multiply( RGB color, float value )
		{
			color.R *= value;
			color.G *= value;
			color.B *= value;
			return color;
		}


		/// <summary>Calculates the ratio between two <see cref="RGB"/> colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <param name="result">Receives the result of the division.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( ref RGB color, ref RGB other, out RGB result )
		{
			result.R = color.R / other.R;
			result.G = color.G / other.G;
			result.B = color.B / other.B;
		}

		/// <summary>Returns the ratio between two <see cref="RGB"/> colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <returns>Returns the result of the division.</returns>
		public static RGB Divide( RGB color, RGB other )
		{
			color.R /= other.R;
			color.G /= other.G;
			color.B /= other.B;
			return color;
		}

		/// <summary>Calculates the ratio between an <see cref="RGB"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the division.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( ref RGB color, float value, out RGB result )
		{
			result.R = color.R / value;
			result.G = color.G / value;
			result.B = color.B / value;
		}

		/// <summary>Returns the ratio between an <see cref="RGB"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the division.</returns>
		public static RGB Divide( RGB color, float value )
		{
			color.R /= value;
			color.G /= value;
			color.B /= value;
			return color;
		}

		/// <summary>Calculates the ratio between a value and an <see cref="RGB"/> color.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="result">Receives the result of the division.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( float value, ref RGB color, out RGB result )
		{
			result.R = value / color.R;
			result.G = value / color.G;
			result.B = value / color.B;
		}

		/// <summary>Returns the ratio between a value and an <see cref="RGB"/> color.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <returns>Returns the result of the division.</returns>
		public static RGB Divide( float value, RGB color )
		{
			color.R = value / color.R;
			color.G = value / color.G;
			color.B = value / color.B;
			return color;
		}


		/// <summary>Retrieves an <see cref="RGB"/> color initialized with the minimum components between the two specified colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <param name="result">Receives an <see cref="RGB"/> color initialized with the minimum components between the two specified colors.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Min( ref RGB color, ref RGB other, out RGB result )
		{
			result.R = Math.Min( color.R, other.R );
			result.G = Math.Min( color.G, other.G );
			result.B = Math.Min( color.B, other.B );
		}

		/// <summary>Returns an <see cref="RGB"/> color initialized with the minimum components between the two specified colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <returns>Returns an <see cref="RGB"/> color initialized with the minimum components between the two specified colors.</returns>
		public static RGB Min( RGB color, RGB other )
		{
			RGB result;
			result.R = Math.Min( color.R, other.R );
			result.G = Math.Min( color.G, other.G );
			result.B = Math.Min( color.B, other.B );
			return result;
		}


		/// <summary>Retrieves an <see cref="RGB"/> color initialized with the maximum components between the two specified colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <param name="result">Receives an <see cref="RGB"/> color initialized with the maximum components between the two specified colors.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Max( ref RGB color, ref RGB other, out RGB result )
		{
			result.R = Math.Max( color.R, other.R );
			result.G = Math.Max( color.G, other.G );
			result.B = Math.Max( color.B, other.B );
		}

		/// <summary>Returns an <see cref="RGB"/> color initialized with the maximum components between the two specified colors.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <returns>Returns an <see cref="RGB"/> color initialized with the maximum components between the two specified colors.</returns>
		public static RGB Max( RGB color, RGB other )
		{
			RGB result;
			result.R = Math.Max( color.R, other.R );
			result.G = Math.Max( color.G, other.G );
			result.B = Math.Max( color.B, other.B );
			return result;
		}


		/// <summary>Performs a linear interpolation between two <see cref="RGB"/> colors.</summary>
		/// <param name="source">The source <see cref="RGB"/> color.</param>
		/// <param name="target">The target <see cref="RGB"/> color.</param>
		/// <param name="amount">The weighting factor; should be within the range [0,1].</param>
		/// <param name="result">Receives the result of the interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Lerp( ref RGB source, ref RGB target, float amount, out RGB result )
		{
			result.R = source.R + ( target.R - source.R ) * amount;
			result.G = source.G + ( target.G - source.G ) * amount;
			result.B = source.B + ( target.B - source.B ) * amount;
		}

		/// <summary>Performs a linear interpolation between two <see cref="RGB"/> colors.</summary>
		/// <param name="source">The source <see cref="RGB"/> color.</param>
		/// <param name="target">The target <see cref="RGB"/> color.</param>
		/// <param name="amount">The weighting factor; should be within the range [0,1].</param>
		/// <returns>Returns the result of the interpolation.</returns>
		public static RGB Lerp( RGB source, RGB target, float amount )
		{
			RGB result;
			result.R = source.R + ( target.R - source.R ) * amount;
			result.G = source.G + ( target.G - source.G ) * amount;
			result.B = source.B + ( target.B - source.B ) * amount;
			return result;
		}


		/// <summary>Performs a cubic interpolation between two <see cref="RGB"/> colors.</summary>
		/// <param name="source">The source <see cref="RGB"/> color.</param>
		/// <param name="target">The target <see cref="RGB"/> color.</param>
		/// <param name="amount">The weighting factor, within the range [0,1].</param>
		/// <param name="result">Receives the result of the interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void SmoothStep( ref RGB source, ref RGB target, float amount, out RGB result )
		{
			amount = amount * amount * ( 3.0f - 2.0f * amount );
			result.R = source.R + ( target.R - source.R ) * amount;
			result.G = source.G + ( target.G - source.G ) * amount;
			result.B = source.B + ( target.B - source.B ) * amount;
		}

		/// <summary>Performs a cubic interpolation between two <see cref="RGB"/> colors.</summary>
		/// <param name="source">The source <see cref="RGB"/> color.</param>
		/// <param name="target">The target <see cref="RGB"/> color.</param>
		/// <param name="amount">The weighting factor, within the range [0,1].</param>
		/// <returns>Returns the result of the interpolation.</returns>
		public static RGB SmoothStep( RGB source, RGB target, float amount )
		{
			amount = amount * amount * ( 3.0f - 2.0f * amount );
			source.R += ( target.R - source.R ) * amount;
			source.G += ( target.G - source.G ) * amount;
			source.B += ( target.B - source.B ) * amount;
			return source;
		}

		#endregion Static methods


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="rgb">An <see cref="RGB"/> structure.</param>
		/// <param name="other">An <see cref="RGB"/> structure.</param>
		/// <returns>Returns true if the <see cref="RGB"/> structures are equal, otherwise returns false.</returns>
		public static bool operator ==( RGB rgb, RGB other )
		{
			return rgb.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="rgb">An <see cref="RGB"/> structure.</param>
		/// <param name="other">An <see cref="RGB"/> structure.</param>
		/// <returns>Returns true if the <see cref="RGB"/> structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( RGB rgb, RGB other )
		{
			return !rgb.Equals( other );
		}

		

		/// <summary>Unary negation operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <returns></returns>
		public static RGB operator -( RGB color )
		{
			color.R = -color.R;
			color.G = -color.G;
			color.B = -color.B;
			return color;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <returns></returns>
		public static RGB operator +( RGB color, RGB other )
		{
			color.R += other.R;
			color.G += other.G;
			color.B += other.B;
			return color;
		}

		/// <summary>Addition operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns></returns>
		public static RGB operator +( RGB color, float value )
		{
			color.R += value;
			color.G += value;
			color.B += value;
			return color;
		}

		/// <summary>Addition operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <returns></returns>
		public static RGB operator +( float value, RGB color )
		{
			color.R += value;
			color.G += value;
			color.B += value;
			return color;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <returns></returns>
		public static RGB operator -( RGB color, RGB other )
		{
			color.R -= other.R;
			color.G -= other.G;
			color.B -= other.B;
			return color;
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns></returns>
		public static RGB operator -( RGB color, float value )
		{
			color.R -= value;
			color.G -= value;
			color.B -= value;
			return color;
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <returns></returns>
		public static RGB operator -( float value, RGB color )
		{
			color.R = value - color.R;
			color.G = value - color.G;
			color.B = value - color.B;
			return color;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <returns></returns>
		public static RGB operator *( RGB color, RGB other )
		{
			color.R *= other.R;
			color.G *= other.G;
			color.B *= other.B;
			return color;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns></returns>
		public static RGB operator *( RGB color, float value )
		{
			color.R *= value;
			color.G *= value;
			color.B *= value;
			return color;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <returns></returns>
		public static RGB operator *( float value, RGB color )
		{
			color.R *= value;
			color.G *= value;
			color.B *= value;
			return color;
		}


		/// <summary>Division operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="other">An <see cref="RGB"/> color.</param>
		/// <returns></returns>
		public static RGB operator /( RGB color, RGB other )
		{
			color.R /= other.R;
			color.G /= other.G;
			color.B /= other.B;
			return color;
		}

		/// <summary>Division operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns></returns>
		public static RGB operator /( RGB color, float value )
		{
			color.R /= value;
			color.G /= value;
			color.B /= value;
			return color;
		}

		/// <summary>Division operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <returns></returns>
		public static RGB operator /( float value, RGB color )
		{
			color.R = value / color.R;
			color.G = value / color.G;
			color.B = value / color.B;
			return color;
		}



		/// <summary><see cref="RGB"/> to <see cref="Color"/> conversion operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <returns></returns>
		public static explicit operator Color( RGB color )
		{
			color.Saturate();
			return new Color( (byte)( color.R * 255.0f ), (byte)( color.G * 255.0f ), (byte)( color.B * 255.0f ), 255 );
		}

		/// <summary><see cref="Color"/> to <see cref="RGB"/> conversion operator.</summary>
		/// <param name="color">A <see cref="Color"/>.</param>
		/// <returns></returns>
		public static explicit operator RGB( Color color )
		{
			RGB result;
			result.R = (float)color.R / 255.0f;
			result.G = (float)color.G / 255.0f;
			result.B = (float)color.B / 255.0f;
			return result;
		}


		/// <summary><see cref="RGB"/> to <see cref="Vector3"/> conversion operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <returns>Returns a <see cref="Vector3"/> initialized with the specified <paramref name="color"/>.</returns>
		public static explicit operator Vector3( RGB color )
		{
			Vector3 result;
			result.X = color.R;
			result.Y = color.G;
			result.Z = color.B;
			return result;
		}

		/// <summary><see cref="Vector3"/> to <see cref="RGB"/> conversion operator.</summary>
		/// <param name="vector">A <see cref="Vector3"/>.</param>
		/// <returns>Returns an <see cref="RGB"/> structure initialized with the specified <paramref name="vector"/>.</returns>
		public static explicit operator RGB( Vector3 vector )
		{
			RGB result;
			result.R = vector.X;
			result.G = vector.Y;
			result.B = vector.Z;
			return result;
		}


		//public static explicit operator float[]( RGB color )
		//{
		//	return new float[] { color.R, color.G, color.B };
		//}

		//public static explicit operator RGB( float[] components )
		//{
		//	if( components == null )
		//		throw new ArgumentNullException( "components" );
			
		//	if( components.Length < 3 )
		//		throw new InvalidCastException( "Not enough components." );

		//	RGB result;
		//	result.R = components[ 0 ];
		//	result.G = components[ 1 ];
		//	result.B = components[ 2 ];
		//	return result;
		//}

		#endregion Operators

	}

}