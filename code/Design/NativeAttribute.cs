using System;
using System.Runtime.InteropServices;


namespace ManagedX.Win32
{
	// FIXME - move to ManagedX.Design namespace !


	/// <summary>An attribute to specify the definition location and native name of a managed type.
	/// <para>In a future implementation of ManagedX, this attribute should help tracking native API changes.</para>
	/// </summary>
	[Serializable]
	[ComVisible( false )]
	[AttributeUsage( AttributeTargets.Class | AttributeTargets.Delegate | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Field, Inherited = false, AllowMultiple = true )]
	public sealed class NativeAttribute : Attribute
	{

		private string fileName;
		private string typeName;



		/// <summary>Initializes a new <see cref="NativeAttribute"/>.</summary>
		/// <param name="definitionFileName">The location of the native definition of the associated type (ie: WinUser.h).</param>
		/// <param name="nativeName">The native name of the associated type.</param>
		public NativeAttribute( string definitionFileName, string nativeName )
			: base()
		{
			this.fileName = definitionFileName ?? string.Empty;
			this.typeName = nativeName ?? string.Empty;
		}


		/// <summary>Initializes a new <see cref="NativeAttribute"/>.</summary>
		/// <param name="definitionFileName">The location (file name) of the native definition of the associated type (ie: WinUser.h).</param>
		public NativeAttribute( string definitionFileName )
			: this( definitionFileName, string.Empty )
		{
		}



		/// <summary>Gets the name of the file holding the definition of the associated type.
		/// <para>For values, this should be empty unless the value doesn't come from the same file as the type declaring the value.</para>
		/// </summary>
		public string DefinitionFileName { get { return string.Copy( fileName ); } }


		/// <summary>Gets the native name of the associated type, or an empty string if the type has no real native equivalent.</summary>
		public string NativeName { get { return string.Copy( typeName ); } }

	}

}