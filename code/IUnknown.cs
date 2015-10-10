using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace System.ComponentModel
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms680509%28v=vs.85%29.aspx


	/// <summary>Base interface for Component Object Model (COM).</summary>
	[ComImport]
	[Guid( "00000000-0000-0000-C000-000000000046" )]
	public interface IUnknown
	{

		/// <summary>Queries the supported COM interface on this instance.</summary>
		/// <param name="guid">The guid of the interface.</param>
		/// <param name="comObject">The output COM object reference.</param>
		/// <returns>Returns an HRESULT.</returns>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Required by implementation." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "Required by implementation." )]
		[PreserveSig]
		int QueryInterface(
			[In] ref Guid guid,
			[Out] out object comObject
		);


		/// <summary>Increments the reference count for an interface on this instance.</summary>
		/// <returns>Returns the new reference count.</returns>
		[PreserveSig]
		int AddRef();


		/// <summary>Decrements the reference count for an interface on this instance.</summary>
		/// <returns>Returns the new reference count.</returns>
		[PreserveSig]
		int Release();

	}

}