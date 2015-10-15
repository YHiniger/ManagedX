using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX
{

	/// <summary>A quaternion.</summary>
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct Quaternion : IEquatable<Quaternion>
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

		/// <summary>Initializes a new <see cref="Quaternion"/>.</summary>
		/// <param name="x">The X component; must be a finite number.</param>
		/// <param name="y">The Y component; must be a finite number.</param>
		/// <param name="z">The Z component; must be a finite number.</param>
		/// <param name="w">The W component; must be a finite number.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "w" )]
		public Quaternion( float x, float y, float z, float w )
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		/// <summary>Initializes a new <see cref="Quaternion"/>.</summary>
		/// <param name="xy">The X and Y components of the vector.</param>
		/// <param name="z">The Z component; must be a finite number.</param>
		/// <param name="w">The W component; must be a finite number.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "w" )]
		public Quaternion( Vector2 xy, float z, float w )
		{
			this.X = xy.X;
			this.Y = xy.Y;
			this.Z = z;
			this.W = w;
		}

		/// <summary>Initializes a new <see cref="Quaternion"/>.</summary>
		/// <param name="xyz">A valid <see cref="Vector3"/> containing the X, Y and Z components of the vector.</param>
		/// <param name="w">The W component; must be a finite number.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "w" )]
		public Quaternion( Vector3 xyz, float w )
		{
			this.X = xyz.X;
			this.Y = xyz.Y;
			this.Z = xyz.Z;
			this.W = w;
		}

		/// <summary>Initializes a new <see cref="Quaternion"/>.</summary>
		/// <param name="xyzw">A valid <see cref="Vector4"/>, containing components of the quaternion.</param>
		public Quaternion( Vector4 xyzw )
		{
			X = xyzw.X;
			Y = xyzw.Y;
			Z = xyzw.Z;
			W = xyzw.W;
		}

		/// <summary>Initializes a new <see cref="Quaternion"/>.</summary>
		/// <param name="xyzw">The value used for both <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components; must be a finite number.</param>
		public Quaternion( float xyzw )
		{
			this.W = this.Z = this.Y = this.X = xyzw;
		}

		#endregion



		/// <summary>Gets the square of the length of this <see cref="Quaternion"/>.
		/// <para>Note: this property is faster than <see cref="Length"/>, since it doesn't calculate the square root.</para>
		/// </summary>
		public float LengthSquared { get { return X * X + Y * Y + Z * Z + W * W; } }


		/// <summary>Gets the length of this <see cref="Quaternion"/>.</summary>
		public float Length { get { return XMath.Sqrt( X * X + Y * Y + Z * Z + W * W ); } }


		/// <summary>Normalizes this <see cref="Quaternion"/>.</summary>
		public void Normalize()
		{
			var length = XMath.Sqrt( X * X + Y * Y + Z * Z + W * W );
			if( length != 0.0f )
			{
				var scale = 1.0f / length;
				X *= scale;
				Y *= scale;
				Z *= scale;
				W *= scale; // really ?
			}
		}


		/// <summary>Inverts this <see cref="Quaternion"/>.</summary>
		public void Invert()
		{
			var length = this.Length;
			if( length != 0.0f )
			{
				var scale = 1.0f / length;
				X *= -scale;
				Y *= -scale;
				Z *= -scale;
				W *= scale;
			}
		}


		/// <summary>Inverts the sign of all this <see cref="Quaternion"/>'s components.</summary>
		public void Negate()
		{
			X = -X;
			Y = -Y;
			Z = -Z;
			W = -W;
		}


		/// <summary>Rotates the specified <see cref="Vector2"/>.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives the rotated <see cref="Vector2"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public void Transform( ref Vector2 vector, out Vector2 result )
		{
			var y2 = Y + Y;
			var z2 = Z + Z;
			
			var xSquared2 = X * ( X + X );
			var ySquared2 = Y * y2;
			var zSquared2 = Z * z2;
			var xy2 = X * y2;
			var wz2 = W * z2;

			result.X = vector.X * ( 1.0f - ySquared2 - zSquared2 ) + vector.Y * ( xy2 - wz2 );
			result.Y = vector.X * ( xy2 + wz2 ) + vector.Y * ( 1.0f - xSquared2 - zSquared2 );
		}

		/// <summary>Rotates and returns the specified <see cref="Vector2"/>.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the rotated <see cref="Vector2"/>.</returns>
		public Vector2 Transform( Vector2 vector )
		{
			Vector2 result;
			this.Transform( ref vector, out result );
			return result;
		}


		/// <summary>Rotates the specified <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the rotated <see cref="Vector3"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public void Transform( ref Vector3 vector, out Vector3 result )
		{
			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;

			var xSquared2 = X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var ySquared2 = Y * y2;
			var yz2 = Y * z2;
			var zSquared2 = Z * z2;
			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;

			result.X = vector.X * ( 1.0f - ySquared2 - zSquared2 ) + vector.Y * ( xy2 - wz2 ) + vector.Z * ( xz2 + wy2 );
			result.Y = vector.X * ( xy2 + wz2 ) + vector.Y * ( 1.0f - xSquared2 - zSquared2 ) + vector.Z * ( yz2 - wx2 );
			result.Z = vector.X * ( xz2 - wy2 ) + vector.Y * ( yz2 + wx2 ) + vector.Z * ( 1.0f - xSquared2 - ySquared2 );
		}

		/// <summary>Rotates and returns the specified <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the rotated <see cref="Vector3"/>.</returns>
		public Vector3 Transform( Vector3 vector )
		{
			Vector3 result;
			this.Transform( ref vector, out result );
			return result;
		}


		/// <summary>Rotates the specified <see cref="Vector4"/>.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="result">Receives the rotated <see cref="Vector4"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public void Transform( ref Vector4 vector, out Vector4 result )
		{
			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;

			var xSquared2 = X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var ySquared2 = Y * y2;
			var yz2 = Y * z2;
			var zSquared2 = Z * z2;
			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;

			result.X = vector.X * ( 1.0f - ySquared2 - zSquared2 ) + vector.Y * ( xy2 - wz2 ) + vector.Z * ( xz2 + wy2 );
			result.Y = vector.X * ( xy2 + wz2 ) + vector.Y * ( 1.0f - xSquared2 - zSquared2 ) + vector.Z * ( yz2 - wx2 );
			result.Z = vector.X * ( xz2 - wy2 ) + vector.Y * ( yz2 + wx2 ) + vector.Z * ( 1.0f - xSquared2 - ySquared2 );
			result.W = vector.W;
		}

		/// <summary>Rotates and returns the specified <see cref="Vector4"/>.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns the rotated <see cref="Vector4"/>.</returns>
		public Vector4 Transform( Vector4 vector )
		{
			Vector4 result;
			this.Transform( ref vector, out result );
			return result;
		}


		/// <summary>Returns a hash code for this <see cref="Quaternion"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Quaternion"/> structure.</returns>
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode() ^ this.W.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Quaternion"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Quaternion other )
		{
			return ( this.X == other.X ) && ( this.Y == other.Y ) && ( this.Z == other.Z ) && ( this.W == other.W );
		}


		/// <summary>Returns a value indicating whether this <see cref="Quaternion"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Quaternion"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Quaternion ) && this.Equals( (Quaternion)obj );
		}


		/// <summary>Returns a string representing this <see cref="Quaternion"/> structure, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>,<see cref="Z"/>,<see cref="W"/>)</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="Quaternion"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "({0},{1},{2},{3})", this.X, this.Y, this.Z, this.W );
		}


		/// <summary>Returns an array containing, respectively, the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components of this <see cref="Quaternion"/> structure.</summary>
		/// <returns>Returns an array containing the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components of this <see cref="Quaternion"/> structure.</returns>
		public float[] ToArray()
		{
			return new float[] { this.X, this.Y, this.Z, this.W };
		}


		/// <summary>Returns a <see cref="Vector4"/> structure initialized with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components of this <see cref="Quaternion"/>.</summary>
		/// <returns>Returns a <see cref="Vector4"/> structure initialized with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components of this <see cref="Quaternion"/>.</returns>
		public Vector4 ToVector4()
		{
			return new Vector4( X, Y, Z, W );
		}


		// TODO - Transform, TransformNormal

		/// <summary>A <see cref="Quaternion"/> representing no rotation.</summary>
		public static readonly Quaternion Identity = new Quaternion( 0.0f, 0.0f, 0.0f, 1.0f );


		/// <summary>Adds two <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives the sum of the two specified <see cref="Quaternion"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Add( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			result.X = quaternion.X + other.X;
			result.Y = quaternion.Y + other.Y;
			result.Z = quaternion.Z + other.Z;
			result.W = quaternion.W + other.W;
		}

		/// <summary>Returns the sum of two <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/>.</param>
		/// <param name="other">A valid <see cref="Quaternion"/>.</param>
		/// <returns>Returns the sum of the two specified <see cref="Quaternion"/>.</returns>
		public static Quaternion Add( Quaternion quaternion, Quaternion other )
		{
			quaternion.X += other.X;
			quaternion.Y += other.Y;
			quaternion.Z += other.Z;
			quaternion.W += other.W;
			return quaternion;
		}


		/// <summary>Subtracts a <see cref="Quaternion"/> (<paramref name="other"/>) from another <see cref="Quaternion"/> (<paramref name="quaternion"/>).</summary>
		/// <param name="quaternion">A <see cref="Quaternion"/>.</param>
		/// <param name="other">A <see cref="Quaternion"/>.</param>
		/// <param name="result">Receives the difference between <paramref name="quaternion"/> and <paramref name="other"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Subtract( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			result.X = quaternion.X - other.X;
			result.Y = quaternion.Y - other.Y;
			result.Z = quaternion.Z - other.Z;
			result.W = quaternion.W - other.W;
		}

		/// <summary>Subtracts a vector (<paramref name="other"/>) from another vector (<paramref name="quaternion"/>).</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/>.</param>
		/// <param name="other">A valid <see cref="Quaternion"/>.</param>
		/// <returns>Returns the difference between <paramref name="quaternion"/> and <paramref name="other"/>.</returns>
		public static Quaternion Subtract( Quaternion quaternion, Quaternion other )
		{
			quaternion.X -= other.X;
			quaternion.Y -= other.Y;
			quaternion.Z -= other.Z;
			quaternion.W -= other.W;
			return quaternion;
		}


		/// <summary>Multiplies two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives the result of ( <paramref name="quaternion"/> × <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Multiply( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			result.X = quaternion.X * other.X;
			result.Y = quaternion.Y * other.Y;
			result.Z = quaternion.Z * other.Z;
			result.W = quaternion.W * other.W;
		}

		/// <summary>Multiplies two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="quaternion"/> × <paramref name="other"/> ).</returns>
		public static Quaternion Multiply( Quaternion quaternion, Quaternion other )
		{
			quaternion.X *= other.X;
			quaternion.Y *= other.Y;
			quaternion.Z *= other.Z;
			quaternion.W *= other.W;
			return quaternion;
		}


		/// <summary>Divides a <see cref="Quaternion"/> by another <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives the result of ( <paramref name="quaternion"/> : <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Divide( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			result.X = quaternion.X / other.X;
			result.Y = quaternion.Y / other.Y;
			result.Z = quaternion.Z / other.Z;
			result.W = quaternion.W / other.W;
		}

		/// <summary>Divides a <see cref="Quaternion"/> by another <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="quaternion"/> : <paramref name="other"/> ).</returns>
		public static Quaternion Divide( Quaternion quaternion, Quaternion other )
		{
			quaternion.X /= other.X;
			quaternion.Y /= other.Y;
			quaternion.Z /= other.Z;
			quaternion.W /= other.W;
			return quaternion;
		}



		/// <summary>Retrieves a <see cref="Quaternion"/> structure whose components are set to the minimum components between two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives a <see cref="Quaternion"/> structure whose components are set to the minimum components between two <see cref="Quaternion"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Min( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			result.X = XMath.Min( quaternion.X, other.X );
			result.Y = XMath.Min( quaternion.Y, other.Y );
			result.Z = XMath.Min( quaternion.Z, other.Z );
			result.W = XMath.Min( quaternion.W, other.W );
		}

		/// <summary>Returns a <see cref="Quaternion"/> structure whose components are set to the minimum components between two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns a <see cref="Quaternion"/> structure whose components are set to the minimum components between two <see cref="Quaternion"/> values.</returns>
		public static Quaternion Min( Quaternion quaternion, Quaternion other )
		{
			return new Quaternion(
				XMath.Min( quaternion.X, other.X ),
				XMath.Min( quaternion.Y, other.Y ),
				XMath.Min( quaternion.Z, other.Z ),
				XMath.Min( quaternion.W, other.W )
			);
		}


		/// <summary>Retrieves a <see cref="Quaternion"/> structure whose components are set to the maximum components between two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives a <see cref="Quaternion"/> structure whose components are set to the maximum components between two <see cref="Quaternion"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Max( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			result.X = XMath.Max( quaternion.X, other.X );
			result.Y = XMath.Max( quaternion.Y, other.Y );
			result.Z = XMath.Max( quaternion.Z, other.Z );
			result.W = XMath.Max( quaternion.W, other.W );
		}

		/// <summary>Returns a <see cref="Quaternion"/> structure whose components are set to the maximum components between two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns a <see cref="Quaternion"/> structure whose components are set to the maximum components between two <see cref="Quaternion"/> values.</returns>
		public static Quaternion Max( Quaternion quaternion, Quaternion other )
		{
			return new Quaternion(
				XMath.Max( quaternion.X, other.X ),
				XMath.Max( quaternion.Y, other.Y ),
				XMath.Max( quaternion.Z, other.Z ),
				XMath.Max( quaternion.W, other.W )
			);
		}


		/// <summary>Calculates the dot product of two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> value.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> value.</param>
		/// <param name="result">Receives the dot product of the two specified <see cref="Quaternion"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Dot( ref Quaternion quaternion, ref Quaternion other, out float result )
		{
			result = quaternion.X * other.X + quaternion.Y * other.Y + quaternion.Z * other.Z + quaternion.W * other.W;
		}

		/// <summary>Calculates the dot product of two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> value.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> value.</param>
		/// <returns>Returns the dot product of the two specified <see cref="Quaternion"/> values.</returns>
		public static float Dot( Quaternion quaternion, Quaternion other )
		{
			return quaternion.X * other.X + quaternion.Y * other.Y + quaternion.Z * other.Z + quaternion.W * other.W;
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
		public static void Lerp( ref Quaternion source, ref Quaternion target, float amount, out Quaternion result )
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
		public static Quaternion Lerp( Quaternion source, Quaternion target, float amount )
		{
			return new Quaternion(
				XMath.Lerp( source.X, target.X, amount ),
				XMath.Lerp( source.Y, target.Y, amount ),
				XMath.Lerp( source.Z, target.Z, amount ),
				XMath.Lerp( source.W, target.W, amount )
			);
		}


		/// <summary></summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <param name="amount"></param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void SLerp( ref Quaternion source, ref Quaternion target, float amount, out Quaternion result )
		{
			var dotProduct = source.X * target.X + source.Y * target.Y + source.Z * target.Z + source.W * target.W;
			
			var signInverted = false;
			if( dotProduct < 0f )
			{
				signInverted = true;
				dotProduct = -dotProduct;
			}

			float srcFactor, tgtFactor;
			if( dotProduct > 0.999999f )
			{
				srcFactor = 1.0f - amount;
				tgtFactor = amount;
			}
			else
			{
				var angle = (float)Math.Acos( (double)dotProduct );
				var invSinAngle = (float)( 1.0 / Math.Sin( (double)angle ) );
				srcFactor = (float)Math.Sin( (double)( ( 1.0f - amount ) * angle ) ) * invSinAngle;
				tgtFactor = (float)Math.Sin( (double)( amount * angle ) ) * invSinAngle;
			}

			if( signInverted )
				tgtFactor = -tgtFactor;

			result.X = srcFactor * source.X + tgtFactor * target.X;
			result.Y = srcFactor * source.Y + tgtFactor * target.Y;
			result.Z = srcFactor * source.Z + tgtFactor * target.Z;
			result.W = srcFactor * source.W + tgtFactor * target.W;
		}

		/// <summary></summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		public static Quaternion SLerp( Quaternion source, Quaternion target, float amount )
		{
			Quaternion result;
			SLerp( ref source, ref target, amount, out result );
			return result;
		}


		/// <summary></summary>
		/// <param name="quaternion"></param>
		/// <param name="other"></param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Concatenate( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			var x = other.Y * quaternion.Z - other.Z * quaternion.Y;
			var y = other.Z * quaternion.X - other.X * quaternion.Z;
			var z = other.X * quaternion.Y - other.Y * quaternion.X;
			var w = other.X * quaternion.X + other.Y * quaternion.Y + other.Z * quaternion.Z;

			result.X = quaternion.X * other.W + other.X * quaternion.W + x;
			result.Y = quaternion.Y * other.W + other.Y * quaternion.W + y;
			result.Z = quaternion.Z * other.W + other.Z * quaternion.W + z;
			result.W = other.W * quaternion.W - w;
		}

		/// <summary></summary>
		/// <param name="quaternion"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static Quaternion Concatenate( Quaternion quaternion, Quaternion other )
		{
			Quaternion result;
			Concatenate( ref quaternion, ref other, out result );
			return result;
		}


		/// <summary></summary>
		/// <param name="quaternion"></param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Conjugate( ref Quaternion quaternion, out Quaternion result )
		{
			result.X = -quaternion.X;
			result.Y = -quaternion.Y;
			result.Z = -quaternion.Z;
			result.W = quaternion.W;
		}

		/// <summary></summary>
		/// <param name="quaternion"></param>
		/// <returns></returns>
		public static Quaternion Conjugate( Quaternion quaternion )
		{
			Conjugate( ref quaternion, out quaternion );
			return quaternion;
			//return new Quaternion(
			//	-quaternion.X,
			//	-quaternion.Y,
			//	-quaternion.Z,
			//	quaternion.W
			//);
		}


		/// <summary></summary>
		/// <param name="quaternion"></param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Inverse( ref Quaternion quaternion, out Quaternion result )
		{
			result = quaternion;
			result.Invert(); // TODO - make sure this doesn't affect quaternion !

			//var length = result.Length;
			//if( length != 0.0f )
			//{
			//	var scale = 1.0f / length;
			//	result.X *= -scale;
			//	result.Y *= -scale;
			//	result.Z *= -scale;
			//	result.W *= scale;
			//}
		}

		/// <summary></summary>
		/// <param name="quaternion"></param>
		/// <returns></returns>
		public static Quaternion Inverse( Quaternion quaternion )
		{
			quaternion.Invert();
			return quaternion;
		}


		// TODO - CreateFrom*


		#region Operators

		/// <summary><see cref="Vector3"/> conversion operator.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns a <see cref="Vector3"/> structure initializes with the X, Y and Z components of the specified <see cref="Vector4"/>.</returns>
		public static explicit operator Vector3( Quaternion vector )
		{
			return new Vector3( vector.X, vector.Y, vector.Z );
		}


		/// <summary><see cref="Quaternion"/> conversion operator.</summary>
		/// <param name="vector">A <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns a <see cref="Vector4"/> structure initializes with the X, Y and Z components of the specified <see cref="Quaternion"/>.</returns>
		public static explicit operator Vector4( Quaternion vector )
		{
			return vector.ToVector4();
		}


		/// <summary>Equality comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Quaternion vector, Quaternion other )
		{
			return vector.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Quaternion vector, Quaternion other )
		{
			return !vector.Equals( other );
		}


		/// <summary>Unary negation operator.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator -( Quaternion vector )
		{
			vector.Negate();
			return vector;
		}


		/// <summary>Inferiority comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <( Quaternion vector, Quaternion other )
		{
			return ( vector.X < other.X ) && ( vector.Y < other.Y ) && ( vector.Z < other.Z );
		}

		/// <summary>Inferiority or equality comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <=( Quaternion vector, Quaternion other )
		{
			return ( vector.X <= other.X ) && ( vector.Y <= other.Y ) && ( vector.Z <= other.Z );
		}


		/// <summary>Superiority comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >( Quaternion vector, Quaternion other )
		{
			return ( vector.X > other.X ) && ( vector.Y > other.Y ) && ( vector.Z > other.Z );
		}

		/// <summary>Superiority or equality comparer.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >=( Quaternion vector, Quaternion other )
		{
			return ( vector.X >= other.X ) && ( vector.Y >= other.Y ) && ( vector.Z >= other.Z );
		}


		/// <summary>Addition operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator +( Quaternion vector, Quaternion other )
		{
			return new Quaternion( vector.X + other.X, vector.Y + other.Y, vector.Z + other.Z, vector.W + other.W );
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator -( Quaternion vector, Quaternion other )
		{
			return new Quaternion( vector.X - other.X, vector.Y - other.Y, vector.Z - other.Z, vector.W - other.W );
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator *( Quaternion vector, Quaternion other )
		{
			return new Quaternion( vector.X * other.X, vector.Y * other.Y, vector.Z * other.Z, vector.W * other.W );
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns></returns>
		public static Quaternion operator *( Quaternion vector, float value )
		{
			return new Quaternion( vector.X * value, vector.Y * value, vector.Z * value, vector.W * value );
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator *( float value, Quaternion vector )
		{
			return new Quaternion( vector.X * value, vector.Y * value, vector.Z * value, vector.W * value );
		}


		/// <summary>Division operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator /( Quaternion vector, Quaternion other )
		{
			return new Quaternion( vector.X / other.X, vector.Y / other.Y, vector.Z / other.Z, vector.W / other.W );
		}

		/// <summary>Division operator.</summary>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="value">A finite, non-zero, single-precision floating-point value.</param>
		/// <returns></returns>
		public static Quaternion operator /( Quaternion vector, float value )
		{
			var inv = 1.0f / value;
			return new Quaternion( vector.X * inv, vector.Y * inv, vector.Z * inv, vector.W * inv );
		}

		/// <summary>Division operator.</summary>
		/// <param name="value">A finite, non-zero, single-precision floating-point value.</param>
		/// <param name="vector">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator /( float value, Quaternion vector )
		{
			return new Quaternion( value / vector.X, value / vector.Y, value / vector.Z, value / vector.W );
		}

		#endregion

	}

}