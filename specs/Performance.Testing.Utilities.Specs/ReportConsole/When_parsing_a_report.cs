using System;
using Machine.Specifications;
using Performance.Testing.Utilities.ReportConsole.Framework.DataTransfer;
using Performance.Testing.Utilities.ReportConsole.Framework.Parsers;

namespace Performance.Testing.Utilities.Specs.FluentRequestSpecs.ReportConsole
{
    public class When_parsing_a_report
    {
        static IReportParser sut;
        const string ReportFilePath = "ExampleReport.trx.xml";
        static Report result;

        Establish context = () =>
            {
                sut = new ReportParser();
            };

        Because of = () => result = sut.Parse(ReportFilePath, "Congress");

        It should_map_the_load_test_runs = () => result.ShouldNotBeNull();
    }

    public class When_attempting_to_parse_a_report_which_does_not_exist
    {
        static IReportParser sut;
        const string ReportFilePath = "unknownfile.trx";
        static Report result;

        Establish context = () =>
        {
            sut = new ReportParser();
        };

        Because of = () => ex = Catch.Exception(() => { result = sut.Parse(ReportFilePath, string.Empty); });

        It should_fail = () => ex.ShouldNotBeNull();
        static Exception ex;
    }

    public class When_attempting_to_parse_a_report_by_supplying_an_empty_target_report_path_location
    {
        static IReportParser sut;
        const string ReportFilePath = "";
        static Report result;

        Establish context = () =>
        {
            sut = new ReportParser();
        };

        Because of = () => ex = Catch.Exception(() => { result = sut.Parse(ReportFilePath, string.Empty); });

        It should_fail = () => ex.ShouldNotBeNull();
        static Exception ex;
    }
}