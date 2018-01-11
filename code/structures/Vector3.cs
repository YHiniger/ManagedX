using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	using Win32;


	/// <summary>A 3D vector.</summary>
	[Source( "MFAPI.h", "MF_FLOAT3" )]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 12 )]
	public struct Vector3 : IEquatable<Vector3>
	{

		/// <summary>The X component of this <see cref="Vector3"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public float X;

		/// <summary>The Y component of this <see cref="Vector3"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public float Y;

		/// <summary>The Z component of this <see cref="Vector3"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public float Z;


		#region Constructors

		/// <summary>Initializes a new <see cref="Vector3"/>.</summary>
		/// <param name="x">The X component.</param>
		/// <param name="y">The Y component.</param>
		/// <param name="z">The Z component.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public Vector3( float x, float y, float z )
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>Initializes a new <see cref="Vector3"/>.</summary>
		/// <param name="xy">The X and Y components of the vector.</param>
		/// <param name="z">The Z component.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public Vector3( Vector2 xy, float z )
		{
			X = xy.X;
			Y = xy.Y;
			this.Z = z;
		}


		/// <summary>Initializes a new <see cref="Vector3"/>.</summary>
		/// <param name="xyz">The value used for both <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> components.</param>
		public Vector3( float xyz )
		{
			X = Y = Z = xyz;
		}

		#endregion Constructors



		/// <summary>Gets the length of this <see cref="Vector3"/>.</summary>
		public float Length { get { return (float)Math.Sqrt( (double)( X * X + Y * Y + Z * Z ) ); } }


		/// <summary>Gets the square of the length of this <see cref="Vector3"/> value.</summary>
		public float LengthSquared { get { return X * X + Y * Y + Z * Z; } }


		/// <summary>Normalizes this <see cref="Vector3"/> value.</summary>
		public void Normalize()
		{
			var oneOverLength = 1.0f / (float)Math.Sqrt( (double)( X * X + Y * Y + Z * Z ) );
			X *= oneOverLength;
			Y *= oneOverLength;
			Z *= oneOverLength;
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
			return (float)Math.Sqrt( (double)( x * x + y * y + z * z ) );
		}


		/// <summary>Forces the components of this <see cref="Vector3"/> within the range [<paramref name="min"/>,<paramref name="max"/>].</summary>
		/// <param name="min">A valid <see cref="Vector3"/> structure containing the minimum value for each component.</param>
		/// <param name="max">A valid <see cref="Vector3"/> structure containing the maximum value for each component.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		public void Clamp( ref Vector3 min, ref Vector3 max )
		{
			if( X < min.X )
				X = min.X;
			else if( X > max.X )
				X = max.X;

			if( Y < min.Y )
				Y = min.Y;
			else if( Y > max.Y )
				Y = max.Y;

			if( Z < min.Z )
				Z = min.Z;
			else if( Z > max.Z )
				Z = max.Z;
		}


		/// <summary>Forces the components of this <see cref="Vector3"/> within the range [0,1].</summary>
		public void Saturate()
		{
			if( X < 0.0f )
				X = 0.0f;
			else if( X > 1.0f )
				X = 1.0f;

			if( Y < 0.0f )
				Y = 0.0f;
			else if( Y > 1.0f )
				Y = 1.0f;

			if( Z < 0.0f )
				Z = 0.0f;
			else if( Z > 1.0f )
				Z = 1.0f;
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

		internal bool Equals( ref Vector3 other )
		{
			return ( X == other.X ) && ( Y == other.Y ) && ( Z == other.Z );
		}


		/// <summary>Returns a value indicating whether this <see cref="Vector3"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Vector3"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is Vector3 v && this.Equals( v );
		}


		/// <summary>Returns a string representing this <see cref="Vector3"/>, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>,<see cref="Z"/>)</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="Vector3"/>.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "({0},{1},{2})", X, Y, Z );
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



		/// <summary>The «zero», or «null», <see cref="Vector3"/>.</summary>
		public static readonly Vector3 Zero;

		/// <summary>A <see cref="Vector3"/> whose components are set to 1.</summary>
		public static readonly Vector3 One = new Vector3( 1.0f, 1.0f, 1.0f );

		/// <summary>Unit vector pointing to the left: (-1,0,0).</summary>
		public static readonly Vector3 Left = new Vector3( -1.0f, 0.0f, 0.0f );

		/// <summary>Unit vector pointing to the right: (1,0,0).
		/// <para>Also known as <see cref="UnitX"/>.</para>
		/// </summary>
		public static readonly Vector3 Right = new Vector3( 1.0f, 0.0f, 0.0f );

		/// <summary>Unit vector pointing upward: (0,1,0).
		/// <para>Also known as <see cref="UnitY"/>.</para>
		/// </summary>
		public static readonly Vector3 Up = new Vector3( 0.0f, 1.0f, 0.0f );

		/// <summary>Unit vector pointing downward: (0,-1,0).</summary>
		public static readonly Vector3 Down = new Vector3( 0.0f, -1.0f, 0.0f );

		/// <summary>Unit vector pointing forward: (0,0,-1).</summary>
		public static readonly Vector3 Forward = new Vector3( 0.0f, 0.0f, -1.0f );

		/// <summary>Unit vector pointing backward: (0,0,1).
		/// <para>Also known as <see cref="UnitZ"/>.</para>
		/// </summary>
		public static readonly Vector3 Backward = new Vector3( 0.0f, 0.0f, 1.0f );

		/// <summary>Unit vector pointing to the positive X direction.</summary>
		public static readonly Vector3 UnitX = Right;

		/// <summary>Unit vector pointing to the positive Y direction.</summary>
		public static readonly Vector3 UnitY = Up;

		/// <summary>Unit vector pointing to the positive Z direction.</summary>
		public static readonly Vector3 UnitZ = Backward;


		#region Static methods

		/// <summary>Calculates the sum of two <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the sum of the two specified <see cref="Vector3"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			result.X = vector.X + other.X;
			result.Y = vector.Y + other.Y;
			result.Z = vector.Z + other.Z;
		}

		/// <summary>Returns the sum of two <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the sum of the two specified <see cref="Vector3"/>.</returns>
		public static Vector3 Add( Vector3 vector, Vector3 other )
		{
			vector.X += other.X;
			vector.Y += other.Y;
			vector.Z += other.Z;
			return vector;
		}

		/// <summary>Calculates the sum of a <see cref="Vector3"/> and a value.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the sum of the two specified <see cref="Vector3"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref Vector3 vector, float value, out Vector3 result )
		{
			result.X = vector.X + value;
			result.Y = vector.Y + value;
			result.Z = vector.Z + value;
		}

		/// <summary>Returns the sum of a <see cref="Vector3"/> and a value.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the sum of the two specified <see cref="Vector3"/>.</returns>
		public static Vector3 Add( Vector3 vector, float value )
		{
			vector.X += value;
			vector.Y += value;
			vector.Z += value;
			return vector;
		}


		/// <summary>Calculates the difference between two vectors.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the difference between <paramref name="vector"/> and <paramref name="other"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			result.X = vector.X - other.X;
			result.Y = vector.Y - other.Y;
			result.Z = vector.Z - other.Z;
		}

		/// <summary>Subtracts a vector (<paramref name="other"/>) from another vector (<paramref name="vector"/>).</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the difference between <paramref name="vector"/> and <paramref name="other"/>.</returns>
		public static Vector3 Subtract( Vector3 vector, Vector3 other )
		{
			vector.X -= other.X;
			vector.Y -= other.Y;
			vector.Z -= other.Z;
			return vector;
		}

		/// <summary>Calculates the difference between a <see cref="Vector3"/> and a value.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the subtraction.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref Vector3 vector, float value, out Vector3 result )
		{
			result.X = vector.X - value;
			result.Y = vector.Y - value;
			result.Z = vector.Z - value;
		}

		/// <summary>Subtracts a value from a <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static Vector3 Subtract( Vector3 vector, float value )
		{
			vector.X = vector.X - value;
			vector.Y = vector.Y - value;
			vector.Z = vector.Z - value;
			return vector;
		}

		/// <summary>Subtracts a <see cref="Vector3"/> from a value.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the result of the subtraction.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( float value, ref Vector3 vector, out Vector3 result )
		{
			result.X = value - vector.X;
			result.Y = value - vector.Y;
			result.Z = value - vector.Z;
		}

		/// <summary>Subtracts a <see cref="Vector3"/> from a value.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static Vector3 Subtract( float value, Vector3 vector )
		{
			vector.X = value - vector.X;
			vector.Y = value - vector.Y;
			vector.Z = value - vector.Z;
			return vector;
		}


		/// <summary>Multiplies two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> × <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Multiply( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			result.X = vector.X * other.X;
			result.Y = vector.Y * other.Y;
			result.Z = vector.Z * other.Z;
		}

		/// <summary>Multiplies two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
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
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> × <paramref name="value"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Multiply( ref Vector3 vector, float value, out Vector3 result )
		{
			result.X = vector.X * value;
			result.Y = vector.Y * value;
			result.Z = vector.Z * value;
		}

		/// <summary>Multiplies two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> × <paramref name="value"/> ).</returns>
		public static Vector3 Multiply( Vector3 vector, float value )
		{
			vector.X *= value;
			vector.Y *= value;
			vector.Z *= value;
			return vector;
		}


		/// <summary>Divides a <see cref="Vector3"/> by another <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> : <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			result.X = vector.X / other.X;
			result.Y = vector.Y / other.Y;
			result.Z = vector.Z / other.Z;
		}
		
		/// <summary>Divides a <see cref="Vector3"/> by another <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
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
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> : <paramref name="value"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( ref Vector3 vector, float value, out Vector3 result )
		{
			value = 1.0f / value;
			result.X = vector.X * value;
			result.Y = vector.Y * value;
			result.Z = vector.Z * value;
		}

		/// <summary>Divides a <see cref="Vector3"/> by a value.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> : <paramref name="value"/> ).</returns>
		public static Vector3 Divide( Vector3 vector, float value )
		{
			value = 1.0f / value;
			vector.X *= value;
			vector.Y *= value;
			vector.Z *= value;
			return vector;
		}

		/// <summary>Divides a value by a <see cref="Vector3"/>.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the result of the division.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( float value, ref Vector3 vector, out Vector3 result )
		{
			result.X = value / vector.X;
			result.Y = value / vector.Y;
			result.Z = value / vector.Z;
		}

		/// <summary>Divides a <see cref="Vector3"/> by a value.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the result of the division.</returns>
		public static Vector3 Divide( float value, Vector3 vector )
		{
			vector.X = value / vector.X;
			vector.Y = value / vector.Y;
			vector.Z = value / vector.Z;
			return vector;
		}


		/// <summary>Retrieves a <see cref="Vector3"/> structure whose components are set to the minimum components between two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vector3"/> structure whose components are set to the minimum components between two <see cref="Vector3"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Min( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			if( other.X < vector.X )
				result.X = other.X;
			else
				result.X = vector.X;

			if( other.Y < vector.Y )
				result.Y = other.Y;
			else
				result.Y = vector.Y;

			if( other.Z < vector.Z )
				result.Z = other.Z;
			else
				result.Z = vector.Z;
		}

		/// <summary>Returns a <see cref="Vector3"/> structure whose components are set to the minimum components between two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns a <see cref="Vector3"/> structure whose components are set to the minimum components between two <see cref="Vector3"/> values.</returns>
		public static Vector3 Min( Vector3 vector, Vector3 other )
		{
			if( other.X < vector.X )
				vector.X = other.X;

			if( other.Y < vector.Y )
				vector.Y = other.Y;

			if( other.Z < vector.Z )
				vector.Z = other.Z;
			
			return vector;
		}


		/// <summary>Retrieves a <see cref="Vector3"/> structure whose components are set to the maximum components between two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vector3"/> structure whose components are set to the maximum components between two <see cref="Vector3"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Max( ref Vector3 vector, ref Vector3 other, out Vector3 result )
		{
			if( other.X > vector.X )
				result.X = other.X;
			else
				result.X = vector.X;

			if( other.Y > vector.Y )
				result.Y = other.Y;
			else
				result.Y = vector.Y;

			if( other.Z > vector.Z )
				result.Z = other.Z;
			else
				result.Z = vector.Z;
		}

		/// <summary>Returns a <see cref="Vector3"/> structure whose components are set to the maximum components between two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns a <see cref="Vector3"/> structure whose components are set to the maximum components between two <see cref="Vector3"/> values.</returns>
		public static Vector3 Max( Vector3 vector, Vector3 other )
		{
			if( other.X > vector.X )
				vector.X = other.X;

			if( other.Y > vector.Y )
				vector.Y = other.Y;

			if( other.Z > vector.Z )
				vector.Z = other.Z;

			return vector;
		}


		/// <summary>Calculates the distance between two <see cref="Vector3"/> positions.</summary>
		/// <param name="position">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the distance between the two specified positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Distance( ref Vector3 position, ref Vector3 other, out float result )
		{
			var x = position.X - other.X;
			var y = position.Y - other.Y;
			var z = position.Z - other.Z;
			result = (float)Math.Sqrt( (double)( x * x + y * y + z * z ) );
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
			return (float)Math.Sqrt( (double)( x * x + y * y + z * z ) );
		}


		/// <summary>Calculates the square of the distance between two <see cref="Vector3"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector3"/> value.</param>
		/// <param name="other">A valid <see cref="Vector3"/> value.</param>
		/// <param name="result">Receives the square of the distance between the two specified positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
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


		/// <summary>Normalizes a <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the normalized <paramref name="vector"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Normalize( ref Vector3 vector, out Vector3 result )
		{
			var oneOverLength = 1.0f / (float)Math.Sqrt( (double)( vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z ) );
			result.X = vector.X * oneOverLength;
			result.Y = vector.Y * oneOverLength;
			result.Z = vector.Z * oneOverLength;
		}

		/// <summary>Normalizes a <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the normalized <paramref name="vector"/>.</returns>
		public static Vector3 Normalize( Vector3 vector )
		{
			var oneOverLength = 1.0f / (float)Math.Sqrt( (double)( vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z ) );
			vector.X *= oneOverLength;
			vector.Y *= oneOverLength;
			vector.Z *= oneOverLength;
			return vector;
		}

	
		/// <summary>Calculates the dot product of two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> value.</param>
		/// <param name="other">A valid <see cref="Vector3"/> value.</param>
		/// <param name="result">Receives the dot product of the two specified <see cref="Vector3"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
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
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
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

			vector.X -= dot2 * normal.X;
			vector.Y -= dot2 * normal.Y;
			vector.Z -= dot2 * normal.Z;
			
			return vector;
		}


		/// <summary>Performs a linear interpolation between two vectors.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <param name="result">Receives the result of the linear interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Lerp( ref Vector3 source, ref Vector3 target, float amount, out Vector3 result )
		{
			result.X = source.X + ( target.X - source.X ) * amount;
			result.Y = source.Y + ( target.Y - source.Y ) * amount;
			result.Z = source.Z + ( target.Z - source.Z ) * amount;
		}

		/// <summary>Performs a linear interpolation between two vectors.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <returns>Returns the result of the linear interpolation.</returns>
		public static Vector3 Lerp( Vector3 source, Vector3 target, float amount )
		{
			source.X += ( target.X - source.X ) * amount;
			source.Y += ( target.Y - source.Y ) * amount;
			source.Z += ( target.Z - source.Z ) * amount;
			return source;
		}


		/// <summary>Interpolates between two values using a cubic equation.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; will be saturated.</param>
		/// <param name="result">Receives the result of the cubic interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void SmoothStep( ref Vector3 source, ref Vector3 target, float amount, out Vector3 result )
		{
			if( amount < 0.0f )
				amount = 0.0f;
			else if( amount > 1.0f )
				amount = 1.0f;
			
			amount = amount * amount * ( 3.0f - 2.0f * amount );
			
			result.X = source.X + ( target.X - source.X ) * amount;
			result.Y = source.Y + ( target.Y - source.Y ) * amount;
			result.Z = source.Z + ( target.Z - source.Z ) * amount;
		}

		/// <summary>Interpolates between two values using a cubic equation.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; will be saturated.</param>
		/// <returns>Returns the result of the cubic interpolation.</returns>
		public static Vector3 SmoothStep( Vector3 source, Vector3 target, float amount )
		{
			if( amount < 0.0f )
				amount = 0.0f;
			else if( amount > 1.0f )
				amount = 1.0f;
			
			amount = amount * amount * ( 3.0f - 2.0f * amount );
			
			source.X += ( target.X - source.X ) * amount;
			source.Y += ( target.Y - source.Y ) * amount;
			source.Z += ( target.Z - source.Z ) * amount;
			return source;
		}


        /// <summary>Performs a Catmull-Rom interpolation between the specified values.</summary>
        /// <param name="value1">The first value in the interpolation.</param>
        /// <param name="value2">The second value in the interpolation.</param>
        /// <param name="value3">The third value in the interpolation.</param>
        /// <param name="value4">The fourth value in the interpolation.</param>
        /// <param name="amount">The weighting factor.</param>
        /// <param name="result">Receives the result of the interpolation.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Catmull")]
        [SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
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
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Catmull")]
        public static Vector3 CatmullRom( Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, float amount )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;

			value1.X = 0.5f * ( 2.0f * value2.X + ( -value1.X + value3.X ) * amount + ( 2.0f * value1.X - 5.0f * value2.X + 4.0f * value3.X - value4.X ) * amountSquared + ( -value1.X + 3.0f * value2.X - 3.0f * value3.X + value4.X ) * amountCubed );
			value1.Y = 0.5f * ( 2.0f * value2.Y + ( -value1.Y + value3.Y ) * amount + ( 2.0f * value1.Y - 5.0f * value2.Y + 4.0f * value3.Y - value4.Y ) * amountSquared + ( -value1.Y + 3.0f * value2.Y - 3.0f * value3.Y + value4.Y ) * amountCubed );
			value1.Z = 0.5f * ( 2.0f * value2.Z + ( -value1.Z + value3.Z ) * amount + ( 2.0f * value1.Z - 5.0f * value2.Z + 4.0f * value3.Z - value4.Z ) * amountSquared + ( -value1.Z + 3.0f * value2.Z - 3.0f * value3.Z + value4.Z ) * amountCubed );

			return value1;
		}


        /// <summary>Performs a Hermite spline interpolation.</summary>
        /// <param name="position1">A source position.</param>
        /// <param name="tangent1">The tangent associated with the source position.</param>
        /// <param name="position2">Another source position.</param>
        /// <param name="tangent2">The tangent associated with the other source position.</param>
        /// <param name="amount">The weighting factor.</param>
        /// <param name="result">Receives the result of the Hermite spline interpolation.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Hermite")]
        [SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Hermite( ref Vector3 position1, ref Vector3 tangent1, ref Vector3 position2, ref Vector3 tangent2, float amount, out Vector3 result )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;

			//var a = 2.0f * amountCubed - 3.0f * amountSquared + 1.0f;
			var b = -2.0f * amountCubed + 3.0f * amountSquared;
			var a = 1.0f - b;
			//var c = amountCubed - 2.0f * amountSquared + amount;
			var d = amountCubed - amountSquared;
			var c = d - amountSquared + amount;

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
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Hermite")]
        public static Vector3 Hermite( Vector3 position1, Vector3 tangent1, Vector3 position2, Vector3 tangent2, float amount )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;

			//var a = 2.0f * amountCubed - 3.0f * amountSquared + 1.0f;
			var b = -2.0f * amountCubed + 3.0f * amountSquared;
			var a = 1.0f - b;
			//var c = amountCubed - 2.0f * amountSquared + amount;
			var d = amountCubed - amountSquared;
			var c = d - amountSquared + amount;

			position1.X = position1.X * a + position2.X * b + tangent1.X * c + tangent2.X * d;
			position1.Y = position1.Y * a + position2.Y * b + tangent1.Y * c + tangent2.Y * d;
			position1.Z = position1.Z * a + position2.Z * b + tangent1.Z * c + tangent2.Z * d;

			return position1;
		}


        /// <summary>Returns a <see cref="Vector3"/> containing the 3D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 3D triangle.</summary>
        /// <param name="value1">A <see cref="Vector3"/> containing the 3D Cartesian coordinates of vertex 1 of the triangle.</param>
        /// <param name="value2">A <see cref="Vector3"/> containing the 3D Cartesian coordinates of vertex 2 of the triangle.</param>
        /// <param name="value3">A <see cref="Vector3"/> containing the 3D Cartesian coordinates of vertex 3 of the triangle.</param>
        /// <param name="amount1">Barycentric coordinate b2, which expresses the weighting factor toward vertex 2 (specified in value2).</param>
        /// <param name="amount2">Barycentric coordinate b3, which expresses the weighting factor toward vertex 3 (specified in value3).</param>
        /// <param name="result">Receives a <see cref="Vector3"/> containing the 3D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 3D triangle.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Barycentric")]
        [SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
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
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Barycentric")]
        public static Vector3 Barycentric( Vector3 value1, Vector3 value2, Vector3 value3, float amount1, float amount2 )
		{
			value1.X += amount1 * ( value2.X - value1.X ) + amount2 * ( value3.X - value1.X );
			value1.Y += amount1 * ( value2.Y - value1.Y ) + amount2 * ( value3.Y - value1.Y );
			value1.Z += amount1 * ( value2.Z - value1.Z ) + amount2 * ( value3.Z - value1.Z );
			return value1;
		}


		/// <summary>Forces a <see cref="Vector3"/> within a specified range.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="min">A <see cref="Vector3"/> structure containing the minimum value for each component.</param>
		/// <param name="max">A <see cref="Vector3"/> structure containing the maximum value for each component.</param>
		/// <param name="result">Receives the clamped <paramref name="vector"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Clamp( ref Vector3 vector, ref Vector3 min, ref Vector3 max, out Vector3 result )
		{
			if( vector.X < min.X )
				result.X = min.X;
			else if( vector.X > max.X )
				result.X = max.X;
			else
				result.X = vector.X;

			if( vector.Y < min.Y )
				result.Y = min.Y;
			else if( vector.Y > max.Y )
				result.Y = max.Y;
			else
				result.Y = vector.Y;

			if( vector.Z < min.Z )
				result.Z = min.Z;
			else if( vector.Z > max.Z )
				result.Z = max.Z;
			else
				result.Z = vector.Z;
		}

		/// <summary>Forces a <see cref="Vector3"/> within a specified range.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="min">A <see cref="Vector3"/> structure containing the minimum value for each component.</param>
		/// <param name="max">A <see cref="Vector3"/> structure containing the maximum value for each component.</param>
		/// <returns>Returns the clamped <paramref name="vector"/>.</returns>
		public static Vector3 Clamp( Vector3 vector, Vector3 min, Vector3 max )
		{
			if( vector.X < min.X )
				vector.X = min.X;
			else if( vector.X > max.X )
				vector.X = max.X;

			if( vector.Y < min.Y )
				vector.Y = min.Y;
			else if( vector.Y > max.Y )
				vector.Y = max.Y;

			if( vector.Z < min.Z )
				vector.Z = min.Z;
			else if( vector.Z > max.Z )
				vector.Z = max.Z;

			return vector;
		}


		/// <summary>Forces the components of a <see cref="Vector3"/> within the range [0,1].</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the saturated <paramref name="vector"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Saturate( ref Vector3 vector, out Vector3 result )
		{
			if( vector.X < 0.0f )
				result.X = 0.0f;
			else if( vector.X > 1.0f )
				result.X = 1.0f;
			else
				result.X = vector.X;

			if( vector.Y < 0.0f )
				result.Y = 0.0f;
			else if( vector.Y > 1.0f )
				result.Y = 1.0f;
			else
				result.Y = vector.Y;

			if( vector.Z < 0.0f )
				result.Z = 0.0f;
			else if( vector.Z > 1.0f )
				result.Z = 1.0f;
			else
				result.Z = vector.Z;
		}

		/// <summary>Forces the components of a <see cref="Vector3"/> within the range [0,1].</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the saturated <paramref name="vector"/>.</returns>
		public static Vector3 Saturate( Vector3 vector )
		{
			if( vector.X < 0.0f )
				vector.X = 0.0f;
			else if( vector.X > 1.0f )
				vector.X = 1.0f;

			if( vector.Y < 0.0f )
				vector.Y = 0.0f;
			else if( vector.Y > 1.0f )
				vector.Y = 1.0f;

			if( vector.Z < 0.0f )
				vector.Z = 0.0f;
			else if( vector.Z > 1.0f )
				vector.Z = 1.0f;
			
			return vector;
		}


		/// <summary>Calculates the cross product of two <see cref="Vector3"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the cross product of the two specified <see cref="Vector3"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
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
			var x = vector.X;
			var y = vector.Y;

			vector.X = y * other.Z - vector.Z * other.Y;
			vector.Y = vector.Z * other.X - x * other.Z;
			vector.Z = x * other.Y - y * other.X;

			return vector;
		}

		#endregion Static methods


		#region Operators

		/// <summary><see cref="Vector3"/> to <see cref="Vector2"/> conversion operator.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns a new <see cref="Vector2"/> structure initialized with the specified <paramref name="vector"/>.</returns>
		public static explicit operator Vector2( Vector3 vector )
		{
			return new Vector2( vector.X, vector.Y );
		}


		/// <summary><see cref="Vector2"/> to <see cref="Vector3"/> conversion operator.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns a new <see cref="Vector3"/> structure initialized with the specified <paramref name="vector"/>.</returns>
		public static explicit operator Vector3( Vector2 vector )
		{
			return new Vector3( vector, 0.0f );
		}


		/// <summary>Equality comparer.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Vector3 vector, Vector3 other )
		{
			return ( vector.X == other.X ) && ( vector.Y == other.Y ) && ( vector.Z == other.Z );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Vector3 vector, Vector3 other )
		{
			return ( vector.X != other.X ) || ( vector.Y != other.Y ) || ( vector.Z != other.Z );
		}


		/// <summary>Unary negation operator.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator -( Vector3 vector )
		{
			vector.X = -vector.X;
			vector.Y = -vector.Y;
			vector.Z = -vector.Z;
			return vector;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="other">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator +( Vector3 vector, Vector3 other )
		{
			vector.X += other.X;
			vector.Y += other.Y;
			vector.Z += other.Z;
			return vector;
		}

		/// <summary>Addition operator.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns></returns>
		public static Vector3 operator +( Vector3 vector, float value )
		{
			vector.X += value;
			vector.Y += value;
			vector.Z += value;
			return vector;
		}

		/// <summary>Addition operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator +( float value, Vector3 vector )
		{
			vector.X += value;
			vector.Y += value;
			vector.Z += value;
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

		/// <summary>Subtraction operator.</summary>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns></returns>
		public static Vector3 operator -( Vector3 vector, float value )
		{
			vector.X -= value;
			vector.Y -= value;
			vector.Z -= value;
			return vector;
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A valid <see cref="Vector3"/> structure.</param>
		/// <returns></returns>
		public static Vector3 operator -( float value, Vector3 vector )
		{
			vector.X = value - vector.X;
			vector.Y = value - vector.Y;
			vector.Z = value - vector.Z;
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
			value = 1.0f / value;
			vector.X *= value;
			vector.Y *= value;
			vector.Z *= value;
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