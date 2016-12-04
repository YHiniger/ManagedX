using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX
{

	/// <summary>A bounding box.
	/// <para>This structure is equivalent to the native <code>BoundingBox</code> structure (defined in DirectXCollision.h).</para>
	/// </summary>
	[Win32.Source( "DirectXCollision.h" )]
	[Serializable]
	public struct BoundingBox : IEquatable<BoundingBox>
	{

		/// <summary>Defines the number of corners of a <see cref="BoundingBox"/>.</summary>
		public const int CornerCount = 8;


		/// <summary>The minimum point the <see cref="BoundingBox"/> contains.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Vector3 Min;

		/// <summary>The maximum point the <see cref="BoundingBox"/> contains.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Vector3 Max;



		/// <summary>Initializes a new <see cref="BoundingBox"/>.</summary>
		/// <param name="min">The minimum point the bounding box contains.</param>
		/// <param name="max">The maximum point the bounding box contains.</param>
		public BoundingBox( Vector3 min, Vector3 max )
		{
			Vector3.Min( ref min, ref max, out Min );
			Vector3.Max( ref min, ref max, out Max );
		}



		/// <summary>Gets the center of this <see cref="BoundingBox"/>.</summary>
		public Vector3 Center
		{
			get
			{
				Vector3 result;
				Vector3.Add( ref Min, ref Max, out result );
				Vector3.Multiply( ref result, 0.5f, out result );
				return result;
			}
		}


		#region Contains

		/// <summary>Determines whether this <see cref="BoundingBox"/> contains a point.</summary>
		/// <param name="point">A <see cref="Vector3"/>.</param>
		/// <param name="result">Receives a value indicating the extent of overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Contains( ref Vector3 point, out ContainmentType result )
		{
			if( ( point.X > Min.X && point.X < Max.X ) && ( point.Y > Min.Y && point.Y < Max.Y ) && ( point.Z > Min.Z && point.Z < Max.Z ) )
				result = ContainmentType.Contains;
			else if( ( point.X == Min.X || point.X == Max.X ) && ( point.Y == Min.Y || point.Y == Max.Y ) && ( point.Z == Min.Z || point.Z == Max.Z ) )
				result = ContainmentType.Intersects;
			else
				result = ContainmentType.Disjoint;
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingBox"/> contains a point.</summary>
		/// <param name="point">A <see cref="Vector3"/>.</param>
		/// <returns>Returns a value indicating the extent of overlap.</returns>
		public ContainmentType Contains( Vector3 point )
		{
			if( ( point.X > Min.X && point.X < Max.X ) && ( point.Y > Min.Y && point.Y < Max.Y ) && ( point.Z > Min.Z && point.Z < Max.Z ) )
				return ContainmentType.Contains;

			if( ( point.X == Min.X || point.X == Max.X ) && ( point.Y == Min.Y || point.Y == Max.Y ) && ( point.Z == Min.Z || point.Z == Max.Z ) )
				return ContainmentType.Intersects;
			
			return ContainmentType.Disjoint;
		}


		/// <summary>Determines whether this <see cref="BoundingBox"/> contains another <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="result">Receives a value indicating the extent of overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Contains( ref BoundingBox box, out ContainmentType result )
		{
			var boxMin = box.Min;
			var boxMax = box.Max;

			if( ( boxMin.X > Max.X || boxMax.X < Min.X ) ||
				( boxMin.Y > Max.Y || boxMax.Y < Min.Y ) ||
				( boxMin.Z > Max.Z || boxMax.Z < Min.Z ) )
			{
				result = ContainmentType.Disjoint;
				return;
			}

			if( ( boxMin.X > Min.X && boxMax.X < Max.X ) && 
				( boxMin.Y > Min.Y && boxMax.Y < Max.Y ) && 
				( boxMin.Z > Min.Z && boxMax.Z < Max.Z ) )
			{
				result = ContainmentType.Contains;
				return;
			}
			
			result = ContainmentType.Intersects;
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingBox"/> contains another <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <returns>Returns a value indicating the extent of overlap.</returns>
		public ContainmentType Contains( BoundingBox box )
		{
			var boxMin = box.Min;
			var boxMax = box.Max;

			if( ( boxMin.X > Max.X || boxMax.X < Min.X ) ||
				( boxMin.Y > Max.Y || boxMax.Y < Min.Y ) ||
				( boxMin.Z > Max.Z || boxMax.Z < Min.Z ) )
				return ContainmentType.Disjoint;

			if( ( boxMin.X > Min.X && boxMax.X < Max.X ) &&
				( boxMin.Y > Min.Y && boxMax.Y < Max.Y ) &&
				( boxMin.Z > Min.Z && boxMax.Z < Max.Z ) )
				return ContainmentType.Contains;

			return ContainmentType.Intersects;
		}


		/// <summary>Determines whether this <see cref="BoundingBox"/> contains a <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <param name="result">Receives a value indicating the extent of overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Contains( ref BoundingSphere sphere, out ContainmentType result )
		{
			var sphereCenter = sphere.Center;
			var radius = sphere.Radius;

			Vector3 clamped;
			Vector3.Clamp( ref sphereCenter, ref Min, ref Max, out clamped );
			
			float distanceSquared;
			Vector3.DistanceSquared( ref sphereCenter, ref clamped, out distanceSquared );


			if( distanceSquared > radius * radius )
			{
				result = ContainmentType.Disjoint;
				return;
			}

			if( ( sphereCenter.X >= Min.X + radius && sphereCenter.X <= Max.X - radius && radius < Max.X - Min.X ) &&
				( sphereCenter.Y >= Min.Y + radius && sphereCenter.Y <= Max.Y - radius && radius < Max.Y - Min.Y ) &&
				( sphereCenter.Z >= Min.Z + radius && sphereCenter.Z <= Max.Z - radius && radius < Max.Z - Min.Z ) )
				result = ContainmentType.Contains;
			else
				result = ContainmentType.Intersects;
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingBox"/> contains a <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <returns>Returns a value indicating the extent of overlap.</returns>
		public ContainmentType Contains( BoundingSphere sphere )
		{
			var sphereCenter = sphere.Center;

			Vector3 clamped;
			Vector3.Clamp( ref sphereCenter, ref Min, ref Max, out clamped );

			float distanceSquared;
			Vector3.DistanceSquared( ref sphereCenter, ref clamped, out distanceSquared );

			var radius = sphere.Radius;

			if( distanceSquared > radius * radius )
				return ContainmentType.Disjoint;

			if( ( sphereCenter.X >= Min.X + radius && sphereCenter.X <= Max.X - radius && radius < Max.X - Min.X ) &&
				( sphereCenter.Y >= Min.Y + radius && sphereCenter.Y <= Max.Y - radius && radius < Max.Y - Min.Y ) && 
				( sphereCenter.Z >= Min.Z + radius && sphereCenter.Z <= Max.Z - radius && radius < Max.Z - Min.Z ) )
				return ContainmentType.Contains;
			
			return ContainmentType.Intersects;
		}


		/// <summary>Determines whether this <see cref="BoundingBox"/> contains a <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		/// <param name="result">Receives a value indicating the extent of overlap.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Contains( ref BoundingFrustum frustum, out ContainmentType result )
		{
			var cornerArray = frustum.corners;
			if( cornerArray == null )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			if( !frustum.Intersects( this ) )
			{
				result = ContainmentType.Disjoint;
				return;
			}

			ContainmentType containmentType;
			for( var c = 0; c < BoundingFrustum.CornerCount; c++ )
			{
				this.Contains( ref cornerArray[ c ], out containmentType );
				if( containmentType == ContainmentType.Disjoint )
				{
					result = ContainmentType.Intersects;
					return;
				}
			}
			
			result = ContainmentType.Contains;
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingBox"/> contains a <see cref="BoundingFrustum"/>.</summary>
		/// <param name="frustum">A <see cref="BoundingFrustum"/>.</param>
		/// <returns>Returns a value indicating the extent of overlap.</returns>
		public ContainmentType Contains( BoundingFrustum frustum )
		{
			var cornerArray = frustum.corners;
			if( cornerArray == null )
				throw new ArgumentException( "Invalid bounding frustum.", "frustum" );

			if( !frustum.Intersects( this ) )
				return ContainmentType.Disjoint;

			ContainmentType containmentType;
			for( var c = 0; c < BoundingFrustum.CornerCount; c++ )
			{
				this.Contains( ref cornerArray[ c ], out containmentType );
				if( containmentType == ContainmentType.Disjoint )
					return ContainmentType.Intersects;
			}
			
			return ContainmentType.Contains;
		}

		#endregion Contains


		#region Intersects

		/// <summary>Determines whether this <see cref="BoundingBox"/> intersects another <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="result">Receives true if this <see cref="BoundingBox"/> and the other <see cref="BoundingBox"/> intersect, false otherwise.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref BoundingBox box, out bool result )
		{
			//unsafe
			//{
			//	fixed( Vector3* boxMin = &box.Min, boxMax = &box.Max )
			//	{
			//		result = (
			//			( boxMin->X <= Max.X && boxMax->X >= Min.X ) &&
			//			( boxMin->Y <= Max.Y && boxMax->Y >= Min.Y ) &&
			//			( boxMin->Z <= Max.Z && boxMax->Z >= Min.Z ) );
			//	}
			//}

			var boxMin = box.Min;
			var boxMax = box.Max;

			result = (
				( boxMin.X <= Max.X && boxMax.X >= Min.X ) &&
				( boxMin.Y <= Max.Y && boxMax.Y >= Min.Y ) &&
				( boxMin.Z <= Max.Z && boxMax.Z >= Min.Z ) );
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingBox"/> intersects another <see cref="BoundingBox"/>.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <returns>Returns true if this <see cref="BoundingBox"/> and the other <see cref="BoundingBox"/> intersect, otherwise returns false.</returns>
		public bool Intersects( BoundingBox box )
		{
			return 
				( box.Min.X <= Max.X && box.Max.X >= Min.X ) &&
				( box.Min.Y <= Max.Y && box.Max.Y >= Min.Y ) &&
				( box.Min.Z <= Max.Z && box.Max.Z >= Min.Z );
		}


		/// <summary>Determines whether this <see cref="BoundingBox"/> intersects a <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <param name="result">Receives true if this <see cref="BoundingBox"/> and the <see cref="BoundingSphere"/> intersect, false otherwise.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Intersects( ref BoundingSphere sphere, out bool result )
		{
			var sphereCenter = sphere.Center;
			var sphereRadius = sphere.Radius;

			Vector3 clamped;
			Vector3.Clamp( ref sphereCenter, ref Min, ref Max, out clamped );
			
			float distanceSquared;
			Vector3.DistanceSquared( ref sphereCenter, ref clamped, out distanceSquared );
			
			result = ( distanceSquared <= sphereRadius * sphereRadius );
		}

		/// <summary>Returns a value indicating whether this <see cref="BoundingBox"/> intersects a <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">A <see cref="BoundingSphere"/>.</param>
		/// <returns>Returns true if this <see cref="BoundingBox"/> and the <see cref="BoundingSphere"/> intersect, otherwise returns false.</returns>
		public bool Intersects( BoundingSphere sphere )
		{
			Vector3 clamped;
			Vector3.Clamp( ref sphere.Center, ref Min, ref Max, out clamped );

			float distanceSquared;
			Vector3.DistanceSquared( ref sphere.Center, ref clamped, out distanceSquared );

			return ( distanceSquared <= sphere.Radius * sphere.Radius );
		}


		///// <summary>Determines whether this <see cref="BoundingBox"/> intersects a <see cref="BoundingFrustum"/>.</summary>
		///// <param name="frustum">A valid <see cref="BoundingFrustum"/>.</param>
		///// <param name="result">Receives true if this <see cref="BoundingBox"/> and the <see cref="BoundingFrustum"/> intersect, false otherwise.</param>
		//[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		//[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		//public void Intersects( ref BoundingFrustum frustum, out bool result )
		//{
		//	frustum.Intersects( ref this, out result );
		//}

		///// <summary>Returns a value indicating whether this <see cref="BoundingBox"/> intersects a <see cref="BoundingFrustum"/>.</summary>
		///// <param name="frustum">A valid <see cref="BoundingFrustum"/>.</param>
		///// <returns>Returns true if this <see cref="BoundingBox"/> and the <see cref="BoundingFrustum"/> intersect, otherwise returns false.</returns>
		//public bool Intersects( BoundingFrustum frustum )
		//{
		//	bool result;
		//	frustum.Intersects( ref this, out result );
		//	return result;
		//}


		// Required by BoundingFrustum/GJK
		internal void SupportMapping( ref Vector3 vector, out Vector3 result )
		{
			result.X = ( vector.X >= 0.0f ) ? Max.X : Min.X;
			result.Y = ( vector.Y >= 0.0f ) ? Max.Y : Min.Y;
			result.Z = ( vector.Z >= 0.0f ) ? Max.Z : Min.Z;
		}

		#endregion Intersects


		/// <summary>Returns an array containing the corners of this <see cref="BoundingBox"/>.</summary>
		/// <returns>Returns an array containing the corners of this <see cref="BoundingBox"/>.</returns>
		public Vector3[] GetCorners()
		{
			return new Vector3[]
			{
				new Vector3( this.Min.X, this.Max.Y, this.Max.Z ),
				new Vector3( this.Max.X, this.Max.Y, this.Max.Z ),
				new Vector3( this.Max.X, this.Min.Y, this.Max.Z ),
				new Vector3( this.Min.X, this.Min.Y, this.Max.Z ),
				new Vector3( this.Min.X, this.Max.Y, this.Min.Z ),
				new Vector3( this.Max.X, this.Max.Y, this.Min.Z ),
				new Vector3( this.Max.X, this.Min.Y, this.Min.Z ),
				new Vector3( this.Min.X, this.Min.Y, this.Min.Z )
			};
		}

		/// <summary>Copies the corners of this <see cref="BoundingBox"/> to an array.</summary>
		/// <param name="destination">An array of <see cref="Vector3"/> which receives the corners; must not be null, and must contain at least <paramref name="startIndex"/> + 8 elements.</param>
		/// <param name="startIndex">The zero-based index the copy should start at; must be zero or greater.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void GetCorners( Vector3[] destination, int startIndex )
		{
			if( destination == null )
				throw new ArgumentNullException( "destination" );

			if( destination.Length < CornerCount )
				throw new ArgumentException( "Not enough corners", "destination" );

			if( startIndex < 0 || startIndex + CornerCount > destination.Length )
				throw new ArgumentOutOfRangeException( "startIndex" );

			destination[ startIndex + 0 ] = new Vector3( this.Min.X, this.Max.Y, this.Max.Z );
			destination[ startIndex + 1 ] = new Vector3( this.Max.X, this.Max.Y, this.Max.Z );
			destination[ startIndex + 2 ] = new Vector3( this.Max.X, this.Min.Y, this.Max.Z );
			destination[ startIndex + 3 ] = new Vector3( this.Min.X, this.Min.Y, this.Max.Z );
			destination[ startIndex + 4 ] = new Vector3( this.Min.X, this.Max.Y, this.Min.Z );
			destination[ startIndex + 5 ] = new Vector3( this.Max.X, this.Max.Y, this.Min.Z );
			destination[ startIndex + 6 ] = new Vector3( this.Max.X, this.Min.Y, this.Min.Z );
			destination[ startIndex + 7 ] = new Vector3( this.Min.X, this.Min.Y, this.Min.Z );
		}


		/// <summary>Returns a hash code for this <see cref="BoundingBox"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="BoundingBox"/>.</returns>
		public override int GetHashCode()
		{
			return Min.GetHashCode() ^ Max.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="BoundingBox"/> equals another bounding box.</summary>
		/// <param name="other">A <see cref="BoundingBox"/>.</param>
		/// <returns>Returns true if the bounding boxes are equal, otherwise returns false.</returns>
		public bool Equals( BoundingBox other )
		{
			return Min.Equals( ref other.Min ) && Max.Equals( ref other.Max );
		}


		/// <summary>Returns a value indicating whether this <see cref="BoundingBox"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="BoundingBox"/> which equals this bounding box, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is BoundingBox ) && this.Equals( (BoundingBox)obj );
		}


		/// <summary>Returns a string representing this <see cref="BoundingBox"/>.</summary>
		/// <returns>Returns a string representing this <see cref="BoundingBox"/>.</returns>
		public override string ToString()
		{
			return "{Min: " + Min.ToString() + ", Max: " + Max.ToString() + '}';
		}



		/// <summary>The empty <see cref="BoundingBox"/>.</summary>
		public static readonly BoundingBox Empty;


		/// <summary>Creates the smallest <see cref="BoundingBox"/> that will contain a group of points.</summary>
		/// <param name="points">A list of points the <see cref="BoundingBox"/> should contain.</param>
		/// <returns></returns>
		public static BoundingBox CreateFromPoints( IEnumerable<Vector3> points )
		{
			if( points == null )
				throw new ArgumentNullException( "points" );

			var valid = false;
			var min = new Vector3( float.MaxValue );
			var max = new Vector3( float.MinValue );
			foreach( var current in points )
			{
				var vector = current;
				Vector3.Min( ref min, ref vector, out min );
				Vector3.Max( ref max, ref vector, out max );
				valid = true;
			}

			if( !valid )
				throw new ArgumentException( "No points defined.", "points" );

			return new BoundingBox( min, max );
		}


		/// <summary>Creates the smallest <see cref="BoundingBox"/> that will contain the specified <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">The <see cref="BoundingSphere"/> to contain.</param>
		/// <param name="result">Receives the created <see cref="BoundingBox"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void CreateFromSphere( ref BoundingSphere sphere, out BoundingBox result )
		{
			var center = sphere.Center;
			var radius = sphere.Radius;

			result.Min = new Vector3(
				center.X - radius,
				center.Y - radius,
				center.Z - radius
			);
			
			result.Max = new Vector3(
				center.X + radius,
				center.Y + radius,
				center.Z + radius
			);
		}

		/// <summary>Creates the smallest <see cref="BoundingBox"/> that will contain the specified <see cref="BoundingSphere"/>.</summary>
		/// <param name="sphere">The <see cref="BoundingSphere"/> to contain.</param>
		public static BoundingBox CreateFromSphere( BoundingSphere sphere )
		{
			var center = sphere.Center;
			BoundingBox result;
			result.Min.X = center.X - sphere.Radius;
			result.Min.Y = center.Y - sphere.Radius;
			result.Min.Z = center.Z - sphere.Radius;
			result.Max.X = center.X + sphere.Radius;
			result.Max.Y = center.Y + sphere.Radius;
			result.Max.Z = center.Z + sphere.Radius;
			return result;
		}


		/// <summary>Creates the smallest <see cref="BoundingBox"/> containing the two specified bounding boxes.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="other">Another <see cref="BoundingBox"/>.</param>
		/// <param name="result">Receives the created <see cref="BoundingBox"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public static void Merge( ref BoundingBox box, ref BoundingBox other, out BoundingBox result )
		{
			Vector3.Min( ref box.Min, ref other.Min, out result.Min );
			Vector3.Max( ref box.Max, ref other.Max, out result.Max );
		}

		/// <summary>Returns the smallest <see cref="BoundingBox"/> containing the two specified bounding boxes.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="other">Another <see cref="BoundingBox"/>.</param>
		/// <returns>Returns the created <see cref="BoundingBox"/>.</returns>
		public static BoundingBox Merge( BoundingBox box, BoundingBox other )
		{
			BoundingBox result;
			Vector3.Min( ref box.Min, ref other.Min, out result.Min );
			Vector3.Max( ref box.Max, ref other.Max, out result.Max );
			return result;
		}


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="other">A <see cref="BoundingBox"/>.</param>
		/// <returns>Returns true if the bounding boxes are equal, otherwise returns false.</returns>
		public static bool operator ==( BoundingBox box, BoundingBox other )
		{
			return box.Min.Equals( ref other.Min ) && box.Max.Equals( ref other.Max );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="box">A <see cref="BoundingBox"/>.</param>
		/// <param name="other">A <see cref="BoundingBox"/>.</param>
		/// <returns>Returns true if the bounding boxes are not equal, otherwise returns false.</returns>
		public static bool operator !=( BoundingBox box, BoundingBox other )
		{
			return !( box.Min.Equals( ref other.Min ) && box.Max.Equals( ref other.Max ) );
		}

		#endregion Operators

	}

}