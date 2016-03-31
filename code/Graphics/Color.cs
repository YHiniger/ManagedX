using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{
	
	/// <summary>An RGBA color (8 bits per component).</summary>
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 4 )]
	public unsafe struct Color : IEquatable<Color>
	{

		private uint rgba;


		
		#region Constructors

		/// <summary>Initializes a new <see cref="Color"/>.</summary>
		/// <param name="red">The red component of the color.</param>
		/// <param name="green">The green component of the color.</param>
		/// <param name="blue">The blue component of the color.</param>
		/// <param name="opacity">The opacity (alpha component) of the color.</param>
		public Color( byte red, byte green, byte blue, byte opacity )
		{
			fixed( void* ptr = &rgba )
			{
				var b = (byte*)ptr;
				b[ 0 ] = red;
				b[ 1 ] = green;
				b[ 2 ] = blue;
				b[ 3 ] = opacity;
			}
		}


        /// <summary>Initializes a new <see cref="Color"/>.</summary>
        /// <param name="rgba">The packed RGBA value.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rgba")]
        [CLSCompliant( false )]
		public Color( uint rgba )
		{
			this.rgba = rgba;
		}

		#endregion Constructors



		/// <summary>Gets or sets the red component of this <see cref="Color"/>.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public byte R
		{
			get
			{
				fixed( void* ptr = &rgba )
					return *(byte*)ptr;
			}
			set
			{
				fixed( void* ptr = &rgba )
					*(byte*)ptr = value;
			}
		}


		/// <summary>Gets or sets the green component of this <see cref="Color"/>.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public byte G
		{
			get
			{
				fixed( void* ptr = &rgba )
					return *( (byte*)ptr + 1 );
			}
			set
			{
				fixed( void* ptr = &rgba )
					*( (byte*)ptr + 1 ) = value;
			}
		}

		
		/// <summary>Gets or sets the blue component of this <see cref="Color"/>.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public byte B
		{
			get
			{
				fixed( void* ptr = &rgba )
					return *( (byte*)ptr + 2 );
			}
			set
			{
				fixed( void* ptr = &rgba )
					*( (byte*)ptr + 2 ) = value;
			}
		}


		/// <summary>Gets or sets the alpha component (opacity) of this <see cref="Color"/>.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public byte A
		{
			get
			{
				fixed( void* ptr = &rgba )
					return *( (byte*)ptr + 3 );
			}
			set
			{
				fixed( void* ptr = &rgba )
					*( (byte*)ptr + 3 ) = value;
			}
		}


		/// <summary>Returns a <see cref="Vector3"/> representing this <see cref="Color"/>.</summary>
		/// <param name="preMultiply">Indicates whether to multiply the <see cref="R"/>, <see cref="G"/> and <see cref="B"/> components by the alpha (see <see cref="A"/>) value.</param>
		/// <returns>Returns a <see cref="Vector3"/> representing this <see cref="Color"/>.</returns>
		public Vector3 ToVector3( bool preMultiply )
		{
			Vector3 result;
			result.X = (float)this.R / 255.0f;
			result.Y = (float)this.G / 255.0f;
			result.Z = (float)this.B / 255.0f;

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
			result.X = (float)this.R / 255.0f;
			result.Y = (float)this.G / 255.0f;
			result.Z = (float)this.B / 255.0f;
			result.W = (float)this.A / 255.0f;

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
			return new byte[] { this.R, this.G, this.B, this.A };
		}


		/// <summary>Returns a hash code for this <see cref="Color"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Color"/>.</returns>
		public override int GetHashCode()
		{
			return unchecked( (int)rgba );
		}


		/// <summary>Returns a value indicating whether this <see cref="Color"/> equals another <see cref="Color"/>.</summary>
		/// <param name="other">A <see cref="Color"/>.</param>
		/// <returns>Returns true if this <see cref="Color"/> and the <paramref name="other"/> <see cref="Color"/> are equal, otherwise returns false.</returns>
		public bool Equals( Color other )
		{
			return ( rgba == other.rgba );
		}

		
		/// <summary>Returns a value indicating whether this <see cref="Color"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Color"/> which equals this <see cref="Color"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Color ) && this.Equals( (Color)obj );
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
		public static void Min( ref Color color, ref Color other, ref Color result )
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
		public static void Max( ref Color color, ref Color other, ref Color result )
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
			return color.rgba == other.rgba;
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="color">A <see cref="Color"/>.</param>
		/// <param name="other">A <see cref="Color"/>.</param>
		/// <returns>Returns true if the colors are not equal, otherwise returns false.</returns>
		public static bool operator !=( Color color, Color other )
		{
			return color.rgba != other.rgba;
		}

		#endregion Operators

	}

}