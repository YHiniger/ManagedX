using System;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace ManagedX
{

	/// <summary>A <see cref="MissingRequirementException"/> to be thrown when no audio hardware is available.</summary>
	[DebuggerStepThrough]
	[Serializable]
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
			: base( "No audio hardware found." )
		{
		}


		private NoAudioHardwareException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
		}

	}

}