﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	
	/// <summary>A 2D vector, also used to represent coordinates.</summary>
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Vector2 : IEquatable<Vector2>
	{
		
		/// <summary>The X component of the <see cref="Vector2"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Justification = "Seriously..." )]
		public float X;

		/// <summary>The Y component of the <see cref="Vector2"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Justification = "Seriously..." )]
		public float Y;


		#region Constructors


		/// <summary>Initializes a new <see cref="Vector2"/> structure.</summary>
		/// <param name="x">The x component.</param>
		/// <param name="y">The y component.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y" )]
		public Vector2( float x, float y )
		{
			this.X = x;
			this.Y = y;
		}


		/// <summary>Initializes a new <see cref="Vector2"/> structure.</summary>
		/// <param name="xy">The value used for both <see cref="X"/> and <see cref="Y"/> components.</param>
		public Vector2( float xy )
		{
			X = Y = xy;
		}

		
		#endregion


		///// <summary>Gets or sets a component of this <see cref="Vector2"/> structure, given its index.</summary>
		///// <param name="index">The index of the concerned component: 0 for <see cref="X"/>, 1 for <see cref="Y"/>.</param>
		///// <returns>Returns the component associated with the specified <paramref name="index"/>.</returns>
		///// <exception cref="ArgumentOutOfRangeException"/>
		//public float this[ int index ]
		//{
		//	get { return ( index == 0 ) ? X : ( index == 1 ) ? Y : 0.0f; }
		//	set
		//	{
		//		if( index == 0 )
		//			X = value;
		//		else if( index == 1 )
		//			Y = value;
		//		else
		//			throw new ArgumentOutOfRangeException( "index" );
		//	}
		//}


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


		/// <summary>Returns the distance between this <see cref="Vector2"/> and another <see cref="Vector2"/>.</summary>
		/// <param name="other">A <see cref="Vector2"/> value.</param>
		/// <returns>Returns the distance between this <see cref="Vector2"/> and the <paramref name="other"/> <see cref="Vector2"/>.</returns>
		public float DistanceTo( Vector2 other )
		{
			var x = other.X - X;
			var y = other.Y - Y;
			return XMath.Sqrt( x * x + y * y );
		}


		/// <summary>Forces the components of this <see cref="Vector2"/> to the range [<paramref name="min"/>,<paramref name="max"/>].</summary>
		/// <param name="min">A valid <see cref="Vector2"/> structure containing the minimum value for each component.</param>
		/// <param name="max">A valid <see cref="Vector2"/> structure containing the maximum value for each component.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		public void Clamp( ref Vector2 min, ref Vector2 max )
		{
			X = X.Clamp( min.X, max.X );
			Y = Y.Clamp( min.Y, max.Y );
		}


		/// <summary>Gets the square of the length of this <see cref="Vector2"/>.
		/// <para>Note: this property is faster than <see cref="Length"/>.</para>
		/// </summary>
		public float LengthSquared { get { return X * X + Y * Y; } }

		/// <summary>Gets the length of this <see cref="Vector2"/>.</summary>
		public float Length { get { return XMath.Sqrt( X * X + Y * Y ); } }
		

		// TODO - Transform, TransformNormal


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

		
		/// <summary>Returns an array containing the <see cref="X"/> and <see cref="Y"/> components of this <see cref="Vector2"/> structure.</summary>
		/// <returns>Returns an array containing the <see cref="X"/> and <see cref="Y"/> components of this <see cref="Vector2"/> structure.</returns>
		public float[] ToArray()
		{
			return new float[] { X, Y };
		}




		/// <summary>The zero <see cref="Vector2"/>.</summary>
		public static readonly Vector2 Zero = new Vector2();

		/// <summary>A <see cref="Vector2"/> whose components are set to 1.</summary>
		public static readonly Vector2 One = new Vector2( 1.0f, 1.0f );

		/// <summary>A unit <see cref="Vector2"/> pointing to the positive x-direction.</summary>
		public static readonly Vector2 UnitX = new Vector2( 1.0f, 0.0f );

		/// <summary>A unit <see cref="Vector2"/> pointing to the positive y-direction.</summary>
		public static readonly Vector2 UnitY = new Vector2( 0.0f, 1.0f );




		/// <summary>Adds two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> + <paramref name="other"/> ).</returns>
		public static Vector2 Add( Vector2 vector, Vector2 other )
		{
			return new Vector2( vector.X + other.X, vector.Y + other.Y );
		}


		/// <summary>Subtracts a <see cref="Vector2"/> value (<paramref name="other"/>) from another <see cref="Vector2"/> value (<paramref name="vector"/>).</summary>
		/// <param name="vector">The initial <see cref="Vector2"/> value.</param>
		/// <param name="other">The subtracted <see cref="Vector2"/> value.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> - <paramref name="other"/> ).</returns>
		public static Vector2 Subtract( Vector2 vector, Vector2 other )
		{
			return new Vector2( vector.X - other.X, vector.Y - other.Y );
		}


		/// <summary>Returns the product of two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> × <paramref name="other"/> ).</returns>
		public static Vector2 Multiply( Vector2 vector, Vector2 other )
		{
			return new Vector2( vector.X * other.X, vector.Y * other.Y );
		}

		/// <summary>Returns the product of two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">The multiplier; must be a finite value.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> × <paramref name="value"/> ).</returns>
		public static Vector2 Multiply( Vector2 vector, float value )
		{
			return new Vector2( vector.X * value, vector.Y * value );
		}


		/// <summary>Returns the result of the division of two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> : <paramref name="other"/> ).</returns>
		public static Vector2 Divide( Vector2 vector, Vector2 other )
		{
			return new Vector2( vector.X / other.X, vector.Y / other.Y );
		}

		/// <summary>Returns the result of the division of a <see cref="Vector2"/> by a value.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="value">The divider; must be a finite, non-zero value.</param>
		/// <returns>Returns the result of ( <paramref name="vector"/> : <paramref name="value"/> ).</returns>
		public static Vector2 Divide( Vector2 vector, float value )
		{
			return new Vector2( vector.X / value, vector.Y / value );
		}


		/// <summary>Returns a <see cref="Vector2"/> structure whose components are set to the minimum components between two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <returns>Returns a <see cref="Vector2"/> structure whose components are set to the minimum components between the two <see cref="Vector2"/> values.</returns>
		public static Vector2 Min( Vector2 vector, Vector2 other )
		{
			return new Vector2( Math.Min( vector.X, other.X ), Math.Min( vector.Y, other.Y ) );
		}


		/// <summary>Returns a <see cref="Vector2"/> structure whose components are set to the maximum components between two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <returns>Returns a <see cref="Vector2"/> structure whose components are set to the maximum components between the two <see cref="Vector2"/> values.</returns>
		public static Vector2 Max( Vector2 vector, Vector2 other )
		{
			return new Vector2( Math.Max( vector.X, other.X ), Math.Max( vector.Y, other.Y ) );
		}

	

		/// <summary>Calculates the distance between two <see cref="Vector2"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector2"/> position.</param>
		/// <param name="other">A valid <see cref="Vector2"/> position.</param>
		/// <param name="result">Receives the distance between the two specified <see cref="Vector2"/> positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Distance( ref Vector2 position, ref Vector2 other, out float result )
		{
			var x = other.X - position.X;
			var y = other.Y - position.Y;
			result = XMath.Sqrt( x * x + y * y );
		}

		/// <summary>Returns the distance between two <see cref="Vector2"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector2"/> position.</param>
		/// <param name="other">A valid <see cref="Vector2"/> position.</param>
		/// <returns>Returns the distance between the two specified <see cref="Vector2"/> positions.</returns>
		public static float Distance( Vector2 position, Vector2 other )
		{
			var x = other.X - position.X;
			var y = other.Y - position.Y;
			return XMath.Sqrt( x * x + y * y );
		}


		/// <summary>Calculates the square of the distance (=distance²) between two <see cref="Vector2"/> positions.</summary>
		/// <param name="position">A valid <see cref="Vector2"/> position.</param>
		/// <param name="other">A valid <see cref="Vector2"/> position.</param>
		/// <param name="result">Receives the square of the distance between the two specified <see cref="Vector2"/> positions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
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


		/// <summary>Calculates the dot product of two <see cref="Vector2"/> values.</summary>
		/// <param name="vector">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives the dot product of two <see cref="Vector2"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Dot( ref Vector2 vector, ref Vector2 other, out float result )
		{
			result = vector.X * other.X + vector.Y * other.Y;
		}

		/// <summary>Returns the dot product of two <see cref="Vector2"/> values.</summary>
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
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Reflect( ref Vector2 vector, ref Vector2 normal, out Vector2 result )
		{
			float dot2 = ( vector.X * normal.X + vector.Y * normal.Y ) * 2.0f;
			result = new Vector2(
				vector.X - dot2 * normal.X,
				vector.Y - dot2 * normal.Y
			);
		}

		/// <summary>Determines the reflect vector of the given vector and normal.</summary>
		/// <param name="vector">The source vector.</param>
		/// <param name="normal">The normal of vector.</param>
		/// <returns>Returns the reflected vector.</returns>
		public static Vector2 Reflect( Vector2 vector, Vector2 normal )
		{
			float dot2 = ( vector.X * normal.X + vector.Y * normal.Y ) * 2.0f;
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
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Performance matters." )]
		public static void Lerp( ref Vector2 source, ref Vector2 target, float amount, out Vector2 result )
		{
			result = new Vector2(
				XMath.Lerp( source.X, target.X, amount ),
				XMath.Lerp( source.Y, target.Y, amount )
			);
		}

		/// <summary>Performs a linear interpolation between two vectors.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <returns>Returns the result of the linear interpolation between the two specified <see cref="Vector2"/>.</returns>
		public static Vector2 Lerp( Vector2 source, Vector2 target, float amount )
		{
			return new Vector2(
				XMath.Lerp( source.X, target.X, amount ),
				XMath.Lerp( source.Y, target.Y, amount )
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
		public static void SmoothStep( ref Vector2 source, ref Vector2 target, float amount, out Vector2 result )
		{
			amount = amount.Saturate();
			Lerp( ref source, ref target, amount * amount * ( 3.0f - 2.0f * amount ), out result );
		}

		/// <summary>Interpolates between two values using a cubic equation.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; will be saturated.</param>
		/// <returns>Returns the result of the cubic interpolation.</returns>
		public static Vector2 SmoothStep( Vector2 source, Vector2 target, float amount )
		{
			amount = amount.Saturate();
			Vector2 result;
			Lerp( ref source, ref target, amount * amount * ( 3.0f - 2.0f * amount ), out result );
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

		
		/// <summary>Performs a Catmull-Rom interpolation.</summary>
		/// <param name="value1">The first value in the interpolation.</param>
		/// <param name="value2">The second value in the interpolation.</param>
		/// <param name="value3">The third value in the interpolation.</param>
		/// <param name="value4">The fourth value in the interpolation.</param>
		/// <param name="amount">The weighting factor.</param>
		public static Vector2 CatmullRom( Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount )
		{
			var amount2 = amount * amount;
			var amount3 = amount * amount2;
			return new Vector2(
				0.5f * ( 2.0f * value2.X + ( -value1.X + value3.X ) * amount + ( 2.0f * value1.X - 5.0f * value2.X + 4.0f * value3.X - value4.X ) * amount2 + ( -value1.X + 3.0f * value2.X - 3.0f * value3.X + value4.X ) * amount3 ),
				0.5f * ( 2.0f * value2.Y + ( -value1.Y + value3.Y ) * amount + ( 2.0f * value1.Y - 5.0f * value2.Y + 4.0f * value3.Y - value4.Y ) * amount2 + ( -value1.Y + 3.0f * value2.Y - 3.0f * value3.Y + value4.Y ) * amount3 )
			);
		}


		/// <summary>Performs a Hermite spline interpolation.</summary>
		/// <param name="value1">Source position vector.</param>
		/// <param name="tangent1">Source tangent vector.</param>
		/// <param name="value2">Source position vector.</param>
		/// <param name="tangent2">Source tangent vector.</param>
		/// <param name="amount">Weighting factor.</param>
		public static Vector2 Hermite( Vector2 value1, Vector2 tangent1, Vector2 value2, Vector2 tangent2, float amount )
		{
			float amountSquared = amount * amount;
			float amountCubed = amount * amountSquared;
			
			float amountSquared3 = 3.0f * amountSquared;
			float amountCubed2 = 2.0f * amountCubed;

			float a = amountCubed2 - amountSquared3 + 1.0f;
			float b = -amountCubed2 + amountSquared3;
			float c = amountCubed - 2.0f * amountSquared + amount;
			float d = amountCubed - amountSquared;
			
			return new Vector2(
				value1.X * a + value2.X * b + tangent1.X * c + tangent2.X * d,
				value1.Y * a + value2.Y * b + tangent1.Y * c + tangent2.Y * d
			);
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


		/// <summary>Inferiority comparer.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <( Vector2 vector2, Vector2 other )
		{
			return ( vector2.X < other.X ) && ( vector2.Y < other.Y );
		}

		/// <summary>Inferiority or equality comparer.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator <=( Vector2 vector2, Vector2 other )
		{
			return ( vector2.X <= other.X ) && ( vector2.Y <= other.Y );
		}


		/// <summary>Superiority comparer.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >( Vector2 vector2, Vector2 other )
		{
			return ( vector2.X > other.X ) && ( vector2.Y > other.Y );
		}

		/// <summary>Superiority or equality comparer.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This wouldn't make sense." )]
		public static bool operator >=( Vector2 vector2, Vector2 other )
		{
			return ( vector2.X >= other.X ) && ( vector2.Y >= other.Y );
		}


		/// <summary>Addition operator.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator +( Vector2 vector2, Vector2 other )
		{
			return new Vector2( vector2.X + other.X, vector2.Y + other.Y );
		}

		/// <summary>Subtraction operator.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator -( Vector2 vector2, Vector2 other )
		{
			return new Vector2( vector2.X - other.X, vector2.Y - other.Y );
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator *( Vector2 vector2, Vector2 other )
		{
			return new Vector2( vector2.X * other.X, vector2.Y * other.Y );
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <returns></returns>
		public static Vector2 operator *( Vector2 vector2, float value )
		{
			return new Vector2( vector2.X * value, vector2.Y * value );
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator *( float value, Vector2 vector2 )
		{
			return new Vector2( vector2.X * value, vector2.Y * value );
		}


		/// <summary>Division operator.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="other">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator /( Vector2 vector2, Vector2 other )
		{
			return new Vector2( vector2.X / other.X, vector2.Y / other.Y );
		}

		/// <summary>Division operator.</summary>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <returns></returns>
		public static Vector2 operator /( Vector2 vector2, float value )
		{
			return new Vector2( vector2.X / value, vector2.Y / value );
		}

		/// <summary>Division operator.</summary>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <param name="vector2">A <see cref="Vector2"/> structure.</param>
		/// <returns></returns>
		public static Vector2 operator /( float value, Vector2 vector2 )
		{
			return new Vector2( value / vector2.X, value / vector2.Y );
		}
		
		#endregion

	}

}