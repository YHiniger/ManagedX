using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


[assembly: AssemblyTitle( "ManagedX.dll" )]
[assembly: AssemblyDescription( "ManagedX.dll" )]
#if DEBUG
[assembly: AssemblyConfiguration( "Debug" )]
#else
[assembly: AssemblyConfiguration( "Release" )]
#endif
[assembly: AssemblyCompany( "" )]
[assembly: AssemblyProduct( "ManagedX" )]
[assembly: AssemblyCopyright( "Copyright © Yvan J.W. HINIGER 2014-2017" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]

[assembly: ComVisible( false )]
//[assembly: Guid( "d72e6229-6967-42c0-ab8b-5c89fbb22c07" )]

[assembly: AssemblyVersion( "0.8.6.0" )]
[assembly: AssemblyFileVersion( "0.8.6.0" )]
[assembly: NeutralResourcesLanguageAttribute( "en" )]

[assembly: CLSCompliant( true )]
[assembly: DefaultDependency( LoadHint.Always )]