namespace ManagedX
{

	// DirectXCollision.h
	// https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.directxcollision.planeintersectiontype%28v=vs.85%29.aspx


	/// <summary>Indicates whether an object intersects a plane.</summary>
	public enum PlaneIntersectionType : int
	{

		/// <summary>The object is in front of the plane.</summary>
		Front = 0,

		/// <summary>The object intersects the plane.</summary>
		Intersecting = 1,

		/// <summary>The object is behind the plane.</summary>
		Back = 2

	}

}
