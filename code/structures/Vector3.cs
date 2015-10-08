using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	
	/// <summary>A 3D vector.</summary>
	[StructLayout( LayoutKind.Sequential, Pack = 16, Size = 12 )]
	public struct Vector3 : IEquatable<Vector3> // TODO: , IComparable<Vector3>
	{

		/// <summary>The X component of this <see cref="Vector3"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		public float X;

		/// <summary>The Y component of this <see cref="Vector3"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		public float Y;


		/// <summary>The Z component of this <see cref="Vector3"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Z" )]
		public float Z;


		/// <summary>Initializes a new <see cref="Vector3"/>.</summary>
		/// <param name="x">The X component; must be a finite number.</param>
		/// <param name="y">The Y component; must be a finite number.</param>
		/// <param name="z">The Z component; must be a finite number.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z" )]
		public Vector3( float x, float y, float z )
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>Initializes a new <see cref="Vector3"/>.</summary>
		/// <param name="xy">The X and Y components of the vector.</param>
		/// <param name="z">The Z component; must be a finite number.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z" )]
		public Vector3( Vector2 xy, float z )
		{
			X = xy.X;
			Y = xy.Y;
			this.Z = z;
		}


		/// <summary>Initializes a new <see cref="Vector3"/>.</summary>
		/// <param name="xyz">The value used for both <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> components; must be a finite number.</param>
		public Vector3( float xyz )
		{
			X = Y = Z = xyz;
		}


		/// <summary></summary>
		public void Normalize()
		{
			float length = Length( this );
			
			if( length == 0.0f )
				return;
			
			X /= length;
			Y /= length;
			Z /= length;
		}


		/// <summary>Returns a hash code for this <see cref="Vector2"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Vector2"/> structure.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}


		/// <summary></summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals( Vector3 other )
		{
			return ( X == other.X ) && ( Y == other.Y ) && ( Z == other.Z );
		}


		/// <summary></summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals( object obj )
		{
			return ( obj is Vector3 ) && this.Equals( (Vector3)obj );
		}

		
		/// <summary></summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "({0},{1},{2})", X, Y, Z );
		}


		/// <summary></summary>
		public void Negate()
		{
			X = -X;
			Y = -Y;
			Z = -Z;
		}


		/// <summary></summary>
		/// <param name="value"></param>
		public void Multiply( float value )
		{
			X *= value;
			Y *= value;
			Z *= value;
		}

		/// <summary></summary>
		/// <param name="value"></param>
		public void Multiply( Vector3 value )
		{
			X *= value.X;
			Y *= value.Y;
			Z *= value.Z;
		}


		/// <summary></summary>
		/// <param name="value"></param>
		public void Divide( float value )
		{
			X /= value;
			Y /= value;
			Z /= value;
		}

		/// <summary></summary>
		/// <param name="value"></param>
		public void Divide( Vector3 value )
		{
			X /= value.X;
			Y /= value.Y;
			Z /= value.Z;
		}


		// TODO - Transform, TransformNormal, Reflect, SmoothStep, Lerp, Hermite, Barycentric, CatmullRom, Clamp

		/// <summary>The zero <see cref="Vector3"/> structure.</summary>
		public static readonly Vector3 Zero = new Vector3();

		/// <summary>Unit vector pointing to the right: (1,0,0).</summary>
		public static readonly Vector3 Right = new Vector3( 1.0f, 0.0f, 0.0f );

		/// <summary>Unit vector pointing to the left: (-1,0,0).</summary>
		public static readonly Vector3 Left = new Vector3( -1.0f, 0.0f, 0.0f );

		/// <summary>Unit vector pointing upward: (0,1,0).</summary>
		public static readonly Vector3 Up = new Vector3( 0.0f, 1.0f, 0.0f );

		/// <summary>Unit vector pointing downward: (0,-1,0).</summary>
		public static readonly Vector3 Down = new Vector3( 0.0f, -1.0f, 0.0f );

		/// <summary>Unit vector pointing backward: (0,0,1).</summary>
		public static readonly Vector3 Backward = new Vector3( 0.0f, 0.0f, 1.0f );
		
		/// <summary>Unit vector pointing forward: (0,0,-1).</summary>
		public static readonly Vector3 Forward = new Vector3( 0.0f, 0.0f, -1.0f );


		/// <summary></summary>
		/// <param name="vector"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static Vector3 Add( Vector3 vector, Vector3 other )
		{
			return new Vector3( vector.X + other.X, vector.Y + other.Y, vector.Z + other.Z );
		}


		/// <summary></summary>
		/// <param name="vector"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static Vector3 Subtract( Vector3 vector, Vector3 other )
		{
			return new Vector3( vector.X - other.X, vector.Y - other.Y, vector.Z - other.Z );
		}

	
		/// <summary></summary>
		/// <param name="vector3"></param>
		/// <returns></returns>
		public static float LengthSquared( Vector3 vector3 )
		{
			return vector3.X * vector3.X + vector3.Y * vector3.Y + vector3.Z * vector3.Z;
		}

		/// <summary></summary>
		/// <param name="vector3"></param>
		/// <returns></returns>
		public static float Length( Vector3 vector3 )
		{
			return (float)Math.Sqrt( (double)( vector3.X * vector3.X + vector3.Y * vector3.Y + vector3.Z * vector3.Z ) );
		}


		/// <summary></summary>
		/// <param name="vector3"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static Vector3 Min( Vector3 vector3, Vector3 other )
		{
			float x = Math.Min( vector3.X, other.X );
			float y = Math.Min( vector3.Y, other.Y );
			float z = Math.Min( vector3.Z, other.Z );
			return new Vector3( x, y, z );
		}


		/// <summary></summary>
		/// <param name="vector3"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static Vector3 Max( Vector3 vector3, Vector3 other )
		{
			float x = Math.Max( vector3.X, other.X );
			float y = Math.Max( vector3.Y, other.Y );
			float z = Math.Max( vector3.Z, other.Z );
			return new Vector3( x, y, z );
		}

	
		/// <summary></summary>
		/// <param name="vector3"></param>
		/// <param name="other"></param>
		/// <param name="dist"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#" )]
		public static void Distance( ref Vector3 vector3, ref Vector3 other, out float dist )
		{
			dist = Length( other - vector3 );
		}

		/// <summary></summary>
		/// <param name="vector3"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static float Distance( Vector3 vector3, Vector3 other )
		{
			return Length( other - vector3 );
		}


		/// <summary></summary>
		/// <param name="vector3"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		public static float DistanceSquared( ref Vector3 vector3, ref Vector3 other )
		{
			return LengthSquared( other - vector3 );
		}


		/// <summary></summary>
		/// <param name="vector3"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		public static float Dot( ref Vector3 vector3, ref Vector3 other )
		{
			return vector3.X * other.X + vector3.Y * other.Y + vector3.Z * other.Z;
		}

		
		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static bool operator ==( Vector3 vector3, Vector3 other )
		{
			return vector3.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static bool operator !=( Vector3 vector3, Vector3 other )
		{
			return !vector3.Equals( other );
		}


		/// <summary>Unary negation operator.</summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator -( Vector3 vector3 )
		{
			vector3.Negate();
			return vector3;
			//return new Vector2( -vector2.x, -vector2.y );
		}


		/// <summary></summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static bool operator <( Vector3 vector3, Vector3 other )
		{
			return ( vector3.X < other.X ) && ( vector3.Y < other.Y ) && ( vector3.Z < other.Z );
		}

		/// <summary></summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static bool operator <=( Vector3 vector3, Vector3 other )
		{
			return ( vector3.X <= other.X ) && ( vector3.Y <= other.Y ) && ( vector3.Z <= other.Z );
		}


		/// <summary></summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static bool operator >( Vector3 vector3, Vector3 other )
		{
			return ( vector3.X > other.X ) && ( vector3.Y > other.Y ) && ( vector3.Z > other.Z );
		}

		/// <summary></summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static bool operator >=( Vector3 vector3, Vector3 other )
		{
			return ( vector3.X >= other.X ) && ( vector3.Y >= other.Y ) && ( vector3.Z >= other.Z );
		}


		/// <summary></summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator +( Vector3 vector3, Vector3 other )
		{
			return new Vector3( vector3.X + other.X, vector3.Y + other.Y, vector3.Z + other.Z );
		}

		/// <summary></summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator -( Vector3 vector3, Vector3 other )
		{
			return new Vector3( vector3.X - other.X, vector3.Y - other.Y, vector3.Z - other.Z );
		}


		/// <summary></summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator *( Vector3 vector3, Vector3 other )
		{
			return new Vector3( vector3.X * other.X, vector3.Y * other.Y, vector3.Z * other.Z );
		}

		/// <summary></summary>
		/// <param name="vector3"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Vector3 operator *( Vector3 vector3, float value )
		{
			return new Vector3( vector3.X * value, vector3.Y * value, vector3.Z * value );
		}

		/// <summary></summary>
		/// <param name="value"></param>
		/// <param name="vector3"></param>
		/// <returns></returns>
		public static Vector3 operator *( float value, Vector3 vector3 )
		{
			return new Vector3( vector3.X * value, vector3.Y * value, vector3.Z * value );
		}


		/// <summary></summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator /( Vector3 vector3, Vector3 other )
		{
			return new Vector3( vector3.X / other.X, vector3.Y / other.Y, vector3.Z / other.Z );
		}

		/// <summary></summary>
		/// <param name="vector3"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Vector3 operator /( Vector3 vector3, float value )
		{
			return new Vector3( vector3.X / value, vector3.Y / value, vector3.Z / value );
		}

		/// <summary></summary>
		/// <param name="value"></param>
		/// <param name="vector3"></param>
		/// <returns></returns>
		public static Vector3 operator /( float value, Vector3 vector3 )
		{
			return new Vector3( value / vector3.X, value / vector3.Y, value / vector3.Z );
		}

		
		///// <summary></summary>
		///// <param name="vector2"></param>
		///// <param name="other"></param>
		///// <returns></returns>
		//public static float operator .( Vector3 vector2, Vector3 other )
		//{
		//	float dotProduct;
		//	Dot( ref vector2, ref other, out dotProduct );
		//	return dotProduct;
		//}

		#endregion


	}

}