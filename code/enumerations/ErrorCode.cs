using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Win32
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms681382(v=vs.85).aspx
	// WinUser.h
	

	/// <summary>Enumerates Windows error codes (HRESULT starting with 0x8).</summary>
	public enum ErrorCode : int
	{
		
		/// <summary>The operation was successful.</summary>
		None = 0,

		
		// FIXME - those are status codes, not error codes ?

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

		/// <summary>Element not found.</summary>
		NotFound = 1168,

		/// <summary>XInput: the controller state is empty ?
		/// </summary>
		Empty = 4306,

		// /FIXME - status codes ?


		#region Stg*

		// https://msdn.microsoft.com/en-us/library/windows/desktop/dd542645%28v=vs.85%29.aspx

		/// <summary>STG: access denied.</summary>
		StgAccessDenied = unchecked( (int)0x80030005 ),		// STG_E_ACCESSDENIED
		
		/// <summary>STG: invalid parameter.</summary>
		StgInvalidArgument = unchecked( (int)0x80030057 ),	// STG_E_INVALIDPARAMETER

		#endregion // Stg*


		/// <summary>Not implemented.</summary>
		NotImplemented = unchecked( (int)0x80004001 ),	// E_NOTIMPL

		/// <summary>No such interface supported.</summary>
		NoInterface = unchecked( (int)0x80004002 ),		// E_NOINTERFACE

		/// <summary>Pointer that is not valid.</summary>
		Pointer = unchecked( (int)0x80004003 ),			// E_POINTER

		/// <summary>Operation aborted.</summary>
		Abort = unchecked( (int)0x80004004 ),			// E_ABORT

		/// <summary>Unspecified failure.</summary>
		Fail = unchecked( (int)0x80004005 ),			// E_FAIL


		/// <summary>One or more arguments are not valid.</summary>
		InvalidArgument = unchecked( (int)0x80070057 ),	// E_INVALIDARG

		/// <summary>Failed to allocate necessary memory.</summary>
		OutOfMemory = unchecked( (int)0x8007000E ),		// E_OUTOFMEMORY


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
		//STRSAFE_E_INSUFFICIENT_BUFFER = 2147942522,
		//REGDB_E_CLASSNOTREG = 2147746132,
		//ERROR_SHARING_VIOLATION = 2147942432


		#region DXGI

		// https://msdn.microsoft.com/en-us/library/windows/desktop/bb509553%28v=vs.85%29.aspx
		// https://msdn.microsoft.com/en-us/library/windows/desktop/aa378137%28v=vs.85%29.aspx


		/// <summary>The application provided invalid parameter data; this must be debugged and fixed before the application is released.
		/// Either the parameters of the call or the state of some object was incorrect. Enable the D3D debug layer in order to see details via debug messages.
		/// </summary>
		DxgiInvalidCall = unchecked( (int)0x887A0001 ),

		/// <summary>The object was not found. If calling IDXGIFactory::EnumAdapters, there is no adapter with the specified ordinal.</summary>
		DxgiNotFound = unchecked( (int)0x887A0002 ),

		/// <summary>The buffer supplied by the application is not big enough to hold the requested data.</summary>
		DxgiMoreData = unchecked( (int)0x887A0003 ),

		/// <summary>The specified device interface or feature level is not supported on this system.</summary>
		DxgiUnsupported = unchecked( (int)0x887A0004 ),

		/// <summary>The video card has been physically removed from the system, or a driver upgrade for the video card has occurred. The application should destroy and recreate the device.
		/// The GPU device instance has been suspended. Use ID3D10Device::GetDeviceRemovedReason to determine the appropriate action.
		/// </summary>
		DxgiDeviceRemoved = unchecked( (int)0x887A0005 ),

		/// <summary>The application's device failed due to badly formed commands sent by the application. This is a design-time issue that should be investigated and fixed.
		/// The GPU will not respond to more commands, most likely because of an invalid command passed by the calling application.
		/// </summary>
		DxgiDeviceHung = unchecked( (int)0x887A0006 ),

		/// <summary>The device failed due to a badly formed command. This is a run-time issue; the application should destroy and recreate the device.
		/// The GPU will not respond to more commands, most likely because some other application submitted invalid commands.
		/// </summary>
		DxgiDeviceReset = unchecked( (int)0x887A0007 ),

		/// <summary>The GPU was busy at the moment when the call was made, and the call was neither executed nor scheduled.</summary>
		DxgiWasStillDrawing = unchecked( (int)0x887A000A ),

		/// <summary>An event (such as power cycle) interrupted the gathering of presentation statistics. Any previous statistics should be considered invalid.</summary>
		DxgiFrameStatisticsDisjoint = unchecked( (int)0x887A000B ),

		/// <summary>The application attempted to acquire exclusive ownership of an output, but failed because some other application (or device within the application) already acquired ownership.
		/// Fullscreen mode could not be achieved because the specified output was already in use.
		/// </summary>
		DxgiGraphicsVidPNSourceInUse = unchecked( (int)0x887A000C ),

		/// <summary>The driver encountered a problem and was put into the device removed state.
		/// An internal issue prevented the driver from carrying out the specified operation.
		/// The driver's state is probably suspect, and the application should not continue.
		/// </summary>
		DxgiDriverInternalError = unchecked( (int)0x887A0020 ),

		/// <summary>A global counter resource was in use, and the specified counter cannot be used by this Direct3D device at this time.</summary>
		DxgiNonExclusive = unchecked( (int)0x887A0021 ),

		/// <summary>A resource is not available at the time of the call, but may become available later.</summary>
		DxgiNotCurrentlyAvailable = unchecked( (int)0x887A0022 ),

		/// <summary>Reserved.
		/// The application's remote device has been removed due to session disconnect or network disconnect.
		/// The application should call IDXGIFactory1::IsCurrent to find out when the remote device becomes available again.
		/// </summary>
		DxgiRemoteClientDisconnected = unchecked( (int)0x887A0023 ),

		/// <summary>Reserved.
		/// The device has been removed during a remote session because the remote computer ran out of memory.
		/// </summary>
		DxgiRemoteOutOfMemory = unchecked( (int)0x887A0024 ),

		/// <summary>The keyed mutex was abandoned.</summary>
		DxgiAccessLost = unchecked( (int)0x887A0026 ),

		/// <summary>The timeout value has elapsed and the resource is not yet available.</summary>
		DxgiWaitTimeout = unchecked( (int)0x887A0027 ),

		/// <summary>The output duplication has been turned off because the Windows session ended or was disconnected.
		/// This happens when a remote user disconnects, or when "switch user" is used locally.
		/// </summary>
		DxgiSessionDisconnected = unchecked( (int)0x887A0028 ),

		/// <summary>The DXGI outuput (monitor) to which the swapchain content was restricted, has been disconnected or changed.</summary>
		DxgiRestrictToOutputStale = unchecked( (int)0x887A0029 ),

		/// <summary>DXGI is unable to provide content protection on the swapchain. This is typically caused by an older driver,
		/// or by the application using a swapchain that is incompatible with content protection.
		/// </summary>
		DxgiCannotProtectContent = unchecked( (int)0x887A002A ),

		/// <summary>The application is trying to use a resource to which it does not have the required access privileges.
		/// This is most commonly caused by writing to a shared resource with read-only access.
		/// </summary>
		DxgiAccessDenied = unchecked( (int)0x887A002B ),

		/// <summary>The application is trying to create a shared handle using a name that is already associated with some other resource.</summary>
		DxgiNameAlreadyExists = unchecked( (int)0x887A002C ),

		/// <summary>The operation depends on an SDK component that is missing or mismatched.</summary>
		DxgiSdkComponentMissing = unchecked( (int)0x887A002D ),

		#endregion DXGI


		#region WASAPI

		// AudioClient.h

		/// <summary>The audio stream has not been successfully initialized.</summary>
		AudioClientNotInitialized = unchecked( (int)0x88890001 ),					// AUDCLNT_E_NOT_INITIALIZED

		/// <summary>The IAudioClient object is already initialized.</summary>
		AudioClientAlreadyInitialized = unchecked( (int)0x88890002 ),				// AUDCLNT_E_ALREADY_INITIALIZED

		/// <summary>The caller tried to access an IAudioCaptureClient interface on a rendering endpoint, or an IAudioRenderClient interface on a capture endpoint.</summary>
		AudioClientWrongEndPointType = unchecked( (int)0x88890003 ),				// AUDCLNT_E_WRONG_ENDPOINT_TYPE

		/// <summary>The audio endpoint device has been unplugged, or the audio hardware or associated hardware resources have been reconfigured, disabled, removed, or otherwise made unavailable for use.</summary>
		AudioClientDeviceInvalidated = unchecked( (int)0x88890004 ),				// AUDCLNT_E_DEVICE_INVALIDATED

		/// <summary>The audio stream was not stopped at the time the call was made.</summary>
		AudioClientNotStopped = unchecked( (int)0x88890005 ),						// AUDCLNT_E_NOT_STOPPED
		
		/// <summary></summary>
		AudioClientBufferTooLarge = unchecked( (int)0x88890006 ),					// AUDCLNT_E_BUFFER_TOO_LARGE
		
		/// <summary></summary>
		AudioClientOutOfOrder = unchecked( (int)0x88890007 ),						// AUDCLNT_E_OUT_OF_ORDER

		/// <summary>The audio engine (shared mode) or audio endpoint device (exclusive mode) does not support the specified format.</summary>
		AudioClientUnsupportedFormat = unchecked( (int)0x88890008 ),				// AUDCLNT_E_UNSUPPORTED_FORMAT

		/// <summary></summary>
		AudioClientInvalidSize = unchecked( (int)0x88890009 ),						// AUDCLNT_E_INVALID_SIZE

		/// <summary>The endpoint device is already in use.
		/// <para>Either the device is being used in exclusive mode, or the device is being used in shared mode and the caller asked to use the device in exclusive mode.</para>
		/// </summary>
		AudioClientDeviceInUse = unchecked( (int)0x8889000a ),						// AUDCLNT_E_DEVICE_IN_USE

		/// <summary>The client is currently writing to or reading from the buffer.</summary>
		AudioClientBufferOperationPending = unchecked( (int)0x8889000b ),			// AUDCLNT_E_BUFFER_OPERATION_PENDING

		/// <summary></summary>
		AudioClientThreadNotRegistered = unchecked( (int)0x8889000c ),				// AUDCLNT_E_THREAD_NOT_REGISTERED

		// 0x8889000d ?

		/// <summary>The caller is requesting exclusive-mode use of the endpoint device, but the user has disabled exclusive-mode use of the device.</summary>
		AudioClientExclusiveModeNotAllowed = unchecked( (int)0x8889000e ),			// AUDCLNT_E_EXCLUSIVE_MODE_NOT_ALLOWED

		/// <summary>The method failed to create the audio endpoint for the render or the capture device.
		/// <para>This can occur if the audio endpoint device has been unplugged, or the audio hardware or associated hardware resources have been reconfigured, disabled, removed, or otherwise made unavailable for use.</para>
		/// </summary>
		AudioClientEndPointCreateFailed = unchecked( (int)0x8889000f ),				// AUDCLNT_E_ENDPOINT_CREATE_FAILED

		/// <summary>The Windows audio service is not running.</summary>
		AudioClientServiceNotRunning = unchecked( (int)0x88890010 ),				// AUDCLNT_E_SERVICE_NOT_RUNNING

		/// <summary>The audio stream was not initialized for event-driven buffering.</summary>
		AudioClientEventHandleNotExpected = unchecked( (int)0x88890011 ),			// AUDCLNT_E_EVENTHANDLE_NOT_EXPECTED

		/// <summary></summary>
		AudioClientExclusiveModeOnly = unchecked( (int)0x88890012 ),				// AUDCLNT_E_EXCLUSIVE_MODE_ONLY

		/// <summary>The AUDCLNT_STREAMFLAGS_EVENTCALLBACK flag is set but parameters hnsBufferDuration and hnsPeriodicity are not equal.</summary>
		AudioClientBufferDurationPeriodNotEqual = unchecked( (int)0x88890013 ),		// AUDCLNT_E_BUFDURATION_PERIOD_NOT_EQUAL

		/// <summary>The audio stream is configured to use event-driven buffering, but the caller has not called IAudioClient::SetEventHandle to set the event handle on the stream.</summary>
		AudioClientEventHandleNotSet = unchecked( (int)0x88890014 ),				// AUDCLNT_E_EVENTHANDLE_NOT_SET

		/// <summary></summary>
		AudioClientIncorrectBufferSize = unchecked( (int)0x88890015 ),				// AUDCLNT_E_INCORRECT_BUFFER_SIZE

		/// <summary>Indicates that the buffer duration value requested by an exclusive-mode client is out of range.
		/// <para>The requested duration value for pull mode must not be greater than 500 milliseconds; for push mode the duration value must not be greater than 2 seconds.</para>
		/// </summary>
		AudioClientBufferSizeError = unchecked( (int)0x88890016 ),					// AUDCLNT_E_BUFFER_SIZE_ERROR

		/// <summary>Indicates that the process-pass duration exceeded the maximum CPU usage.
		/// <para>
		/// The audio engine keeps track of CPU usage by maintaining the number of times the process-pass duration exceeds the maximum CPU usage.
		/// The maximum CPU usage is calculated as a percent of the engine's periodicity.
		/// The percentage value is the system's CPU throttle value (within the range of 10% and 90%).
		/// If this value is not found, then the default value of 40% is used to calculate the maximum CPU usage.
		/// </para>
		/// </summary>
		AudioClientCpuUsageExceeded = unchecked( (int)0x88890017 ),					// AUDCLNT_E_CPUUSAGE_EXCEEDED

		/// <summary></summary>
		AudioClientBufferError = unchecked( (int)0x88890018 ),						// AUDCLNT_E_BUFFER_ERROR

		/// <summary></summary>
		AudioClientBufferSizeNotAligned = unchecked( (int)0x88890019 ),				// AUDCLNT_E_BUFFER_SIZE_NOT_ALIGNED

		// 0x8889001a -> 0x8889001f ?

		/// <summary>Indicates that the device period requested by an exclusive-mode client is greater than 500 milliseconds.</summary>
		AudioClientInvalidDevicePeriod = unchecked( (int)0x88890020 ),				// AUDCLNT_E_INVALID_DEVICE_PERIOD

		/// <summary></summary>
		AudioClientInvalidStreamOption = unchecked( (int)0x88890021 ),				// AUDCLNT_E_INVALID_STREAM_FLAG

		/// <summary></summary>
		AudioClientEndPointOffloadNotCapable = unchecked( (int)0x88890022 ),		// AUDCLNT_E_ENDPOINT_OFFLOAD_NOT_CAPABLE

		/// <summary></summary>
		AudioClientOutOfOffloadResources = unchecked( (int)0x88890023 ),			// AUDCLNT_E_OUT_OF_OFFLOAD_RESOURCES

		/// <summary></summary>
		AudioClientOffloadModeOnly = unchecked( (int)0x88890024 ),					// AUDCLNT_E_OFFLOAD_MODE_ONLY

		/// <summary></summary>
		AudioClientNonOffloadModeOnly = unchecked( (int)0x88890025 ),				// AUDCLNT_E_NONOFFLOAD_MODE_ONLY

		/// <summary></summary>
		AudioClientResourcesInvalidated = unchecked( (int)0x88890026 ),				// AUDCLNT_E_RESOURCES_INVALIDATED

		/// <summary></summary>
		AudioClientRawModeUnsupported = unchecked( (int)0x88890027 ),				// AUDCLNT_E_RAW_MODE_UNSUPPORTED

		/// <summary></summary>
		AudioClientEnginePeriodicityLocked = unchecked( (int)0x88890028 ),			// AUDCLNT_E_ENGINE_PERIODICITY_LOCKED
		
		/// <summary></summary>
		AudioClientEngineFormatLocked = unchecked( (int)0x88890029 ),				// AUDCLNT_E_ENGINE_FORMAT_LOCKED


		//AudioClientBufferEmpty = StatusCode.AudioClientBufferEmpty,							// AUDCLNT_S_BUFFER_EMPTY
		//AudioClientThreadAlreadyRegistered = StatusCode.AudioClientThreadAlreadyRegistered,	// AUDCLNT_S_THREAD_ALREADY_REGISTERED
		//AudioClientPositionStalled = StatusCode.AudioClientPositionStalled,					// AUDCLNT_S_POSITION_STALLED

		#endregion WASAPI


		#region XAudio2, XAPO

		// https://msdn.microsoft.com/en-us/library/windows/desktop/ee419234%28v=vs.85%29.aspx
		// xAudio2.h

		/// <summary>Returned by XAudio2 for certain API usage errors (invalid calls and so on) that are hard to avoid completely and should be handled by a title at runtime.
		/// <para>(API usage errors that are completely avoidable, such as invalid parameters, cause an ASSERT in debug builds and undefined behavior in retail builds, so no error code is defined for them.)</para>
		/// </summary>
		XAudio2InvalidCall = unchecked( (int)0x88960001 ),			// XAUDIO2_E_INVALID_CALL

		/// <summary>The Xbox 360 XMA hardware suffered an unrecoverable error.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "XMA", Justification = "XMA = Cross-platform Media Audio" )]
		XMADecoderError = unchecked( (int)0x88960002 ),				// XAUDIO2_E_XMA_DECODER_ERROR

		/// <summary>An effect failed to instantiate.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "XAPO", Justification = "XAPO = Cross-platform Audio Processing Object" )]
		XAPOCreationFailed = unchecked( (int)0x88960003 ),			// XAUDIO2_E_XAPO_CREATION_FAILED

		/// <summary>An audio device became unusable through being unplugged or some other event.</summary>
		XAudio2DeviceInvalidated = unchecked( (int)0x88960004 ),	// XAUDIO2_E_DEVICE_INVALIDATED


		/// <summary>Requested audio format is unsupported.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "XAPO", Justification = "XAPO = Cross-platform Audio Processing Object" )]
		XAPOFormatUnsupported = unchecked( (int)0x88970001 ),		// ?

		#endregion XAudio2, XAPO

	}

}