using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{
	
	/// <summary>A plane.</summary>
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct Plane : IEquatable<Plane>
	{

		/// <summary>The normal of the <see cref="Plane"/>.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance matters." )]
		public Vector3 Normal;
		
		/// <summary>The distance of the plane along its <see cref="Normal"/> from the origin.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance matters." )]
		public float Distance;



		#region Constructors
		
		/// <summary>Initializes a new <see cref="Plane"/>.</summary>
		/// <param name="normal">The normal of the plane; should be normalized.</param>
		/// <param name="distance">The distance of the plane along its normal from the origin.</param>
		public Plane( Vector3 normal, float distance )
		{
			Normal = normal;
			Distance = distance;
		}


		/// <summary>Initializes a new <see cref="Plane"/>.</summary>
		/// <param name="value">A <see cref="Vector4"/> with X, Y, and Z components defining the normal of the plane, and the W component defining the distance of the plane along its normal from the origin.</param>
		public Plane( Vector4 value )
		{
			Normal.X = value.X;
			Normal.Y = value.Y;
			Normal.Z = value.Z;
			Distance = value.W;
		}


		/// <summary>Initializes a new <see cref="Plane"/>.</summary>
		/// <param name="x">The X component of the normal defining the plane.</param>
		/// <param name="y">The Y component of the normal defining the plane.</param>
		/// <param name="z">The Z component of the normal defining the plane.</param>
		/// <param name="distance">The distance of the plane along its normal from the origin.</param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z" )]
		public Plane( float x, float y, float z, float distance )
		{
			Normal.X = x;
			Normal.Y = y;
			Normal.Z = z;
			Distance = distance;
		}


		/// <summary>Initializes a new <see cref="Plane"/> from a triangle.</summary>
		/// <param name="position1">One point of a triangle defining the plane.</param>
		/// <param name="position2">Another point of a triangle defining the plane.</param>
		/// <param name="position3">The last point of a triangle defining the plane.</param>
		public Plane( Vector3 position1, Vector3 position2, Vector3 position3 )
		{
			var xAB = position2.X - position1.X;
			var yAB = position2.Y - position1.Y;
			var zAB = position2.Z - position1.Z;
			
			var xAC = position3.X - position1.X;
			var yAC = position3.Y - position1.Y;
			var zAC = position3.Z - position1.Z;
			
			Normal.X = yAB * zAC - zAB * yAC;
			Normal.Y = zAB * xAC - xAB * zAC;
			Normal.Z = xAB * yAC - yAB * xAC;
			Normal.Normalize();

			Distance = -Vector3.Dot( Normal, position1 );
		}

		#endregion Constructors



		/// <summary>Changes the coefficients of the <see cref="Normal"/> vector of this <see cref="Plane"/> to make it of unit length.</summary>
		public void Normalize()
		{
			var lengthSquared = Normal.X * Normal.X + Normal.Y * Normal.Y + Normal.Z * Normal.Z;
			if( lengthSquared == 0.0f )
				return;

			var oneOverLength = 1.0f / (float)Math.Sqrt( (double)lengthSquared );
			Normal *= oneOverLength;
			Distance *= oneOverLength;
		}


		/// <summary>Calculates the dot product of this <see cref="Plane"/> and a <see cref="Vector4"/>.</summary>
		/// <param name="value">A <see cref="Vector4"/> structure.</param>
		/// <param name="result">Receives the resulting dot product.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void Dot( ref Vector4 value, out float result )
		{
			result = Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z + Distance * value.W;
		}

		/// <summary>Returns the dot product of this <see cref="Plane"/> and a <see cref="Vector4"/>.</summary>
		/// <param name="value">A <see cref="Vector4"/> structure.</param>
		/// <returns>Returns the resulting dot product.</returns>
		public float Dot( Vector4 value )
		{
			return Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z + Distance * value.W;
		}


		/// <summary>Calculates the dot product of a specified <see cref="Vector3"/> and the <see cref="Normal"/> vector of this <see cref="Plane"/>.</summary>
		/// <param name="normal">A <see cref="Vector3"/>.</param>
		/// <param name="result">Receives the resulting dot product.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void DotNormal( ref Vector3 normal, out float result )
		{
			result = Normal.X * normal.X + Normal.Y * normal.Y + Normal.Z * normal.Z;
		}

		/// <summary>Returns the dot product of a specified <see cref="Vector3"/> and the <see cref="Normal"/> vector of this <see cref="Plane"/>.</summary>
		/// <param name="normal">A <see cref="Vector3"/>.</param>
		/// <returns>Returns the dot product of a specified <see cref="Vector3"/> and the <see cref="Normal"/> vector of this <see cref="Plane"/>.</returns>
		public float DotNormal( Vector3 normal )
		{
			return Normal.X * normal.X + Normal.Y * normal.Y + Normal.Z * normal.Z;
		}


		/// <summary>Calculates the dot product of a specified <see cref="Vector3"/> and the <see cref="Normal"/> vector of this <see cref="Plane"/> plus its distance (<see cref="Distance"/>) value.</summary>
		/// <param name="value">A <see cref="Vector3"/>.</param>
		/// <param name="result">Receives the dot product of a specified <see cref="Vector3"/> and the <see cref="Normal"/> vector of this <see cref="Plane"/> plus its distance (<see cref="Distance"/>) value.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void DotCoordinate( ref Vector3 value, out float result )
		{
			result = Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z + Distance;
		}

		/// <summary>Returns the dot product of a specified <see cref="Vector3"/> and the <see cref="Normal"/> vector of this <see cref="Plane"/> plus its distance (<see cref="Distance"/>) value.</summary>
		/// <param name="value">A <see cref="Vector3"/>.</param>
		/// <returns>Returns the dot product of a specified <see cref="Vector3"/> and the <see cref="Normal"/> vector of this <see cref="Plane"/> plus its distance (<see cref="Distance"/>) value.</returns>
		public float DotCoordinate( Vector3 value )
		{
			return Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z + Distance;
		}


		/// <summary>Calculates the intersection point with a <see cref="Ray"/>.</summary>
		/// <param name="ray">A <see cref="Ray"/>.</param>
		/// <param name="result">Receives the intersection point.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void CalculateIntersection( ref Ray ray, out Vector3 result )
		{
			float NdotP;
			Vector3.Dot( ref Normal, ref ray.Position, out NdotP );

			float NdotD;
			Vector3.Dot( ref Normal, ref ray.Direction, out NdotD );

			var distance = ( -Distance - NdotP ) / NdotD;
			result = ray.Position + ray.Direction * distance;
		}

		/// <summary>Returns the intersection point with a <see cref="Ray"/>.</summary>
		/// <param name="ray">A <see cref="Ray"/>.</param>
		/// <returns>Returns the intersection point with the specified <paramref name="ray"/>.</returns>
		public Vector3 CalculateIntersection( Ray ray )
		{
			float NdotP;
			Vector3.Dot( ref Normal, ref ray.Position, out NdotP );

			float NdotD;
			Vector3.Dot( ref Normal, ref ray.Direction, out NdotD );

			var distance = ( -Distance - NdotP ) / NdotD;
			return ray.Position + ray.Direction * distance;
		}


		/// <summary>Calculates the intersection line with another <see cref="Plane"/>.</summary>
		/// <param name="other">A <see cref="Plane"/>.</param>
		/// <param name="result">Receives a <see cref="Ray"/> representing the intersection line.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void CalculateIntersectionLine( ref Plane other, out Ray result )
		{
			var normal = other.Normal;
			
			Vector3 direction;
			Vector3.Cross( ref Normal, ref normal, out direction );

			Vector3 a;
			Vector3.Multiply( ref normal, -Distance, out a );

			Vector3 b;
			Vector3.Multiply( ref Normal, other.Distance, out b );

			Vector3 c;
			Vector3.Add( ref a, ref b, out c );

			result.Position = Vector3.Cross( c, direction ) / direction.LengthSquared;
			result.Direction = direction;
		}

		/// <summary>Returns the intersection line with another <see cref="Plane"/>.</summary>
		/// <param name="other">A <see cref="Plane"/>.</param>
		/// <returns>Returns a <see cref="Ray"/> representing the intersection line.</returns>
		public Ray CalculateIntersectionLine( Plane other )
		{
			Ray result;
			Vector3.Cross( ref Normal, ref other.Normal, out result.Direction );

			Vector3 a;
			Vector3.Multiply( ref other.Normal, -Distance, out a );

			Vector3 b;
			Vector3.Multiply( ref Normal, other.Distance, out b );

			Vector3 c;
			Vector3.Add( ref a, ref b, out c );

			result.Position = Vector3.Cross( c, result.Direction ) / result.Direction.LengthSquared;
			return result;
		}


		/// <summary>Determines the location of a point relative to this <see cref="Plane"/>.</summary>
		/// <param name="point">A <see cref="Vector3"/>.</param>
		/// <param name="result">Receives a value indicating the location of the specified <paramref name="point"/> relative to this <see cref="Plane"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void Locate( ref Vector3 point, out PlaneIntersectionType result )
		{
			float NdotP;
			Vector3.Dot( ref Normal, ref point, out NdotP );
			NdotP += Distance;

			if( NdotP > 0.0f )
			{
				result = PlaneIntersectionType.Front;
				return;
			}

			if( NdotP < 0.0f )
			{
				result = PlaneIntersectionType.Back;
				return;
			}

			result = PlaneIntersectionType.Intersecting;
		}

		/// <summary>Returns a value indicating the location of a point relative to this <see cref="Plane"/>.</summary>
		/// <param name="point">A <see cref="Vector3"/>.</param>
		/// <returns>Returns a value indicating the location of the specified <paramref name="point"/> relative to this <see cref="Plane"/>.</returns>
		public PlaneIntersectionType Locate( Vector3 point )
		{
			float NdotP;
			Vector3.Dot( ref Normal, ref point, out NdotP );
			NdotP += Distance;

			if( NdotP > 0.0f )
				return PlaneIntersectionType.Front;

			if( NdotP < 0.0f )
				return PlaneIntersectionType.Back;

			return PlaneIntersectionType.Intersecting;
		}


		#region Intersects

		/// <summary>Determines whether this <see cref="Plane"/> intersects a <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="result">Receives a value indicating whether this <see cref="Plane"/> intersects the <see cref="BoundingBox"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void Intersects( ref BoundingBox box, out PlaneIntersectionType result )
		{
			var boxMin = box.Min;
			var boxMax = box.Max;

			Vector3 v1, v2;
			if( Normal.X >= 0.0f )
			{
				v1.X = boxMin.X;
				v2.X = boxMax.X;
				v1.Y = boxMin.Y;
				v2.Y = boxMax.Y;
				v1.Z = boxMin.Z;
				v2.Z = boxMax.Z;
			}
			else
			{
				v1.X = boxMax.X;
				v2.X = boxMin.X;
				v1.Y = boxMax.Y;
				v2.Y = boxMin.Y;
				v1.Z = boxMax.Z;
				v2.Z = boxMin.Z;
			}

			var NdotV = Normal.X * v1.X + Normal.Y * v1.Y + Normal.Z * v1.Z;
			if( NdotV + Distance > 0.0f )
			{
				result = PlaneIntersectionType.Front;
				return;
			}

			NdotV = Normal.X * v2.X + Normal.Y * v2.Y + Normal.Z * v2.Z;
			if( NdotV + Distance < 0.0f )
			{
				result = PlaneIntersectionType.Back;
				return;
			}

			result = PlaneIntersectionType.Intersecting;
		}

		/// <summary>Returns a value indicating whether this <see cref="Plane"/> intersects a specified <see cref="BoundingBox"/>.</summary>
		/// <param name="box">The <see cref="BoundingBox"/> to test for intersection with.</param>
		/// <returns>Returns a value indicating whether this <see cref="Plane"/> intersects a specified <see cref="BoundingBox"/>.</returns>
		public PlaneIntersectionType Intersects( BoundingBox box )
		{
			var boxMin = box.Min;
			var boxMax = box.Max;

			Vector3 v1, v2;
			if( Normal.X >= 0.0f )
			{
				v1.X = boxMin.X;
				v2.X = boxMax.X;
				v1.Y = boxMin.Y;
				v2.Y = boxMax.Y;
				v1.Z = boxMin.Z;
				v2.Z = boxMax.Z;
			}
			else
			{
				v1.X = boxMax.X;
				v2.X = boxMin.X;
				v1.Y = boxMax.Y;
				v2.Y = boxMin.Y;
				v1.Z = boxMax.Z;
				v2.Z = boxMin.Z;
			}

			var NdotV = Normal.X * v1.X + Normal.Y * v1.Y + Normal.Z * v1.Z;
			if( NdotV + Distance > 0.0f )
				return PlaneIntersectionType.Front;
			
			NdotV = Normal.X * v2.X + Normal.Y * v2.Y + Normal.Z * v2.Z;
			if( NdotV + Distance < 0f )
				return PlaneIntersectionType.Back;
			
			return PlaneIntersectionType.Intersecting;
		}


		/// <summary>Determines whether this <see cref="Plane"/> intersects a <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <param name="result">Receives a value indicating whether this <see cref="Plane"/> intersects the bounding <paramref name="sphere"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void Intersects( ref BoundingSphere sphere, out PlaneIntersectionType result )
		{
			var sphereCenter = sphere.Center;
			var NdotC = sphereCenter.X * Normal.X + sphereCenter.Y * Normal.Y + sphereCenter.Z * Normal.Z;
			var distance = NdotC + Distance;
			
			if( distance > sphere.Radius )
			{
				result = PlaneIntersectionType.Front;
				return;
			}

			if( distance < -sphere.Radius )
			{
				result = PlaneIntersectionType.Back;
				return;
			}

			result = PlaneIntersectionType.Intersecting;
		}

		/// <summary>Returns a value indicating whether this <see cref="Plane"/> intersects a specified <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">The <see cref="BoundingSphere"/> to check for intersection with.</param>
		/// <returns>Returns a value indicating whether this <see cref="Plane"/> intersects the specified bounding <paramref name="sphere"/>.</returns>
		public PlaneIntersectionType Intersects( BoundingSphere sphere )
		{
			var NdotC = sphere.Center.X * Normal.X + sphere.Center.Y * Normal.Y + sphere.Center.Z * Normal.Z;
			var distance = NdotC + Distance;
			
			if( distance > sphere.Radius )
				return PlaneIntersectionType.Front;
			
			if( distance < -sphere.Radius )
				return PlaneIntersectionType.Back;
			
			return PlaneIntersectionType.Intersecting;
		}


		/// <summary>Determines whether this <see cref="Plane"/> intersects a <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		/// <param name="result">Receives a value indicating whether this <see cref="Plane"/> intersects the <see cref="BoundingFrustum"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void Intersects( ref BoundingFrustum frustum, out PlaneIntersectionType result )
		{
			var corners = frustum.corners;
			if( corners == null || corners.Length != BoundingFrustum.CornerCount )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			var flags = 0;
			float NdotP;
			for( var i = 0; i < BoundingFrustum.CornerCount; i++ )
			{
				Vector3.Dot( ref Normal, ref corners[ i ], out NdotP );
				
				if( NdotP + Distance > 0.0f )
					flags |= 1;
				else
					flags |= 2;
				
				if( flags == 3 )
				{
					result = PlaneIntersectionType.Intersecting;
					return;
				}
			}

			result = ( flags == 1 ) ? PlaneIntersectionType.Front : PlaneIntersectionType.Back;
		}

		/// <summary>Returns a value indicating whether this <see cref="Plane"/> intersects the <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		/// <returns>Returns a value indicating whether this <see cref="Plane"/> intersects the <see cref="BoundingFrustum"/>.</returns>
		public PlaneIntersectionType Intersects( BoundingFrustum frustum )
		{
			var flags = 0;
			var corners = frustum.corners;

			float NdotP;
			for( var i = 0; i < BoundingFrustum.CornerCount; i++ )
			{
				Vector3.Dot( ref Normal, ref corners[ i ], out NdotP );
				
				if( NdotP + Distance > 0.0f )
					flags |= 1;
				else
					flags |= 2;
				
				if( flags == 3 )
					return PlaneIntersectionType.Intersecting;
			}
			
			if( flags != 1 )
				return PlaneIntersectionType.Back;
			
			return PlaneIntersectionType.Front;
		}

		#endregion Intersects


		/// <summary>Returns a hash code for this <see cref="Plane"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Plane"/>.</returns>
		public override int GetHashCode()
		{
			return Normal.GetHashCode() ^ Distance.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Plane"/> equals another plane.</summary>
		/// <param name="other">A <see cref="Plane"/>.</param>
		/// <returns>Returns true if the planes are equal, otherwise returns false.</returns>
		public bool Equals( Plane other )
		{
			return ( Distance == other.Distance ) && Normal.Equals( ref other.Normal );
		}


		/// <summary>Returns a value indicating whether this <see cref="Plane"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Plane"/> which equals this plane, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Plane ) && this.Equals( (Plane)obj );
		}


		/// <summary>Returns a string representing this <see cref="Plane"/>.</summary>
		/// <returns>Returns a string representing this <see cref="Plane"/>.</returns>
		public override string ToString()
		{
			var formatProvider = System.Globalization.CultureInfo.InvariantCulture;
			return "{Normal: " + Normal.ToString( formatProvider ) + ", Distance: " + Distance.ToString( formatProvider ) + '}';
		}


		/// <summary>The «zero» (and invalid) <see cref="Plane"/>.</summary>
		public static readonly Plane Zero;


		/// <summary>Changes the coefficients of the <see cref="Normal"/> vector of a <see cref="Plane"/> to make it of unit length.</summary>
		/// <param name="plane">A <see cref="Plane"/>.</param>
		/// <param name="result">Receives the normalized <paramref name="plane"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public static void Normalize( ref Plane plane, out Plane result )
		{
			var planeNormal = plane.Normal;
			
			var lengthSquared = planeNormal.X * planeNormal.X + planeNormal.Y * planeNormal.Y + planeNormal.Z * planeNormal.Z;
			if( lengthSquared == 0.0f )
			{
				result.Normal = planeNormal;
				result.Distance = plane.Distance;
				return;
			}
			
			var oneOverLength = 1.0f / (float)Math.Sqrt( (double)lengthSquared );
			result.Normal = planeNormal * oneOverLength;
			result.Distance = plane.Distance * oneOverLength;
		}

		/// <summary>Returns a <see cref="Plane"/> whose <see cref="Normal"/> coefficients have been changed to make it of unit length.</summary>
		/// <param name="plane">A <see cref="Plane"/>.</param>
		/// <returns>Returns the normalized <paramref name="plane"/>.</returns>
		public static Plane Normalize( Plane plane )
		{
			var lengthSquared = plane.Normal.X * plane.Normal.X + plane.Normal.Y * plane.Normal.Y + plane.Normal.Z * plane.Normal.Z;
			if( lengthSquared == 0.0f )
				return plane;
			
			var oneOverLength = 1.0f / (float)Math.Sqrt( (double)lengthSquared );
			plane.Normal *= oneOverLength;
			plane.Distance *= oneOverLength;

			return plane;
		}


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="plane">A <see cref="Plane"/>.</param>
		/// <param name="other">A <see cref="Plane"/>.</param>
		/// <returns>Returns true if the planes are equal, otherwise returns false.</returns>
		public static bool operator ==( Plane plane, Plane other )
		{
			return ( plane.Distance == other.Distance ) && plane.Normal.Equals( ref other.Normal );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="plane">A <see cref="Plane"/>.</param>
		/// <param name="other">A <see cref="Plane"/>.</param>
		/// <returns>Returns true if the planes are not equal, otherwise returns false.</returns>
		public static bool operator !=( Plane plane, Plane other )
		{
			return ( plane.Distance != other.Distance ) || !plane.Normal.Equals( ref other.Normal );
		}

		#endregion Operators

	}

}