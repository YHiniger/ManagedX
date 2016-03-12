using System;


namespace ManagedX.Design
{

	/// <summary>Specifies the definition location and native name of a managed type.</summary>
	[Serializable]
	[AttributeUsage( AttributeTargets.Class | AttributeTargets.Delegate | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct, Inherited = false, AllowMultiple = true )]
	public sealed class NativeAttribute : Attribute
	{

		private string definitionLocation;
		private string nativeName;



		/// <summary>Initializes a new <see cref="NativeAttribute"/>.</summary>
		/// <param name="definitionLocation">The location of the native definition of the associated type.</param>
		/// <param name="nativeName">The native name of the associated type.</param>
		public NativeAttribute( string definitionLocation, string nativeName )
			: base()
		{
			this.definitionLocation = definitionLocation ?? string.Empty;
			this.nativeName = nativeName ?? string.Empty;
		}


		/// <summary>Initializes a new <see cref="NativeAttribute"/>.</summary>
		/// <param name="definitionLocation">The location of the native definition of the associated type.</param>
		public NativeAttribute( string definitionLocation )
			: this( definitionLocation, string.Empty )
		{
		}



		/// <summary>Gets the location of the definition of the associated type (ie: "WinGDI.h").</summary>
		public string DefinitionLocation { get { return string.Copy( definitionLocation ); } }


		/// <summary>Gets the native name of the associated type, or an empty string if the managed name is the same as the native name.</summary>
		public string NativeName { get { return string.Copy( nativeName ); } }

	}

}