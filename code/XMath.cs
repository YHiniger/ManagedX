using System;
using System.Runtime.CompilerServices;


namespace ManagedX
{

	/// <summary>Provides common math constants and functions, as well as extension methods to <see cref="float"/> and <see cref="double"/>.</summary>
	public static class XMath
	{

		#region Constants

		/// <summary>Defines the value of Pi.</summary>
		public const float Pi = 3.141592654f;

		/// <summary>Defines the value of two times Pi.</summary>
		public const float TwoPi = 6.283185307f;

		/// <summary>Defines Pi / 2.</summary>
		public const float PiOver2 = Pi / 2.0f;

		/// <summary>Defines Pi / 4.</summary>
		public const float PiOver4 = Pi / 4.0f;

		private const float Deg2Rad = 180.0f / Pi;
		private const float Rad2Deg = Pi / 180.0f;

		
		/// <summary>Defines the value of the golden number.</summary>
		public const float GoldenRatio = 1.61803398875f; // 0.5 + Sqrt( 5.0 ) / 2.0

		#endregion


		/// <summary>Converts an angle in radians to degrees.</summary>
		/// <param name="radians">An angle in radians.</param>
		/// <returns></returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float ToDegrees( float radians )
		{
			return radians * Rad2Deg;
		}


		/// <summary>Converts an angle in degrees to radians.</summary>
		/// <param name="degrees">An angle in degrees.</param>
		/// <returns></returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float ToRadians( float degrees )
		{
			return degrees * Deg2Rad;
		}


		/// <summary>Reduces an angle to a value within the range [-π,+π].</summary>
		/// <param name="radians">An angle, in radians; must be a valid, finite number.</param>
		/// <returns></returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float WrapAngle( float radians )
		{
			radians %= TwoPi; // TODO - use Math.IEEERemainder ?
			return ( radians <= -Pi ) ? radians + TwoPi : ( radians >= Pi ) ? radians - TwoPi : radians;
		}


		#region Lerp

		/// <summary>Performs a linear interpolation between two single-precision floating-point values.</summary>
		/// <param name="from">The source value.</param>
		/// <param name="to">The target value.</param>
		/// <param name="amount">The amount of <paramref name="to"/> in the final blend; should be within the range [0,1].</param>
		/// <returns></returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float Lerp( float from, float to, float amount )
		{
			return from + ( to - from ) * amount;
			//return from * ( 1.0f - amount ) + to * amount;
		}


		/// <summary>Performs a linear interpolation between two double-precision floating-point values.</summary>
		/// <param name="from">The source value.</param>
		/// <param name="to">The target value.</param>
		/// <param name="amount">The amount of <paramref name="to"/> in the final blend; should be within the range [0,1].</param>
		/// <returns></returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static double Lerp( double from, double to, double amount )
		{
			return from + ( to - from ) * amount;
			//return from * ( 1.0 - amount ) + to * amount;
		}

		#endregion


		#region Sqrt

		/// <summary>Returns the square root of a value.</summary>
		/// <param name="value">A finite single-precision (32bit) floating-point value.</param>
		/// <returns>Returns the square root of the specified <paramref name="value"/>.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float Sqrt( float value )
		{
			return (float)Math.Sqrt( (double)value );
		}


		/// <summary>Returns the square root of a value.</summary>
		/// <param name="value">A finite double-precision (64bit) floating-point value.</param>
		/// <returns>Returns the square root of the specified <paramref name="value"/>.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static double Sqrt( double value )
		{
			return Math.Sqrt( value );
		}

		#endregion


		#region Extension methods (MakeFinite, Clamp, Saturate)

		/// <summary>Converts <see cref="float.NaN"/> to 0, <see cref="float.PositiveInfinity"/> to <see cref="float.MaxValue"/> and <see cref="float.NegativeInfinity"/> to <see cref="float.MinValue"/>; otherwise, returns the specified value.</summary>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns>Returns the nearest finite value.</returns>
		public static float MakeFinite( this float value )
		{
			if( float.IsInfinity( value ) )
				return float.IsPositiveInfinity( value ) ? float.MaxValue : float.MinValue;

			return float.IsNaN( value ) ? 0.0f : value;
		}

		/// <summary>Converts <see cref="double.NaN"/> to 0, <see cref="double.PositiveInfinity"/> to <see cref="double.MaxValue"/> and <see cref="double.NegativeInfinity"/> to <see cref="double.MinValue"/>; otherwise, returns the specified value.</summary>
		/// <param name="value">A double-precision floating-point value.</param>
		/// <returns>Returns the nearest finite value.</returns>
		public static double MakeFinite( this double value )
		{
			if( double.IsInfinity( value ) )
				return double.IsPositiveInfinity( value ) ? double.MaxValue : double.MinValue;

			return double.IsNaN( value ) ? 0.0 : value;
		}



		/// <summary>Returns the nearest value within the specified range.</summary>
		/// <typeparam name="TValue">A comparable value type.</typeparam>
		/// <param name="value">A value; must be a finite number.</param>
		/// <param name="min">The minimum value; must be a finite number.</param>
		/// <param name="max">The maximum value; must be a finite number.</param>
		/// <returns>Returns:
		/// <para><paramref name="min"/> if <paramref name="value"/> is lower than or equal to <paramref name="min"/>.</para>
		/// <para><paramref name="max"/> if <paramref name="value"/> is greater than or equal to <paramref name="max"/>.</para>
		/// <para><paramref name="value"/> otherwise.</para>
		/// </returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static TValue Clamp<TValue>( this TValue value, TValue min, TValue max )
			where TValue : struct, IComparable<TValue>
		{
			return ( value.CompareTo( min ) <= 0 ) ? min : ( value.CompareTo( max ) >= 0 ) ? max : value;
		}


		/// <summary>Returns the nearest value within the specified range.</summary>
		/// <param name="value">A finite number.</param>
		/// <param name="min">The minimum value; must be a finite number.</param>
		/// <param name="max">The maximum value; must be a finite number, greater than <paramref name="min"/>.</param>
		/// <returns>Returns <paramref name="min"/> if <paramref name="value"/> is smaller than or equal to <paramref name="min"/>.
		/// <para>Returns <paramref name="max"/> if <paramref name="value"/> is greater than or equal to <paramref name="max"/>.</para>
		/// Returns <paramref name="value"/> otherwise.
		/// </returns>
		public static float Clamp( this float value, float min, float max )
		{
			return Clamp<float>( MakeFinite( value ), min, max );
		}

		/// <summary>Returns the nearest value within the specified range.</summary>
		/// <param name="value">A finite number.</param>
		/// <param name="min">The minimum value; must be a finite number.</param>
		/// <param name="max">The maximum value; must be a finite number, greater than <paramref name="min"/>.</param>
		/// <returns>Returns <paramref name="min"/> if <paramref name="value"/> is smaller than or equal to <paramref name="min"/>.
		/// <para>Returns <paramref name="max"/> if <paramref name="value"/> is greater than or equal to <paramref name="max"/>.</para>
		/// Returns <paramref name="value"/> otherwise.
		/// </returns>
		public static double Clamp( this double value, double min, double max )
		{
			return Clamp<double>( MakeFinite( value ), min, max );
		}



		/// <summary>Returns the nearest finite value within the range [0,1].</summary>
		/// <param name="value">The value to be saturated.</param>
		/// <returns>Returns the nearest value within the range [0,1].</returns>
		public static float Saturate( this float value )
		{
			return Clamp<float>( MakeFinite( value ), 0.0f, 1.0f );
		}

		/// <summary>Returns the nearest finite value within the range [0,1].</summary>
		/// <param name="value">The value to be saturated.</param>
		/// <returns>Returns the nearest value within the range [0,1].</returns>
		public static double Saturate( this double value )
		{
			return Clamp<double>( MakeFinite( value ), 0.0, 1.0 );
		}

		#endregion

	}

}