using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{

	/// <summary>A bounding sphere.
	/// <para>This structure is equivalent to the native <code>BoundingSphere</code> structure (defined in DirectXCollision.h).</para>
	/// </summary>
	[Win32.Source( "DirectXCollision.h" )]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct BoundingSphere : IEquatable<BoundingSphere>
	{

		/// <summary>The position of the center of the sphere.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Vector3 Center;
		
		/// <summary>The radius of the sphere.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float Radius;



		/// <summary>Initializes a new <see cref="BoundingSphere"/>.</summary>
		/// <param name="center">The position of the center of the sphere.</param>
		/// <param name="radius">The radius of the sphere; must be a finite, positive value (or zero).</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public BoundingSphere( Vector3 center, float radius )
		{
			if( float.IsNaN( radius ) || radius < 0.0f )
				throw new ArgumentOutOfRangeException( "radius" );

			Center = center;
			Radius = radius;
		}



		#region Contains

		/// <summary>Determines whether this <see cref="BoundingSphere"/> contains the specified point.</summary>
		/// <param name="point">The point to test for overlap.</param>
		/// <param name="result">Receives a value indicating the extent of overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void Contains( ref Vector3 point, out ContainmentType result )
		{
			Vector3.DistanceSquared( ref Center, ref point, out float distanceSquared );

			var radiusSquared = Radius * Radius;
			
			if( distanceSquared < radiusSquared )
				result = ContainmentType.Contains;
			else if( distanceSquared > radiusSquared )
				result = ContainmentType.Disjoint;
			else
				result = ContainmentType.Intersects;
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingSphere"/> contains the specified point.</summary>
		/// <param name="point">The point to test for overlap.</param>
		/// <returns>Returns a value indicating the extent of overlap.</returns>
		public ContainmentType Contains( Vector3 point )
		{
			Vector3.DistanceSquared( ref Center, ref point, out float distanceSquared );

			var radiusSquared = Radius * Radius;
			
			if( distanceSquared < radiusSquared )
				return ContainmentType.Contains;

			if( distanceSquared > radiusSquared )
				return ContainmentType.Disjoint;
				
			return ContainmentType.Intersects;
		}


		/// <summary>Determines whether this <see cref="BoundingSphere"/> contains another <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">The <see cref="BoundingSphere"/> to test for overlap.</param>
		/// <param name="result">Receives a value indicating the extent of overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void Contains( ref BoundingSphere sphere, out ContainmentType result )
		{
			Vector3.Distance( ref Center, ref sphere.Center, out float distance );

			var sphereRadius = sphere.Radius;

			if( distance > Radius + sphereRadius )
				result = ContainmentType.Disjoint;
			else if( distance <= Radius - sphereRadius )
				result = ContainmentType.Contains;
			else
				result = ContainmentType.Intersects;
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingSphere"/> contains another <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">The <see cref="BoundingSphere"/> to test for overlap.</param>
		/// <returns>Returns a value indicating the extent of overlap.</returns>
		public ContainmentType Contains( BoundingSphere sphere )
		{
			Vector3.Distance( ref Center, ref sphere.Center, out float distance );

			var sphereRadius = sphere.Radius;

			if( distance > Radius + sphereRadius )
				return ContainmentType.Disjoint;

			if( distance <= Radius - sphereRadius )
				return ContainmentType.Contains;

			return ContainmentType.Intersects;
		}


		/// <summary>Determines whether this <see cref="BoundingSphere"/> contains the specified <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="result">Receives a value indicating the extent of overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void Contains( ref BoundingBox box, out ContainmentType result )
		{
			box.Intersects( ref this, out bool intersects );
			if( !intersects )
			{
				result = ContainmentType.Disjoint;
				return;
			}
			
			var radiusSquared = Radius * Radius;
			result = ContainmentType.Intersects;

			var boxMin = box.Min;
			var boxMax = box.Max;

			Vector3 vector;
			vector.X = Center.X - boxMin.X;
			vector.Y = Center.Y - boxMax.Y;
			vector.Z = Center.Z - boxMax.Z;
			if( vector.LengthSquared > radiusSquared )
				return;
			
			vector.X = Center.X - boxMax.X;
			vector.Y = Center.Y - boxMax.Y;
			vector.Z = Center.Z - boxMax.Z;
			if( vector.LengthSquared > radiusSquared )
				return;
			
			vector.X = Center.X - boxMax.X;
			vector.Y = Center.Y - boxMin.Y;
			vector.Z = Center.Z - boxMax.Z;
			if( vector.LengthSquared > radiusSquared )
				return;

			vector.X = Center.X - boxMin.X;
			vector.Y = Center.Y - boxMin.Y;
			vector.Z = Center.Z - boxMax.Z;
			if( vector.LengthSquared > radiusSquared )
				return;
	
			vector.X = Center.X - boxMin.X;
			vector.Y = Center.Y - boxMax.Y;
			vector.Z = Center.Z - boxMin.Z;
			if( vector.LengthSquared > radiusSquared )
				return;
			
			vector.X = Center.X - boxMax.X;
			vector.Y = Center.Y - boxMax.Y;
			vector.Z = Center.Z - boxMin.Z;
			if( vector.LengthSquared > radiusSquared )
				return;

			vector.X = Center.X - boxMax.X;
			vector.Y = Center.Y - boxMin.Y;
			vector.Z = Center.Z - boxMin.Z;
			if( vector.LengthSquared > radiusSquared )
				return;
			
			vector.X = Center.X - boxMin.X;
			vector.Y = Center.Y - boxMin.Y;
			vector.Z = Center.Z - boxMin.Z;
			if( vector.LengthSquared > radiusSquared )
				return;
			
			result = ContainmentType.Contains;
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingSphere"/> contains the specified <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <returns>Returns a value indicating whether this <see cref="BoundingSphere"/> contains the specified <paramref name="box"/>.</returns>
		public ContainmentType Contains( BoundingBox box )
		{
			box.Intersects( ref this, out bool intersects );
			if( !intersects )
				return ContainmentType.Disjoint;

			var radiusSquared = Radius * Radius;

			var boxMin = box.Min;
			var boxMax = box.Max;

			Vector3 vector;
			vector.X = Center.X - boxMin.X;
			vector.Y = Center.Y - boxMax.Y;
			vector.Z = Center.Z - boxMax.Z;
			if( vector.LengthSquared > radiusSquared )
				return ContainmentType.Intersects;

			vector.X = Center.X - boxMax.X;
			vector.Y = Center.Y - boxMax.Y;
			vector.Z = Center.Z - boxMax.Z;
			if( vector.LengthSquared > radiusSquared )
				return ContainmentType.Intersects;

			vector.X = Center.X - boxMax.X;
			vector.Y = Center.Y - boxMin.Y;
			vector.Z = Center.Z - boxMax.Z;
			if( vector.LengthSquared > radiusSquared )
				return ContainmentType.Intersects;

			vector.X = Center.X - boxMin.X;
			vector.Y = Center.Y - boxMin.Y;
			vector.Z = Center.Z - boxMax.Z;
			if( vector.LengthSquared > radiusSquared )
				return ContainmentType.Intersects;

			vector.X = Center.X - boxMin.X;
			vector.Y = Center.Y - boxMax.Y;
			vector.Z = Center.Z - boxMin.Z;
			if( vector.LengthSquared > radiusSquared )
				return ContainmentType.Intersects;

			vector.X = Center.X - boxMax.X;
			vector.Y = Center.Y - boxMax.Y;
			vector.Z = Center.Z - boxMin.Z;
			if( vector.LengthSquared > radiusSquared )
				return ContainmentType.Intersects;

			vector.X = Center.X - boxMax.X;
			vector.Y = Center.Y - boxMin.Y;
			vector.Z = Center.Z - boxMin.Z;
			if( vector.LengthSquared > radiusSquared )
				return ContainmentType.Intersects;

			vector.X = Center.X - boxMin.X;
			vector.Y = Center.Y - boxMin.Y;
			vector.Z = Center.Z - boxMin.Z;
			if( vector.LengthSquared > radiusSquared )
				return ContainmentType.Intersects;

			return ContainmentType.Contains;
		}


		/// <summary>Determines whether this <see cref="BoundingSphere"/> contains the specified <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		/// <param name="result">Receives a value indicating whether this <see cref="BoundingSphere"/> contains the specified <paramref name="frustum"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void Contains( ref BoundingFrustum frustum, out ContainmentType result )
		{
			var corners = frustum.corners;
			if( corners == null )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			if( !frustum.Intersects( this ) )
			{
				result = ContainmentType.Disjoint;
				return;
			}

			var radiusSquared = Radius * Radius;

			result = ContainmentType.Contains;
			for( var i = 0; i < BoundingFrustum.CornerCount; i++ )
			{
				Vector3.Subtract( ref corners[ i ], ref Center, out Vector3 vector );
				if( vector.LengthSquared > radiusSquared )
				{
					result = ContainmentType.Intersects;
					return;
				}
			}
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingSphere"/> contains the specified <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		/// <returns>Returns a value indicating whether this <see cref="BoundingSphere"/> contains the specified <paramref name="frustum"/>.</returns>
		public ContainmentType Contains( BoundingFrustum frustum )
		{
			var cornerArray = frustum.corners;
			if( cornerArray == null )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			if( !frustum.Intersects( this ) )
				return ContainmentType.Disjoint;
			
			var radiusSquared = Radius * Radius;

			for( var i = 0; i < BoundingFrustum.CornerCount; i++ )
			{
				Vector3.Subtract( ref cornerArray[ i ], ref Center, out Vector3 vector2 );
				if( vector2.LengthSquared > radiusSquared )
					return ContainmentType.Intersects;
			}

			return ContainmentType.Contains;
		}

		#endregion Contains


		#region Intersects

		/// <summary>Determines whether this <see cref="BoundingSphere"/> intersects another <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <param name="result">Receives a value indicating whether the <see cref="BoundingSphere"/> instances intersect.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void Intersects( ref BoundingSphere sphere, out bool result )
		{
			Vector3.DistanceSquared( ref Center, ref sphere.Center, out float distanceSquared );

			var radius2 = sphere.Radius;
			result = ( Radius * Radius + 2.0f * Radius * radius2 + radius2 * radius2 > distanceSquared );
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingSphere"/> intersects another <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <returns>Returns a value indicating whether the <see cref="BoundingSphere"/> instances intersect.</returns>
		public bool Intersects( BoundingSphere sphere )
		{
			Vector3.DistanceSquared( ref Center, ref sphere.Center, out float distanceSquared );
			var radius2 = sphere.Radius;
			return ( Radius * Radius + 2.0f * Radius * radius2 + radius2 * radius2 > distanceSquared );
		}


		/// <summary>Determines whether this <see cref="BoundingSphere"/> intersects a <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="result">Receives true if the <see cref="BoundingSphere"/> and <see cref="BoundingBox"/> intersect, false otherwise.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		public void Intersects( ref BoundingBox box, out bool result )
		{
			Vector3.Clamp( ref Center, ref box.Min, ref box.Max, out Vector3 clamped );

			Vector3.DistanceSquared( ref Center, ref clamped, out float distanceSquared );

			result = ( distanceSquared <= Radius * Radius );
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingSphere"/> intersects with a specified <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <returns>Returns a value indicating whether this <see cref="BoundingSphere"/> and the <see cref="BoundingBox"/> intersect, otherwise returns false.</returns>
		public bool Intersects( BoundingBox box )
		{
			Vector3.Clamp( ref this.Center, ref box.Min, ref box.Max, out Vector3 vector );
			Vector3.DistanceSquared( ref this.Center, ref vector, out float num );
			return num <= this.Radius * this.Radius;
		}


		///// <summary>Determines whether this <see cref="BoundingSphere"/> intersects a <see cref="BoundingFrustum"/>.</summary>
		///// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		///// <param name="result">Receives true if the <see cref="BoundingSphere"/> and <see cref="BoundingFrustum"/> intersect, false otherwise.</param>
		//[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		//[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		//public void Intersects( ref BoundingFrustum frustum, out bool result )
		//{
		//	frustum.Intersects( ref this, out result );
		//}

		///// <summary>Returns a value indicating whether this <see cref="BoundingSphere"/> intersects with a specified <see cref="BoundingFrustum"/>.</summary>
		///// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		///// <returns>Returns a value indicating whether this <see cref="BoundingSphere"/> and the <see cref="BoundingFrustum"/> intersect, otherwise returns false.</returns>
		//public bool Intersects( BoundingFrustum frustum )
		//{
		//	bool result;
		//	frustum.Intersects( ref this, out result );
		//	return result;
		//}

	
		internal void SupportMapping( ref Vector3 v, out Vector3 result )
		{
			var radiusOverLength = Radius / v.Length;
			result.X = Center.X + v.X * radiusOverLength;
			result.Y = Center.Y + v.Y * radiusOverLength;
			result.Z = Center.Z + v.Z * radiusOverLength;
		}

		#endregion Intersects


		/// <summary>Returns a hash code for this <see cref="BoundingSphere"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="BoundingSphere"/>.</returns>
		public override int GetHashCode()
		{
			return Center.GetHashCode() ^ Radius.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="BoundingSphere"/> equals another <see cref="BoundingSphere"/>.</summary>
		/// <param name="other">A <see cref="BoundingSphere"/>.</param>
		/// <returns>Returns true if the bounding spheres are equal, otherwise returns false.</returns>
		public bool Equals( BoundingSphere other )
		{
			return ( Radius == other.Radius ) && Center.Equals( ref other.Center );
		}


		/// <summary>Returns a value indicating whether this <see cref="BoundingSphere"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="BoundingSphere"/> which equals this <see cref="BoundingSphere"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is BoundingSphere bs ) && this.Equals( bs );
		}



		/// <summary>Creates a <see cref="BoundingSphere"/> that contains the two specified bounding spheres.</summary>
		/// <param name="sphere">The <see cref="BoundingSphere"/> to be merged.</param>
		/// <param name="other">The other <see cref="BoundingSphere"/> to be merged.</param>
		/// <param name="result">Receives the resulting <see cref="BoundingSphere"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Merge( ref BoundingSphere sphere, ref BoundingSphere other, out BoundingSphere result )
		{
			Vector3.Subtract( ref other.Center, ref sphere.Center, out Vector3 value );

			var distance = value.Length;
			var radius = sphere.Radius;
			var radius2 = other.Radius;
			
			if( radius + radius2 >= distance )
			{
				if( radius - radius2 >= distance )
				{
					result = sphere;
					return;
				}

				if( radius2 - radius >= distance )
				{
					result = other;
					return;
				}
			}

			var direction = value * ( 1.0f / distance );
			var min = Math.Min( -radius, distance - radius2 );
			var max = Math.Max( radius, distance + radius2 );

			result.Radius = ( max - min ) * 0.5f;
			result.Center = sphere.Center + direction * ( result.Radius + min );
		}

		/// <summary>Returns a <see cref="BoundingSphere"/> containing the two specified bounding spheres.</summary>
		/// <param name="sphere">The <see cref="BoundingSphere"/> to be merged.</param>
		/// <param name="other">The other <see cref="BoundingSphere"/> to be merged.</param>
		/// <returns>Returns the resulting <see cref="BoundingSphere"/>.</returns>
		public static BoundingSphere Merge( BoundingSphere sphere, BoundingSphere other )
		{
			Vector3.Subtract( ref other.Center, ref sphere.Center, out Vector3 value );

			var distance = value.Length;
			var radius = sphere.Radius;
			var radius2 = other.Radius;

			if( radius + radius2 >= distance )
			{
				if( radius - radius2 >= distance )
					return sphere;

				if( radius2 - radius >= distance )
					return other;
			}

			var direction = value * ( 1.0f / distance );
			var min = Math.Min( -radius, distance - radius2 );
			var max = Math.Max( radius, distance + radius2 );

			sphere.Radius = ( max - min ) * 0.5f;
			sphere.Center = sphere.Center + direction * ( sphere.Radius + min );
			return sphere;
		}


		/// <summary>Returns the smallest <see cref="BoundingSphere"/> containing a list of points.</summary>
		/// <param name="points">The list of points the <see cref="BoundingSphere"/> must contain.</param>
		/// <returns>Returns a <see cref="BoundingSphere"/> which can contain the specified <paramref name="points"/>.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public static BoundingSphere CreateFromPoints( IEnumerable<Vector3> points )
		{
			if( points == null )
				throw new ArgumentNullException( "points" );
			
			var enumerator = points.GetEnumerator();
			if( !enumerator.MoveNext() )
				throw new ArgumentException( "No points defined." );
			
			Vector3 vector6, vector5, vector4, vector3, vector2, vector;
			vector = vector2 = ( vector3 = ( vector4 = ( vector5 = ( vector6 = enumerator.Current ) ) ) );
			
			foreach( var point in points )
			{
				if( point.X < vector2.X )
					vector2 = point;
				
				if( point.X > vector.X )
					vector = point;
				
				if( point.Y < vector3.Y )
					vector3 = point;
				
				if( point.Y > vector4.Y )
					vector4 = point;
				
				if( point.Z < vector5.Z )
					vector5 = point;
				
				if( point.Z > vector6.Z )
					vector6 = point;
			}

			Vector3.Distance( ref vector, ref vector2, out float distance1 );

			Vector3.Distance( ref vector4, ref vector3, out float distance2 );

			Vector3.Distance( ref vector6, ref vector5, out float distance3 );

			Vector3 position;
			float distance;
			if( distance1 > distance2 )
			{
				if( distance1 > distance3 )
				{
					Vector3.Lerp( ref vector, ref vector2, 0.5f, out position );
					distance = distance1 * 0.5f;
				}
				else
				{
					Vector3.Lerp( ref vector6, ref vector5, 0.5f, out position );
					distance = distance3 * 0.5f;
				}
			}
			else if( distance2 > distance3 )
			{
				Vector3.Lerp( ref vector4, ref vector3, 0.5f, out position );
				distance = distance2 * 0.5f;
			}
			else
			{
				Vector3.Lerp( ref vector6, ref vector5, 0.5f, out position );
				distance = distance3 * 0.5f;
			}
			
			foreach( var point in points )
			{
				Vector3 value;
				value.X = point.X - position.X;
				value.Y = point.Y - position.Y;
				value.Z = point.Z - position.Z;
				var len = value.Length;
				if( len > distance )
				{
					distance = ( distance + len ) * 0.5f;
					position += ( 1.0f - distance / len ) * value;
				}
			}
			
			BoundingSphere result;
			result.Center = position;
			result.Radius = distance;
			return result;
		}


		/// <summary>Creates the smallest <see cref="BoundingSphere"/> containing a specified <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="result">Receives the created <see cref="BoundingSphere"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateFromBoundingBox( ref BoundingBox box, out BoundingSphere result )
		{
			var boxMin = box.Min;
			var boxMax = box.Max;

			Vector3.Add( ref boxMin, ref boxMax, out Vector3 sum );

			Vector3.Multiply( ref sum, 0.5f, out result.Center );

			Vector3.Distance( ref boxMin, ref boxMax, out float distance );
			result.Radius = distance * 0.5f;
		}

		/// <summary>Returns the smallest <see cref="BoundingSphere"/> containing a specified <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <returns>Returns the created <see cref="BoundingSphere"/>.</returns>
		public static BoundingSphere CreateFromBoundingBox( BoundingBox box )
		{
			var boxMin = box.Min;
			var boxMax = box.Max;

			Vector3.Add( ref boxMin, ref boxMax, out Vector3 sum );

			BoundingSphere result;
			Vector3.Multiply( ref sum, 0.5f, out result.Center );

			Vector3.Distance( ref boxMin, ref boxMax, out float distance );
			result.Radius = distance * 0.5f;
			
			return result;
		}


		/// <summary>Creates the smallest <see cref="BoundingSphere"/> containing a specified <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A valid <see cref="BoundingFrustum"/>.</param>
		/// <param name="result">Receives the created <see cref="BoundingSphere"/>.</param>
		/// <exception cref="ArgumentException"/>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateFromBoundingFrustum( ref BoundingFrustum frustum, out BoundingSphere result )
		{
			var corners = frustum.corners;
			if( corners == null )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			result = CreateFromPoints( corners );
		}

		/// <summary>Returns the smallest <see cref="BoundingSphere"/> containing a specified <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A valid <see cref="BoundingFrustum"/>.</param>
		/// <returns>Returns the created <see cref="BoundingSphere"/>.</returns>
		/// <exception cref="ArgumentException"/>
		public static BoundingSphere CreateFromBoundingFrustum( BoundingFrustum frustum )
		{
			var corners = frustum.corners;
			if( corners == null )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			return CreateFromPoints( corners );
		}


		/// <summary></summary>
		public static readonly BoundingSphere Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <param name="other">A <see cref="BoundingSphere"/>.</param>
		/// <returns>Returns true if the spheres are equal, otherwise returns false.</returns>
		public static bool operator ==( BoundingSphere sphere, BoundingSphere other )
		{
			return ( sphere.Radius == other.Radius ) && sphere.Center.Equals( ref other.Center );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <param name="other">A <see cref="BoundingSphere"/>.</param>
		/// <returns>Returns true if the spheres are not equal, otherwise returns false.</returns>
		public static bool operator !=( BoundingSphere sphere, BoundingSphere other )
		{
			return ( sphere.Radius != other.Radius ) || !sphere.Center.Equals( ref other.Center );
		}

		#endregion Operators

	}

}