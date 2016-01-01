using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{

	/// <summary></summary>
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 24 )]
	public struct Viewport : IEquatable<Viewport>
	{

		/// <summary></summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public int X;
		
		/// <summary></summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly" )]
		public int Y;
		
		/// <summary></summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Width;
		
		/// <summary></summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Height;
		
		/// <summary></summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float MinDepth;
		
		/// <summary></summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float MaxDepth;


		/// <summary></summary>
		public float AspectRatio
		{
			get
			{
				if( Width == 0 || Height == 0 )
					return 0.0f;
				return (float)Width / (float)Height;
			}
		}


		/// <summary></summary>
		public Rect Bounds
		{
			get
			{
				Rect rect;
				rect.Left = X;
				rect.Top = Y;
				rect.Right = X + Width;
				rect.Bottom = Y + Height;
				return rect;
			}
			set
			{
				X = value.Left;
				Y = value.Top;
				Width = value.Right - value.Left;
				Height = value.Bottom - value.Top;
			}
		}


		/// <summary></summary>
		/// <param name="position"></param>
		/// <param name="worldViewProjection"></param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Project( ref Vector3 position, ref Matrix worldViewProjection, out Vector3 result )
		{
			Vector3 transformed;
			worldViewProjection.Transform( ref position, out transformed );

			var n = position.X * worldViewProjection.M14 + position.Y * worldViewProjection.M24 + position.Z * worldViewProjection.M34 + worldViewProjection.M44;
			if( n != 0.0f )
				Vector3.Divide( ref transformed, n, out transformed );

			result.X = ( transformed.X + 1.0f ) * 0.5f * (float)Width + (float)X;
			result.Y = ( -transformed.Y + 1.0f ) * 0.5f * (float)Height + (float)Y;
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
			Matrix worldViewProj;
			Matrix.Multiply( ref world, ref view, out worldViewProj );
			Matrix.Multiply( ref worldViewProj, ref projection, out worldViewProj );

			Vector3 transformed;
			this.Project( ref position, ref worldViewProj, out transformed );
			return transformed;
		}


		/// <summary></summary>
		/// <param name="position"></param>
		/// <param name="inverseWorldViewProjection"></param>
		/// <param name="result"></param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters" )]
		public void Unproject( ref Vector3 position, ref Matrix inverseWorldViewProjection, out Vector3 result )
		{
			position.X = ( position.X - (float)X ) / (float)Width * 2.0f - 1.0f;
			position.Y = -( ( position.Y - (float)Y ) / (float)Height * 2.0f - 1.0f );
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
		public Vector3 Unproject( Vector3 position, Matrix projection, Matrix view, Matrix world )
		{
			Matrix worldViewProj;
			Matrix.Multiply( ref world, ref view, out worldViewProj );
			Matrix.Multiply( ref worldViewProj, ref projection, out worldViewProj );

			Matrix invWorldViewProj;
			Matrix.Invert( ref worldViewProj, out invWorldViewProj );

			Vector3 result;
			this.Unproject( ref position, ref invWorldViewProj, out result );
			return result;
		}


		/// <summary>Returns a hash code for this <see cref="Viewport"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Viewport"/>.</returns>
		public override int GetHashCode()
		{
			return X ^ Y ^ Width ^ Height ^ MinDepth.GetHashCode() ^ MaxDepth.GetHashCode();
		}

		
		/// <summary>Returns a value indicating whether this <see cref="Viewport"/> equals another <see cref="Viewport"/>.</summary>
		/// <param name="other">A <see cref="Viewport"/>.</param>
		/// <returns>Returns true if this <see cref="Viewport"/> and the <paramref name="other"/> <see cref="Viewport"/> are equal, otherwise returns false.</returns>
		public bool Equals( Viewport other )
		{
			return ( X == other.X ) && ( Y == other.Y ) && ( Width == other.Width ) && ( Height == other.Height ) && ( MinDepth == other.MinDepth ) && ( MaxDepth == other.MaxDepth );
		}


		/// <summary>Returns a value indicating whether this <see cref="Viewport"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Viewport"/> which equals this <see cref="Viewport"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Viewport ) && this.Equals( (Viewport)obj );
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
			return ( viewport.X == other.X ) && ( viewport.Y == other.Y ) && ( viewport.Width == other.Width ) && ( viewport.Height == other.Height ) && ( viewport.MinDepth == other.MinDepth ) && ( viewport.MaxDepth == other.MaxDepth );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="viewport">A <see cref="Viewport"/>.</param>
		/// <param name="other">A <see cref="Viewport"/>.</param>
		/// <returns>Returns true if the viewports are not equal, otherwise returns false.</returns>
		public static bool operator !=( Viewport viewport, Viewport other )
		{
			return ( viewport.X != other.X ) || ( viewport.Y != other.Y ) || ( viewport.Width != other.Width ) || ( viewport.Height != other.Height ) || ( viewport.MinDepth != other.MinDepth ) || ( viewport.MaxDepth != other.MaxDepth );
		}

		#endregion Operators

	}

}