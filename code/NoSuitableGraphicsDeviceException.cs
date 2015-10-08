using System;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace ManagedX.Graphics
{
	using Properties;


	/// <summary>A <see cref="MissingRequirementException"/> to be thrown when no graphics device can be used.</summary>
	[Serializable, DebuggerStepThrough]
	public sealed class NoSuitableGraphicsDeviceException : MissingRequirementException
	{

		/// <summary>Instantiates a new <see cref="NoSuitableGraphicsDeviceException"/>.</summary>
		/// <param name="message">The message associated with the exception.</param>
		/// <param name="innerException">The inner exception.</param>
		public NoSuitableGraphicsDeviceException( string message, Exception innerException )
			: base( message, innerException )
		{
		}

		/// <summary>Instantiates a new <see cref="NoSuitableGraphicsDeviceException"/>.</summary>
		/// <param name="message">The message associated with the exception.</param>
		public NoSuitableGraphicsDeviceException( string message )
			: base( message )
		{
		}

		/// <summary>Instantiates a new <see cref="NoSuitableGraphicsDeviceException"/>.</summary>
		public NoSuitableGraphicsDeviceException()
			: this( Resources.NoSuitableGraphicsDeviceExceptionMessage )
		{
		}

		private NoSuitableGraphicsDeviceException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
		}

	}

}