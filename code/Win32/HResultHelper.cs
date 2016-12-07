namespace ManagedX.Win32
{

	/// <summary></summary>
	public static class HResultHelper
	{

		/// <summary>Returns a value indicating whether an HRESULT is an error code.</summary>
		/// <param name="hResult">An HRESULT.</param>
		/// <returns>Returns true if the specified HRESULT is an error code, otherwise returns false.</returns>
		public static bool IsError( int hResult )
		{
			const int Mask = unchecked((int)0x80000000);
			return ( hResult & Mask ) == Mask;
		}

	}

}