using System;
using System.Runtime.InteropServices;


namespace ManagedX.Audio
{


	/// <summary>General extended waveform format structure.
	/// <para>Use this for all non PCM formats (information common to all formats).</para>
	/// This structure is equivalent to the native <code>WAVEFORMATEX</code> structure (defined in MMReg.h).
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "That's the best I can do given the native name." )]
	[Win32.Source( "MMReg.h", "WAVEFORMATEX" )]
    [StructLayout( LayoutKind.Sequential, Pack = 2, Size = 18 )]
	public struct WaveFormatEx : IEquatable<WaveFormatEx>, IEquatable<WaveFormat>
	{

		private WaveFormat baseFormat;
		private ushort bitsPerSample;
		private ushort extraInfoSize;



		#region Constructors

		/// <summary>Initializes a new <see cref="WaveFormatEx"/> structure.</summary>
		/// <param name="formatTag">The format tag.</param>
		/// <param name="channelCount">The number of channels; must be within the range [1,65535].</param>
		/// <param name="samplesPerSecond">The sample rate, in hertz (Hz); must be greater than zero.</param>
		/// <param name="averageBytesPerSecond">For buffer estimation; must be greater than zero.</param>
		/// <param name="blockAlign">The block size of data, in bytes; must be within the range [0,65535].</param>
		/// <param name="bitsPerSample">The number of bits per sample, within the range [0,65535].</param>
		/// <param name="extraInfoSize">The size, in bytes, of the extra info (located after this <see cref="WaveFormatEx"/> structure), within the range [0,65535].</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public WaveFormatEx( short formatTag, int channelCount, int samplesPerSecond, int averageBytesPerSecond, int blockAlign, int bitsPerSample, int extraInfoSize )
		{
			try
			{
				baseFormat = new WaveFormat( formatTag, channelCount, samplesPerSecond, averageBytesPerSecond, blockAlign );
			}
			catch( ArgumentOutOfRangeException )
			{
				throw;
			}

			if( bitsPerSample <= 0 || bitsPerSample > ushort.MaxValue )
				throw new ArgumentOutOfRangeException( "bitsPerSample" );

			if( extraInfoSize <= 0 || extraInfoSize > ushort.MaxValue )
				throw new ArgumentOutOfRangeException( "extraInfoSize" );

			this.bitsPerSample = (ushort)bitsPerSample;
			this.extraInfoSize = (ushort)extraInfoSize;
		}


		/// <summary>Initializes a new <see cref="WaveFormatEx"/> structure from a <see cref="WaveFormat"/> structure.</summary>
		/// <param name="waveFormat">A <see cref="WaveFormat"/> structure.</param>
		/// <param name="bitsPerSample">The number of bits per sample, within the range [0,65535].</param>
		/// <param name="extraInfoSize">The size, in bytes, of the extra info (located after this <see cref="WaveFormatEx"/> structure), within the range [0,65535].</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public WaveFormatEx( WaveFormat waveFormat, int bitsPerSample, int extraInfoSize )
		{
			if( bitsPerSample <= 0 || bitsPerSample > ushort.MaxValue )
				throw new ArgumentOutOfRangeException( "bitsPerSample" );

			if( extraInfoSize <= 0 || extraInfoSize > ushort.MaxValue )
				throw new ArgumentOutOfRangeException( "extraInfoSize" );

			baseFormat = waveFormat;
			this.bitsPerSample = (ushort)bitsPerSample;
			this.extraInfoSize = (ushort)extraInfoSize;
		}

		#endregion Constructors



		#region WaveFormat properties

		/// <summary>Gets or sets the format type (see <see cref="WaveFormatTag"/>).</summary>
		public short FormatTag
		{
			get { return baseFormat.FormatTag; }
			set { baseFormat.FormatTag = value; }
		}


		/// <summary>Gets or sets the number of channels (i.e. mono, stereo...).
		/// <para>Must be within the range [0,65535].</para>
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int ChannelCount
		{
			get { return baseFormat.ChannelCount; }
			set { baseFormat.ChannelCount = value; }
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
			get { return bitsPerSample; }
			set
			{
				if( value < 0 || value > ushort.MaxValue )
					throw new ArgumentOutOfRangeException( "value" );
				bitsPerSample = (ushort)value;
			}
		}


		/// <summary>Gets or sets the size, in bytes, of the extra information (after this field).
		/// <para>Must be greater than or equal to zero.</para>
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int ExtraInfoSize
		{
			get { return extraInfoSize; }
			set
			{
				if( value < 0 || value > ushort.MaxValue )
					throw new ArgumentOutOfRangeException( "value" );
				extraInfoSize = (ushort)value;
			}
		}

		#endregion WaveFormatEx properties


		/// <summary>Returns a hash value for this <see cref="WaveFormatEx"/> structure.</summary>
		/// <returns>Returns a hash value for this <see cref="WaveFormatEx"/> structure.</returns>
		public override int GetHashCode()
		{
			return baseFormat.GetHashCode() ^ ( (uint)bitsPerSample | ( (uint)extraInfoSize << 16 ) ).GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="WaveFormatEx"/> structure equals another <see cref="WaveFormatEx"/> structure.</summary>
		/// <param name="other">A <see cref="WaveFormatEx"/> structure.</param>
		/// <returns>Returns true if the <paramref name="other"/> <see cref="WaveFormatEx"/> structure equals this <see cref="WaveFormatEx"/> structure, otherwise returns false.</returns>
		public bool Equals( WaveFormatEx other )
		{
			return baseFormat.Equals( other.baseFormat ) && ( bitsPerSample == other.bitsPerSample ) && ( extraInfoSize == other.extraInfoSize );
		}


		/// <summary>Returns a value indicating whether this <see cref="WaveFormatEx"/> structure is equivalent to a <see cref="WaveFormat"/> structure.</summary>
		/// <param name="other">A <see cref="WaveFormat"/> structure.</param>
		/// <returns>Returns true if the <see cref="WaveFormat"/> structure is equivalent to this <see cref="WaveFormatEx"/> structure, otherwise returns false.</returns>
		public bool Equals( WaveFormat other )
		{
			return baseFormat.Equals( other );
		}


		/// <summary>Returns a value indicating whether this <see cref="WaveFormatEx"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="WaveFormatEx"/> or a <see cref="WaveFormat"/> structure equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			if( obj is WaveFormatEx )
				return this.Equals( (WaveFormatEx)obj );

			if( obj is WaveFormat )
				return this.Equals( (WaveFormat)obj );

			return false;
		}


		/// <summary>Returns a <see cref="WaveFormat"/> structure initialized with this <see cref="WaveFormatEx"/> structure.</summary>
		/// <returns>Returns a <see cref="WaveFormat"/> structure initialized with this <see cref="WaveFormatEx"/> structure.</returns>
		public WaveFormat ToWaveFormat()
		{
			return baseFormat;
		}


	
		/// <summary>The empty <see cref="WaveFormatEx"/> structure.</summary>
		public static readonly WaveFormatEx Empty;


		#region Operators

		/// <summary><see cref="WaveFormatEx"/> to <see cref="WaveFormat"/> conversion operator.</summary>
		/// <param name="formatEx">A <see cref="WaveFormatEx"/> structure.</param>
		/// <returns>A <see cref="WaveFormat"/> initialized with the specified <paramref name="formatEx"/>.</returns>
		public static explicit operator WaveFormat( WaveFormatEx formatEx )
		{
			return formatEx.baseFormat;
		}


		/// <summary>Equality comparer.</summary>
		/// <param name="format">A <see cref="WaveFormatEx"/> structure.</param>
		/// <param name="other">A <see cref="WaveFormatEx"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( WaveFormatEx format, WaveFormatEx other )
		{
			return format.Equals( other );
		}

		/// <summary>Equality comparer.</summary>
		/// <param name="format">A <see cref="WaveFormatEx"/> structure.</param>
		/// <param name="other">A <see cref="WaveFormat"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( WaveFormatEx format, WaveFormat other )
		{
			return format.Equals( other );
		}

		/// <summary>Equality comparer.</summary>
		/// <param name="format">A <see cref="WaveFormat"/> structure.</param>
		/// <param name="other">A <see cref="WaveFormatEx"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( WaveFormat format, WaveFormatEx other )
		{
			return other.Equals( format );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="format">A <see cref="WaveFormatEx"/> structure.</param>
		/// <param name="other">A <see cref="WaveFormatEx"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( WaveFormatEx format, WaveFormatEx other )
		{
			return !format.Equals( other );
		}

		/// <summary>Inequality comparer.</summary>
		/// <param name="format">A <see cref="WaveFormatEx"/> structure.</param>
		/// <param name="other">A <see cref="WaveFormat"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( WaveFormatEx format, WaveFormat other )
		{
			return !format.Equals( other );
		}

		/// <summary>Inequality comparer.</summary>
		/// <param name="format">A <see cref="WaveFormat"/> structure.</param>
		/// <param name="other">A <see cref="WaveFormatEx"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( WaveFormat format, WaveFormatEx other )
		{
			return !other.Equals( format );
		}

		#endregion Operators

	}

}