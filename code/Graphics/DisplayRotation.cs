namespace ManagedX.Graphics
{
	using Win32;


	/// <summary>Enumerates the (clockwise) rotations of the display.
	/// <para>This enumeration is equivalent to the native
	/// <code>DISPLAYCONFIG_ROTATION</code> (defined in WinGDI.h) and
	/// <code>DXGI_MODE_ROTATION</code> (defined in DXGI.h) enumerations.
	/// </para>
	/// </summary>
	/// <remarks>
	/// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553970%28v=vs.85%29.aspx (DISPLAYCONFIG_ROTATION)
	/// https://msdn.microsoft.com/en-us/library/windows/desktop/bb173065%28v=vs.85%29.aspx (DXGI_MODE_ROTATION)
	/// </remarks>
	[Native( "WinGDI.h", "DISPLAYCONFIG_ROTATION" )]
	[Native( "DXGIType.h", "DXGI_MODE_ROTATION" )]
	public enum DisplayRotation : int
	{

		/// <summary>Unspecified.</summary>
		[Native( "DXGIType.h", "DXGI_MODE_ROTATION_UNSPECIFIED" )]
		Unspecified,

		/// <summary>Rotation is 0 degrees—landscape mode.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_ROTATION_IDENTITY" )]
		[Native( "DXGIType.h", "DXGI_MODE_ROTATION_IDENTITY" )]
		Identity,

		/// <summary>Rotation is 90 degrees clockwise—portrait mode.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_ROTATION_ROTATE90" )]
		[Native( "DXGIType.h", "DXGI_MODE_ROTATION_ROTATE90" )]
		Rotate90,

		/// <summary>Rotation is 180 degrees clockwise—inverted landscape mode.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_ROTATION_ROTATE180" )]
		[Native( "DXGIType.h", "DXGI_MODE_ROTATION_ROTATE180" )]
		Rotate180,

		/// <summary>Rotation is 270 degrees clockwise—inverted portrait mode.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_ROTATION_ROTATE270" )]
		[Native( "DXGIType.h", "DXGI_MODE_ROTATION_ROTATE270" )]
		Rotate270

	}

}