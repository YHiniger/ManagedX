using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{

	/// <summary>A quaternion, used for vector rotation.</summary>
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct Quaternion : IEquatable<Quaternion>
	{

		/// <summary>The X component of this <see cref="Vector4"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		public float X;

		/// <summary>The Y component of this <see cref="Vector4"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		public float Y;

		/// <summary>The Z component of this <see cref="Vector4"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Z" )]
		public float Z;

		/// <summary>The W component of this <see cref="Vector4"/> structure; must be a finite number.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "W" )]
		public float W;



		#region Constructors

		/// <summary>Initializes a new <see cref="Quaternion"/>.</summary>
		/// <param name="x">The X component; must be a finite number.</param>
		/// <param name="y">The Y component; must be a finite number.</param>
		/// <param name="z">The Z component; must be a finite number.</param>
		/// <param name="w">The W component; must be a finite number.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "w" )]
		public Quaternion( float x, float y, float z, float w )
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		/// <summary>Initializes a new <see cref="Quaternion"/>.</summary>
		/// <param name="xy">The X and Y components of the vector.</param>
		/// <param name="z">The Z component; must be a finite number.</param>
		/// <param name="w">The W component; must be a finite number.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "w" )]
		public Quaternion( Vector2 xy, float z, float w )
		{
			this.X = xy.X;
			this.Y = xy.Y;
			this.Z = z;
			this.W = w;
		}

		/// <summary>Initializes a new <see cref="Quaternion"/>.</summary>
		/// <param name="xyz">A valid <see cref="Vector3"/> containing the X, Y and Z components of the vector.</param>
		/// <param name="w">The W component; must be a finite number.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "w" )]
		public Quaternion( Vector3 xyz, float w )
		{
			this.X = xyz.X;
			this.Y = xyz.Y;
			this.Z = xyz.Z;
			this.W = w;
		}

		/// <summary>Initializes a new <see cref="Quaternion"/>.</summary>
		/// <param name="xyzw">A valid <see cref="Vector4"/>, containing components of the quaternion.</param>
		public Quaternion( Vector4 xyzw )
		{
			X = xyzw.X;
			Y = xyzw.Y;
			Z = xyzw.Z;
			W = xyzw.W;
		}

		/// <summary>Initializes a new <see cref="Quaternion"/>.</summary>
		/// <param name="xyzw">The value used for both <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components; must be a finite number.</param>
		public Quaternion( float xyzw )
		{
			this.W = this.Z = this.Y = this.X = xyzw;
		}

		#endregion // Constructors



		/// <summary>Gets the length of this <see cref="Quaternion"/>.</summary>
		public float Length { get { return (float)Math.Sqrt( X * X + Y * Y + Z * Z + W * W ); } }


		/// <summary>Gets the square of the length of this <see cref="Quaternion"/>.
		/// <para>Note: this property is faster than <see cref="Length"/>, since it doesn't calculate the square root.</para>
		/// </summary>
		public float LengthSquared { get { return X * X + Y * Y + Z * Z + W * W; } }


		/// <summary>Normalizes this <see cref="Quaternion"/>.</summary>
		public void Normalize()
		{
			var length = (float)Math.Sqrt( X * X + Y * Y + Z * Z + W * W );
			if( length != 0.0f )
			{
				var inv = 1.0f / length;
				X *= inv;
				Y *= inv;
				Z *= inv;
				W *= inv;
			}
		}


		/// <summary>Inverts this <see cref="Quaternion"/>.</summary>
		public void Invert()
		{
			var length = (float)Math.Sqrt( X * X + Y * Y + Z * Z + W * W );
			if( length != 0.0f )
			{
				var inv = 1.0f / length;
				X *= -inv;
				Y *= -inv;
				Z *= -inv;
				W *= inv;
			}
		}


		/// <summary>Inverts the sign of all this <see cref="Quaternion"/>'s components.</summary>
		public void Negate()
		{
			X = -X;
			Y = -Y;
			Z = -Z;
			W = -W;
		}


		/// <summary>Rotates a <see cref="Vector2"/>.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <param name="result">Receives the rotated <paramref name="vector"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		public void Transform( ref Vector2 vector, out Vector2 result )
		{
			var y2 = Y + Y;
			var z2 = Z + Z;
			
			var xSquared2 = X * ( X + X );
			var ySquared2 = Y * y2;
			var oneMinusZSquared2 = 1.0f - Z * z2;
			var xy2 = X * y2;
			var wz2 = W * z2;

			result.X = vector.X * ( oneMinusZSquared2 - ySquared2 ) + vector.Y * ( xy2 - wz2 );
			result.Y = vector.X * ( xy2 + wz2 ) + vector.Y * ( oneMinusZSquared2 - xSquared2 );
		}

		/// <summary>Rotates a <see cref="Vector2"/>.</summary>
		/// <param name="vector">A <see cref="Vector2"/> structure.</param>
		/// <returns>Returns the rotated <paramref name="vector"/>.</returns>
		public Vector2 Transform( Vector2 vector )
		{
			var y2 = Y + Y;
			var z2 = Z + Z;

			var xSquared2 = X * ( X + X );
			var ySquared2 = Y * y2;
			var oneMinusZSquared2 = 1.0f - Z * z2;
			var xy2 = X * y2;
			var wz2 = W * z2;

			return new Vector2(
				vector.X * ( oneMinusZSquared2 - ySquared2 ) + vector.Y * ( xy2 - wz2 ),
				vector.X * ( xy2 + wz2 ) + vector.Y * ( oneMinusZSquared2 - xSquared2 )
			);
		}

		/// <summary>Rotates multiple <see cref="Vector2"/> from an array.</summary>
		/// <param name="source">An array of <see cref="Vector2"/> structures.</param>
		/// <param name="sourceIndex">The zero-based index in the <paramref name="source"/> array to start reading from.</param>
		/// <param name="destination">An array of <see cref="Vector2"/> structures, to receive the rotated vectors.</param>
		/// <param name="destinationIndex">The zero-based index in the <paramref name="destination"/> array the rotated vectors should be copied to.</param>
		/// <param name="count">The number of vectors to rotate.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void Transform( Vector2[] source, int sourceIndex, Vector2[] destination, int destinationIndex, int count )
		{
			if( source == null )
				throw new ArgumentNullException( "source" );

			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( sourceIndex < 0 )
				throw new ArgumentOutOfRangeException( "sourceIndex" );

			if( destinationIndex < 0 )
				throw new ArgumentOutOfRangeException( "destinationIndex" );

			if( source.Length < sourceIndex + count || destination.Length < destinationIndex + count )
				throw new ArgumentOutOfRangeException( "count" );

			var y2 = Y + Y;
			var z2 = Z + Z;

			var xSquared2 = X * ( X + X );
			var ySquared2 = Y * y2;
			var oneMinusZSquared2 = 1.0f - Z * z2;
			var xy2 = X * y2;
			var wz2 = W * z2;

			var a = oneMinusZSquared2 - ySquared2;
			var b = xy2 - wz2;
			var c = xy2 + wz2;
			var d = oneMinusZSquared2 - xSquared2;

			Vector2 input, output;
			for( int v = 0; v < count; v++ )
			{
				input = source[ sourceIndex + v ];
				
				output.X = input.X * a + input.Y * b;
				output.Y = input.X * c + input.Y * d;
				
				destination[ destinationIndex + v ] = output;
			}
		}


		/// <summary>Rotates a <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <param name="result">Receives the rotated <paramref name="vector"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		public void Transform( ref Vector3 vector, out Vector3 result )
		{
			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;

			var xSquared2 = X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var ySquared2 = Y * y2;
			var yz2 = Y * z2;
			var oneMinusZSquared2 = 1.0f - Z * z2;
			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;

			result.X = vector.X * ( oneMinusZSquared2 - ySquared2 ) + vector.Y * ( xy2 - wz2 ) + vector.Z * ( xz2 + wy2 );
			result.Y = vector.X * ( xy2 + wz2 ) + vector.Y * ( oneMinusZSquared2 - xSquared2 ) + vector.Z * ( yz2 - wx2 );
			result.Z = vector.X * ( xz2 - wy2 ) + vector.Y * ( yz2 + wx2 ) + vector.Z * ( 1.0f - xSquared2 - ySquared2 );
		}

		/// <summary>Rotates a <see cref="Vector3"/>.</summary>
		/// <param name="vector">A <see cref="Vector3"/> structure.</param>
		/// <returns>Returns the rotated <paramref name="vector"/>.</returns>
		public Vector3 Transform( Vector3 vector )
		{
			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;

			var xSquared2 = X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var ySquared2 = Y * y2;
			var yz2 = Y * z2;
			var oneMinusZSquared2 = 1.0f - Z * z2;
			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;

			return new Vector3(
				vector.X * ( oneMinusZSquared2 - ySquared2 ) + vector.Y * ( xy2 - wz2 ) + vector.Z * ( xz2 + wy2 ),
				vector.X * ( xy2 + wz2 ) + vector.Y * ( oneMinusZSquared2 - xSquared2 ) + vector.Z * ( yz2 - wx2 ),
				vector.X * ( xz2 - wy2 ) + vector.Y * ( yz2 + wx2 ) + vector.Z * ( 1.0f - xSquared2 - ySquared2 )
			);
		}

		/// <summary>Rotates an array of <see cref="Vector3"/>.</summary>
		/// <param name="source">An array of <see cref="Vector3"/> structures.</param>
		/// <param name="sourceIndex">The zero-based index in the <paramref name="source"/> array to start reading from.</param>
		/// <param name="destination">An array of <see cref="Vector3"/> structures, to receive the rotated vectors.</param>
		/// <param name="destinationIndex">The zero-based index in the <paramref name="destination"/> array the rotated vectors should be copied to.</param>
		/// <param name="count">The number of vectors to rotate.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void Transform( Vector3[] source, int sourceIndex, Vector3[] destination, int destinationIndex, int count )
		{
			if( source == null )
				throw new ArgumentNullException( "source" );

			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( sourceIndex < 0 )
				throw new ArgumentOutOfRangeException( "sourceIndex" );

			if( destinationIndex < 0 )
				throw new ArgumentOutOfRangeException( "destinationIndex" );

			if( source.Length < sourceIndex + count || destination.Length < destinationIndex + count )
				throw new ArgumentOutOfRangeException( "count" );

			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;
			var xSquared2 = X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var ySquared2 = Y * y2;
			var yz2 = Y * z2;
			var oneMinusZSquared2 = 1.0f - Z * z2;
			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;

			var a = oneMinusZSquared2 - ySquared2;
			var b = xy2 - wz2;
			var c = xz2 + wy2;
			var d = xy2 + wz2;
			var e = oneMinusZSquared2 - xSquared2;
			var f = yz2 - wx2;
			var g = xz2 - wy2;
			var h = yz2 + wx2;
			var i = 1.0f - xSquared2 - ySquared2;

			Vector3 input, output;
			for( int v = 0; v < count; v++ )
			{
				input = source[ sourceIndex + v ];

				output.X = input.X * a + input.Y * b + input.Z * c;
				output.Y = input.X * d + input.Y * e + input.Z * f;
				output.Z = input.X * g + input.Y * h + input.Z * i;

				destination[ destinationIndex + v ] = output;
			}
		}


		/// <summary>Rotates a <see cref="Vector4"/>.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <param name="result">Receives the rotated <paramref name="vector"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		public void Transform( ref Vector4 vector, out Vector4 result )
		{
			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;

			var oneMinusXSquared2 = 1.0f - X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var ySquared2 = Y * y2;
			var yz2 = Y * z2;
			var zSquared2 = Z * z2;
			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;

			result.X = vector.X * ( 1.0f - ySquared2 - zSquared2 ) + vector.Y * ( xy2 - wz2 ) + vector.Z * ( xz2 + wy2 );
			result.Y = vector.X * ( xy2 + wz2 ) + vector.Y * ( oneMinusXSquared2 - zSquared2 ) + vector.Z * ( yz2 - wx2 );
			result.Z = vector.X * ( xz2 - wy2 ) + vector.Y * ( yz2 + wx2 ) + vector.Z * ( oneMinusXSquared2 - ySquared2 );
			result.W = vector.W;
		}

		/// <summary>Rotates a <see cref="Vector4"/>.</summary>
		/// <param name="vector">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns the rotated <paramref name="vector"/>.</returns>
		public Vector4 Transform( Vector4 vector )
		{
			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;

			var oneMinusXSquared2 = 1.0f - X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var ySquared2 = Y * y2;
			var yz2 = Y * z2;
			var zSquared2 = Z * z2;
			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;

			return new Vector4(
				vector.X * ( 1.0f - ySquared2 - zSquared2 ) + vector.Y * ( xy2 - wz2 ) + vector.Z * ( xz2 + wy2 ),
				vector.X * ( xy2 + wz2 ) + vector.Y * ( oneMinusXSquared2 - zSquared2 ) + vector.Z * ( yz2 - wx2 ),
				vector.X * ( xz2 - wy2 ) + vector.Y * ( yz2 + wx2 ) + vector.Z * ( oneMinusXSquared2 - ySquared2 ),
				vector.W
			);
		}

		/// <summary>Rotates an array of <see cref="Vector4"/>.</summary>
		/// <param name="source">An array of <see cref="Vector4"/> structures.</param>
		/// <param name="sourceIndex">The zero-based index in the <paramref name="source"/> array to start reading from.</param>
		/// <param name="destination">An array of <see cref="Vector4"/> structures, to receive the rotated vectors.</param>
		/// <param name="destinationIndex">The zero-based index in the <paramref name="destination"/> array the rotated vectors should be copied to.</param>
		/// <param name="count">The number of vectors to rotate.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void Transform( Vector4[] source, int sourceIndex, Vector4[] destination, int destinationIndex, int count )
		{
			if( source == null )
				throw new ArgumentNullException( "source" );

			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( sourceIndex < 0 )
				throw new ArgumentOutOfRangeException( "sourceIndex" );

			if( destinationIndex < 0 )
				throw new ArgumentOutOfRangeException( "destinationIndex" );

			if( source.Length < sourceIndex + count || destination.Length < destinationIndex + count )
				throw new ArgumentOutOfRangeException( "count" );

			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;
			var oneMinusXSquared2 = 1.0f - X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var ySquared2 = Y * y2;
			var yz2 = Y * z2;
			var zSquared2 = Z * z2;
			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;

			var a = 1.0f - ySquared2 - zSquared2;
			var b = xy2 - wz2;
			var c = xz2 + wy2;
			var d = xy2 + wz2;
			var e = oneMinusXSquared2 - zSquared2;
			var f = yz2 - wx2;
			var g = xz2 - wy2;
			var h = yz2 + wx2;
			var i = oneMinusXSquared2 - ySquared2;

			Vector4 input, output;
			for( int v = 0; v < count; v++ )
			{
				input = source[ sourceIndex + v ];

				output.X = input.X * a + input.Y * b + input.Z * c;
				output.Y = input.X * d + input.Y * e + input.Z * f;
				output.Z = input.X * g + input.Y * h + input.Z * i;
				output.W = input.W;

				destination[ destinationIndex + v ] = output;
			}
		}


		/// <summary>Rotates a <see cref="Ray"/>.</summary>
		/// <param name="ray">The <see cref="Ray"/> to rotate.</param>
		/// <param name="result">Receives the rotated <see cref="Ray"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		public void Transform( ref Ray ray, out Ray result )
		{
			result.Position = ray.Position;
			this.Transform( ref ray.Direction, out result.Direction );
		}

		/// <summary>Returns a rotated <see cref="Ray"/>.</summary>
		/// <param name="ray">The <see cref="Ray"/> to rotate.</param>
		/// <returns>Returns the rotated <see cref="Ray"/>.</returns>
		public Ray Transform( Ray ray )
		{
			this.Transform( ref ray.Direction, out ray.Direction );
			return ray;
		}

		/// <summary>Rotates an array of <see cref="Ray"/>.</summary>
		/// <param name="source">An array of source <see cref="Ray"/> structures; must not be null, and must contain at least <paramref name="sourceIndex"/> + <paramref name="count"/> rays.</param>
		/// <param name="sourceIndex">The zero-based index of the first <see cref="Ray"/> in the <paramref name="source"/> array; must not be negative.</param>
		/// <param name="destination">An array to receive the rotated <see cref="Ray"/> structures; must not be null, and must contain at least <paramref name="destinationIndex"/> + <paramref name="count"/> rays.</param>
		/// <param name="destinationIndex">The zero-based index of the first <see cref="Ray"/> in the <paramref name="destination"/> array; must not be negative.</param>
		/// <param name="count">The number of rays to rotate; must be greater than zero.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void Transform( Ray[] source, int sourceIndex, Ray[] destination, int destinationIndex, int count )
		{
			if( source == null )
				throw new ArgumentNullException( "source" );

			if( sourceIndex < 0 || sourceIndex >= source.Length )
				throw new ArgumentOutOfRangeException( "sourceIndex" );

			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( destinationIndex < 0 || destinationIndex >= destination.Length )
				throw new ArgumentOutOfRangeException( "destinationIndex" );

			if( count <= 0 || count + sourceIndex >= source.Length || count + destinationIndex >= destination.Length )
				throw new ArgumentOutOfRangeException( "count" );

			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;

			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;
			var xx2 = X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var yy2 = Y * y2;
			var yz2 = Y * z2;
			var zz2 = Z * z2;

			var a = 1.0f - yy2 - zz2;
			var b = xy2 - wz2;
			var c = xz2 + wy2;

			var d = xy2 + wz2;
			var e = 1.0f - xx2 - zz2;
			var f = yz2 - wx2;

			var g = xz2 - wy2;
			var h = yz2 + wx2;
			var i = 1.0f - xx2 - yy2;

			Ray plane, result;
			for( var p = 0; p < count; p++ )
			{
				plane = source[ sourceIndex + p ];

				var x = plane.Direction.X;
				var y = plane.Direction.Y;
				var z = plane.Direction.Z;

				result.Position = plane.Position;
				result.Direction.X = x * a + y * b + z * c;
				result.Direction.Y = x * d + y * e + z * f;
				result.Direction.Z = x * g + y * h + z * i;

				destination[ destinationIndex + p ] = result;
			}
		}


		/// <summary>Rotates a <see cref="Plane"/>.</summary>
		/// <param name="plane">A <see cref="Plane"/> structure.</param>
		/// <param name="result">Receives the rotated <paramref name="plane"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		public void Transform( ref Plane plane, out Plane result )
		{
			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;
			
			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;
			var xx2 = X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var yy2 = Y * y2;
			var yz2 = Y * z2;
			var zz2 = Z * z2;
			
			var a = 1.0f - yy2 - zz2;
			var b = xy2 - wz2;
			var c = xz2 + wy2;
			
			var d = xy2 + wz2;
			var e = 1.0f - xx2 - zz2;
			var f = yz2 - wx2;
			
			var g = xz2 - wy2;
			var h = yz2 + wx2;
			var i = 1.0f - xx2 - yy2;

			var planeNormal = plane.Normal;
			var x = planeNormal.X;
			var y = planeNormal.Y;
			var z = planeNormal.Z;

			planeNormal.X = x * a + y * b + z * c;
			planeNormal.Y = x * d + y * e + z * f;
			planeNormal.Z = x * g + y * h + z * i;
			
			result.Normal = planeNormal;
			result.Distance = plane.Distance;
		}

		/// <summary>Rotates a <see cref="Plane"/>.</summary>
		/// <param name="plane">A <see cref="Plane"/> structure.</param>
		/// <returns>Returns the rotated <paramref name="plane"/>.</returns>
		public Plane Transform( Plane plane )
		{
			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;

			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;
			var xx2 = X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var yy2 = Y * y2;
			var yz2 = Y * z2;
			var zz2 = Z * z2;

			var a = 1.0f - yy2 - zz2;
			var b = xy2 - wz2;
			var c = xz2 + wy2;

			var d = xy2 + wz2;
			var e = 1.0f - xx2 - zz2;
			var f = yz2 - wx2;

			var g = xz2 - wy2;
			var h = yz2 + wx2;
			var i = 1.0f - xx2 - yy2;

			var x = plane.Normal.X;
			var y = plane.Normal.Y;
			var z = plane.Normal.Z;

			Plane result;
			result.Normal.X = x * a + y * b + z * c;
			result.Normal.Y = x * d + y * e + z * f;
			result.Normal.Z = x * g + y * h + z * i;
			result.Distance = plane.Distance;
			return result;
		}

		/// <summary>Rotates an array of <see cref="Plane"/>.</summary>
		/// <param name="source">An array of source <see cref="Plane"/> structures; must not be null.</param>
		/// <param name="sourceIndex">The zero-based index of the first <see cref="Plane"/> in the <paramref name="source"/> array; must not be negative.</param>
		/// <param name="destination">An array to receive the rotated <see cref="Plane"/> structures; must not be null.</param>
		/// <param name="destinationIndex">The zero-based index of the first <see cref="Plane"/> in the <paramref name="destination"/> array; must not be negative.</param>
		/// <param name="count">The number of planes to rotate; must be greater than zero.</param>
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
			
			if( count <= 0 || count + sourceIndex >= source.Length || count + destinationIndex >= destination.Length )
				throw new ArgumentOutOfRangeException( "count" );

			var x2 = X + X;
			var y2 = Y + Y;
			var z2 = Z + Z;

			var wx2 = W * x2;
			var wy2 = W * y2;
			var wz2 = W * z2;
			var xx2 = X * x2;
			var xy2 = X * y2;
			var xz2 = X * z2;
			var yy2 = Y * y2;
			var yz2 = Y * z2;
			var zz2 = Z * z2;

			var a = 1.0f - yy2 - zz2;
			var b = xy2 - wz2;
			var c = xz2 + wy2;

			var d = xy2 + wz2;
			var e = 1.0f - xx2 - zz2;
			var f = yz2 - wx2;

			var g = xz2 - wy2;
			var h = yz2 + wx2;
			var i = 1.0f - xx2 - yy2;

			Plane plane, result;
			for( var p = 0; p < count; p++ )
			{
				plane = source[ sourceIndex + p ];
				
				var x = plane.Normal.X;
				var y = plane.Normal.Y;
				var z = plane.Normal.Z;

				result.Normal.X = x * a + y * b + z * c;
				result.Normal.Y = x * d + y * e + z * f;
				result.Normal.Z = x * g + y * h + z * i;
				result.Distance = plane.Distance;
				
				destination[ destinationIndex + p ] = result;
			}
		}


		/// <summary>Returns a hash code for this <see cref="Quaternion"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Quaternion"/> structure.</returns>
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode() ^ this.W.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Quaternion"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Quaternion other )
		{
			return
				( X == other.X ) &&
				( Y == other.Y ) &&
				( Z == other.Z ) &&
				( W == other.W );
		}


		/// <summary>Returns a value indicating whether this <see cref="Quaternion"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Quaternion"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Quaternion ) && this.Equals( (Quaternion)obj );
		}


		/// <summary>Returns a string representing this <see cref="Quaternion"/> structure, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>,<see cref="Z"/>,<see cref="W"/>)</para>
		/// </summary>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>Returns a string representing this <see cref="Quaternion"/> structure.</returns>
		public string ToString( IFormatProvider formatProvider )
		{
			return string.Format( formatProvider ?? System.Globalization.CultureInfo.InvariantCulture, "({0},{1},{2},{3})", X, Y, Z, W );
			// FIXME - what's the "math syntax" for a quaternion ?
		}


		/// <summary>Returns a string representing this <see cref="Quaternion"/> structure, in the form:
		/// <para>(<see cref="X"/>,<see cref="Y"/>,<see cref="Z"/>,<see cref="W"/>)</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="Quaternion"/> structure.</returns>
		public override string ToString()
		{
			return this.ToString( System.Globalization.CultureInfo.InvariantCulture );
		}


		/// <summary>Returns an array containing, respectively, the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components of this <see cref="Quaternion"/> structure.</summary>
		/// <returns>Returns an array containing the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components of this <see cref="Quaternion"/> structure.</returns>
		public float[] ToArray()
		{
			return new float[] { this.X, this.Y, this.Z, this.W };
		}


		/// <summary>Returns a <see cref="Vector4"/> structure initialized with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components of this <see cref="Quaternion"/>.</summary>
		/// <returns>Returns a <see cref="Vector4"/> structure initialized with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> components of this <see cref="Quaternion"/>.</returns>
		public Vector4 ToVector4()
		{
			return new Vector4( X, Y, Z, W );
		}


		#region Static

		/// <summary>A <see cref="Quaternion"/> representing no rotation.</summary>
		public static readonly Quaternion Identity = new Quaternion( 0.0f, 0.0f, 0.0f, 1.0f );


		/// <summary>Adds two <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives the sum of the two specified <see cref="Quaternion"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Add( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			result.X = quaternion.X + other.X;
			result.Y = quaternion.Y + other.Y;
			result.Z = quaternion.Z + other.Z;
			result.W = quaternion.W + other.W;
		}

		/// <summary>Returns the sum of two <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/>.</param>
		/// <param name="other">A valid <see cref="Quaternion"/>.</param>
		/// <returns>Returns the sum of the two specified <see cref="Quaternion"/>.</returns>
		public static Quaternion Add( Quaternion quaternion, Quaternion other )
		{
			quaternion.X += other.X;
			quaternion.Y += other.Y;
			quaternion.Z += other.Z;
			quaternion.W += other.W;
			return quaternion;
		}


		/// <summary>Subtracts a <see cref="Quaternion"/> (<paramref name="other"/>) from another <see cref="Quaternion"/> (<paramref name="quaternion"/>).</summary>
		/// <param name="quaternion">A <see cref="Quaternion"/>.</param>
		/// <param name="other">A <see cref="Quaternion"/>.</param>
		/// <param name="result">Receives the difference between <paramref name="quaternion"/> and <paramref name="other"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Subtract( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			result.X = quaternion.X - other.X;
			result.Y = quaternion.Y - other.Y;
			result.Z = quaternion.Z - other.Z;
			result.W = quaternion.W - other.W;
		}

		/// <summary>Subtracts a vector (<paramref name="other"/>) from another vector (<paramref name="quaternion"/>).</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/>.</param>
		/// <param name="other">A valid <see cref="Quaternion"/>.</param>
		/// <returns>Returns the difference between <paramref name="quaternion"/> and <paramref name="other"/>.</returns>
		public static Quaternion Subtract( Quaternion quaternion, Quaternion other )
		{
			quaternion.X -= other.X;
			quaternion.Y -= other.Y;
			quaternion.Z -= other.Z;
			quaternion.W -= other.W;
			return quaternion;
		}


		/// <summary>Multiplies two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives the result of ( <paramref name="quaternion"/> × <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Multiply( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			result.X = quaternion.X * other.X;
			result.Y = quaternion.Y * other.Y;
			result.Z = quaternion.Z * other.Z;
			result.W = quaternion.W * other.W;
		}

		/// <summary>Multiplies two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="quaternion"/> × <paramref name="other"/> ).</returns>
		public static Quaternion Multiply( Quaternion quaternion, Quaternion other )
		{
			quaternion.X *= other.X;
			quaternion.Y *= other.Y;
			quaternion.Z *= other.Z;
			quaternion.W *= other.W;
			return quaternion;
		}


		/// <summary>Divides a <see cref="Quaternion"/> by another <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives the result of ( <paramref name="quaternion"/> : <paramref name="other"/> ).</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Divide( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			result.X = quaternion.X / other.X;
			result.Y = quaternion.Y / other.Y;
			result.Z = quaternion.Z / other.Z;
			result.W = quaternion.W / other.W;
		}

		/// <summary>Divides a <see cref="Quaternion"/> by another <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns the result of ( <paramref name="quaternion"/> : <paramref name="other"/> ).</returns>
		public static Quaternion Divide( Quaternion quaternion, Quaternion other )
		{
			quaternion.X /= other.X;
			quaternion.Y /= other.Y;
			quaternion.Z /= other.Z;
			quaternion.W /= other.W;
			return quaternion;
		}



		/// <summary>Retrieves a <see cref="Quaternion"/> structure whose components are set to the minimum components between two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives a <see cref="Quaternion"/> structure whose components are set to the minimum components between two <see cref="Quaternion"/> values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Min( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			if( other.X < quaternion.X )
				result.X = other.X;
			else
				result.X = quaternion.X;

			if( other.Y < quaternion.Y )
				result.Y = other.Y;
			else
				result.Y = quaternion.Y;

			if( other.Z < quaternion.Z )
				result.Z = other.Z;
			else
				result.Z = quaternion.Z;

			if( other.W < quaternion.W )
				result.W = other.W;
			else
				result.W = quaternion.W;
		}

		/// <summary>Returns a <see cref="Quaternion"/> structure whose components are set to the minimum components between two <see cref="Quaternion"/> values.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns a <see cref="Quaternion"/> structure whose components are set to the minimum components between two <see cref="Quaternion"/> values.</returns>
		public static Quaternion Min( Quaternion quaternion, Quaternion other )
		{
			if( other.X < quaternion.X )
				quaternion.X = other.X;

			if( other.Y < quaternion.Y )
				quaternion.Y = other.Y;

			if( other.Z < quaternion.Z )
				quaternion.Z = other.Z;

			if( other.W < quaternion.W )
				quaternion.W = other.W;

			return quaternion;
		}


		/// <summary>Retrieves a <see cref="Quaternion"/> structure whose components are set to the maximum components between two quaternions.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives a <see cref="Quaternion"/> structure whose components are set to the maximum components between the two specified quaternions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Max( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			if( other.X > quaternion.X )
				result.X = other.X;
			else
				result.X = quaternion.X;

			if( other.Y > quaternion.Y )
				result.Y = other.Y;
			else
				result.Y = quaternion.Y;

			if( other.Z > quaternion.Z )
				result.Z = other.Z;
			else
				result.Z = quaternion.Z;

			if( other.W > quaternion.W )
				result.W = other.W;
			else
				result.W = quaternion.W;
		}

		/// <summary>Returns a <see cref="Quaternion"/> structure whose components are set to the maximum components between two quaternions.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns a <see cref="Quaternion"/> structure whose components are set to the maximum components between the two specified quaternions.</returns>
		public static Quaternion Max( Quaternion quaternion, Quaternion other )
		{
			if( other.X > quaternion.X )
				quaternion.X = other.X;

			if( other.Y > quaternion.Y )
				quaternion.Y = other.Y;

			if( other.Z > quaternion.Z )
				quaternion.Z = other.Z;

			if( other.W > quaternion.W )
				quaternion.W = other.W;

			return quaternion;
		}


		/// <summary>Calculates the dot product of two quaternions.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> value.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> value.</param>
		/// <param name="result">Receives the dot product of the two specified quaternions.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Dot( ref Quaternion quaternion, ref Quaternion other, out float result )
		{
			result = quaternion.X * other.X + quaternion.Y * other.Y + quaternion.Z * other.Z + quaternion.W * other.W;
		}

		/// <summary>Returns the dot product of two quaternions.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> value.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> value.</param>
		/// <returns>Returns the dot product of the two specified <see cref="Quaternion"/> values.</returns>
		public static float Dot( Quaternion quaternion, Quaternion other )
		{
			return quaternion.X * other.X + quaternion.Y * other.Y + quaternion.Z * other.Z + quaternion.W * other.W;
		}


		/// <summary>Performs a linear interpolation between two quaternions.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <param name="result">Receives the result of the linear interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Performance matters." )]
		public static void Lerp( ref Quaternion source, ref Quaternion target, float amount, out Quaternion result )
		{
			result.X = source.X + ( target.X - source.X ) * amount;
			result.Y = source.Y + ( target.Y - source.Y ) * amount;
			result.Z = source.Z + ( target.Z - source.Z ) * amount;
			result.W = source.W + ( target.W - source.W ) * amount;
		}

		/// <summary>Performs a linear interpolation between two quaternions.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <returns>Returns the result of the linear interpolation.</returns>
		public static Quaternion Lerp( Quaternion source, Quaternion target, float amount )
		{
			source.X = source.X + ( target.X - source.X ) * amount;
			source.Y = source.Y + ( target.Y - source.Y ) * amount;
			source.Z = source.Z + ( target.Z - source.Z ) * amount;
			source.W = source.W + ( target.W - source.W ) * amount;
			return source;
		}


		/// <summary>Performs a spherical interpolation between two quaternions.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <param name="result">Receives the result of the interpolation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Performance matters." )]
		public static void SLerp( ref Quaternion source, ref Quaternion target, float amount, out Quaternion result )
		{
			var dotProduct = source.X * target.X + source.Y * target.Y + source.Z * target.Z + source.W * target.W;

			var signInverted = false;
			if( dotProduct < 0.0f )
			{
				signInverted = true;
				dotProduct = -dotProduct;
			}

			float srcFactor, tgtFactor;
			if( dotProduct > 0.999999f )
			{
				srcFactor = 1.0f - amount;
				tgtFactor = amount;
			}
			else
			{
				var angle = (float)Math.Acos( (double)dotProduct );
				var invSinAngle = 1.0f / (float)Math.Sin( (double)angle );
				srcFactor = (float)Math.Sin( ( 1.0f - amount ) * angle ) * invSinAngle;
				tgtFactor = (float)Math.Sin( amount * angle ) * invSinAngle;
			}

			if( signInverted )
				tgtFactor = -tgtFactor;

			result.X = srcFactor * source.X + tgtFactor * target.X;
			result.Y = srcFactor * source.Y + tgtFactor * target.Y;
			result.Z = srcFactor * source.Z + tgtFactor * target.Z;
			result.W = srcFactor * source.W + tgtFactor * target.W;
		}

		/// <summary>Returns the spherical interpolation between two quaternions.</summary>
		/// <param name="source">The source value.</param>
		/// <param name="target">The target value.</param>
		/// <param name="amount">The weighting factor; should be in the range [0,1].</param>
		/// <returns>Returns the result of the interpolation.</returns>
		public static Quaternion SLerp( Quaternion source, Quaternion target, float amount )
		{
			Quaternion result;
			SLerp( ref source, ref target, amount, out result );
			return result;
		}


		/// <summary>Concatenates two quaternions.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives the result of the concatenation.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void Concatenate( ref Quaternion quaternion, ref Quaternion other, out Quaternion result )
		{
			var x = other.Y * quaternion.Z - other.Z * quaternion.Y;
			var y = other.Z * quaternion.X - other.X * quaternion.Z;
			var z = other.X * quaternion.Y - other.Y * quaternion.X;
			var w = other.X * quaternion.X + other.Y * quaternion.Y + other.Z * quaternion.Z;

			result.X = quaternion.X * other.W + other.X * quaternion.W + x;
			result.Y = quaternion.Y * other.W + other.Y * quaternion.W + y;
			result.Z = quaternion.Z * other.W + other.Z * quaternion.W + z;
			result.W = other.W * quaternion.W - w;
		}

		/// <summary>Concatenates two quaternions.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="other">A valid <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns the result of the concatenation.</returns>
		public static Quaternion Concatenate( Quaternion quaternion, Quaternion other )
		{
			Quaternion result;
			var x = other.Y * quaternion.Z - other.Z * quaternion.Y;
			var y = other.Z * quaternion.X - other.X * quaternion.Z;
			var z = other.X * quaternion.Y - other.Y * quaternion.X;
			var w = other.X * quaternion.X + other.Y * quaternion.Y + other.Z * quaternion.Z;

			result.X = quaternion.X * other.W + other.X * quaternion.W + x;
			result.Y = quaternion.Y * other.W + other.Y * quaternion.W + y;
			result.Z = quaternion.Z * other.W + other.Z * quaternion.W + z;
			result.W = other.W * quaternion.W - w;
			return result;
		}


		/// <summary>Calculates the conjugate of a <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives the conjugate of the specified <paramref name="quaternion"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		public static void Conjugate( ref Quaternion quaternion, out Quaternion result )
		{
			result.X = -quaternion.X;
			result.Y = -quaternion.Y;
			result.Z = -quaternion.Z;
			result.W = quaternion.W;
		}

		/// <summary>Returns the conjugate of a <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns the conjugate of the specified <paramref name="quaternion"/>.</returns>
		public static Quaternion Conjugate( Quaternion quaternion )
		{
			quaternion.X = -quaternion.X;
			quaternion.Y = -quaternion.Y;
			quaternion.Z = -quaternion.Z;
			return quaternion;
		}


		/// <summary>Calculates the inverse of a <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <param name="result">Receives the inverse of the specified <paramref name="quaternion"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Performance matters." )]
		public static void Inverse( ref Quaternion quaternion, out Quaternion result )
		{
			result = quaternion;
			result.Invert();
		}

		/// <summary>Returns the inverse of a <see cref="Quaternion"/>.</summary>
		/// <param name="quaternion">A valid <see cref="Quaternion"/> structure.</param>
		/// <returns>Returns the inverse of the specified <paramref name="quaternion"/>.</returns>
		public static Quaternion Inverse( Quaternion quaternion )
		{
			quaternion.Invert();
			return quaternion;
		}


		/// <summary>Creates a new <see cref="Quaternion"/> structure from the specified yaw, pitch, and roll angles.</summary>
		/// <param name="yaw">The yaw angle, in radians, around the y-axis.</param>
		/// <param name="pitch">The pitch angle, in radians, around the x-axis.</param>
		/// <param name="roll">The roll angle, in radians, around the z-axis.</param>
		/// <param name="result">Receives a <see cref="Quaternion"/> structure filled in to express the specified yaw, pitch, and roll angles.</param>
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Performance matters." )]
		public static void CreateFromYawPitchRoll( float yaw, float pitch, float roll, out Quaternion result )
		{
			var halfRoll = roll * 0.5f;
			var sinHalfRoll = (float)Math.Sin( halfRoll );
			var cosHalfRoll = (float)Math.Cos( halfRoll );

			var halfPitch = pitch * 0.5f;
			var sinHalfPitch = (float)Math.Sin( halfPitch );
			var cosHalfPitch = (float)Math.Cos( halfPitch );
			
			var halfYaw = yaw * 0.5f;
			var sinHalfYaw = (float)Math.Sin( halfYaw );
			var cosHalfYaw = (float)Math.Cos( halfYaw );
			
			result.X = cosHalfYaw * sinHalfPitch * cosHalfRoll + sinHalfYaw * cosHalfPitch * sinHalfRoll;
			result.Y = sinHalfYaw * cosHalfPitch * cosHalfRoll - cosHalfYaw * sinHalfPitch * sinHalfRoll;
			result.Z = cosHalfYaw * cosHalfPitch * sinHalfRoll - sinHalfYaw * sinHalfPitch * cosHalfRoll;
			result.W = cosHalfYaw * cosHalfPitch * cosHalfRoll + sinHalfYaw * sinHalfPitch * sinHalfRoll;
		}

		/// <summary>Returns a <see cref="Quaternion"/> structure from the specified yaw, pitch, and roll angles.</summary>
		/// <param name="yaw">The yaw angle, in radians, around the y-axis.</param>
		/// <param name="pitch">The pitch angle, in radians, around the x-axis.</param>
		/// <param name="roll">The roll angle, in radians, around the z-axis.</param>
		/// <returns>Returns a <see cref="Quaternion"/> structure filled in to express the specified yaw, pitch, and roll angles.</returns>
		public static Quaternion CreateFromYawPitchRoll( float yaw, float pitch, float roll )
		{
			Quaternion result;
			var halfRoll = roll * 0.5f;
			var sinHalfRoll = (float)Math.Sin( halfRoll );
			var cosHalfRoll = (float)Math.Cos( halfRoll );

			var halfPitch = pitch * 0.5f;
			var sinHalfPitch = (float)Math.Sin( halfPitch );
			var cosHalfPitch = (float)Math.Cos( halfPitch );

			var halfYaw = yaw * 0.5f;
			var sinHalfYaw = (float)Math.Sin( halfYaw );
			var cosHalfYaw = (float)Math.Cos( halfYaw );

			result.X = cosHalfYaw * sinHalfPitch * cosHalfRoll + sinHalfYaw * cosHalfPitch * sinHalfRoll;
			result.Y = sinHalfYaw * cosHalfPitch * cosHalfRoll - cosHalfYaw * sinHalfPitch * sinHalfRoll;
			result.Z = cosHalfYaw * cosHalfPitch * sinHalfRoll - sinHalfYaw * sinHalfPitch * cosHalfRoll;
			result.W = cosHalfYaw * cosHalfPitch * cosHalfRoll + sinHalfYaw * sinHalfPitch * sinHalfRoll;
			return result;
		}


		/// <summary>Creates a <see cref="Quaternion"/> from a <see cref="Vector3"/> and an angle to rotate about the vector.</summary>
		/// <param name="axis">The vector to rotate around.</param>
		/// <param name="angle">The angle to rotate around the vector.</param>
		/// <param name="result">Receives the <see cref="Quaternion"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		public static void CreateFromAxisAngle( ref Vector3 axis, float angle, out Quaternion result )
		{
			var halfAngle = angle * 0.5f;
			var sinHalfAngle = (float)Math.Sin( halfAngle );
			
			result.X = axis.X * sinHalfAngle;
			result.Y = axis.Y * sinHalfAngle;
			result.Z = axis.Z * sinHalfAngle;
			result.W = (float)Math.Cos( halfAngle );
		}

		/// <summary>Returns a <see cref="Quaternion"/> from a <see cref="Vector3"/> and an angle to rotate about the vector.</summary>
		/// <param name="axis">The vector to rotate around.</param>
		/// <param name="angle">The angle to rotate around the vector.</param>
		/// <returns>Returns the <see cref="Quaternion"/>.</returns>
		public static Quaternion CreateFromAxisAngle( Vector3 axis, float angle )
		{
			Quaternion result;
			
			var halfAngle = angle * 0.5f;
			var sinHalfAngle = (float)Math.Sin( halfAngle );
			
			result.X = axis.X * sinHalfAngle;
			result.Y = axis.Y * sinHalfAngle;
			result.Z = axis.Z * sinHalfAngle;
			result.W = (float)Math.Cos( halfAngle );
			return result;
		}

		#endregion // Static


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="quaternion">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Quaternion quaternion, Quaternion other )
		{
			return ( quaternion.X == other.X ) && ( quaternion.Y == other.Y ) && ( quaternion.Z == other.Z ) && ( quaternion.W == other.W );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="quaternion">A <see cref="Vector4"/> structure.</param>
		/// <param name="other">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Quaternion quaternion, Quaternion other )
		{
			return ( quaternion.X != other.X ) || ( quaternion.Y != other.Y ) || ( quaternion.Z != other.Z ) || ( quaternion.W != other.W );
		}


		/// <summary>Unary negation operator.</summary>
		/// <param name="quaternion">A <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator -( Quaternion quaternion )
		{
			quaternion.X = -quaternion.X;
			quaternion.Y = -quaternion.Y;
			quaternion.Z = -quaternion.Z;
			quaternion.W = -quaternion.W;
			return quaternion;
		}


		/// <summary>Addition operator.</summary>
		/// <param name="quaternion">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator +( Quaternion quaternion, Quaternion other )
		{
			quaternion.X += other.X;
			quaternion.Y += other.Y;
			quaternion.Z += other.Z;
			quaternion.W += other.W;
			return quaternion;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="quaternion">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator -( Quaternion quaternion, Quaternion other )
		{
			quaternion.X -= other.X;
			quaternion.Y -= other.Y;
			quaternion.Z -= other.Z;
			quaternion.W -= other.W;
			return quaternion;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="quaternion">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator *( Quaternion quaternion, Quaternion other )
		{
			quaternion.X *= other.X;
			quaternion.Y *= other.Y;
			quaternion.Z *= other.Z;
			quaternion.W *= other.W;
			return quaternion;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="quaternion">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns></returns>
		public static Quaternion operator *( Quaternion quaternion, float value )
		{
			quaternion.X *= value;
			quaternion.Y *= value;
			quaternion.Z *= value;
			quaternion.W *= value;
			return quaternion;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="quaternion">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator *( float value, Quaternion quaternion )
		{
			quaternion.X *= value;
			quaternion.Y *= value;
			quaternion.Z *= value;
			quaternion.W *= value;
			return quaternion;
		}


		/// <summary>Division operator.</summary>
		/// <param name="quaternion">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="other">A valid, non-zero, <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator /( Quaternion quaternion, Quaternion other )
		{
			quaternion.X /= other.X;
			quaternion.Y /= other.Y;
			quaternion.Z /= other.Z;
			quaternion.W /= other.W;
			return quaternion;
		}

		/// <summary>Division operator.</summary>
		/// <param name="quaternion">A valid <see cref="Vector4"/> structure.</param>
		/// <param name="value">A finite, non-zero, single-precision floating-point value.</param>
		/// <returns></returns>
		public static Quaternion operator /( Quaternion quaternion, float value )
		{
			value = 1.0f / value;
			quaternion.X *= value;
			quaternion.Y *= value;
			quaternion.Z *= value;
			quaternion.W *= value;
			return quaternion;
		}

		/// <summary>Division operator.</summary>
		/// <param name="value">A finite, non-zero, single-precision floating-point value.</param>
		/// <param name="quaternion">A valid <see cref="Vector4"/> structure.</param>
		/// <returns></returns>
		public static Quaternion operator /( float value, Quaternion quaternion )
		{
			quaternion.X = value / quaternion.X;
			quaternion.Y = value / quaternion.Y;
			quaternion.Z = value / quaternion.Z;
			quaternion.W = value / quaternion.W;
			return quaternion;
		}

		#endregion // Operators

	}

}