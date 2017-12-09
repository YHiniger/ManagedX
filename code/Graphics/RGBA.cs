using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{
	using Win32;


	/// <summary>Represents an RGBA color.
	/// <para>This structure is equivalent to the native structure <code>D3DCOLORVALUE</code>, and its aliases <code>D2D_COLOR_F</code> and <code>DXGI_RGBA</code>.</para>
	/// </summary>
	/// <remarks>
	/// https://msdn.microsoft.com/en-us/library/windows/desktop/dd368193%28v=vs.85%29.aspx
	/// https://msdn.microsoft.com/en-us/library/windows/desktop/dd368175%28v=vs.85%29.aspx
	/// https://msdn.microsoft.com/en-us/library/windows/desktop/hh404524%28v=vs.85%29.aspx
	/// https://msdn.microsoft.com/en-us/library/windows/desktop/dd318405(v=vs.85).aspx
	/// </remarks>
	[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RGBA" )]
	[Source( "D3D9Types.h", "D3DCOLORVALUE" )]
	[Source( "D2DBaseTypes.h", "D2D_COLOR_F" )]
	[Source( "DXGIType.h", "DXGI_RGBA" )]
	[Source( "D3D11.h", "D3D11_VIDEO_COLOR_RGBA" )]
	[Source( "DXVAHD.h", "DXVAHD_COLOR_RGBA" )]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct RGBA : IEquatable<RGBA>
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

		/// <summary>The alpha component of the color, also called "opacity"; should be within the range [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public float A;



		#region Constructors

		/// <summary>Initializes a new <see cref="RGBA"/> structure.</summary>
		/// <param name="red">The value of the red component.</param>
		/// <param name="green">The value of the green component.</param>
		/// <param name="blue">The value of the blue component.</param>
		/// <param name="alpha">The value of the alpha component.</param>
		public RGBA( float red, float green, float blue, float alpha )
		{
			R = red;
			G = green;
			B = blue;
			A = alpha;
		}

		/// <summary>Initializes a new <see cref="RGBA"/> structure.</summary>
		/// <param name="red">The value of the red component.</param>
		/// <param name="green">The value of the green component.</param>
		/// <param name="blue">The value of the blue component.</param>
		public RGBA( float red, float green, float blue )
		{
			R = red;
			G = green;
			B = blue;
			A = 1.0f;
		}


		/// <summary>Initializes a new <see cref="RGBA"/> structure from an <see cref="RGB"/> and an alpha value.</summary>
		/// <param name="rgb">An <see cref="RGB"/> color.</param>
		/// <param name="alpha">The alpha (opacity) value.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rgb" )]
		public RGBA( RGB rgb, float alpha )
		{
			R = rgb.R;
			G = rgb.G;
			B = rgb.B;
			A = alpha;
		}

		/// <summary>Initializes a new <see cref="RGBA"/> structure from an <see cref="RGB"/>.</summary>
		/// <param name="rgb">An <see cref="RGB"/> color.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rgb" )]
		public RGBA( RGB rgb )
		{
			R = rgb.R;
			G = rgb.G;
			B = rgb.B;
			A = 1.0f;
		}


		/// <summary>Initializes a new <see cref="RGBA"/> structure from a <see cref="Vector4"/>.</summary>
		/// <param name="rgba">A <see cref="Vector4"/> representing the color.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rgba" )]
		public RGBA( Vector4 rgba )
		{
			R = rgba.X;
			G = rgba.Y;
			B = rgba.Z;
			A = rgba.W;
		}


		/// <summary>Initializes a new <see cref="RGBA"/> structure from a <see cref="Vector3"/>.</summary>
		/// <param name="rgb">A <see cref="Vector3"/> representing the color.</param>
		/// <param name="alpha">The opacity (alpha) of the color.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rgb" )]
		public RGBA( Vector3 rgb, float alpha )
		{
			R = rgb.X;
			G = rgb.Y;
			B = rgb.Z;
			A = alpha;
		}

		/// <summary>Initializes a new <see cref="RGBA"/> structure from a <see cref="Vector3"/>.</summary>
		/// <param name="rgb">A <see cref="Vector3"/> representing the color.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rgb" )]
		public RGBA( Vector3 rgb )
		{
			R = rgb.X;
			G = rgb.Y;
			B = rgb.Z;
			A = 1.0f;
		}

		#endregion Constructors



		/// <summary>Normalizes this <see cref="RGBA"/> color, so that its components are forced within the range [0,1] while preserving the ratios.</summary>
		public void Normalize()
		{
			if( R < 0.0f )
				R = 0.0f;

			if( G < 0.0f )
				G = 0.0f;

			if( B < 0.0f )
				B = 0.0f;

			if( A < 0.0f )
				A = 0.0f;
			else if( A > 1.0f )
				A = 1.0f;

			var max = Math.Max( Math.Max( R, G ), B );
			if( max > 1.0f )
			{
				R /= max;
				G /= max;
				B /= max;
			}
		}


		/// <summary>Forces the component of this <see cref="RGBA"/> color within the valid range [0,1].</summary>
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

			if( A < 0.0f )
				A = 0.0f;
			else if( A > 1.0f )
				A = 1.0f;
		}

		
		/// <summary>Returns a hash code for this <see cref="RGBA"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RGBA"/> structure.</returns>
		public override int GetHashCode()
		{
			return R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode() ^ A.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="RGBA"/> structure equals another structure of the same type.</summary>
		/// <param name="other">An <see cref="RGBA"/> structure.</param>
		/// <returns>Returns true if this <see cref="RGBA"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( RGBA other )
		{
			return ( R == other.R ) && ( G == other.G ) && ( B == other.B ) && ( A == other.A );
		}


		/// <summary>Returns a value indicating whether this <see cref="RGBA"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is an <see cref="RGBA"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is RGBA rgba && this.Equals( rgba );
		}


		/// <summary>Returns a <see cref="Color"/> corresponding to this <see cref="RGB"/> color.</summary>
		/// <param name="preMultiply"></param>
		/// <returns>Returns a <see cref="Color"/> corresponding to this <see cref="RGB"/> color.</returns>
		public Color ToColor( bool preMultiply )
		{
			RGBA rgba;
			if( preMultiply )
			{
				rgba.R = R * A;
				rgba.G = G * A;
				rgba.B = B * A;
				rgba.A = A;
			}
			else
			{
				rgba.R = R;
				rgba.G = G;
				rgba.B = B;
				rgba.A = A;
			}
			rgba.Normalize();

			return new Color( (byte)( rgba.R * 255.0f ), (byte)( rgba.G * 255.0f ), (byte)( rgba.B * 255.0f ), (byte)( rgba.A * 255.0f ) );
		}


        /// <summary>Returns an <see cref="RGB"/> color corresponding to this <see cref="RGBA"/> color.</summary>
        /// <param name="preMultiply">Indicates whether the returned <see cref="RGB"/> color is multiplied by the opacity factor (alpha, see <see cref="A"/>).</param>
        /// <returns>Returns an <see cref="RGB"/> color corresponding to this <see cref="RGBA"/> color.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RGB")]
        public RGB ToRGB( bool preMultiply )
		{
			RGB result;
			if( !preMultiply )
			{
				result.R = R;
				result.G = G;
				result.B = B;
			}
			else
			{
				result.R = R * A;
				result.G = G * A;
				result.B = B * A;
			}
			return result;
		}


		/// <summary>Returns a <see cref="Vector3"/> corresponding to this <see cref="RGBA"/> color.</summary>
		/// <param name="preMultiply">Indicates whether the returned <see cref="Vector3"/> is multiplied by the alpha component (see <see cref="A"/>).</param>
		/// <returns>Returns a <see cref="Vector3"/> corresponding to this <see cref="RGBA"/> color.</returns>
		public Vector3 ToVector3( bool preMultiply )
		{
			Vector3 result;
			if( !preMultiply )
			{
				result.X = R;
				result.Y = G;
				result.Z = B;
			}
			else
			{
				result.X = R * A;
				result.Y = G * A;
				result.Z = B * A;
			}
			return result;
		}


		/// <summary>Returns a <see cref="Vector4"/> corresponding to this <see cref="RGBA"/> color.</summary>
		/// <returns>Returns a <see cref="Vector4"/> corresponding to this <see cref="RGBA"/> color.</returns>
		public Vector4 ToVector4()
		{
			Vector4 result;
			result.X = R;
			result.Y = G;
			result.Z = B;
			result.W = A;
			return result;
		}


		/// <summary>Returns an array filled with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> components of this <see cref="RGBA"/> color.</summary>
		/// <returns>Returns an array filled with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> components of this <see cref="RGBA"/> color.</returns>
		public float[] ToArray()
		{
			return new float[] { R, G, B, A };
		}


		/// <summary>Returns a string representing this <see cref="RGBA"/> color.</summary>
		/// <returns>Returns a string representing this <see cref="RGBA"/> color.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{R:{0}, G:{1}, B:{2}, A:{3}}}", this.R, this.G, this.B, this.A );
		}


		/// <summary>An <see cref="RGBA"/> structure whose components are set to 0.</summary>
		public static readonly RGBA Transparent = new RGBA( 0.0f, 0.0f, 0.0f, 0.0f );

		/// <summary>An <see cref="RGBA"/> structure whose components are set to 0, except the opacity which is set to 1.</summary>
		public static readonly RGBA Black = new RGBA( 0.0f, 0.0f, 0.0f, 1.0f );

		/// <summary>An <see cref="RGBA"/> structure whose components are set to 1.</summary>
		public static readonly RGBA White = new RGBA( 1.0f, 1.0f, 1.0f, 1.0f );


		#region Static methods

		/// <summary>Calculates the opposite of an <see cref="RGBA"/> color.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="result">Receives the opposite of the specified <see cref="RGBA"/> <paramref name="color"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Negate( ref RGBA color, out RGBA result )
		{
			result.R = -color.R;
			result.G = -color.G;
			result.B = -color.B;
			result.A = -color.A;
		}

		/// <summary>Returns the opposite of an <see cref="RGBA"/> color.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns the opposite of the specified <see cref="RGBA"/> <paramref name="color"/>.</returns>
		public static RGBA Negate( RGBA color )
		{
			color.R = -color.R;
			color.G = -color.G;
			color.B = -color.B;
			color.A = -color.A;
			return color;
		}


		/// <summary>Calculates the sum of two <see cref="RGBA"/> colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <param name="result">Receives the result of the addition.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref RGBA color, ref RGBA other, out RGBA result )
		{
			result.R = color.R + other.R;
			result.G = color.G + other.G;
			result.B = color.B + other.B;
			result.A = color.A + other.A;
		}

		/// <summary>Returns the sum of two <see cref="RGBA"/> colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns the result of the addition.</returns>
		public static RGBA Add( RGBA color, RGBA other )
		{
			color.R += other.R;
			color.G += other.G;
			color.B += other.B;
			color.A += other.A;
			return color;
		}

		/// <summary>Calculates the sum of an <see cref="RGBA"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the addition.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref RGBA color, float value, out RGBA result )
		{
			result.R = color.R + value;
			result.G = color.G + value;
			result.B = color.B + value;
			result.A = color.A + value;
		}

		/// <summary>Returns the sum of an <see cref="RGBA"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the addition.</returns>
		public static RGBA Add( RGBA color, float value )
		{
			color.R += value;
			color.G += value;
			color.B += value;
			color.A += value;
			return color;
		}


		/// <summary>Calculates the difference between two <see cref="RGBA"/> colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <param name="result">Receives the result of the subtraction.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref RGBA color, ref RGBA other, out RGBA result )
		{
			result.R = color.R - other.R;
			result.G = color.G - other.G;
			result.B = color.B - other.B;
			result.A = color.A - other.A;
		}

		/// <summary>Returns the difference between two <see cref="RGBA"/> colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static RGBA Subtract( RGBA color, RGBA other )
		{
			color.R -= other.R;
			color.G -= other.G;
			color.B -= other.B;
			color.A -= other.A;
			return color;
		}

		/// <summary>Calculates the difference between an <see cref="RGBA"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the subtraction.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref RGBA color, float value, out RGBA result )
		{
			result.R = color.R - value;
			result.G = color.G - value;
			result.B = color.B - value;
			result.A = color.A - value;
		}

		/// <summary>Returns the difference between an <see cref="RGBA"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static RGBA Subtract( RGBA color, float value )
		{
			color.R -= value;
			color.G -= value;
			color.B -= value;
			color.A -= value;
			return color;
		}

		/// <summary>Calculates the difference between a value and an <see cref="RGBA"/> color.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="result">Receives the result of the subtraction.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( float value, ref RGBA color, out RGBA result )
		{
			result.R = value - color.R;
			result.G = value - color.G;
			result.B = value - color.B;
			result.A = value - color.A;
		}

		/// <summary>Returns the difference between a value and an <see cref="RGBA"/> color.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static RGBA Subtract( float value, RGBA color )
		{
			color.R = value - color.R;
			color.G = value - color.G;
			color.B = value - color.B;
			color.A = value - color.A;
			return color;
		}


		/// <summary>Calculates the product of two <see cref="RGBA"/> colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <param name="result">Receives the result of the multiplication.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Multiply( ref RGBA color, ref RGBA other, out RGBA result )
		{
			result.R = color.R * other.R;
			result.G = color.G * other.G;
			result.B = color.B * other.B;
			result.A = color.A * other.A;
		}

		/// <summary>Returns the product of two <see cref="RGBA"/> colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static RGBA Multiply( RGBA color, RGBA other )
		{
			color.R *= other.R;
			color.G *= other.G;
			color.B *= other.B;
			color.A *= other.A;
			return color;
		}

		/// <summary>Calculates the product of an <see cref="RGBA"/> color with a value.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the multiplication.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Multiply( ref RGBA color, float value, out RGBA result )
		{
			result.R = color.R * value;
			result.G = color.G * value;
			result.B = color.B * value;
			result.A = color.A * value;
		}

		/// <summary>Returns the product of an <see cref="RGBA"/> color with a value.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static RGBA Multiply( RGBA color, float value )
		{
			color.R *= value;
			color.G *= value;
			color.B *= value;
			color.A *= value;
			return color;
		}


		/// <summary>Calculates the ratio between two <see cref="RGBA"/> colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <param name="result">Receives the result of the division.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( ref RGBA color, ref RGBA other, out RGBA result )
		{
			result.R = color.R / other.R;
			result.G = color.G / other.G;
			result.B = color.B / other.B;
			result.A = color.A / other.A;
		}

		/// <summary>Returns the ratio between two <see cref="RGBA"/> colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns the result of the division.</returns>
		public static RGBA Divide( RGBA color, RGBA other )
		{
			color.R /= other.R;
			color.G /= other.G;
			color.B /= other.B;
			color.A /= other.A;
			return color;
		}

		/// <summary>Calculates the ratio between an <see cref="RGBA"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the division.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( ref RGBA color, float value, out RGBA result )
		{
			result.R = color.R / value;
			result.G = color.G / value;
			result.B = color.B / value;
			result.A = color.A / value;
		}

		/// <summary>Returns the ratio between an <see cref="RGBA"/> color and a value.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the division.</returns>
		public static RGBA Divide( RGBA color, float value )
		{
			color.R /= value;
			color.G /= value;
			color.B /= value;
			color.A /= value;
			return color;
		}

		/// <summary>Calculates the ratio between a value and an <see cref="RGBA"/> color.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="result">Receives the result of the division.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( float value, ref RGBA color, out RGBA result )
		{
			result.R = value / color.R;
			result.G = value / color.G;
			result.B = value / color.B;
			result.A = value / color.A;
		}

		/// <summary>Returns the ratio between a value and an <see cref="RGBA"/> color.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns the result of the division.</returns>
		public static RGBA Divide( float value, RGBA color )
		{
			color.R = value / color.R;
			color.G = value / color.G;
			color.B = value / color.B;
			color.A = value / color.A;
			return color;
		}


		/// <summary>Retrieves an <see cref="RGBA"/> color initialized with the minimum components between the two specified colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <param name="result">Receives an <see cref="RGBA"/> color initialized with the minimum components between the two specified colors.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Min( ref RGBA color, ref RGBA other, out RGBA result )
		{
			result.R = Math.Min( color.R, other.R );
			result.G = Math.Min( color.G, other.G );
			result.B = Math.Min( color.B, other.B );
			result.A = Math.Min( color.A, other.A );
		}

		/// <summary>Returns an <see cref="RGBA"/> color initialized with the minimum components between the two specified colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns an <see cref="RGBA"/> color initialized with the minimum components between the two specified colors.</returns>
		public static RGBA Min( RGBA color, RGBA other )
		{
			RGBA result;
			result.R = Math.Min( color.R, other.R );
			result.G = Math.Min( color.G, other.G );
			result.B = Math.Min( color.B, other.B );
			result.A = Math.Min( color.A, other.A );
			return result;
		}


		/// <summary>Retrieves an <see cref="RGBA"/> color initialized with the maximum components between the two specified colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <param name="result">Receives an <see cref="RGBA"/> color initialized with the maximum components between the two specified colors.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Max( ref RGBA color, ref RGBA other, out RGBA result )
		{
			result.R = Math.Max( color.R, other.R );
			result.G = Math.Max( color.G, other.G );
			result.B = Math.Max( color.B, other.B );
			result.A = Math.Max( color.A, other.A );
		}

		/// <summary>Returns an <see cref="RGBA"/> color initialized with the maximum components between the two specified colors.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns an <see cref="RGBA"/> color initialized with the maximum components between the two specified colors.</returns>
		public static RGBA Max( RGBA color, RGBA other )
		{
			RGBA result;
			result.R = Math.Max( color.R, other.R );
			result.G = Math.Max( color.G, other.G );
			result.B = Math.Max( color.B, other.B );
			result.A = Math.Max( color.A, other.A );
			return result;
		}


		/// <summary>Performs a linear interpolation between two <see cref="RGBA"/> colors.</summary>
		/// <param name="source">The source <see cref="RGBA"/> color.</param>
		/// <param name="target">The target <see cref="RGBA"/> color.</param>
		/// <param name="amount">The weighting factor; should be within the range [0,1].</param>
		/// <param name="result">Receives the result of the interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Lerp( ref RGBA source, ref RGBA target, float amount, out RGBA result )
		{
			result.R = source.R + ( target.R - source.R ) * amount;
			result.G = source.G + ( target.G - source.G ) * amount;
			result.B = source.B + ( target.B - source.B ) * amount;
			result.A = source.A + ( target.A - source.A ) * amount;
		}

		/// <summary>Performs a linear interpolation between two <see cref="RGBA"/> colors.</summary>
		/// <param name="source">The source <see cref="RGBA"/> color.</param>
		/// <param name="target">The target <see cref="RGBA"/> color.</param>
		/// <param name="amount">The weighting factor; should be within the range [0,1].</param>
		/// <returns>Returns the result of the interpolation.</returns>
		public static RGBA Lerp( RGBA source, RGBA target, float amount )
		{
			RGBA result;
			result.R = source.R + ( target.R - source.R ) * amount;
			result.G = source.G + ( target.G - source.G ) * amount;
			result.B = source.B + ( target.B - source.B ) * amount;
			result.A = source.A + ( target.A - source.A ) * amount;
			return result;
		}


		/// <summary>Performs a cubic interpolation between two <see cref="RGBA"/> colors.</summary>
		/// <param name="source">The source <see cref="RGBA"/> color.</param>
		/// <param name="target">The target <see cref="RGBA"/> color.</param>
		/// <param name="amount">The weighting factor, within the range [0,1].</param>
		/// <param name="result">Receives the result of the interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void SmoothStep( ref RGBA source, ref RGBA target, float amount, out RGBA result )
		{
			amount = amount * amount * ( 3.0f - 2.0f * amount );
			result.R = source.R + ( target.R - source.R ) * amount;
			result.G = source.G + ( target.G - source.G ) * amount;
			result.B = source.B + ( target.B - source.B ) * amount;
			result.A = source.A + ( target.A - source.A ) * amount;
		}

		/// <summary>Performs a cubic interpolation between two <see cref="RGBA"/> colors.</summary>
		/// <param name="source">The source <see cref="RGBA"/> color.</param>
		/// <param name="target">The target <see cref="RGBA"/> color.</param>
		/// <param name="amount">The weighting factor, within the range [0,1].</param>
		/// <returns>Returns the result of the interpolation.</returns>
		public static RGBA SmoothStep( RGBA source, RGBA target, float amount )
		{
			amount = amount * amount * ( 3.0f - 2.0f * amount );
			source.R += ( target.R - source.R ) * amount;
			source.G += ( target.G - source.G ) * amount;
			source.B += ( target.B - source.B ) * amount;
			source.A += ( target.A - source.A ) * amount;
			return source;
		}

		#endregion Static methods


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="color">An <see cref="RGBA"/> structure.</param>
		/// <param name="other">An <see cref="RGBA"/> structure.</param>
		/// <returns>Returns true if the <see cref="RGBA"/> structures are equal, otherwise returns false.</returns>
		public static bool operator ==( RGBA color, RGBA other )
		{
			return color.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="color">An <see cref="RGBA"/> structure.</param>
		/// <param name="other">An <see cref="RGBA"/> structure.</param>
		/// <returns>Returns true if the <see cref="RGBA"/> structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( RGBA color, RGBA other )
		{
			return !color.Equals( other );
		}


		/// <summary>Unary negation operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <returns></returns>
		public static RGBA operator -( RGBA color )
		{
			color.R = -color.R;
			color.G = -color.G;
			color.B = -color.B;
			color.A = -color.A;
			return color;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <returns></returns>
		public static RGBA operator +( RGBA color, RGBA other )
		{
			color.R += other.R;
			color.G += other.G;
			color.B += other.B;
			color.A += other.A;
			return color;
		}

		/// <summary>Addition operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns></returns>
		public static RGBA operator +( RGBA color, float value )
		{
			color.R += value;
			color.G += value;
			color.B += value;
			color.A += value;
			return color;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <returns></returns>
		public static RGBA operator -( RGBA color, RGBA other )
		{
			color.R -= other.R;
			color.G -= other.G;
			color.B -= other.B;
			color.A -= other.A;
			return color;
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns></returns>
		public static RGBA operator -( RGBA color, float value )
		{
			color.R -= value;
			color.G -= value;
			color.B -= value;
			color.A -= value;
			return color;
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <returns></returns>
		public static RGBA operator -( float value, RGBA color )
		{
			color.R = value - color.R;
			color.G = value - color.G;
			color.B = value - color.B;
			color.A = value - color.A;
			return color;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <returns></returns>
		public static RGBA operator *( RGBA color, RGBA other )
		{
			color.R *= other.R;
			color.G *= other.G;
			color.B *= other.B;
			color.A *= other.A;
			return color;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns></returns>
		public static RGBA operator *( RGBA color, float value )
		{
			color.R *= value;
			color.G *= value;
			color.B *= value;
			color.A *= value;
			return color;
		}


		/// <summary>Division operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="other">An <see cref="RGBA"/> color.</param>
		/// <returns></returns>
		public static RGBA operator /( RGBA color, RGBA other )
		{
			color.R /= other.R;
			color.G /= other.G;
			color.B /= other.B;
			color.A /= other.A;
			return color;
		}

		/// <summary>Division operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns></returns>
		public static RGBA operator /( RGBA color, float value )
		{
			color.R /= value;
			color.G /= value;
			color.B /= value;
			color.A /= value;
			return color;
		}

		/// <summary>Division operator.</summary>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <returns></returns>
		public static RGBA operator /( float value, RGBA color)
		{
			color.R = value / color.R;
			color.G = value / color.G;
			color.B = value / color.B;
			color.A = value / color.A;
			return color;
		}



		/// <summary><see cref="RGBA"/> to <see cref="Color"/> conversion operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns a <see cref="Color"/> corresponding to the specified <see cref="RGBA"/> <paramref name="color"/>.</returns>
		public static explicit operator Color( RGBA color )
		{
			return color.ToColor( false );
		}

		/// <summary><see cref="Color"/> to <see cref="RGBA"/> conversion operator.</summary>
		/// <param name="color">A <see cref="Color"/>.</param>
		/// <returns>Returns an <see cref="RGBA"/> color corresponding to the specified <paramref name="color"/>.</returns>
		public static explicit operator RGBA( Color color )
		{
			RGBA result;
			result.R = (float)color.R / 255.0f;
			result.G = (float)color.G / 255.0f;
			result.B = (float)color.B / 255.0f;
			result.A = (float)color.A / 255.0f;
			return result;
		}


		/// <summary><see cref="RGBA"/> to <see cref="Vector3"/> conversion operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns a <see cref="Vector3"/> initialized with the specified <paramref name="color"/>; the opacity (alpha, see <see cref="A"/>) is ignored.</returns>
		public static explicit operator Vector3( RGBA color )
		{
			Vector3 result;
			result.X = color.R;
			result.Y = color.G;
			result.Z = color.B;
			return result;
		}

		/// <summary><see cref="Vector3"/> to <see cref="RGBA"/> conversion operator.</summary>
		/// <param name="vector">A <see cref="Vector3"/>.</param>
		/// <returns>Returns an <see cref="RGBA"/> structure initialized with the specified <paramref name="vector"/>.</returns>
		public static explicit operator RGBA( Vector3 vector )
		{
			RGBA result;
			result.R = vector.X;
			result.G = vector.Y;
			result.B = vector.Z;
			result.A = 1.0f;
			return result;
		}


		/// <summary><see cref="RGBA"/> to <see cref="Vector4"/> conversion operator.</summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns a <see cref="Vector4"/> initialized with the specified <paramref name="color"/>.</returns>
		public static explicit operator Vector4( RGBA color )
		{
			Vector4 result;
			result.X = color.R;
			result.Y = color.G;
			result.Z = color.B;
			result.W = color.A;
			return result;
		}

		/// <summary><see cref="Vector4"/> to <see cref="RGBA"/> conversion operator.</summary>
		/// <param name="vector">A <see cref="Vector4"/>.</param>
		/// <returns>Returns an <see cref="RGBA"/> structure initialized with the specified <paramref name="vector"/>.</returns>
		public static explicit operator RGBA( Vector4 vector )
		{
			RGBA result;
			result.R = vector.X;
			result.G = vector.Y;
			result.B = vector.Z;
			result.A = vector.W;
			return result;
		}


		/// <summary><see cref="RGBA"/> to <see cref="RGB"/> conversion operator.
		/// <para>The opacity component (alpha) is ignored.</para>
		/// </summary>
		/// <param name="color">An <see cref="RGBA"/> color.</param>
		/// <returns>Returns a <see cref="Vector4"/> initialized with the specified <paramref name="color"/>.</returns>
		public static explicit operator RGB( RGBA color )
		{
			RGB result;
			result.R = color.R;
			result.G = color.G;
			result.B = color.B;
			return result;
		}

		/// <summary><see cref="RGB"/> to <see cref="RGBA"/> conversion operator.</summary>
		/// <param name="color">An <see cref="RGB"/> color.</param>
		/// <returns>Returns an <see cref="RGBA"/> structure initialized with the specified <see cref="RGB"/> <paramref name="color"/>.</returns>
		public static explicit operator RGBA( RGB color )
		{
			RGBA result;
			result.R = color.R;
			result.G = color.G;
			result.B = color.B;
			result.A = 1.0f;
			return result;
		}

		#endregion Operators

	}

}