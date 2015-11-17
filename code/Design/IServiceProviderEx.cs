using System;


namespace ManagedX.Design
{

	/// <summary>Defines methods to retrieve service objects.
	/// <para>Inherits from <see cref="IServiceProvider"/>.</para>
	/// </summary>
	public interface IServiceProviderEx : IServiceProvider
	{

		/// <summary>Returns a service given its type or interface.</summary>
		/// <typeparam name="T">Type of service.</typeparam>
		/// <returns>Returns the service associated with the specified type.</returns>
		T GetService<T>() where T : class;

	}

}