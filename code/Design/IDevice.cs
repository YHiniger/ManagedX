namespace ManagedX.Design
{
	
	/// <summary>Exposes properties common to all devices.</summary>
	public interface IDevice
	{

		/// <summary>Gets the identifier of the device.</summary>
		string DeviceIdentifier { get; }


		/// <summary>Gets the friendly name of the device.</summary>
		string DisplayName { get; }

	}

}