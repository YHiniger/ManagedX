namespace ManagedX.Design
{
	
	/// <summary>Defines properties to be implemented by 3D objects (ie: projectiles, audio emitters/listeners, etc).</summary>
	public interface I3DObject
	{
		
		/// <summary>Gets or sets the object position, in user-defined world units.</summary>
		Vector3 Position { get; set; }

		/// <summary>Gets or sets the orientation of front direction; must be orthogonal with <see cref="Top"/>.</summary>
		Vector3 Front { get; set; }

		/// <summary>Gets or sets the orientation of top direction; must be orthogonal with <see cref="Front"/>.</summary>
		Vector3 Top { get; set; }

		/// <summary>Gets or sets the object velocity, in user-defined world units per second.</summary>
		Vector3 Velocity { get; set; }
	
	}

}