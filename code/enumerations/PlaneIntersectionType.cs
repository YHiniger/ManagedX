namespace ManagedX
{

	/// <summary>Enumerates values indicating whether an object intersects a <see cref="Plane"/>.
	/// <para>This enumeration is equivalent to the native <code>PlaneIntersectionType</code> enumeration (defined in DirectXCollision.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.directxcollision.planeintersectiontype%28v=vs.85%29.aspx</remarks>
	public enum PlaneIntersectionType : int
	{

		/// <summary>The object is in front of the plane.</summary>
		Front,

		/// <summary>The object intersects the plane.</summary>
		Intersecting,

		/// <summary>The object is behind the plane.</summary>
		Back

	}

}