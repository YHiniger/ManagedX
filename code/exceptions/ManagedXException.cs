using System;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace ManagedX
{

	/// <summary>Base class for ManagedX exceptions (=exceptions related to DirectX).</summary>
	[DebuggerStepThrough]
	[Serializable]
	public class ManagedXException : Exception
	{

		/// <summary>Initializes a new <see cref="ManagedXException"/>.</summary>
		public ManagedXException()
			: base()
		{
		}


		/// <summary>Initializes a new <see cref="ManagedXException"/> with a specified error message.</summary>
		/// <param name="message">The exception message.</param>
		public ManagedXException( string message )
			: base( message )
		{
		}


		/// <summary>Initializes a new <see cref="ManagedXException"/> with a specified error message and a reference to the inner exception which caused this exception.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerException">The inner exception.</param>
		public ManagedXException( string message, Exception innerException )
			: base( message, innerException )
		{
		}


		/// <summary>Initializes a new <see cref="ManagedXException"/> with serialized data.</summary>
		/// <param name="info">Holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		protected ManagedXException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
		}

	}

}