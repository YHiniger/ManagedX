using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


// Les informations générales relatives à un assembly dépendent de 
// l'ensemble d'attributs suivant. Changez les valeurs de ces attributs pour modifier les informations
// associées à un assembly.
[assembly: AssemblyTitle( "ManagedX.dll" )]
[assembly: AssemblyDescription( "ManagedX.dll" )]
#if DEBUG
[assembly: AssemblyConfiguration( "Debug" )]
#else
[assembly: AssemblyConfiguration( "Release" )]
#endif
[assembly: AssemblyCompany( "" )]
[assembly: AssemblyProduct( "ManagedX" )]
[assembly: AssemblyCopyright( "Copyright © Yvan J.W. HINIGER 2015" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]

// L'affectation de la valeur false à ComVisible rend les types invisibles dans cet assembly 
// aux composants COM. Si vous devez accéder à un type dans cet assembly à partir de 
// COM, affectez la valeur true à l'attribut ComVisible sur ce type.
[assembly: ComVisible( false )]

// Le GUID suivant est pour l'ID de la typelib si ce projet est exposé à COM
[assembly: Guid( "d72e6229-6967-42c0-ab8b-5c89fbb22c07" )]

// Les informations de version pour un assembly se composent des quatre valeurs suivantes :
//
//      Version principale
//      Version secondaire 
//      Numéro de build
//      Révision
//
// Vous pouvez spécifier toutes les valeurs ou indiquer les numéros de build et de révision par défaut 
// en utilisant '*', comme indiqué ci-dessous :
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion( "0.5.0.0" )]
[assembly: AssemblyFileVersion( "0.5.0.0" )]

// L'anglais est la langue par défaut.
[assembly: NeutralResourcesLanguageAttribute( "en" )]

// Tous les types exportés sont conformes au Common Language System (CLS):
[assembly: CLSCompliant( true )]

// 
[assembly: DefaultDependency( LoadHint.Always )]