Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess

Namespace Dashboard_CreateGauges
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Function CreateGauges(ByVal dataSource As DataSource) As GaugeDashboardItem

			' Creates a gauge dashboard item and specifies its data source.
			Dim gauges As New GaugeDashboardItem()
			gauges.DataSource = dataSource

			' Creates the Gauge object with measures that provide data for calculating actual and target
			' values, and then adds this object to the Gauges collection of the gauge dashboard item.
			Dim gauge As New Gauge()
			gauge.ActualValue = New Measure("Extended Price", SummaryType.Average)
			gauge.TargetValue = New Measure("Extended Price", SummaryType.Max)
			gauges.Gauges.Add(gauge)

			' Specifies the dimension that provides data for a gauge dashboard item series.
			gauges.SeriesDimensions.Add(New Dimension("Sales Person"))

			Return gauges
		End Function
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

			' Creates a dashboard and sets it as the currently opened dashboard in the dashboard viewer.
			dashboardViewer1.Dashboard = New Dashboard()

			' Creates a data source and adds it to the dashboard data source collection.
			Dim dataSource As New DataSource("Sales Person")
			dashboardViewer1.Dashboard.DataSources.Add(dataSource)

			' Creates a gauge dashboard item with the specified data source 
			' and adds it to the Items collection to display within the dashboard.
			Dim gauges As GaugeDashboardItem = CreateGauges(dataSource)
			dashboardViewer1.Dashboard.Items.Add(gauges)

			' Reloads data in the data sources.
			dashboardViewer1.ReloadData()
		End Sub
        Private Sub dashboardViewer1_DataLoading(ByVal sender As Object, _
                    ByVal e As DataLoadingEventArgs) Handles dashboardViewer1.DataLoading

            ' Specifies data for the current data source.
            e.Data = (New nwindDataSetTableAdapters.SalesPersonTableAdapter()).GetData()
        End Sub
	End Class
End Namespace
