namespace ManagedX.Audio
{
	using Win32;


	/// <summary>Enumerates common <see cref="AudioChannels"/> combinations.
	/// <para>This enumeration is equivalent to the native constants SPEAKER_* (defined in X3DAudio.h).</para>
	/// </summary>
	public enum SpeakerConfiguration : int
	{

		/// <summary>Undefined speaker configuration; this value is invalid.</summary>
		None = AudioChannels.None,

		/// <summary>Mono speaker configuration: front center.</summary>
		[Source( "X3dAudio.h", "SPEAKER_MONO" )]
		Mono = AudioChannels.FrontCenter,

		/// <summary>Stereo speaker configuration: front left and front right.</summary>
		[Source( "X3dAudio.h", "SPEAKER_STEREO" )]
		Stereo = AudioChannels.FrontLeft | AudioChannels.FrontRight,

		/// <summary>2.1 speaker configuration: front left, front right and low frequency emitter.</summary>
		[Source( "X3dAudio.h", "SPEAKER_2POINT1" )]
		TwoPointOne = AudioChannels.FrontLeft | AudioChannels.FrontRight | AudioChannels.LowFrequency,

		/// <summary>Surround speaker configuration: front left, front right, front center and back center.</summary>
		[Source( "X3dAudio.h", "SPEAKER_SURROUND" )]
		Surround = AudioChannels.FrontLeft | AudioChannels.FrontRight | AudioChannels.FrontCenter | AudioChannels.BackCenter,

		/// <summary>Quad speaker configuration: front left, front right, back left and back right.</summary>
		[Source( "X3dAudio.h", "SPEAKER_QUAD" )]
		Quad = AudioChannels.FrontLeft | AudioChannels.FrontRight | AudioChannels.BackLeft | AudioChannels.BackRight,

		/// <summary>4.1 speaker configuration: front left, front right, back left, back right and low frequency emitter.</summary>
		[Source( "X3dAudio.h", "SPEAKER_4POINT1" )]
		FourPointOne = AudioChannels.FrontLeft | AudioChannels.FrontRight | AudioChannels.LowFrequency | AudioChannels.BackLeft | AudioChannels.BackRight,

		/// <summary>5.1 speaker configuration: front left, front right, front center, back left, back right and low frequency emitter.
		/// <para>The native constant SPEAKER_XBOX refers to this value.</para>
		/// </summary>
		[Source( "X3dAudio.h", "SPEAKER_5POINT1" )]
		FivePointOne = AudioChannels.FrontLeft | AudioChannels.FrontRight | AudioChannels.FrontCenter | AudioChannels.LowFrequency | AudioChannels.BackLeft | AudioChannels.BackRight,

		/// <summary>7.1 speaker configuration: front left, front right, front center, back left, back right, front left of center, front right of center and low frequency emitter.</summary>
		[Source( "X3dAudio.h", "SPEAKER_7POINT1" )]
		SevenPointOne = AudioChannels.FrontLeft | AudioChannels.FrontRight | AudioChannels.FrontCenter | AudioChannels.LowFrequency | AudioChannels.BackLeft | AudioChannels.BackRight | AudioChannels.FrontLeftOfCenter | AudioChannels.FrontRightOfCenter,

		/// <summary>5.1 Surround speaker configuration: front left, front right, front center, side left, side right and low frequency emitter.</summary>
		[Source( "X3dAudio.h", "SPEAKER_5POINT1_SURROUND" )]
		FivePointOneSurround = AudioChannels.FrontLeft | AudioChannels.FrontRight | AudioChannels.FrontCenter | AudioChannels.LowFrequency | AudioChannels.SideLeft | AudioChannels.SideRight,

		/// <summary>7.1 Surround speaker configuration: front left, front right, front center, back left, back right, side left, side right and low frequency emitter.</summary>
		[Source( "X3dAudio.h", "SPEAKER_7POINT1_SURROUND" )]
		SevenPointOneSurround = AudioChannels.FrontLeft | AudioChannels.FrontRight | AudioChannels.FrontCenter | AudioChannels.LowFrequency | AudioChannels.BackLeft | AudioChannels.BackRight | AudioChannels.SideLeft | AudioChannels.SideRight

	}

}