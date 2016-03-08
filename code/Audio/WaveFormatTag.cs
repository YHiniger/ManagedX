using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Audio
{

	/// <summary>Enumerates WAVE format tags (defined in MMReg.h).</summary>
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required to match native declaration." )]
	public enum WaveFormatTag : short
	{

		/// <summary>Unknown format.
		/// <para>This value is invalid.</para>
		/// </summary>
		None = 0,
		
		/// <summary>Pulse code modulation.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PCM", Justification = "PCM: Pulse Code Modulation" )]
		PCM = 1,

		/// <summary>Adaptive differential pulse code modulation.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ADPCM", Justification = "ADPCM: Adaptive Differential Pulse Code Modulation" )]
		ADPCM = 2,
		
		/// <summary></summary>
		Float = 3,

		/// <summary>A-law.</summary>
		ALaw = 6,
		
		/// <summary>Mu-law.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "µ" )]
		µLaw = 7,
		
		/// <summary></summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DRM", Justification = "DRM = Digital Rights Management" )]
		DRM = 9,
		
		/// <summary></summary>
		Mpeg = 50,
		
		/// <summary></summary>
		MpegLayer3 = 55,
		
		/// <summary></summary>
		DolbyAC3Spdif = 92,
		
		/// <summary></summary>
		WmaSpdif = 164,

		/// <summary><see cref="WaveFormatExtensible"/> tag.</summary>
		Extensible = -2
	
	}

}
