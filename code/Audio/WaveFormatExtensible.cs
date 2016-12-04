using System;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace ManagedX.Audio
{

	/// <summary>New wave format development should be based on this structure.
	/// <para>WaveFormatExtensible allows you to avoid having to register a new format tag with Microsoft.</para>
	/// Simply define a new GUID value for the <see cref="WaveFormatExtensible.SubFormat"/> field and use <see cref="WaveFormatTag.Extensible"/> in the <see cref="WaveFormatExtensible.FormatTag"/> field.
	/// <para>This structure is equivalent to the native <code>WAVEFORMATEXTENSIBLE</code> structure (defined in MMReg.h).</para>
	/// </summary>
	[Win32.Source( "MMReg.h", "WAVEFORMATEXTENSIBLE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 2, Size = 40 )]
	public struct WaveFormatExtensible : IEquatable<WaveFormatExtensible>, IEquatable<WaveFormatEx>, IEquatable<WaveFormat>
	{

		/// <summary>Defines the (minimum) value expected for <see cref="ExtraInfoSize"/>.</summary>
		public const int ExpectedExtraInfoSize = 22;



		private WaveFormatEx baseFormat;
		private ushort samples;	// Either ValidBitsPerSample, SamplesPerBlock or Reserved(0)
		private AudioChannels channelMask;
		private Guid subFormat;



		#region Constructors

		/// <summary>Initializes a new <see cref="WaveFormatExtensible"/> structure.</summary>
		/// <param name="samplesPerSecond">The sample rate, in hertz (Hz).</param>
		/// <param name="averageBytesPerSecond">For buffer estimation.</param>
		/// <param name="blockAlign">The block size of data, in bytes.</param>
		/// <param name="bitsPerSample">The number of bits per sample.</param>
		/// <param name="samples">Either ValidBitsPerSample, SamplesPerBlock or Reserved(0).</param>
		/// <param name="channelMask">Indicates which channels are present in the stream.</param>
		/// <param name="subFormat">Sub format <see cref="Guid"/>.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		/// <exception cref="InvalidEnumArgumentException"/>
		public WaveFormatExtensible( int samplesPerSecond, int averageBytesPerSecond, int blockAlign, int bitsPerSample, int samples, AudioChannels channelMask, Guid subFormat )
		{
			if( samples < 0 || samples > ushort.MaxValue )
				throw new ArgumentOutOfRangeException( "samples" );

			try
			{
				baseFormat = new WaveFormatEx( (short)WaveFormatTag.Extensible, channelMask.GetChannelCount(), samplesPerSecond, averageBytesPerSecond, blockAlign, bitsPerSample, ExpectedExtraInfoSize );
			}
			catch( ArgumentOutOfRangeException )
			{
				throw;
			}

			this.samples = (ushort)samples;
			this.channelMask = channelMask;
			this.subFormat = subFormat;
		}


		/// <summary>Instantiates a new <see cref="WaveFormatExtensible"/> structure.</summary>
		/// <param name="waveFormatEx">A <see cref="WaveFormatEx"/> structure.</param>
		/// <param name="samples">Either ValidBitsPerSample, SamplesPerBlock or 0.</param>
		/// <param name="channelMask">Indicates which channels are present in the stream.</param>
		/// <param name="subFormat">Sub format <see cref="Guid"/>.</param>
		public WaveFormatExtensible( WaveFormatEx waveFormatEx, int samples, AudioChannels channelMask, Guid subFormat )
		{
			if( waveFormatEx.FormatTag != (short)WaveFormatTag.Extensible )
				throw new ArgumentException( "Invalid wave format: only Extensible is allowed.", "waveFormatEx" );

			if( waveFormatEx.ChannelCount == 0 || waveFormatEx.AverageBytesPerSecond == 0 || waveFormatEx.BitsPerSample == 0 || waveFormatEx.BlockAlign == 0 || waveFormatEx.SamplesPerSecond == 0 )
				throw new ArgumentException( "Invalid wave format.", "waveFormatEx" );

			if( waveFormatEx.ExtraInfoSize < ExpectedExtraInfoSize )
				throw new ArgumentException( "Invalid wave format: extra info size is too small.", "waveFormatEx" );

			if( samples < 0 || samples > ushort.MaxValue )
				throw new ArgumentOutOfRangeException( "samples" );

			if( channelMask == AudioChannels.None )
				throw new ArgumentOutOfRangeException( "channelMask" );

			baseFormat = waveFormatEx;
			this.samples = (ushort)samples;
			this.channelMask = channelMask;
			this.subFormat = subFormat;
		}

		#endregion Constructors



		#region WaveFormat properties

		/// <summary>Gets the format type; must be <see cref="WaveFormatTag.Extensible"/>.</summary>
		public short FormatTag
		{
			get { return baseFormat.FormatTag; }
			//set { /* for compatibility with WaveFormat* structures */ }
		}


		/// <summary>Gets the number of channels (i.e. mono, stereo...), within the range [0,65535].
		/// <para>This property is affected by <see cref="ChannelMask"/>.</para>
		/// </summary>
		public int ChannelCount
		{
			get { return baseFormat.ChannelCount; }
			//set { /* for compatibility with WaveFormat* structures */ }
		}


		/// <summary>Gets or sets the sample rate, in Hertz (Hz).
		/// <para>Must be greater than or equal to zero.</para>
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int SamplesPerSecond
		{
			get { return baseFormat.SamplesPerSecond; }
			set { baseFormat.SamplesPerSecond = value; }
		}


		/// <summary>Gets or sets the average bytes per second on mono data; for buffer estimation.
		/// <para>Must be greater than or equal to zero.</para>
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int AverageBytesPerSecond
		{
			get { return baseFormat.AverageBytesPerSecond; }
			set { baseFormat.AverageBytesPerSecond = value; }
		}


		/// <summary>Gets or sets the block size of data, in bytes, within the range [0,65535].</summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int BlockAlign
		{
			get { return baseFormat.BlockAlign; }
			set { baseFormat.BlockAlign = value; }
		}

		#endregion WaveFormat properties

		
		#region WaveFormatEx properties

		/// <summary>Gets or sets the number of bits per sample of mono data.
		/// <para>Must be within the range [0,65535].</para>
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int BitsPerSample
		{
			get { return baseFormat.BitsPerSample; }
			set { baseFormat.BitsPerSample = value; }
		}


		/// <summary>Gets or sets the size, in bytes, of the extra information (after this field).
		/// <para>Must be at least <see cref="ExpectedExtraInfoSize"/>(22).</para>
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int ExtraInfoSize
		{
			get { return baseFormat.ExtraInfoSize; }
			set
			{
				if( value < ExpectedExtraInfoSize )
					throw new ArgumentOutOfRangeException( "value" );
				baseFormat.ExtraInfoSize = value;
			}
		}

		#endregion WaveFormatEx properties

		
		#region WaveFormatExtensible properties

		/// <summary>When <see cref="BitsPerSample"/> is 0, gets the number of samples per block. Otherwise, returns the bits of precision (ValidBitsPerSample) or 0 (Reserved).
		/// <para>Ranges between 0 and 65'535.</para>
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int Samples
		{
			get { return (int)samples; }
			set
			{
				if( value < 0 || value > ushort.MaxValue )
					throw new ArgumentOutOfRangeException( "value" );
				samples = (ushort)value;
			}
		}


		/// <summary>Gets or sets a value indicating the audio channels used.
		/// <para>Affects <see cref="ChannelCount"/>.</para>
		/// </summary>
		public AudioChannels ChannelMask
		{
			get { return channelMask; }
			set
			{
				channelMask = value;
				baseFormat.ChannelCount = value.GetChannelCount();
			}
		}


		/// <summary>Gets or sets the subformat <see cref="Guid"/>.</summary>
		public Guid SubFormat
		{
			get { return subFormat; }
			set { subFormat = value; }
		}

		#endregion WaveFormatExtensible properties


		/// <summary>Returns a hash code for this <see cref="WaveFormatExtensible"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="WaveFormatExtensible"/> structure.</returns>
		public override int GetHashCode()
		{
			return baseFormat.GetHashCode() ^ samples.GetHashCode() ^ (int)channelMask ^ subFormat.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="WaveFormatExtensible"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="WaveFormatExtensible"/> structure.</param>
		/// <returns>Returns true if the structures are equals, otherwise returns false.</returns>
		public bool Equals( WaveFormatExtensible other )
		{
			return baseFormat.Equals( other.baseFormat ) && ( samples == other.samples ) && ( channelMask == other.channelMask ) && ( subFormat == other.subFormat );
		}


		/// <summary>Returns a value indicating whether this <see cref="WaveFormatExtensible"/> structure is equivalent to a <see cref="WaveFormatEx"/> structure.</summary>
		/// <param name="other">A <see cref="WaveFormatEx"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, otherwise returns false.</returns>
		public bool Equals( WaveFormatEx other )
		{
			return baseFormat.Equals( other );
		}


		/// <summary>Returns a value indicating whether this <see cref="WaveFormatExtensible"/> structure is equivalent to a <see cref="WaveFormat"/> structure.</summary>
		/// <param name="other">A <see cref="WaveFormat"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, otherwise returns false.</returns>
		public bool Equals( WaveFormat other )
		{
			return baseFormat.Equals( other );
		}


		/// <summary>Returns a value indicating whether this <see cref="WaveFormatExtensible"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="WaveFormatExtensible"/>, a <see cref="WaveFormatEx"/> or a <see cref="WaveFormat"/> structure equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			if( obj is WaveFormatExtensible )
				return this.Equals( (WaveFormatExtensible)obj );

			if( obj is WaveFormatEx )
				return this.Equals( (WaveFormatEx)obj );

			if( obj is WaveFormat )
				return this.Equals( (WaveFormat)obj );

			return false;
		}


		/// <summary>Returns a <see cref="WaveFormatEx"/> structure initialized with this <see cref="WaveFormatExtensible"/> structure.</summary>
		/// <returns>Returns a <see cref="WaveFormatEx"/> structure initialized with this <see cref="WaveFormatExtensible"/> structure.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix" )]
		public WaveFormatEx ToWaveFormatEx()
		{
			return baseFormat;
		}


		/// <summary>Returns a <see cref="WaveFormat"/> structure initialized with this <see cref="WaveFormatExtensible"/> structure.</summary>
		/// <returns>Returns a <see cref="WaveFormat"/> structure initialized with this <see cref="WaveFormatExtensible"/> structure.</returns>
		public WaveFormat ToWaveFormat()
		{
			return baseFormat.ToWaveFormat();
		}



		/// <summary>The empty <see cref="WaveFormatExtensible"/> structure.</summary>
		public static readonly WaveFormatExtensible Empty;


		#region Operators

		/// <summary><see cref="WaveFormatExtensible"/> to <see cref="WaveFormatEx"/> conversion operator.</summary>
		/// <param name="format">A <see cref="WaveFormatExtensible"/> structure.</param>
		/// <returns>Returns a <see cref="WaveFormatEx"/> structure initialized with the specified <paramref name="format"/>.</returns>
		public static explicit operator WaveFormatEx( WaveFormatExtensible format )
		{
			return format.ToWaveFormatEx(); 
		}


		/// <summary><see cref="WaveFormatExtensible"/> to <see cref="WaveFormat"/> conversion operator.</summary>
		/// <param name="format">A <see cref="WaveFormatExtensible"/> structure.</param>
		/// <returns>Returns a <see cref="WaveFormat"/> structure initialized with the specified <paramref name="format"/>.</returns>
		public static explicit operator WaveFormat( WaveFormatExtensible format )
		{
			return format.ToWaveFormat();
		}


		/// <summary>Equality comparer.</summary>
		/// <param name="waveFormat">A <see cref="WaveFormatExtensible"/> structure.</param>
		/// <param name="other">A <see cref="WaveFormatExtensible"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( WaveFormatExtensible waveFormat, WaveFormatExtensible other )
		{
			return waveFormat.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="waveFormat">A <see cref="WaveFormatExtensible"/> structure.</param>
		/// <param name="other">A <see cref="WaveFormatExtensible"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( WaveFormatExtensible waveFormat, WaveFormatExtensible other )
		{
			return !waveFormat.Equals( other );
		}

		#endregion Operators

	}

}