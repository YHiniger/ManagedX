using System;
using System.Runtime.Serialization;


namespace ManagedX
{

	/// <summary>An <see cref="Exception"/> to be thrown when a requirement is missing (ie: no audio device, no suitable graphics device, no internet connection, etc).</summary>
	[Serializable]
	public class MissingRequirementException : Exception
	{

		/// <summary>Instantiates a new <see cref="MissingRequirementException"/>.</summary>
		public MissingRequirementException()
			: base()
		{
		}


		/// <summary>Instantiates a new <see cref="MissingRequirementException"/>.</summary>
		/// <param name="message">The exception message.</param>
		public MissingRequirementException( string message )
			: base( message )
		{
		}


		/// <summary>Instantiates a new <see cref="MissingRequirementException"/>.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerException">The inner exception.</param>
		public MissingRequirementException( string message, Exception innerException )
			: base( message, innerException )
		{
		}


		/// <summary>Instantiates a new <see cref="MissingRequirementException"/>.</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected MissingRequirementException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
		}

	}

}