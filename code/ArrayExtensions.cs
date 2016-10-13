using System;


namespace ManagedX
{

	/// <summary></summary>
	public static class ArrayExtensions
	{

		/// <summary>Returns the address of a structure.</summary>
		/// <typeparam name="T">Structure type.</typeparam>
		/// <param name="structure">A reference to the structure whose address is to be retrieved.</param>
		/// <returns>Returns the address of the specified structure.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#" )]
		public unsafe static IntPtr GetAddressOf<T>( ref T structure )
			where T : struct
		{
			var reference = __makeref(structure);
			return *(IntPtr*)( &reference );
		}


		/// <summary>Returns the address of an element from an array.</summary>
		/// <typeparam name="T">Structure type.</typeparam>
		/// <param name="array">The array.</param>
		/// <param name="index">The zero-based index of the element whose address is to be retrieved.</param>
		/// <returns>Returns the address of the specified element, or <see cref="IntPtr.Zero"/> if the <paramref name="array"/> is null.</returns>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public static IntPtr GetAddress<T>( this T[] array, int index )
			where T : struct
		{
			if( array == null )
				return IntPtr.Zero;

			if( index < 0 || index >= array.Length )
				throw new ArgumentOutOfRangeException( "index" );

			return GetAddressOf( ref array[ index ] );
		}

	}

}