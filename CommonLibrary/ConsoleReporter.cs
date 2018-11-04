using System;
using TSQLLint.Common;

namespace CommonLibrary
{
    public class ConsoleReporter : IReporter
    {
        private int warningCount;
        private int errorCount;
        public void Report(string message)
        {
            NonBlockingConsole.WriteLine(message);
        }

        public void ReportFileResults()
        {
        }

        public void ReportResults(TimeSpan timespan, int fileCount)
        {
            Report($"\nLinted {fileCount} files in {timespan.TotalSeconds} seconds\n\n{errorCount} Errors.\n{warningCount} Warnings");
        }

        public void ReportViolation(IRuleViolation violation)
        {
            switch (violation.Severity)
            {
                case RuleViolationSeverity.Warning:
                    warningCount++;
                    break;
                case RuleViolationSeverity.Error:
                    errorCount++;
                    break;
                default:
                    return;
            }

            ReportViolation(
                violation.FileName,
                violation.Line.ToString(),
                violation.Column.ToString(),
                violation.Severity.ToString().ToLowerInvariant(),
                violation.RuleName,
                violation.Text);
        }

        public void ReportViolation(string fileName, string line, string column, string severity, string ruleName, string violationText)
        {
            Report($"{fileName}({line},{column}): {severity} {ruleName} : {violationText}.");
        }
    }
}
