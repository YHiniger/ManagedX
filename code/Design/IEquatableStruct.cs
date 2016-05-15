using System;


namespace ManagedX.Design
{

	/// <summary>For internal use only; this interface is not CLS-compliant.</summary>
	/// <typeparam name="T">Structure type.</typeparam>
	[CLSCompliant( false )]
	public interface IEquatableStruct<T> : IEquatable<T>
		where T : struct
	{

		/// <summary>Returns a value indicating whether the structure is equivalent to another structure of the same type.</summary>
		/// <param name="other">A structure.</param>
		/// <returns>Returns true if the structures are equivalent, otherwise returns false.</returns>
		bool Equals( ref T other );

	}

}