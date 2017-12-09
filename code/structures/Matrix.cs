using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	using Win32;


	/// <summary>A 4x4 matrix.</summary>
	[Source( "D2DBaseTypes.h", "D2D_MATRIX_4X4_F" )]
	[Source( "D2D1.h", "D2D1_MATRIX_4X4_F" )]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 64 )]
	public struct Matrix : IEquatable<Matrix>
	{

		private const float BillboardDistanceSquaredThreshold = 1E-04f;
		private const float ConstrainedBillboardThreshold = 0.998254657f;


		/// <summary>Value at row 1 column 1 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M11;
		
		/// <summary>Value at row 1 column 2 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M12;
		
		/// <summary>Value at row 1 column 3 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M13;
		
		/// <summary>Value at row 1 column 4 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M14;
		
		/// <summary>Value at row 2 column 1 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M21;
		
		/// <summary>Value at row 2 column 2 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M22;
		
		/// <summary>Value at row 2 column 3 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M23;
		
		/// <summary>Value at row 2 column 4 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M24;
		
		/// <summary>Value at row 3 column 1 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M31;
		
		/// <summary>Value at row 3 column 2 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M32;
		
		/// <summary>Value at row 3 column 3 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M33;
		
		/// <summary>Value at row 3 column 4 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M34;
		
		/// <summary>Value at row 4 column 1 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M41;
		
		/// <summary>Value at row 4 column 2 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M42;
		
		/// <summary>Value at row 4 column 3 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M43;
		
		/// <summary>Value at row 4 column 4 of the matrix.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float M44;



        /// <summary>Initializes a new <see cref="Matrix"/>.</summary>
        /// <param name="m11">Value at row 1 column 1 of the matrix.</param>
        /// <param name="m12">Value at row 1 column 2 of the matrix.</param>
        /// <param name="m13">Value at row 1 column 3 of the matrix.</param>
        /// <param name="m14">Value at row 1 column 4 of the matrix.</param>
        /// <param name="m21">Value at row 2 column 1 of the matrix.</param>
        /// <param name="m22">Value at row 2 column 2 of the matrix.</param>
        /// <param name="m23">Value at row 2 column 3 of the matrix.</param>
        /// <param name="m24">Value at row 2 column 4 of the matrix.</param>
        /// <param name="m31">Value at row 3 column 1 of the matrix.</param>
        /// <param name="m32">Value at row 3 column 2 of the matrix.</param>
        /// <param name="m33">Value at row 3 column 3 of the matrix.</param>
        /// <param name="m34">Value at row 3 column 4 of the matrix.</param>
        /// <param name="m41">Value at row 4 column 1 of the matrix.</param>
        /// <param name="m42">Value at row 4 column 2 of the matrix.</param>
        /// <param name="m43">Value at row 4 column 3 of the matrix.</param>
        /// <param name="m44">Value at row 4 column 4 of the matrix.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "m")]
        [SuppressMessage( "Microsoft.Design", "CA1025:ReplaceRepetitiveArgumentsWithParamsArray" )]
		public Matrix( 
			float m11, float m12, float m13, float m14, 
			float m21, float m22, float m23, float m24, 
			float m31, float m32, float m33, float m34, 
			float m41, float m42, float m43, float m44 )
		{
			M11 = m11;
			M12 = m12;
			M13 = m13;
			M14 = m14;
			
			M21 = m21;
			M22 = m22;
			M23 = m23;
			M24 = m24;
			
			M31 = m31;
			M32 = m32;
			M33 = m33;
			M34 = m34;
			
			M41 = m41;
			M42 = m42;
			M43 = m43;
			M44 = m44;
		}



		/// <summary>Gets or sets the left vector of this <see cref="Matrix"/>.</summary>
		public Vector3 Left
		{
			get { return new Vector3( -M11, -M12, -M13 ); }
			set
			{
				M11 = -value.X;
				M12 = -value.Y;
				M13 = -value.Z;
			}
		}


		/// <summary>Gets or sets the right vector of this <see cref="Matrix"/>.</summary>
		public Vector3 Right
		{
			get { return new Vector3( M11, M12, M13 ); }
			set
			{
				M11 = value.X;
				M12 = value.Y;
				M13 = value.Z;
			}
		}


		/// <summary>Gets or sets the up vector of this <see cref="Matrix"/>.</summary>
		public Vector3 Up
		{
			get { return new Vector3( M21, M22, M23 ); }
			set
			{
				M21 = value.X;
				M22 = value.Y;
				M23 = value.Z;
			}
		}


		/// <summary>Gets or sets the down vector of this <see cref="Matrix"/>.</summary>
		public Vector3 Down
		{
			get { return new Vector3( -M21, -M22, -M23 ); }
			set
			{
				M21 = -value.X;
				M22 = -value.Y;
				M23 = -value.Z;
			}
		}


		/// <summary>Gets or sets the forward vector of this <see cref="Matrix"/>.</summary>
		public Vector3 Forward
		{
			get { return new Vector3( -M31, -M32, -M33 ); }
			set
			{
				M31 = -value.X;
				M32 = -value.Y;
				M33 = -value.Z;
			}
		}


		/// <summary>Gets or sets the backward vector of this <see cref="Matrix"/>.</summary>
		public Vector3 Backward
		{
			get { return new Vector3( M31, M32, M33 ); }
			set
			{
				M31 = value.X;
				M32 = value.Y;
				M33 = value.Z;
			}
		}


		/// <summary>Gets the scale of this <see cref="Matrix"/>.</summary>
		public Vector3 Scale
		{
			get
			{
				Vector3 scale;
				scale.X = (float)Math.Sqrt( (double)( M11 * M11 + M12 * M12 + M13 * M13 ) );
				scale.Y = (float)Math.Sqrt( (double)( M21 * M21 + M22 * M22 + M23 * M23 ) );
				scale.Z = (float)Math.Sqrt( (double)( M31 * M31 + M32 * M32 + M33 * M33 ) );
				return scale;
			}
		}


		/// <summary>Gets a <see cref="Quaternion"/> representing the rotation of this <see cref="Matrix"/>.</summary>
		public Quaternion Rotation
		{
			get
			{
				Quaternion result;

				var sum = M11 + M22 + M33;
				float n;
				if( sum > 0.0f )
				{
					n = (float)Math.Sqrt( (double)( 1.0f + sum ) );
					result.W = n * 0.5f;
					n = 0.5f / n;
					result.X = ( M23 - M32 ) * n;
					result.Y = ( M31 - M13 ) * n;
					result.Z = ( M12 - M21 ) * n;
					return result;
				}

				if( ( M11 >= M22 ) && ( M11 >= M33 ) )
				{
					n = (float)Math.Sqrt( (double)( 1.0f + M11 - M22 - M33 ) );
					result.X = 0.5f * n;
					n = 0.5f / n;
					result.Y = ( M12 + M21 ) * n;
					result.Z = ( M13 + M31 ) * n;
					result.W = ( M23 - M32 ) * n;
					return result;
				}

				if( M22 > M33 )
				{
					n = (float)Math.Sqrt( (double)( 1.0f + M22 - M11 - M33 ) );
					result.Y = 0.5f * n;
					n = 0.5f / n;
					result.X = ( M21 + M12 ) * n;
					result.Z = ( M32 + M23 ) * n;
					result.W = ( M31 - M13 ) * n;
					return result;
				}

				n = (float)Math.Sqrt( (double)( 1.0f + M33 - M11 - M22 ) );
				result.Z = 0.5f * n;
				n = 0.5f / n;
				result.X = ( M31 + M13 ) * n;
				result.Y = ( M32 + M23 ) * n;
				result.W = ( M12 - M21 ) * n;
				return result;
			}
		}


		/// <summary>Gets or sets the translation of this <see cref="Matrix"/>.</summary>
		public Vector3 Translation
		{
			get { return new Vector3( M41, M42, M43 ); }
			set
			{
				M41 = value.X;
				M42 = value.Y;
				M43 = value.Z;
			}
		}


		/// <summary>Negates this <see cref="Matrix"/>.</summary>
		public void Negate()
		{
			M11 = -M11;
			M12 = -M12;
			M13 = -M13;
			M14 = -M14;
			M21 = -M21;
			M22 = -M22;
			M23 = -M23;
			M24 = -M24;
			M31 = -M31;
			M32 = -M32;
			M33 = -M33;
			M34 = -M34;
			M41 = -M41;
			M42 = -M42;
			M43 = -M43;
			M44 = -M44;
		}


		/// <summary>Gets the determinant of this <see cref="Matrix"/>.</summary>
		public float Determinant
		{
			get
			{
				var a = M33 * M44 - M34 * M43;
				var b = M32 * M44 - M34 * M42;
				var c = M32 * M43 - M33 * M42;
				var d = M31 * M44 - M34 * M41;
				var e = M31 * M43 - M33 * M41;
				var f = M31 * M42 - M32 * M41;
				return M11 * ( M22 * a - M23 * b + M24 * c ) - M12 * ( M21 * a - M23 * d + M24 * e ) + M13 * ( M21 * b - M22 * d + M24 * f ) - M14 * ( M21 * c - M22 * e + M23 * f );
			}
		}


		#region Transform, TransformNormal (Vector2)

		/// <summary>Transforms a <see cref="Vector2"/>.</summary>
		/// <param name="vector">A <see cref="Vector2"/>.</param>
		/// <param name="result">Receives the transformed <paramref name="vector"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Transform( ref Vector2 vector, out Vector2 result )
		{
			var x = vector.X;
			result.X = x * M11 + vector.Y * M21 + M41;
			result.Y = x * M12 + vector.Y * M22 + M42;
		}

		/// <summary>Returns a rotated <see cref="Vector2"/>.</summary>
		/// <param name="vector">A <see cref="Vector2"/>.</param>
		/// <returns>Returns the transformed <paramref name="vector"/>.</returns>
		public Vector2 Transform( Vector2 vector )
		{
			Vector2 result;
			result.X = vector.X * M11 + vector.Y * M21 + M41;
			result.Y = vector.X * M12 + vector.Y * M22 + M42;
			return result;
		}

		/// <summary>Transforms multiple <see cref="Vector2"/>.</summary>
		/// <param name="source">An array of <see cref="Vector2"/>; must not be null.</param>
		/// <param name="sourceIndex">The index in the <paramref name="source"/> array to start reading from; must be greater than or equal to zero.</param>
		/// <param name="destination">An array of <see cref="Vector2"/> structures, to receive the transformed vectors; must not be null.</param>
		/// <param name="destinationIndex">The index in the <paramref name="destination"/> array the transformed vectors should be copied to; must be greater than or equal to zero.</param>
		/// <param name="count">The number of vectors to transform; must be greater than zero.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void Transform( Vector2[] source, int sourceIndex, Vector2[] destination, int destinationIndex, int count )
		{
			if( source == null )
				throw new ArgumentNullException( "source" );

			if( sourceIndex < 0 || sourceIndex >= source.Length )
				throw new ArgumentOutOfRangeException( "sourceIndex" );

			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( destinationIndex < 0 || destinationIndex >= destination.Length )
				throw new ArgumentOutOfRangeException( "destinationIndex" );

			if( count <= 0 || source.Length <= count + sourceIndex || destination.Length <= count + destinationIndex )
				throw new ArgumentOutOfRangeException( "count" );

			Vector2 input, output;
			for( var index = 0; index < count; index++ )
			{
				input = source[ sourceIndex + index ];
				
				output.X = input.X * M11 + input.Y * M21 + M41;
				output.Y = input.X * M12 + input.Y * M22 + M42;
				
				destination[ destinationIndex + index ] = output;
			}
		}


		/// <summary>Transforms a 2D normal.</summary>
		/// <param name="normal">A 2D normal.</param>
		/// <param name="result">Receives the transformed <paramref name="normal"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void TransformNormal( ref Vector2 normal, out Vector2 result )
		{
			var x = normal.X;
			result.X = x * M11 + normal.Y * M21;
			result.Y = x * M12 + normal.Y * M22;
		}

		/// <summary>Returns a transformed 2D normal.</summary>
		/// <param name="normal">A 2D normal.</param>
		/// <returns>Returns the transformed 2D <paramref name="normal"/>.</returns>
		public Vector2 TransformNormal( Vector2 normal )
		{
			Vector2 result;
			result.X = normal.X * M11 + normal.Y * M21;
			result.Y = normal.X * M12 + normal.Y * M22;
			return result;
		}

		/// <summary>Transforms multiple 2D normals.</summary>
		/// <param name="source">An array of <see cref="Vector2"/> representing the normals to transform; must not be null.</param>
		/// <param name="sourceIndex">The index in the <paramref name="source"/> array to start reading from; must be greater than or equal to zero.</param>
		/// <param name="destination">An array of <see cref="Vector2"/> which receives the transformed normals; must not be null.</param>
		/// <param name="destinationIndex">The index in the <paramref name="destination"/> array the transformed normals should be copied to; must be greater than or equal to zero.</param>
		/// <param name="count">The number of normals to transform; must be greater than zero.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void TransformNormal( Vector2[] source, int sourceIndex, Vector2[] destination, int destinationIndex, int count )
		{
			if( source == null )
				throw new ArgumentNullException( "source" );

			if( sourceIndex < 0 || sourceIndex >= source.Length )
				throw new ArgumentOutOfRangeException( "sourceIndex" );

			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( destinationIndex < 0 || destinationIndex >= destination.Length )
				throw new ArgumentOutOfRangeException( "destinationIndex" );

			if( count <= 0 || source.Length <= count + sourceIndex || destination.Length <= count + destinationIndex )
				throw new ArgumentOutOfRangeException( "count" );

			Vector2 input, output;
			for( var index = 0; index < count; index++ )
			{
				input = source[ sourceIndex + index ];

				output.X = input.X * M11 + input.Y * M21;
				output.Y = input.X * M12 + input.Y * M22;

				destination[ destinationIndex + index ] = output;
			}
		}

		#endregion Transform, TransformNormal (Vector2)
		
		#region Transform, TransformNormal (Vector3)

		/// <summary>Transforms a <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/>.</param>
		/// <param name="result">Receives the transformed <paramref name="vector"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Transform( ref Vector3 vector, out Vector3 result )
		{
			var x = vector.X;
			var y = vector.Y;
			result.X = x * M11 + y * M21 + vector.Z * M31 + M41;
			result.Y = x * M12 + y * M22 + vector.Z * M32 + M42;
			result.Z = x * M13 + y * M23 + vector.Z * M33 + M43;
		}

		/// <summary>Returns a transformed <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/>.</param>
		/// <returns>Returns the transformed <paramref name="vector"/>.</returns>
		public Vector3 Transform( Vector3 vector )
		{
			Vector3 result;
			result.X = vector.X * M11 + vector.Y * M21 + vector.Z * M31 + M41;
			result.Y = vector.X * M12 + vector.Y * M22 + vector.Z * M32 + M42;
			result.Z = vector.X * M13 + vector.Y * M23 + vector.Z * M33 + M43;
			return result;
		}

		/// <summary>Transforms multiple <see cref="Vector3"/>.</summary>
		/// <param name="source">An array of <see cref="Vector3"/>; must not be null.</param>
		/// <param name="sourceIndex">The index in the <paramref name="source"/> array to start reading from; must be greater than or equal to zero.</param>
		/// <param name="destination">An array of <see cref="Vector3"/>, to receive the transformed vectors; must not be null.</param>
		/// <param name="destinationIndex">The index in the <paramref name="destination"/> array the transformed vectors should be copied to; must be greater than or equal to zero.</param>
		/// <param name="count">The number of vectors to transform; must be greater than zero.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void Transform( Vector3[] source, int sourceIndex, Vector3[] destination, int destinationIndex, int count )
		{
			if( source == null )
				throw new ArgumentNullException( "source" );

			if( sourceIndex < 0 || sourceIndex >= source.Length )
				throw new ArgumentOutOfRangeException( "sourceIndex" );

			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( destinationIndex < 0 || destinationIndex >= destination.Length )
				throw new ArgumentOutOfRangeException( "destinationIndex" );

			if( count <= 0 || source.Length <= count + sourceIndex || destination.Length <= count + destinationIndex )
				throw new ArgumentOutOfRangeException( "count" );


			Vector3 input, output;
			for( var index = 0; index < count; index++ )
			{
				input = source[ sourceIndex + index ];

				output.X = input.X * M11 + input.Y * M21 + input.Z * M31 + M41;
				output.Y = input.X * M12 + input.Y * M22 + input.Z * M32 + M42;
				output.Z = input.X * M13 + input.Y * M23 + input.Z * M33 + M43;
	
				destination[ destinationIndex + index ] = output;
			}
		}


		/// <summary>Transforms a 3D normal.</summary>
		/// <param name="normal">A 3D normal.</param>
		/// <param name="result">Receives the transformed 3D <paramref name="normal"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void TransformNormal( ref Vector3 normal, out Vector3 result )
		{
			var x = normal.X;
			var y = normal.Y;
			result.X = x * M11 + y * M21 + normal.Z * M31;
			result.Y = x * M12 + y * M22 + normal.Z * M32;
			result.Z = x * M13 + y * M23 + normal.Z * M33;
		}

		/// <summary>Returns a transformed 3D normal.</summary>
		/// <param name="normal">A 3D normal.</param>
		/// <returns>Returns the transformed 3D <paramref name="normal"/>.</returns>
		public Vector3 TransformNormal( Vector3 normal )
		{
			Vector3 result;
			result.X = normal.X * M11 + normal.Y * M21 + normal.Z * M31;
			result.Y = normal.X * M12 + normal.Y * M22 + normal.Z * M32;
			result.Z = normal.X * M13 + normal.Y * M23 + normal.Z * M33;
			return result;
		}

		/// <summary>Transforms multiple 3D normals.</summary>
		/// <param name="source">An array of <see cref="Vector3"/> representing the normals to transform; must not be null.</param>
		/// <param name="sourceIndex">The index in the <paramref name="source"/> array to start reading from; must be greater than or equal to zero.</param>
		/// <param name="destination">An array of <see cref="Vector3"/> structures, to receive the transformed normals; must not be null.</param>
		/// <param name="destinationIndex">The index in the <paramref name="destination"/> array the transformed normals should be copied to; must be greater than or equal to zero.</param>
		/// <param name="count">The number of normals to transform; must be greater than zero.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void TransformNormal( Vector3[] source, int sourceIndex, Vector3[] destination, int destinationIndex, int count )
		{
			if( source == null )
				throw new ArgumentNullException( "source" );

			if( sourceIndex < 0 || sourceIndex >= source.Length )
				throw new ArgumentOutOfRangeException( "sourceIndex" );

			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( destinationIndex < 0 || destinationIndex >= destination.Length )
				throw new ArgumentOutOfRangeException( "destinationIndex" );

			if( count <= 0 || source.Length <= count + sourceIndex || destination.Length <= count + destinationIndex )
				throw new ArgumentOutOfRangeException( "count" );

			Vector3 input, output;
			for( var index = 0; index < count; index++ )
			{
				input = source[ sourceIndex + index ];
				
				output.X = input.X * M11 + input.Y * M21 + input.Z * M31;
				output.Y = input.X * M12 + input.Y * M22 + input.Z * M32;
				output.Z = input.X * M13 + input.Y * M23 + input.Z * M33;

				destination[ destinationIndex + index ] = output;
			}
		}

		#endregion Transform, TransformNormal (Vector3)

		#region Transform (Vector4)

		/// <summary>Transforms a <see cref="Vector4"/>.</summary>
		/// <param name="vector">A <see cref="Vector4"/>.</param>
		/// <param name="result">Receives the transformed <paramref name="vector"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Transform( ref Vector4 vector, out Vector4 result )
		{
			var x = vector.X;
			var y = vector.Y;
			var z = vector.Z;
			result.X = x * M11 + y * M21 + z * M31 + vector.W * M41;
			result.Y = x * M12 + y * M22 + z * M32 + vector.W * M42;
			result.Z = x * M13 + y * M23 + z * M33 + vector.W * M43;
			result.W = x * M14 + y * M24 + z * M34 + vector.W * M44;
		}

		/// <summary>Returns a transformed <see cref="Vector4"/>.</summary>
		/// <param name="vector">A <see cref="Vector4"/>.</param>
		/// <returns>Returns the transformed <paramref name="vector"/>.</returns>
		public Vector4 Transform( Vector4 vector )
		{
			Vector4 result;
			result.X = vector.X * M11 + vector.Y * M21 + vector.Z * M31 + vector.W * M41;
			result.Y = vector.X * M12 + vector.Y * M22 + vector.Z * M32 + vector.W * M42;
			result.Z = vector.X * M13 + vector.Y * M23 + vector.Z * M33 + vector.W * M43;
			result.W = vector.X * M14 + vector.Y * M24 + vector.Z * M34 + vector.W * M44;
			return result;
		}

		/// <summary>Transforms multiple <see cref="Vector4"/>.</summary>
		/// <param name="source">An array of <see cref="Vector4"/>; must not be null.</param>
		/// <param name="sourceIndex">The index in the <paramref name="source"/> array to start reading from; must be greater than or equal to zero.</param>
		/// <param name="destination">An array of <see cref="Vector4"/>, to receive the transformed vectors; must not be null.</param>
		/// <param name="destinationIndex">The index in the <paramref name="destination"/> array the transformed vectors should be copied to; must be greater than or equal to zero.</param>
		/// <param name="count">The number of vectors to transform; must be greater than zero.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void Transform( Vector4[] source, int sourceIndex, Vector4[] destination, int destinationIndex, int count )
		{
			if( source == null )
				throw new ArgumentNullException( "source" );

			if( sourceIndex < 0 || sourceIndex >= source.Length )
				throw new ArgumentOutOfRangeException( "sourceIndex" );

			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( destinationIndex < 0 || destinationIndex >= destination.Length )
				throw new ArgumentOutOfRangeException( "destinationIndex" );

			if( count <= 0 || source.Length <= count + sourceIndex || destination.Length <= count + destinationIndex )
				throw new ArgumentOutOfRangeException( "count" );

			Vector4 input, output;
			for( var index = 0; index < count; index++ )
			{
				input = source[ sourceIndex + index ];

				output.X = input.X * M11 + input.Y * M21 + input.Z * M31 + input.W * M41;
				output.Y = input.X * M12 + input.Y * M22 + input.Z * M32 + input.W * M42;
				output.Z = input.X * M13 + input.Y * M23 + input.Z * M33 + input.W * M43;
				output.W = input.X * M14 + input.Y * M24 + input.Z * M34 + input.W * M44;

				destination[ destinationIndex + index ] = output;
			}
		}

		#endregion Transform (Vector4)

		#region Transform (Plane)

		/// <summary>Transforms a <see cref="Plane"/>.</summary>
		/// <param name="plane">A <see cref="Plane"/>.</param>
		/// <param name="result">Receives the transformed <see cref="Plane"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1062" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Transform( ref Plane plane, out Plane result )
		{
			Invert( ref this, out Matrix inverted );

			var x = plane.Normal.X;
			var y = plane.Normal.Y;
			var z = plane.Normal.Z;
			var d = plane.Distance;

			result.Normal.X = x * inverted.M11 + y * inverted.M12 + z * inverted.M13 + d * inverted.M14;
			result.Normal.Y = x * inverted.M21 + y * inverted.M22 + z * inverted.M23 + d * inverted.M24;
			result.Normal.Z = x * inverted.M31 + y * inverted.M32 + z * inverted.M33 + d * inverted.M34;
			result.Distance = x * inverted.M41 + y * inverted.M42 + z * inverted.M43 + d * inverted.M44;
		}

		/// <summary>Returns a transformed <see cref="Plane"/>.</summary>
		/// <param name="plane">A <see cref="Plane"/>.</param>
		/// <returns>Returns the transformed <paramref name="plane"/>.</returns>
		public Plane Transform( Plane plane )
		{
			Invert( ref this, out Matrix inverted );

			var x = plane.Normal.X;
			var y = plane.Normal.Y;
			var z = plane.Normal.Z;
			var d = plane.Distance;

			Plane result;
			result.Normal.X = x * inverted.M11 + y * inverted.M12 + z * inverted.M13 + d * inverted.M14;
			result.Normal.Y = x * inverted.M21 + y * inverted.M22 + z * inverted.M23 + d * inverted.M24;
			result.Normal.Z = x * inverted.M31 + y * inverted.M32 + z * inverted.M33 + d * inverted.M34;
			result.Distance = x * inverted.M41 + y * inverted.M42 + z * inverted.M43 + d * inverted.M44;
			return result;
		}

		/// <summary>Transforms multiple <see cref="Plane"/>.</summary>
		/// <param name="source">An array of <see cref="Plane"/>; must not be null.</param>
		/// <param name="sourceIndex">The index in the <paramref name="source"/> array to start reading from; must be greater than or equal to zero.</param>
		/// <param name="destination">An array of <see cref="Plane"/>, to receive the transformed planes; must not be null.</param>
		/// <param name="destinationIndex">The index in the <paramref name="destination"/> array the transformed planes should be copied to; must be greater than or equal to zero.</param>
		/// <param name="count">The number of planes to transform; must be greater than zero.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void Transform( Plane[] source, int sourceIndex, Plane[] destination, int destinationIndex, int count )
		{
			if( source == null )
				throw new ArgumentNullException( "source" );

			if( sourceIndex < 0 || sourceIndex >= source.Length )
				throw new ArgumentOutOfRangeException( "sourceIndex" );

			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( destinationIndex < 0 || destinationIndex >= destination.Length )
				throw new ArgumentOutOfRangeException( "destinationIndex" );

			if( count <= 0 || source.Length <= count + sourceIndex || destination.Length <= count + destinationIndex )
				throw new ArgumentOutOfRangeException( "count" );

			Invert( ref this, out Matrix inverted );

			Plane input, output;
			float x, y, z, d;
			for( var p = 0; p < count; p++ )
			{
				input = source[ sourceIndex + p ];
				
				x = input.Normal.X;
				y = input.Normal.Y;
				z = input.Normal.Z;
				d = input.Distance;

				output.Normal.X = x * inverted.M11 + y * inverted.M12 + z * inverted.M13 + d * inverted.M14;
				output.Normal.Y = x * inverted.M21 + y * inverted.M22 + z * inverted.M23 + d * inverted.M24;
				output.Normal.Z = x * inverted.M31 + y * inverted.M32 + z * inverted.M33 + d * inverted.M34;
				output.Distance = x * inverted.M41 + y * inverted.M42 + z * inverted.M43 + d * inverted.M44;

				destination[ destinationIndex + p ] = output;
			}
		}

		#endregion Transform (Plane)

		#region Transform (BoundingSphere)

		/// <summary>Transforms a <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <param name="result">Receives the transformed <paramref name="sphere"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Transform( ref BoundingSphere sphere, out BoundingSphere result )
		{
			var xScaleSquared = M11 * M11 + M12 * M12 + M13 * M13;
			var yScaleSquared = M21 * M21 + M22 * M22 + M23 * M23;
			var zScaleSquared = M31 * M31 + M32 * M32 + M33 * M33;

			this.Transform( ref sphere.Center, out result.Center );
			result.Radius = sphere.Radius * (float)Math.Sqrt( (double)Math.Max( xScaleSquared, Math.Max( yScaleSquared, zScaleSquared ) ) );
		}

		/// <summary>Returns a transformed <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <returns>Returns the transformed <paramref name="sphere"/>.</returns>
		public BoundingSphere Transform( BoundingSphere sphere )
		{
			var xScaleSquared = M11 * M11 + M12 * M12 + M13 * M13;
			var yScaleSquared = M21 * M21 + M22 * M22 + M23 * M23;
			var zScaleSquared = M31 * M31 + M32 * M32 + M33 * M33;

			this.Transform( ref sphere.Center, out sphere.Center );
			sphere.Radius *= (float)Math.Sqrt( (double)Math.Max( xScaleSquared, Math.Max( yScaleSquared, zScaleSquared ) ) );
			return sphere;
		}

		#endregion Transform (BoundingSphere)


		/// <summary>Returns a hash code for this <see cref="Matrix"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Matrix"/>.</returns>
		public override int GetHashCode()
		{
			return
				M11.GetHashCode() ^ M12.GetHashCode() ^ M13.GetHashCode() ^ M14.GetHashCode() ^
				M21.GetHashCode() ^ M22.GetHashCode() ^ M23.GetHashCode() ^ M24.GetHashCode() ^
				M31.GetHashCode() ^ M32.GetHashCode() ^ M33.GetHashCode() ^ M34.GetHashCode() ^
				M41.GetHashCode() ^ M42.GetHashCode() ^ M43.GetHashCode() ^ M44.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Matrix"/> equals another <see cref="Matrix"/>.</summary>
		/// <param name="other">A <see cref="Matrix"/>.</param>
		/// <returns>Returns true if this <see cref="Matrix"/> and the <paramref name="other"/> <see cref="Matrix"/> are equal, otherwise returns false.</returns>
		public bool Equals( Matrix other )
		{
			return
				( M11 == other.M11 ) && ( M12 == other.M12 ) && ( M13 == other.M13 ) && ( M14 == other.M14 ) &&
				( M21 == other.M21 ) && ( M22 == other.M22 ) && ( M23 == other.M23 ) && ( M24 == other.M24 ) &&
				( M31 == other.M31 ) && ( M32 == other.M32 ) && ( M33 == other.M33 ) && ( M34 == other.M34 ) &&
				( M41 == other.M41 ) && ( M42 == other.M42 ) && ( M43 == other.M43 ) && ( M44 == other.M44 );
		}

		// used by BoundingFrustum
		internal bool Equals( ref Matrix other )
		{
			return
				( M11 == other.M11 ) && ( M12 == other.M12 ) && ( M13 == other.M13 ) && ( M14 == other.M14 ) &&
				( M21 == other.M21 ) && ( M22 == other.M22 ) && ( M23 == other.M23 ) && ( M24 == other.M24 ) &&
				( M31 == other.M31 ) && ( M32 == other.M32 ) && ( M33 == other.M33 ) && ( M34 == other.M34 ) &&
				( M41 == other.M41 ) && ( M42 == other.M42 ) && ( M43 == other.M43 ) && ( M44 == other.M44 );
		}


		/// <summary>Returns a value indicating whether this <see cref="Matrix"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Matrix"/> which equals this <see cref="Matrix"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is Matrix m && this.Equals( m );
		}


		/// <summary>Returns an array of single-precision floating-point values representing this <see cref="Matrix"/>.</summary>
		/// <returns>Returns an array of single-precision floating-point values representing this <see cref="Matrix"/>.</returns>
		public float[] ToArray()
		{
			return new float[]
			{
				M11, M12, M13, M14,
				M21, M22, M23, M24,
				M31, M32, M33, M34,
				M41, M42, M43, M44
			};
		}



		/// <summary>The «zero» (and invalid) <see cref="Matrix"/>.</summary>
		public static readonly Matrix Zero;


		/// <summary>The identity <see cref="Matrix"/>.</summary>
		public static readonly Matrix Identity = new Matrix(
			1.0f, 0.0f, 0.0f, 0.0f,
			0.0f, 1.0f, 0.0f, 0.0f,
			0.0f, 0.0f, 1.0f, 0.0f,
			0.0f, 0.0f, 0.0f, 1.0f
		);


		#region Static methods

		/// <summary>Adds two matrices.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="other">A <see cref="Matrix"/> structure.</param>
		/// <param name="result">Receives a <see cref="Matrix"/> initialized with the sum of the specified matrices.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Add( ref Matrix matrix, ref Matrix other, out Matrix result )
		{
			result.M11 = matrix.M11 + other.M11;
			result.M12 = matrix.M12 + other.M12;
			result.M13 = matrix.M13 + other.M13;
			result.M14 = matrix.M14 + other.M14;

			result.M21 = matrix.M21 + other.M21;
			result.M22 = matrix.M22 + other.M22;
			result.M23 = matrix.M23 + other.M23;
			result.M24 = matrix.M24 + other.M24;

			result.M31 = matrix.M31 + other.M31;
			result.M32 = matrix.M32 + other.M32;
			result.M33 = matrix.M33 + other.M33;
			result.M34 = matrix.M34 + other.M34;

			result.M41 = matrix.M41 + other.M41;
			result.M42 = matrix.M42 + other.M42;
			result.M43 = matrix.M43 + other.M43;
			result.M44 = matrix.M44 + other.M44;
		}

		/// <summary>Returns the sum of two matrices.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="other">A <see cref="Matrix"/> structure.</param>
		/// <returns>Returns a <see cref="Matrix"/> initialized with the sum of the specified matrices.</returns>
		public static Matrix Add( Matrix matrix, Matrix other )
		{
			matrix.M11 += other.M11;
			matrix.M12 += other.M12;
			matrix.M13 += other.M13;
			matrix.M14 += other.M14;

			matrix.M21 += other.M21;
			matrix.M22 += other.M22;
			matrix.M23 += other.M23;
			matrix.M24 += other.M24;

			matrix.M31 += other.M31;
			matrix.M32 += other.M32;
			matrix.M33 += other.M33;
			matrix.M34 += other.M34;

			matrix.M41 += other.M41;
			matrix.M42 += other.M42;
			matrix.M43 += other.M43;
			matrix.M44 += other.M44;

			return matrix;
		}


		/// <summary>Subtracts a <see cref="Matrix"/> (<paramref name="other"/>) from another <see cref="Matrix"/> (<paramref name="matrix"/>).</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="other">A <see cref="Matrix"/> structure.</param>
		/// <param name="result">Receives a <see cref="Matrix"/> initialized with the sum of the specified matrices.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Subtract( ref Matrix matrix, ref Matrix other, out Matrix result )
		{
			result.M11 = matrix.M11 - other.M11;
			result.M12 = matrix.M12 - other.M12;
			result.M13 = matrix.M13 - other.M13;
			result.M14 = matrix.M14 - other.M14;

			result.M21 = matrix.M21 - other.M21;
			result.M22 = matrix.M22 - other.M22;
			result.M23 = matrix.M23 - other.M23;
			result.M24 = matrix.M24 - other.M24;

			result.M31 = matrix.M31 - other.M31;
			result.M32 = matrix.M32 - other.M32;
			result.M33 = matrix.M33 - other.M33;
			result.M34 = matrix.M34 - other.M34;

			result.M41 = matrix.M41 - other.M41;
			result.M42 = matrix.M42 - other.M42;
			result.M43 = matrix.M43 - other.M43;
			result.M44 = matrix.M44 - other.M44;
		}

		/// <summary>Returns the difference between two matrices.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="other">A <see cref="Matrix"/> structure.</param>
		/// <returns>Returns a <see cref="Matrix"/> initialized with the difference between the specified matrices.</returns>
		public static Matrix Subtract( Matrix matrix, Matrix other )
		{
			matrix.M11 -= other.M11;
			matrix.M12 -= other.M12;
			matrix.M13 -= other.M13;
			matrix.M14 -= other.M14;

			matrix.M21 -= other.M21;
			matrix.M22 -= other.M22;
			matrix.M23 -= other.M23;
			matrix.M24 -= other.M24;

			matrix.M31 -= other.M31;
			matrix.M32 -= other.M32;
			matrix.M33 -= other.M33;
			matrix.M34 -= other.M34;

			matrix.M41 -= other.M41;
			matrix.M42 -= other.M42;
			matrix.M43 -= other.M43;
			matrix.M44 -= other.M44;

			return matrix;
		}


		/// <summary>Divides a <see cref="Matrix"/> by another <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="other">A <see cref="Matrix"/> structure.</param>
		/// <param name="result">Receives the result of the operation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Multiply( ref Matrix matrix, ref Matrix other, out Matrix result )
		{
			result.M11 = matrix.M11 * other.M11 + matrix.M12 * other.M21 + matrix.M13 * other.M31 + matrix.M14 * other.M41;
			result.M12 = matrix.M11 * other.M12 + matrix.M12 * other.M22 + matrix.M13 * other.M32 + matrix.M14 * other.M42;
			result.M13 = matrix.M11 * other.M13 + matrix.M12 * other.M23 + matrix.M13 * other.M33 + matrix.M14 * other.M43;
			result.M14 = matrix.M11 * other.M14 + matrix.M12 * other.M24 + matrix.M13 * other.M34 + matrix.M14 * other.M44;
			result.M21 = matrix.M21 * other.M11 + matrix.M22 * other.M21 + matrix.M23 * other.M31 + matrix.M24 * other.M41;
			result.M22 = matrix.M21 * other.M12 + matrix.M22 * other.M22 + matrix.M23 * other.M32 + matrix.M24 * other.M42;
			result.M23 = matrix.M21 * other.M13 + matrix.M22 * other.M23 + matrix.M23 * other.M33 + matrix.M24 * other.M43;
			result.M24 = matrix.M21 * other.M14 + matrix.M22 * other.M24 + matrix.M23 * other.M34 + matrix.M24 * other.M44;
			result.M31 = matrix.M31 * other.M11 + matrix.M32 * other.M21 + matrix.M33 * other.M31 + matrix.M34 * other.M41;
			result.M32 = matrix.M31 * other.M12 + matrix.M32 * other.M22 + matrix.M33 * other.M32 + matrix.M34 * other.M42;
			result.M33 = matrix.M31 * other.M13 + matrix.M32 * other.M23 + matrix.M33 * other.M33 + matrix.M34 * other.M43;
			result.M34 = matrix.M31 * other.M14 + matrix.M32 * other.M24 + matrix.M33 * other.M34 + matrix.M34 * other.M44;
			result.M41 = matrix.M41 * other.M11 + matrix.M42 * other.M21 + matrix.M43 * other.M31 + matrix.M44 * other.M41;
			result.M42 = matrix.M41 * other.M12 + matrix.M42 * other.M22 + matrix.M43 * other.M32 + matrix.M44 * other.M42;
			result.M43 = matrix.M41 * other.M13 + matrix.M42 * other.M23 + matrix.M43 * other.M33 + matrix.M44 * other.M43;
			result.M44 = matrix.M41 * other.M14 + matrix.M42 * other.M24 + matrix.M43 * other.M34 + matrix.M44 * other.M44;
		}

		/// <summary>Divides a <see cref="Matrix"/> by another <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="other">A <see cref="Matrix"/> structure.</param>
		/// <returns>Returns a <see cref="Matrix"/> initialized with the difference between the specified matrices.</returns>
		public static Matrix Multiply( Matrix matrix, Matrix other )
		{
			Matrix result;
			result.M11 = matrix.M11 * other.M11 + matrix.M12 * other.M21 + matrix.M13 * other.M31 + matrix.M14 * other.M41;
			result.M12 = matrix.M11 * other.M12 + matrix.M12 * other.M22 + matrix.M13 * other.M32 + matrix.M14 * other.M42;
			result.M13 = matrix.M11 * other.M13 + matrix.M12 * other.M23 + matrix.M13 * other.M33 + matrix.M14 * other.M43;
			result.M14 = matrix.M11 * other.M14 + matrix.M12 * other.M24 + matrix.M13 * other.M34 + matrix.M14 * other.M44;
			result.M21 = matrix.M21 * other.M11 + matrix.M22 * other.M21 + matrix.M23 * other.M31 + matrix.M24 * other.M41;
			result.M22 = matrix.M21 * other.M12 + matrix.M22 * other.M22 + matrix.M23 * other.M32 + matrix.M24 * other.M42;
			result.M23 = matrix.M21 * other.M13 + matrix.M22 * other.M23 + matrix.M23 * other.M33 + matrix.M24 * other.M43;
			result.M24 = matrix.M21 * other.M14 + matrix.M22 * other.M24 + matrix.M23 * other.M34 + matrix.M24 * other.M44;
			result.M31 = matrix.M31 * other.M11 + matrix.M32 * other.M21 + matrix.M33 * other.M31 + matrix.M34 * other.M41;
			result.M32 = matrix.M31 * other.M12 + matrix.M32 * other.M22 + matrix.M33 * other.M32 + matrix.M34 * other.M42;
			result.M33 = matrix.M31 * other.M13 + matrix.M32 * other.M23 + matrix.M33 * other.M33 + matrix.M34 * other.M43;
			result.M34 = matrix.M31 * other.M14 + matrix.M32 * other.M24 + matrix.M33 * other.M34 + matrix.M34 * other.M44;
			result.M41 = matrix.M41 * other.M11 + matrix.M42 * other.M21 + matrix.M43 * other.M31 + matrix.M44 * other.M41;
			result.M42 = matrix.M41 * other.M12 + matrix.M42 * other.M22 + matrix.M43 * other.M32 + matrix.M44 * other.M42;
			result.M43 = matrix.M41 * other.M13 + matrix.M42 * other.M23 + matrix.M43 * other.M33 + matrix.M44 * other.M43;
			result.M44 = matrix.M41 * other.M14 + matrix.M42 * other.M24 + matrix.M43 * other.M34 + matrix.M44 * other.M44;
			return result;
		}

		/// <summary>Divides a <see cref="Matrix"/> by another <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the operation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Multiply( ref Matrix matrix, float value, out Matrix result )
		{
			result.M11 = matrix.M11 * value;
			result.M12 = matrix.M12 * value;
			result.M13 = matrix.M13 * value;
			result.M14 = matrix.M14 * value;

			result.M21 = matrix.M21 * value;
			result.M22 = matrix.M22 * value;
			result.M23 = matrix.M23 * value;
			result.M24 = matrix.M24 * value;

			result.M31 = matrix.M31 * value;
			result.M32 = matrix.M32 * value;
			result.M33 = matrix.M33 * value;
			result.M34 = matrix.M34 * value;

			result.M41 = matrix.M41 * value;
			result.M42 = matrix.M42 * value;
			result.M43 = matrix.M43 * value;
			result.M44 = matrix.M44 * value;
		}

		/// <summary>Divides a <see cref="Matrix"/> by another <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns a <see cref="Matrix"/> initialized with the difference between the specified matrices.</returns>
		public static Matrix Multiply( Matrix matrix, float value )
		{
			matrix.M11 *= value;
			matrix.M12 *= value;
			matrix.M13 *= value;
			matrix.M14 *= value;

			matrix.M21 *= value;
			matrix.M22 *= value;
			matrix.M23 *= value;
			matrix.M24 *= value;

			matrix.M31 *= value;
			matrix.M32 *= value;
			matrix.M33 *= value;
			matrix.M34 *= value;

			matrix.M41 *= value;
			matrix.M42 *= value;
			matrix.M43 *= value;
			matrix.M44 *= value;

			return matrix;
		}

	
		/// <summary>Divides a <see cref="Matrix"/> by another <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="other">A <see cref="Matrix"/> structure.</param>
		/// <param name="result">Receives the result of the operation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( ref Matrix matrix, ref Matrix other, out Matrix result )
		{
			result.M11 = matrix.M11 / other.M11;
			result.M12 = matrix.M12 / other.M12;
			result.M13 = matrix.M13 / other.M13;
			result.M14 = matrix.M14 / other.M14;

			result.M21 = matrix.M21 / other.M21;
			result.M22 = matrix.M22 / other.M22;
			result.M23 = matrix.M23 / other.M23;
			result.M24 = matrix.M24 / other.M24;

			result.M31 = matrix.M31 / other.M31;
			result.M32 = matrix.M32 / other.M32;
			result.M33 = matrix.M33 / other.M33;
			result.M34 = matrix.M34 / other.M34;

			result.M41 = matrix.M41 / other.M41;
			result.M42 = matrix.M42 / other.M42;
			result.M43 = matrix.M43 / other.M43;
			result.M44 = matrix.M44 / other.M44;
		}

		/// <summary>Divides a <see cref="Matrix"/> by another <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="other">A <see cref="Matrix"/> structure.</param>
		/// <returns>Returns a <see cref="Matrix"/> initialized with the difference between the specified matrices.</returns>
		public static Matrix Divide( Matrix matrix, Matrix other )
		{
			matrix.M11 /= other.M11;
			matrix.M12 /= other.M12;
			matrix.M13 /= other.M13;
			matrix.M14 /= other.M14;

			matrix.M21 /= other.M21;
			matrix.M22 /= other.M22;
			matrix.M23 /= other.M23;
			matrix.M24 /= other.M24;

			matrix.M31 /= other.M31;
			matrix.M32 /= other.M32;
			matrix.M33 /= other.M33;
			matrix.M34 /= other.M34;

			matrix.M41 /= other.M41;
			matrix.M42 /= other.M42;
			matrix.M43 /= other.M43;
			matrix.M44 /= other.M44;

			return matrix;
		}

		/// <summary>Divides a <see cref="Matrix"/> by another <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="result">Receives the result of the operation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Divide( ref Matrix matrix, float value, out Matrix result )
		{
			var inv = 1.0f / value;
			
			result.M11 = matrix.M11 * inv;
			result.M12 = matrix.M12 * inv;
			result.M13 = matrix.M13 * inv;
			result.M14 = matrix.M14 * inv;

			result.M21 = matrix.M21 * inv;
			result.M22 = matrix.M22 * inv;
			result.M23 = matrix.M23 * inv;
			result.M24 = matrix.M24 * inv;

			result.M31 = matrix.M31 * inv;
			result.M32 = matrix.M32 * inv;
			result.M33 = matrix.M33 * inv;
			result.M34 = matrix.M34 * inv;

			result.M41 = matrix.M41 * inv;
			result.M42 = matrix.M42 * inv;
			result.M43 = matrix.M43 * inv;
			result.M44 = matrix.M44 * inv;
		}

		/// <summary>Divides a <see cref="Matrix"/> by another <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns a <see cref="Matrix"/> initialized with the difference between the specified matrices.</returns>
		public static Matrix Divide( Matrix matrix, float value )
		{
			var inv = 1.0f / value;

			matrix.M11 *= inv;
			matrix.M12 *= inv;
			matrix.M13 *= inv;
			matrix.M14 *= inv;

			matrix.M21 *= inv;
			matrix.M22 *= inv;
			matrix.M23 *= inv;
			matrix.M24 *= inv;

			matrix.M31 *= inv;
			matrix.M32 *= inv;
			matrix.M33 *= inv;
			matrix.M34 *= inv;

			matrix.M41 *= inv;
			matrix.M42 *= inv;
			matrix.M43 *= inv;
			matrix.M44 *= inv;

			return matrix;
		}


		/// <summary>Negates a <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="result">Receives the negated <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Negate( ref Matrix matrix, out Matrix result )
		{
			result.M11 = -matrix.M11;
			result.M12 = -matrix.M12;
			result.M13 = -matrix.M13;
			result.M14 = -matrix.M14;
			result.M21 = -matrix.M21;
			result.M22 = -matrix.M22;
			result.M23 = -matrix.M23;
			result.M24 = -matrix.M24;
			result.M31 = -matrix.M31;
			result.M32 = -matrix.M32;
			result.M33 = -matrix.M33;
			result.M34 = -matrix.M34;
			result.M41 = -matrix.M41;
			result.M42 = -matrix.M42;
			result.M43 = -matrix.M43;
			result.M44 = -matrix.M44;
		}

		/// <summary>Negates a <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <returns>Returns the negated <paramref name="matrix"/>.</returns>
		public static Matrix Negate( Matrix matrix )
		{
			matrix.M11 = -matrix.M11;
			matrix.M12 = -matrix.M12;
			matrix.M13 = -matrix.M13;
			matrix.M14 = -matrix.M14;
			matrix.M21 = -matrix.M21;
			matrix.M22 = -matrix.M22;
			matrix.M23 = -matrix.M23;
			matrix.M24 = -matrix.M24;
			matrix.M31 = -matrix.M31;
			matrix.M32 = -matrix.M32;
			matrix.M33 = -matrix.M33;
			matrix.M34 = -matrix.M34;
			matrix.M41 = -matrix.M41;
			matrix.M42 = -matrix.M42;
			matrix.M43 = -matrix.M43;
			matrix.M44 = -matrix.M44; 
			return matrix;
		}


		/// <summary>Transposes the rows and columns of a <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <param name="result">Receives the transposed <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Transpose( ref Matrix matrix, out Matrix result )
		{
			result.M11 = matrix.M11;
			result.M12 = matrix.M21;
			result.M13 = matrix.M31;
			result.M14 = matrix.M41;

			result.M21 = matrix.M12;
			result.M22 = matrix.M22;
			result.M23 = matrix.M32;
			result.M24 = matrix.M42;

			result.M31 = matrix.M13;
			result.M32 = matrix.M23;
			result.M33 = matrix.M33;
			result.M34 = matrix.M43;

			result.M41 = matrix.M14;
			result.M42 = matrix.M24;
			result.M43 = matrix.M34;
			result.M44 = matrix.M44;
		}

		/// <summary>Transposes the rows and columns of a <see cref="Matrix"/>.</summary>
		/// <param name="matrix">A <see cref="Matrix"/> structure.</param>
		/// <returns>Returns the transposed matrix.</returns>
		public static Matrix Transpose( Matrix matrix )
		{
			Matrix result;
			result.M11 = matrix.M11;
			result.M12 = matrix.M21;
			result.M13 = matrix.M31;
			result.M14 = matrix.M41;

			result.M21 = matrix.M12;
			result.M22 = matrix.M22;
			result.M23 = matrix.M32;
			result.M24 = matrix.M42;

			result.M31 = matrix.M13;
			result.M32 = matrix.M23;
			result.M33 = matrix.M33;
			result.M34 = matrix.M43;

			result.M41 = matrix.M14;
			result.M42 = matrix.M24;
			result.M43 = matrix.M34;
			result.M44 = matrix.M44;
			return result;
		}


		/// <summary>Performs a linear interpolation between two matrices.</summary>
		/// <param name="source">The source <see cref="Matrix"/>.</param>
		/// <param name="target">The target <see cref="Matrix"/>.</param>
		/// <param name="amount">The weighting factor, within the range [0,1].</param>
		/// <param name="result">Receives the interpolated <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Lerp( ref Matrix source, ref Matrix target, float amount, out Matrix result )
		{
			result.M11 = source.M11 + ( target.M11 - source.M11 ) * amount;
			result.M12 = source.M12 + ( target.M12 - source.M12 ) * amount;
			result.M13 = source.M13 + ( target.M13 - source.M13 ) * amount;
			result.M14 = source.M14 + ( target.M14 - source.M14 ) * amount;
			
			result.M21 = source.M21 + ( target.M21 - source.M21 ) * amount;
			result.M22 = source.M22 + ( target.M22 - source.M22 ) * amount;
			result.M23 = source.M23 + ( target.M23 - source.M23 ) * amount;
			result.M24 = source.M24 + ( target.M24 - source.M24 ) * amount;
			
			result.M31 = source.M31 + ( target.M31 - source.M31 ) * amount;
			result.M32 = source.M32 + ( target.M32 - source.M32 ) * amount;
			result.M33 = source.M33 + ( target.M33 - source.M33 ) * amount;
			result.M34 = source.M34 + ( target.M34 - source.M34 ) * amount;
			
			result.M41 = source.M41 + ( target.M41 - source.M41 ) * amount;
			result.M42 = source.M42 + ( target.M42 - source.M42 ) * amount;
			result.M43 = source.M43 + ( target.M43 - source.M43 ) * amount;
			result.M44 = source.M44 + ( target.M44 - source.M44 ) * amount;
		}

		/// <summary>Performs a linear interpolation between two matrices.</summary>
		/// <param name="source">The source <see cref="Matrix"/>.</param>
		/// <param name="target">The target <see cref="Matrix"/>.</param>
		/// <param name="amount">The weighting factor, within the range [0,1].</param>
		/// <returns>Returns the interpolated <see cref="Matrix"/>.</returns>
		public static Matrix Lerp( Matrix source, Matrix target, float amount )
		{
			source.M11 += ( target.M11 - source.M11 ) * amount;
			source.M12 += ( target.M12 - source.M12 ) * amount;
			source.M13 += ( target.M13 - source.M13 ) * amount;
			source.M14 += ( target.M14 - source.M14 ) * amount;
			source.M21 += ( target.M21 - source.M21 ) * amount;
			source.M22 += ( target.M22 - source.M22 ) * amount;
			source.M23 += ( target.M23 - source.M23 ) * amount;
			source.M24 += ( target.M24 - source.M24 ) * amount;
			source.M31 += ( target.M31 - source.M31 ) * amount;
			source.M32 += ( target.M32 - source.M32 ) * amount;
			source.M33 += ( target.M33 - source.M33 ) * amount;
			source.M34 += ( target.M34 - source.M34 ) * amount;
			source.M41 += ( target.M41 - source.M41 ) * amount;
			source.M42 += ( target.M42 - source.M42 ) * amount;
			source.M43 += ( target.M43 - source.M43 ) * amount;
			source.M44 += ( target.M44 - source.M44 ) * amount;
			return source;
		}


		/// <summary>Calculates the inverse of a <see cref="Matrix"/>.</summary>
		/// <param name="matrix">The source <see cref="Matrix"/>.</param>
		/// <param name="result">Receives the inverse of <paramref name="matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Invert( ref Matrix matrix, out Matrix result )
		{
			var a = matrix.M33 * matrix.M44 - matrix.M34 * matrix.M43;
			var b = matrix.M32 * matrix.M44 - matrix.M34 * matrix.M42;
			var c = matrix.M32 * matrix.M43 - matrix.M33 * matrix.M42;
			var d = matrix.M31 * matrix.M44 - matrix.M34 * matrix.M41;
			var e = matrix.M31 * matrix.M43 - matrix.M33 * matrix.M41;
			var f = matrix.M31 * matrix.M42 - matrix.M32 * matrix.M41;
			
			var g = matrix.M22 * a - matrix.M23 * b + matrix.M24 * c;
			var h = -( matrix.M21 * a - matrix.M23 * d + matrix.M24 * e );
			var i = matrix.M21 * b - matrix.M22 * d + matrix.M24 * f;
			var j = -( matrix.M21 * c - matrix.M22 * e + matrix.M23 * f );
			
			var k = 1.0f / ( matrix.M11 * g + matrix.M12 * h + matrix.M13 * i + matrix.M14 * j );
			
			result.M11 = g * k;
			result.M21 = h * k;
			result.M31 = i * k;
			result.M41 = j * k;
			result.M12 = -( matrix.M12 * a - matrix.M13 * b + matrix.M14 * c ) * k;
			result.M22 = ( matrix.M11 * a - matrix.M13 * d + matrix.M14 * e ) * k;
			result.M32 = -( matrix.M11 * b - matrix.M12 * d + matrix.M14 * f ) * k;
			result.M42 = ( matrix.M11 * c - matrix.M12 * e + matrix.M13 * f ) * k;

			a = matrix.M23 * matrix.M44 - matrix.M24 * matrix.M43;
			b = matrix.M22 * matrix.M44 - matrix.M24 * matrix.M42;
			c = matrix.M22 * matrix.M43 - matrix.M23 * matrix.M42;
			d = matrix.M21 * matrix.M44 - matrix.M24 * matrix.M41;
			e = matrix.M21 * matrix.M43 - matrix.M23 * matrix.M41;
			f = matrix.M21 * matrix.M42 - matrix.M22 * matrix.M41;
			result.M13 = ( matrix.M12 * a - matrix.M13 * b + matrix.M14 * c ) * k;
			result.M23 = -( matrix.M11 * a - matrix.M13 * d + matrix.M14 * e ) * k;
			result.M33 = ( matrix.M11 * b - matrix.M12 * d + matrix.M14 * f ) * k;
			result.M43 = -( matrix.M11 * c - matrix.M12 * e + matrix.M13 * f ) * k;

			a = matrix.M23 * matrix.M34 - matrix.M24 * matrix.M33;
			b = matrix.M22 * matrix.M34 - matrix.M24 * matrix.M32;
			c = matrix.M22 * matrix.M33 - matrix.M23 * matrix.M32;
			d = matrix.M21 * matrix.M34 - matrix.M24 * matrix.M31;
			e = matrix.M21 * matrix.M33 - matrix.M23 * matrix.M31;
			f = matrix.M21 * matrix.M32 - matrix.M22 * matrix.M31;
			result.M14 = -( matrix.M12 * a - matrix.M13 * b + matrix.M14 * c ) * k;
			result.M24 = ( matrix.M11 * a - matrix.M13 * d + matrix.M14 * e ) * k;
			result.M34 = -( matrix.M11 * b - matrix.M12 * d + matrix.M14 * f ) * k;
			result.M44 = ( matrix.M11 * c - matrix.M12 * e + matrix.M13 * f ) * k;
		}

		/// <summary>Returns the inverse of a <see cref="Matrix"/>.</summary>
		/// <param name="matrix">The source <see cref="Matrix"/>.</param>
		/// <returns>Returns the inverse of <paramref name="matrix"/>.</returns>
		public static Matrix Invert( Matrix matrix )
		{
			Matrix result;

			var a = matrix.M33 * matrix.M44 - matrix.M34 * matrix.M43;
			var b = matrix.M32 * matrix.M44 - matrix.M34 * matrix.M42;
			var c = matrix.M32 * matrix.M43 - matrix.M33 * matrix.M42;
			var d = matrix.M31 * matrix.M44 - matrix.M34 * matrix.M41;
			var e = matrix.M31 * matrix.M43 - matrix.M33 * matrix.M41;
			var f = matrix.M31 * matrix.M42 - matrix.M32 * matrix.M41;

			var g = matrix.M22 * a - matrix.M23 * b + matrix.M24 * c;
			var h = -( matrix.M21 * a - matrix.M23 * d + matrix.M24 * e );
			var i = matrix.M21 * b - matrix.M22 * d + matrix.M24 * f;
			var j = -( matrix.M21 * c - matrix.M22 * e + matrix.M23 * f );

			var k = 1.0f / ( matrix.M11 * g + matrix.M12 * h + matrix.M13 * i + matrix.M14 * j );

			result.M11 = g * k;
			result.M21 = h * k;
			result.M31 = i * k;
			result.M41 = j * k;
			
			result.M12 = -( matrix.M12 * a - matrix.M13 * b + matrix.M14 * c ) * k;
			result.M22 = ( matrix.M11 * a - matrix.M13 * d + matrix.M14 * e ) * k;
			result.M32 = -( matrix.M11 * b - matrix.M12 * d + matrix.M14 * f ) * k;
			result.M42 = ( matrix.M11 * c - matrix.M12 * e + matrix.M13 * f ) * k;

			a = matrix.M23 * matrix.M44 - matrix.M24 * matrix.M43;
			b = matrix.M22 * matrix.M44 - matrix.M24 * matrix.M42;
			c = matrix.M22 * matrix.M43 - matrix.M23 * matrix.M42;
			d = matrix.M21 * matrix.M44 - matrix.M24 * matrix.M41;
			e = matrix.M21 * matrix.M43 - matrix.M23 * matrix.M41;
			f = matrix.M21 * matrix.M42 - matrix.M22 * matrix.M41;
			result.M13 = ( matrix.M12 * a - matrix.M13 * b + matrix.M14 * c ) * k;
			result.M23 = -( matrix.M11 * a - matrix.M13 * d + matrix.M14 * e ) * k;
			result.M33 = ( matrix.M11 * b - matrix.M12 * d + matrix.M14 * f ) * k;
			result.M43 = -( matrix.M11 * c - matrix.M12 * e + matrix.M13 * f ) * k;

			a = matrix.M23 * matrix.M34 - matrix.M24 * matrix.M33;
			b = matrix.M22 * matrix.M34 - matrix.M24 * matrix.M32;
			c = matrix.M22 * matrix.M33 - matrix.M23 * matrix.M32;
			d = matrix.M21 * matrix.M34 - matrix.M24 * matrix.M31;
			e = matrix.M21 * matrix.M33 - matrix.M23 * matrix.M31;
			f = matrix.M21 * matrix.M32 - matrix.M22 * matrix.M31;
			result.M14 = -( matrix.M12 * a - matrix.M13 * b + matrix.M14 * c ) * k;
			result.M24 = ( matrix.M11 * a - matrix.M13 * d + matrix.M14 * e ) * k;
			result.M34 = -( matrix.M11 * b - matrix.M12 * d + matrix.M14 * f ) * k;
			result.M44 = ( matrix.M11 * c - matrix.M12 * e + matrix.M13 * f ) * k;

			return result;
		}


		/// <summary>Creates a scaling <see cref="Matrix"/>.</summary>
		/// <param name="scale">Amounts to scale by on the x, y, and z axes.</param>
		/// <param name="result">Receives the scaling <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateScale( ref Vector3 scale, out Matrix result )
		{
			result.M11 = scale.X;
			result.M12 = 0.0f;
			result.M13 = 0.0f;
			result.M14 = 0.0f;

			result.M21 = 0.0f;
			result.M22 = scale.Y;
			result.M23 = 0.0f;
			result.M24 = 0.0f;

			result.M31 = 0.0f;
			result.M32 = 0.0f;
			result.M33 = scale.Z;
			result.M34 = 0.0f;

			result.M41 = 0.0f;
			result.M42 = 0.0f;
			result.M43 = 0.0f;
			result.M44 = 1.0f;
		}

		/// <summary>Returns a scaling <see cref="Matrix"/>.</summary>
		/// <param name="scale">Amounts to scale by on the x, y, and z axes.</param>
		/// <returns>Returns the created scaling <see cref="Matrix"/>.</returns>
		public static Matrix CreateScale( Vector3 scale )
		{
			Matrix result;
			
			result.M11 = scale.X;
			result.M12 = 0.0f;
			result.M13 = 0.0f;
			result.M14 = 0.0f;
			
			result.M21 = 0.0f;
			result.M22 = scale.Y;
			result.M23 = 0.0f;
			result.M24 = 0.0f;

			result.M31 = 0.0f;
			result.M32 = 0.0f;
			result.M33 = scale.Z;
			result.M34 = 0.0f;

			result.M41 = 0.0f;
			result.M42 = 0.0f;
			result.M43 = 0.0f;
			result.M44 = 1.0f;
			
			return result;
		}


		/// <summary>Creates a translation <see cref="Matrix"/>.</summary>
		/// <param name="translation">Amounts to translate by on the x, y, and z axes.</param>
		/// <param name="result">Receives the translation <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateTranslation( ref Vector3 translation, out Matrix result )
		{
			result.M11 = 1.0f;
			result.M12 = 0.0f;
			result.M13 = 0.0f;
			result.M14 = 0.0f;

			result.M21 = 0.0f;
			result.M22 = 1.0f;
			result.M23 = 0.0f;
			result.M24 = 0.0f;

			result.M31 = 0.0f;
			result.M32 = 0.0f;
			result.M33 = 1.0f;
			result.M34 = 0.0f;

			result.M41 = translation.X;
			result.M42 = translation.Y;
			result.M43 = translation.Z;
			result.M44 = 1.0f;
		}

		/// <summary>Returns a translation <see cref="Matrix"/>.</summary>
		/// <param name="translation">Amounts to translate by on the x, y, and z axes.</param>
		/// <returns>Returns the translation <see cref="Matrix"/>.</returns>
		public static Matrix CreateTranslation( Vector3 translation )
		{
			Matrix result;
			
			result.M11 = 1.0f;
			result.M12 = result.M13 = result.M14 = 0.0f;

			result.M21 = 0.0f;
			result.M22 = 1.0f;
			result.M23 = result.M24 = 0.0f;

			result.M31 = result.M32 = 0.0f;
			result.M33 = 1.0f;
			result.M34 = 0.0f;
			
			result.M41 = translation.X;
			result.M42 = translation.Y;
			result.M43 = translation.Z;
			result.M44 = 1.0f;

			return result;
		}


		/// <summary>Creates a <see cref="Matrix"/> which can be used to rotate a set of vertices around the x-axis.</summary>
		/// <param name="angle">The angle, in radians, to rotate around the x-axis.</param>
		/// <param name="result">Receives the rotation <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateRotationX( float angle, out Matrix result )
		{
			var cosAngle = (float)Math.Cos( (double)angle );
			var sinAngle = (float)Math.Sin( (double)angle );

			result.M11 = 1.0f;
			result.M12 = 0.0f;
			result.M13 = 0.0f;
			result.M14 = 0.0f;

			result.M21 = 0.0f;
			result.M22 = cosAngle;
			result.M23 = sinAngle;
			result.M24 = 0.0f;

			result.M31 = 0.0f;
			result.M32 = -sinAngle;
			result.M33 = cosAngle;
			result.M34 = 0.0f;

			result.M41 = 0.0f;
			result.M42 = 0.0f;
			result.M43 = 0.0f;
			result.M44 = 1.0f;
		}

		/// <summary>Returns a <see cref="Matrix"/> that can be used to rotate a set of vertices around the x-axis.</summary>
		/// <param name="angle">The angle, in radians, to rotate around the x-axis.</param>
		/// <returns>Returns the rotation <see cref="Matrix"/>.</returns>
		public static Matrix CreateRotationX( float angle )
		{
			var cosAngle = (float)Math.Cos( (double)angle );
			var sinAngle = (float)Math.Sin( (double)angle );
			
			Matrix result;

			result.M11 = 1.0f;
			result.M12 = 0.0f;
			result.M13 = 0.0f;
			result.M14 = 0.0f;

			result.M21 = 0.0f;
			result.M22 = cosAngle;
			result.M23 = sinAngle;
			result.M24 = 0.0f;

			result.M31 = 0.0f;
			result.M32 = -sinAngle;
			result.M33 = cosAngle;
			result.M34 = 0.0f;

			result.M41 = 0.0f;
			result.M42 = 0.0f;
			result.M43 = 0.0f;
			result.M44 = 1.0f;
			
			return result;
		}


		/// <summary>Creates a <see cref="Matrix"/> which can be used to rotate a set of vertices around the y-axis.</summary>
		/// <param name="angle">The angle, in radians, to rotate around the y-axis.</param>
		/// <param name="result">Receives the rotation <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateRotationY( float angle, out Matrix result )
		{
			var cosAngle = (float)Math.Cos( (double)angle );
			var sinAngle = (float)Math.Sin( (double)angle );

			result.M11 = cosAngle;
			result.M12 = 0.0f;
			result.M13 = -sinAngle;
			result.M14 = 0.0f;

			result.M21 = 0.0f;
			result.M22 = 1.0f;
			result.M23 = 0.0f;
			result.M24 = 0.0f;

			result.M31 = sinAngle;
			result.M32 = 0.0f;
			result.M33 = cosAngle;
			result.M34 = 0.0f;

			result.M41 = 0.0f;
			result.M42 = 0.0f;
			result.M43 = 0.0f;
			result.M44 = 1.0f;
		}

		/// <summary>Returns a <see cref="Matrix"/> that can be used to rotate a set of vertices around the y-axis.</summary>
		/// <param name="angle">The angle, in radians, to rotate around the y-axis.</param>
		/// <returns>Returns the rotation <see cref="Matrix"/>.</returns>
		public static Matrix CreateRotationY( float angle )
		{
			var cosAngle = (float)Math.Cos( (double)angle );
			var sinAngle = (float)Math.Sin( (double)angle );
			
			Matrix result;

			result.M11 = cosAngle;
			result.M12 = 0.0f;
			result.M13 = -sinAngle;
			result.M14 = 0.0f;

			result.M21 = 0.0f;
			result.M22 = 1.0f;
			result.M23 = 0.0f;
			result.M24 = 0.0f;

			result.M31 = sinAngle;
			result.M32 = 0.0f;
			result.M33 = cosAngle;
			result.M34 = 0.0f;

			result.M41 = 0.0f;
			result.M42 = 0.0f;
			result.M43 = 0.0f;
			result.M44 = 1.0f;
			
			return result;
		}


		/// <summary>Creates a <see cref="Matrix"/> which can be used to rotate a set of vertices around the z-axis.</summary>
		/// <param name="angle">The angle, in radians, to rotate around the z-axis.</param>
		/// <param name="result">Receives the rotation <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateRotationZ( float angle, out Matrix result )
		{
			var cosAngle = (float)Math.Cos( (double)angle );
			var sinAngle = (float)Math.Sin( (double)angle );

			result.M11 = cosAngle;
			result.M12 = sinAngle;
			result.M13 = 0.0f;
			result.M14 = 0.0f;

			result.M21 = -sinAngle;
			result.M22 = cosAngle;
			result.M23 = 0.0f;
			result.M24 = 0.0f;

			result.M31 = 0.0f;
			result.M32 = 0.0f;
			result.M33 = 1.0f;
			result.M34 = 0.0f;

			result.M41 = 0.0f;
			result.M42 = 0.0f;
			result.M43 = 0.0f;
			result.M44 = 1.0f;
		}

		/// <summary>Returns a <see cref="Matrix"/> that can be used to rotate a set of vertices around the z-axis.</summary>
		/// <param name="angle">The angle, in radians, to rotate around the z-axis.</param>
		/// <returns>Returns the rotation <see cref="Matrix"/>.</returns>
		public static Matrix CreateRotationZ( float angle )
		{
			var cosAngle = (float)Math.Cos( (double)angle );
			var sinAngle = (float)Math.Sin( (double)angle );
			
			Matrix result;
			
			result.M11 = cosAngle;
			result.M12 = sinAngle;
			result.M13 = 0.0f;
			result.M14 = 0.0f;
			
			result.M21 = -sinAngle;
			result.M22 = cosAngle;
			result.M23 = 0.0f;
			result.M24 = 0.0f;

			result.M31 = 0.0f;
			result.M32 = 0.0f;
			result.M33 = 1.0f;
			result.M34 = 0.0f;

			result.M41 = 0.0f;
			result.M42 = 0.0f;
			result.M43 = 0.0f;
			result.M44 = 1.0f;
			
			return result;
		}


		/// <summary>Creates a new <see cref="Matrix"/> that rotates around an arbitrary axis.</summary>
		/// <param name="axis">A <see cref="Vector3"/> structure representing the axis to rotate around.</param>
		/// <param name="angle">The angle, in radians, to rotate around the <paramref name="axis"/>.</param>
		/// <param name="result">Receives the rotation <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateFromAxisAngle( ref Vector3 axis, float angle, out Matrix result )
		{
			var xSquared = axis.X * axis.X;
			var ySquared = axis.Y * axis.Y;
			var zSquared = axis.Z * axis.Z;
			var xy = axis.X * axis.Y;
			var xz = axis.X * axis.Z;
			var yz = axis.Y * axis.Z;

			var sinAngle = (float)Math.Sin( (double)angle );
			var cosAngle = (float)Math.Cos( (double)angle );

			result.M11 = xSquared + cosAngle * ( 1.0f - xSquared );
			result.M12 = xy - cosAngle * xy + sinAngle * axis.Z;
			result.M13 = xz - cosAngle * xz - sinAngle * axis.Y;
			result.M14 = 0.0f;

			result.M21 = xy - cosAngle * xy - sinAngle * axis.Z;
			result.M22 = ySquared + cosAngle * ( 1.0f - ySquared );
			result.M23 = yz - cosAngle * yz + sinAngle * axis.X;
			result.M24 = 0.0f;

			result.M31 = xz - cosAngle * xz + sinAngle * axis.Y;
			result.M32 = yz - cosAngle * yz - sinAngle * axis.X;
			result.M33 = zSquared + cosAngle * ( 1.0f - zSquared );
			result.M34 = 0.0f;

			result.M41 = 0.0f;
			result.M42 = 0.0f;
			result.M43 = 0.0f;
			result.M44 = 1.0f;
		}

		/// <summary>Returns a <see cref="Matrix"/> which rotates around an arbitrary axis.</summary>
		/// <param name="axis">A <see cref="Vector3"/> structure representing the axis to rotate around.</param>
		/// <param name="angle">The angle, in radians, to rotate around the <paramref name="axis"/>.</param>
		/// <returns>Returns the rotation <see cref="Matrix"/>.</returns>
		public static Matrix CreateFromAxisAngle( Vector3 axis, float angle )
		{
			var xSquared = axis.X * axis.X;
			var ySquared = axis.Y * axis.Y;
			var zSquared = axis.Z * axis.Z;
			var xy = axis.X * axis.Y;
			var xz = axis.X * axis.Z;
			var yz = axis.Y * axis.Z;
			
			var sinAngle = (float)Math.Sin( (double)angle );
			var cosAngle = (float)Math.Cos( (double)angle );
			
			Matrix result;
			
			result.M11 = xSquared + cosAngle * ( 1.0f - xSquared );
			result.M12 = xy - cosAngle * xy + sinAngle * axis.Z;
			result.M13 = xz - cosAngle * xz - sinAngle * axis.Y;
			result.M14 = 0.0f;
			
			result.M21 = xy - cosAngle * xy - sinAngle * axis.Z;
			result.M22 = ySquared + cosAngle * ( 1.0f - ySquared );
			result.M23 = yz - cosAngle * yz + sinAngle * axis.X;
			result.M24 = 0.0f;
			
			result.M31 = xz - cosAngle * xz + sinAngle * axis.Y;
			result.M32 = yz - cosAngle * yz - sinAngle * axis.X;
			result.M33 = zSquared + cosAngle * ( 1.0f - zSquared );
			result.M34 = 0.0f;
			
			result.M41 = 0.0f;
			result.M42 = 0.0f;
			result.M43 = 0.0f;
			result.M44 = 1.0f;
			
			return result;
		}


		/// <summary>Creates a rotation <see cref="Matrix"/> from a <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives the rotation <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateFromQuaternion( ref Quaternion quaternion, out Matrix result )
		{
			var xSquared = quaternion.X * quaternion.X;
			var ySquared = quaternion.Y * quaternion.Y;
			var zSquared = quaternion.Z * quaternion.Z;
			var xy = quaternion.X * quaternion.Y;
			var zw = quaternion.Z * quaternion.W;
			var zx = quaternion.Z * quaternion.X;
			var yw = quaternion.Y * quaternion.W;
			var yz = quaternion.Y * quaternion.Z;
			var xw = quaternion.X * quaternion.W;
			
			result.M11 = 1.0f - 2.0f * ( ySquared + zSquared );
			result.M12 = 2.0f * ( xy + zw );
			result.M13 = 2.0f * ( zx - yw );
			result.M14 = 0.0f;
			
			result.M21 = 2.0f * ( xy - zw );
			result.M22 = 1.0f - 2.0f * ( zSquared + xSquared );
			result.M23 = 2.0f * ( yz + xw );
			result.M24 = 0.0f;
			
			result.M31 = 2.0f * ( zx + yw );
			result.M32 = 2.0f * ( yz - xw );
			result.M33 = 1.0f - 2.0f * ( ySquared + xSquared );
			result.M34 = 0.0f;

			result.M41 = result.M42 = result.M43 = 0.0f;
			result.M44 = 1.0f;
		}

		/// <summary>Returns a rotation <see cref="Matrix"/> from a <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns the rotation <see cref="Matrix"/>.</returns>
		public static Matrix CreateFromQuaternion( Quaternion quaternion )
		{
			var xSquared = quaternion.X * quaternion.X;
			var ySquared = quaternion.Y * quaternion.Y;
			var zSquared = quaternion.Z * quaternion.Z;
			var xy = quaternion.X * quaternion.Y;
			var zw = quaternion.Z * quaternion.W;
			var zx = quaternion.Z * quaternion.X;
			var yw = quaternion.Y * quaternion.W;
			var yz = quaternion.Y * quaternion.Z;
			var xw = quaternion.X * quaternion.W;

			Matrix result;
			result.M11 = 1.0f - 2.0f * ( ySquared + zSquared );
			result.M12 = 2.0f * ( xy + zw );
			result.M13 = 2.0f * ( zx - yw );
			result.M14 = 0.0f;

			result.M21 = 2.0f * ( xy - zw );
			result.M22 = 1.0f - 2.0f * ( zSquared + xSquared );
			result.M23 = 2.0f * ( yz + xw );
			result.M24 = 0.0f;

			result.M31 = 2.0f * ( zx + yw );
			result.M32 = 2.0f * ( yz - xw );
			result.M33 = 1.0f - 2.0f * ( ySquared + xSquared );
			result.M34 = 0.0f;

			result.M41 = result.M42 = result.M43 = 0.0f;
			result.M44 = 1.0f;
			return result;
		}


		/// <summary>Creates a rotation <see cref="Matrix"/> from yaw, pitch and roll angles.</summary>
		/// <param name="yaw">The yaw angle, in radians, around the y-axis.</param>
		/// <param name="pitch">The pitch angle, in radians, around the x-axis.</param>
		/// <param name="roll">The roll angle, in radians, around the z-axis.</param>
		/// <param name="result">Receives the rotation <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateFromYawPitchRoll( float yaw, float pitch, float roll, out Matrix result )
		{
			Quaternion.CreateFromYawPitchRoll( yaw, pitch, roll, out Quaternion rotation );
			CreateFromQuaternion( ref rotation, out result );
		}

		/// <summary>Returns a rotation <see cref="Matrix"/> from yaw, pitch and roll angles.</summary>
		/// <param name="yaw">The yaw angle, in radians, around the y-axis.</param>
		/// <param name="pitch">The pitch angle, in radians, around the x-axis.</param>
		/// <param name="roll">The roll angle, in radians, around the z-axis.</param>
		/// <returns>Returns the rotation <see cref="Matrix"/>.</returns>
		public static Matrix CreateFromYawPitchRoll( float yaw, float pitch, float roll )
		{
			Quaternion.CreateFromYawPitchRoll( yaw, pitch, roll, out Quaternion rotation );

			CreateFromQuaternion( ref rotation, out Matrix result );
			return result;
		}


		/// <summary>Creates a world <see cref="Matrix"/> with the specified parameters.</summary>
		/// <param name="position">The position of the object; this value is used in translation operations.</param>
		/// <param name="forward">The forward direction of the object.</param>
		/// <param name="up">Upward direction of the object; usually [0, 1, 0] (<see cref="Vector3.Up"/>).</param>
		/// <param name="result">Receives the world <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateWorld( ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix result )
		{
			var front = -forward;
			front.Normalize();

			Vector3.Cross( ref up, ref front, out Vector3 right );
			right.Normalize();

			Vector3.Cross( ref front, ref right, out Vector3 top );
			// up doesn't need to be normalized, since forward and right have already been normalized

			result.M11 = right.X;
			result.M12 = right.Y;
			result.M13 = right.Z;
			result.M14 = 0.0f;

			result.M21 = top.X;
			result.M22 = top.Y;
			result.M23 = top.Z;
			result.M24 = 0.0f;

			result.M31 = front.X;
			result.M32 = front.Y;
			result.M33 = front.Z;
			result.M34 = 0.0f;

			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1.0f;
		}

		/// <summary>Returns a world <see cref="Matrix"/> with the specified parameters.</summary>
		/// <param name="position">The position of the object; this value is used in translation operations.</param>
		/// <param name="forward">The forward direction of the object.</param>
		/// <param name="up">Upward direction of the object; usually [0, 1, 0] (<see cref="Vector3.Up"/>).</param>
		/// <returns>Returns the world <see cref="Matrix"/>.</returns>
		public static Matrix CreateWorld( Vector3 position, Vector3 forward, Vector3 up )
		{
			forward.Negate();
			forward.Normalize();

			Vector3.Cross( ref up, ref forward, out Vector3 right );
			right.Normalize();

			Vector3.Cross( ref forward, ref right, out up );
			// up doesn't need to be normalized, since forward and right have already been normalized

			Matrix result;

			result.M11 = right.X;
			result.M12 = right.Y;
			result.M13 = right.Z;
			result.M14 = 0.0f;

			result.M21 = up.X;
			result.M22 = up.Y;
			result.M23 = up.Z;
			result.M24 = 0.0f;

			result.M31 = forward.X;
			result.M32 = forward.Y;
			result.M33 = forward.Z;
			result.M34 = 0.0f;

			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1.0f;

			return result;
		}


		/// <summary>Creates a view <see cref="Matrix"/>.</summary>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="cameraTarget">The target the camera is pointing to.</param>
		/// <param name="cameraUpVector">The direction that is "up" from the camera's point of view.</param>
		/// <param name="result">Receives the created view <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateLookAt( ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix result )
		{
			Vector3 targetToCamera;

			targetToCamera = cameraPosition - cameraTarget;
			targetToCamera.Normalize();

			Vector3.Cross( ref cameraUpVector, ref targetToCamera, out Vector3 side );
			side.Normalize();

			Vector3.Cross( ref targetToCamera, ref side, out Vector3 up );

			result.M11 = side.X;
			result.M12 = up.X;
			result.M13 = targetToCamera.X;
			result.M14 = 0.0f;

			result.M21 = side.Y;
			result.M22 = up.Y;
			result.M23 = targetToCamera.Y;
			result.M24 = 0.0f;

			result.M31 = side.Z;
			result.M32 = up.Z;
			result.M33 = targetToCamera.Z;
			result.M34 = 0.0f;

			result.M41 = -Vector3.Dot( side, cameraPosition );
			result.M42 = -Vector3.Dot( up, cameraPosition );
			result.M43 = -Vector3.Dot( targetToCamera, cameraPosition );
			result.M44 = 1.0f;
		}

		/// <summary>Returns a view <see cref="Matrix"/>.</summary>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="cameraTarget">The target the camera is pointing to.</param>
		/// <param name="cameraUpVector">The direction that is "up" from the camera's point of view.</param>
		/// <returns>Returns the created view <see cref="Matrix"/>.</returns>
		public static Matrix CreateLookAt( Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector )
		{
			Vector3 targetToCamera;

			targetToCamera = cameraPosition - cameraTarget;
			targetToCamera.Normalize();
			
			Vector3.Cross( ref cameraUpVector, ref targetToCamera, out Vector3 side );
			side.Normalize();
			
			Vector3.Cross( ref targetToCamera, ref side, out Vector3 up );
			
			Matrix result;
			
			result.M11 = side.X;
			result.M12 = up.X;
			result.M13 = targetToCamera.X;
			result.M14 = 0.0f;
			
			result.M21 = side.Y;
			result.M22 = up.Y;
			result.M23 = targetToCamera.Y;
			result.M24 = 0.0f;
			
			result.M31 = side.Z;
			result.M32 = up.Z;
			result.M33 = targetToCamera.Z;
			result.M34 = 0.0f;
			
			result.M41 = -Vector3.Dot( side, cameraPosition );
			result.M42 = -Vector3.Dot( up, cameraPosition );
			result.M43 = -Vector3.Dot( targetToCamera, cameraPosition );
			result.M44 = 1.0f;
			
			return result;
		}


		/// <summary>Creates a <see cref="Matrix"/> which reflects the coordinate system about a specified <see cref="Plane"/>.</summary>
		/// <param name="plane">A <see cref="Plane"/>; will be normalized.</param>
		/// <param name="result">Receives the created reflection <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1062" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateReflection( ref Plane plane, out Matrix result )
		{
			plane.Normalize();

			var x = plane.Normal.X;
			var y = plane.Normal.Y;
			var z = plane.Normal.Z;

			var minus2x = -2.0f * x;
			var minus2y = -2.0f * y;
			var minus2z = -2.0f * z;

			result.M11 = minus2x * x + 1.0f;
			result.M12 = minus2y * x;
			result.M13 = minus2z * x;
			result.M14 = 0.0f;
			result.M21 = minus2x * y;
			result.M22 = minus2y * y + 1.0f;
			result.M23 = minus2z * y;
			result.M24 = 0.0f;
			result.M31 = minus2x * z;
			result.M32 = minus2y * z;
			result.M33 = minus2z * z + 1.0f;
			result.M34 = 0.0f;
			result.M41 = minus2x * plane.Distance;
			result.M42 = minus2y * plane.Distance;
			result.M43 = minus2z * plane.Distance;
			result.M44 = 1.0f;
		}

		/// <summary>Returns a <see cref="Matrix"/> which reflects the coordinate system about a specified <see cref="Plane"/>.</summary>
		/// <param name="plane">A <see cref="Plane"/>.</param>
		/// <returns>Returns the created reflection <see cref="Matrix"/>.</returns>
		public static Matrix CreateReflection( Plane plane )
		{
			plane.Normalize();

			var x = plane.Normal.X;
			var y = plane.Normal.Y;
			var z = plane.Normal.Z;

			var minus2x = -2.0f * x;
			var minus2y = -2.0f * y;
			var minus2z = -2.0f * z;

			Matrix result;
			result.M11 = minus2x * x + 1.0f;
			result.M12 = minus2y * x;
			result.M13 = minus2z * x;
			result.M14 = 0.0f;
			result.M21 = minus2x * y;
			result.M22 = minus2y * y + 1.0f;
			result.M23 = minus2z * y;
			result.M24 = 0.0f;
			result.M31 = minus2x * z;
			result.M32 = minus2y * z;
			result.M33 = minus2z * z + 1.0f;
			result.M34 = 0.0f;
			result.M41 = minus2x * plane.Distance;
			result.M42 = minus2y * plane.Distance;
			result.M43 = minus2z * plane.Distance;
			result.M44 = 1.0f;
			return result;
		}


		/// <summary>Creates a <see cref="Matrix"/> to flatten geometry into a specified <see cref="Plane"/> as if casting a shadow from a specified light source.</summary>
		/// <param name="lightSourceDirection">A <see cref="Vector3"/> specifying the direction from which the light that will cast the shadow is coming.</param>
		/// <param name="plane">The <see cref="Plane"/> onto which the new matrix should flatten geometry so as to cast a shadow.</param>
		/// <param name="result">Receives a <see cref="Matrix"/> which can be used to flatten geometry onto the specified plane from the specified direction.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateShadow( ref Vector3 lightSourceDirection, ref Plane plane, out Matrix result )
		{
			Plane.Normalize( ref plane, out Plane normalizedPlane );

			var NdotL = normalizedPlane.Normal.X * lightSourceDirection.X + normalizedPlane.Normal.Y * lightSourceDirection.Y + normalizedPlane.Normal.Z * lightSourceDirection.Z;
			var minusX = -normalizedPlane.Normal.X;
			var minusY = -normalizedPlane.Normal.Y;
			var minusZ = -normalizedPlane.Normal.Z;
			var minusDistance = -normalizedPlane.Distance;

			result.M11 = minusX * lightSourceDirection.X + NdotL;
			result.M12 = minusX * lightSourceDirection.Y;
			result.M13 = minusX * lightSourceDirection.Z;
			result.M14 = 0.0f;

			result.M21 = minusY * lightSourceDirection.X;
			result.M22 = minusY * lightSourceDirection.Y + NdotL;
			result.M23 = minusY * lightSourceDirection.Z;
			result.M24 = 0.0f;

			result.M31 = minusZ * lightSourceDirection.X;
			result.M32 = minusZ * lightSourceDirection.Y;
			result.M33 = minusZ * lightSourceDirection.Z + NdotL;
			result.M34 = 0.0f;

			result.M41 = minusDistance * lightSourceDirection.X;
			result.M42 = minusDistance * lightSourceDirection.Y;
			result.M43 = minusDistance * lightSourceDirection.Z;
			result.M44 = NdotL;
		}

		/// <summary>Returns a <see cref="Matrix"/> to flatten geometry into a specified <see cref="Plane"/> as if casting a shadow from a specified light source.</summary>
		/// <param name="lightSourceDirection">A <see cref="Vector3"/> specifying the direction from which the light that will cast the shadow is coming.</param>
		/// <param name="plane">The <see cref="Plane"/> onto which the new matrix should flatten geometry so as to cast a shadow.</param>
		/// <returns>Returns a <see cref="Matrix"/> which can be used to flatten geometry onto the specified plane from the specified direction.</returns>
		public static Matrix CreateShadow( Vector3 lightSourceDirection, Plane plane )
		{
			plane.Normalize();

			var NdotL = plane.Normal.X * lightSourceDirection.X + plane.Normal.Y * lightSourceDirection.Y + plane.Normal.Z * lightSourceDirection.Z;
			var minusX = -plane.Normal.X;
			var minusY = -plane.Normal.Y;
			var minusZ = -plane.Normal.Z;
			var minusDistance = -plane.Distance;

			Matrix result;
			result.M11 = minusX * lightSourceDirection.X + NdotL;
			result.M12 = minusX * lightSourceDirection.Y;
			result.M13 = minusX * lightSourceDirection.Z;
			result.M14 = 0.0f;

			result.M21 = minusY * lightSourceDirection.X;
			result.M22 = minusY * lightSourceDirection.Y + NdotL;
			result.M23 = minusY * lightSourceDirection.Z;
			result.M24 = 0.0f;

			result.M31 = minusZ * lightSourceDirection.X;
			result.M32 = minusZ * lightSourceDirection.Y;
			result.M33 = minusZ * lightSourceDirection.Z + NdotL;
			result.M34 = 0.0f;

			result.M41 = minusDistance * lightSourceDirection.X;
			result.M42 = minusDistance * lightSourceDirection.Y;
			result.M43 = minusDistance * lightSourceDirection.Z;
			result.M44 = NdotL;

			return result;
		}


        /// <summary>Creates an orthogonal projection <see cref="Matrix"/>.</summary>
        /// <param name="width">The width of the view volume.</param>
        /// <param name="height">The height of the view volume.</param>
        /// <param name="zNearPlane">The minimum z-value of the view volume.</param>
        /// <param name="zFarPlane">The maximum z-value of the view volume.</param>
        /// <param name="result">Receives the created orthogonal projection <see cref="Matrix"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z")]
        [SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateOrthographic( float width, float height, float zNearPlane, float zFarPlane, out Matrix result )
		{
			if( float.IsNaN( width ) || float.IsInfinity( width ) || width <= 0.0f )
				throw new ArgumentOutOfRangeException( "width" );

			if( float.IsNaN( height ) || float.IsInfinity( height ) || height <= 0.0f )
				throw new ArgumentOutOfRangeException( "height" );

			if( float.IsNaN( zNearPlane ) || float.IsInfinity( zNearPlane ) )
				throw new ArgumentOutOfRangeException( "zNearPlane" );

			if( float.IsNaN( zFarPlane ) || float.IsInfinity( zFarPlane ) || ( zFarPlane <= zNearPlane ) )
				throw new ArgumentOutOfRangeException( "zFarPlane" );

			result.M11 = 2.0f / width;
			result.M12 = result.M13 = result.M14 = 0.0f;

			result.M21 = result.M23 = result.M24 = 0.0f;
			result.M22 = 2.0f / height;

			result.M31 = result.M32 = result.M34 = 0.0f;
			result.M33 = 1.0f / ( zNearPlane - zFarPlane );

			result.M41 = result.M42 = 0.0f;
			result.M43 = zNearPlane / ( zNearPlane - zFarPlane );
			result.M44 = 1.0f;
		}

        /// <summary>Returns an orthogonal projection <see cref="Matrix"/>.</summary>
        /// <param name="width">The width of the view volume.</param>
        /// <param name="height">The height of the view volume.</param>
        /// <param name="zNearPlane">The minimum z-value of the view volume.</param>
        /// <param name="zFarPlane">The maximum z-value of the view volume.</param>
        /// <returns>Returns the created orthogonal projection <see cref="Matrix"/>.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z")]
        public static Matrix CreateOrthographic( float width, float height, float zNearPlane, float zFarPlane )
		{
			if( float.IsNaN( width ) || float.IsInfinity( width ) || width <= 0.0f )
				throw new ArgumentOutOfRangeException( "width" );

			if( float.IsNaN( height ) || float.IsInfinity( height ) || height <= 0.0f )
				throw new ArgumentOutOfRangeException( "height" );

			if( float.IsNaN( zNearPlane ) || float.IsInfinity( zNearPlane ) )
				throw new ArgumentOutOfRangeException( "zNearPlane" );

			if( float.IsNaN( zFarPlane ) || float.IsInfinity( zFarPlane ) || ( zFarPlane <= zNearPlane ) )
				throw new ArgumentOutOfRangeException( "zFarPlane" );

			Matrix result;
			
			result.M11 = 2.0f / width;
			result.M12 = result.M13 = result.M14 = 0.0f;
			
			result.M21 = result.M23 = result.M24 = 0.0f;
			result.M22 = 2.0f / height;
			
			result.M31 = result.M32 = result.M34 = 0.0f;
			result.M33 = 1.0f / ( zNearPlane - zFarPlane );
			
			result.M41 = result.M42 = 0.0f;
			result.M43 = zNearPlane / ( zNearPlane - zFarPlane );
			result.M44 = 1.0f;

			return result;
		}


        /// <summary>Creates a customized, orthogonal projection <see cref="Matrix"/>.</summary>
        /// <param name="left">Minimum x-value of the view volume.</param>
        /// <param name="right">Maximum x-value of the view volume.</param>
        /// <param name="bottom">Minimum y-value of the view volume.</param>
        /// <param name="top">Maximum y-value of the view volume.</param>
        /// <param name="zNearPlane">Minimum z-value of the view volume.</param>
        /// <param name="zFarPlane">Maximum z-value of the view volume.</param>
        /// <param name="result">Receives the created <see cref="Matrix"/>.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z")]
        [SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateOrthographicOffCenter( float left, float right, float bottom, float top, float zNearPlane, float zFarPlane, out Matrix result )
		{
			result.M11 = 2.0f / ( right - left );
			result.M12 = result.M13 = result.M14 = 0.0f;

			result.M21 = 0.0f;
			result.M22 = 2.0f / ( top - bottom );
			result.M23 = result.M24 = 0.0f;

			result.M31 = result.M32 = 0.0f;
			result.M33 = 1.0f / ( zNearPlane - zFarPlane );
			result.M34 = 0.0f;

			result.M41 = ( left + right ) / ( left - right );
			result.M42 = ( top + bottom ) / ( bottom - top );
			result.M43 = zNearPlane / ( zNearPlane - zFarPlane );
			result.M44 = 1.0f;
		}

        /// <summary>Returns a customized, orthogonal projection matrix.</summary>
        /// <param name="left">Minimum x-value of the view volume.</param>
        /// <param name="right">Maximum x-value of the view volume.</param>
        /// <param name="bottom">Minimum y-value of the view volume.</param>
        /// <param name="top">Maximum y-value of the view volume.</param>
        /// <param name="zNearPlane">Minimum z-value of the view volume.</param>
        /// <param name="zFarPlane">Maximum z-value of the view volume.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z")]
        public static Matrix CreateOrthographicOffCenter( float left, float right, float bottom, float top, float zNearPlane, float zFarPlane )
		{
			Matrix result;
			
			result.M11 = 2.0f / ( right - left );
			result.M12 = result.M13 = result.M14 = 0.0f;

			result.M21 = 0.0f;
			result.M22 = 2.0f / ( top - bottom );
			result.M23 = result.M24 = 0.0f;

			result.M31 = result.M32 = 0.0f;
			result.M33 = 1.0f / ( zNearPlane - zFarPlane );
			result.M34 = 0.0f;
			
			result.M41 = ( left + right ) / ( left - right );
			result.M42 = ( top + bottom ) / ( bottom - top );
			result.M43 = zNearPlane / ( zNearPlane - zFarPlane );
			result.M44 = 1.0f;

			return result;
		}


		/// <summary>Creates a perspective projection <see cref="Matrix"/>.</summary>
		/// <param name="width">Width of the view volume at the near view plane.</param>
		/// <param name="height">Height of the view volume at the near view plane.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane; must be greater than zero.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane; must be greater that zero.</param>
		/// <param name="result">Receives the created perspective projection <see cref="Matrix"/>.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		/// <exception cref="ArgumentException"/>
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreatePerspective( float width, float height, float nearPlaneDistance, float farPlaneDistance, out Matrix result )
		{
			if( float.IsNaN( nearPlaneDistance ) || nearPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "nearPlaneDistance" );

			if( float.IsNaN( farPlaneDistance ) || farPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "farPlaneDistance" );

			if( farPlaneDistance <= nearPlaneDistance )
				throw new ArgumentException( "farPlaneDistance <= nearPlaneDistance", "farPlaneDistance" );

			result.M11 = 2.0f * nearPlaneDistance / width;
			result.M12 = result.M13 = result.M14 = 0.0f;

			result.M21 = result.M23 = result.M24 = 0.0f;
			result.M22 = 2.0f * nearPlaneDistance / height;

			result.M33 = farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
			result.M31 = result.M32 = 0.0f;
			result.M34 = -1.0f;

			result.M41 = result.M42 = result.M44 = 0.0f;
			result.M43 = nearPlaneDistance * farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
		}

		/// <summary>Returns a perspective projection <see cref="Matrix"/>.</summary>
		/// <param name="width">Width of the view volume at the near view plane.</param>
		/// <param name="height">Height of the view volume at the near view plane.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane; must be greater than zero.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane; must be greater that zero.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"/>
		/// <exception cref="ArgumentException"/>
		public static Matrix CreatePerspective( float width, float height, float nearPlaneDistance, float farPlaneDistance )
		{
			if( float.IsNaN( nearPlaneDistance ) || nearPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "nearPlaneDistance" );

			if( float.IsNaN( farPlaneDistance ) || farPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "farPlaneDistance" );

			if( farPlaneDistance <= nearPlaneDistance )
				throw new ArgumentException( "farPlaneDistance <= nearPlaneDistance", "farPlaneDistance" );

			Matrix result;
			
			result.M11 = 2.0f * nearPlaneDistance / width;
			result.M12 = result.M13 = result.M14 = 0.0f;

			result.M21 = result.M23 = result.M24 = 0.0f;
			result.M22 = 2.0f * nearPlaneDistance / height;
			
			result.M33 = farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
			result.M31 = result.M32 = 0.0f;
			result.M34 = -1.0f;
			
			result.M41 = result.M42 = result.M44 = 0.0f;
			result.M43 = nearPlaneDistance * farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
			
			return result;
		}


		/// <summary>Creates a perspective projection <see cref="Matrix"/> based on a field of view.</summary>
		/// <param name="fieldOfView">The field of view in the y direction, in radians; must be within the range ]0,π[.</param>
		/// <param name="aspectRatio">The aspect ratio, defined as view space width divided by height.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane; must be greater than zero.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane; must be greater that zero.</param>
		/// <param name="result">Receives the created perspective projection <see cref="Matrix"/>.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		/// <exception cref="ArgumentException"/>
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreatePerspectiveFieldOfView( float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance, out Matrix result )
		{
			if( float.IsNaN( fieldOfView ) || fieldOfView <= 0.0f || fieldOfView >= XMath.Pi )
				throw new ArgumentOutOfRangeException( "fieldOfView" );

			if( float.IsNaN( aspectRatio ) || aspectRatio <= 0.0f )
				throw new ArgumentOutOfRangeException( "aspectRatio" );

			if( float.IsNaN( nearPlaneDistance ) || nearPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "nearPlaneDistance" );

			if( float.IsNaN( farPlaneDistance ) || farPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "farPlaneDistance" );

			if( farPlaneDistance <= nearPlaneDistance )
				throw new ArgumentException( "farPlaneDistance <= nearPlaneDistance", "farPlaneDistance" );

			var oneOverTanHalfFOV = 1.0f / (float)Math.Tan( (double)( fieldOfView * 0.5f ) );

			result.M11 = oneOverTanHalfFOV / aspectRatio;
			result.M12 = result.M13 = result.M14 = 0.0f;

			result.M21 = result.M23 = result.M24 = 0.0f;
			result.M22 = oneOverTanHalfFOV;

			result.M31 = result.M32 = 0.0f;
			result.M33 = farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
			result.M34 = -1.0f;

			result.M41 = result.M42 = result.M44 = 0.0f;
			result.M43 = nearPlaneDistance * farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
		}

		/// <summary>Returns a perspective projection <see cref="Matrix"/> based on a field of view.</summary>
		/// <param name="fieldOfView">The field of view in the y direction, in radians; must be within the range ]0,π[.</param>
		/// <param name="aspectRatio">The aspect ratio, defined as view space width divided by height.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane; must be greater than zero.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane; must be greater that zero.</param>
		/// <returns>Returns the created perspective projection <see cref="Matrix"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException"/>
		/// <exception cref="ArgumentException"/>
		public static Matrix CreatePerspectiveFieldOfView( float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance )
		{
			if( float.IsNaN( fieldOfView ) || fieldOfView <= 0.0f || fieldOfView >= XMath.Pi )
				throw new ArgumentOutOfRangeException( "fieldOfView" );

			if( float.IsNaN( aspectRatio ) || aspectRatio <= 0.0f )
				throw new ArgumentOutOfRangeException( "aspectRatio" );

			if( float.IsNaN( nearPlaneDistance ) || nearPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "nearPlaneDistance" );

			if( float.IsNaN( farPlaneDistance ) || farPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "farPlaneDistance" );
			
			if( farPlaneDistance <= nearPlaneDistance )
				throw new ArgumentException( "farPlaneDistance <= nearPlaneDistance", "farPlaneDistance" );
			
			var oneOverTanHalfFOV = 1.0f / (float)Math.Tan( (double)( fieldOfView * 0.5f ) );
			
			Matrix result;
			
			result.M11 = oneOverTanHalfFOV / aspectRatio;
			result.M12 = result.M13 = result.M14 = 0.0f;

			result.M21 = result.M23 = result.M24 = 0.0f;
			result.M22 = oneOverTanHalfFOV;
			
			result.M31 = result.M32 = 0.0f;
			result.M33 = farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
			result.M34 = -1.0f;

			result.M41 = result.M42 = result.M44 = 0.0f;
			result.M43 = nearPlaneDistance * farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
			
			return result;
		}


		/// <summary>Creates a customized, perspective projection <see cref="Matrix"/>.</summary>
		/// <param name="left">The minimum x-value of the view volume at the near view plane.</param>
		/// <param name="right">The maximum x-value of the view volume at the near view plane.</param>
		/// <param name="bottom">The minimum y-value of the view volume at the near view plane.</param>
		/// <param name="top">The maximum y-value of the view volume at the near view plane.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane; must be greater than zero.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane; must be greater that zero.</param>
		/// <param name="result">Receives the customized perspective projection <see cref="Matrix"/>.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		/// <exception cref="ArgumentException"/>
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreatePerspectiveOffCenter( float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance, out Matrix result )
		{
			if( float.IsNaN( nearPlaneDistance ) || nearPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "nearPlaneDistance" );

			if( float.IsNaN( farPlaneDistance ) || farPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "farPlaneDistance" );

			if( farPlaneDistance <= nearPlaneDistance )
				throw new ArgumentException( "farPlaneDistance <= nearPlaneDistance", "farPlaneDistance" );

			result.M11 = 2.0f * nearPlaneDistance / ( right - left );
			result.M12 = result.M13 = result.M14 = 0.0f;

			result.M21 = result.M23 = result.M24 = 0.0f;
			result.M22 = 2.0f * nearPlaneDistance / ( top - bottom );

			result.M31 = ( left + right ) / ( right - left );
			result.M32 = ( top + bottom ) / ( top - bottom );
			result.M33 = farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
			result.M34 = -1.0f;

			result.M43 = nearPlaneDistance * farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
			result.M41 = result.M42 = result.M44 = 0.0f;
		}

		/// <summary>Returns a customized, perspective projection <see cref="Matrix"/>.</summary>
		/// <param name="left">The minimum x-value of the view volume at the near view plane.</param>
		/// <param name="right">The maximum x-value of the view volume at the near view plane.</param>
		/// <param name="bottom">The minimum y-value of the view volume at the near view plane.</param>
		/// <param name="top">The maximum y-value of the view volume at the near view plane.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane; must be greater than zero.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane; must be greater that zero.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"/>
		/// <exception cref="ArgumentException"/>
		public static Matrix CreatePerspectiveOffCenter( float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance )
		{
			if( float.IsNaN( nearPlaneDistance ) || nearPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "nearPlaneDistance" );

			if( float.IsNaN( farPlaneDistance ) || farPlaneDistance <= 0.0f )
				throw new ArgumentOutOfRangeException( "farPlaneDistance" );

			if( farPlaneDistance <= nearPlaneDistance )
				throw new ArgumentException( "farPlaneDistance <= nearPlaneDistance", "farPlaneDistance" );
			
			Matrix result;
			
			result.M11 = 2.0f * nearPlaneDistance / ( right - left );
			result.M12 = result.M13 = result.M14 = 0.0f;
			
			result.M21 = result.M23 = result.M24 = 0.0f;
			result.M22 = 2.0f * nearPlaneDistance / ( top - bottom );
			
			result.M31 = ( left + right ) / ( right - left );
			result.M32 = ( top + bottom ) / ( top - bottom );
			result.M33 = farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
			result.M34 = -1.0f;
			
			result.M43 = nearPlaneDistance * farPlaneDistance / ( nearPlaneDistance - farPlaneDistance );
			result.M41 = result.M42 = result.M44 = 0.0f;
			
			return result;
		}


		/// <summary>Creates a spherical billboard which rotates around a specified object position.</summary>
		/// <param name="objectPosition">The position of the object the billboard will rotate around.</param>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="cameraUpVector">The up vector of the camera.</param>
		/// <param name="cameraForwardVector">The forward vector of the camera, or zero.</param>
		/// <param name="result">Receives the spherical billboard <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateBillboard( ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, ref Vector3 cameraForwardVector, out Matrix result )
		{
			var cameraToObject = objectPosition - cameraPosition;

			var distanceSquared = cameraToObject.LengthSquared;
			if( distanceSquared < BillboardDistanceSquaredThreshold )
				if( cameraForwardVector != Vector3.Zero )
				{
					cameraToObject = -cameraForwardVector;
					cameraToObject.Normalize();
				}
				else
					cameraToObject = Vector3.Forward;
			else
				cameraToObject /= (float)Math.Sqrt( (double)distanceSquared ); // normalized

			Vector3.Cross( ref cameraUpVector, ref cameraToObject, out Vector3 rightVector );
			rightVector.Normalize();

			Vector3.Cross( ref cameraToObject, ref rightVector, out cameraUpVector );

			result.M11 = rightVector.X;
			result.M12 = rightVector.Y;
			result.M13 = rightVector.Z;
			result.M14 = 0.0f;
			result.M21 = cameraUpVector.X;
			result.M22 = cameraUpVector.Y;
			result.M23 = cameraUpVector.Z;
			result.M24 = 0.0f;
			result.M31 = cameraToObject.X;
			result.M32 = cameraToObject.Y;
			result.M33 = cameraToObject.Z;
			result.M34 = 0.0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1.0f;
		}

		/// <summary>Returns a spherical billboard which rotates around a specified object position.</summary>
		/// <param name="objectPosition">The position of the object the billboard will rotate around.</param>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="cameraUpVector">The up vector of the camera.</param>
		/// <param name="cameraForwardVector">The forward vector of the camera, or zero.</param>
		/// <returns>Returns the spherical billboard <see cref="Matrix"/>.</returns>
		public static Matrix CreateBillboard( Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3 cameraForwardVector )
		{
			var cameraToObject = objectPosition - cameraPosition;
			
			var distanceSquared = cameraToObject.LengthSquared;
			if( distanceSquared < BillboardDistanceSquaredThreshold )
				if( cameraForwardVector != Vector3.Zero )
				{
					cameraToObject = -cameraForwardVector;
					cameraToObject.Normalize();
				}
				else
					cameraToObject = Vector3.Forward;
			else
				cameraToObject /= (float)Math.Sqrt( (double)distanceSquared ); // normalized

			Vector3.Cross( ref cameraUpVector, ref cameraToObject, out Vector3 rightVector );
			rightVector.Normalize();
			
			Vector3.Cross( ref cameraToObject, ref rightVector, out cameraUpVector );
			
			Matrix result;
			result.M11 = rightVector.X;
			result.M12 = rightVector.Y;
			result.M13 = rightVector.Z;
			result.M14 = 0.0f;
			result.M21 = cameraUpVector.X;
			result.M22 = cameraUpVector.Y;
			result.M23 = cameraUpVector.Z;
			result.M24 = 0.0f;
			result.M31 = cameraToObject.X;
			result.M32 = cameraToObject.Y;
			result.M33 = cameraToObject.Z;
			result.M34 = 0.0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1.0f;
			return result;
		}


		/// <summary>Creates a cylindrical billboard which rotates around a specified axis.</summary>
		/// <param name="objectPosition">The position of the object the billboard will rotate around.</param>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="rotateAxis">The axis to rotate the billboard around.</param>
		/// <param name="cameraForwardVector">The forward vector of the camera, or <see cref="Vector3.Zero"/>.</param>
		/// <param name="objectForwardVector">The forward vector of the object, or <see cref="Vector3.Zero"/>.</param>
		/// <param name="result">Receives the cylindrical billboard <see cref="Matrix"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateConstrainedBillboard( ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 rotateAxis, ref Vector3 cameraForwardVector, ref Vector3 objectForwardVector, out Matrix result )
		{
			var cameraToObject = objectPosition - cameraPosition;

			var distanceSquared = cameraToObject.LengthSquared;
			if( distanceSquared < BillboardDistanceSquaredThreshold )
				if( cameraForwardVector != Vector3.Zero )
				{
					cameraToObject = -cameraForwardVector;
					cameraToObject.Normalize();
				}
				else
					cameraToObject = Vector3.Forward;
			else
				cameraToObject /= (float)Math.Sqrt( (double)distanceSquared );  // normalized

			Vector3.Dot( ref rotateAxis, ref cameraToObject, out float dot );

			Vector3 frontVector, sideVector;
			if( Math.Abs( dot ) > ConstrainedBillboardThreshold )
			{
				if( objectForwardVector != Vector3.Zero )
				{
					frontVector = objectForwardVector;
					Vector3.Dot( ref rotateAxis, ref frontVector, out dot );
					if( Math.Abs( dot ) > ConstrainedBillboardThreshold )
					{
						dot = Vector3.Dot( rotateAxis, Vector3.Forward );
						frontVector = ( ( Math.Abs( dot ) > ConstrainedBillboardThreshold ) ? Vector3.Right : Vector3.Forward );
					}
				}
				else
				{
					dot = Vector3.Dot( rotateAxis, Vector3.Forward );
					frontVector = ( ( Math.Abs( dot ) > ConstrainedBillboardThreshold ) ? Vector3.Right : Vector3.Forward );
				}

				Vector3.Cross( ref rotateAxis, ref frontVector, out sideVector );
			}
			else
			{
				Vector3.Cross( ref rotateAxis, ref cameraToObject, out sideVector );
			}

			sideVector.Normalize();

			Vector3.Cross( ref sideVector, ref rotateAxis, out frontVector );
			frontVector.Normalize();

			result.M11 = sideVector.X;
			result.M12 = sideVector.Y;
			result.M13 = sideVector.Z;
			result.M14 = 0.0f;
			result.M21 = rotateAxis.X;
			result.M22 = rotateAxis.Y;
			result.M23 = rotateAxis.Z;
			result.M24 = 0.0f;
			result.M31 = frontVector.X;
			result.M32 = frontVector.Y;
			result.M33 = frontVector.Z;
			result.M34 = 0.0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1.0f;
		}

		/// <summary>Returns a cylindrical billboard which rotates around a specified axis.</summary>
		/// <param name="objectPosition">The position of the object the billboard will rotate around.</param>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="rotateAxis">The axis to rotate the billboard around.</param>
		/// <param name="cameraForwardVector">The forward vector of the camera, or <see cref="Vector3.Zero"/>.</param>
		/// <param name="objectForwardVector">The forward vector of the object, or <see cref="Vector3.Zero"/>.</param>
		/// <returns>Returns the cylindrical billboard <see cref="Matrix"/>.</returns>
		public static Matrix CreateConstrainedBillboard( Vector3 objectPosition, Vector3 cameraPosition, Vector3 rotateAxis, Vector3 cameraForwardVector, Vector3 objectForwardVector )
		{
			var cameraToObject = objectPosition - cameraPosition;
			
			var distanceSquared = cameraToObject.LengthSquared;
			if( distanceSquared < BillboardDistanceSquaredThreshold )
				if( cameraForwardVector != Vector3.Zero )
				{
					cameraToObject = -cameraForwardVector;
					cameraToObject.Normalize();
				}
				else
					cameraToObject = Vector3.Forward;
			else
				cameraToObject /= (float)Math.Sqrt( (double)distanceSquared );  // normalized

			Vector3.Dot( ref rotateAxis, ref cameraToObject, out float dot );

			Vector3 frontVector, sideVector;
			if( Math.Abs( dot ) > ConstrainedBillboardThreshold )
			{
				if( objectForwardVector != Vector3.Zero )
				{
					frontVector = objectForwardVector;
					Vector3.Dot( ref rotateAxis, ref frontVector, out dot );
					if( Math.Abs( dot ) > ConstrainedBillboardThreshold )
					{
						dot = Vector3.Dot( rotateAxis, Vector3.Forward );
						frontVector = ( ( Math.Abs( dot ) > ConstrainedBillboardThreshold ) ? Vector3.Right : Vector3.Forward );
					}
				}
				else
				{
					dot = Vector3.Dot( rotateAxis, Vector3.Forward );
					frontVector = ( ( Math.Abs( dot ) > ConstrainedBillboardThreshold ) ? Vector3.Right : Vector3.Forward );
				}
				
				Vector3.Cross( ref rotateAxis, ref frontVector, out sideVector );
			}
			else
			{
				Vector3.Cross( ref rotateAxis, ref cameraToObject, out sideVector );
			}
			
			sideVector.Normalize();

			Vector3.Cross( ref sideVector, ref rotateAxis, out frontVector );
			frontVector.Normalize();
			
			Matrix result;
			result.M11 = sideVector.X;
			result.M12 = sideVector.Y;
			result.M13 = sideVector.Z;
			result.M14 = 0.0f;
			result.M21 = rotateAxis.X;
			result.M22 = rotateAxis.Y;
			result.M23 = rotateAxis.Z;
			result.M24 = 0.0f;
			result.M31 = frontVector.X;
			result.M32 = frontVector.Y;
			result.M33 = frontVector.Z;
			result.M34 = 0.0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1.0f;
			return result;
		}


		/// <summary>Creates a <see cref="Matrix"/> from scale, rotation and translation (SRT) parameters.</summary>
		/// <param name="scale">A <see cref="Vector3"/> representing the scale.</param>
		/// <param name="rotation">A <see cref="Quaternion"/> representing the rotation.</param>
		/// <param name="translation">A <see cref="Vector3"/> representing the translation.</param>
		/// <param name="result">Receives a <see cref="Matrix"/> initialized with the specified parameters.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		[SuppressMessage( "Microsoft.Design", "CA1062", MessageId = "3" )]
		public static void CreateFromScaleRotationTranslation( ref Vector3 scale, ref Quaternion rotation, ref Vector3 translation, out Matrix result )
		{
			CreateFromQuaternion( ref rotation, out result );

			var factor = scale.X;
			result.M11 *= factor;
			result.M12 *= factor;
			result.M13 *= factor;

			factor = scale.Y;
			result.M21 *= factor;
			result.M22 *= factor;
			result.M23 *= factor;

			factor = scale.Z;
			result.M31 *= factor;
			result.M32 *= factor;
			result.M33 *= factor;

			result.M41 = translation.X;
			result.M42 = translation.Y;
			result.M43 = translation.Z;
		}

		/// <summary>Returns a <see cref="Matrix"/> created from scale, rotation and translation (SRT) parameters.</summary>
		/// <param name="scale">A <see cref="Vector3"/> representing the scale.</param>
		/// <param name="rotation">A <see cref="Quaternion"/> representing the rotation.</param>
		/// <param name="translation">A <see cref="Vector3"/> representing the translation.</param>
		/// <returns>Returns a <see cref="Matrix"/> initialized with the specified parameters.</returns>
		public static Matrix CreateFromScaleRotationTranslation( Vector3 scale, Quaternion rotation, Vector3 translation )
		{
			CreateFromQuaternion( ref rotation, out Matrix result );

			var factor = scale.X;
			result.M11 *= factor;
			result.M12 *= factor;
			result.M13 *= factor;

			factor = scale.Y;
			result.M21 *= factor;
			result.M22 *= factor;
			result.M23 *= factor;

			factor = scale.Z;
			result.M31 *= factor;
			result.M32 *= factor;
			result.M33 *= factor;

			result.M41 = translation.X;
			result.M42 = translation.Y;
			result.M43 = translation.Z;
			return result;
		}

		#endregion Static methods


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="matrix">A <see cref="Matrix"/>.</param>
		/// <param name="other">A <see cref="Matrix"/>.</param>
		/// <returns>Returns true if the specified matrices are equal, otherwise returns false.</returns>
		public static bool operator ==( Matrix matrix, Matrix other )
		{
			return
				( matrix.M11 == other.M11 ) && ( matrix.M12 == other.M12 ) && ( matrix.M13 == other.M13 ) && ( matrix.M14 == other.M14 ) &&
				( matrix.M21 == other.M21 ) && ( matrix.M22 == other.M22 ) && ( matrix.M23 == other.M23 ) && ( matrix.M24 == other.M24 ) &&
				( matrix.M31 == other.M31 ) && ( matrix.M32 == other.M32 ) && ( matrix.M33 == other.M33 ) && ( matrix.M34 == other.M34 ) &&
				( matrix.M41 == other.M41 ) && ( matrix.M42 == other.M42 ) && ( matrix.M43 == other.M43 ) && ( matrix.M44 == other.M44 );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="matrix">A <see cref="Matrix"/>.</param>
		/// <param name="other">A <see cref="Matrix"/>.</param>
		/// <returns>Returns true if the specified matrices are not equal, otherwise returns false.</returns>
		public static bool operator !=( Matrix matrix, Matrix other )
		{
			return
				( matrix.M11 != other.M11 ) || ( matrix.M12 != other.M12 ) || ( matrix.M13 != other.M13 ) || ( matrix.M14 != other.M14 ) ||
				( matrix.M21 != other.M21 ) || ( matrix.M22 != other.M22 ) || ( matrix.M23 != other.M23 ) || ( matrix.M24 != other.M24 ) ||
				( matrix.M31 != other.M31 ) || ( matrix.M32 != other.M32 ) || ( matrix.M33 != other.M33 ) || ( matrix.M34 != other.M34 ) ||
				( matrix.M41 != other.M41 ) || ( matrix.M42 != other.M42 ) || ( matrix.M43 != other.M43 ) || ( matrix.M44 != other.M44 );
		}


		/// <summary>Unary negation operator.</summary>
		/// <param name="matrix">A <see cref="Matrix"/>.</param>
		/// <returns></returns>
		public static Matrix operator -( Matrix matrix )
		{
			matrix.M11 = -matrix.M11;
			matrix.M12 = -matrix.M12;
			matrix.M13 = -matrix.M13;
			matrix.M14 = -matrix.M14;
			matrix.M21 = -matrix.M21;
			matrix.M22 = -matrix.M22;
			matrix.M23 = -matrix.M23;
			matrix.M24 = -matrix.M24;
			matrix.M31 = -matrix.M31;
			matrix.M32 = -matrix.M32;
			matrix.M33 = -matrix.M33;
			matrix.M34 = -matrix.M34;
			matrix.M41 = -matrix.M41;
			matrix.M42 = -matrix.M42;
			matrix.M43 = -matrix.M43;
			matrix.M44 = -matrix.M44;
			return matrix;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="matrix">A <see cref="Matrix"/>.</param>
		/// <param name="other">A <see cref="Matrix"/>.</param>
		/// <returns></returns>
		public static Matrix operator +( Matrix matrix, Matrix other )
		{
			matrix.M11 += other.M11;
			matrix.M12 += other.M12;
			matrix.M13 += other.M13;
			matrix.M14 += other.M14;

			matrix.M21 += other.M21;
			matrix.M22 += other.M22;
			matrix.M23 += other.M23;
			matrix.M24 += other.M24;

			matrix.M31 += other.M31;
			matrix.M32 += other.M32;
			matrix.M33 += other.M33;
			matrix.M34 += other.M34;

			matrix.M41 += other.M41;
			matrix.M42 += other.M42;
			matrix.M43 += other.M43;
			matrix.M44 += other.M44;

			return matrix;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="matrix">A <see cref="Matrix"/>.</param>
		/// <param name="other">A <see cref="Matrix"/>.</param>
		/// <returns></returns>
		public static Matrix operator -( Matrix matrix, Matrix other )
		{
			matrix.M11 -= other.M11;
			matrix.M12 -= other.M12;
			matrix.M13 -= other.M13;
			matrix.M14 -= other.M14;

			matrix.M21 -= other.M21;
			matrix.M22 -= other.M22;
			matrix.M23 -= other.M23;
			matrix.M24 -= other.M24;

			matrix.M31 -= other.M31;
			matrix.M32 -= other.M32;
			matrix.M33 -= other.M33;
			matrix.M34 -= other.M34;

			matrix.M41 -= other.M41;
			matrix.M42 -= other.M42;
			matrix.M43 -= other.M43;
			matrix.M44 -= other.M44;

			return matrix;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="matrix">A <see cref="Matrix"/>.</param>
		/// <param name="other">A <see cref="Matrix"/>.</param>
		/// <returns></returns>
		public static Matrix operator *( Matrix matrix, Matrix other )
		{
			Matrix result;
			result.M11 = matrix.M11 * other.M11 + matrix.M12 * other.M21 + matrix.M13 * other.M31 + matrix.M14 * other.M41;
			result.M12 = matrix.M11 * other.M12 + matrix.M12 * other.M22 + matrix.M13 * other.M32 + matrix.M14 * other.M42;
			result.M13 = matrix.M11 * other.M13 + matrix.M12 * other.M23 + matrix.M13 * other.M33 + matrix.M14 * other.M43;
			result.M14 = matrix.M11 * other.M14 + matrix.M12 * other.M24 + matrix.M13 * other.M34 + matrix.M14 * other.M44;
			result.M21 = matrix.M21 * other.M11 + matrix.M22 * other.M21 + matrix.M23 * other.M31 + matrix.M24 * other.M41;
			result.M22 = matrix.M21 * other.M12 + matrix.M22 * other.M22 + matrix.M23 * other.M32 + matrix.M24 * other.M42;
			result.M23 = matrix.M21 * other.M13 + matrix.M22 * other.M23 + matrix.M23 * other.M33 + matrix.M24 * other.M43;
			result.M24 = matrix.M21 * other.M14 + matrix.M22 * other.M24 + matrix.M23 * other.M34 + matrix.M24 * other.M44;
			result.M31 = matrix.M31 * other.M11 + matrix.M32 * other.M21 + matrix.M33 * other.M31 + matrix.M34 * other.M41;
			result.M32 = matrix.M31 * other.M12 + matrix.M32 * other.M22 + matrix.M33 * other.M32 + matrix.M34 * other.M42;
			result.M33 = matrix.M31 * other.M13 + matrix.M32 * other.M23 + matrix.M33 * other.M33 + matrix.M34 * other.M43;
			result.M34 = matrix.M31 * other.M14 + matrix.M32 * other.M24 + matrix.M33 * other.M34 + matrix.M34 * other.M44;
			result.M41 = matrix.M41 * other.M11 + matrix.M42 * other.M21 + matrix.M43 * other.M31 + matrix.M44 * other.M41;
			result.M42 = matrix.M41 * other.M12 + matrix.M42 * other.M22 + matrix.M43 * other.M32 + matrix.M44 * other.M42;
			result.M43 = matrix.M41 * other.M13 + matrix.M42 * other.M23 + matrix.M43 * other.M33 + matrix.M44 * other.M43;
			result.M44 = matrix.M41 * other.M14 + matrix.M42 * other.M24 + matrix.M43 * other.M34 + matrix.M44 * other.M44;
			return result;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="matrix">A <see cref="Matrix"/>.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns></returns>
		public static Matrix operator *( Matrix matrix, float value )
		{
			matrix.M11 *= value;
			matrix.M12 *= value;
			matrix.M13 *= value;
			matrix.M14 *= value;

			matrix.M21 *= value;
			matrix.M22 *= value;
			matrix.M23 *= value;
			matrix.M24 *= value;

			matrix.M31 *= value;
			matrix.M32 *= value;
			matrix.M33 *= value;
			matrix.M34 *= value;

			matrix.M41 *= value;
			matrix.M42 *= value;
			matrix.M43 *= value;
			matrix.M44 *= value;

			return matrix;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <param name="matrix">A <see cref="Matrix"/>.</param>
		/// <returns></returns>
		public static Matrix operator *( float value, Matrix matrix )
		{
			matrix.M11 *= value;
			matrix.M12 *= value;
			matrix.M13 *= value;
			matrix.M14 *= value;

			matrix.M21 *= value;
			matrix.M22 *= value;
			matrix.M23 *= value;
			matrix.M24 *= value;

			matrix.M31 *= value;
			matrix.M32 *= value;
			matrix.M33 *= value;
			matrix.M34 *= value;

			matrix.M41 *= value;
			matrix.M42 *= value;
			matrix.M43 *= value;
			matrix.M44 *= value;

			return matrix;
		}


		/// <summary>Division operator.</summary>
		/// <param name="matrix">A <see cref="Matrix"/>.</param>
		/// <param name="other">A <see cref="Matrix"/>.</param>
		/// <returns></returns>
		public static Matrix operator /( Matrix matrix, Matrix other )
		{
			matrix.M11 /= other.M11;
			matrix.M12 /= other.M12;
			matrix.M13 /= other.M13;
			matrix.M14 /= other.M14;

			matrix.M21 /= other.M21;
			matrix.M22 /= other.M22;
			matrix.M23 /= other.M23;
			matrix.M24 /= other.M24;

			matrix.M31 /= other.M31;
			matrix.M32 /= other.M32;
			matrix.M33 /= other.M33;
			matrix.M34 /= other.M34;

			matrix.M41 /= other.M41;
			matrix.M42 /= other.M42;
			matrix.M43 /= other.M43;
			matrix.M44 /= other.M44;

			return matrix;
		}

		/// <summary>Division operator.</summary>
		/// <param name="matrix">A <see cref="Matrix"/>.</param>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <returns></returns>
		public static Matrix operator /( Matrix matrix, float value )
		{
			var oneOverValue = 1.0f / value;

			matrix.M11 *= oneOverValue;
			matrix.M12 *= oneOverValue;
			matrix.M13 *= oneOverValue;
			matrix.M14 *= oneOverValue;

			matrix.M21 *= oneOverValue;
			matrix.M22 *= oneOverValue;
			matrix.M23 *= oneOverValue;
			matrix.M24 *= oneOverValue;

			matrix.M31 *= oneOverValue;
			matrix.M32 *= oneOverValue;
			matrix.M33 *= oneOverValue;
			matrix.M34 *= oneOverValue;

			matrix.M41 *= oneOverValue;
			matrix.M42 *= oneOverValue;
			matrix.M43 *= oneOverValue;
			matrix.M44 *= oneOverValue;

			return matrix;
		}

		#endregion Operators

	}

}