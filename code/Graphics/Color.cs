using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{

	/// <summary>An ARGB (8 bits per component) color.</summary>
	[Win32.Source( "MFObjects.h", "MFARGB" )]
	[Serializable]
	[StructLayout( LayoutKind.Explicit, Pack = 4, Size = 4 )]
	unsafe public struct Color : IEquatable<Color>
	{

		[FieldOffset( 0 )]
		private uint bgra;

		/// <summary>The blue component of this <see cref="Color"/>.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B" )]
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[FieldOffset( 0 )]
		public byte B;

		/// <summary>The green component of this <see cref="Color"/>.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "G" )]
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[FieldOffset( 1 )]
		public byte G;

		/// <summary>The red component of this <see cref="Color"/>.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "R" )]
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[FieldOffset( 2 )]
		public byte R;

		/// <summary>The alpha component (opacity) of this <see cref="Color"/>.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A" )]
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[FieldOffset( 3 )]
		public byte A;


		
		#region Constructors

		/// <summary>Initializes a new <see cref="Color"/>.</summary>
		/// <param name="red">The red component of the color.</param>
		/// <param name="green">The green component of the color.</param>
		/// <param name="blue">The blue component of the color.</param>
		/// <param name="opacity">The opacity (alpha component) of the color.</param>
		public Color( byte red, byte green, byte blue, byte opacity )
		{
			bgra = 0;
			B = blue;
			G = green;
			R = red;
			A = opacity;
		}


		/// <summary>Initializes a new <see cref="Color"/>.</summary>
		/// <param name="bgra">The packed ARGB value.</param>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "bgra")]
        [CLSCompliant( false )]
		public Color( uint bgra )
		{
			A = R = G = B = 0;
			this.bgra = bgra;
		}

		#endregion Constructors



		/// <summary>Returns a <see cref="Vector3"/> representing this <see cref="Color"/>.</summary>
		/// <param name="preMultiply">Indicates whether to multiply the <see cref="R"/>, <see cref="G"/> and <see cref="B"/> components by the <see cref="A"/> value.</param>
		/// <returns>Returns a <see cref="Vector3"/> representing this <see cref="Color"/>.</returns>
		public Vector3 ToVector3( bool preMultiply )
		{
			Vector3 result;
			fixed ( uint* ptr = &bgra )
			{
				var ptr2 = (byte*)ptr;

				result.X = (float)ptr2[ 2 ] / 255.0f;
				result.Y = (float)ptr2[ 1 ] / 255.0f;
				result.Z = (float)ptr2[ 0 ] / 255.0f;
			}

			if( preMultiply )
			{
				var alpha = this.A / 255.0f;
				result.X *= alpha;
				result.Y *= alpha;
				result.Z *= alpha;
			}

			return result;
		}


		/// <summary>Returns a <see cref="Vector4"/> representing this <see cref="Color"/>.</summary>
		/// <param name="preMultiply">Indicates whether to multiply the <see cref="R"/>, <see cref="G"/> and <see cref="B"/> components by the alpha (see <see cref="A"/>) value.</param>
		/// <returns>Returns a <see cref="Vector3"/> representing this <see cref="Color"/>.</returns>
		public Vector4 ToVector4( bool preMultiply )
		{
			Vector4 result;
			fixed ( uint* ptr = &bgra )
			{
				var ptr2 = (byte*)ptr;

				result.X = (float)ptr2[ 2 ] / 255.0f;
				result.Y = (float)ptr2[ 1 ] / 255.0f;
				result.Z = (float)ptr2[ 0 ] / 255.0f;
				result.W = (float)ptr2[ 3 ] / 255.0f;
			}

			if( preMultiply )
			{
				result.X *= result.W;
				result.Y *= result.W;
				result.Z *= result.W;
			}

			return result;
		}


		/// <summary>Returns an array filled with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> components of this <see cref="Color"/>.</summary>
		/// <returns>Returns an array filled with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> components of this <see cref="Color"/>.</returns>
		public byte[] ToArray()
		{
			fixed ( uint* ptr = &bgra )
			{
				var ptr2 = (byte*)ptr;
				return new byte[] { ptr2[ 2 ], ptr2[ 1 ], ptr2[ 0 ], ptr2[ 3 ] };
			}
		}


		/// <summary>Returns a hash code for this <see cref="Color"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Color"/>.</returns>
		public override int GetHashCode()
		{
			return unchecked((int)bgra);
		}


		/// <summary>Returns a value indicating whether this <see cref="Color"/> equals another <see cref="Color"/>.</summary>
		/// <param name="other">A <see cref="Color"/>.</param>
		/// <returns>Returns true if this <see cref="Color"/> and the <paramref name="other"/> <see cref="Color"/> are equal, otherwise returns false.</returns>
		public bool Equals( Color other )
		{
			return ( bgra == other.bgra );
		}

		
		/// <summary>Returns a value indicating whether this <see cref="Color"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Color"/> which equals this <see cref="Color"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is Color c && this.Equals( c );
		}


		/// <summary>Returns a string representing this <see cref="Color"/>.</summary>
		/// <returns>Returns a string representing this <see cref="Color"/>.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{R:{0}, G:{1}, B:{2}, A:{3}}}", this.R, this.G, this.B, this.A );
		}


		/// <summary>Retrieves a <see cref="Color"/> initialized with the minimum value of each component between two <see cref="Color"/> structures.</summary>
		/// <param name="color">A <see cref="Color"/>.</param>
		/// <param name="other">A <see cref="Color"/>.</param>
		/// <param name="result">Receives a <see cref="Color"/> structure initialized with the minimum value of each component between the two specified <see cref="Color"/> structures.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Min( ref Color color, ref Color other, out Color result )
		{
			result = new Color( Math.Min( color.R, other.R ), Math.Min( color.G, other.G ), Math.Min( color.B, other.B ), Math.Min( color.A, other.A ) );
		}

		/// <summary>Returns a <see cref="Color"/> initialized with the minimum value of each component between two <see cref="Color"/> structures.</summary>
		/// <param name="color">A <see cref="Color"/>.</param>
		/// <param name="other">A <see cref="Color"/>.</param>
		/// <returns>Returns a <see cref="Color"/> initialized with the minimum value of each component between two <see cref="Color"/> structures.</returns>
		public static Color Min( Color color, Color other )
		{
			color.R = Math.Min( color.R, other.R );
			color.G = Math.Min( color.G, other.G );
			color.B = Math.Min( color.B, other.B );
			color.A = Math.Min( color.A, other.A );
			return color;
		}


		/// <summary>Retrieves a <see cref="Color"/> initialized with the maximum value of each component between two <see cref="Color"/> structures.</summary>
		/// <param name="color">A <see cref="Color"/>.</param>
		/// <param name="other">A <see cref="Color"/>.</param>
		/// <param name="result">Receives a <see cref="Color"/> structure initialized with the maximum value of each component between the two specified <see cref="Color"/> structures.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Max( ref Color color, ref Color other, out Color result )
		{
			result = new Color( Math.Max( color.R, other.R ), Math.Max( color.G, other.G ), Math.Max( color.B, other.B ), Math.Max( color.A, other.A ) );
		}

		/// <summary>Returns a <see cref="Color"/> initialized with the maximum value of each component between two <see cref="Color"/> structures.</summary>
		/// <param name="color">A <see cref="Color"/>.</param>
		/// <param name="other">A <see cref="Color"/>.</param>
		/// <returns>Returns a <see cref="Color"/> initialized with the maximum value of each component between two <see cref="Color"/> structures.</returns>
		public static Color Max( Color color, Color other )
		{
			color.R = Math.Max( color.R, other.R );
			color.G = Math.Max( color.G, other.G );
			color.B = Math.Max( color.B, other.B );
			color.A = Math.Max( color.A, other.A );
			return color;
		}


		/// <summary>Performs a linear interpolation between two <see cref="Color"/> structures.</summary>
		/// <param name="source">The source <see cref="Color"/>.</param>
		/// <param name="target">The target <see cref="Color"/>.</param>
		/// <param name="amount">The weighting factor, in the range [0,1].</param>
		/// <param name="result">Receives the result of the interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Lerp( ref Color source, ref Color target, float amount, out Color result )
		{
			var r = (float)source.R;
			var g = (float)source.G;
			var b = (float)source.B;
			var a = (float)source.A;

			r += ( (float)target.R - r ) * amount;
			g += ( (float)target.G - g ) * amount;
			b += ( (float)target.B - b ) * amount;
			a += ( (float)target.A - a ) * amount;

			result = new Color(
				(byte)r.Clamp( 0.0f, 255.0f ),
				(byte)g.Clamp( 0.0f, 255.0f ),
				(byte)b.Clamp( 0.0f, 255.0f ),
				(byte)a.Clamp( 0.0f, 255.0f )
			);
		}

		/// <summary>Returns the result of the linear interpolation between two <see cref="Color"/> structures.</summary>
		/// <param name="source">The source <see cref="Color"/>.</param>
		/// <param name="target">The target <see cref="Color"/>.</param>
		/// <param name="amount">The weighting factor, in the range [0,1].</param>
		/// <returns>Returns the result of the interpolation.</returns>
		public static Color Lerp( Color source, Color target, float amount )
		{
			var r = (float)source.R;
			var g = (float)source.G;
			var b = (float)source.B;
			var a = (float)source.A;

			r = r + ( (float)target.R - r ) * amount;
			g = g + ( (float)target.G - g ) * amount;
			b = b + ( (float)target.B - b ) * amount;
			a = a + ( (float)target.A - a ) * amount;

			return new Color(
				(byte)r.Clamp( 0.0f, 255.0f ),
				(byte)g.Clamp( 0.0f, 255.0f ),
				(byte)b.Clamp( 0.0f, 255.0f ),
				(byte)a.Clamp( 0.0f, 255.0f )
			);
		}


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="color">A <see cref="Color"/>.</param>
		/// <param name="other">A <see cref="Color"/>.</param>
		/// <returns>Returns true if the colors are equal, otherwise returns false.</returns>
		public static bool operator ==( Color color, Color other )
		{
			return color.bgra == other.bgra;
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="color">A <see cref="Color"/>.</param>
		/// <param name="other">A <see cref="Color"/>.</param>
		/// <returns>Returns true if the colors are not equal, otherwise returns false.</returns>
		public static bool operator !=( Color color, Color other )
		{
			return color.bgra != other.bgra;
		}

		#endregion Operators

	}

}