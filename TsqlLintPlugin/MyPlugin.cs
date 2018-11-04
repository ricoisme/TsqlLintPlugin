using System;
using TSQLLint.Common;

namespace TsqlLintPlugin
{
    public sealed class MyPlugin : IPlugin
    {
        sealed class Pattern
        {
            public const string sp = @"^\s*[^(--)]{0}(CREATE +PROCEDURE|ALTER +PROCEDURE|CREATE OR ALTER +PROCEDURE|CREATE +PORC|ALTER +PORC|CREATE OR ALTER +PROC)\s+(?<ParentProcName>(\w|_|\[|\]|\.)*)";
            public const string fn = @"^\s*[^(--)]{0}(CREATE +FUNCTION|ALTER +FUNCTION|CREATE OR ALTER +FUNCTION)\s+(?<ParentName>(\w|_|\[|\]|\.)*)";
            public const string ut = @"^\s*[^(--)]{0}(CREATE +TYPE)\s+(?<ParentName>(\w|_|\[|\]|\.)*)";
            public const string uv = @"^\s*[^(--)]{0}(CREATE +VIEW|ALTER +VIEW|CREATE OR ALTER +VIEW)\s+(?<ParentName>(\w|_|\[|\]|\.)*)";
            public const string ix = @"^\s*[^(--)]{0}(CREATE +INDEX|ALTER +INDEX|CREATE +CLUSTERED +INDEX)\s+(?<ParentName>(\w|_|\[|\]|\.)*)";
            public const string tri = @"^\s*[^(--)]{0}(CREATE +TRIGGER|ALTER +TRIGGER|CREATE OR ALTER +TRIGGER)\s+(?<ParentName>(\w|_|\[|\]|\.)*)";
            public const string seq = @"^\s*[^(--)]{0}(CREATE +SEQUENCE)\s+(?<ParentName>(\w|_|\[|\]|\.)*)";
        }

        IPluginContext _pluginContext;
        IReporter _reporter;

        void IPlugin.PerformAction(IPluginContext context, IReporter reporter)
        {
            _pluginContext = context;
            _reporter = reporter;
            string line;
            var lineNumber = 0;
            while ((line = context.FileContents.ReadLine()) != null)
            {
                lineNumber++;
                #region for store procedure
                var SpItems = ParseString.GetNames(Pattern.sp, line, ErrorNameType.sp_);
                if (SpItems.schema.Length > 0 ||
                    SpItems.objName.Length > 0)
                {
                    TwoPartNameCheck(SpItems.schema, line, lineNumber);
                    if (!SpItems.objName.StartsWith("usp_", StringComparison.Ordinal))
                    {
                        var column = line.IndexOf(SpItems.objName);
                        _reporter.ReportViolation(new CustomeRuleViolation(
                            _pluginContext.FilePath,
                            "prefer-usp_ perfix and lowercase",
                            "Should use usp_ stored procedure prefix and lowercase",
                            lineNumber,
                            column,
                            RuleViolationSeverity.Error));
                    }
                    continue;
                }
                #endregion

                #region for user function
                var fuItems = ParseString.GetNames(Pattern.fn, line, ErrorNameType.uf_);
                if (fuItems.schema.Length > 0 ||
                     fuItems.objName.Length > 0)
                {
                    TwoPartNameCheck(fuItems.schema, line, lineNumber);
                    if (!fuItems.objName.StartsWith("ufn_", StringComparison.Ordinal))
                    {
                        var column = line.IndexOf(fuItems.objName);
                        _reporter.ReportViolation(new CustomeRuleViolation(
                            _pluginContext.FilePath,
                            "prefer-ufn_ perfix and lowercase",
                            "Should use ufn_ user function prefix and lowercase",
                            lineNumber,
                            column,
                            RuleViolationSeverity.Error));
                    }
                    continue;
                }
                #endregion

                #region for user type
                var utItems = ParseString.GetNames(Pattern.ut, line, ErrorNameType.ubt_);
                if (utItems.schema.Length > 0 ||
                     utItems.objName.Length > 0)
                {
                    TwoPartNameCheck(utItems.schema, line, lineNumber);
                    if (!utItems.objName.StartsWith("ut_", StringComparison.Ordinal))
                    {
                        var column = line.IndexOf(utItems.objName);
                        _reporter.ReportViolation(new CustomeRuleViolation(
                            _pluginContext.FilePath,
                            "prefer-ut_ perfix and lowercase",
                            "Should use ut_ user Type prefix and lowercase",
                            lineNumber,
                            column,
                            RuleViolationSeverity.Error));
                    }
                    continue;
                }
                #endregion

                #region for View
                var uvItems = ParseString.GetNames(Pattern.uv, line, ErrorNameType.uvv_);
                if (uvItems.schema.Length > 0 ||
                    uvItems.objName.Length > 0)
                {
                    TwoPartNameCheck(uvItems.schema, line, lineNumber);
                    if (!uvItems.objName.StartsWith("uv_", StringComparison.Ordinal))
                    {
                        var column = line.IndexOf(uvItems.objName);
                        _reporter.ReportViolation(new CustomeRuleViolation(
                            _pluginContext.FilePath,
                            "prefer-uv_ perfix and lowercase",
                            "Should use uv_ view prefix and lowercase",
                            lineNumber,
                            column,
                            RuleViolationSeverity.Error));
                    }
                    continue;
                }
                #endregion

                #region for Index
                var ixItems = ParseString.GetNames(Pattern.ix, line, ErrorNameType.idx_);
                if (ixItems.schema.Length > 0 ||
                    ixItems.objName.Length > 0)
                {
                    if (!ixItems.objName.StartsWith("ix_", StringComparison.Ordinal))
                    {
                        var column = line.IndexOf(ixItems.objName);
                        _reporter.ReportViolation(new CustomeRuleViolation(
                            _pluginContext.FilePath,
                            "prefer-ix_ perfix and lowercase",
                            "Should use ix_ index prefix and lowercase",
                            lineNumber,
                            column,
                            RuleViolationSeverity.Error));
                    }
                    continue;
                }
                #endregion

                #region for Trigger
                var triItems = ParseString.GetNames(Pattern.tri, line, ErrorNameType.trr_);
                if (triItems.schema.Length > 0 ||
                   triItems.objName.Length > 0)
                {
                    TwoPartNameCheck(triItems.schema, line, lineNumber);
                    if (!triItems.objName.StartsWith("tri_", StringComparison.Ordinal))
                    {
                        var column = line.IndexOf(triItems.objName);
                        _reporter.ReportViolation(new CustomeRuleViolation(
                            _pluginContext.FilePath,
                            "prefer-tri_ perfix and lowercase",
                            "Should use tri_ trigger prefix and lowercase",
                            lineNumber,
                            column,
                            RuleViolationSeverity.Error));
                    }
                    continue;
                }
                #endregion

                #region for sequence
                var seqItems = ParseString.GetNames(Pattern.seq, line, ErrorNameType.seqq_);
                if (seqItems.schema.Length > 0 ||
                    seqItems.objName.Length > 0)
                {
                    TwoPartNameCheck(seqItems.schema, line, lineNumber);
                    if (!seqItems.objName.StartsWith("seq_", StringComparison.Ordinal))
                    {
                        var column = line.IndexOf(seqItems.objName);
                        _reporter.ReportViolation(new CustomeRuleViolation(
                            _pluginContext.FilePath,
                            "prefer-seq_ perfix and lowercase",
                            "Should use seq_ sequence prefix and lowercase",
                            lineNumber,
                            column,
                            RuleViolationSeverity.Error));
                    }
                }
                #endregion

            }
        }

        void TwoPartNameCheck(string schema, string line, int lineNumber)
        {
            if (!schema.StartsWith("dbo", StringComparison.Ordinal))
            {
                var column = line.IndexOf(schema);
                _reporter.ReportViolation(new CustomeRuleViolation(
                    _pluginContext.FilePath,
                    "two-part name and lowercase",
                    "Should use dbo schema and lowercase",
                    lineNumber,
                    column,
                    RuleViolationSeverity.Error));
            }
        }
    }

    internal sealed class CustomeRuleViolation : IRuleViolation
    {
        public int Column { get; private set; }
        public string FileName { get; private set; }
        public int Line { get; private set; }
        public string RuleName { get; private set; }
        public RuleViolationSeverity Severity { get; private set; }
        public string Text { get; private set; }
        public CustomeRuleViolation(string fileName, string ruleName, string text, int lineNumber, int column, RuleViolationSeverity ruleViolationSeverity)
        {
            FileName = fileName;
            RuleName = ruleName;
            Text = text;
            Line = lineNumber;
            Column = column;
            Severity = ruleViolationSeverity;
        }

    }
}
