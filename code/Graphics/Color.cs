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
		/// <param name="rgba"></param>
		[CLSCompliant( false )]
		public Color( uint rgba )
		{
			this.rgba = rgba;
		}


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



		/// <summary></summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <param name="amount"></param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Lerp( ref Color source, ref Color target, float amount, out Color result )
		{
			var r = (float)source.R;
			var g = (float)source.G;
			var b = (float)source.B;
			var a = (float)source.A;

			r = r + ( (float)target.R - r ) * amount;
			g = g + ( (float)target.G - g ) * amount;
			b = b + ( (float)target.B - b ) * amount;
			a = a + ( (float)target.A - a ) * amount;

			result = new Color(
				(byte)r.Clamp( 0.0f, 255.0f ),
				(byte)g.Clamp( 0.0f, 255.0f ),
				(byte)b.Clamp( 0.0f, 255.0f ),
				(byte)a.Clamp( 0.0f, 255.0f )
			);
		}

		/// <summary></summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
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