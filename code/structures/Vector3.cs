using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	
	/// <summary>A 3D vector.</summary>
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 12 )]
	public struct Vector3 : IEquatable<Vector3>
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


		#region Constructors

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
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z" )]
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

		#endregion



		///// <summary>Gets or sets a component of this <see cref="Vector3"/> structure, given its index.</summary>
		///// <param name="index">The index of the concerned component: 0 for <see cref="X"/>, 1 for <see cref="Y"/>, 2 for <see cref="Z"/>.</param>
		///// <returns>Returns the component associated with the specified <paramref name="index"/>.</returns>
		///// <exception cref="ArgumentOutOfRangeException"/>
		//public float this[ int index ]
		//{
		//	get { return ( index == 0 ) ? X : ( index == 1 ) ? Y : ( index == 2 ) ? Z : 0.0f; }
		//	set
		//	{
		//		if( index == 0 )
		//			X = value;
		//		else if( index == 1 )
		//			Y = value;
		//		else if( index == 2 )
		//			Z = value;
		//		else
		//			throw new ArgumentOutOfRangeException( "index" );
		//	}
		//}


		/// <summary>Normalizes this <see cref="Vector3"/> value.</summary>
		public void Normalize()
		{
			float length = this.Length;
			if( length != 0.0f )
			{
				X /= length;
				Y /= length;
				Z /= length;
			}
		}


		/// <summary>Inverts the sign of all this <see cref="Vector3"/>'s components.</summary>
		public void Negate()
		{
			X = -X;
			Y = -Y;
			Z = -Z;
		}


		/// <summary>Returns the distance between this <see cref="Vector3"/> and another position.</summary>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the distance between this position and the <paramref name="other"/> position.</returns>
		public float DistanceTo( Vector3 other )
		{
			var x = other.X - X;
			var y = other.Y - Y;
			var z = other.Z - Z;
			return XMath.Sqrt( x * x + y * y + z * z );
		}


		/// <summary>Gets the square of the length of this <see cref="Vector3"/> value.
		/// <para>Note: this property is faster than <see cref="Length"/>.</para>
		/// </summary>
		public float LengthSquared { get { return X * X + Y * Y + Z * Z; } }

		/// <summary>Gets the length of this <see cref="Vector3"/> value.</summary>
		public float Length { get { return XMath.Sqrt( X * X + Y * Y + Z * Z ); } }


		/// <summary>Returns a hash code for this <see cref="Vector2"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Vector2"/> structure.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Vector3"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Vector3 other )
		{
			return ( X == other.X ) && ( Y == other.Y ) && ( Z == other.Z );
		}


		/// <summary>Returns a value indicating whether this <see cref="Vector3"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Vector3"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Vector3 ) && this.Equals( (Vector3)obj );
		}


		/// <summary>Returns a string representing this <see cref="Vector2"/> structure, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>,<see cref="Z"/>)</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="Vector2"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "({0},{1},{2})", X, Y, Z );
		}

		
		/// <summary>Returns an array containing the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> components of this <see cref="Vector3"/> structure.</summary>
		/// <returns>Returns an array containing the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> components of this <see cref="Vector3"/> structure.</returns>
		public float[] ToArray()
		{
			return new float[] { X, Y, Z };
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


		/// <summary>Multiplies two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> × <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Multiply( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			result = new Vector3( vector.X * other.X, vector.Y * other.Y, vector.Z * other.Z );
		}

		/// <summary>Multiplies two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> × <paramref name="other"/> ).</returns>
		public static Vector3 Multiply( Vector3 vector, Vector3 other )
		{
			return new Vector3( vector.X * other.X, vector.Y * other.Y, vector.Z * other.Z );
		}


		/// <summary>Divides a <see cref="Vector3"/> by another <see cref="Vector3"/>.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> : <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Divide( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			result = new Vector3( vector.X / other.X, vector.Y / other.Y, vector.Z / other.Z );
		}
		
		/// <summary>Divides a <see cref="Vector3"/> by another <see cref="Vector3"/>.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> : <paramref name="other"/> ).</returns>
		public static Vector3 Divide( Vector3 vector, Vector3 other )
		{
			return new Vector3( vector.X / other.X, vector.Y / other.Y, vector.Z / other.Z );
		}



		/// <summary>Returns a <see cref="Vector3"/> structure whose components are set to the minimum components between two <see cref="Vector3"/> values.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns a <see cref="Vector3"/> structure whose components are set to the minimum components between two <see cref="Vector3"/> values.</returns>
		public static Vector3 Min( Vector3 vector3, Vector3 other )
		{
			return new Vector3( Math.Min( vector3.X, other.X ), Math.Min( vector3.Y, other.Y ), Math.Min( vector3.Z, other.Z ) );
		}


		/// <summary>Returns a <see cref="Vector3"/> structure whose components are set to the maximum components between two <see cref="Vector3"/> values.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns a <see cref="Vector3"/> structure whose components are set to the maximum components between two <see cref="Vector3"/> values.</returns>
		public static Vector3 Max( Vector3 vector3, Vector3 other )
		{
			return new Vector3( Math.Max( vector3.X, other.X ), Math.Max( vector3.Y, other.Y ), Math.Max( vector3.Z, other.Z ) );
		}


	
		/// <summary>Calculates the distance between two <see cref="Vector3"/> positions.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the distance between the two specified positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Distance( ref Vector3 vector3, ref Vector3 other, out float result )
		{
			result = ( other - vector3 ).Length;
		}

		/// <summary>Returns the distance between two <see cref="Vector3"/> positions.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the distance between two <see cref="Vector3"/> positions.</returns>
		public static float Distance( Vector3 vector3, Vector3 other )
		{
			return ( other - vector3 ).Length;
		}


		/// <summary>Calculates the square of the distance between two <see cref="Vector3"/> positions.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> value.</param>
		/// <param name="other">A valid <see cref="Vector3"/> value.</param>
		/// <param name="result">Receives the square of the distance between the two specified positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void DistanceSquared( ref Vector3 vector3, ref Vector3 other, out float result )
		{
			result = ( other - vector3 ).LengthSquared;
		}


		/// <summary>Returns the dot product of two <see cref="Vector3"/> values.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> value.</param>
		/// <param name="other">A valid <see cref="Vector3"/> value.</param>
		/// <param name="result">Receives the dot product of the two specified <see cref="Vector3"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Dot( ref Vector3 vector3, ref Vector3 other, out float result )
		{
			result = vector3.X * other.X + vector3.Y * other.Y + vector3.Z * other.Z;
		}

		
		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Vector3 vector3, Vector3 other )
		{
			return vector3.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
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
		}


		/// <summary>Inferiority comparer.</summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <( Vector3 vector3, Vector3 other )
		{
			return ( vector3.X < other.X ) && ( vector3.Y < other.Y ) && ( vector3.Z < other.Z );
		}

		/// <summary>Inferiority or equality comparer.</summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <=( Vector3 vector3, Vector3 other )
		{
			return ( vector3.X <= other.X ) && ( vector3.Y <= other.Y ) && ( vector3.Z <= other.Z );
		}


		/// <summary>Superiority comparer.</summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >( Vector3 vector3, Vector3 other )
		{
			return ( vector3.X > other.X ) && ( vector3.Y > other.Y ) && ( vector3.Z > other.Z );
		}

		/// <summary>Superiority or equality comparer.</summary>
		/// <param name="vector3">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >=( Vector3 vector3, Vector3 other )
		{
			return ( vector3.X >= other.X ) && ( vector3.Y >= other.Y ) && ( vector3.Z >= other.Z );
		}


		/// <summary>Addition operator.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator +( Vector3 vector3, Vector3 other )
		{
			return new Vector3( vector3.X + other.X, vector3.Y + other.Y, vector3.Z + other.Z );
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator -( Vector3 vector3, Vector3 other )
		{
			return new Vector3( vector3.X - other.X, vector3.Y - other.Y, vector3.Z - other.Z );
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator *( Vector3 vector3, Vector3 other )
		{
			return new Vector3( vector3.X * other.X, vector3.Y * other.Y, vector3.Z * other.Z );
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns></returns>
		public static Vector3 operator *( Vector3 vector3, float value )
		{
			return new Vector3( vector3.X * value, vector3.Y * value, vector3.Z * value );
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator *( float value, Vector3 vector3 )
		{
			return new Vector3( vector3.X * value, vector3.Y * value, vector3.Z * value );
		}


		/// <summary>Division operator.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator /( Vector3 vector3, Vector3 other )
		{
			return new Vector3( vector3.X / other.X, vector3.Y / other.Y, vector3.Z / other.Z );
		}

		/// <summary>Division operator.</summary>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite, non-zero, single-precision floating-point value.</param>
		/// <returns></returns>
		public static Vector3 operator /( Vector3 vector3, float value )
		{
			return new Vector3( vector3.X / value, vector3.Y / value, vector3.Z / value );
		}

		/// <summary>Division operator.</summary>
		/// <param name="value">A finite, non-zero, single-precision floating-point value.</param>
		/// <param name="vector3">A valid <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator /( float value, Vector3 vector3 )
		{
			return new Vector3( value / vector3.X, value / vector3.Y, value / vector3.Z );
		}
		
		#endregion

	}

}