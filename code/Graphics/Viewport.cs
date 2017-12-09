using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{
	using Win32;


	/// <summary>Defines the dimensions of a viewport.
	/// <para>This structure is equivalent to the native <code>D3D11_VIEWPORT</code> structure (defined in D3D11.h),
	/// and its alias <code>D3D12_VIEWPORT</code> (defined in D3D12.h).
	/// </para>
	/// </summary>
	/// <remarks>
	/// https://msdn.microsoft.com/en-us/library/windows/desktop/ff476260%28v=vs.85%29.aspx
	/// </remarks>
	[Source( "D3D11.h", "D3D11_VIEWPORT" )]
	[Source( "D3D12.h", "D3D12_VIEWPORT" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 24 )]
	public struct Viewport : IEquatable<Viewport>
	{

		/// <summary></summary>
		public const float MinBounds10 = -16384.0f;

		/// <summary></summary>
		public const float MaxBounds10 = +16383.0f;


		/// <summary></summary>
		public const float MinBounds = -32768.0f;

		/// <summary></summary>
		public const float MaxBounds = +32767.0f;



		/// <summary>X position of the left hand side of the viewport.
		/// Ranges between D3D11_VIEWPORT_BOUNDS_MIN and D3D11_VIEWPORT_BOUNDS_MAX.
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float TopLeftX;

		/// <summary>Y position of the top of the viewport.
		/// Ranges between D3D11_VIEWPORT_BOUNDS_MIN and D3D11_VIEWPORT_BOUNDS_MAX.
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float TopLeftY;

		/// <summary>Width of the viewport; must be greater than or equal to zero.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float Width;

		/// <summary>Height of the viewport; must be greater than or equal to zero.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float Height;

		/// <summary>Minimum depth of the viewport, within the range [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float MinDepth;

		/// <summary>Maximum depth of the viewport, within the ranges [0,1].</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float MaxDepth;


		/// <summary></summary>
		public float AspectRatio
		{
			get
			{
				if( Width == 0 || Height == 0 )
					return 0.0f; // FIXME ? the "DXGI rule" used in the Rational structure should also apply here: return 1.0f;
				return (float)Width / (float)Height;
			}
		}


		/// <summary></summary>
		public Rect Bounds
		{
			get
			{
				var x = (int)TopLeftX;
				var y = (int)TopLeftY;
				Rect rect;
				rect.Left = x;
				rect.Top = y;
				rect.Right = x + (int)Width;
				rect.Bottom = y + (int)Height;
				return rect;
			}
			set
			{
				TopLeftX = (float)Math.Min( value.Left, value.Right );
				TopLeftY = (float)Math.Min( value.Top, value.Bottom );
				Width = (float)Math.Abs( value.Right - value.Left );
				Height = (float)Math.Abs( value.Bottom - value.Top );
			}
		}


		/// <summary></summary>
		/// <param name="position"></param>
		/// <param name="worldViewProjection"></param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "worldView" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Project( ref Vector3 position, ref Matrix worldViewProjection, out Vector3 result )
		{
			worldViewProjection.Transform( ref position, out Vector3 transformed );

			var n = position.X * worldViewProjection.M14 + position.Y * worldViewProjection.M24 + position.Z * worldViewProjection.M34 + worldViewProjection.M44;
			if( n != 0.0f )
				Vector3.Divide( ref transformed, n, out transformed );

			result.X = ( transformed.X + 1.0f ) * 0.5f * (float)Width + (float)TopLeftX;
			result.Y = ( -transformed.Y + 1.0f ) * 0.5f * (float)Height + (float)TopLeftY;
			result.Z = transformed.Z * ( MaxDepth - MinDepth ) + MinDepth;
		}

		/// <summary></summary>
		/// <param name="position"></param>
		/// <param name="projection"></param>
		/// <param name="view"></param>
		/// <param name="world"></param>
		/// <returns></returns>
		public Vector3 Project( Vector3 position, Matrix projection, Matrix view, Matrix world )
		{
			Matrix.Multiply( ref world, ref view, out Matrix worldViewProj );
			Matrix.Multiply( ref worldViewProj, ref projection, out worldViewProj );

			this.Project( ref position, ref worldViewProj, out Vector3 transformed );
			return transformed;
		}


		/// <summary></summary>
		/// <param name="position"></param>
		/// <param name="inverseWorldViewProjection"></param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unproject" )]
		[SuppressMessage( "Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "WorldView" )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Unproject( ref Vector3 position, ref Matrix inverseWorldViewProjection, out Vector3 result )
		{
			position.X = ( position.X - TopLeftX ) / Width * 2.0f - 1.0f;
			position.Y = -( ( position.Y - TopLeftY ) / Height * 2.0f - 1.0f );
			position.Z = ( position.Z - MinDepth ) / ( MaxDepth - MinDepth );

			inverseWorldViewProjection.Transform( ref position, out result );

			var n = position.X * inverseWorldViewProjection.M14 + position.Y * inverseWorldViewProjection.M24 + position.Z * inverseWorldViewProjection.M34 + inverseWorldViewProjection.M44;
			if( n != 0.0f )
				Vector3.Divide( ref result, n, out result );
		}

		/// <summary></summary>
		/// <param name="position"></param>
		/// <param name="projection"></param>
		/// <param name="view"></param>
		/// <param name="world"></param>
		/// <returns></returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unproject" )]
		public Vector3 Unproject( Vector3 position, Matrix projection, Matrix view, Matrix world )
		{
			Matrix.Multiply( ref world, ref view, out Matrix worldViewProj );
			Matrix.Multiply( ref worldViewProj, ref projection, out worldViewProj );

			Matrix.Invert( ref worldViewProj, out Matrix invWorldViewProj );

			this.Unproject( ref position, ref invWorldViewProj, out Vector3 result );
			return result;
		}


		/// <summary>Returns a hash code for this <see cref="Viewport"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Viewport"/>.</returns>
		public override int GetHashCode()
		{
			return TopLeftX.GetHashCode() ^ TopLeftY.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode() ^ MinDepth.GetHashCode() ^ MaxDepth.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Viewport"/> equals another <see cref="Viewport"/>.</summary>
		/// <param name="other">A <see cref="Viewport"/>.</param>
		/// <returns>Returns true if this <see cref="Viewport"/> and the <paramref name="other"/> <see cref="Viewport"/> are equal, otherwise returns false.</returns>
		public bool Equals( Viewport other )
		{
			return ( TopLeftX == other.TopLeftX ) && ( TopLeftY == other.TopLeftY ) && ( Width == other.Width ) && ( Height == other.Height ) && ( MinDepth == other.MinDepth ) && ( MaxDepth == other.MaxDepth );
		}


		/// <summary>Returns a value indicating whether this <see cref="Viewport"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Viewport"/> which equals this <see cref="Viewport"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is Viewport v && this.Equals( v );
		}


		/// <summary>The empty (and invalid) <see cref="Viewport"/>.</summary>
		public static readonly Viewport Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="viewport">A <see cref="Viewport"/>.</param>
		/// <param name="other">A <see cref="Viewport"/>.</param>
		/// <returns>Returns true if the viewports are equal, otherwise returns false.</returns>
		public static bool operator ==( Viewport viewport, Viewport other )
		{
			return ( viewport.TopLeftX == other.TopLeftX ) && ( viewport.TopLeftY == other.TopLeftY ) && ( viewport.Width == other.Width ) && ( viewport.Height == other.Height ) && ( viewport.MinDepth == other.MinDepth ) && ( viewport.MaxDepth == other.MaxDepth );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="viewport">A <see cref="Viewport"/>.</param>
		/// <param name="other">A <see cref="Viewport"/>.</param>
		/// <returns>Returns true if the viewports are not equal, otherwise returns false.</returns>
		public static bool operator !=( Viewport viewport, Viewport other )
		{
			return ( viewport.TopLeftX != other.TopLeftX ) || ( viewport.TopLeftY != other.TopLeftY ) || ( viewport.Width != other.Width ) || ( viewport.Height != other.Height ) || ( viewport.MinDepth != other.MinDepth ) || ( viewport.MaxDepth != other.MaxDepth );
		}

		#endregion Operators

	}

}