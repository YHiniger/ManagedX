using System;
using System.Collections.ObjectModel;


namespace ManagedX
{

	/// <summary>A generic read-only collection of non-null, unique elements.</summary>
	/// <typeparam name="T">Type of elements in the collection.</typeparam>
	[Serializable]
	public class ReadOnlySafeCollection<T> : ReadOnlyCollection<T>
		where T : class
	{


		/// <summary>Initializes a new <see cref="ReadOnlySafeCollection{T}"/>, which is a read-only wrapper around a <see cref="SafeCollection{T}"/>.</summary>
		/// <param name="safeCollection">The collection to wrap; must not be null.</param>
		/// <exception cref="ArgumentNullException"/>
		public ReadOnlySafeCollection( SafeCollection<T> safeCollection )
			: base( safeCollection )
		{
		}

	}

}