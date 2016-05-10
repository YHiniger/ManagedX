using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;


namespace ManagedX
{

	/// <summary>Defines a frustum and helps determine whether forms intersect with it.
	/// <para>This structure is equivalent to the native <code>BoundingFrustum</code> structure (defined in DirectXCollision.h).</para>
	/// </summary>
	[Win32.Native( "DirectXCollision.h" )]
	[Serializable]
	public struct BoundingFrustum : IEquatable<BoundingFrustum>
	{

		/// <summary>Specifies the total number of corners in the <see cref="BoundingFrustum"/>: 8.</summary>
		public const int CornerCount = 8;

		/// <summary>Defines the number of planes in the <see cref="BoundingFrustum"/>: 6.</summary>
		public const int PlaneCount = 6;


		private const float DefaultIntersectionThreshold = Ray.DefaultIntersectionThreshold;




		private Matrix matrix;
		internal Plane[] planes;
		internal Vector3[] corners;
		private GJK gjk;



		/// <summary>Creates a new instance of <see cref="BoundingFrustum"/>.</summary>
		/// <param name="value">Combined <see cref="Matrix"/> which usually takes view × projection matrix.</param>
		public BoundingFrustum( Matrix value )
		{
			matrix = value;
			planes = new Plane[ PlaneCount ];
			corners = new Vector3[ CornerCount ];
			gjk = new GJK();

			this.BuildPlanes();
		}



		#region Planes

		/// <summary>Gets a <see cref="Plane"/> of this <see cref="BoundingFrustum"/> given its index.</summary>
		/// <param name="index">The index of the requested <see cref="Plane"/>.</param>
		/// <returns>Returns the <see cref="Plane"/> associated with the specified <paramref name="index"/>.</returns>
		public Plane this[ PlaneIndex index ] { get { return planes[ (int)index ]; } }


		/// <summary>Gets the near plane of this <see cref="BoundingFrustum"/>.</summary>
		public Plane Near { get { return planes[ (int)PlaneIndex.Near ]; } }

		/// <summary>Gets the far plane of the <see cref="BoundingFrustum"/>.</summary>
		public Plane Far { get { return planes[ (int)PlaneIndex.Far ]; } }

		/// <summary>Gets the left plane of the <see cref="BoundingFrustum"/>.</summary>
		public Plane Left { get { return planes[ (int)PlaneIndex.Left ]; } }

		/// <summary>Gets the right plane of the <see cref="BoundingFrustum"/>.</summary>
		public Plane Right { get { return planes[ (int)PlaneIndex.Right ]; } }

		/// <summary>Gets the top plane of the <see cref="BoundingFrustum"/>.</summary>
		public Plane Top { get { return planes[ (int)PlaneIndex.Top ]; } }

		/// <summary>Gets the bottom plane of the <see cref="BoundingFrustum"/>.</summary>
		public Plane Bottom { get { return planes[ (int)PlaneIndex.Bottom ]; } }

		#endregion Planes


		/// <summary>Gets or sets the <see cref="Matrix"/> describing this <see cref="BoundingFrustum"/>.</summary>
		public Matrix Matrix
		{
			get { return matrix; }
			set
			{
				matrix = value;
				this.BuildPlanes();
			}
		}


		private void BuildPlanes()
		{
			planes[ 0 ].Normal.X = -matrix.M13;
			planes[ 0 ].Normal.Y = -matrix.M23;
			planes[ 0 ].Normal.Z = -matrix.M33;
			planes[ 0 ].Distance = -matrix.M43;

			planes[ 1 ].Normal.X = -matrix.M14 + matrix.M13;
			planes[ 1 ].Normal.Y = -matrix.M24 + matrix.M23;
			planes[ 1 ].Normal.Z = -matrix.M34 + matrix.M33;
			planes[ 1 ].Distance = -matrix.M44 + matrix.M43;

			planes[ 2 ].Normal.X = -matrix.M14 - matrix.M11;
			planes[ 2 ].Normal.Y = -matrix.M24 - matrix.M21;
			planes[ 2 ].Normal.Z = -matrix.M34 - matrix.M31;
			planes[ 2 ].Distance = -matrix.M44 - matrix.M41;

			planes[ 3 ].Normal.X = -matrix.M14 + matrix.M11;
			planes[ 3 ].Normal.Y = -matrix.M24 + matrix.M21;
			planes[ 3 ].Normal.Z = -matrix.M34 + matrix.M31;
			planes[ 3 ].Distance = -matrix.M44 + matrix.M41;

			planes[ 4 ].Normal.X = -matrix.M14 + matrix.M12;
			planes[ 4 ].Normal.Y = -matrix.M24 + matrix.M22;
			planes[ 4 ].Normal.Z = -matrix.M34 + matrix.M32;
			planes[ 4 ].Distance = -matrix.M44 + matrix.M42;

			planes[ 5 ].Normal.X = -matrix.M14 - matrix.M12;
			planes[ 5 ].Normal.Y = -matrix.M24 - matrix.M22;
			planes[ 5 ].Normal.Z = -matrix.M34 - matrix.M32;
			planes[ 5 ].Distance = -matrix.M44 - matrix.M42;

			for( var p = 0; p < PlaneCount; p++ )
				planes[ p ].Normalize();

			Ray ray;
			planes[ 0 ].CalculateIntersectionLine( ref planes[ 2 ], out ray );
			planes[ 4 ].CalculateIntersection( ref ray, out corners[ 0 ] );
			planes[ 5 ].CalculateIntersection( ref ray, out corners[ 3 ] );

			planes[ 3 ].CalculateIntersectionLine( ref planes[ 0 ], out ray );
			planes[ 4 ].CalculateIntersection( ref ray, out corners[ 1 ] );
			planes[ 5 ].CalculateIntersection( ref ray, out corners[ 2 ] );

			planes[ 2 ].CalculateIntersectionLine( ref planes[ 1 ], out ray );
			planes[ 4 ].CalculateIntersection( ref ray, out corners[ 4 ] );
			planes[ 5 ].CalculateIntersection( ref ray, out corners[ 7 ] );

			planes[ 1 ].CalculateIntersectionLine( ref planes[ 3 ], out ray );
			planes[ 4 ].CalculateIntersection( ref ray, out corners[ 5 ] );
			planes[ 5 ].CalculateIntersection( ref ray, out corners[ 6 ] );
		}


		/// <summary>Returns an array of points which make up the corners of this <see cref="BoundingFrustum"/>.</summary>
		/// <returns>Returns an array of points which make up the corners of this <see cref="BoundingFrustum"/>.</returns>
		/// <exception cref="InvalidOperationException"/>
		public Vector3[] GetCorners()
		{
			if( corners == null )
				throw new InvalidOperationException();

			return (Vector3[])corners.Clone();
		}


		/// <summary>Copies the corners of this <see cref="BoundingFrustum"/> to an array.</summary>
		/// <param name="destination">An array of at least 8 <see cref="Vector3"/> points where the corners of the <see cref="BoundingFrustum"/> are copied.</param>
		/// <param name="startIndex">The zero-based index the copy should start at.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		/// <exception cref="InvalidOperationException"/>
		public void GetCorners( Vector3[] destination, int startIndex )
		{
			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( destination.Length < CornerCount )
				throw new ArgumentException( "Not enough corners.", "destination" );

			if( startIndex < 0 || startIndex + CornerCount > destination.Length )
				throw new ArgumentOutOfRangeException( "startIndex" );

			if( corners == null )
				throw new InvalidOperationException();

			corners.CopyTo( destination, startIndex );
		}


		/// <summary>Returns a hash code for this <see cref="BoundingFrustum"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="BoundingFrustum"/>.</returns>
		public override int GetHashCode()
		{
			return matrix.GetHashCode();
		}


		/// <summary>Determines whether the specified <see cref="BoundingFrustum"/> is equal to the current <see cref="BoundingFrustum"/>.</summary>
		/// <param name="other">The <see cref="BoundingFrustum"/> to compare with the current <see cref="BoundingFrustum"/>.</param>
		public bool Equals( BoundingFrustum other )
		{
			return matrix.Equals( ref other.matrix );
		}


		/// <summary>Determines whether the specified Object is equal to the <see cref="BoundingFrustum"/>.</summary>
		/// <param name="obj">The Object to compare with the current <see cref="BoundingFrustum"/>.</param>
		public override bool Equals( object obj )
		{
			return ( obj is BoundingFrustum ) && this.Equals( (BoundingFrustum)obj );
		}


		/// <summary>Returns a string representing this <see cref="BoundingFrustum"/>.</summary>
		/// <returns>Returns a string representing this <see cref="BoundingFrustum"/>.</returns>
		public override string ToString()
		{
			return string.Format( CultureInfo.InvariantCulture, "{{Near: {0} Far: {1} Left: {2} Right: {3} Top: {4} Bottom: {5}}}", Near, Far, Left, Right, Top, Bottom );
		}


		#region Intersects

		///// <summary>Checks whether the current <see cref="BoundingFrustum"/> intersects a Ray.</summary>
		///// <param name="ray">The Ray to check for intersection with.</param>
		///// <param name="result">[OutAttribute] Distance at which the ray intersects the <see cref="BoundingFrustum"/> or null if there is no intersection.</param>
		//[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		//[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		//public void Intersects( ref Ray ray, out float result )
		//{
		//	ray.Intersects( ref this, out result );
		//}

		///// <summary>Checks whether the current <see cref="BoundingFrustum"/> intersects the specified Ray.</summary>
		///// <param name="ray">The Ray to check for intersection.</param>
		//public float Intersects( Ray ray )
		//{
		//	float result;
		//	ray.Intersects( ref this, out result );
		//	return result;
		//}


		///// <summary>Checks whether the current <see cref="BoundingFrustum"/> intersects a Plane.</summary>
		///// <param name="plane">The Plane to check for intersection with.</param>
		///// <param name="result">[OutAttribute] An enumeration indicating whether the <see cref="BoundingFrustum"/> intersects the Plane.</param>
		//[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		//[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#" )]
		//public void Intersects( ref Plane plane, out PlaneIntersectionType result )
		//{
		//	plane.Intersects( ref this, out result );
		//}

		///// <summary>Checks whether the current <see cref="BoundingFrustum"/> intersects the specified Plane.</summary>
		///// <param name="plane">The Plane to check for intersection.</param>
		//public PlaneIntersectionType Intersects( Plane plane )
		//{
		//	PlaneIntersectionType result;
		//	plane.Intersects( ref this, out result );
		//	return result;
		//}


		/// <summary>Determines whether this <see cref="BoundingFrustum"/> intersects a <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="result">Receives true if this <see cref="BoundingFrustum"/> and the <see cref="BoundingBox"/> intersect, false otherwise.</param>
		/// <exception cref="InvalidOperationException"/>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref BoundingBox box, out bool result )
		{
			if( corners == null )
				throw new InvalidOperationException( "Invalid bounding frustum." );

			gjk.Reset();

			Vector3 closestPoint;
			Vector3.Subtract( ref this.corners[ 0 ], ref box.Min, out closestPoint );
			if( closestPoint.LengthSquared < DefaultIntersectionThreshold )
				Vector3.Subtract( ref this.corners[ 0 ], ref box.Max, out closestPoint );

			var num = float.MaxValue;
			result = false;

			while( true )
			{
				var vector = -closestPoint;

				Vector3 vector2;
				this.SupportMapping( ref vector, out vector2 );

				Vector3 vector3;
				box.SupportMapping( ref closestPoint, out vector3 );

				Vector3 vector4;
				Vector3.Subtract( ref vector2, ref vector3, out vector4 );
				var num2 = closestPoint.X * vector4.X + closestPoint.Y * vector4.Y + closestPoint.Z * vector4.Z;
				if( num2 > 0.0f )
					return;

				gjk.AddSupportPoint( ref vector4 );
				closestPoint = gjk.ClosestPoint;

				float num3 = num;
				num = closestPoint.LengthSquared;
				if( num3 - num <= DefaultIntersectionThreshold * num3 )
					return;

				var num4 = GJK.Scale * gjk.MaxLengthSquared;
				if( gjk.FullSimplex || num < num4 )
				{
					result = true;
					return;
				}
			}
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingFrustum"/> intersects a <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <returns>Receives true if this <see cref="BoundingFrustum"/> and the <see cref="BoundingBox"/> intersect, otherwise returns false.</returns>
		/// <exception cref="InvalidOperationException"/>
		public bool Intersects( BoundingBox box )
		{
			if( corners == null )
				throw new InvalidOperationException( "Invalid bounding frustum." );

			gjk.Reset();

			Vector3 closestPoint;
			Vector3.Subtract( ref this.corners[ 0 ], ref box.Min, out closestPoint );
			if( closestPoint.LengthSquared < DefaultIntersectionThreshold )
				Vector3.Subtract( ref this.corners[ 0 ], ref box.Max, out closestPoint );

			var num = float.MaxValue;

			while( true )
			{
				var vector = -closestPoint;

				Vector3 vector2;
				this.SupportMapping( ref vector, out vector2 );

				Vector3 vector3;
				box.SupportMapping( ref closestPoint, out vector3 );

				Vector3 vector4;
				Vector3.Subtract( ref vector2, ref vector3, out vector4 );
				var num2 = closestPoint.X * vector4.X + closestPoint.Y * vector4.Y + closestPoint.Z * vector4.Z;
				if( num2 > 0.0f )
					return false;

				gjk.AddSupportPoint( ref vector4 );
				closestPoint = gjk.ClosestPoint;

				var num3 = num;
				num = closestPoint.LengthSquared;
				if( num3 - num <= DefaultIntersectionThreshold * num3 )
					return false;

				var num4 = GJK.Scale * gjk.MaxLengthSquared;
				if( gjk.FullSimplex || num < num4 )
					return true;
			}
		}


		/// <summary>Determines whether this <see cref="BoundingFrustum"/> intersects another <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		/// <param name="result">Receives true if this <see cref="BoundingFrustum"/> intersects the specified <see cref="BoundingFrustum"/>, false otherwise.</param>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="InvalidOperationException"/>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref BoundingFrustum frustum, out bool result )
		{
			var frustumCorners = frustum.corners;
			if( frustumCorners == null )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			if( corners == null )
				throw new InvalidOperationException( "Invalid bounding frustum." );

			gjk.Reset();

			Vector3 closestPoint;
			Vector3.Subtract( ref corners[ 0 ], ref frustumCorners[ 0 ], out closestPoint );
			if( closestPoint.LengthSquared < DefaultIntersectionThreshold )
				Vector3.Subtract( ref corners[ 0 ], ref frustumCorners[ 1 ], out closestPoint );

			var num = float.MaxValue;
			result = false;
			while( true )
			{
				var vector = -closestPoint;

				Vector3 vector2;
				this.SupportMapping( ref vector, out vector2 );

				Vector3 vector3;
				frustum.SupportMapping( ref closestPoint, out vector3 );

				Vector3 vector4;
				Vector3.Subtract( ref vector2, ref vector3, out vector4 );

				var dot = closestPoint.X * vector4.X + closestPoint.Y * vector4.Y + closestPoint.Z * vector4.Z;
				if( dot > 0.0f )
					return;

				gjk.AddSupportPoint( ref vector4 );
				closestPoint = gjk.ClosestPoint;

				var num3 = num;
				num = closestPoint.LengthSquared;

				var num4 = GJK.Scale * gjk.MaxLengthSquared;
				if( num3 - num <= DefaultIntersectionThreshold * num3 )
					return;

				if( gjk.FullSimplex || num < num4 )
					result = true;
			}
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingFrustum"/> intersects another <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		/// <returns>Returns true if this <see cref="BoundingFrustum"/> intersects the specified <see cref="BoundingFrustum"/>, otherwise returns false.</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="InvalidOperationException"/>
		public bool Intersects( BoundingFrustum frustum )
		{
			if( frustum.corners == null )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			if( corners == null )
				throw new InvalidOperationException( "Invalid bounding frustum." );

			gjk.Reset();

			Vector3 closestPoint;
			Vector3.Subtract( ref corners[ 0 ], ref frustum.corners[ 0 ], out closestPoint );
			if( closestPoint.LengthSquared < DefaultIntersectionThreshold )
				Vector3.Subtract( ref corners[ 0 ], ref frustum.corners[ 1 ], out closestPoint );

			var num = float.MaxValue;
			while( true )
			{
				Vector3 vector;
				vector.X = -closestPoint.X;
				vector.Y = -closestPoint.Y;
				vector.Z = -closestPoint.Z;

				Vector3 vector2;
				this.SupportMapping( ref vector, out vector2 );

				Vector3 vector3;
				frustum.SupportMapping( ref closestPoint, out vector3 );

				Vector3 vector4;
				Vector3.Subtract( ref vector2, ref vector3, out vector4 );

				var num2 = closestPoint.X * vector4.X + closestPoint.Y * vector4.Y + closestPoint.Z * vector4.Z;
				if( num2 > 0.0f )
					return false;

				gjk.AddSupportPoint( ref vector4 );
				closestPoint = gjk.ClosestPoint;

				var num3 = num;
				num = closestPoint.LengthSquared;

				var num4 = GJK.Scale * gjk.MaxLengthSquared;
				if( num3 - num <= DefaultIntersectionThreshold * num3 )
					return false;

				if( gjk.FullSimplex || num < num4 )
					return true;
			}
		}


		/// <summary>Determines whether this <see cref="BoundingFrustum"/> intersects a <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <param name="result">Receives true if this <see cref="BoundingFrustum"/> and <see cref="BoundingSphere"/> intersect, false otherwise.</param>
		/// <exception cref="InvalidOperationException"/>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref BoundingSphere sphere, out bool result )
		{
			if( corners == null )
				throw new InvalidOperationException( "Invalid bounding frustum." );

			gjk.Reset();

			Vector3 vector;
			Vector3.Subtract( ref corners[ 0 ], ref sphere.Center, out vector );

			if( vector.LengthSquared < DefaultIntersectionThreshold )
				vector = Vector3.UnitX;

			var num = float.MaxValue;
			result = false;

			while( true )
			{
				var vector2 = -vector;

				Vector3 vector3;
				this.SupportMapping( ref vector2, out vector3 );

				Vector3 vector4;
				sphere.SupportMapping( ref vector, out vector4 );

				Vector3 vector5;
				Vector3.Subtract( ref vector3, ref vector4, out vector5 );

				var dot = vector.X * vector5.X + vector.Y * vector5.Y + vector.Z * vector5.Z;
				if( dot > 0.0f )
					return;

				gjk.AddSupportPoint( ref vector5 );
				vector = gjk.ClosestPoint;

				var num3 = num;
				num = vector.LengthSquared;
				if( num3 - num <= DefaultIntersectionThreshold * num3 )
					return;

				var num4 = GJK.Scale * gjk.MaxLengthSquared;
				if( gjk.FullSimplex || num < num4 )
				{
					result = true;
					return;
				}
			}
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingFrustum"/> intersects a <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <returns>Returns true if this <see cref="BoundingFrustum"/> and <see cref="BoundingSphere"/> intersect, otherwise returns false.</returns>
		/// <exception cref="InvalidOperationException"/>
		public bool Intersects( BoundingSphere sphere )
		{
			if( corners == null )
				throw new InvalidOperationException( "Invalid bounding frustum." );

			gjk.Reset();

			Vector3 vector;
			Vector3.Subtract( ref corners[ 0 ], ref sphere.Center, out vector );

			if( vector.LengthSquared < DefaultIntersectionThreshold )
				vector = Vector3.UnitX;

			var num = float.MaxValue;

			while( true )
			{
				var vector2 = -vector;

				Vector3 vector3;
				this.SupportMapping( ref vector2, out vector3 );

				Vector3 vector4;
				sphere.SupportMapping( ref vector, out vector4 );

				Vector3 vector5;
				Vector3.Subtract( ref vector3, ref vector4, out vector5 );

				var dot = vector.X * vector5.X + vector.Y * vector5.Y + vector.Z * vector5.Z;
				if( dot > 0.0f )
					return false;

				gjk.AddSupportPoint( ref vector5 );
				vector = gjk.ClosestPoint;

				var num3 = num;
				num = vector.LengthSquared;
				if( num3 - num <= DefaultIntersectionThreshold * num3 )
					return false;

				var num4 = GJK.Scale * gjk.MaxLengthSquared;
				if( gjk.FullSimplex || num < num4 )
					return true;
			}
		}

		#endregion Intersects


		#region Contains

		/// <summary>Returns a value indicating whether this <see cref="BoundingFrustum"/> contains another <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		/// <param name="result">Receives a value indicating whether this <see cref="BoundingFrustum"/> contains the specified <paramref name="frustum"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Contains( ref BoundingFrustum frustum, out ContainmentType result )
		{
			var otherCorners = frustum.corners;
			if( otherCorners == null )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			if( !this.Intersects( frustum ) )
			{
				result = ContainmentType.Disjoint;
				return;
			}

			for( var c = 0; c < CornerCount; c++ )
			{
				this.Contains( ref otherCorners[ c ], out result );
				if( result == ContainmentType.Disjoint )
				{
					result = ContainmentType.Intersects;
					return;
				}
			}

			result = ContainmentType.Contains;
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingFrustum"/> contains another <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		/// <returns>Returns a value indicating whether this <see cref="BoundingFrustum"/> contains the specified <paramref name="frustum"/>.</returns>
		public ContainmentType Contains( BoundingFrustum frustum )
		{
			var otherCorners = frustum.corners;
			if( otherCorners == null )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			if( !this.Intersects( frustum ) )
				return ContainmentType.Disjoint;

			ContainmentType result;
			for( var c = 0; c < CornerCount; c++ )
			{
				this.Contains( ref otherCorners[ c ], out result );
				if( result == ContainmentType.Disjoint )
					return ContainmentType.Intersects;
			}

			return ContainmentType.Contains;
		}


		/// <summary>Checks whether the current <see cref="BoundingFrustum"/> contains the specified point.</summary>
		/// <param name="point">The point to test for overlap.</param>
		/// <param name="result">[OutAttribute] Enumeration indicating the extent of overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Contains( ref Vector3 point, out ContainmentType result )
		{
			Plane plane;
			float dot;
			for( var i = 0; i < PlaneCount; i++ )
			{
				plane = planes[ i ];
				dot = plane.Normal.X * point.X + plane.Normal.Y * point.Y + plane.Normal.Z * point.Z + plane.Distance;  // * point.W (where point.W = 1.0f)
				if( dot > DefaultIntersectionThreshold )
				{
					result = ContainmentType.Disjoint;
					return;
				}
			}
			result = ContainmentType.Contains;
		}

		/// <summary>Checks whether the current <see cref="BoundingFrustum"/> contains the specified point.</summary>
		/// <param name="point">The point to check against the current <see cref="BoundingFrustum"/>.</param>
		public ContainmentType Contains( Vector3 point )
		{
			Plane plane;
			float dot;
			for( var i = 0; i < PlaneCount; i++ )
			{
				plane = planes[ i ];
				dot = plane.Normal.X * point.X + plane.Normal.Y * point.Y + plane.Normal.Z * point.Z + plane.Distance;
				if( dot > DefaultIntersectionThreshold )
					return ContainmentType.Disjoint;
			}
			return ContainmentType.Contains;
		}


		/// <summary>Determines whether this <see cref="BoundingFrustum"/> contains a <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="result">Receives a value indicating the extent of overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Contains( ref BoundingBox box, out ContainmentType result )
		{
			var intersecting = false;

			for( var i = 0; i < PlaneCount; i++ )
			{
				PlaneIntersectionType planeIntersectionType;
				planes[ i ].Intersects( ref box, out planeIntersectionType );

				if( planeIntersectionType == PlaneIntersectionType.Front )
				{
					result = ContainmentType.Disjoint;
					return;
				}

				if( planeIntersectionType == PlaneIntersectionType.Intersecting )
					intersecting = true;
			}

			result = intersecting ? ContainmentType.Intersects : ContainmentType.Contains;
		}

		/// <summary>Checks whether this <see cref="BoundingFrustum"/> contains the specified <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <returns>Returns a value indicating the extent of overlap.</returns>
		public ContainmentType Contains( BoundingBox box )
		{
			var intersects = false;
			for( var i = 0; i < PlaneCount; i++ )
			{
				PlaneIntersectionType planeIntersectionType;
				planes[ i ].Intersects( ref box, out planeIntersectionType );

				if( planeIntersectionType == PlaneIntersectionType.Front )
					return ContainmentType.Disjoint;

				if( planeIntersectionType == PlaneIntersectionType.Intersecting )
					intersects = true;
			}

			return intersects ? ContainmentType.Intersects : ContainmentType.Contains;
		}


		/// <summary>Determines whether this <see cref="BoundingFrustum"/> contains a <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <param name="result">Receives a value indicating the extent of overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Contains( ref BoundingSphere sphere, out ContainmentType result )
		{
			var center = sphere.Center;
			var radius = sphere.Radius;
			var count = 0;

			Plane plane;
			float PdotC;

			for( var p = 0; p < planes.Length; p++ )
			{
				plane = planes[ p ];
				PdotC = plane.Normal.X * center.X + plane.Normal.Y * center.Y + plane.Normal.Z * center.Z + plane.Distance;

				if( PdotC > radius )
				{
					result = ContainmentType.Disjoint;
					return;
				}

				if( PdotC < -radius )
					count++;
			}

			result = ( count == 6 ) ? ContainmentType.Contains : ContainmentType.Intersects;
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingFrustum"/> contains a <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <returns></returns>
		public ContainmentType Contains( BoundingSphere sphere )
		{
			var center = sphere.Center;
			var radius = sphere.Radius;
			var count = 0;

			Plane plane;
			float PdotC;

			for( var p = 0; p < planes.Length; p++ )
			{
				plane = planes[ p ];
				PdotC = plane.Normal.X * center.X + plane.Normal.Y * center.Y + plane.Normal.Z * center.Z + plane.Distance;

				if( PdotC > radius )
					return ContainmentType.Disjoint;

				if( PdotC < -radius )
					count++;
			}

			return ( count == 6 ) ? ContainmentType.Contains : ContainmentType.Intersects;
		}

		#endregion Contains


		internal void SupportMapping( ref Vector3 vector, out Vector3 result )
		{
			var maxPdotV = float.MinValue;
			var index = 0;

			float PdotV;
			for( var i = 0; i < CornerCount; i++ )
			{
				Vector3.Dot( ref corners[ i ], ref vector, out PdotV );
				if( PdotV > maxPdotV )
				{
					index = i;
					maxPdotV = PdotV;
				}
			}

			result = corners[ index ];
		}


		/// <summary>The empty (and invalid) <see cref="BoundingFrustum"/>.</summary>
		public static readonly BoundingFrustum Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="boundingFrustum">A <see cref="BoundingFrustum"/>.</param>
		/// <param name="other">A <see cref="BoundingFrustum"/>.</param>
		/// <returns>Returns true if the <see cref="BoundingFrustum"/> instances are equal, otherwise returns false.</returns>
		public static bool operator ==( BoundingFrustum boundingFrustum, BoundingFrustum other )
		{
			return boundingFrustum.matrix.Equals( ref other.matrix );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="boundingFrustum">A <see cref="BoundingFrustum"/>.</param>
		/// <param name="other">A <see cref="BoundingFrustum"/>.</param>
		/// <returns>Returns true if the <see cref="BoundingFrustum"/> instances are not equal, otherwise returns false.</returns>
		public static bool operator !=( BoundingFrustum boundingFrustum, BoundingFrustum other )
		{
			return !boundingFrustum.matrix.Equals( ref other.matrix );
		}

		#endregion Operators

	}

}