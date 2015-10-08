namespace ManagedX
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms681382(v=vs.85).aspx
	// WinUser.h
	

	/// <summary>Enumerates Windows error codes.</summary>
	public enum ErrorCode : int
	{
		
		/// <summary>The operation was successful.</summary>
		None = 0,

		/// <summary>Access is denied.</summary>
		AccessDenied = 5,

		/// <summary>DisplayConfig: a device attached to the system is not functioning.
		/// </summary>
		GenFailure = 31,

		/// <summary>DisplayConfig: the request is not supported.
		/// </summary>
		NotSupported = 50,

		/// <summary>The parameter is incorrect.</summary>
		InvalidParameter = 87,

		/// <summary>The data area passed to a system call is too small.</summary>
		InsufficientBuffer = 122,

		/// <summary></summary>
		Busy = 170,

		/// <summary></summary>
		AlreadyExists = 183,

		/// <summary></summary>
		Pending = 997,

		/// <summary>XInput: the controller is not connected.
		/// </summary>
		NotConnected = 1167,

		/// <summary>XInput: the controller state is empty ?
		/// </summary>
		Empty = 4306,

		//D3DERR_WRONGTEXTUREFORMAT = 2289436696,
		//D3DERR_TOOMANYOPERATIONS = 2289436701,
		//D3DERR_DRIVERINTERNALERROR = 2289436711,
		//D3DERR_NOTFOUND = 2289436774,
		//D3DERR_MOREDATA,
		//D3DERR_DEVICELOST,
		//D3DERR_DEVICENOTRESET,
		//D3DERR_NOTAVAILABLE = 2289436784,
		//D3DERR_OUTOFVIDEOMEMORY = 2289435004,
		//D3DERR_INVALIDCALL = 2289436786,
		//XACTENGINE_E_ALREADYINITIALIZED = 2328297473,
		//XACTENGINE_E_NOTINITIALIZED,
		//XACTENGINE_E_EXPIRED,
		//XACTENGINE_E_NONOTIFICATIONCALLBACK,
		//XACTENGINE_E_NOTIFICATIONREGISTERED,
		//XACTENGINE_E_INVALIDUSAGE,
		//XACTENGINE_E_INVALIDDATA,
		//XACTENGINE_E_INSTANCELIMITFAILTOPLAY,
		//XACTENGINE_E_NOGLOBALSETTINGS,
		//XACTENGINE_E_INVALIDVARIABLEINDEX,
		//XACTENGINE_E_INVALIDCATEGORY,
		//XACTENGINE_E_INVALIDCUEINDEX,
		//XACTENGINE_E_INVALIDWAVEINDEX,
		//XACTENGINE_E_INVALIDTRACKINDEX,
		//XACTENGINE_E_INVALIDSOUNDOFFSETORINDEX,
		//XACTENGINE_E_READFILE,
		//XACTENGINE_E_UNKNOWNEVENT,
		//XACTENGINE_E_INCALLBACK,
		//XACTENGINE_E_NOWAVEBANK,
		//XACTENGINE_E_SELECTVARIATION,
		//XACTENGINE_E_MULTIPLEAUDITIONENGINES,
		//XACTENGINE_E_WAVEBANKNOTPREPARED,
		//XACTENGINE_E_NORENDERER,
		//XACTENGINE_E_INVALIDENTRYCOUNT,
		//XACTENGINE_E_SEEKTIMEBEYONDCUEEND,
		//XACTENGINE_E_AUDITION_WRITEFILE = 2328297729,
		//XACTENGINE_E_AUDITION_NOSOUNDBANK,
		//XACTENGINE_E_AUDITION_INVALIDRPCINDEX,
		//XACTENGINE_E_AUDITION_MISSINGDATA,
		//XACTENGINE_E_AUDITION_UNKNOWNCOMMAND,
		//XACTENGINE_E_AUDITION_INVALIDDSPINDEX,
		//XACTENGINE_E_AUDITION_MISSINGWAVE,
		//XACTENGINE_E_AUDITION_CREATEDIRECTORYFAILED,
		//XACTENGINE_E_AUDITION_INVALIDSESSION,
		//ZDKSYSTEM_E_AUDIO_INSTANCELIMIT = 2343370753,
		//ZDKSYSTEM_E_AUDIO_INVALIDSTATE,
		//ZDKSYSTEM_E_AUDIO_INVALIDDATA,
		//CAPTURE_ENGINE_E_DEVICEGONE = 2364407809,
		//DIRECTRENDERING_E_INVALID_MODE = 2150814720,
		//DIRECTRENDERING_E_ELEMENT_NOT_IN_VISUALTREE = 2281703676,
		//VFW_E_NO_AUDIO_HARDWARE = 2147746390,
		//E_INVALIDARG = 2147942487,
		//E_FAIL = 2147500037,
		//E_ABORT = 2147500036,
		//E_ACCESSDENIED = 2147942405,
		//E_NOTIMPL = 2147500033,
		//E_OUTOFMEMORY = 2147942414,
		//STRSAFE_E_INSUFFICIENT_BUFFER = 2147942522,
		//REGDB_E_CLASSNOTREG = 2147746132,
		//ERROR_SHARING_VIOLATION = 2147942432


		#region XAudio2

		// https://msdn.microsoft.com/en-us/library/windows/desktop/ee419234%28v=vs.85%29.aspx

		/// <summary>Returned by XAudio2 for certain API usage errors (invalid calls and so on) that are hard to avoid completely and should be handled by a title at runtime.
		/// <para>(API usage errors that are completely avoidable, such as invalid parameters, cause an ASSERT in debug builds and undefined behavior in retail builds, so no error code is defined for them.)</para>
		/// </summary>
		InvalidCall = -2003435519,

		/// <summary>The Xbox 360 XMA hardware suffered an unrecoverable error.</summary>
		XmaDecoderError = -2003435518,

		/// <summary>An effect failed to instantiate.</summary>
		XapoCreationFailed = -2003435517,

		/// <summary>An audio device became unusable through being unplugged or some other event.</summary>
		DeviceInvalidated = -2003435516

		#endregion

	}

}