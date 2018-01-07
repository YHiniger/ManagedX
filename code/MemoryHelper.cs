using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX
{

	/// <summary>Provides functions to copy memory blocks.</summary>
	public static class MemoryHelper
	{

		[System.Security.SuppressUnmanagedCodeSecurity]
		private static class SafeNativeMethods
		{

			internal const string LibraryName = "MSVCRT.dll";


			/// <summary>Copies bytes between buffers. This is a version of memcpy with security enhancements as described in Security Features in the CRT.</summary>
			/// <param name="dest">New buffer.</param>
			/// <param name="destSize">Size of the destination buffer, in bytes.</param>
			/// <param name="src">Buffer to copy from.</param>
			/// <param name="count">Number of bytes to copy.</param>
			/// <returns>Returns zero if successful; an error code on failure.</returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/wes2t00f.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, PreserveSig = true )]
#pragma warning disable IDE1006, CS208
			internal static extern int memcpy_s(
				[In] IntPtr dest,
				[In] UIntPtr destSize,
				[In] IntPtr src,
				[In] UIntPtr count
			);
#pragma warning restore IDE1006, CS208

		}



		/// <summary>Copies bytes between buffers.</summary>
		/// <param name="source">A pointer to the buffer to copy from.</param>
		/// <param name="destination">A pointer to the destination buffer.</param>
		/// <param name="count">The number of bytes to copy; must be greater than zero, and less than or equal to the size of the <paramref name="destination"/> buffer.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		/// <exception cref="InvalidOperationException"/>
		[SuppressMessage( "Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "memcpys" )]
		public static void Copy( IntPtr source, IntPtr destination, int count )
		{
			if( source == IntPtr.Zero )
				throw new ArgumentNullException( "source" );

			if( destination == IntPtr.Zero )
				throw new ArgumentNullException( "destination" );

			if( destination == source )
				throw new ArgumentException( "Can't copy to self.", "destination" );

			if( count <= 0 )
				throw new ArgumentOutOfRangeException( "count" );

			ulong d, s;
			unchecked
			{
				d = (ulong)destination;
				s = (ulong)source;
			}
			var spacing = d > s ? d - s : s - d;
			if( spacing < (ulong)count )
				throw new ArgumentException( "Source and destination overlap.", "destination" );

			var c = (UIntPtr)count;
			var errorCode = SafeNativeMethods.memcpy_s( destination, c, source, c );
			if( errorCode != 0 )
				throw new InvalidOperationException( SafeNativeMethods.LibraryName + " : memcpy_s", Marshal.GetExceptionForHR( errorCode ) );
		}


		/// <summary>Copies bytes between buffers.</summary>
		/// <param name="source">A pointer to the buffer to copy from.</param>
		/// <param name="destination">A pointer to the destination buffer.</param>
		/// <param name="count">The number of bytes to copy; must be greater than zero, and less than or equal to the size of the <paramref name="destination"/> buffer.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentOutOfRangeException"/>
		/// <exception cref="InvalidOperationException"/>
		[SuppressMessage( "Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "memcpys" )]
		public static void Copy( IntPtr source, IntPtr destination, long count )
		{
			if( source == IntPtr.Zero )
				throw new ArgumentNullException( "source" );

			if( destination == IntPtr.Zero )
				throw new ArgumentNullException( "destination" );

			if( destination == source )
				throw new ArgumentException( "Can't copy to self.", "destination" );

			if( count <= 0 )
				throw new ArgumentOutOfRangeException( "count" );

			ulong d, s;
			unchecked
			{
				d = (ulong)destination;
				s = (ulong)source;
			}
			var spacing = d > s ? d - s : s - d;
			if( spacing < (ulong)count )
				throw new ArgumentException( "Source and destination overlap.", "destination" );

			var c = (UIntPtr)count;
			var errorCode = SafeNativeMethods.memcpy_s( destination, c, source, c );
			if( errorCode != 0 )
				throw new InvalidOperationException( SafeNativeMethods.LibraryName + " : memcpy_s", Marshal.GetExceptionForHR( errorCode ) );
		}

	}

}