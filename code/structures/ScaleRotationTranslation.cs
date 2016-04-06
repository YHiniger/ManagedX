using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{

	/// <summary>Contains information about the scale, rotation and translation (SRT) transformations.</summary>
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 40 )]
	public struct ScaleRotationTranslation : IEquatable<ScaleRotationTranslation>
	{

		/// <summary>The scale part of the transformation.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Vector3 Scale;
		
		/// <summary>The rotation part of the transformation.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Quaternion Rotation;
		
		/// <summary>The translation part of the transformation.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Vector3 Translation;



		/// <summary>Initializes a new <see cref="ScaleRotationTranslation"/>.</summary>
		/// <param name="scale">The scale part of the transformation.</param>
		/// <param name="rotation">The rotation part of the transformation.</param>
		/// <param name="translation">The translation part of the transformation.</param>
		public ScaleRotationTranslation( Vector3 scale, Quaternion rotation, Vector3 translation )
		{
			Scale = scale;
			Rotation = rotation;
			Translation = translation;
		}



		/// <summary>Returns a <see cref="Matrix"/> corresponding to this <see cref="ScaleRotationTranslation"/> structure.</summary>
		/// <returns>Returns a <see cref="Matrix"/> corresponding to this <see cref="ScaleRotationTranslation"/> structure.</returns>
		public Matrix ToMatrix()
		{
			Matrix result;
			Matrix.CreateFromScaleRotationTranslation( ref Scale, ref Rotation, ref Translation, out result );
			return result;
		}


		/// <summary>Returns a hash code for this <see cref="ScaleRotationTranslation"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="ScaleRotationTranslation"/>.</returns>
		public override int GetHashCode()
		{
			return Scale.GetHashCode() ^ Rotation.GetHashCode() ^ Translation.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="ScaleRotationTranslation"/> equals another <see cref="ScaleRotationTranslation"/>.</summary>
		/// <param name="other">A <see cref="ScaleRotationTranslation"/>.</param>
		/// <returns>Returns true if the <see cref="ScaleRotationTranslation"/> transformations are equal, otherwise returns false.</returns>
		public bool Equals( ScaleRotationTranslation other )
		{
			return Scale.Equals( ref other.Scale ) && Rotation.Equals( ref other.Rotation ) && Translation.Equals( ref other.Translation );
		}


		/// <summary>Returns a value indicating whether this <see cref="ScaleRotationTranslation"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="ScaleRotationTranslation"/> which equals this <see cref="ScaleRotationTranslation"/>.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is ScaleRotationTranslation ) && this.Equals( (ScaleRotationTranslation)obj );
		}


		/// <summary>Returns a string representing this <see cref="ScaleRotationTranslation"/>.</summary>
		/// <returns>Returns a string representing this <see cref="ScaleRotationTranslation"/>.</returns>
		public override string ToString()
		{
			return "{Scale: " + Scale.ToString() + ", Rotation: " + Rotation.ToString() + ", Translation: " + Translation.ToString() + '}';
		}

		
		/// <summary>The identity <see cref="ScaleRotationTranslation"/>.</summary>
		public static readonly ScaleRotationTranslation Identity = new ScaleRotationTranslation( Vector3.One, Quaternion.Identity, Vector3.Zero );


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="transform">A <see cref="ScaleRotationTranslation"/>.</param>
		/// <param name="other">A <see cref="ScaleRotationTranslation"/>.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( ScaleRotationTranslation transform, ScaleRotationTranslation other )
		{
			return transform.Scale.Equals( ref other.Scale ) && transform.Rotation.Equals( ref other.Rotation ) && transform.Translation.Equals( ref other.Translation );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="transform">A <see cref="ScaleRotationTranslation"/>.</param>
		/// <param name="other">A <see cref="ScaleRotationTranslation"/>.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( ScaleRotationTranslation transform, ScaleRotationTranslation other )
		{
			return !( transform.Scale.Equals( ref other.Scale ) && transform.Rotation.Equals( ref other.Rotation ) && transform.Translation.Equals( ref other.Translation ) );
		}

		#endregion Operators

	}

}