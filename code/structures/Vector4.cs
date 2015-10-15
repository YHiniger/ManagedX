using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX
{
	
	/// <summary>A 4D vector.</summary>
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct Vector4 : IEquatable<Vector4>
	{

		/// <summary>The X component of this <see cref="Vector4"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		public float X;

		/// <summary>The Y component of this <see cref="Vector4"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		public float Y;

		/// <summary>The Z component of this <see cref="Vector4"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Z" )]
		public float Z;

		/// <summary>The W component of this <see cref="Vector4"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "W" )]
		public float W;


		#region Constructors

		/// <summary>Initializes a new <see cref="Vector4"/>.</summary>
		/// <param name="x">The X component; must be a finite number.</param>
		/// <param name="y">The Y component; must be a finite number.</param>
		/// <param name="z">The Z component; must be a finite number.</param>
		/// <param name="w">The W component; must be a finite number.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "w" )]
		public Vector4( float x, float y, float z, float w )
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		/// <summary>Initializes a new <see cref="Vector4"/>.</summary>
		/// <param name="xy">The X and Y components of the vector.</param>
		/// <param name="z">The Z component; must be a finite number.</param>
		/// <param name="w">The W component; must be a finite number.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "w" )]
		public Vector4( Vector2 xy, float z, float w )
		{
			this.X = xy.X;
			this.Y = xy.Y;
			this.Z = z;
			this.W = w;
		}

		/// <summary>Initializes a new <see cref="Vector4"/>.</summary>
		/// <param name="xyz">A valid <see cref="Vector3"/> containing the X, Y and Z components of the vector.</param>
		/// <param name="w">The W component; must be a finite number.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "w" )]
		public Vector4( Vector3 xyz, float w )
		{
			this.X = xyz.X;
			this.Y = xyz.Y;
			this.Z = xyz.Z;
			this.W = w;
		}

		/// <summary>Initializes a new <see cref="Vector4"/>.</summary>
		/// <param name="xyzw">The value used for both <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components; must be a finite number.</param>
		public Vector4( float xyzw )
		{
			this.W = this.Z = this.Y = this.X = xyzw;
		}

		#endregion



		/// <summary>Normalizes this <see cref="Vector4"/>.</summary>
		public void Normalize()
		{
			var length = this.Length;
			if( length != 0.0f )
			{
				X /= length;
				Y /= length;
				Z /= length;
				W /= length;
			}
		}


		/// <summary>Inverts the sign of all this <see cref="Vector4"/>'s components.</summary>
		public void Negate()
		{
			X = -X;
			Y = -Y;
			Z = -Z;
			W = -W;
		}


		/// <summary>Returns the distance between this <see cref="Vector4"/> and another position.</summary>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns>Returns the distance between this position and the <paramref name="other"/> position.</returns>
		public float DistanceTo( Vector4 other )
		{
			var x = other.X - this.X;
			var y = other.Y - this.Y;
			var z = other.Z - this.Z;
			var w = other.W - this.W;
			return XMath.Sqrt( x * x + y * y + z * z + w * w );
		}


		/// <summary>Gets the square of the length of this <see cref="Vector4"/>.
		/// <para>Note: this property is faster than <see cref="Length"/>, since it doesn't calculate the square root.</para>
		/// </summary>
		public float LengthSquared { get { return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W; } }

		/// <summary>Gets the length of this <see cref="Vector4"/>.</summary>
		public float Length { get { return XMath.Sqrt( this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W ); } }


		/// <summary>Forces the components of this <see cref="Vector4"/> to the range [<paramref name="min"/>,<paramref name="max"/>].</summary>
		/// <param name="min">A valid <see cref="Vector4"/> structure containing the minimum value for each component.</param>
		/// <param name="max">A valid <see cref="Vector4"/> structure containing the maximum value for each component.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		public void Clamp( ref Vector4 min, ref Vector4 max )
		{
			this.X = this.X.Clamp( min.X, max.X );
			this.Y = this.Y.Clamp( min.Y, max.Y );
			this.Z = this.Z.Clamp( min.Z, max.Z );
			this.W = this.W.Clamp( min.W, max.W );
		}


		/// <summary>Returns a hash code for this <see cref="Vector4"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Vector4"/> structure.</returns>
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode() ^ this.W.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Vector4"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Vector4 other )
		{
			return ( this.X == other.X ) && ( this.Y == other.Y ) && ( this.Z == other.Z ) && ( this.W == other.W );
		}


		/// <summary>Returns a value indicating whether this <see cref="Vector4"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Vector4"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Vector4 ) && this.Equals( (Vector4)obj );
		}


		/// <summary>Returns a string representing this <see cref="Vector4"/> structure, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>,<see cref="Z"/>,<see cref="W"/>)</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="Vector4"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "({0},{1},{2},{3})", this.X, this.Y, this.Z, this.W );
		}


		/// <summary>Returns an array containing, respectively, the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components of this <see cref="Vector4"/> structure.</summary>
		/// <returns>Returns an array containing the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components of this <see cref="Vector4"/> structure.</returns>
		public float[] ToArray()
		{
			return new float[] { this.X, this.Y, this.Z, this.W };
		}


		/// <summary>Returns a <see cref="Vector2"/> structure initialized with the <see cref="X"/> and <see cref="Y"/> components of this <see cref="Vector4"/>.</summary>
		/// <returns>Returns a <see cref="Vector2"/> structure initialized with the <see cref="X"/> and <see cref="Y"/> components of this <see cref="Vector4"/>.</returns>
		public Vector2 ToVector2()
		{
			return new Vector2( this.X, this.Y );
		}


		/// <summary>Returns a <see cref="Vector3"/> structure initialized with the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> components of this <see cref="Vector4"/>.</summary>
		/// <returns>Returns a <see cref="Vector3"/> structure initialized with the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> components of this <see cref="Vector4"/>.</returns>
		public Vector3 ToVector3()
		{
			return new Vector3( this.X, this.Y, this.Z );
		}
		
		
		// TODO - Transform, TransformNormal


		/// <summary>The zero <see cref="Vector4"/> structure.</summary>
		public static readonly Vector4 Zero = new Vector4();

		/// <summary>A <see cref="Vector4"/> whose components are set to 1.</summary>
		public static readonly Vector4 One = new Vector4( 1.0f, 1.0f, 1.0f, 1.0f );

		/// <summary>Unit vector: (1,0,0,0).</summary>
		public static readonly Vector4 UnitX = new Vector4( 1.0f, 0.0f, 0.0f, 0.0f );

		/// <summary>Unit vector: (0,1,0,0).</summary>
		public static readonly Vector4 UnitY = new Vector4( 0.0f, 1.0f, 0.0f, 0.0f );

		/// <summary>Unit vector: (0,0,1,0).</summary>
		public static readonly Vector4 UnitZ = new Vector4( 0.0f, 0.0f, 1.0f, 0.0f );
		
		/// <summary>Unit vector: (0,0,0,1).</summary>
		public static readonly Vector4 UnitW = new Vector4( 0.0f, 0.0f, 0.0f, 1.0f );


		#region Static functions

		/// <summary>Calculates the sum of two <see cref="Vector4"/>.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <param name="result">Receives the sum of the two specified <see cref="Vector4"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Add( ref Vector4 vector, ref Vector4 other, out Vector4 result )
		{
			result.X = vector.X + other.X;
			result.Y = vector.Y + other.Y;
			result.Z = vector.Z + other.Z;
			result.W = vector.W + other.W;
		}

		/// <summary>Returns the sum of two <see cref="Vector4"/>.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns the sum of the two specified <see cref="Vector4"/>.</returns>
		public static Vector4 Add( Vector4 vector, Vector4 other )
		{
			vector.X += other.X;
			vector.Y += other.Y;
			vector.Z += other.Z;
			vector.W += other.W;
			return vector;
		}

		// TODO - Add( Vector4 vector, float value ), Subtract, Divide


		/// <summary>Subtracts a vector (<paramref name="other"/>) from another vector (<paramref name="vector"/>).</summary>
		/// <param name="vector">A valid <see cref="Vector4"/>.</param>
		/// <param name="other">A valid <see cref="Vector4"/>.</param>
		/// <param name="result">Receives the difference between <paramref name="vector"/> and <paramref name="other"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Subtract( ref Vector4 vector, ref Vector4 other, out Vector4 result )
		{
			result.X = vector.X - other.X;
			result.Y = vector.Y - other.Y;
			result.Z = vector.Z - other.Z;
			result.W = vector.W - other.W;
		}

		/// <summary>Subtracts a vector (<paramref name="other"/>) from another vector (<paramref name="vector"/>).</summary>
		/// <param name="vector">A valid <see cref="Vector4"/>.</param>
		/// <param name="other">A valid <see cref="Vector4"/>.</param>
		/// <returns>Returns the difference between <paramref name="vector"/> and <paramref name="other"/>.</returns>
		public static Vector4 Subtract( Vector4 vector, Vector4 other )
		{
			vector.X -= other.X;
			vector.Y -= other.Y;
			vector.Z -= other.Z;
			vector.W -= other.W;
			return vector;
		}


		/// <summary>Multiplies two <see cref="Vector4"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> × <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Multiply( ref Vector4 vector, ref Vector4 other, out Vector4 result )
		{
			result.X = vector.X * other.X;
			result.Y = vector.Y * other.Y;
			result.Z = vector.Z * other.Z;
			result.W = vector.W * other.W;
		}

		/// <summary>Multiplies two <see cref="Vector4"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> × <paramref name="other"/> ).</returns>
		public static Vector4 Multiply( Vector4 vector, Vector4 other )
		{
			vector.X *= other.X;
			vector.Y *= other.Y;
			vector.Z *= other.Z;
			vector.W *= other.W;
			return vector;
		}

		/// <summary>Multiplies a <see cref="Vector4"/> by a value.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <param name="result">Receives the multiplied <see cref="Vector4"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Multiply( ref Vector4 vector, float value, out Vector4 result )
		{
			result.X = vector.X * value;
			result.Y = vector.Y * value;
			result.Z = vector.Z * value;
			result.W = vector.W * value;
		}

		/// <summary>Multiplies a <see cref="Vector4"/> by a value.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns>Returns the multiplied <see cref="Vector4"/>.</returns>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static Vector4 Multiply( Vector4 vector, float value )
		{
			vector.X *= value;
			vector.Y *= value;
			vector.Z *= value;
			vector.W *= value;
			return vector;
		}


		/// <summary>Divides a <see cref="Vector4"/> by another <see cref="Vector4"/>.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Vector4"/> structure.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> : <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Divide( ref Vector4 vector, ref Vector4 other, out Vector4 result )
		{
			result.X = vector.X / other.X;
			result.Y = vector.Y / other.Y;
			result.Z = vector.Z / other.Z;
			result.W = vector.W / other.W;
		}

		/// <summary>Divides a <see cref="Vector4"/> by another <see cref="Vector4"/>.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Vector4"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> : <paramref name="other"/> ).</returns>
		public static Vector4 Divide( Vector4 vector, Vector4 other )
		{
			vector.X /= other.X;
			vector.Y /= other.Y;
			vector.Z /= other.Z;
			vector.W /= other.W;
			return vector;
		}

		/// <summary>Divides a <see cref="Vector4"/> by another <see cref="Vector4"/>.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> : <paramref name="value"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Divide( ref Vector4 vector, float value, out Vector4 result )
		{
			var inv = 1.0f / value;
			result.X = vector.X * inv;
			result.Y = vector.Y * inv;
			result.Z = vector.Z * inv;
			result.W = vector.W * inv;
		}

		/// <summary>Divides a <see cref="Vector4"/> by another <see cref="Vector4"/>.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> : <paramref name="value"/> ).</returns>
		public static Vector4 Divide( Vector4 vector, float value )
		{
			var inv = 1.0f / value;
			vector.X *= inv;
			vector.Y *= inv;
			vector.Z *= inv;
			vector.W *= inv;
			return vector;
		}



		/// <summary>Retrieves a <see cref="Vector4"/> structure whose components are set to the minimum components between two <see cref="Vector4"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vector4"/> structure whose components are set to the minimum components between two <see cref="Vector4"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Min( ref Vector4 vector, ref Vector4 other, out Vector4 result )
		{
			result.X = XMath.Min( vector.X, other.X );
			result.Y = XMath.Min( vector.Y, other.Y );
			result.Z = XMath.Min( vector.Z, other.Z );
			result.W = XMath.Min( vector.W, other.W );
		}

		/// <summary>Returns a <see cref="Vector4"/> structure whose components are set to the minimum components between two <see cref="Vector4"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns>Returns a <see cref="Vector4"/> structure whose components are set to the minimum components between two <see cref="Vector4"/> values.</returns>
		public static Vector4 Min( Vector4 vector, Vector4 other )
		{
			vector.X = XMath.Min( vector.X, other.X );
			vector.Y = XMath.Min( vector.Y, other.Y );
			vector.Z = XMath.Min( vector.Z, other.Z );
			vector.W = XMath.Min( vector.W, other.W );
			return vector;
		}


		/// <summary>Retrieves a <see cref="Vector4"/> structure whose components are set to the maximum components between two <see cref="Vector4"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vector4"/> structure whose components are set to the maximum components between two <see cref="Vector4"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Max( ref Vector4 vector, ref Vector4 other, out Vector4 result )
		{
			result.X = XMath.Max( vector.X, other.X );
			result.Y = XMath.Max( vector.Y, other.Y );
			result.Z = XMath.Max( vector.Z, other.Z );
			result.W = XMath.Max( vector.W, other.W );
		}

		/// <summary>Returns a <see cref="Vector4"/> structure whose components are set to the maximum components between two <see cref="Vector4"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns>Returns a <see cref="Vector4"/> structure whose components are set to the maximum components between two <see cref="Vector4"/> values.</returns>
		public static Vector4 Max( Vector4 vector, Vector4 other )
		{
			vector.X = XMath.Max( vector.X, other.X );
			vector.Y = XMath.Max( vector.Y, other.Y );
			vector.Z = XMath.Max( vector.Z, other.Z );
			vector.W = XMath.Max( vector.W, other.W );
			return vector;
		}


		/// <summary>Calculates the distance between two <see cref="Vector4"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="result">Receives the distance between the two specified positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Distance( ref Vector4 position, ref Vector4 other, out float result )
		{
			Vector4 diff;
			Subtract( ref position, ref other, out diff );
			result = diff.Length;
		}

		/// <summary>Returns the distance between two <see cref="Vector4"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns>Returns the distance between two <see cref="Vector4"/> positions.</returns>
		public static float Distance( Vector4 position, Vector4 other )
		{
			Vector4 diff;
			Subtract( ref position, ref other, out diff );
			return diff.Length;
		}


		/// <summary>Calculates the square of the distance between two <see cref="Vector4"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector4"/> value.</param>
		/// <param name="other">A valid <see cref="Vector4"/> value.</param>
		/// <param name="result">Receives the square of the distance between the two specified positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void DistanceSquared( ref Vector4 position, ref Vector4 other, out float result )
		{
			Vector4 diff;
			Subtract( ref position, ref other, out diff );
			result = diff.LengthSquared;
		}

		/// <summary>Returns the square of the distance between two <see cref="Vector4"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector4"/> value.</param>
		/// <param name="other">A valid <see cref="Vector4"/> value.</param>
		/// <returns>Returns the square of the distance between the two specified positions.</returns>
		public static float DistanceSquared( Vector4 position, Vector4 other )
		{
			Vector4 diff;
			Subtract( ref position, ref other, out diff );
			return diff.LengthSquared;
		}


		/// <summary>Calculates the dot product of two <see cref="Vector4"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> value.</param>
		/// <param name="other">A valid <see cref="Vector4"/> value.</param>
		/// <param name="result">Receives the dot product of the two specified <see cref="Vector4"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Dot( ref Vector4 vector, ref Vector4 other, out float result )
		{
			result = vector.X * other.X + vector.Y * other.Y + vector.Z * other.Z + vector.W * other.W;
		}

		/// <summary>Calculates the dot product of two <see cref="Vector4"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> value.</param>
		/// <param name="other">A valid <see cref="Vector4"/> value.</param>
		/// <returns>Returns the dot product of the two specified <see cref="Vector4"/> values.</returns>
		public static float Dot( Vector4 vector, Vector4 other )
		{
			return vector.X * other.X + vector.Y * other.Y + vector.Z * other.Z + vector.W * other.W;
		}


		/// <summary>Performs a linear interpolation between two vectors.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <param name="result">Receives the result of the linear interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Lerp( ref Vector4 source, ref Vector4 target, float amount, out Vector4 result )
		{
			result.X = XMath.Lerp( source.X, target.X, amount );
			result.Y = XMath.Lerp( source.Y, target.Y, amount );
			result.Z = XMath.Lerp( source.Z, target.Z, amount );
			result.W = XMath.Lerp( source.W, target.W, amount );
		}

		/// <summary>Performs a linear interpolation between two vectors.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <returns>Returns the result of the linear interpolation.</returns>
		public static Vector4 Lerp( Vector4 source, Vector4 target, float amount )
		{
			return new Vector4(
				XMath.Lerp( source.X, target.X, amount ),
				XMath.Lerp( source.Y, target.Y, amount ),
				XMath.Lerp( source.Z, target.Z, amount ),
				XMath.Lerp( source.W, target.W, amount )
			);
		}


		/// <summary>Interpolates between two values using a cubic equation.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; will be saturated.</param>
		/// <param name="result">Receives the result of the cubic interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void SmoothStep( ref Vector4 source, ref Vector4 target, float amount, out Vector4 result )
		{
			amount = amount.Saturate();
			Lerp( ref source, ref target, amount * amount * ( 3.0f - 2.0f * amount ), out result );
		}

		/// <summary>Interpolates between two values using a cubic equation.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; will be saturated.</param>
		/// <returns>Returns the result of the cubic interpolation.</returns>
		public static Vector4 SmoothStep( Vector4 source, Vector4 target, float amount )
		{
			Vector4 result;
			SmoothStep( ref source, ref target, amount, out result );
			return result;
		}


		/// <summary>Returns a <see cref="Vector4"/> containing the 4D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 4D triangle.</summary>
		/// <param name="value1">A <see cref="Vector4"/> containing the 4D Cartesian coordinates of vertex 1 of the triangle.</param>
		/// <param name="value2">A <see cref="Vector4"/> containing the 4D Cartesian coordinates of vertex 2 of the triangle.</param>
		/// <param name="value3">A <see cref="Vector4"/> containing the 4D Cartesian coordinates of vertex 3 of the triangle.</param>
		/// <param name="amount1">Barycentric coordinate b2, which expresses the weighting factor toward vertex 2 (specified in value2).</param>
		/// <param name="amount2">Barycentric coordinate b3, which expresses the weighting factor toward vertex 3 (specified in value3).</param>
		/// <param name="result">Receives a <see cref="Vector4"/> containing the 4D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 4D triangle.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "5#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Barycentric( ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, float amount1, float amount2, out Vector4 result )
		{
			result.X = value1.X + amount1 * ( value2.X - value1.X ) + amount2 * ( value3.X - value1.X );
			result.Y = value1.Y + amount1 * ( value2.Y - value1.Y ) + amount2 * ( value3.Y - value1.Y );
			result.Z = value1.Z + amount1 * ( value2.Z - value1.Z ) + amount2 * ( value3.Z - value1.Z );
			result.W = value1.W + amount1 * ( value2.W - value1.W ) + amount2 * ( value3.W - value1.W );
		}

		/// <summary>Returns a Vector4 containing the 4D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 4D triangle.</summary>
		/// <param name="value1">A <see cref="Vector4"/> containing the 4D Cartesian coordinates of vertex 1 of the triangle.</param>
		/// <param name="value2">A <see cref="Vector4"/> containing the 4D Cartesian coordinates of vertex 2 of the triangle.</param>
		/// <param name="value3">A <see cref="Vector4"/> containing the 4D Cartesian coordinates of vertex 3 of the triangle.</param>
		/// <param name="amount1">Barycentric coordinate b2, which expresses the weighting factor toward vertex 2 (specified in value2).</param>
		/// <param name="amount2">Barycentric coordinate b3, which expresses the weighting factor toward vertex 3 (specified in value3).</param>
		/// <returns>Returns a <see cref="Vector4"/> containing the 4D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 4D triangle.</returns>
		public static Vector4 Barycentric( Vector4 value1, Vector4 value2, Vector4 value3, float amount1, float amount2 )
		{
			Vector4 result;
			Barycentric( ref value1, ref value2, ref value3, amount1, amount2, out result );
			return result;
		}


		/// <summary>Performs a Catmull-Rom interpolation.</summary>
		/// <param name="value1">The first value in the interpolation.</param>
		/// <param name="value2">The second value in the interpolation.</param>
		/// <param name="value3">The third value in the interpolation.</param>
		/// <param name="value4">The fourth value in the interpolation.</param>
		/// <param name="amount">The weighting factor.</param>
		/// <param name="result">Receives the result of the Catmull-Rom interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "3#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "5#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void CatmullRom( ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, ref Vector4 value4, float amount, out Vector4 result )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;
			result.X = 0.5f * ( 2.0f * value2.X + ( -value1.X + value3.X ) * amount + ( 2.0f * value1.X - 5.0f * value2.X + 4.0f * value3.X - value4.X ) * amountSquared + ( -value1.X + 3.0f * value2.X - 3.0f * value3.X + value4.X ) * amountCubed );
			result.Y = 0.5f * ( 2.0f * value2.Y + ( -value1.Y + value3.Y ) * amount + ( 2.0f * value1.Y - 5.0f * value2.Y + 4.0f * value3.Y - value4.Y ) * amountSquared + ( -value1.Y + 3.0f * value2.Y - 3.0f * value3.Y + value4.Y ) * amountCubed );
			result.Z = 0.5f * ( 2.0f * value2.Z + ( -value1.Z + value3.Z ) * amount + ( 2.0f * value1.Z - 5.0f * value2.Z + 4.0f * value3.Z - value4.Z ) * amountSquared + ( -value1.Z + 3.0f * value2.Z - 3.0f * value3.Z + value4.Z ) * amountCubed );
			result.W = 0.5f * ( 2.0f * value2.W + ( -value1.W + value3.W ) * amount + ( 2.0f * value1.W - 5.0f * value2.W + 4.0f * value3.W - value4.W ) * amountSquared + ( -value1.W + 3.0f * value2.W - 3.0f * value3.W + value4.W ) * amountCubed );
		}

		/// <summary>Performs a Catmull-Rom interpolation.</summary>
		/// <param name="value1">The first value in the interpolation.</param>
		/// <param name="value2">The second value in the interpolation.</param>
		/// <param name="value3">The third value in the interpolation.</param>
		/// <param name="value4">The fourth value in the interpolation.</param>
		/// <param name="amount">The weighting factor.</param>
		/// <returns>Returns the result of the Catmull-Rom interpolation.</returns>
		public static Vector4 CatmullRom( Vector4 value1, Vector4 value2, Vector4 value3, Vector4 value4, float amount )
		{
			Vector4 result;
			CatmullRom( ref value1, ref value2, ref value3, ref value4, amount, out result );
			return result;
		}


		/// <summary>Performs a Hermite spline interpolation.</summary>
		/// <param name="position1">A source position.</param>
		/// <param name="tangent1">The tangent associated with the source position.</param>
		/// <param name="position2">Another source position.</param>
		/// <param name="tangent2">The tangent associated with the other source position.</param>
		/// <param name="amount">The weighting factor.</param>
		/// <param name="result">Receives the result of the Hermite spline interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "3#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "5#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Hermite( ref Vector4 position1, ref Vector4 tangent1, ref Vector4 position2, ref Vector4 tangent2, float amount, out Vector4 result )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;

			var amountSquared3 = 3.0f * amountSquared;
			var amountCubed2 = 2.0f * amountCubed;

			var a = amountCubed2 - amountSquared3 + 1.0f;
			var b = -amountCubed2 + amountSquared3;
			var c = amountCubed - 2.0f * amountSquared + amount;
			var d = amountCubed - amountSquared;

			result.X = position1.X * a + position2.X * b + tangent1.X * c + tangent2.X * d;
			result.Y = position1.Y * a + position2.Y * b + tangent1.Y * c + tangent2.Y * d;
			result.Z = position1.Z * a + position2.Z * b + tangent1.Z * c + tangent2.Z * d;
			result.W = position1.W * a + position2.W * b + tangent1.W * c + tangent2.W * d;
		}

		/// <summary>Performs a Hermite spline interpolation.</summary>
		/// <param name="position1">A source position.</param>
		/// <param name="tangent1">The tangent associated with the source position.</param>
		/// <param name="position2">Another source position.</param>
		/// <param name="tangent2">The tangent associated with the other source position.</param>
		/// <param name="amount">The weighting factor.</param>
		/// <returns>Returns the result of the Hermite spline interpolation.</returns>
		public static Vector4 Hermite( Vector4 position1, Vector4 tangent1, Vector4 position2, Vector4 tangent2, float amount )
		{
			Vector4 result;
			Hermite( ref position1, ref tangent1, ref position2, ref tangent2, amount, out result );
			return result;
		}

		#endregion


		#region Operators

		/// <summary><see cref="Vector2"/> conversion operator.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns a <see cref="Vector2"/> structure initializes with the X and Y components of the specified <see cref="Vector4"/>.</returns>
		public static explicit operator Vector2( Vector4 vector )
		{
			return vector.ToVector2();
		}


		/// <summary><see cref="Vector3"/> conversion operator.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns a <see cref="Vector3"/> structure initializes with the X, Y and Z components of the specified <see cref="Vector4"/>.</returns>
		public static explicit operator Vector3( Vector4 vector )
		{
			return vector.ToVector3();
		}


		/// <summary>Equality comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Vector4 vector, Vector4 other )
		{
			return vector.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Vector4 vector, Vector4 other )
		{
			return !vector.Equals( other );
		}


		/// <summary>Unary negation operator.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Vector4 operator -( Vector4 vector )
		{
			vector.Negate();
			return vector;
		}


		/// <summary>Inferiority comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <( Vector4 vector, Vector4 other )
		{
			return ( vector.X < other.X ) && ( vector.Y < other.Y ) && ( vector.Z < other.Z );
		}

		/// <summary>Inferiority or equality comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <=( Vector4 vector, Vector4 other )
		{
			return ( vector.X <= other.X ) && ( vector.Y <= other.Y ) && ( vector.Z <= other.Z );
		}


		/// <summary>Superiority comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >( Vector4 vector, Vector4 other )
		{
			return ( vector.X > other.X ) && ( vector.Y > other.Y ) && ( vector.Z > other.Z );
		}

		/// <summary>Superiority or equality comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >=( Vector4 vector, Vector4 other )
		{
			return ( vector.X >= other.X ) && ( vector.Y >= other.Y ) && ( vector.Z >= other.Z );
		}


		/// <summary>Addition operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Vector4 operator +( Vector4 vector, Vector4 other )
		{
			vector.X += other.X;
			vector.Y += other.Y;
			vector.Z += other.Z;
			vector.W += other.W;
			return vector;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Vector4 operator -( Vector4 vector, Vector4 other )
		{
			vector.X -= other.X;
			vector.Y -= other.Y;
			vector.Z -= other.Z;
			vector.W -= other.W;
			return vector;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Vector4 operator *( Vector4 vector, Vector4 other )
		{
			vector.X *= other.X;
			vector.Y *= other.Y;
			vector.Z *= other.Z;
			vector.W *= other.W;
			return vector;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns></returns>
		public static Vector4 operator *( Vector4 vector, float value )
		{
			vector.X *= value;
			vector.Y *= value;
			vector.Z *= value;
			vector.W *= value;
			return vector;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Vector4 operator *( float value, Vector4 vector )
		{
			vector.X *= value;
			vector.Y *= value;
			vector.Z *= value;
			vector.W *= value;
			return vector;
		}


		/// <summary>Division operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Vector4 operator /( Vector4 vector, Vector4 other )
		{
			vector.X /= other.X;
			vector.Y /= other.Y;
			vector.Z /= other.Z;
			vector.W /= other.W;
			return vector;
		}

		/// <summary>Division operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="value">A finite, non-zero, single-precision floating-point value.</param>
		/// <returns></returns>
		public static Vector4 operator /( Vector4 vector, float value )
		{
			var inv = 1.0f / value;
			vector.X *= inv;
			vector.Y *= inv;
			vector.Z *= inv;
			vector.W *= inv;
			return vector;
		}

		/// <summary>Division operator.</summary>
		/// <param name="value">A finite, non-zero, single-precision floating-point value.</param>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Vector4 operator /( float value, Vector4 vector )
		{
			vector.X = value / vector.X;
			vector.Y = value / vector.Y;
			vector.Z = value / vector.Z;
			vector.W = value / vector.W;
			return vector;
		}
		
		#endregion

	}

}