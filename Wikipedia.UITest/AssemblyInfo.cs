using Browser.Core.Framework;
using NUnit.Framework;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Setting for running tests in parallel across test fixtures
[assembly: Parallelizable(ParallelScope.Fixtures)]

// Setting for running tests in parallel across test methods
//[assembly: Parallelizable(ParallelScope.Children)]

// Setting the max amount of tests to be run in parallel
[assembly: LevelOfParallelism(70)]

[assembly: ComVisible(false)]

