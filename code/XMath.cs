using System;
using System.Runtime.CompilerServices;


namespace ManagedX
{

	/// <summary>Provides common math constants and functions, as well as extension methods to <see cref="float"/> and <see cref="double"/>.</summary>
	public static class XMath
	{

		#region Constants

		/// <summary>Defines the value of π (Pi).</summary>
		public const float Pi = 3.141592654f;

		/// <summary>Defines the value of two times π (Pi).</summary>
		public const float TwoPi = Pi * 2.0f;

		/// <summary>Defines π / 2.</summary>
		public const float PiOver2 = Pi * 0.5f;

		/// <summary>Defines π / 4.</summary>
		public const float PiOver4 = Pi * 0.25f;


		///// <summary>Defines the value of the golden number.</summary>
		//public const float GoldenRatio = 1.61803398875f; // 0.5 + Sqrt( 5.0 ) / 2.0

		#endregion // Constants



		#region Temperature conversion functions

		// TODO - move this into a XScience static class

		///// <summary>Converts a temperature in degrees Celcius (°C) to a temperature in degrees Fahrenheit (°F).</summary>
		///// <param name="temperatureInDegreesCelcius">A temperature in degrees Celcius.</param>
		///// <returns>Returns the specified temperature, in degrees Fahrenheit.</returns>
		//public static float ToFahrenheit( float temperatureInDegreesCelcius )
		//{
		//	return temperatureInDegreesCelcius * 1.8f + 32.0f;
		//}

		///// <summary>Converts a temperature in degrees Celcius (°C) to a temperature in degrees Fahrenheit (°F).</summary>
		///// <param name="temperatureInDegreesCelcius">A temperature in degrees Celcius.</param>
		///// <returns>Returns the specified temperature, in degrees Fahrenheit.</returns>
		//public static double ToFahrenheit( double temperatureInDegreesCelcius )
		//{
		//	return temperatureInDegreesCelcius * 1.8 + 32.0;
		//}


		///// <summary>Converts a temperature in degrees Fahrenheit (°F) to a temperature in degrees Celcius (°C).</summary>
		///// <param name="temperatureInDegreesFahrenheit">A temperature in degrees Fahrenheit.</param>
		///// <returns>Returns the specified temperature, in degrees Celcius.</returns>
		//public static float ToCelcius( float temperatureInDegreesFahrenheit )
		//{
		//	return temperatureInDegreesFahrenheit / 1.8f - 32.0f;
		//}

		///// <summary>Converts a temperature in degrees Fahrenheit (°F) to a temperature in degrees Celcius (°C).</summary>
		///// <param name="temperatureInDegreesFahrenheit">A temperature in degrees Fahrenheit.</param>
		///// <returns>Returns the specified temperature, in degrees Celcius.</returns>
		//public static double ToCelcius( double temperatureInDegreesFahrenheit )
		//{
		//	return temperatureInDegreesFahrenheit / 1.8 - 32.0;
		//}

		#endregion // Temperature conversion functions


		#region Angle conversion functions

		/// <summary>Converts an angle in radians to degrees.</summary>
		/// <param name="radians">An angle in radians.</param>
		/// <returns></returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float ToDegrees( float radians )
		{
			return radians * Pi / 180.0f;
		}


		/// <summary>Converts an angle in degrees to radians.</summary>
		/// <param name="degrees">An angle in degrees.</param>
		/// <returns></returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float ToRadians( float degrees )
		{
			return degrees * 180.0f / Pi;
		}

		#endregion // Angle conversion functions


		/// <summary>Reduces an angle to a value within the range [-π,+π].</summary>
		/// <param name="radians">An angle, in radians.</param>
		/// <returns>Returns the specified angle reduced to the range [-π,+π].</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float WrapAngle( float radians )
		{
			radians %= TwoPi;
			return ( radians <= -Pi ) ? radians + TwoPi : ( radians >= Pi ) ? radians - TwoPi : radians;
		}


		/// <summary>Returns the smallest value between two values.</summary>
		/// <param name="value1">A finite single-precision floating-point value.</param>
		/// <param name="value2">A finite single-precision floating-point value.</param>
		/// <returns>Returns the smallest value between the two specified values.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float Min( float value1, float value2 )
		{
			return ( value1 < value2 ) ? value1 : value2;
		}

		/// <summary>Returns the largest value between two values.</summary>
		/// <param name="value1">A finite single-precision floating-point value.</param>
		/// <param name="value2">A finite single-precision floating-point value.</param>
		/// <returns>Returns the largest value between the two specified values.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float Max( float value1, float value2 )
		{
			return ( value1 > value2 ) ? value1 : value2;
		}

		
		/// <summary>Returns <code>e</code> to the specified power.</summary>
		/// <param name="power">A finite single-precision floating-point value specifying the power.</param>
		/// <returns>Returns <code>e</code> to the specified power.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float Exp( float power )
		{
			return (float)Math.Exp( (double)power );
		}


		/// <summary></summary>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <param name="value3"></param>
		/// <param name="amount1"></param>
		/// <param name="amount2"></param>
		/// <returns></returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float Barycentric( float value1, float value2, float value3, float amount1, float amount2 )
		{
			return value1 + amount1 * ( value2 - value1 ) + amount2 * ( value3 - value1 );
		}


		/// <summary></summary>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <param name="value3"></param>
		/// <param name="value4"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float CatmullRom( float value1, float value2, float value3, float value4, float amount )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;
			return 0.5f * ( 2.0f * value2 + ( -value1 + value3 ) * amount + ( 2.0f * value1 - 5.0f * value2 + 4.0f * value3 - value4 ) * amountSquared + ( -value1 + 3.0f * value2 - 3.0f * value3 + value4 ) * amountCubed );
		}


		#region Lerp

		/// <summary>Performs a linear interpolation between two single-precision floating-point values.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The amount of <paramref name="target"/> in the final blend; should be within the range [0,1].</param>
		/// <returns></returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float Lerp( float source, float target, float amount )
		{
			return source + ( target - source ) * amount;
			//return from * ( 1.0f - amount ) + to * amount;
		}


		/// <summary>Performs a linear interpolation between two double-precision floating-point values.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The amount of <paramref name="target"/> in the final blend; should be within the range [0,1].</param>
		/// <returns></returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static double Lerp( double source, double target, double amount )
		{
			return source + ( target - source ) * amount;
			//return from * ( 1.0 - amount ) + to * amount;
		}

		#endregion


		#region SmoothStep

		/// <summary>Returns the cubic interpolation between two values.</summary>
		/// <param name="source">A finite single-precision floating-point value.</param>
		/// <param name="target">A finite single-precision floating-point value.</param>
		/// <param name="amount">A finite single-precision floating-point value; will be saturated (=forced into the the range [0,1]).</param>
		/// <returns>Returns the cubic interpolation between the two specified values.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float SmoothStep( float source, float target, float amount )
		{
			amount = amount.Saturate();
			return Lerp( source, target, amount * amount * ( 3.0f - 2.0f * amount ) );
		}

		/// <summary>Returns the cubic interpolation between two values.</summary>
		/// <param name="source">A finite double-precision floating-point value.</param>
		/// <param name="target">A finite double-precision floating-point value.</param>
		/// <param name="amount">A finite double-precision floating-point value; will be saturated (=forced into the the range [0,1]).</param>
		/// <returns>Returns the cubic interpolation between the two specified values.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static double SmoothStep( double source, double target, double amount )
		{
			amount = amount.Saturate();
			return Lerp( source, target, amount * amount * ( 3.0 - 2.0 * amount ) );
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


		#region Sin, Cos

		/// <summary>Returns the sinus of an angle expressed in radians.</summary>
		/// <param name="angle">An angle, in radians.</param>
		/// <returns>Returns the sinus of the specified <paramref name="angle"/>.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float Sin( float angle )
		{
			return (float)Math.Sin( (double)angle );
		}


		/// <summary>Returns the cosinus of an angle expressed in radians.</summary>
		/// <param name="angle">An angle, in radians.</param>
		/// <returns>Returns the cosinus of the specified <paramref name="angle"/>.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float Cos( float angle )
		{
			return (float)Math.Cos( (double)angle );
		}

		#endregion // Sin, Cos


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
		/// <typeparam name="TValue">Comparable value type.</typeparam>
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