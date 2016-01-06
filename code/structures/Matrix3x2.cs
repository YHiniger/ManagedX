using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{

	/// <summary>A 3x2 (3 rows, 2 columns) matrix.
	/// <para>Used with GetMatrixTransform and SetMatrixTransform to indicate the scaling and translation transform for SwapChainPanel swap chains.</para>
	/// This structure is equivalent to the native <code>DXGI_MATRIX_3X2_F</code> structure (defined in DXGI1_3.h).
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dn268308%28v=vs.85%29.aspx</remarks>
	[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "x" )]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 24 )]
	public struct Matrix3x2 : IEquatable<Matrix3x2>
	{

		/// <summary>Value at row 1 column 1 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M11;
		
		/// <summary>Value at row 1 column 2 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M12;
		
		/// <summary>Value at row 2 column 1 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M21;
		
		/// <summary>Value at row 2 column 2 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M22;
		
		/// <summary>Value at row 3 column 1 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M31;
		
		/// <summary>Value at row 3 column 2 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M32;



		/// <summary>Initializes a new <see cref="Matrix3x2"/>.</summary>
		/// <param name="m11">Value at row 1 column 1 of the matrix.</param>
		/// <param name="m12">Value at row 1 column 2 of the matrix.</param>
		/// <param name="m21">Value at row 2 column 1 of the matrix.</param>
		/// <param name="m22">Value at row 2 column 2 of the matrix.</param>
		/// <param name="m31">Value at row 3 column 1 of the matrix.</param>
		/// <param name="m32">Value at row 3 column 2 of the matrix.</param>
		[SuppressMessage( "Microsoft.Design", "CA1025:ReplaceRepetitiveArgumentsWithParamsArray" )]
		public Matrix3x2( 
			float m11, float m12,
			float m21, float m22,
			float m31, float m32 )
		{
			M11 = m11;
			M12 = m12;
			M21 = m21;
			
			M22 = m22;
			M31 = m31;
			M32 = m32;
		}



		/// <summary>Gets the scale of this <see cref="Matrix3x2"/>.</summary>
		public Vector2 Scale
		{
			get
			{
				Vector2 scale;
				scale.X = (float)Math.Sqrt( (double)( M11 * M11 + M12 * M12 ) );
				scale.Y = (float)Math.Sqrt( (double)( M21 * M21 + M22 * M22 ) );
				return scale;
			}
		}


		/// <summary>Gets or sets the translation part of this <see cref="Matrix3x2"/>.</summary>
		public Vector2 Translation
		{
			get
			{
				Vector2 translation;
				translation.X = M31;
				translation.Y = M32;
				return translation;
			}
			set
			{
				M31 = value.X;
				M32 = value.Y;
			}
		}


		///// <summary>Gets the determinant of this <see cref="Matrix3x2"/>.</summary>
		//public float Determinant { get { return M11 * M22 - M12 * M21; } }


		/// <summary>Negates this <see cref="Matrix3x2"/>.</summary>
		public void Negate()
		{
			M11 = -M11;
			M12 = -M12;
			M21 = -M21;
			M22 = -M22;
			M31 = -M31;
			M32 = -M32;
		}


		/// <summary>Returns a hash code for this <see cref="Matrix3x2"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Matrix3x2"/>.</returns>
		public override int GetHashCode()
		{
			return
				M11.GetHashCode() ^ M12.GetHashCode() ^ M21.GetHashCode() ^ 
				M22.GetHashCode() ^ M31.GetHashCode() ^ M32.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Matrix3x2"/> equals another <see cref="Matrix3x2"/>.</summary>
		/// <param name="other">A <see cref="Matrix3x2"/>.</param>
		/// <returns>Returns true if this <see cref="Matrix3x2"/> and the <paramref name="other"/> <see cref="Matrix3x2"/> are equal, otherwise returns false.</returns>
		public bool Equals( Matrix3x2 other )
		{
			return
				( M11 == other.M11 ) && ( M12 == other.M12 ) && ( M21 == other.M21 ) &&
				( M22 == other.M22 ) && ( M31 == other.M31 ) && ( M32 == other.M32 );
		}


		/// <summary>Returns a value indicating whether this <see cref="Matrix3x2"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Matrix3x2"/> which equals this <see cref="Matrix3x2"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Matrix3x2 ) && this.Equals( (Matrix3x2)obj );
		}


		/// <summary>Returns an array of single-precision floating-point values representing this <see cref="Matrix3x2"/>.</summary>
		/// <returns>Returns an array of single-precision floating-point values representing this <see cref="Matrix3x2"/>.</returns>
		public float[] ToArray()
		{
			return new float[]
			{
				M11, M12,
				M21, M22,
				M31, M32
			};
		}


		/// <summary>Transforms a 2D point.</summary>
		/// <param name="point">A <see cref="Vector2"/>.</param>
		/// <param name="result">Receives the transformed <paramref name="point"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Transform( ref Vector2 point, out Vector2 result )
		{
			var x = point.X;
			result.X = x * M11 + point.Y * M21 + M31;
			result.Y = x * M12 + point.Y * M22 + M32;
		}

		/// <summary>Transforms a 2D point.</summary>
		/// <param name="point">A <see cref="Vector2"/>.</param>
		/// <returns>Returns the transformed <paramref name="point"/>.</returns>
		public Vector2 Transform( Vector2 point )
		{
			var x = point.X;
			point.X = x * M11 + point.Y * M21 + M31;
			point.Y = x * M12 + point.Y * M22 + M32;
			return point;
		}


		/// <summary>Transforms a 2D normal.</summary>
		/// <param name="normal">A <see cref="Vector2"/>.</param>
		/// <param name="result">Receives the transformed <paramref name="normal"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void TransformNormal( ref Vector2 normal, out Vector2 result )
		{
			var x = normal.X;
			result.X = x * M11 + normal.Y * M21;
			result.Y = x * M12 + normal.Y * M22;
		}

		/// <summary>Transforms a 2D normal.</summary>
		/// <param name="normal">A <see cref="Vector2"/>.</param>
		/// <returns>Returns the transformed <paramref name="normal"/>.</returns>
		public Vector2 TransformNormal( Vector2 normal )
		{
			var x = normal.X;
			normal.X = x * M11 + normal.Y * M21;
			normal.Y = x * M12 + normal.Y * M22;
			return normal;
		}


		/// <summary>The "zero" <see cref="Matrix3x2"/>.</summary>
		public static readonly Matrix3x2 Zero;
		

		/// <summary>The identity <see cref="Matrix3x2"/>.</summary>
		public static readonly Matrix3x2 Identity = new Matrix3x2(
			1.0f, 0.0f,
			0.0f, 1.0f,
			0.0f, 0.0f
		);

		
		// TODO - Add, Subtract, Multiply, Divide, Create*


		/// <summary>Performs a linear interpolation between two matrices.</summary>
		/// <param name="source">The source <see cref="Matrix3x2"/>.</param>
		/// <param name="target">The target <see cref="Matrix3x2"/>.</param>
		/// <param name="amount">The weighting factor, within the range [0,1].</param>
		/// <param name="result">Receives the interpolated <see cref="Matrix3x2"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Lerp( ref Matrix3x2 source, ref Matrix3x2 target, float amount, out Matrix3x2 result )
		{
			result.M11 = source.M11 + ( target.M11 - source.M11 ) * amount;
			result.M12 = source.M12 + ( target.M12 - source.M12 ) * amount;
			result.M21 = source.M21 + ( target.M21 - source.M21 ) * amount;
			result.M22 = source.M22 + ( target.M22 - source.M22 ) * amount;
			result.M31 = source.M31 + ( target.M31 - source.M31 ) * amount;
			result.M32 = source.M32 + ( target.M32 - source.M32 ) * amount;
		}

		/// <summary>Linearly interpolates between two matrices.</summary>
		/// <param name="source">The source <see cref="Matrix3x2"/>.</param>
		/// <param name="target">The target <see cref="Matrix3x2"/>.</param>
		/// <param name="amount">The weighting factor, within the range [0,1].</param>
		/// <returns>Returns the interpolated <see cref="Matrix3x2"/>.</returns>
		public static Matrix3x2 Lerp( Matrix3x2 source, Matrix3x2 target, float amount )
		{
			Matrix3x2 result;
			result.M11 = source.M11 + ( target.M11 - source.M11 ) * amount;
			result.M12 = source.M12 + ( target.M12 - source.M12 ) * amount;
			result.M21 = source.M21 + ( target.M21 - source.M21 ) * amount;
			result.M22 = source.M22 + ( target.M22 - source.M22 ) * amount;
			result.M31 = source.M31 + ( target.M31 - source.M31 ) * amount;
			result.M32 = source.M32 + ( target.M32 - source.M32 ) * amount;
			return result;
		}


		#region Create*

		/// <summary>Creates a scale matrix.</summary>
		/// <param name="x">The horizontal scale.</param>
		/// <param name="y">The vertical scale.</param>
		/// <param name="result">Receives the created scale matrix.</param>
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateScale( float x, float y, out Matrix3x2 result )
		{
			result.M11 = x;
			result.M12 = 0.0f;
			result.M21 = 0.0f;
			result.M22 = y;
			result.M31 = 0.0f;
			result.M32 = 0.0f;
		}

		/// <summary>Creates a scale matrix.</summary>
		/// <param name="x">The horizontal scale.</param>
		/// <param name="y">The vertical scale.</param>
		/// <returns>Returns the created scale matrix.</returns>
		public static Matrix3x2 CreateScale( float x, float y )
		{
			Matrix3x2 result;
			result.M11 = x;
			result.M12 = 0.0f;
			result.M21 = 0.0f;
			result.M22 = y;
			result.M31 = 0.0f;
			result.M32 = 0.0f;
			return result;
		}

		/// <summary>Creates a scale matrix.</summary>
		/// <param name="scale">A <see cref="Vector2"/> representing the scaling.</param>
		/// <param name="result">Receives the created scale matrix.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateScale( ref Vector2 scale, out Matrix3x2 result )
		{
			result.M11 = scale.X;
			result.M12 = 0.0f;
			result.M21 = 0.0f;
			result.M22 = scale.Y;
			result.M31 = 0.0f;
			result.M32 = 0.0f;
		}

		/// <summary>Creates a scale matrix.</summary>
		/// <param name="scale">A <see cref="Vector2"/> representing the scaling.</param>
		/// <returns>Returns the created scale matrix.</returns>
		public static Matrix3x2 CreateScale( Vector2 scale )
		{
			Matrix3x2 result;
			result.M11 = scale.X;
			result.M12 = 0.0f;
			result.M21 = 0.0f;
			result.M22 = scale.Y;
			result.M31 = 0.0f;
			result.M32 = 0.0f;
			return result;
		}



		/// <summary>Creates a translation matrix.</summary>
		/// <param name="x">The translation in the X direction.</param>
		/// <param name="y">The translation in the Y direction.</param>
		/// <param name="result">Receives the created translation matrix.</param>
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateTranslation( float x, float y, out Matrix3x2 result )
		{
			result.M11 = 1.0f;
			result.M12 = 0.0f;
			result.M21 = 0.0f;
			result.M22 = 1.0f;
			result.M31 = x;
			result.M32 = y;
		}

		/// <summary>Returns a translation matrix.</summary>
		/// <param name="x">The translation in the X direction.</param>
		/// <param name="y">The translation in the Y direction.</param>
		/// <returns>Returns the created translation matrix.</returns>
		public static Matrix3x2 CreateTranslation( float x, float y )
		{
			Matrix3x2 result;
			result.M11 = 1.0f;
			result.M12 = 0.0f;
			result.M21 = 0.0f;
			result.M22 = 1.0f;
			result.M31 = x;
			result.M32 = y;
			return result;
		}


		/// <summary>Creates a translation matrix.</summary>
		/// <param name="translation">A <see cref="Vector2"/> representing the translation.</param>
		/// <param name="result">Receives the created translation matrix.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateTranslation( ref Vector2 translation, out Matrix3x2 result )
		{
			result.M11 = 1.0f;
			result.M12 = 0.0f;
			result.M21 = 0.0f;
			result.M22 = 1.0f;
			result.M31 = translation.X;
			result.M32 = translation.Y;
		}

		/// <summary>Returns a translation matrix.</summary>
		/// <param name="translation">A <see cref="Vector2"/> representing the translation.</param>
		/// <returns>Returns the created translation matrix.</returns>
		public static Matrix3x2 CreateTranslation( Vector2 translation )
		{
			Matrix3x2 result;
			result.M11 = 1.0f;
			result.M12 = 0.0f;
			result.M21 = 0.0f;
			result.M22 = 1.0f;
			result.M31 = translation.X;
			result.M32 = translation.Y;
			return result;
		}

		#endregion Create*


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="matrix">A <see cref="Matrix3x2"/>.</param>
		/// <param name="other">A <see cref="Matrix3x2"/>.</param>
		/// <returns>Returns true if the specified matrices are equal, otherwise returns false.</returns>
		public static bool operator ==( Matrix3x2 matrix, Matrix3x2 other )
		{
			return
				( matrix.M11 == other.M11 ) && ( matrix.M12 == other.M12 ) && ( matrix.M21 == other.M21 ) &&
				( matrix.M22 == other.M22 ) && ( matrix.M31 == other.M31 ) && ( matrix.M32 == other.M32 );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="matrix">A <see cref="Matrix3x2"/>.</param>
		/// <param name="other">A <see cref="Matrix3x2"/>.</param>
		/// <returns>Returns true if the specified matrices are not equal, otherwise returns false.</returns>
		public static bool operator !=( Matrix3x2 matrix, Matrix3x2 other )
		{
			return
				( matrix.M11 != other.M11 ) || ( matrix.M12 != other.M12 ) || ( matrix.M21 != other.M21 ) ||
				( matrix.M22 != other.M22 ) || ( matrix.M31 != other.M31 ) || ( matrix.M32 != other.M32 );
		}



		/// <summary>Negation operator.</summary>
		/// <param name="matrix">A <see cref="Matrix3x2"/>.</param>
		/// <returns>Returns the result of the negation.</returns>
		public static Matrix3x2 operator -( Matrix3x2 matrix )
		{
			matrix.M11 = -matrix.M11;
			matrix.M12 = -matrix.M12;
			matrix.M21 = -matrix.M21;
			matrix.M22 = -matrix.M22;
			matrix.M31 = -matrix.M31;
			matrix.M32 = -matrix.M32;
			return matrix;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="matrix">A <see cref="Matrix3x2"/>.</param>
		/// <param name="other">A <see cref="Matrix3x2"/>.</param>
		/// <returns>Returns the result of the addition.</returns>
		public static Matrix3x2 operator +( Matrix3x2 matrix, Matrix3x2 other )
		{
			matrix.M11 += other.M11;
			matrix.M12 += other.M12;
			matrix.M21 += other.M21;
			matrix.M22 += other.M22;
			matrix.M31 += other.M31;
			matrix.M32 += other.M32;
			return matrix;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="matrix">A <see cref="Matrix3x2"/>.</param>
		/// <param name="other">A <see cref="Matrix3x2"/>.</param>
		/// <returns>Returns the result of the subtraction.</returns>
		public static Matrix3x2 operator -( Matrix3x2 matrix, Matrix3x2 other )
		{
			matrix.M11 -= other.M11;
			matrix.M12 -= other.M12;
			matrix.M21 -= other.M21;
			matrix.M22 -= other.M22;
			matrix.M31 -= other.M31;
			matrix.M32 -= other.M32;
			return matrix;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="matrix">A <see cref="Matrix3x2"/>.</param>
		/// <param name="other">A <see cref="Matrix3x2"/>.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static Matrix3x2 operator *( Matrix3x2 matrix, Matrix3x2 other )
		{
			Matrix3x2 result;
			result.M11 = matrix.M11 * other.M11 + matrix.M12 * other.M21;
			result.M12 = matrix.M11 * other.M12 + matrix.M12 * other.M22;
			result.M21 = matrix.M21 * other.M11 + matrix.M22 * other.M21;
			result.M22 = matrix.M21 * other.M12 + matrix.M22 * other.M22;
			result.M31 = matrix.M31 * other.M11 + matrix.M32 * other.M21 + other.M31;
			result.M32 = matrix.M31 * other.M12 + matrix.M32 * other.M22 + other.M32;
			return result;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="matrix">A <see cref="Matrix3x2"/>.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static Matrix3x2 operator *( Matrix3x2 matrix, float value )
		{
			matrix.M11 *= value;
			matrix.M12 *= value;
			matrix.M21 *= value;
			matrix.M22 *= value;
			matrix.M31 *= value;
			matrix.M32 *= value;
			return matrix;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="matrix">A <see cref="Matrix3x2"/>.</param>
		/// <returns>Returns the result of the multiplication.</returns>
		public static Matrix3x2 operator *( float value, Matrix3x2 matrix )
		{
			matrix.M11 *= value;
			matrix.M12 *= value;
			matrix.M21 *= value;
			matrix.M22 *= value;
			matrix.M31 *= value;
			matrix.M32 *= value;
			return matrix;
		}


		/// <summary>Division operator.</summary>
		/// <param name="matrix">A <see cref="Matrix3x2"/>.</param>
		/// <param name="other">A <see cref="Matrix3x2"/>.</param>
		/// <returns>Returns the result of the division.</returns>
		public static Matrix3x2 operator /( Matrix3x2 matrix, Matrix3x2 other )
		{
			matrix.M11 /= other.M11;
			matrix.M12 /= other.M12;
			matrix.M21 /= other.M21;
			matrix.M22 /= other.M22;
			matrix.M31 /= other.M31;
			matrix.M32 /= other.M32;
			return matrix;
		}

		/// <summary>Division operator.</summary>
		/// <param name="matrix">A <see cref="Matrix3x2"/>.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns the result of the division.</returns>
		public static Matrix3x2 operator /( Matrix3x2 matrix, float value )
		{
			var oneOverValue = 1.0f / value;
			matrix.M11 *= oneOverValue;
			matrix.M12 *= oneOverValue;
			matrix.M21 *= oneOverValue;
			matrix.M22 *= oneOverValue;
			matrix.M31 *= oneOverValue;
			matrix.M32 *= oneOverValue;
			return matrix;
		}

		/// <summary>Division operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="matrix">A <see cref="Matrix3x2"/>.</param>
		/// <returns>Returns the result of the division.</returns>
		public static Matrix3x2 operator /( float value, Matrix3x2 matrix )
		{
			matrix.M11 = value / matrix.M11;
			matrix.M12 = value / matrix.M12;
			matrix.M21 = value / matrix.M21;
			matrix.M22 = value / matrix.M22;
			matrix.M31 = value / matrix.M31;
			matrix.M32 = value / matrix.M32;
			return matrix;
		}

		#endregion Operators

	}

}