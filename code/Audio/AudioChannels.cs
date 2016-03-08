namespace ManagedX.Audio
{

	/// <summary>Enumerates audio channels.
	/// <para>This enumeration is equivalent to the native constants SPEAKER_* (defined in X3DAudio.h).</para>
	/// For common channel combinations, see <see cref="SpeakerConfiguration"/>.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags", Justification = "They ARE flags." )]
	[System.Flags]
	public enum AudioChannels : int
	{

		/// <summary>No channel specified.</summary>
		None = 0x00000000,
		
		/// <summary>The front left channel.</summary>
		FrontLeft = 0x00000001,
		
		/// <summary>The front right channel.</summary>
		FrontRight = 0x00000002,
		
		/// <summary>The front center channel.</summary>
		FrontCenter = 0x00000004,
		
		/// <summary>The low frequency channel.</summary>
		LowFrequency = 0x00000008,
		
		/// <summary>The back left channel.</summary>
		BackLeft = 0x00000010,
		
		/// <summary>The back right channel.</summary>
		BackRight = 0x00000020,

		/// <summary>The front left of center channel.</summary>
		FrontLeftOfCenter = 0x00000040,
		
		/// <summary>The front right of center channel.</summary>
		FrontRightOfCenter = 0x00000080,
		
		/// <summary>The back center channel.</summary>
		BackCenter = 0x00000100,
		
		/// <summary>The side left channel.</summary>
		SideLeft = 0x00000200,
		
		/// <summary>The side right channel.</summary>
		SideRight = 0x00000400,
		
		/// <summary>The top center channel.</summary>
		TopCenter = 0x00000800,
		
		/// <summary>The top front left channel.</summary>
		TopFrontLeft = 0x00001000,
		
		/// <summary>The top front center channel.</summary>
		TopFrontCenter = 0x00002000,
		
		/// <summary>The top front right channel.</summary>
		TopFrontRight = 0x00004000,
		
		/// <summary>The top back left channel.</summary>
		TopBackLeft = 0x00008000,
		
		/// <summary>The top back center channel.</summary>
		TopBackCenter = 0x00010000,
		
		/// <summary>The top back right channel.</summary>
		TopBackRight = 0x00020000,

		///// <summary>Bit mask locations reserved for future use.</summary>
		//Reserved = 0x7FFC0000,

		///// <summary>Used to specify that any possible permutation of speaker configurations.</summary>
		//All = unchecked ( (int)0x80000000 )

	}

}