﻿using System;
using System.Collections.Generic;


namespace ManagedX
{
	using Design;


	/// <summary>A service container; implements <see cref="IServiceProvider"/>.</summary>
	public class ServiceContainer : IServiceProvider1
	{

		private readonly Dictionary<Guid, object> services;



		/// <summary>Initializes a new <see cref="ServiceContainer"/>.</summary>
		public ServiceContainer()
		{
			services = new Dictionary<Guid, object>();
		}



		/// <summary>Adds a service to the container.</summary>
		/// <param name="serviceType">The service type; must not be null.</param>
		/// <param name="service">The service; must not be null.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public void AddService( Type serviceType, object service )
		{
			if( serviceType == null )
				throw new ArgumentNullException( "serviceType" );

			if( service == null )
				throw new ArgumentNullException( "service" );

			if( service == this )
				throw new ArgumentException( "Invalid service.", "service" );

			services.Add( serviceType.GUID, service );
		}


		/// <summary>Adds a service to the container.</summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="service">The service; must not be null.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="InvalidOperationException"/>
		public void AddService<TService>( TService service )
			where TService : class
		{
			if( service == null )
				throw new ArgumentNullException( "service" );

			var serviceTypeGuid = typeof( TService ).GUID;
			if( services.TryGetValue( serviceTypeGuid, out object svc ) )
				throw new InvalidOperationException();

			services.Add( serviceTypeGuid, service );
		}


		/// <summary>Returns a service given its type.</summary>
		/// <param name="serviceType">The type of the requested service.</param>
		/// <returns>The corresponding service, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		public object GetService( Type serviceType )
		{
			if( serviceType == null )
				throw new ArgumentNullException( "serviceType" );

			if( !services.TryGetValue( serviceType.GUID, out object output ) )
				output = null;
			return output;
		}


		/// <summary>Returns a service given its type.</summary>
		/// <typeparam name="TService">The service type.</typeparam>
		public TService GetService<TService>()
			where TService : class
		{
			return (TService)this.GetService( typeof( TService ) );
		}


		/// <summary>Removes a service from the container.</summary>
		/// <param name="serviceType">The type of the service to remove.</param>
		/// <returns>Returns true if the associated service has been removed from the container, otherwise returns false (ie: service not found).</returns>
		/// <exception cref="ArgumentNullException"/>
		public bool RemoveService( Type serviceType )
		{
			if( serviceType == null )
				throw new ArgumentNullException( "serviceType" );

			return services.Remove( serviceType.GUID );
		}


		///// <summary>Removes a service.</summary>
		///// <typeparam name="TService">The service type.</typeparam>
		///// <returns>Returns true if the associated service has been removed from the container, otherwise returns false (ie: service not found).</returns>
		//public bool RemoveService<TService>()
		//	where TService : class
		//{
		//	return this.RemoveService( typeof( TService ) );
		//}

	}

}