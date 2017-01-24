using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Camelot")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Camelot")]
[assembly: AssemblyCopyright("Copyright ©  2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]

[assembly: AssemblyVersion("0.1.0.0")]
[assembly: AssemblyFileVersion("0.1.0.0")]

#if DEBUG

// sn -p Key.snk PK.pk
// sn -tp PK.pk
[assembly: InternalsVisibleTo("Camelot.Core.Specs, PublicKey=" +
                              "0024000004800000940000000602000000240000525341310004000001000100418989e4fa0b0c" +
                              "e30eb905fe38cd5342bcbf5c0f72d1ee03efea44a2b12a4616edd2d2a742ae3129fb040dee7ae5" +
                              "601b00936b76c1f364bf1400afdd5d6ebb64f68893ea60ecfc381ff13265758b9eb68122a3cb79" +
                              "f1eb4dd873768bf0135848c83672d99c3d93ecac5680775f4f86be44a35ea39ffe44a6e4cdb1c5" +
                              "3776d0a0")]
#endif