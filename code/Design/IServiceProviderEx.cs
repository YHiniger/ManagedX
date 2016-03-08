using System;


namespace ManagedX.Design
{

	/// <summary>Defines methods to retrieve service objects.</summary>
	public interface IServiceProviderEx
	{

		/// <summary>Returns a service given its type.</summary>
		/// <typeparam name="T">Service type.</typeparam>
		/// <returns>Returns the service associated with the specified type.</returns>
		T GetService<T>()
			where T : class;

	}

}