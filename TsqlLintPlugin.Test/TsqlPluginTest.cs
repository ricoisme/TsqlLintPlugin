using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommonLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TSQLLint.Common;


namespace TsqlLintPlugin.Test
{
    [TestClass]
    public class TsqlPluginTest
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly string _testDirectoryPath;
        private readonly IConfigurationRoot _configurationRoot;
        public TsqlPluginTest()
        {
            _serviceProvider = new ServiceCollection().BuildServiceProvider();
            _testDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles");
            _configurationRoot = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .Build();
        }

        [DataTestMethod]
        [DataRow("usp_GetCurrency.sql", DisplayName = "usp_GetCurrency.sql")]
        [DataRow("usp_GetCurrency1.sql", DisplayName = "usp_GetCurrency1.sql")]
        [DataRow("usp_GetCurrency2.sql", DisplayName = "usp_GetCurrency2.sql")]
        [DataRow("usp_GetCurrency3.sql", DisplayName = "usp_GetCurrency3.sql")]
        [Timeout(8 * 1000)]
        public void TestStoreProcedure(string testFile)
        {
            var currentConsoleOut = Console.Out;
            string path = Path.Combine(_testDirectoryPath, testFile);
            var ruleExceptions = new List<IRuleException>();
            var reporter = new ConsoleReporter();
            var startTime = DateTime.Now;
            using (TextReader textReader = File.OpenText(path))
            {
                var pluginContext = new PluginContext(path, ruleExceptions, textReader);
                IPlugin plugin = new MyPlugin();
                plugin.PerformAction(pluginContext, reporter);
            }
            var endtime = DateTime.Now;
            var duration = startTime - endtime;

            using (var consoleOutput = new ConsoleOutput())
            {
                reporter.ReportResults(duration, 4);
                NonBlockingConsole.Consumer();
                var consoleOutputValues = consoleOutput.GetOuput().Split('\n');
                var result = consoleOutputValues?.Where(x => x.Trim().EndsWith("Errors."))
                    .FirstOrDefault();

                switch (testFile)
                {
                    case "usp_GetCurrency1.sql":
                        Assert.AreEqual("2 Errors.", result);
                        break;
                    case "usp_GetCurrency2.sql":
                        Assert.AreEqual("0 Errors.", result);
                        break;
                    case "usp_GetCurrency.sql":
                        Assert.AreEqual("1 Errors.", result);
                        break;
                    case "usp_GetCurrency3.sql":
                        Assert.AreEqual("2 Errors.", result);
                        break;
                }
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [DataTestMethod]
        [DataRow("uf_GetBalance.sql", DisplayName = "uf_GetBalance.sql")]
        [DataRow("uf_GetBalance1.sql", DisplayName = "uf_GetBalance1.sql")]
        [DataRow("uf_GetBalance2.sql", DisplayName = "uf_GetBalance2.sql")]
        [Timeout(8 * 1000)]
        public void TestFunction(string testFile)
        {
            var currentConsoleOut = Console.Out;
            string path = Path.Combine(_testDirectoryPath, testFile);
            var ruleExceptions = new List<IRuleException>();
            var reporter = new ConsoleReporter();
            var startTime = DateTime.Now;
            using (TextReader textReader = File.OpenText(path))
            {
                var pluginContext = new PluginContext(path, ruleExceptions, textReader);
                IPlugin plugin = new MyPlugin();
                plugin.PerformAction(pluginContext, reporter);
            }
            var endtime = DateTime.Now;
            var duration = startTime - endtime;

            using (var consoleOutput = new ConsoleOutput())
            {
                reporter.ReportResults(duration, 3);
                NonBlockingConsole.Consumer();
                var consoleOutputValues = consoleOutput.GetOuput().Split('\n');
                var result = consoleOutputValues?.Where(x => x.Trim().EndsWith("Errors."))
                    .FirstOrDefault();

                switch (testFile)
                {
                    case "uf_GetBalance.sql":
                    case "uf_GetBalance1.sql":
                    case "uf_GetBalance2.sql":
                        Assert.AreEqual("1 Errors.", result);
                        break;
                }
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [DataTestMethod]
        [DataRow("Bank_ix_BankNumber.sql", DisplayName = "Bank_ix_BankNumber.sql")]
        [DataRow("Player_IX_Name_Currency.sql", DisplayName = "Player_IX_Name_Currency.sql")]
        [DataRow("Wallet_IX_Balance.sql", DisplayName = "Wallet_IX_Balance.sql")]
        [Timeout(8 * 1000)]
        public void TestIndex(string testFile)
        {
            var currentConsoleOut = Console.Out;
            string path = Path.Combine(_testDirectoryPath, testFile);
            var ruleExceptions = new List<IRuleException>();
            var reporter = new ConsoleReporter();
            var startTime = DateTime.Now;
            using (TextReader textReader = File.OpenText(path))
            {
                var pluginContext = new PluginContext(path, ruleExceptions, textReader);
                IPlugin plugin = new MyPlugin();
                plugin.PerformAction(pluginContext, reporter);
            }
            var endtime = DateTime.Now;
            var duration = startTime - endtime;

            using (var consoleOutput = new ConsoleOutput())
            {
                reporter.ReportResults(duration, 3);
                NonBlockingConsole.Consumer();
                var consoleOutputValues = consoleOutput.GetOuput().Split('\n');
                var result = consoleOutputValues?.Where(x => x.Trim().EndsWith("Errors."))
                    .FirstOrDefault();

                switch (testFile)
                {
                    case "Bank_ix_BankNumber.sql":
                        Assert.AreEqual("0 Errors.", result);
                        break;
                    case "Player_IX_Name_Currency.sql":
                    case "Wallet_IX_Balance.sql":
                        Assert.AreEqual("1 Errors.", result);
                        break;
                }
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [DataTestMethod]
        [DataRow("uv_Test.sql", DisplayName = "uv_Test.sql")]
        [DataRow("uvv_Test.sql", DisplayName = "uvv_Test.sql")]
        [Timeout(8 * 1000)]
        public void TestView(string testFile)
        {
            var currentConsoleOut = Console.Out;
            string path = Path.Combine(_testDirectoryPath, testFile);
            var ruleExceptions = new List<IRuleException>();
            var reporter = new ConsoleReporter();
            var startTime = DateTime.Now;
            using (TextReader textReader = File.OpenText(path))
            {
                var pluginContext = new PluginContext(path, ruleExceptions, textReader);
                IPlugin plugin = new MyPlugin();
                plugin.PerformAction(pluginContext, reporter);
            }
            var endtime = DateTime.Now;
            var duration = startTime - endtime;

            using (var consoleOutput = new ConsoleOutput())
            {
                reporter.ReportResults(duration, 2);
                NonBlockingConsole.Consumer();
                var consoleOutputValues = consoleOutput.GetOuput().Split('\n');
                var result = consoleOutputValues?.Where(x => x.Trim().EndsWith("Errors."))
                    .FirstOrDefault();

                switch (testFile)
                {
                    case "uv_Test.sql":
                        Assert.AreEqual("0 Errors.", result);
                        break;
                    case "uvv_Test.sql":
                        Assert.AreEqual("2 Errors.", result);
                        break;
                }
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [DataTestMethod]
        [DataRow("ut_UpdatedWallet.sql", DisplayName = "ut_UpdatedWallet.sql")]
        [DataRow("ut_Test2.sql", DisplayName = "ut_Test2.sql")]
        [Timeout(8 * 1000)]
        public void TestType(string testFile)
        {
            var currentConsoleOut = Console.Out;
            string path = Path.Combine(_testDirectoryPath, testFile);
            var ruleExceptions = new List<IRuleException>();
            var reporter = new ConsoleReporter();
            var startTime = DateTime.Now;
            using (TextReader textReader = File.OpenText(path))
            {
                var pluginContext = new PluginContext(path, ruleExceptions, textReader);
                IPlugin plugin = new MyPlugin();
                plugin.PerformAction(pluginContext, reporter);
            }
            var endtime = DateTime.Now;
            var duration = startTime - endtime;

            using (var consoleOutput = new ConsoleOutput())
            {
                reporter.ReportResults(duration, 2);
                NonBlockingConsole.Consumer();
                var consoleOutputValues = consoleOutput.GetOuput().Split('\n');
                var result = consoleOutputValues?.Where(x => x.Trim().EndsWith("Errors."))
                    .FirstOrDefault();

                switch (testFile)
                {
                    case "ut_UpdatedWallet.sql":
                        Assert.AreEqual("0 Errors.", result);
                        break;
                    case "ut_Test2.sql":
                        Assert.AreEqual("2 Errors.", result);
                        break;
                }
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [DataTestMethod]
        [DataRow("Bank_tr_Test.sql", DisplayName = "Bank_tr_Test.sql")]
        [DataRow("Bank_tri_Test.sql", DisplayName = "Bank_tri_Test.sql")]
        [DataRow("Bank_tri_Test1.sql", DisplayName = "Bank_tri_Test1.sql")]
        [Timeout(8 * 1000)]
        public void TestTrigger(string testFile)
        {
            var currentConsoleOut = Console.Out;
            string path = Path.Combine(_testDirectoryPath, testFile);
            var ruleExceptions = new List<IRuleException>();
            var reporter = new ConsoleReporter();
            var startTime = DateTime.Now;
            using (TextReader textReader = File.OpenText(path))
            {
                var pluginContext = new PluginContext(path, ruleExceptions, textReader);
                IPlugin plugin = new MyPlugin();
                plugin.PerformAction(pluginContext, reporter);
            }
            var endtime = DateTime.Now;
            var duration = startTime - endtime;

            using (var consoleOutput = new ConsoleOutput())
            {
                reporter.ReportResults(duration, 3);
                NonBlockingConsole.Consumer();
                var consoleOutputValues = consoleOutput.GetOuput().Split('\n');
                var result = consoleOutputValues?.Where(x => x.Trim().EndsWith("Errors."))
                    .FirstOrDefault();

                switch (testFile)
                {
                    case "Bank_tr_Test.sql":
                        Assert.AreEqual("1 Errors.", result);
                        break;
                    case "Bank_tri_Test.sql":
                    case "Bank_tri_Test1.sql":
                        Assert.AreEqual("0 Errors.", result);
                        break;
                }
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [DataTestMethod]
        [DataRow("seq_A.sql", DisplayName = "seq_A.sql")]
        [DataRow("seq_B.sql", DisplayName = "seq_B.sql")]
        [DataRow("seq_C.sql", DisplayName = "seq_C.sql")]
        [Timeout(8 * 1000)]
        public void TestSequence(string testFile)
        {
            var currentConsoleOut = Console.Out;
            string path = Path.Combine(_testDirectoryPath, testFile);
            var ruleExceptions = new List<IRuleException>();
            var reporter = new ConsoleReporter();
            var startTime = DateTime.Now;
            using (TextReader textReader = File.OpenText(path))
            {
                var pluginContext = new PluginContext(path, ruleExceptions, textReader);
                IPlugin plugin = new MyPlugin();
                plugin.PerformAction(pluginContext, reporter);
            }
            var endtime = DateTime.Now;
            var duration = startTime - endtime;

            using (var consoleOutput = new ConsoleOutput())
            {
                reporter.ReportResults(duration, 3);
                NonBlockingConsole.Consumer();
                var consoleOutputValues = consoleOutput.GetOuput().Split('\n');
                var result = consoleOutputValues?.Where(x => x.Trim().EndsWith("Errors."))
                    .FirstOrDefault();

                switch (testFile)
                {
                    case "seq_A.sql":
                        Assert.AreEqual("1 Errors.", result);
                        break;
                    case "seq_B.sql":
                        Assert.AreEqual("2 Errors.", result);
                        break;
                    case "seq_C.sql":
                        Assert.AreEqual("0 Errors.", result);
                        break;
                }
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
    }
}
