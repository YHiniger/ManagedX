using System;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace ManagedX.Audio
{
	using Properties;


	/// <summary>An <see cref="Exception"/> to be thrown when no audio hardware is available.</summary>
	[Serializable, DebuggerStepThrough]
	public sealed class NoAudioHardwareException : MissingRequirementException
	{

		/// <summary>Instantiates a new <see cref="NoAudioHardwareException"/>.</summary>
		/// <param name="message">The message associated with the exception.</param>
		/// <param name="innerException">The inner exception.</param>
		public NoAudioHardwareException( string message, Exception innerException )
			: base( message, innerException )
		{
		}


		/// <summary>Instantiates a new <see cref="NoAudioHardwareException"/>.</summary>
		/// <param name="message">The message associated with the exception.</param>
		public NoAudioHardwareException( string message )
			: base( message )
		{
		}


		/// <summary>Instantiates a new <see cref="NoAudioHardwareException"/>.</summary>
		public NoAudioHardwareException()
			: base( Resources.NoAudioHardwareExceptionMessage )
		{
		}


		private NoAudioHardwareException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
		}

	}

}