﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Performance.Testing.Utilities.Data.LoadTestDB
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="LoadTest2010")]
	public partial class LoadTestingDatabaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public LoadTestingDatabaseDataContext() : 
				base(global::Performance.Testing.Utilities.Data.Properties.Settings.Default.LoadTest2010ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public LoadTestingDatabaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LoadTestingDatabaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LoadTestingDatabaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LoadTestingDatabaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.Reporting_GetRequestSummary")]
		public ISingleResult<Reporting_GetRequestSummaryResult> Reporting_GetRequestSummary([global::System.Data.Linq.Mapping.ParameterAttribute(Name="LoadTestRunId", DbType="Int")] System.Nullable<int> loadTestRunId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), loadTestRunId);
			return ((ISingleResult<Reporting_GetRequestSummaryResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.Reporting_GetRequestsBySec")]
		public ISingleResult<Reporting_GetRequestsBySecResult> Reporting_GetRequestsBySec([global::System.Data.Linq.Mapping.ParameterAttribute(Name="LoadTestRunId", DbType="Int")] System.Nullable<int> loadTestRunId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), loadTestRunId);
			return ((ISingleResult<Reporting_GetRequestsBySecResult>)(result.ReturnValue));
		}
	}
	
	public partial class Reporting_GetRequestSummaryResult
	{
		
		private string _TestCaseName;
		
		private string _ScenarioName;
		
		private string _RequestUri;
		
		private int _TotalRequests;
		
		private double _AverageResponseTime;
		
		private double _MinimumResponseTime;
		
		private double _MaximumResponseTime;
		
		private System.Nullable<double> _Percentile90;
		
		private System.Nullable<double> _Percentile95;
		
		private System.Nullable<double> _Percentile99;
		
		private System.Nullable<double> _Median;
		
		private System.Nullable<double> _StandardDeviation;
		
		public Reporting_GetRequestSummaryResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TestCaseName", DbType="NVarChar(64) NOT NULL", CanBeNull=false)]
		public string TestCaseName
		{
			get
			{
				return this._TestCaseName;
			}
			set
			{
				if ((this._TestCaseName != value))
				{
					this._TestCaseName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScenarioName", DbType="NVarChar(64) NOT NULL", CanBeNull=false)]
		public string ScenarioName
		{
			get
			{
				return this._ScenarioName;
			}
			set
			{
				if ((this._ScenarioName != value))
				{
					this._ScenarioName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RequestUri", DbType="NVarChar(2048) NOT NULL", CanBeNull=false)]
		public string RequestUri
		{
			get
			{
				return this._RequestUri;
			}
			set
			{
				if ((this._RequestUri != value))
				{
					this._RequestUri = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TotalRequests", DbType="Int NOT NULL")]
		public int TotalRequests
		{
			get
			{
				return this._TotalRequests;
			}
			set
			{
				if ((this._TotalRequests != value))
				{
					this._TotalRequests = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AverageResponseTime", DbType="Float NOT NULL")]
		public double AverageResponseTime
		{
			get
			{
				return this._AverageResponseTime;
			}
			set
			{
				if ((this._AverageResponseTime != value))
				{
					this._AverageResponseTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MinimumResponseTime", DbType="Float NOT NULL")]
		public double MinimumResponseTime
		{
			get
			{
				return this._MinimumResponseTime;
			}
			set
			{
				if ((this._MinimumResponseTime != value))
				{
					this._MinimumResponseTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MaximumResponseTime", DbType="Float NOT NULL")]
		public double MaximumResponseTime
		{
			get
			{
				return this._MaximumResponseTime;
			}
			set
			{
				if ((this._MaximumResponseTime != value))
				{
					this._MaximumResponseTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Percentile90", DbType="Float")]
		public System.Nullable<double> Percentile90
		{
			get
			{
				return this._Percentile90;
			}
			set
			{
				if ((this._Percentile90 != value))
				{
					this._Percentile90 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Percentile95", DbType="Float")]
		public System.Nullable<double> Percentile95
		{
			get
			{
				return this._Percentile95;
			}
			set
			{
				if ((this._Percentile95 != value))
				{
					this._Percentile95 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Percentile99", DbType="Float")]
		public System.Nullable<double> Percentile99
		{
			get
			{
				return this._Percentile99;
			}
			set
			{
				if ((this._Percentile99 != value))
				{
					this._Percentile99 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Median", DbType="Float")]
		public System.Nullable<double> Median
		{
			get
			{
				return this._Median;
			}
			set
			{
				if ((this._Median != value))
				{
					this._Median = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StandardDeviation", DbType="Float")]
		public System.Nullable<double> StandardDeviation
		{
			get
			{
				return this._StandardDeviation;
			}
			set
			{
				if ((this._StandardDeviation != value))
				{
					this._StandardDeviation = value;
				}
			}
		}
	}
	
	public partial class Reporting_GetRequestsBySecResult
	{
		
		private string _TestCaseName;
		
		private string _ScenarioName;
		
		private string _RequestUri;
		
		private System.Nullable<int> _RequestsPerSec;
		
		private System.Nullable<System.DateTime> _Date1SecIncrement;
		
		public Reporting_GetRequestsBySecResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TestCaseName", DbType="NVarChar(64) NOT NULL", CanBeNull=false)]
		public string TestCaseName
		{
			get
			{
				return this._TestCaseName;
			}
			set
			{
				if ((this._TestCaseName != value))
				{
					this._TestCaseName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScenarioName", DbType="NVarChar(64) NOT NULL", CanBeNull=false)]
		public string ScenarioName
		{
			get
			{
				return this._ScenarioName;
			}
			set
			{
				if ((this._ScenarioName != value))
				{
					this._ScenarioName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RequestUri", DbType="NVarChar(2048) NOT NULL", CanBeNull=false)]
		public string RequestUri
		{
			get
			{
				return this._RequestUri;
			}
			set
			{
				if ((this._RequestUri != value))
				{
					this._RequestUri = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RequestsPerSec", DbType="Int")]
		public System.Nullable<int> RequestsPerSec
		{
			get
			{
				return this._RequestsPerSec;
			}
			set
			{
				if ((this._RequestsPerSec != value))
				{
					this._RequestsPerSec = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date1SecIncrement", DbType="DateTime")]
		public System.Nullable<System.DateTime> Date1SecIncrement
		{
			get
			{
				return this._Date1SecIncrement;
			}
			set
			{
				if ((this._Date1SecIncrement != value))
				{
					this._Date1SecIncrement = value;
				}
			}
		}
	}
}
#pragma warning restore 1591