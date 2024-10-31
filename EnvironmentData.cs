using NUnit.Framework;

namespace Test.UI.SpecFlow
{
    internal static class EnvironmentData
    {
        public static string baseUrl { get; } = TestContext.Parameters["baseUrl"];
        public static string browser { get; } = TestContext.Parameters["browser"];
    }
}
