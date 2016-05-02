namespace ManagedX.Design
{
	
	/// <summary>Defines properties to be implemented by 3D objects (ie: projectiles, audio emitters/listeners, cameras, etc).</summary>
	public interface I3DObject
	{
		
		/// <summary>Gets or sets the object position, in user-defined world units.</summary>
		Vector3 Position { get; set; }


		/// <summary>Gets or sets the orientation of front direction; must be orthogonal with <see cref="Top"/>.</summary>
		Vector3 Front { get; set; }


		/// <summary>Gets or sets the orientation of top direction; must be orthogonal with <see cref="Front"/>.</summary>
		Vector3 Top { get; set; }


		/// <summary>Gets or sets the object velocity, in user-defined world units per second.
		/// <para>Implementations should store the velocity as a direction vector and a speed.</para>
		/// </summary>
		Vector3 Velocity { get; set; } // = MoveDirection * Speed, so that we don't need to calculate the speed (=length of the vector)
	
	}

}