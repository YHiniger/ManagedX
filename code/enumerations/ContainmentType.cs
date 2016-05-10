namespace ManagedX
{
	using Win32;


	/// <summary>Enumerates values indicating whether an object contains another object.
	/// <para>This enumeration is equivalent to the native <code>ContainmentType</code> enumeration (defined in DirectXCollision.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.directxcollision.containmenttype%28v=vs.85%29.aspx</remarks>
	[Native( "DirectXCollision.h" )]
	public enum ContainmentType : int
	{

		/// <summary>The object does not contain the specified object.</summary>
		[Native( "DirectXCollision.h", "DISJOINT" )]
		Disjoint,

		/// <summary>The objects intersect (or match).</summary>
		[Native( "DirectXCollision.h", "INTERSECTS" )]
		Intersects,

		/// <summary>The object contains the specified object.</summary>
		[Native( "DirectXCollision.h", "CONTAINS" )]
		Contains

	}

}