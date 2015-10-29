namespace ManagedX
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.directxcollision.containmenttype%28v=vs.85%29.aspx
	// DirectXCollision.h


	/// <summary>Indicates whether an object contains another object.</summary>
	public enum ContainmentType : int
	{

		/// <summary>The object does not contain the specified object.</summary>
		Disjoint = 0,

		/// <summary>The objects intersect (or match).</summary>
		Intersects = 1,

		/// <summary>The object contains the specified object.</summary>
		Contains = 2

	}

}