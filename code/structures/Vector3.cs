using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX
{
	
	/// <summary>A 3D vector.</summary>
	[Serializable]
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

		#endregion // Constructors



		/// <summary>Gets the length of this <see cref="Vector3"/>.</summary>
		public float Length { get { return (float)Math.Sqrt( X * X + Y * Y + Z * Z ); } }


		/// <summary>Gets the square of the length of this <see cref="Vector3"/> value.</summary>
		public float LengthSquared { get { return X * X + Y * Y + Z * Z; } }


		/// <summary>Normalizes this <see cref="Vector3"/> value.</summary>
		public void Normalize()
		{
			var length = (float)Math.Sqrt( X * X + Y * Y + Z * Z ); ;
			if( length != 0.0f )
			{
				var inv = 1.0f / length;
				X *= inv;
				Y *= inv;
				Z *= inv;
			}
		}


		/// <summary>Inverts the sign of this <see cref="Vector3"/>'s components.</summary>
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
			return (float)Math.Sqrt( x * x + y * y + z * z );
		}


		/// <summary>Forces the components of this <see cref="Vector3"/> to the range [<paramref name="min"/>,<paramref name="max"/>].</summary>
		/// <param name="min">A valid <see cref="Vector3"/> structure containing the minimum value for each component.</param>
		/// <param name="max">A valid <see cref="Vector3"/> structure containing the maximum value for each component.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		public void Clamp( ref Vector3 min, ref Vector3 max )
		{
			X = X.Clamp( min.X, max.X );
			Y = Y.Clamp( min.Y, max.Y );
			Z = Z.Clamp( min.Z, max.Z );
		}


		/// <summary>Returns a hash code for this <see cref="Vector3"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Vector3"/> structure.</returns>
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


		/// <summary>Returns a string representing this <see cref="Vector3"/> structure, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>,<see cref="Z"/>)</para>
		/// </summary>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>Returns a string representing this <see cref="Vector3"/> structure.</returns>
		public string ToString( IFormatProvider formatProvider )
		{
			return string.Format( formatProvider ?? System.Globalization.CultureInfo.InvariantCulture, "({0},{1},{2})", X, Y, Z );
		}


		/// <summary>Returns a string representing this <see cref="Vector3"/> structure, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>,<see cref="Z"/>)</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="Vector3"/> structure.</returns>
		public override string ToString()
		{
			return this.ToString( System.Globalization.CultureInfo.InvariantCulture );
		}

		
		/// <summary>Returns an array containing, respectively, the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> components of this <see cref="Vector3"/> structure.</summary>
		/// <returns>Returns an array containing, respectively, the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> components of this <see cref="Vector3"/> structure.</returns>
		public float[] ToArray()
		{
			return new float[] { X, Y, Z };
		}

		
		/// <summary>Returns a <see cref="Vector2"/> structure initialized with the <see cref="X"/> and <see cref="Y"/> components of this <see cref="Vector3"/>.</summary>
		/// <returns>Returns a <see cref="Vector2"/> structure initialized with the <see cref="X"/> and <see cref="Y"/> components of this <see cref="Vector3"/>.</returns>
		public Vector2 ToVector2()
		{
			return new Vector2( X, Y );
		}

		
		#region Static

		/// <summary>A <see cref="Vector3"/> whose components are set to 0.</summary>
		public static readonly Vector3 Zero = new Vector3();

		/// <summary>A <see cref="Vector3"/> whose components are set to 1.</summary>
		public static readonly Vector3 One = new Vector3( 1.0f, 1.0f, 1.0f );

		/// <summary>Unit vector pointing to the left: (-1,0,0).</summary>
		public static readonly Vector3 Left = new Vector3( -1.0f, 0.0f, 0.0f );

		/// <summary>Unit vector pointing to the right: (1,0,0).</summary>
		public static readonly Vector3 Right = new Vector3( 1.0f, 0.0f, 0.0f );

		/// <summary>Unit vector pointing upward: (0,1,0).</summary>
		public static readonly Vector3 Up = new Vector3( 0.0f, 1.0f, 0.0f );

		/// <summary>Unit vector pointing downward: (0,-1,0).</summary>
		public static readonly Vector3 Down = new Vector3( 0.0f, -1.0f, 0.0f );

		/// <summary>Unit vector pointing forward: (0,0,-1).</summary>
		public static readonly Vector3 Forward = new Vector3( 0.0f, 0.0f, -1.0f );

		/// <summary>Unit vector pointing backward: (0,0,1).</summary>
		public static readonly Vector3 Backward = new Vector3( 0.0f, 0.0f, 1.0f );

		

		/// <summary>Returns the sum of two <see cref="Vector3"/>.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/>.</param>
		/// <param name="other">A valid <see cref="Vector3"/>.</param>
		/// <param name="result">Receives the sum of the two specified <see cref="Vector3"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Add( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			result.X = vector.X + other.X;
			result.Y = vector.Y + other.Y;
			result.Z = vector.Z + other.Z;
		}

		/// <summary>Returns the sum of two <see cref="Vector3"/>.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/>.</param>
		/// <param name="other">A valid <see cref="Vector3"/>.</param>
		/// <returns>Returns the sum of the two specified <see cref="Vector3"/>.</returns>
		public static Vector3 Add( Vector3 vector, Vector3 other )
		{
			vector.X += other.X;
			vector.Y += other.Y;
			vector.Z += other.Z;
			return vector;
		}


		/// <summary>Subtracts a vector (<paramref name="other"/>) from another vector (<paramref name="vector"/>).</summary>
		/// <param name="vector">A valid <see cref="Vector3"/>.</param>
		/// <param name="other">A valid <see cref="Vector3"/>.</param>
		/// <param name="result">Receives the difference between <paramref name="vector"/> and <paramref name="other"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Subtract( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			result.X = vector.X - other.X;
			result.Y = vector.Y - other.Y;
			result.Z = vector.Z - other.Z;
		}

		/// <summary>Subtracts a vector (<paramref name="other"/>) from another vector (<paramref name="vector"/>).</summary>
		/// <param name="vector">A valid <see cref="Vector3"/>.</param>
		/// <param name="other">A valid <see cref="Vector3"/>.</param>
		/// <returns>Returns the difference between <paramref name="vector"/> and <paramref name="other"/>.</returns>
		public static Vector3 Subtract( Vector3 vector, Vector3 other )
		{
			vector.X -= other.X;
			vector.Y -= other.Y;
			vector.Z -= other.Z;
			return vector;
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
			result.X = vector.X * other.X;
			result.Y = vector.Y * other.Y;
			result.Z = vector.Z * other.Z;
		}

		/// <summary>Multiplies two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> × <paramref name="other"/> ).</returns>
		public static Vector3 Multiply( Vector3 vector, Vector3 other )
		{
			vector.X *= other.X;
			vector.Y *= other.Y;
			vector.Z *= other.Z;
			return vector;
		}

		/// <summary>Multiplies two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> × <paramref name="value"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Multiply( ref Vector3 vector, float value, out Vector3 result )
		{
			result.X = vector.X * value;
			result.Y = vector.Y * value;
			result.Z = vector.Z * value;
		}

		/// <summary>Multiplies two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="value">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> × <paramref name="value"/> ).</returns>
		public static Vector3 Multiply( Vector3 vector, float value )
		{
			vector.X *= value;
			vector.Y *= value;
			vector.Z *= value;
			return vector;
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
			result.X = vector.X / other.X;
			result.Y = vector.Y / other.Y;
			result.Z = vector.Z / other.Z;
		}
		
		/// <summary>Divides a <see cref="Vector3"/> by another <see cref="Vector3"/>.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> : <paramref name="other"/> ).</returns>
		public static Vector3 Divide( Vector3 vector, Vector3 other )
		{
			vector.X /= other.X;
			vector.Y /= other.Y;
			vector.Z /= other.Z;
			return vector;
		}

		/// <summary>Divides a <see cref="Vector3"/> by a value.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> : <paramref name="value"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Divide( ref Vector3 vector, float value, out Vector3 result )
		{
			var inv = 1.0f / value;
			result.X = vector.X * inv;
			result.Y = vector.Y * inv;
			result.Z = vector.Z * inv;
		}

		/// <summary>Divides a <see cref="Vector3"/> by a value.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> : <paramref name="value"/> ).</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Vector3 Divide( Vector3 vector, float value )
		{
			var inv = 1.0f / value;
			vector.X *= inv;
			vector.Y *= inv;
			vector.Z *= inv;
			return vector;
		}


		/// <summary>Retrieves a <see cref="Vector3"/> structure whose components are set to the minimum components between two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vector3"/> structure whose components are set to the minimum components between two <see cref="Vector3"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Min( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			result.X = Math.Min( vector.X, other.X );
			result.Y = Math.Min( vector.Y, other.Y );
			result.Z = Math.Min( vector.Z, other.Z );
		}

		/// <summary>Returns a <see cref="Vector3"/> structure whose components are set to the minimum components between two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns a <see cref="Vector3"/> structure whose components are set to the minimum components between two <see cref="Vector3"/> values.</returns>
		public static Vector3 Min( Vector3 vector, Vector3 other )
		{
			return new Vector3(
				Math.Min( vector.X, other.X ),
				Math.Min( vector.Y, other.Y ),
				Math.Min( vector.Z, other.Z )
			);
		}


		/// <summary>Retrieves a <see cref="Vector3"/> structure whose components are set to the maximum components between two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vector3"/> structure whose components are set to the maximum components between two <see cref="Vector3"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Max( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			result.X = Math.Max( vector.X, other.X );
			result.Y = Math.Max( vector.Y, other.Y );
			result.Z = Math.Max( vector.Z, other.Z );
		}

		/// <summary>Returns a <see cref="Vector3"/> structure whose components are set to the maximum components between two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns a <see cref="Vector3"/> structure whose components are set to the maximum components between two <see cref="Vector3"/> values.</returns>
		public static Vector3 Max( Vector3 vector, Vector3 other )
		{
			return new Vector3(
				Math.Max( vector.X, other.X ),
				Math.Max( vector.Y, other.Y ),
				Math.Max( vector.Z, other.Z )
			);
		}

	
		/// <summary>Calculates the distance between two <see cref="Vector3"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the distance between the two specified positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Distance( ref Vector3 position, ref Vector3 other, out float result )
		{
			var x = position.X - other.X;
			var y = position.Y - other.Y;
			var z = position.Z - other.Z;
			result = (float)Math.Sqrt( x * x + y * y + z * z );
		}

		/// <summary>Returns the distance between two <see cref="Vector3"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the distance between two <see cref="Vector3"/> positions.</returns>
		public static float Distance( Vector3 position, Vector3 other )
		{
			var x = position.X - other.X;
			var y = position.Y - other.Y;
			var z = position.Z - other.Z;
			return (float)Math.Sqrt( x * x + y * y + z * z );
		}


		/// <summary>Calculates the square of the distance between two <see cref="Vector3"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector3"/> value.</param>
		/// <param name="other">A valid <see cref="Vector3"/> value.</param>
		/// <param name="result">Receives the square of the distance between the two specified positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void DistanceSquared( ref Vector3 position, ref Vector3 other, out float result )
		{
			var x = position.X - other.X;
			var y = position.Y - other.Y;
			var z = position.Z - other.Z;
			result = x * x + y * y + z * z;
		}

		/// <summary>Returns the square of the distance between two <see cref="Vector3"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector3"/> value.</param>
		/// <param name="other">A valid <see cref="Vector3"/> value.</param>
		/// <returns>Returns the square of the distance between the two specified positions.</returns>
		public static float DistanceSquared( Vector3 position, Vector3 other )
		{
			var x = position.X - other.X;
			var y = position.Y - other.Y;
			var z = position.Z - other.Z;
			return x * x + y * y + z * z;
		}


		/// <summary>Calculates the dot product of two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> value.</param>
		/// <param name="other">A valid <see cref="Vector3"/> value.</param>
		/// <param name="result">Receives the dot product of the two specified <see cref="Vector3"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Dot( ref Vector3 vector, ref Vector3 other, out float result )
		{
			result = vector.X * other.X + vector.Y * other.Y + vector.Z * other.Z;
		}

		/// <summary>Calculates the dot product of two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> value.</param>
		/// <param name="other">A valid <see cref="Vector3"/> value.</param>
		/// <returns>Returns the dot product of the two specified <see cref="Vector3"/> values.</returns>
		public static float Dot( Vector3 vector, Vector3 other )
		{
			return vector.X * other.X + vector.Y * other.Y + vector.Z * other.Z;
		}


		/// <summary>Determines the reflect vector of the given vector and normal.</summary>
		/// <param name="vector">Source vector.</param>
		/// <param name="normal">Normal of vector.</param>
		/// <param name="result">Receives the reflected vector.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Reflect( ref Vector3 vector, ref Vector3 normal, out Vector3 result )
		{
			var dot2 = ( vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z ) * 2.0f;
			
			result.X = vector.X - dot2 * normal.X;
			result.Y = vector.Y - dot2 * normal.Y;
			result.Z = vector.Z - dot2 * normal.Z;
		}

		/// <summary>Determines the reflect vector of the given vector and normal.</summary>
		/// <param name="vector">The source vector.</param>
		/// <param name="normal">The normal of vector.</param>
		/// <returns>Returns the reflected vector.</returns>
		public static Vector3 Reflect( Vector3 vector, Vector3 normal )
		{
			var dot2 = ( vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z ) * 2.0f;

			return new Vector3(
				vector.X - dot2 * normal.X,
				vector.Y - dot2 * normal.Y,
				vector.Z - dot2 * normal.Z
			);
		}


		/// <summary>Performs a linear interpolation between two vectors.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <param name="result">Receives the result of the linear interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Performance matters." )]
		public static void Lerp( ref Vector3 source, ref Vector3 target, float amount, out Vector3 result )
		{
			result.X = XMath.Lerp( source.X, target.X, amount );
			result.Y = XMath.Lerp( source.Y, target.Y, amount );
			result.Z = XMath.Lerp( source.Z, target.Z, amount );
		}

		/// <summary>Performs a linear interpolation between two vectors.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <returns>Returns the result of the linear interpolation.</returns>
		public static Vector3 Lerp( Vector3 source, Vector3 target, float amount )
		{
			return new Vector3(
				XMath.Lerp( source.X, target.X, amount ),
				XMath.Lerp( source.Y, target.Y, amount ),
				XMath.Lerp( source.Z, target.Z, amount )
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
		public static void SmoothStep( ref Vector3 source, ref Vector3 target, float amount, out Vector3 result )
		{
			amount = amount.Saturate();
			amount = amount * amount * ( 3.0f - 2.0f * amount );
			result.X = XMath.Lerp( source.X, target.X, amount );
			result.Y = XMath.Lerp( source.Y, target.Y, amount );
			result.Z = XMath.Lerp( source.Z, target.Z, amount );
		}

		/// <summary>Interpolates between two values using a cubic equation.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; will be saturated.</param>
		/// <returns>Returns the result of the cubic interpolation.</returns>
		public static Vector3 SmoothStep( Vector3 source, Vector3 target, float amount )
		{
			amount = amount.Saturate();
			amount = amount * amount * ( 3.0f - 2.0f * amount );
			return new Vector3(
				XMath.Lerp( source.X, target.X, amount ),
				XMath.Lerp( source.Y, target.Y, amount ),
				XMath.Lerp( source.Z, target.Z, amount )
			);
		}


		/// <summary>Performs a Catmull-Rom interpolation between the specified values.</summary>
		/// <param name="value1">The first value in the interpolation.</param>
		/// <param name="value2">The second value in the interpolation.</param>
		/// <param name="value3">The third value in the interpolation.</param>
		/// <param name="value4">The fourth value in the interpolation.</param>
		/// <param name="amount">The weighting factor.</param>
		/// <param name="result">Receives the result of the interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "3#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "5#", Justification = "Performance matters." )]
		public static void CatmullRom( ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4, float amount, out Vector3 result )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;
			
			result.X = 0.5f * ( 2.0f * value2.X + ( -value1.X + value3.X ) * amount + ( 2.0f * value1.X - 5.0f * value2.X + 4.0f * value3.X - value4.X ) * amountSquared + ( -value1.X + 3.0f * value2.X - 3.0f * value3.X + value4.X ) * amountCubed );
			result.Y = 0.5f * ( 2.0f * value2.Y + ( -value1.Y + value3.Y ) * amount + ( 2.0f * value1.Y - 5.0f * value2.Y + 4.0f * value3.Y - value4.Y ) * amountSquared + ( -value1.Y + 3.0f * value2.Y - 3.0f * value3.Y + value4.Y ) * amountCubed );
			result.Z = 0.5f * ( 2.0f * value2.Z + ( -value1.Z + value3.Z ) * amount + ( 2.0f * value1.Z - 5.0f * value2.Z + 4.0f * value3.Z - value4.Z ) * amountSquared + ( -value1.Z + 3.0f * value2.Z - 3.0f * value3.Z + value4.Z ) * amountCubed );
		}

		/// <summary>Performs a Catmull-Rom interpolation.</summary>
		/// <param name="value1">The first value in the interpolation.</param>
		/// <param name="value2">The second value in the interpolation.</param>
		/// <param name="value3">The third value in the interpolation.</param>
		/// <param name="value4">The fourth value in the interpolation.</param>
		/// <param name="amount">The weighting factor.</param>
		/// <returns>Returns the result of the Catmull-Rom interpolation.</returns>
		public static Vector3 CatmullRom( Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, float amount )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;

			return new Vector3(
				0.5f * ( 2.0f * value2.X + ( -value1.X + value3.X ) * amount + ( 2.0f * value1.X - 5.0f * value2.X + 4.0f * value3.X - value4.X ) * amountSquared + ( -value1.X + 3.0f * value2.X - 3.0f * value3.X + value4.X ) * amountCubed ),
				0.5f * ( 2.0f * value2.Y + ( -value1.Y + value3.Y ) * amount + ( 2.0f * value1.Y - 5.0f * value2.Y + 4.0f * value3.Y - value4.Y ) * amountSquared + ( -value1.Y + 3.0f * value2.Y - 3.0f * value3.Y + value4.Y ) * amountCubed ),
				0.5f * ( 2.0f * value2.Z + ( -value1.Z + value3.Z ) * amount + ( 2.0f * value1.Z - 5.0f * value2.Z + 4.0f * value3.Z - value4.Z ) * amountSquared + ( -value1.Z + 3.0f * value2.Z - 3.0f * value3.Z + value4.Z ) * amountCubed )
			);
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
		public static void Hermite( ref Vector3 position1, ref Vector3 tangent1, ref Vector3 position2, ref Vector3 tangent2, float amount, out Vector3 result )
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
		}

		/// <summary>Performs a Hermite spline interpolation.</summary>
		/// <param name="position1">A source position.</param>
		/// <param name="tangent1">The tangent associated with the source position.</param>
		/// <param name="position2">Another source position.</param>
		/// <param name="tangent2">The tangent associated with the other source position.</param>
		/// <param name="amount">The weighting factor.</param>
		/// <returns>Returns the result of the Hermite spline interpolation.</returns>
		public static Vector3 Hermite( Vector3 position1, Vector3 tangent1, Vector3 position2, Vector3 tangent2, float amount )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;

			var amountSquared3 = 3.0f * amountSquared;
			var amountCubed2 = 2.0f * amountCubed;

			var a = amountCubed2 - amountSquared3 + 1.0f;
			var b = -amountCubed2 + amountSquared3;
			var c = amountCubed - 2.0f * amountSquared + amount;
			var d = amountCubed - amountSquared;

			return new Vector3(
				position1.X * a + position2.X * b + tangent1.X * c + tangent2.X * d,
				position1.Y * a + position2.Y * b + tangent1.Y * c + tangent2.Y * d,
				position1.Z * a + position2.Z * b + tangent1.Z * c + tangent2.Z * d
			);
		}


		/// <summary>Returns a <see cref="Vector3"/> containing the 3D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 3D triangle.</summary>
		/// <param name="value1">A <see cref="Vector3"/> containing the 3D Cartesian coordinates of vertex 1 of the triangle.</param>
		/// <param name="value2">A <see cref="Vector3"/> containing the 3D Cartesian coordinates of vertex 2 of the triangle.</param>
		/// <param name="value3">A <see cref="Vector3"/> containing the 3D Cartesian coordinates of vertex 3 of the triangle.</param>
		/// <param name="amount1">Barycentric coordinate b2, which expresses the weighting factor toward vertex 2 (specified in value2).</param>
		/// <param name="amount2">Barycentric coordinate b3, which expresses the weighting factor toward vertex 3 (specified in value3).</param>
		/// <param name="result">Receives a <see cref="Vector3"/> containing the 3D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 3D triangle.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "5#", Justification = "Performance matters." )]
		public static void Barycentric( ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, float amount1, float amount2, out Vector3 result )
		{
			result.X = value1.X + amount1 * ( value2.X - value1.X ) + amount2 * ( value3.X - value1.X );
			result.Y = value1.Y + amount1 * ( value2.Y - value1.Y ) + amount2 * ( value3.Y - value1.Y );
			result.Z = value1.Z + amount1 * ( value2.Z - value1.Z ) + amount2 * ( value3.Z - value1.Z );
		}

		/// <summary>Returns a <see cref="Vector3"/> containing the 3D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 3D triangle.</summary>
		/// <param name="value1">A <see cref="Vector3"/> containing the 3D Cartesian coordinates of vertex 1 of the triangle.</param>
		/// <param name="value2">A <see cref="Vector3"/> containing the 3D Cartesian coordinates of vertex 2 of the triangle.</param>
		/// <param name="value3">A <see cref="Vector3"/> containing the 3D Cartesian coordinates of vertex 3 of the triangle.</param>
		/// <param name="amount1">Barycentric coordinate b2, which expresses the weighting factor toward vertex 2 (specified in value2).</param>
		/// <param name="amount2">Barycentric coordinate b3, which expresses the weighting factor toward vertex 3 (specified in value3).</param>
		/// <returns>Returns a <see cref="Vector3"/> containing the 3D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 3D triangle.</returns>
		public static Vector3 Barycentric( Vector3 value1, Vector3 value2, Vector3 value3, float amount1, float amount2 )
		{
			return new Vector3(
				value1.X + amount1 * ( value2.X - value1.X ) + amount2 * ( value3.X - value1.X ),
				value1.Y + amount1 * ( value2.Y - value1.Y ) + amount2 * ( value3.Y - value1.Y ),
				value1.Z + amount1 * ( value2.Z - value1.Z ) + amount2 * ( value3.Z - value1.Z )
			);
		}


		/// <summary>Calculates the cross product of two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the cross product of the two specified <see cref="Vector3"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Cross( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			result.X = vector.Y * other.Z - vector.Z * other.Y;
			result.Y = vector.Z * other.X - vector.X * other.Z;
			result.Z = vector.X * other.Y - vector.Y * other.X;
		}

		/// <summary>Returns the cross product of two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the cross product of the two specified <see cref="Vector3"/> values.</returns>
		public static Vector3 Cross( Vector3 vector, Vector3 other )
		{
			return new Vector3(
				vector.Y * other.Z - vector.Z * other.Y,
				vector.Z * other.X - vector.X * other.Z,
				vector.X * other.Y - vector.Y * other.X
			);
		}

		#endregion // Static


		#region Operators

		/// <summary><see cref="Vector2"/> conversion operator.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns a <see cref="Vector2"/> structure initialized with the X and Y components of the specified <see cref="Vector3"/>.</returns>
		public static explicit operator Vector2( Vector3 vector )
		{
			return new Vector2(
				vector.X,
				vector.Y
			);
		}


		/// <summary>Equality comparer.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Vector3 vector, Vector3 other )
		{
			return vector.Equals( other );
		}

		/// <summary>Inequality comparer.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Vector3 vector, Vector3 other )
		{
			return !vector.Equals( other );
		}


		/// <summary>Inferiority comparer.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <( Vector3 vector, Vector3 other )
		{
			return ( vector.X < other.X ) && ( vector.Y < other.Y ) && ( vector.Z < other.Z );
		}

		/// <summary>Inferiority or equality comparer.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <=( Vector3 vector, Vector3 other )
		{
			return ( vector.X <= other.X ) && ( vector.Y <= other.Y ) && ( vector.Z <= other.Z );
		}

		/// <summary>Superiority comparer.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >( Vector3 vector, Vector3 other )
		{
			return ( vector.X > other.X ) && ( vector.Y > other.Y ) && ( vector.Z > other.Z );
		}

		/// <summary>Superiority or equality comparer.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >=( Vector3 vector, Vector3 other )
		{
			return ( vector.X >= other.X ) && ( vector.Y >= other.Y ) && ( vector.Z >= other.Z );
		}


		/// <summary>Unary negation operator.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator -( Vector3 vector )
		{
			vector.Negate();
			return vector;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator +( Vector3 vector, Vector3 other )
		{
			vector.X += other.X;
			vector.Y += other.Y;
			vector.Z += other.Z;
			return vector;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator -( Vector3 vector, Vector3 other )
		{
			vector.X -= other.X;
			vector.Y -= other.Y;
			vector.Z -= other.Z;
			return vector;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator *( Vector3 vector, Vector3 other )
		{
			vector.X *= other.X;
			vector.Y *= other.Y;
			vector.Z *= other.Z;
			return vector;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns></returns>
		public static Vector3 operator *( Vector3 vector, float value )
		{
			vector.X *= value;
			vector.Y *= value;
			vector.Z *= value;
			return vector;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator *( float value, Vector3 vector )
		{
			vector.X *= value;
			vector.Y *= value;
			vector.Z *= value;
			return vector;
		}


		/// <summary>Division operator.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator /( Vector3 vector, Vector3 other )
		{
			vector.X /= other.X;
			vector.Y /= other.Y;
			vector.Z /= other.Z;
			return vector;
		}

		/// <summary>Division operator.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite, non-zero, single-precision floating-point value.</param>
		/// <returns></returns>
		public static Vector3 operator /( Vector3 vector, float value )
		{
			var inv = 1.0f / value;
			vector.X *= inv;
			vector.Y *= inv;
			vector.Z *= inv;
			return vector;
		}

		/// <summary>Division operator.</summary>
		/// <param name="value">A finite, non-zero, single-precision floating-point value.</param>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator /( float value, Vector3 vector )
		{
			vector.X = value / vector.X;
			vector.Y = value / vector.Y;
			vector.Z = value / vector.Z;
			return vector;
		}

		#endregion // Operators

	}

}