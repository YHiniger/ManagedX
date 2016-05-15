using System;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace ManagedX
{

	/// <summary>An <see cref="Exception"/> to be thrown when a requirement is missing (ie: no audio device, no suitable graphics device, no internet connection, etc).</summary>
	[DebuggerStepThrough]
	[Serializable]
	public class MissingRequirementException : Exception
	{

		/// <summary>Initializes a new <see cref="MissingRequirementException"/>.</summary>
		public MissingRequirementException()
			: base()
		{
		}


		/// <summary>Initializes a new <see cref="MissingRequirementException"/> with a specified error message.</summary>
		/// <param name="message">The exception message.</param>
		public MissingRequirementException( string message )
			: base( message )
		{
		}


		/// <summary>Initializes a new <see cref="MissingRequirementException"/> with a specified error message and a reference to the inner exception which caused this exception.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerException">The inner exception.</param>
		public MissingRequirementException( string message, Exception innerException )
			: base( message, innerException )
		{
		}


		/// <summary>Initializes a new <see cref="MissingRequirementException"/> with serialized data.</summary>
		/// <param name="info">Holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		protected MissingRequirementException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
		}

	}

}