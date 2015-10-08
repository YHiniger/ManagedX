using System;


namespace ManagedX
{

	/// <summary>Provides access to common math constants and functions; also provides extension methods to float and double.</summary>
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

		#endregion


		#region Lerp


		/// <summary></summary>
		/// <param name="from">The source value.</param>
		/// <param name="to">The target value.</param>
		/// <param name="amount">The amount of <paramref name="to"/> in the final blend; must be within the range [0,1].</param>
		/// <returns></returns>
		public static float Lerp( float from, float to, float amount )
		{
			return from + ( to - from ) * amount;
			//return from * ( 1.0f - amount ) + to * amount;
		}

		/// <summary></summary>
		/// <param name="from">The source value.</param>
		/// <param name="to">The target value.</param>
		/// <param name="amount">The amount of <paramref name="to"/> in the final blend; must be within the range [0,1].</param>
		/// <returns></returns>
		public static double Lerp( double from, double to, double amount )
		{
			return from + ( to - from ) * amount;
			//return from * ( 1.0 - amount ) + to * amount;
		}


		#endregion


		/// <summary>Converts an angle in radians to degrees.</summary>
		/// <param name="radians">An angle in radians.</param>
		/// <returns></returns>
		public static float ToDegrees( float radians )
		{
			return radians * Rad2Deg;
		}


		/// <summary>Converts an angle in degrees to radians.</summary>
		/// <param name="degrees">An angle in degrees.</param>
		/// <returns></returns>
		public static float ToRadians( float degrees )
		{
			return degrees * Deg2Rad;
		}


		/// <summary>Reduces an angle to a value within the range [-π,+π].</summary>
		/// <param name="radians">An angle, in radians; must be a valid, finite number.</param>
		/// <returns></returns>
		public static float WrapAngle( float radians )
		{
			radians %= TwoPi; // TODO - use Math.IEEERemainder ?
			
			if( radians <= -Pi )
				radians += TwoPi;
			else if( radians >= Pi )
				radians -= TwoPi;

			return radians;
		}


		#region Extension methods (MakeFinite, Clamp, Saturate)


		/// <summary>Converts NaN to 0, +Inf to the maximum value and -Inf to the minimum value; otherwise, returns the specified value.</summary>
		/// <param name="value">A value.</param>
		/// <returns></returns>
		public static float MakeFinite( this float value )
		{
			if( float.IsNaN( value ) )
				return 0.0f;

			if( float.IsPositiveInfinity( value ) )
				return float.MaxValue;

			if( float.IsNegativeInfinity( value ) )
				return float.MinValue;

			return value;
		}

		/// <summary>Converts NaN to 0, +Inf to the maximum value and -Inf to the minimum value; otherwise, returns the specified value.</summary>
		/// <param name="value">A value.</param>
		/// <returns></returns>
		public static double MakeFinite( this double value )
		{
			if( double.IsNaN( value ) )
				return 0.0f;

			if( double.IsPositiveInfinity( value ) )
				return double.MaxValue;

			if( double.IsNegativeInfinity( value ) )
				return double.MinValue;

			return value;
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
		public static TValue Clamp<TValue>( this TValue value, TValue min, TValue max )
			where TValue : struct, IComparable<TValue>
		{
			if( value.CompareTo( min ) <= 0 )
				return min;

			if( value.CompareTo( max ) >= 0 )
				return max;

			return value;
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
			return Clamp<float>( float.IsNaN( value ) ? 0.0f : value, min, max );
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
			return Clamp<double>( double.IsNaN( value ) ? 0.0 : value, min, max );
		}


		/// <summary>Forces the value within the range [0,1].</summary>
		/// <param name="value">The value to be saturated.</param>
		/// <returns>Returns 0 if <paramref name="value"/> is not a number, a negative number (including -Inf) or zero.
		/// <para>Returns 1 if <paramref name="value"/> is greater than or equal to 1 (including +Inf).</para>
		/// Returns <paramref name="value"/> otherwise.
		/// </returns>
		public static float Saturate( this float value )
		{
			if( float.IsNaN( value ) || float.IsNegativeInfinity( value ) || value <= 0.0f )
				return 0.0f;

			if( float.IsPositiveInfinity( value ) || value >= 1.0f )
				return 1.0f;

			return value;
		}

		/// <summary>Forces the value within the range [0,1].</summary>
		/// <param name="value">The value to be saturated.</param>
		/// <returns>Returns 0 if <paramref name="value"/> is not a number, a negative number (including -Inf) or zero.
		/// <para>Returns 1 if <paramref name="value"/> is greater than or equal to 1 (including +Inf).</para>
		/// Returns <paramref name="value"/> otherwise.
		/// </returns>
		public static double Saturate( this double value )
		{
			if( double.IsNaN( value ) || double.IsNegativeInfinity( value ) || value <= 0.0 )
				return 0.0;

			if( double.IsPositiveInfinity( value ) || value >= 1.0 )
				return 1.0;

			return value;
		}

		#endregion

	}

}