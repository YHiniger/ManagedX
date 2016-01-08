namespace ManagedX
{
	
	/// <summary>Enumerates Windows status codes (HRESULT).</summary>
	public enum StatusCode : int
	{

		/// <summary>Normal status.</summary>
		None = 0x00000000,

		/// <summary>A string has been truncated to fit into the buffer.
		/// <para>The native name of this constant is <code>INPLACE_S_TRUNCATED</code>.</para>
		/// </summary>
		InPlaceTruncated = 0x000401A0,


		#region DXGI
		
		// https://msdn.microsoft.com/en-us/library/windows/desktop/cc308061%28v=vs.85%29.aspx

		/// <summary>The window content is not visible.
		/// When receiving this status, an application can stop rendering and use <code>PresentOptions.Test</code> to determine when to resume rendering.
		/// <para>The native name of this constant is <code>DXGI_STATUS_OCCLUDED</code>.</para>
		/// </summary>
		DxgiOccluded = 0x087A0001,

		/// <summary>The desktop display mode has been changed, there might be color conversion/stretching.
		/// The application should call <code>IDXGISwapChain.ResizeBuffers</code> to match the new display mode.
		/// <para>The native name of this constant is <code>DXGI_STATUS_MODE_CHANGED</code>.</para>
		/// </summary>
		DxgiModeChange = 0x087A0007,

		/// <summary><code>IDXGISwapChain.ResizeTarget</code> and <code>IDXGISwapChain.SetFullscreenState</code> will return this value if a fullscreen/windowed mode transition is occurring when either API is called.
		/// <para>The native name of this constant is <code>DXGI_STATUS_MODE_CHANGE_IN_PROGRESS</code>.</para>
		/// </summary>
		DxgiModeChangeInProgress = 0x087A0008

		#endregion DXGI

	}

}