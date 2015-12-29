using System;


namespace ManagedX
{

	/// <summary>Implementation of the Gilbert–Johnson–Keerthi distance algorithm.
	/// <para>Used to speed-up <see cref="BoundingFrustum"/> collision detection.</para>
	/// </summary>
	[Serializable]
	internal sealed class GJK
	{

		internal const float Scale = 4E-05f;

		
		private static int[] BitsToIndices = new int[]
		{
			0,
			1,
			2,
			17,
			3,
			25,
			26,
			209,
			4,
			33,
			34,
			273,
			35,
			281,
			282,
			2257
		};


		private Vector3[] y;
		private float[] yLengthSquared;
		private Vector3[][] edges;
		private float[][] edgeLengthSquared;
		private float[][] determinants;
		private int simplexBits;
		private float maxLengthSquared;
		private Vector3 closestPoint;



		internal GJK()
		{
			y = new Vector3[ 4 ];
			yLengthSquared = new float[ 4 ];

			edges = new Vector3[ 4 ][];
			edgeLengthSquared = new float[ 4 ][];
			for( var i = 0; i < 4; i++ )
			{
				edges[ i ] = new Vector3[ 4 ];
				edgeLengthSquared[ i ] = new float[ 4 ];
			}

			determinants = new float[ 16 ][];
			for( var i = 0; i < 16; i++ )
				determinants[ i ] = new float[ 4 ];
		}



		internal void Reset()
		{
			simplexBits = 0;
			maxLengthSquared = 0f;
		}


		internal bool FullSimplex { get { return simplexBits == 15; } }

		internal float MaxLengthSquared { get { return maxLengthSquared; } }

		internal Vector3 ClosestPoint { get { return closestPoint; } }


		internal bool AddSupportPoint( ref Vector3 newPoint )
		{
			var index = ( BitsToIndices[ simplexBits ^ 15 ] & 7 ) - 1;
			
			y[ index ] = newPoint;
			yLengthSquared[ index ] = newPoint.LengthSquared;
			
			Vector3 vector;
			int index2;
			float lengthSquared;
			for( var i = BitsToIndices[ simplexBits ]; i != 0; i >>= 3 )
			{
				index2 = ( i & 7 ) - 1;
				
				Vector3.Subtract( ref y[ index2 ], ref newPoint, out vector );
				
				edges[ index2 ][ index ] = vector;
				edges[ index ][ index2 ] = -vector;

				lengthSquared = vector.LengthSquared;
				edgeLengthSquared[ index ][ index2 ] = lengthSquared;
				edgeLengthSquared[ index2 ][ index ] = lengthSquared;
			}

			this.UpdateDeterminant( index );
			
			return this.UpdateSimplex( index );
		}


		private static float Dot( ref Vector3 a, ref Vector3 b )
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}
		
		private void UpdateDeterminant( int xmIdx )
		{
			var bit = 1 << xmIdx;
			determinants[ bit ][ xmIdx ] = 1.0f;
			
			var index = BitsToIndices[ simplexBits ];
			var index2 = index;
			int count = 0;
			int edgeIndex;
			while( index2 != 0 )
			{
				var yBits1 = ( index2 & 7 ) - 1;
				var xBit1 = 1 << yBits1;
				var xBits1 = xBit1 | bit;
				Vector3.Dot( ref edges[ xmIdx ][ yBits1 ], ref y[ xmIdx ], out determinants[ xBits1 ][ yBits1 ] );
				Vector3.Dot( ref edges[ yBits1 ][ xmIdx ], ref y[ yBits1 ], out determinants[ xBits1 ][ xmIdx ] );
				
				var index3 = index;
				for( var i = 0; i < count; i++ )
				{
					var yBits2 = ( index3 & 7 ) - 1;
					var xBit2 = 1 << yBits2;
					var xBits2 = xBits1 | xBit2;
					
					edgeIndex = ( edgeLengthSquared[ yBits1 ][ yBits2 ] < edgeLengthSquared[ xmIdx ][ yBits2 ] ) ? yBits1 : xmIdx;
					determinants[ xBits2 ][ yBits2 ] = determinants[ xBits1 ][ yBits1 ] * Dot( ref edges[ edgeIndex ][ yBits2 ], ref y[ yBits1 ] ) + determinants[ xBits1 ][ xmIdx ] * Dot( ref edges[ edgeIndex ][ yBits2 ], ref y[ xmIdx ] );
					
					edgeIndex = ( edgeLengthSquared[ yBits2 ][ yBits1 ] < edgeLengthSquared[ xmIdx ][ yBits1 ] ) ? yBits2 : xmIdx;
					determinants[ xBits2 ][ yBits1 ] = determinants[ xBit2 | bit ][ yBits2 ] * Dot( ref edges[ edgeIndex ][ yBits1 ], ref y[ yBits2 ] ) + determinants[ xBit2 | bit ][ xmIdx ] * Dot( ref edges[ edgeIndex ][ yBits1 ], ref y[ xmIdx ] );
					
					edgeIndex = ( edgeLengthSquared[ yBits1 ][ xmIdx ] < edgeLengthSquared[ yBits2 ][ xmIdx ] ) ? yBits1 : yBits2;
					determinants[ xBits2 ][ xmIdx ] = determinants[ xBit1 | xBit2 ][ yBits2 ] * Dot( ref edges[ edgeIndex ][ xmIdx ], ref y[ yBits2 ] ) + determinants[ xBit1 | xBit2 ][ yBits1 ] * Dot( ref edges[ edgeIndex ][ xmIdx ], ref y[ yBits1 ] );
					
					index3 >>= 3;
				}
				
				index2 >>= 3;
				count++;
			}

			if( ( simplexBits | bit ) == 15 )
			{
				edgeIndex = 3;
				if( edgeLengthSquared[ 1 ][ 0 ] < edgeLengthSquared[ 2 ][ 0 ] )
				{
					if( edgeLengthSquared[ 1 ][ 0 ] < edgeLengthSquared[ 3 ][ 0 ] )
						edgeIndex = 1;
				}
				else if( edgeLengthSquared[ 2 ][ 0 ] < edgeLengthSquared[ 3 ][ 0 ] )
					edgeIndex = 2;
				var d = determinants[ 14 ];
				determinants[ 15 ][ 0 ] = d[ 1 ] * Dot( ref edges[ edgeIndex ][ 0 ], ref y[ 1 ] ) + d[ 2 ] * Dot( ref edges[ edgeIndex ][ 0 ], ref y[ 2 ] ) + d[ 3 ] * Dot( ref edges[ edgeIndex ][ 0 ], ref y[ 3 ] );

				edgeIndex = 3;
				if( edgeLengthSquared[ 0 ][ 1 ] < edgeLengthSquared[ 2 ][ 1 ] )
				{
					if( edgeLengthSquared[ 0 ][ 1 ] < edgeLengthSquared[ 3 ][ 1 ] )
						edgeIndex = 0;
				}
				else if( edgeLengthSquared[ 2 ][ 1 ] < edgeLengthSquared[ 3 ][ 1 ] )
					edgeIndex = 2;
				d = determinants[ 13 ];
				determinants[ 15 ][ 1 ] = d[ 0 ] * Dot( ref edges[ edgeIndex ][ 1 ], ref y[ 0 ] ) + d[ 2 ] * Dot( ref edges[ edgeIndex ][ 1 ], ref y[ 2 ] ) + d[ 3 ] * Dot( ref edges[ edgeIndex ][ 1 ], ref y[ 3 ] );

				edgeIndex = 3;
				if( edgeLengthSquared[ 0 ][ 2 ] < edgeLengthSquared[ 1 ][ 2 ] ) 
				{
					if( edgeLengthSquared[ 0 ][ 2 ] < edgeLengthSquared[ 3 ][ 2 ] )
						edgeIndex = 0;
				}
				else if( edgeLengthSquared[ 1 ][ 2 ] < edgeLengthSquared[ 3 ][ 2 ] )
					edgeIndex = 1;
				d = determinants[ 11 ];
				determinants[ 15 ][ 2 ] = d[ 0 ] * Dot( ref edges[ edgeIndex ][ 2 ], ref y[ 0 ] ) + d[ 1 ] * Dot( ref edges[ edgeIndex ][ 2 ], ref y[ 1 ] ) + determinants[ 11 ][ 3 ] * Dot( ref edges[ edgeIndex ][ 2 ], ref y[ 3 ] );

				edgeIndex = 2;
				if( edgeLengthSquared[ 0 ][ 3 ] < edgeLengthSquared[ 1 ][ 3 ] )
				{
					if( edgeLengthSquared[ 0 ][ 3 ] < edgeLengthSquared[ 2 ][ 3 ] )
						edgeIndex = 0;
				}
				else if ( edgeLengthSquared[ 1 ][ 3 ] < edgeLengthSquared[ 2 ][ 3 ] )
					edgeIndex = 1;
				d = determinants[ 7 ];
				determinants[ 15 ][ 3 ] = d[ 0 ] * Dot( ref edges[ edgeIndex ][ 3 ], ref y[ 0 ] ) + d[ 1 ] * Dot( ref edges[ edgeIndex ][ 3 ], ref y[ 1 ] ) + d[ 2 ] * Dot( ref edges[ edgeIndex ][ 3 ], ref y[ 2 ] );
			}
		}

		
		private bool SatisfiesRule( int xBits, int yBits )
		{
			int index, mask;
			for( var i = BitsToIndices[ yBits ]; i != 0; i >>= 3 )
			{
				index = ( i & 7 ) - 1;
				mask = 1 << index;
				if( ( mask & xBits ) != 0 )
				{
					if( determinants[ xBits ][ index ] <= 0.0f )
						return false;
				}
				else if( determinants[ xBits | mask ][ index ] > 0.0f )
					return false;
			}
			return true;
		}

		private Vector3 CalculateClosestPoint()
		{
			float n = 0.0f;
			var vector = Vector3.Zero;
			
			Vector3 v;
			int index;
			float d;
			maxLengthSquared = 0.0f;
			for( var i = BitsToIndices[ simplexBits ]; i != 0; i >>= 3 )
			{
				index = ( i & 7 ) - 1;
				d = determinants[ simplexBits ][ index ];
				
				n += d;
				
				Vector3.Multiply( ref y[ index ], d, out v );
				Vector3.Add( ref vector, ref v, out vector );

				maxLengthSquared = Math.Max( maxLengthSquared, yLengthSquared[ index ] );
			}

			return vector / n;
		}

		private bool UpdateSimplex( int newIndex )
		{
			var yBits = simplexBits | 1 << newIndex;
			var bits = 1 << newIndex;
			for( var bit = simplexBits; bit != 0; bit-- )
			{
				if( ( bit & yBits ) == bit && this.SatisfiesRule( bit | bits, yBits ) )
				{
					simplexBits = ( bit | bits );
					closestPoint = this.CalculateClosestPoint();
					return true;
				}
			}
			
			if( this.SatisfiesRule( bits, yBits ) )
			{
				simplexBits = bits;
				closestPoint = y[ newIndex ];
				maxLengthSquared = yLengthSquared[ newIndex ];
				return true;
			}
			
			return false;
		}

	}

}