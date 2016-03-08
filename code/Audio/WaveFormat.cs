using System;
using System.Runtime.InteropServices;


namespace ManagedX.Audio
{

	/// <summary>General waveform format structure (defined in MMReg.h).
	/// <para>Use this for all non PCM formats (information common to all formats).</para>
	/// </summary>
	[StructLayout( LayoutKind.Sequential, Pack = 2, Size = 14 )]
	public struct WaveFormat : IEquatable<WaveFormat>
	{

		private short formatTag;
		private ushort channelCount;
		private int samplesPerSecond;
		private int averageBytesPerSecond;
		private ushort blockAlign;



		/// <summary>Initializes a new <see cref="WaveFormat"/> structure.</summary>
		/// <param name="formatTag">A format tag.</param>
		/// <param name="channelCount">The number of channels.</param>
		/// <param name="samplesPerSecond">The sample rate, in hertz (Hz).</param>
		/// <param name="averageBytesPerSecond">For buffer estimation.</param>
		/// <param name="blockAlign">The block size of data, in bytes.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public WaveFormat( short formatTag, int channelCount, int samplesPerSecond, int averageBytesPerSecond, int blockAlign )
		{
			if( channelCount <= 0 || channelCount > ushort.MaxValue )
				throw new ArgumentOutOfRangeException( "channelCount" );

			if( samplesPerSecond <= 0 )
				throw new ArgumentOutOfRangeException( "samplesPerSecond" );

			if( averageBytesPerSecond <= 0 )
				throw new ArgumentOutOfRangeException( "averageBytesPerSecond" );

			if( blockAlign < 0 || blockAlign > ushort.MaxValue )
				throw new ArgumentOutOfRangeException( "blockAlign" );

			this.formatTag = formatTag;
			this.channelCount = (ushort)channelCount;
			this.samplesPerSecond = samplesPerSecond;
			this.averageBytesPerSecond = averageBytesPerSecond;
			this.blockAlign = (ushort)blockAlign;
		}


		
		#region WaveFormat properties

		/// <summary>Gets or sets the format type (see <see cref="WaveFormatTag"/>).</summary>
		public short FormatTag
		{
			get { return formatTag; }
			set { formatTag = value; }
		}


		/// <summary>Gets or sets the number of channels (i.e. mono, stereo...).
		/// <para>Must be within the range [0,65535].</para>
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int ChannelCount
		{
			get { return (int)channelCount; }
			set
			{
				if( value < 0 || value > ushort.MaxValue )
					throw new ArgumentOutOfRangeException( "value" );
				channelCount = (ushort)value;
			}
		}


		/// <summary>Gets or sets the sample rate, in Hertz (Hz).
		/// <para>Must be greater than or equal to zero.</para>
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int SamplesPerSecond
		{
			get { return samplesPerSecond; }
			set
			{
				if( value < 0 )
					throw new ArgumentOutOfRangeException( "value" );
				samplesPerSecond = value;
			}
		}


		/// <summary>Gets or sets the average bytes per second on mono data; for buffer estimation.
		/// <para>Must be greater than or equal to zero.</para>
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int AverageBytesPerSecond
		{
			get { return averageBytesPerSecond; }
			set
			{
				if( value < 0 )
					throw new ArgumentOutOfRangeException( "value" );
				averageBytesPerSecond = value;
			}
		}


		/// <summary>Gets or sets the block size of data, in bytes, within the range [0,65535].</summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public int BlockAlign
		{
			get { return (int)blockAlign; }
			set
			{
				if( value < 0 || value > ushort.MaxValue )
					throw new ArgumentOutOfRangeException( "value" );
				blockAlign = (ushort)value;
			}
		}

		#endregion WaveFormat properties


		/// <summary>Returns a hash value for this <see cref="WaveFormat"/> structure.</summary>
		/// <returns>Returns a hash value for this <see cref="WaveFormat"/> structure.</returns>
		public override int GetHashCode()
		{
			return formatTag.GetHashCode() ^ channelCount.GetHashCode() ^ samplesPerSecond ^ averageBytesPerSecond ^ blockAlign.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="WaveFormat"/> structure equals another <see cref="WaveFormat"/> structure.</summary>
		/// <param name="other">A <see cref="WaveFormat"/> structure.</param>
		/// <returns>Returns true if all fields of the <paramref name="other"/> structure equal the fields of this <see cref="WaveFormat"/> structure.</returns>
		public bool Equals( WaveFormat other )
		{
			return
				( formatTag == other.formatTag ) &&
				( channelCount == other.channelCount ) && 
				( samplesPerSecond == other.samplesPerSecond ) &&
				( averageBytesPerSecond == other.averageBytesPerSecond ) && 
				( blockAlign == other.blockAlign );
		}


		/// <summary>Returns a value indicating whether this <see cref="WaveFormat"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="WaveFormat"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is WaveFormat ) && this.Equals( (WaveFormat)obj );
		}



		/// <summary>The empty <see cref="WaveFormat"/> structure.</summary>
		public static readonly WaveFormat Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="format">A <see cref="WaveFormat"/> structure.</param>
		/// <param name="other">A <see cref="WaveFormat"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( WaveFormat format, WaveFormat other )
		{
			return format.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="format">A <see cref="WaveFormat"/> structure.</param>
		/// <param name="other">A <see cref="WaveFormat"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( WaveFormat format, WaveFormat other )
		{
			return !format.Equals( other );
		}

		#endregion Operators

	}

}