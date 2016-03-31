using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;


namespace ManagedX
{

	// A little reminder: https://en.wikipedia.org/wiki/List_of_mathematical_symbols
	// THINKABOUTME - try to make use of DirectXMath (https://msdn.microsoft.com/en-us/library/windows/desktop/hh437833%28v=vs.85%29.aspx)

	
	/// <summary>Provides common math constants and functions, as well as extension methods to <see cref="float"/> and <see cref="double"/>.</summary>
	public static class XMath
	{

		#region Constants

		/// <summary>Defines the value of π.</summary>
		public const float Pi = 3.141592654f;

		/// <summary>Defines the value of π × 2.</summary>
		public const float TwoPi = Pi * 2.0f;

		/// <summary>Defines the value of π / 2.</summary>
		public const float PiOver2 = Pi * 0.5f;

		/// <summary>Defines the value of π / 4.</summary>
		public const float PiOver4 = Pi * 0.25f;




        ///// <summary>Defines the mathematical constant e.</summary>
        //public const float E = 2.71828175f;

        ///// <summary>Represents the log base ten of e (<see cref="E"/>).</summary>
        //public const float Log10E = 0.4342945f;

        ///// <summary>Represents the log base two of e (see <see cref="E"/>).</summary>
        //public const float Log2E = 1.442695f;


        ///// <summary>Defines the value of the golden number.</summary>
        //public const float GoldenRatio = 1.61803398875f; // 0.5 + Sqrt( 5.0 ) / 2.0

        #endregion Constants

        #region Temperature conversion functions

        /// <summary>Converts a temperature in degrees Fahrenheit (°F) to a temperature in degrees Celcius (°C).</summary>
        /// <param name="temperatureInDegreesFahrenheit">A temperature in degrees Fahrenheit.</param>
        /// <returns>Returns the specified temperature, in degrees Celcius.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Celcius")]
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float ToCelcius( float temperatureInDegreesFahrenheit )
		{
			return ( temperatureInDegreesFahrenheit - 32.0f ) / 1.8f;
		}

        /// <summary>Converts a temperature in degrees Fahrenheit (°F) to a temperature in degrees Celcius (°C).</summary>
        /// <param name="temperatureInDegreesFahrenheit">A temperature in degrees Fahrenheit.</param>
        /// <returns>Returns the specified temperature, in degrees Celcius.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Celcius")]
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static double ToCelcius( double temperatureInDegreesFahrenheit )
		{
			return ( temperatureInDegreesFahrenheit - 32.0 ) / 1.8;
		}


        /// <summary>Converts a temperature in degrees Celcius (°C) to a temperature in degrees Fahrenheit (°F).</summary>
        /// <param name="temperatureInDegreesCelcius">A temperature in degrees Celcius.</param>
        /// <returns>Returns the specified temperature, in degrees Fahrenheit.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Celcius")]
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float ToFahrenheit( float temperatureInDegreesCelcius )
		{
			return temperatureInDegreesCelcius * 1.8f + 32.0f;
		}

        /// <summary>Converts a temperature in degrees Celcius (°C) to a temperature in degrees Fahrenheit (°F).</summary>
        /// <param name="temperatureInDegreesCelcius">A temperature in degrees Celcius.</param>
        /// <returns>Returns the specified temperature, in degrees Fahrenheit.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Celcius")]
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static double ToFahrenheit( double temperatureInDegreesCelcius )
		{
			return temperatureInDegreesCelcius * 1.8 + 32.0;
		}

		#endregion Temperature conversion functions


		#region Angle conversion functions

		/// <summary>Converts an angle in radians to degrees.</summary>
		/// <param name="radians">An angle in radians.</param>
		/// <returns>Returns the specified angle, expressed in degrees.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float ToDegrees( float radians )
		{
			const float RadToDeg = Pi / 180.0f;
			return radians * RadToDeg;
		}

		/// <summary>Converts an angle in radians to degrees.</summary>
		/// <param name="radians">An angle in radians.</param>
		/// <returns>Returns the specified angle, expressed in degrees.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static double ToDegrees( double radians )
		{
			const double RadToDeg = Math.PI / 180.0;
			return radians * RadToDeg;
		}


		/// <summary>Converts an angle in degrees to radians.</summary>
		/// <param name="degrees">An angle in degrees.</param>
		/// <returns>Returns the specified angle, expressed in radians.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float ToRadians( float degrees )
		{
			const float DegToRad = 180.0f / Pi;
			return degrees * DegToRad;
		}

		/// <summary>Converts an angle in degrees to radians.</summary>
		/// <param name="degrees">An angle in degrees.</param>
		/// <returns>Returns the specified angle, expressed in radians.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static double ToRadians( double degrees )
		{
			const double DegToRad = 180.0 / Math.PI;
			return degrees * DegToRad;
		}

		#endregion Angle conversion functions


		#region WrapAngle

		/// <summary>Reduces an angle to a value within the range [-π,+π].</summary>
		/// <param name="radians">An angle, in radians.</param>
		/// <returns>Returns the specified angle reduced to the range [-π,+π].</returns>
		public static float WrapAngle( float radians )
		{
			radians %= TwoPi;
			
			if( radians < -Pi )
				return radians + TwoPi;
			
			if( radians > Pi )
				return radians - TwoPi;
			
			return radians;
		}

		/// <summary>Reduces an angle to a value within the range [-π,+π].</summary>
		/// <param name="radians">An angle, in radians.</param>
		/// <returns>Returns the specified angle reduced to the range [-π,+π].</returns>
		public static double WrapAngle( double radians )
		{
			const double Pi2 = Math.PI * 2.0;

			radians %= Pi2;

			if( radians < -Math.PI )
				return radians + Pi2;

			if( radians > Math.PI )
				return radians - Pi2;

			return radians;
		}

		#endregion WrapAngle


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
		}

		#endregion Lerp


		#region SmoothStep

		/// <summary>Returns the cubic interpolation between two values.</summary>
		/// <param name="source">A finite single-precision floating-point value.</param>
		/// <param name="target">A finite single-precision floating-point value.</param>
		/// <param name="amount">A finite single-precision floating-point value; will be saturated (=forced within the range [0,1]).</param>
		/// <returns>Returns the cubic interpolation between the two specified values.</returns>
		public static float SmoothStep( float source, float target, float amount )
		{
			if( amount < 0.0f || float.IsNaN( amount ) )
				amount = 0.0f;
			else if( amount > 1.0f )
				amount = 1.0f;
			
			return source + ( target - source ) * amount * amount * ( 3.0f - 2.0f * amount );
		}

		/// <summary>Returns the cubic interpolation between two values.</summary>
		/// <param name="source">A finite double-precision floating-point value.</param>
		/// <param name="target">A finite double-precision floating-point value.</param>
		/// <param name="amount">A finite double-precision floating-point value; will be saturated (=forced within the range [0,1]).</param>
		/// <returns>Returns the cubic interpolation between the two specified values.</returns>
		public static double SmoothStep( double source, double target, double amount )
		{
			if( amount < 0.0 || double.IsNaN( amount ) )
				amount = 0.0;
			else if( amount > 1.0 )
				amount = 1.0;
			
			return source + ( target - source ) * amount * amount * ( 3.0 - 2.0 * amount );
		}


        #endregion SmoothStep

        #region Barycentric

        /// <summary>Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.</summary>
        /// <param name="value1">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="value2">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="value3">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting factor for vertex 2, the coordinate of which is specified in value2.</param>
        /// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting factor for vertex 3, the coordinate of which is specified in value3.</param>
        /// <returns>Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Barycentric")]
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static float Barycentric( float value1, float value2, float value3, float amount1, float amount2 )
		{
			return value1 + ( value2 - value1 ) * amount1 + ( value3 - value1 ) * amount2;
		}

        /// <summary>Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.</summary>
        /// <param name="value1">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="value2">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="value3">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting factor for vertex 2, the coordinate of which is specified in value2.</param>
        /// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting factor for vertex 3, the coordinate of which is specified in value3.</param>
        /// <returns>Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Barycentric")]
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static double Barycentric( double value1, double value2, double value3, double amount1, double amount2 )
		{
			return value1 + ( value2 - value1 ) * amount1 + ( value3 - value1 ) * amount2;
		}


        #endregion Barycentric

        #region Hermite

        /// <summary>Performs a Hermite spline interpolation.</summary>
        /// <param name="position1">The first position.</param>
        /// <param name="position2">The second position.</param>
        /// <param name="tangent1">The first tangent.</param>
        /// <param name="tangent2">The second tangent.</param>
        /// <param name="amount">The weighting factor.</param>
        /// <returns>Returns the result of the interpolation.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Hermite")]
        public static float Hermite( float position1, float position2, float tangent1, float tangent2, float amount)
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;

			var a = 2.0f * amountCubed - 3.0f * amountSquared + 1.0f;
			var b = -2.0f * amountCubed + 3.0f * amountSquared;
			var c = amountCubed - 2.0f * amountSquared + amount;
			var d = amountCubed - amountSquared;

			return position1 * a + position2 * b + tangent1 * c + tangent2 * d;
		}

        /// <summary>Performs a Hermite spline interpolation.</summary>
        /// <param name="position1">The first position.</param>
        /// <param name="position2">The second position.</param>
        /// <param name="tangent1">The first tangent.</param>
        /// <param name="tangent2">The second tangent.</param>
        /// <param name="amount">The weighting factor.</param>
        /// <returns>Returns the result of the interpolation.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Hermite")]
        public static double Hermite( double position1, double position2, double tangent1, double tangent2, double amount )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;

			var a = 2.0 * amountCubed - 3.0 * amountSquared + 1.0;
			var b = -2.0 * amountCubed + 3.0 * amountSquared;
			var c = amountCubed - 2.0 * amountSquared + amount;
			var d = amountCubed - amountSquared;

			return position1 * a + position2 * b + tangent1 * c + tangent2 * d;
		}


        #endregion Hermite

        #region Catmull-Rom

        /// <summary>Performs a Catmull-Rom interpolation using the specified positions.</summary>
        /// <param name="value1">The first position in the interpolation.</param>
        /// <param name="value2">The second position in the interpolation.</param>
        /// <param name="value3">The third position in the interpolation.</param>
        /// <param name="value4">The fourth position in the interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>Returns the Catmull-Rom interpolation of the specified positions.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Catmull")]
        public static float CatmullRom( float value1, float value2, float value3, float value4, float amount )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;
			return 0.5f * ( 2.0f * value2 + ( -value1 + value3 ) * amount + ( 2.0f * value1 - 5.0f * value2 + 4.0f * value3 - value4 ) * amountSquared + ( -value1 + 3.0f * value2 - 3.0f * value3 + value4 ) * amountCubed );
		}

        /// <summary>Performs a Catmull-Rom interpolation using the specified positions.</summary>
        /// <param name="value1">The first position in the interpolation.</param>
        /// <param name="value2">The second position in the interpolation.</param>
        /// <param name="value3">The third position in the interpolation.</param>
        /// <param name="value4">The fourth position in the interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>Returns the Catmull-Rom interpolation of the specified positions.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Catmull")]
        public static double CatmullRom( double value1, double value2, double value3, double value4, double amount )
		{
			var amountSquared = amount * amount;
			var amountCubed = amount * amountSquared;
			return 0.5 * ( 2.0 * value2 + ( -value1 + value3 ) * amount + ( 2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4 ) * amountSquared + ( -value1 + 3.0 * value2 - 3.0 * value3 + value4 ) * amountCubed );
		}

		#endregion Catmull-Rom


		/// <summary>Calculates the length of a vector, given its components.</summary>
		/// <param name="x">The X component of the vector.</param>
		/// <param name="y">The Y component of the vector.</param>
		/// <returns>Returns the length of the specified vector.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public static float Length( float x, float y )
		{
			return (float)Math.Sqrt( (double)( x * x + y * y ) );
		}

		/// <summary>Calculates the length of a vector, given its components.</summary>
		/// <param name="x">The X component of the vector.</param>
		/// <param name="y">The Y component of the vector.</param>
		/// <param name="z">The Z component of the vector.</param>
		/// <returns>Returns the length of the specified vector.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public static float Length( float x, float y, float z )
		{
			return (float)Math.Sqrt( (double)( x * x + y * y + z * z ) );
		}

		/// <summary>Calculates the length of a vector, given its components.</summary>
		/// <param name="x">The x component of the vector.</param>
		/// <param name="y">The y component of the vector.</param>
		/// <param name="z">The z component of the vector.</param>
		/// <param name="w">The w component of the vector.</param>
		/// <returns>Returns the length of the specified vector.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public static float Length( float x, float y, float z, float w )
		{
			return (float)Math.Sqrt( (double)( x * x + y * y + z * z + w * w ) );
		}


		/// <summary>Calculates the square of the length of a vector, given its components.</summary>
		/// <param name="x">The X component of the vector.</param>
		/// <param name="y">The Y component of the vector.</param>
		/// <returns>Returns the square of the length of the specified vector.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public static float LengthSquared( float x, float y )
		{
			return x * x + y * y;
		}

		/// <summary>Calculates the square of the length of a vector, given its components.</summary>
		/// <param name="x">The X component of the vector.</param>
		/// <param name="y">The Y component of the vector.</param>
		/// <param name="z">The Z component of the vector.</param>
		/// <returns>Returns the square of the length of the specified vector.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public static float LengthSquared( float x, float y, float z )
		{
			return x * x + y * y + z * z;
		}

		/// <summary>Calculates the square of the length of a vector, given its components.</summary>
		/// <param name="x">The X component of the vector.</param>
		/// <param name="y">The Y component of the vector.</param>
		/// <param name="z">The Z component of the vector.</param>
		/// <param name="w">The w component of the vector.</param>
		/// <returns>Returns the square of the length of the specified vector.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public static float LengthSquared( float x, float y, float z, float w )
		{
			return x * x + y * y + z * z + w * w;
		}


		#region Extension methods (MakeFinite, Clamp, Saturate)

		/// <summary>Converts <see cref="float.NaN"/> to 0, <see cref="float.PositiveInfinity"/> to <see cref="float.MaxValue"/> and <see cref="float.NegativeInfinity"/> to <see cref="float.MinValue"/>; otherwise, returns the specified value.</summary>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns>Returns the nearest finite value.</returns>
		public static float MakeFinite( this float value )
		{
			if( float.IsNaN( value ) )
				return 0.0f;

			if( float.IsInfinity( value ) )
			{
				if( float.IsPositiveInfinity( value ) )
					return float.MaxValue;
				return float.MinValue;
			}

			return value;
		}

		/// <summary>Converts <see cref="double.NaN"/> to 0, <see cref="double.PositiveInfinity"/> to <see cref="double.MaxValue"/> and <see cref="double.NegativeInfinity"/> to <see cref="double.MinValue"/>; otherwise, returns the specified value.</summary>
		/// <param name="value">A double-precision floating-point value.</param>
		/// <returns>Returns the nearest finite value.</returns>
		public static double MakeFinite( this double value )
		{
			if( double.IsNaN( value ) )
				return 0.0;

			if( double.IsInfinity( value ) )
			{
				if( double.IsPositiveInfinity( value ) )
					return double.MaxValue;
				return double.MinValue;
			}

			return value;
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
			if( float.IsNaN( value ) )
				value = 0.0f;

			if( value < min )
				return min;
			
			if( value > max )
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
		public static double Clamp( this double value, double min, double max )
		{
			if( double.IsNaN( value ) )
				value = 0.0;

			if( value < min )
				return min;
			
			if( value > max )
				return max;
			
			return value;
		}



		/// <summary>Returns the nearest finite value within the range [0,1].</summary>
		/// <param name="value">The value to be saturated.</param>
		/// <returns>Returns the nearest value within the range [0,1].</returns>
		public static float Saturate( this float value )
		{
			if( float.IsNaN( value ) || value < 0.0f )
				return 0.0f;
			
			if( value > 1.0f )
				return 1.0f;
			
			return value;
		}

		/// <summary>Returns the nearest finite value within the range [0,1].</summary>
		/// <param name="value">The value to be saturated.</param>
		/// <returns>Returns the nearest value within the range [0,1].</returns>
		public static double Saturate( this double value )
		{
			if( double.IsNaN( value ) || value < 0.0 )
				return 0.0;

			if( value > 1.0 )
				return 1.0;

			return value;
		}

		#endregion Extension methods

	}

}