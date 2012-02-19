<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportComparer.aspx.cs" Inherits="ReportComparer" %>
<html>
<head>
    <meta charset="utf-8">
    <title>Load Test Report Comparison</title>
    <link rel="stylesheet" type="text/css" href="css/style.css" />
</head>
<body>
    <div id="container">
        <header>
            <div>
                <h1>
                    <span data-bind="text: perf.viewModel.ApplicationName"></span> -
                    <span data-bind="text: perf.viewModel.VersionNumber"></span><br />
                </h1>
                <span data-bind="text: perf.viewModel.VersionUrl"></span><br />
                <%--<span data-bind="text: new Date(parseInt(perf.viewModel.DateCreated().substr(6))).toString()"></span>--%>
            </div>
        </header>
        <div id="main" role="main">
            
            <div id="tabs">
                <ul data-bind="foreach: perf.viewModel.LoadTestRuns">
                    <li><a data-bind="text: TestName, attr: { href: '#' + TestName() }"></a></li>
                </ul>
                
                <div data-bind="foreach: perf.viewModel.LoadTestRuns">
                    <div data-bind="attr: { id: TestName }">
                        <div class="tab-content">
                            <h2><span data-bind="text: TestName"></span></h2>
                            <div class="flowGrid">
                                
                                <!-- RESULT INFO -->
                                <table cellpadding="0" cellspacing="0" border="1">
                                    <thead>
                                        <tr>
                                            <td>Test Name</td>
                                            <td>Start Time</td>
                                            <td>End Time</td>
                                            <td>Duration</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <span data-bind="text: TestName" />
                                            </td>
                                            <td>
                                                <span data-bind="text: StartTime" />
                                            </td>
                                            <td>
                                                <span data-bind="text: EndTime" />
                                            </td>
                                            <td>
                                                <span data-bind="text: Duration" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>    
                                
                                <div class="gridCell">
                                    <div data-bind="foreach: Requests">
                                        
                                        <table cellspacing="0" cellpadding="0" border="1">
                                            <thead>
                                                <tr>
                                                    <th colspan="2">
                                                        <span data-bind="text: Url"></span>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <th colspan="2" class="heading">
                                                        <h3>Request Anaylsis</h3>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <th width="50%">Total Requests</th>
                                                    <td>
                                                        <span data-bind="text: TotalRequests"></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th colspan="2" class="heading">
                                                        <h3>Response Time Analysis</h3>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <th width="50%">Avg Response Time (sec)</th>
                                                    <td>
                                                        <span data-bind="text: AverageResponseTime"></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        Max Response Time (sec)
                                                    </th>
                                                    <td>
                                                        <span data-bind="text: MaximumResponseTime"></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        Min Response Time (sec)
                                                    </th>
                                                    <td>
                                                        <span data-bind="text: MinimumResponseTime"></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        95% Response Time
                                                    </th>
                                                    <td>
                                                        <span data-bind="text: Percentile95ResponseTime"></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th colspan="2" class="heading">
                                                        <h3>Request Per Second Analysis</h3>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <th width="50%">Max Requests Per Second</th>
                                                    <td>
                                                        <span data-bind="text: MaximumRequestsPerSecond"></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        Min Requests Per Second
                                                    </th>
                                                    <td>
                                                        <span data-bind="text: MinimumRequestsPerSecond"></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        Average Requests Per Second
                                                    </th>
                                                    <td>
                                                        <span data-bind="text: AverageRequestsPerSecond"></span>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        
                                    </div>
                                  
                                </div>
                                
                                <div class="gridCell">
                                    <table class="graph">
                                        <thead>
                                            <tr>
                                                <td></td>
                                                <th>Minimum Response Time</th>
                                                <th>Average Response Time</th>
                                                <th>95% Response Time</th>
                                                <th>Maximum Response Time</th>
                                            </tr>
                                        </thead>
                                        <tbody data-bind="foreach: Requests">
                                            <tr>
                                                <th scope="row">
                                                    <span data-bind="text: Url"></span>
                                                </th>
                                                <td>
                                                    <span data-bind="text: MinimumResponseTime"></span>
                                                </td>
                                                <td>
                                                    <span data-bind="text: AverageResponseTime"></span>
                                                </td>
                                                <td>
                                                    <span data-bind="text: Percentile95ResponseTime"></span>
                                                </td>
                                                <td>
                                                    <span data-bind="text: MaximumResponseTime"></span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                
                            </div>	
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <footer>
        </footer>
    </div>
    <!--! end of #container -->

    <link href="http://cdn.wijmo.com/themes/rocket/jquery-wijmo.css" rel="stylesheet" type="text/css" />
    <link href="http://cdn.wijmo.com/jquery.wijmo-open.1.5.0.css" rel="stylesheet" type="text/css" />
    <link href="http://cdn.wijmo.com/jquery.wijmo-complete.1.5.0.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.min.js" type="text/javascript"></script>
    <script src="http://cdn.wijmo.com/external/jquery.bgiframe-2.1.3-pre.js" type="text/javascript"></script>
    <script src="http://cdn.wijmo.com/external/globalize.min.js" type="text/javascript"></script>
    <script src="http://cdn.wijmo.com/external/jquery.mousewheel.min.js" type="text/javascript"></script>
    <script src="http://cdn.wijmo.com/external/raphael.js" type="text/javascript"></script>
    <script src="http://cdn.wijmo.com/jquery.wijmo-open.1.5.0.min.js" type="text/javascript"></script>
    <script src="http://cdn.wijmo.com/jquery.wijmo-complete.1.5.0.min.js" type="text/javascript"></script>
    <script src="http://cloud.github.com/downloads/SteveSanderson/knockout/knockout-2.0.0rc.js" type="text/javascript"></script>
    <script src="https://raw.github.com/SteveSanderson/knockout.mapping/master/build/output/knockout.mapping-latest.js" type="text/javascript"></script>

    <script language="javascript">

        //load the querystring data
        var qs = (function (a) {
            if (a == "") return {};
            var b = {};
            for (var i = 0; i < a.length; ++i) {
                var p = a[i].split('=');
                if (p.length != 2) continue;
                b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
            }
            return b;
        })(window.location.search.substr(1).split('&'));


        var perf = {
            viewModel: ko.observable()
        };

        $(document).ready(function () {
            var leftReportModel = <%= LeftReportModel %>;
            var rightReportModel = <%= RightReportModel %>;
           perf.viewModel.left = ko.mapping.fromJS(leftReportModel);
           perf.viewModel.right = ko.mapping.fromJS(rightReportModel);
           ko.applyBindings(perf.viewModel);

           showCharts();

           $('#tabs').wijtabs();
        });

        function showCharts() {
            $('.graph').wijbarchart({
                showChartLabels: true,
                height: 300,
                legend: {
                    text: "Requests",
                    style: { fill: "#f1f1f1", stroke: "#010101" }
                },
                seriesStyles: [{
                    fill: "#8ede43", stroke: "#7fc73c", opacity: 0.8
                }],
                hint: {
                    content: function () {
                        return this.data.label + '\n' + this.x + ': ' + this.y + ' seconds.';
                    }
                },
                shadow: true
            });
        }
    </script>
</body>
</html>
