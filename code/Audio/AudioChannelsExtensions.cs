namespace ManagedX.Audio
{


	/// <summary>Provides extension methods to the <see cref="AudioChannels"/> and <see cref="SpeakerConfiguration"/> enumerations.</summary>
	//[System.Diagnostics.DebuggerStepThrough]
	public static class AudioChannelsExtensions
	{


		/// <summary>Returns the number of channels set.</summary>
		/// <param name="channels">A combination of <see cref="AudioChannels"/> values.</param>
		/// <returns>Returns the number of channels set.</returns>
		public static int GetChannelCount( this AudioChannels channels )
		{
			var c = (uint)channels;
			
			var channelCount = 0u;
			var i = 0;
			
			while( i < 32 )
			{
				channelCount += ( c >> i ) & 1;
				++i;
			}

			return (int)channelCount;
		}

		/// <summary>Returns the number of channels corresponding to a <see cref="SpeakerConfiguration"/>.</summary>
		/// <param name="speakerConfiguration">A <see cref="SpeakerConfiguration"/> value.</param>
		/// <returns>Returns the number of channels set.</returns>
		public static int GetChannelCount( this SpeakerConfiguration speakerConfiguration )
		{
			return GetChannelCount( (AudioChannels)speakerConfiguration );
		}


		/// <summary>Returns the index of a channel in a combination of audio channels, or -1.</summary>
		/// <param name="channels">The combination of audio channels.</param>
		/// <param name="channel">An audio channel.</param>
		/// <returns>Returns the index of the specified <paramref name="channel"/> in the <paramref name="channels"/> combination, or -1.</returns>
		public static int GetChannelIndex( this AudioChannels channels, AudioChannels channel )
		{
			var channelCount = GetChannelCount( channels );
			var x = (int)channel;
			
			if( channelCount > 0 && x != 0 )
			{
				var c = (int)channels;
				var index = 0;
				var i = 0;
				
				while( index < channelCount )
				{
					if( ( ( c >> i ) & 1 ) == 1 )
						++index;

					if( ( ( x >> i ) & 1 ) == 1 )
						return index;
					
					++i;
				}
			}

			return -1;
		}

		/// <summary>Returns the index of an audio channel in a speaker configuration, or -1.</summary>
		/// <param name="speakerConfiguration">A speaker configuration.</param>
		/// <param name="channel">An audio channel.</param>
		/// <returns>Returns the index of the specified <paramref name="channel"/> in the <paramref name="speakerConfiguration"/>, or -1.</returns>
		public static int GetChannelIndex( this SpeakerConfiguration speakerConfiguration, AudioChannels channel )
		{
			return GetChannelIndex( (AudioChannels)speakerConfiguration, channel );
		}


		/// <summary>Converts a <see cref="SpeakerConfiguration"/> to an <see cref="AudioChannels"/> value.</summary>
		/// <param name="speakerConfiguration">A speaker configuration.</param>
		/// <returns>Returns an <see cref="AudioChannels"/> value representing the specified <paramref name="speakerConfiguration"/>.</returns>
		public static AudioChannels ToAudioChannels( this SpeakerConfiguration speakerConfiguration )
		{
			return (AudioChannels)speakerConfiguration;
		}

	}

}