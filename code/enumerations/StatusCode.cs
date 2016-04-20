namespace ManagedX.Win32
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dxgi")]
        DxgiOccluded = 0x087A0001,

        /// <summary>The desktop display mode has been changed, there might be color conversion/stretching.
        /// The application should call <code>IDXGISwapChain.ResizeBuffers</code> to match the new display mode.
        /// <para>The native name of this constant is <code>DXGI_STATUS_MODE_CHANGED</code>.</para>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dxgi")]
        DxgiModeChange = 0x087A0007,

        /// <summary><code>IDXGISwapChain.ResizeTarget</code> and <code>IDXGISwapChain.SetFullscreenState</code> will return this value if a fullscreen/windowed mode transition is occurring when either API is called.
        /// <para>The native name of this constant is <code>DXGI_STATUS_MODE_CHANGE_IN_PROGRESS</code>.</para>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dxgi")]
        DxgiModeChangeInProgress = 0x087A0008,

		#endregion DXGI


		#region D3D

		/// <summary>The presentation area is occluded.
		/// Occlusion means that the presentation window is minimized or another device entered the fullscreen mode on the same monitor as the presentation window and the presentation window is completely on that monitor.
		/// Occlusion will not occur if the client area is covered by another window.
		/// Occluded applications can continue rendering and all calls will succeed, but the occluded presentation window will not be updated.
		/// Preferably the application should stop rendering to the presentation window using the device and keep calling CheckDeviceState until <see cref="None"/> or <see cref="D3DPresentModeChanged"/> returns.
		/// </summary>
		D3DPresentOccluded = 0x08760878,

		/// <summary>The desktop display mode has been changed.
		/// The application can continue rendering, but there might be color conversion/stretching.
		/// Pick a back buffer format similar to the current display mode, and call Reset to recreate the swap chains.
		/// The device will leave this state after a Reset is called.
		/// </summary>
		D3DPresentModeChanged = 0x08760877, // 2167

		#endregion D3D


		#region WASAPI

		/// <summary></summary>
		AudioClientBufferEmpty = 0x08890001,				// AUDCLNT_S_BUFFER_EMPTY

		/// <summary></summary>
		AudioClientThreadAlreadyRegistered = 0x08890002,	// AUDCLNT_S_THREAD_ALREADY_REGISTERED

		/// <summary></summary>
		AudioClientPositionStalled = 0x08890003,			// AUDCLNT_S_POSITION_STALLED

		#endregion WASAPI

	}

}