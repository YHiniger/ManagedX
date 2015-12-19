using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{

	/// <summary>Contains transformation information: scale, rotation and translation (also known as "SRT").</summary>
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 40 )]
	public struct ScaleRotationTranslation : IEquatable<ScaleRotationTranslation>
	{

		/// <summary>The scale.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Vector3 Scale;
		
		/// <summary>The rotation.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Quaternion Rotation;
		
		/// <summary>The translation.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Vector3 Translation;



		/// <summary>Initializes a new <see cref="ScaleRotationTranslation"/>.</summary>
		/// <param name="scale">The scale.</param>
		/// <param name="rotation">The rotation.</param>
		/// <param name="translation">The translation.</param>
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
			Matrix scale, rotation, translation;
			Matrix scaleRotation, scaleRotationTranslation;

			Matrix.CreateScale( ref Scale, out scale );
			Matrix.CreateFromQuaternion( ref Rotation, out rotation );
			Matrix.CreateTranslation( ref Translation, out translation );
			Matrix.Multiply( ref scale, ref rotation, out scaleRotation );
			Matrix.Multiply( ref scaleRotation, ref translation, out scaleRotationTranslation );
			return scaleRotationTranslation;
		}


		/// <summary>Returns a hash code for this <see cref="ScaleRotationTranslation"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="ScaleRotationTranslation"/>.</returns>
		public override int GetHashCode()
		{
			return Scale.GetHashCode() ^ Rotation.GetHashCode() ^ Translation.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="ScaleRotationTranslation"/> equals another <see cref="ScaleRotationTranslation"/>.</summary>
		/// <param name="other">A <see cref="ScaleRotationTranslation"/>.</param>
		/// <returns>Returns true if the <see cref="ScaleRotationTranslation"/> structures are equal, otherwise returns false.</returns>
		public bool Equals( ScaleRotationTranslation other )
		{
			return Scale.Equals( ref other.Scale ) && Rotation.Equals( ref other.Rotation ) && Translation.Equals( ref other.Translation );
		}


		/// <summary>Returns a value indicating whether this <see cref="ScaleRotationTranslation"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="ScaleRotationTranslation"/> structure which equals this <see cref="ScaleRotationTranslation"/>.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is ScaleRotationTranslation ) && this.Equals( (ScaleRotationTranslation)obj );
		}


		
		/// <summary>The identity <see cref="ScaleRotationTranslation"/>.</summary>
		public static readonly ScaleRotationTranslation Identity = new ScaleRotationTranslation( Vector3.One, Quaternion.Identity, Vector3.Zero );


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="srt">A <see cref="ScaleRotationTranslation"/>.</param>
		/// <param name="other">A <see cref="ScaleRotationTranslation"/>.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( ScaleRotationTranslation srt, ScaleRotationTranslation other )
		{
			return srt.Scale.Equals( ref other.Scale ) && srt.Rotation.Equals( ref other.Rotation ) && srt.Translation.Equals( ref other.Translation );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="srt">A <see cref="ScaleRotationTranslation"/>.</param>
		/// <param name="other">A <see cref="ScaleRotationTranslation"/>.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( ScaleRotationTranslation srt, ScaleRotationTranslation other )
		{
			return !( srt.Scale.Equals( ref other.Scale ) && srt.Rotation.Equals( ref other.Rotation ) && srt.Translation.Equals( ref other.Translation ) );
		}

		#endregion Operators

	}

}