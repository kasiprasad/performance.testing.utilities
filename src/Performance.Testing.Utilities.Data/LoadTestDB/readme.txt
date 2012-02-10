LoadTestDB\Scripts 
	- a series of sql scripts which need to be executed against your 
	Visual Studio 2010 Load Test Database these scripts will create 
	stored procedures which this console application will require for
	anayltic report generation

LoadTestDB\LoadTestingDatabase.dbml
	- linq to sql classes mapped to the Visual Studio 2010 Load Test Database 
	custom stored procedures provided in the Scripts directory


In order to run the ReportConsole applicatio, you must install the SQL Scripts
into your Visual Studio 2010 Load Test Database and 
ensure the App.Config, Connection String - 

Performance.Testing.Utilities.ReportConsole.Properties
.Settings.LoadTest2010ConnectionString 

has been defined and pointed at your Visual Studio 2010 Load Test database


			