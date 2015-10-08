using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	
	/// <summary>A 2D vector.</summary>
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )] // TODO - might require to be 16-bytes aligned
	public struct Vector2 : IEquatable<Vector2>
	{
		
		/// <summary>The "horizontal" component of this <see cref="Vector2"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		public float X;

		/// <summary>The "vertical" component of this <see cref="Vector2"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		public float Y;


		#region Constructors


		/// <summary>Initializes a new <see cref="Vector2"/>.</summary>
		/// <param name="x">The x component.</param>
		/// <param name="y">The y component.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y" )]
		public Vector2( float x, float y )
		{
			this.X = x;
			this.Y = y;
		}


		/// <summary>Initializes a new <see cref="Vector2"/>.</summary>
		/// <param name="xy">The value used for both <see cref="X"/> and <see cref="Y"/> components.</param>
		public Vector2( float xy )
		{
			X = Y = xy;
		}

		
		#endregion


		/// <summary>Returns a hash code for this <see cref="Vector2"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Vector2"/> structure.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		
		/// <summary>Returns a value indicating whether this <see cref="Vector2"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Vector2 other )
		{
			return ( X == other.X ) && ( Y == other.Y );
		}


		/// <summary>Returns a value indicating whether this <see cref="Vector2"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Vector2"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Vector2 ) && this.Equals( (Vector2)obj );
		}

		
		/// <summary>Returns a string representing this <see cref="Vector2"/> structure, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>)</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="Vector2"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "({0},{1})", X, Y );
		}


		/// <summary>Normalizes this <see cref="Vector2"/> structure.</summary>
		public void Normalize()
		{
			float length = this.Length;
			if( length == 0.0f )
				return;
			X /= length;
			Y /= length;
		}


		/// <summary>Inverts the sign of both <see cref="X"/> and <see cref="Y"/>.</summary>
		public void Negate()
		{
			X = -X;
			Y = -Y;
		}


		/// <summary></summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		public void Clamp( ref Vector2 min, ref Vector2 max )
		{
			if( X < min.X )
				X = min.X;
			else if( X > max.X )
				X = max.X;

			if( Y < min.Y )
				Y = min.Y;
			else if( Y > max.Y )
				Y = max.Y;
		}


		/// <summary></summary>
		/// <param name="value">A single-precision floating-point number.</param>
		public void Multiply( float value )
		{
			X *= value;
			Y *= value;
		}

		/// <summary></summary>
		/// <param name="value">A <see cref="Vector2"/> structure.</param>
		public void Multiply( Vector2 value )
		{
			X *= value.X;
			Y *= value.Y;
		}


		/// <summary></summary>
		/// <param name="value">A single-precision floating-point number.</param>
		public void Divide( float value )
		{
			X /= value;
			Y /= value;
		}

		/// <summary></summary>
		/// <param name="value">A <see cref="Vector2"/> structure.</param>
		public void Divide( Vector2 value )
		{
			X /= value.X;
			Y /= value.Y;
		}


		/// <summary>Gets the length of this <see cref="Vector2"/> structure.</summary>
		public float Length { get { return (float)Math.Sqrt( (double)( X * X + Y * Y ) ); } }
		
		/// <summary>Gets the length squared of this <see cref="Vector2"/> structure.</summary>
		public float LengthSquared { get { return X * X + Y * Y; } }


		// TODO - Transform, TransformNormal


		/// <summary>The zero <see cref="Vector2"/> structure.</summary>
		public static readonly Vector2 Zero = new Vector2();

		/// <summary>A <see cref="Vector2"/> structure whose components are set to 1.</summary>
		public static readonly Vector2 One = new Vector2( 1.0f, 1.0f );

		/// <summary>A unit <see cref="Vector2"/> structure pointing to the positive x-direction.</summary>
		public static readonly Vector2 UnitX = new Vector2( 1.0f, 0.0f );

		/// <summary>A unit <see cref="Vector2"/> structure pointing to the positive y-direction.</summary>
		public static readonly Vector2 UnitY = new Vector2( 0.0f, 1.0f );




		/// <summary></summary>
		/// <param name="vector2"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static Vector2 Add( Vector2 vector2, Vector2 other )
		{
			return new Vector2( vector2.X + other.X, vector2.Y + other.Y );
		}

		
		/// <summary></summary>
		/// <param name="vector2"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static Vector2 Subtract( Vector2 vector2, Vector2 other )
		{
			return new Vector2( vector2.X - other.X, vector2.Y - other.Y );
		}



		/// <summary></summary>
		/// <param name="vector2"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static Vector2 Min( Vector2 vector2, Vector2 other )
		{
			float x = Math.Min( vector2.X, other.X );
			float y = Math.Min( vector2.Y, other.Y );
			return new Vector2( x, y );
		}


		/// <summary></summary>
		/// <param name="vector2"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static Vector2 Max( Vector2 vector2, Vector2 other )
		{
			float x = Math.Max( vector2.X, other.X );
			float y = Math.Max( vector2.Y, other.Y );
			return new Vector2( x, y );
		}

	
		/// <summary></summary>
		/// <param name="vector2"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		public static float Distance( ref Vector2 vector2, ref Vector2 other )
		{
			return ( other - vector2 ).Length;
		}

		/// <summary></summary>
		/// <param name="vector2"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		public static float DistanceSquared( ref Vector2 vector2, ref Vector2 other )
		{
			return ( other - vector2 ).LengthSquared;
		}


		/// <summary></summary>
		/// <param name="vector2"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		public static float Dot( ref Vector2 vector2, ref Vector2 other )
		{
			return vector2.X * other.X + vector2.Y * other.Y;
		}



		/// <summary>Determines the reflect vector of the given vector and normal.</summary>
		/// <param name="vector">Source vector.</param>
		/// <param name="normal">Normal of vector.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		public static Vector2 Reflect( ref Vector2 vector, ref Vector2 normal )
		{
			float dot = vector.X * normal.X + vector.Y * normal.Y;
			return new Vector2(
				vector.X - 2.0f * dot * normal.X,
				vector.Y - 2.0f * dot * normal.Y
			);
		}


		/// <summary>Performs a linear interpolation between two vectors.</summary>
		/// <param name="vector1">Source vector.</param>
		/// <param name="vector2">Source vector.</param>
		/// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="vector2"/>.</param>
		public static Vector2 Lerp( Vector2 vector1, Vector2 vector2, float amount )
		{
			Vector2 result;
			result.X = vector1.X + ( vector2.X - vector1.X ) * amount;
			result.Y = vector1.Y + ( vector2.Y - vector1.Y ) * amount;
			return result;
		}


		/// <summary>Returns a Vector2 containing the 2D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 2D triangle.</summary>
		/// <param name="value1">A Vector2 containing the 2D Cartesian coordinates of vertex 1 of the triangle.</param>
		/// <param name="value2">A Vector2 containing the 2D Cartesian coordinates of vertex 2 of the triangle.</param>
		/// <param name="value3">A Vector2 containing the 2D Cartesian coordinates of vertex 3 of the triangle.</param>
		/// <param name="amount1">Barycentric coordinate b2, which expresses the weighting factor toward vertex 2 (specified in value2).</param>
		/// <param name="amount2">Barycentric coordinate b3, which expresses the weighting factor toward vertex 3 (specified in value3).</param>
		public static Vector2 Barycentric( Vector2 value1, Vector2 value2, Vector2 value3, float amount1, float amount2 )
		{
			return new Vector2(
				value1.X + amount1 * ( value2.X - value1.X ) + amount2 * ( value3.X - value1.X ),
				value1.Y + amount1 * ( value2.Y - value1.Y ) + amount2 * ( value3.Y - value1.Y )
			);
		}

		
		/// <summary>Interpolates between two values using a cubic equation.</summary>
		/// <param name="value1">Source value.</param>
		/// <param name="value2">Source value.</param>
		/// <param name="amount">Weighting value.</param>
		public static Vector2 SmoothStep( Vector2 value1, Vector2 value2, float amount )
		{
			amount = ( ( amount > 1.0f ) ? 1.0f : ( ( amount < 0.0f ) ? 0.0f : amount ) );
			amount = amount * amount * ( 3.0f - 2.0f * amount );
			return new Vector2(
				value1.X + ( value2.X - value1.X ) * amount,
				value1.Y + ( value2.Y - value1.Y ) * amount
			);
		}


		/// <summary>Performs a Catmull-Rom interpolation using the specified positions.</summary>
		/// <param name="value1">The first position in the interpolation.</param>
		/// <param name="value2">The second position in the interpolation.</param>
		/// <param name="value3">The third position in the interpolation.</param>
		/// <param name="value4">The fourth position in the interpolation.</param>
		/// <param name="amount">Weighting factor.</param>
		public static Vector2 CatmullRom( Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount )
		{
			float num = amount * amount;
			float num2 = amount * num;
			Vector2 result;
			result.X = 0.5f * ( 2.0f * value2.X + ( -value1.X + value3.X ) * amount + ( 2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X ) * num + ( -value1.X + 3.0f * value2.X - 3.0f * value3.X + value4.X ) * num2 );
			result.Y = 0.5f * ( 2f * value2.Y + ( -value1.Y + value3.Y ) * amount + ( 2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y ) * num + ( -value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y ) * num2 );
			return result;
		}


		/// <summary>Performs a Hermite spline interpolation.</summary>
		/// <param name="value1">Source position vector.</param>
		/// <param name="tangent1">Source tangent vector.</param>
		/// <param name="value2">Source position vector.</param>
		/// <param name="tangent2">Source tangent vector.</param>
		/// <param name="amount">Weighting factor.</param>
		public static Vector2 Hermite( Vector2 value1, Vector2 tangent1, Vector2 value2, Vector2 tangent2, float amount )
		{
			float num = amount * amount;
			float num2 = amount * num;
			float num3 = 2f * num2 - 3f * num + 1f;
			float num4 = -2f * num2 + 3f * num;
			float num5 = num2 - 2f * num + amount;
			float num6 = num2 - num;
			Vector2 result;
			result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
			result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
			return result;
		}



		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Vector2 vector2, Vector2 other )
		{
			return vector2.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Vector2 vector2, Vector2 other )
		{
			return !vector2.Equals( other );
		}


		/// <summary>Unary negation operator.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator -( Vector2 vector2 )
		{
			vector2.Negate();
			return vector2;
			//return new Vector2( -vector2.x, -vector2.y );
		}


		/// <summary></summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static bool operator <( Vector2 vector2, Vector2 other )
		{
			return ( vector2.X < other.X ) && ( vector2.Y < other.Y );
		}

		/// <summary></summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static bool operator <=( Vector2 vector2, Vector2 other )
		{
			return ( vector2.X <= other.X ) && ( vector2.Y <= other.Y );
		}


		/// <summary></summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static bool operator >( Vector2 vector2, Vector2 other )
		{
			return ( vector2.X > other.X ) && ( vector2.Y > other.Y );
		}

		/// <summary></summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static bool operator >=( Vector2 vector2, Vector2 other )
		{
			return ( vector2.X >= other.X ) && ( vector2.Y >= other.Y );
		}


		/// <summary></summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator +( Vector2 vector2, Vector2 other )
		{
			return new Vector2( vector2.X + other.X, vector2.Y + other.Y );
		}

		/// <summary></summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator -( Vector2 vector2, Vector2 other )
		{
			return new Vector2( vector2.X - other.X, vector2.Y - other.Y );
		}


		/// <summary></summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator *( Vector2 vector2, Vector2 other )
		{
			return new Vector2( vector2.X * other.X, vector2.Y * other.Y );
		}

		/// <summary></summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <returns></returns>
		public static Vector2 operator *( Vector2 vector2, float value )
		{
			return new Vector2( vector2.X * value, vector2.Y * value );
		}

		/// <summary></summary>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator *( float value, Vector2 vector2 )
		{
			return new Vector2( vector2.X * value, vector2.Y * value );
		}


		/// <summary></summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator /( Vector2 vector2, Vector2 other )
		{
			return new Vector2( vector2.X / other.X, vector2.Y / other.Y );
		}

		/// <summary></summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <returns></returns>
		public static Vector2 operator /( Vector2 vector2, float value )
		{
			return new Vector2( vector2.X / value, vector2.Y / value );
		}

		/// <summary></summary>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator /( float value, Vector2 vector2 )
		{
			return new Vector2( value / vector2.X, value / vector2.Y );
		}

		
		///// <summary></summary>
		///// <param name="vector2"></param>
		///// <param name="other"></param>
		///// <returns></returns>
		//public static float operator %( Vector2 vector2, Vector2 other )
		//{
		//	float dotProduct;
		//	Dot( ref vector2, ref other, out dotProduct );
		//	return dotProduct;
		//}

		#endregion


	}

}