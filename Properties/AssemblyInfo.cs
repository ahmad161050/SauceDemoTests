// Enables parallel execution of test fixtures in NUnit.
// This allows multiple test classes to run simultaneously, improving execution time.

using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
