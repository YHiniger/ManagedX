﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	using Win32;


	/// <summary>A 2D vector.
	/// <para>This structure is equivalent to the native <code>D2D_VECTOR_2F</code> and <code>D2D_POINT_2F</code> structures (defined in D2DBaseTypes.h).</para>
	/// </summary>
	[Source( "D2DBaseTypes.h", "D2D_VECTOR_2F" )]
	[Source( "D2DBaseTypes.h", "D2D_POINT_2F" )]
	[Source( "MFAPI.h", "MF_FLOAT2" )]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Vector2 : IEquatable<Vector2>
	{
		
		/// <summary>The X component of the <see cref="Vector2"/>.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public float X;

		/// <summary>The Y component of the <see cref="Vector2"/>.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public float Y;


		#region Constructors

		/// <summary>Initializes a new <see cref="Vector2"/>.</summary>
		/// <param name="x">The x component of the vector.</param>
		/// <param name="y">The y component of the vector.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public Vector2( float x, float y )
		{
			X = x;
			Y = y;
		}


        /// <summary>Initializes a new <see cref="Vector2"/>.</summary>
        /// <param name="xy">The value used for both <see cref="X"/> and <see cref="Y"/> components of the vector.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xy")]
        public Vector2( float xy )
		{
			X = Y = xy;
		}

		#endregion Constructors


		/// <summary>Gets the length of this <see cref="Vector2"/>.</summary>
		public float Length { get { return (float)Math.Sqrt( (double)( X * X + Y * Y ) ); } }


		/// <summary>Gets the square of the length of this <see cref="Vector2"/>.</summary>
		public float LengthSquared { get { return X * X + Y * Y; } }


		/// <summary>Normalizes this <see cref="Vector2"/>.</summary>
		public void Normalize()
		{
			var length = (float)Math.Sqrt( (double)( X * X + Y * Y ) );
			X /= length;
			Y /= length;
		}


		/// <summary>Inverts the sign of this <see cref="Vector2"/>.</summary>
		public void Negate()
		{
			X = -X;
			Y = -Y;
		}


		/// <summary>Returns the distance between this <see cref="Vector2"/> and another <see cref="Vector2"/>.</summary>
		/// <param name="other">A <see cref="Vector2"/>.</param>
		/// <returns>Returns the distance between this <see cref="Vector2"/> and the <paramref name="other"/> <see cref="Vector2"/>.</returns>
		public float DistanceTo( Vector2 other )
		{
			var x = other.X - X;
			var y = other.Y - Y;
			return (float)Math.Sqrt( (double)( x * x + y * y ) );
		}


		/// <summary>Forces the components of this <see cref="Vector2"/> within the specified range.</summary>
		/// <param name="min">A <see cref="Vector2"/> containing the minimum value for each component.</param>
		/// <param name="max">A <see cref="Vector2"/> containing the maximum value for each component.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
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


		/// <summary>Forces the components of this <see cref="Vector2"/> within the range [0,1].</summary>
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
		}


		/// <summary>Returns a hash code for this <see cref="Vector2"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Vector2"/> structure.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Vector2"/> equals another <see cref="Vector2"/>.</summary>
		/// <param name="other">A <see cref="Vector2"/>.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Vector2 other )
		{
			return ( X == other.X ) && ( Y == other.Y );
		}


		/// <summary>Returns a value indicating whether this <see cref="Vector2"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Vector2"/> which equals this <see cref="Vector2"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is Vector2 v && this.Equals( v );
		}


		/// <summary>Returns a string representing this <see cref="Vector2"/>, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>)</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="Vector2"/>.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "({0},{1})", X, Y );
		}

		
		/// <summary>Returns an array containing the <see cref="X"/> and <see cref="Y"/> components of this <see cref="Vector2"/>.</summary>
		/// <returns>Returns an array containing the <see cref="X"/> and <see cref="Y"/> components of this <see cref="Vector2"/>.</returns>
		public float[] ToArray()
		{
			return new float[] { X, Y };
		}



		/// <summary>The «zero» <see cref="Vector2"/>.</summary>
		public static readonly Vector2 Zero;

		/// <summary>A <see cref="Vector2"/> whose components are set to 1.</summary>
		public static readonly Vector2 One = new Vector2( 1.0f, 1.0f );

		/// <summary>A unit <see cref="Vector2"/> pointing to the positive x-direction.</summary>
		public static readonly Vector2 UnitX = new Vector2( 1.0f, 0.0f );

		/// <summary>A unit <see cref="Vector2"/> pointing to the positive y-direction.</summary>
		public static readonly Vector2 UnitY = new Vector2( 0.0f, 1.0f );


		#region Static methods

		/// <summary>Adds two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives the sum of the two specified <see cref="Vector2"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref Vector2 vector, ref Vector2 other, out Vector2 result )
		{
			result.X = vector.X + other.X;
			result.Y = vector.Y + other.Y;
		}

		/// <summary>Adds two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> + <paramref name="other"/> ).</returns>
		public static Vector2 Add( Vector2 vector, Vector2 other )
		{
			vector.X += other.X;
			vector.Y += other.Y;
			return vector;
		}

		/// <summary>Adds a <see cref="Vector2"/> and a value.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the addition.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref Vector2 vector, float value, out Vector2 result )
		{
			result.X = vector.X + value;
			result.Y = vector.Y + value;
		}

		/// <summary>Adds a <see cref="Vector2"/> and a value.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the addition.</returns>
		public static Vector2 Add( Vector2 vector, float value )
		{
			vector.X += value;
			vector.Y += value;
			return vector;
		}


		/// <summary>Subtracts a <see cref="Vector2"/> value (<paramref name="other"/>) from another <see cref="Vector2"/> value (<paramref name="vector"/>).</summary>
		/// <param name="vector">The initial <see cref="Vector2"/> value.</param>
		/// <param name="other">The subtracted <see cref="Vector2"/> value.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> - <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref Vector2 vector, ref Vector2 other, out Vector2 result )
		{
			result.X = vector.X - other.X;
			result.Y = vector.Y - other.Y;
		}

		/// <summary>Subtracts a <see cref="Vector2"/> value (<paramref name="other"/>) from another <see cref="Vector2"/> value (<paramref name="vector"/>).</summary>
		/// <param name="vector">The initial <see cref="Vector2"/> value.</param>
		/// <param name="other">The subtracted <see cref="Vector2"/> value.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> - <paramref name="other"/> ).</returns>
		public static Vector2 Subtract( Vector2 vector, Vector2 other )
		{
			vector.X -= other.X;
			vector.Y -= other.Y;
			return vector;
		}

		/// <summary>Subtracts a value from a <see cref="Vector2"/>.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the subtraction.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref Vector2 vector, float value, out Vector2 result )
		{
			result.X = vector.X - value;
			result.Y = vector.Y - value;
		}

		/// <summary>Subtracts a value from a <see cref="Vector2"/> value.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static Vector2 Subtract( Vector2 vector, float value )
		{
			vector.X -= value;
			vector.Y -= value;
			return vector;
		}

		/// <summary>Subtracts a <see cref="Vector2"/> value from a value.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives the result of the subtraction.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( float value, ref Vector2 vector, out Vector2 result )
		{
			result.X = value - vector.X;
			result.Y = value - vector.Y;
		}

		/// <summary>Subtracts a <see cref="Vector2"/> value from a value.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static Vector2 Subtract( float value, Vector2 vector )
		{
			vector.X = value - vector.X;
			vector.Y = value - vector.Y;
			return vector;
		}


		/// <summary>Multiplies two <see cref="Vector2"/>.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives the result of ( <paramref name="vector"/> × <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Multiply( ref Vector2 vector, ref Vector2 other, out Vector2 result )
		{
			result.X = vector.X * other.X;
			result.Y = vector.Y * other.Y;
		}

		/// <summary>Returns the product of two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> × <paramref name="other"/> ).</returns>
		public static Vector2 Multiply( Vector2 vector, Vector2 other )
		{
			vector.X *= other.X;
			vector.Y *= other.Y;
			return vector;
		}

		/// <summary>Multiplies a <see cref="Vector2"/> by a value.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the product.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Multiply( ref Vector2 vector, float value, out Vector2 result )
		{
			result.X = vector.X * value;
			result.Y = vector.Y * value;
		}

		/// <summary>Multiplies a <see cref="Vector2"/> by a value.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the product.</returns>
		public static Vector2 Multiply( Vector2 vector, float value )
		{
			vector.X *= value;
			vector.Y *= value;
			return vector;
		}


		/// <summary>Divides a <see cref="Vector2"/> by another <see cref="Vector2"/>.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives the result of the division.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( ref Vector2 vector, ref Vector2 other, out Vector2 result )
		{
			result.X = vector.X / other.X;
			result.Y = vector.Y / other.Y;
		}

		/// <summary>Returns the result of the division of two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the result of the division.</returns>
		public static Vector2 Divide( Vector2 vector, Vector2 other )
		{
			vector.X /= other.X;
			vector.Y /= other.Y;
			return vector;
		}

		/// <summary>Divides a <see cref="Vector2"/> by a value.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the division.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( ref Vector2 vector, float value, out Vector2 result )
		{
			result.X = vector.X / value;
			result.Y = vector.Y / value;
		}

		/// <summary>Returns the result of the division of a <see cref="Vector2"/> by a value.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns>Returns the result of the division.</returns>
		public static Vector2 Divide( Vector2 vector, float value )
		{
			vector.X /= value;
			vector.Y /= value;
			return vector;
		}

		/// <summary>Divides a value by a <see cref="Vector2"/>.</summary>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives the result of the division.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( float value, ref Vector2 vector, out Vector2 result )
		{
			result.X = value / vector.X;
			result.Y = value / vector.Y;
		}

		/// <summary>Returns the result of the division of a value by a <see cref="Vector2"/>.</summary>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the result of the division.</returns>
		public static Vector2 Divide( float value, Vector2 vector )
		{
			vector.X = value / vector.X;
			vector.Y = value / vector.Y;
			return vector;
		}


		/// <summary>Returns a <see cref="Vector2"/> structure whose components are set to the maximum components between two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vector2"/> structure whose components are set to the maximum components between the two <see cref="Vector2"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Min( ref Vector2 vector, ref Vector2 other, out Vector2 result )
		{
			if( other.X < vector.X )
				result.X = other.X;
			else
				result.X = vector.X;

			if( other.Y < vector.Y )
				result.Y = other.Y;
			else
				result.Y = vector.Y;
		}

		/// <summary>Returns a <see cref="Vector2"/> structure whose components are set to the minimum components between two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <returns>Returns a <see cref="Vector2"/> structure whose components are set to the minimum components between the two <see cref="Vector2"/> values.</returns>
		public static Vector2 Min( Vector2 vector, Vector2 other )
		{
			if( other.X < vector.X )
				vector.X = other.X;

			if( other.Y < vector.Y )
				vector.Y = other.Y;

			return vector;
		}


		/// <summary>Returns a <see cref="Vector2"/> structure whose components are set to the maximum components between two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vector2"/> structure whose components are set to the maximum components between the two <see cref="Vector2"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Max( ref Vector2 vector, ref Vector2 other, out Vector2 result )
		{
			if( other.X > vector.X )
				result.X = other.X;
			else
				result.X = vector.X;

			if( other.Y > vector.Y )
				result.Y = other.Y;
			else
				result.Y = vector.Y;
		}

		/// <summary>Returns a <see cref="Vector2"/> structure whose components are set to the maximum components between two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <returns>Returns a <see cref="Vector2"/> structure whose components are set to the maximum components between the two <see cref="Vector2"/> values.</returns>
		public static Vector2 Max( Vector2 vector, Vector2 other )
		{
			if( other.X > vector.X )
				vector.X = other.X;

			if( other.Y > vector.Y )
				vector.Y = other.Y;

			return vector;
		}


		/// <summary>Calculates the distance between two <see cref="Vector2"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector2"/> position.</param>
		/// <param name="other">A valid <see cref="Vector2"/> position.</param>
		/// <param name="result">Receives the distance between the two specified <see cref="Vector2"/> positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Distance( ref Vector2 position, ref Vector2 other, out float result )
		{
			var x = other.X - position.X;
			var y = other.Y - position.Y;
			result = (float)Math.Sqrt( (double)( x * x + y * y ) );
		}

		/// <summary>Returns the distance between two <see cref="Vector2"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector2"/> position.</param>
		/// <param name="other">A valid <see cref="Vector2"/> position.</param>
		/// <returns>Returns the distance between the two specified <see cref="Vector2"/> positions.</returns>
		public static float Distance( Vector2 position, Vector2 other )
		{
			var x = other.X - position.X;
			var y = other.Y - position.Y;
			return (float)Math.Sqrt( (double)( x * x + y * y ) );
		}


		/// <summary>Calculates the square of the distance (=distance²) between two <see cref="Vector2"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector2"/> position.</param>
		/// <param name="other">A valid <see cref="Vector2"/> position.</param>
		/// <param name="result">Receives the square of the distance between the two specified <see cref="Vector2"/> positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void DistanceSquared( ref Vector2 position, ref Vector2 other, out float result )
		{
			var x = other.X - position.X;
			var y = other.Y - position.Y;
			result = x * x + y * y;
		}

		/// <summary>Returns the square of the distance between two <see cref="Vector2"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector2"/> position.</param>
		/// <param name="other">A valid <see cref="Vector2"/> position.</param>
		/// <returns>Returns the square of the distance between the two specified <see cref="Vector2"/> positions.</returns>
		public static float DistanceSquared( Vector2 position, Vector2 other )
		{
			var x = other.X - position.X;
			var y = other.Y - position.Y;
			return x * x + y * y;
		}


		/// <summary>Normalizes a <see cref="Vector2"/>.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives the normalized <paramref name="vector"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Normalize( ref Vector2 vector, out Vector2 result )
		{
			var length = (float)Math.Sqrt( (double)( vector.X * vector.X + vector.Y * vector.Y ) );
			if( length == 0.0f )
				result = vector;
			else
			{
				result.X = vector.X / length;
				result.Y = vector.Y / length;
			}
		}

		/// <summary>Returns a normalized <see cref="Vector2"/>.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the normalized <paramref name="vector"/>.</returns>
		public static Vector2 Normalize( Vector2 vector )
		{
			var length = (float)Math.Sqrt( (double)( vector.X * vector.X + vector.Y * vector.Y ) );
			vector.X /= length;
			vector.Y /= length;
			return vector;
		}


		/// <summary>Calculates the dot product of two <see cref="Vector2"/>.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives the dot product of the two specified <see cref="Vector2"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Dot( ref Vector2 vector, ref Vector2 other, out float result )
		{
			result = vector.X * other.X + vector.Y * other.Y;
		}

		/// <summary>Returns the dot product of two <see cref="Vector2"/>.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the dot product of the two specified <see cref="Vector2"/>.</returns>
		public static float Dot( Vector2 vector, Vector2 other )
		{
			return vector.X * other.X + vector.Y * other.Y;
		}


		/// <summary>Determines the reflect vector of the given vector and normal.</summary>
		/// <param name="vector">Source vector.</param>
		/// <param name="normal">Normal of vector.</param>
		/// <param name="result">Receives the reflected vector.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Reflect( ref Vector2 vector, ref Vector2 normal, out Vector2 result )
		{
			var dot2 = ( vector.X * normal.X + vector.Y * normal.Y ) * 2.0f;
			
			result.X = vector.X - dot2 * normal.X;
			result.Y = vector.Y - dot2 * normal.Y;
		}

		/// <summary>Determines the reflect vector of the given vector and normal.</summary>
		/// <param name="vector">The source vector.</param>
		/// <param name="normal">The normal of vector.</param>
		/// <returns>Returns the reflected vector.</returns>
		public static Vector2 Reflect( Vector2 vector, Vector2 normal )
		{
			var dot2 = ( vector.X * normal.X + vector.Y * normal.Y ) * 2.0f;

			return new Vector2(
				vector.X - dot2 * normal.X,
				vector.Y - dot2 * normal.Y
			);
		}


		/// <summary>Performs a linear interpolation between two vectors.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <param name="result">Receives the result of the linear interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Lerp( ref Vector2 source, ref Vector2 target, float amount, out Vector2 result )
		{
			result.X = source.X + ( target.X - source.X ) * amount;
			result.Y = source.Y + ( target.Y - source.Y ) * amount;
		}

		/// <summary>Performs a linear interpolation between two vectors.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <returns>Returns the result of the linear interpolation.</returns>
		public static Vector2 Lerp( Vector2 source, Vector2 target, float amount )
		{
			source.X += ( target.X - source.X ) * amount;
			source.Y += ( target.Y - source.Y ) * amount;
			return source;
		}


		/// <summary>Interpolates between two values using a cubic equation.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; will be saturated.</param>
		/// <param name="result">Receives the result of the cubic interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void SmoothStep( ref Vector2 source, ref Vector2 target, float amount, out Vector2 result )
		{
			if( amount < 0.0f )
				amount = 0.0f;
			else if( amount > 1.0f )
				amount = 1.0f;

			amount = amount * amount * ( 3.0f - 2.0f * amount );

			result.X = source.X + ( target.X - source.X ) * amount;
			result.Y = source.Y + ( target.Y - source.Y ) * amount;
		}

		/// <summary>Interpolates between two values using a cubic equation.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; will be saturated.</param>
		/// <returns>Returns the result of the cubic interpolation.</returns>
		public static Vector2 SmoothStep( Vector2 source, Vector2 target, float amount )
		{
			if( amount < 0.0f )
				amount = 0.0f;
			else if( amount > 1.0f )
				amount = 1.0f;

			amount = amount * amount * ( 3.0f - 2.0f * amount );

			source.X += ( target.X - source.X ) * amount;
			source.Y += ( target.Y - source.Y ) * amount;
			return source;
		}


        /// <summary>Performs a Catmull-Rom interpolation.</summary>
        /// <param name="value1">The first value in the interpolation.</param>
        /// <param name="value2">The second value in the interpolation.</param>
        /// <param name="value3">The third value in the interpolation.</param>
        /// <param name="value4">The fourth value in the interpolation.</param>
        /// <param name="amount">The weighting factor.</param>
        /// <param name="result">Receives the result of the Catmull-Rom interpolation.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Catmull")]
        [SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CatmullRom( ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, float amount, out Vector2 result )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;
			
			result.X = 0.5f * ( 2.0f * value2.X + ( -value1.X + value3.X ) * amount + ( 2.0f * value1.X - 5.0f * value2.X + 4.0f * value3.X - value4.X ) * amountSquared + ( -value1.X + 3.0f * value2.X - 3.0f * value3.X + value4.X ) * amountCubed );
			result.Y = 0.5f * ( 2.0f * value2.Y + ( -value1.Y + value3.Y ) * amount + ( 2.0f * value1.Y - 5.0f * value2.Y + 4.0f * value3.Y - value4.Y ) * amountSquared + ( -value1.Y + 3.0f * value2.Y - 3.0f * value3.Y + value4.Y ) * amountCubed );
		}

        /// <summary>Performs a Catmull-Rom interpolation.</summary>
        /// <param name="value1">The first value in the interpolation.</param>
        /// <param name="value2">The second value in the interpolation.</param>
        /// <param name="value3">The third value in the interpolation.</param>
        /// <param name="value4">The fourth value in the interpolation.</param>
        /// <param name="amount">The weighting factor.</param>
        /// <returns>Returns the result of the Catmull-Rom interpolation.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Catmull")]
        public static Vector2 CatmullRom( Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;

			value1.X = 0.5f * ( 2.0f * value2.X + ( -value1.X + value3.X ) * amount + ( 2.0f * value1.X - 5.0f * value2.X + 4.0f * value3.X - value4.X ) * amountSquared + ( -value1.X + 3.0f * value2.X - 3.0f * value3.X + value4.X ) * amountCubed );
			value1.Y = 0.5f * ( 2.0f * value2.Y + ( -value1.Y + value3.Y ) * amount + ( 2.0f * value1.Y - 5.0f * value2.Y + 4.0f * value3.Y - value4.Y ) * amountSquared + ( -value1.Y + 3.0f * value2.Y - 3.0f * value3.Y + value4.Y ) * amountCubed );
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
		public static void Hermite( ref Vector2 position1, ref Vector2 tangent1, ref Vector2 position2, ref Vector2 tangent2, float amount, out Vector2 result )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;

			var a = 2.0f * amountCubed - 3.0f * amountSquared + 1.0f;
			var b = -2.0f * amountCubed + 3.0f * amountSquared;
			var c = amountCubed - 2.0f * amountSquared + amount;
			var d = amountCubed - amountSquared;

			result.X = position1.X * a + position2.X * b + tangent1.X * c + tangent2.X * d;
			result.Y = position1.Y * a + position2.Y * b + tangent1.Y * c + tangent2.Y * d;
		}

        /// <summary>Performs a Hermite spline interpolation.</summary>
        /// <param name="position1">A source position.</param>
        /// <param name="tangent1">The tangent associated with the source position.</param>
        /// <param name="position2">Another source position.</param>
        /// <param name="tangent2">The tangent associated with the other source position.</param>
        /// <param name="amount">The weighting factor.</param>
        /// <returns>Returns the result of the Hermite spline interpolation.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Hermite")]
        public static Vector2 Hermite( Vector2 position1, Vector2 tangent1, Vector2 position2, Vector2 tangent2, float amount )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;

			var a = 2.0f * amountCubed - 3.0f * amountSquared + 1.0f;
			var b = -2.0f * amountCubed + 3.0f * amountSquared;
			var c = amountCubed - 2.0f * amountSquared + amount;
			var d = amountCubed - amountSquared;

			position1.X = position1.X * a + position2.X * b + tangent1.X * c + tangent2.X * d;
			position1.Y = position1.Y * a + position2.Y * b + tangent1.Y * c + tangent2.Y * d;
			return position1;
		}


        /// <summary>Returns a <see cref="Vector2"/> structure containing the 2D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 2D triangle.</summary>
        /// <param name="value1">A <see cref="Vector2"/> containing the 2D Cartesian coordinates of vertex 1 of the triangle.</param>
        /// <param name="value2">A <see cref="Vector2"/> containing the 2D Cartesian coordinates of vertex 2 of the triangle.</param>
        /// <param name="value3">A <see cref="Vector2"/> containing the 2D Cartesian coordinates of vertex 3 of the triangle.</param>
        /// <param name="amount1">Barycentric coordinate b2, which expresses the weighting factor toward vertex 2 (specified in value2).</param>
        /// <param name="amount2">Barycentric coordinate b3, which expresses the weighting factor toward vertex 3 (specified in value3).</param>
        /// <param name="result">Receives a <see cref="Vector2"/> structure containing the 2D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 2D triangle.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Barycentric")]
        [SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Barycentric( ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, float amount1, float amount2, out Vector2 result )
		{
			result.X = value1.X + amount1 * ( value2.X - value1.X ) + amount2 * ( value3.X - value1.X );
			result.Y = value1.Y + amount1 * ( value2.Y - value1.Y ) + amount2 * ( value3.Y - value1.Y );
		}

        /// <summary>Returns a <see cref="Vector2"/> structure containing the 2D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 2D triangle.</summary>
        /// <param name="value1">A <see cref="Vector2"/> containing the 2D Cartesian coordinates of vertex 1 of the triangle.</param>
        /// <param name="value2">A <see cref="Vector2"/> containing the 2D Cartesian coordinates of vertex 2 of the triangle.</param>
        /// <param name="value3">A <see cref="Vector2"/> containing the 2D Cartesian coordinates of vertex 3 of the triangle.</param>
        /// <param name="amount1">Barycentric coordinate b2, which expresses the weighting factor toward vertex 2 (specified in value2).</param>
        /// <param name="amount2">Barycentric coordinate b3, which expresses the weighting factor toward vertex 3 (specified in value3).</param>
        /// <returns>Returns a <see cref="Vector2"/> structure containing the 2D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 2D triangle.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Barycentric")]
        public static Vector2 Barycentric( Vector2 value1, Vector2 value2, Vector2 value3, float amount1, float amount2 )
		{
			value1.X += amount1 * ( value2.X - value1.X ) + amount2 * ( value3.X - value1.X );
			value1.Y += amount1 * ( value2.Y - value1.Y ) + amount2 * ( value3.Y - value1.Y );
			return value1;
		}


		/// <summary>Forces the components of a <see cref="Vector2"/> within the specified range.</summary>
		/// <param name="value">A <see cref="Vector2"/> structure.</param>
		/// <param name="min">A <see cref="Vector2"/> structure containing the minimum value for each component.</param>
		/// <param name="max">A <see cref="Vector2"/> structure containing the maximum value for each component.</param>
		/// <param name="result">Receives a <see cref="Vector2"/> whose components are forced within the range specified by <paramref name="min"/> and <paramref name="max"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Clamp( ref Vector2 value, ref Vector2 min, ref Vector2 max, out Vector2 result )
		{
			if( value.X < min.X )
				result.X = min.X;
			else if( value.X > max.X )
				result.X = max.X;
			else
				result.X = value.X;

			if( value.Y < min.Y )
				result.Y = min.Y;
			else if( value.Y > max.Y )
				result.Y = max.Y;
			else
				result.Y = value.Y;
		}

		/// <summary>Returns a <see cref="Vector2"/> whose components are forced within the specified range.</summary>
		/// <param name="value">A <see cref="Vector2"/> structure.</param>
		/// <param name="min">A <see cref="Vector2"/> structure containing the minimum value for each component.</param>
		/// <param name="max">A <see cref="Vector2"/> structure containing the maximum value for each component.</param>
		/// <returns>Returns a <see cref="Vector2"/> whose components are forced within the range specified by <paramref name="min"/> and <paramref name="max"/>.</returns>
		public static Vector2 Clamp( Vector2 value, Vector2 min, Vector2 max )
		{
			if( value.X <= min.X )
				value.X = min.X;
			else if( value.X >= max.X )
				value.X = max.X;

			if( value.Y <= min.Y )
				value.Y = min.Y;
			else if( value.Y >= max.Y )
				value.Y = max.Y;

			return value;
		}


		/// <summary>Forces the components of a <see cref="Vector2"/> within the range [0,1].</summary>
		/// <param name="value">A <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vector2"/> whose components are forced within the range [0,1].</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Saturate( ref Vector2 value, out Vector2 result )
		{
			if( value.X < 0.0f )
				result.X = 0.0f;
			else if( value.X > 1.0f )
				result.X = 1.0f;
			else
				result.X = value.X;

			if( value.Y < 0.0f )
				result.Y = 0.0f;
			else if( value.Y > 1.0f )
				result.Y = 1.0f;
			else
				result.Y = value.Y;
		}

		/// <summary>Returns a <see cref="Vector2"/> whose components are forced within the range [0,1].</summary>
		/// <param name="value">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns a <see cref="Vector2"/> whose components are forced within the range [0,1].</returns>
		public static Vector2 Saturate( Vector2 value )
		{
			if( value.X < 0.0f )
				value.X = 0.0f;
			else if( value.X > 1.0f )
				value.X = 1.0f;

			if( value.Y < 0.0f )
				value.Y = 0.0f;
			else if( value.Y > 1.0f )
				value.Y = 1.0f;

			return value;
		}

		#endregion Static methods


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Vector2 vector, Vector2 other )
		{
			return ( vector.X == other.X ) && ( vector.Y == other.Y );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Vector2 vector, Vector2 other )
		{
			return ( vector.X != other.X ) && ( vector.Y != other.Y );
		}


		/// <summary>Unary negation operator.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns a <see cref="Vector2"/> whose components are set to the opposite of the specified value.</returns>
		public static Vector2 operator -( Vector2 vector )
		{
			vector.X = -vector.X;
			vector.Y = -vector.Y;
			return vector;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the sum of the specified <see cref="Vector2"/> values.</returns>
		public static Vector2 operator +( Vector2 vector, Vector2 other )
		{
			vector.X += other.X;
			vector.Y += other.Y;
			return vector;
		}

		/// <summary>Addition operator.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the sum of the specified <see cref="Vector2"/> values.</returns>
		public static Vector2 operator +( Vector2 vector, float value )
		{
			vector.X += value;
			vector.Y += value;
			return vector;
		}
		
		/// <summary>Addition operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the sum of the specified <see cref="Vector2"/> values.</returns>
		public static Vector2 operator +( float value, Vector2 vector )
		{
			vector.X += value;
			vector.Y += value;
			return vector;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the difference between <paramref name="vector"/> and <paramref name="other"/>.</returns>
		public static Vector2 operator -( Vector2 vector, Vector2 other )
		{
			vector.X -= other.X;
			vector.Y -= other.Y;
			return vector;
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static Vector2 operator -( Vector2 vector, float value )
		{
			vector.X -= value;
			vector.Y -= value;
			return vector;
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static Vector2 operator -( float value, Vector2 vector )
		{
			vector.X = value - vector.X;
			vector.Y = value - vector.Y;
			return vector;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static Vector2 operator *( Vector2 vector, Vector2 other )
		{
			vector.X *= other.X;
			vector.Y *= other.Y;
			return vector;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static Vector2 operator *( Vector2 vector, float value )
		{
			vector.X *= value;
			vector.Y *= value;
			return vector;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static Vector2 operator *( float value, Vector2 vector )
		{
			vector.X *= value;
			vector.Y *= value;
			return vector;
		}


		/// <summary>Division operator.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator /( Vector2 vector, Vector2 other )
		{
			vector.X /= other.X;
			vector.Y /= other.Y;
			return vector;
		}

		/// <summary>Division operator.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <returns></returns>
		public static Vector2 operator /( Vector2 vector, float value )
		{
			vector.X /= value;
			vector.Y /= value;
			return vector;
		}

		/// <summary>Division operator.</summary>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator /( float value, Vector2 vector )
		{
			vector.X = value / vector.X;
			vector.Y = value / vector.Y;
			return vector;
		}

		#endregion Operators

	}

}