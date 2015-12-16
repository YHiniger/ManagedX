namespace ManagedX
{
	
	/// <summary>Enumerates Windows status codes (HRESULT).</summary>
	public enum StatusCode : int
	{

		/// <summary>Normal status.</summary>
		None = 0x00000000,

		/// <summary>A string has been truncated to fit into the buffer.</summary>
		InPlaceTruncated = 0x000401A0, // INPLACE_S_TRUNCATED

	}

}