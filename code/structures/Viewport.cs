using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
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
		/// <param name="world"></param>
		/// <param name="view"></param>
		/// <param name="projection"></param>
		/// <returns></returns>
		public Vector3 Project( Vector3 position, Matrix world, Matrix view, Matrix projection )
		{
			Matrix worldViewProj;
			Matrix.Multiply( ref world, ref view, out worldViewProj );
			Matrix.Multiply( ref worldViewProj, ref projection, out worldViewProj );

			Vector3 transformed;
			worldViewProj.Transform( ref position, out transformed );
			
			var n = position.X * worldViewProj.M14 + position.Y * worldViewProj.M24 + position.Z * worldViewProj.M34 + worldViewProj.M44;
			if( Math.Abs( n ) > float.Epsilon )
				Vector3.Divide( ref transformed, n, out transformed );

			transformed.X = ( transformed.X + 1.0f ) * 0.5f * (float)Width + (float)X;
			transformed.Y = ( -transformed.Y + 1.0f ) * 0.5f * (float)Height + (float)Y;
			transformed.Z = transformed.Z * ( MaxDepth - MinDepth ) + MinDepth;

			return transformed;
		}


		/// <summary></summary>
		/// <param name="position"></param>
		/// <param name="world"></param>
		/// <param name="view"></param>
		/// <param name="projection"></param>
		/// <returns></returns>
		public Vector3 Unproject( Vector3 position, Matrix world, Matrix view, Matrix projection )
		{
			Matrix worldViewProj;
			Matrix.Multiply( ref world, ref view, out worldViewProj );
			Matrix.Multiply( ref worldViewProj, ref projection, out worldViewProj );

			Matrix invWorldViewProj;
			Matrix.Invert( ref worldViewProj, out invWorldViewProj );

			position.X = ( position.X - (float)X ) / (float)Width * 2.0f - 1.0f;
			position.Y = -( ( position.Y - (float)Y ) / (float)Height * 2.0f - 1.0f );
			position.Z = ( position.Z - MinDepth ) / ( MaxDepth - MinDepth );
			
			Vector3 transformed;
			worldViewProj.Transform( ref position, out transformed );

			var n = position.X * worldViewProj.M14 + position.Y * worldViewProj.M24 + position.Z * worldViewProj.M34 + worldViewProj.M44;
			if( Math.Abs( n ) > float.Epsilon )
				Vector3.Divide( ref transformed, n, out transformed );
			return transformed;
		}


		/// <summary></summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return X ^ Y ^ Width ^ Height ^ MinDepth.GetHashCode() ^ MaxDepth.GetHashCode();
		}

		
		/// <summary></summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals( Viewport other )
		{
			return ( X == other.X ) && ( Y == other.Y ) && ( Width == other.Width ) && ( Height == other.Height ) && ( MinDepth == other.MinDepth ) && ( MaxDepth == other.MaxDepth );
		}


		/// <summary></summary>
		/// <param name="obj"></param>
		/// <returns></returns>
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