Imports System
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon

Namespace Dashboard_CreateGauges
    Partial Public Class Form1
        Inherits DevExpress.XtraEditors.XtraForm

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Function CreateGauges(ByVal dataSource As DashboardObjectDataSource) As GaugeDashboardItem

			Dim gauges As New GaugeDashboardItem()
			gauges.ViewType = GaugeViewType.CircularHalf
			gauges.DataSource = dataSource

			Dim gauge As New Gauge()
			gauge.ActualValue = New Measure("Extended Price", SummaryType.Sum)
			gauges.Gauges.Add(gauge)

			gauges.SeriesDimensions.Add(New Dimension("Sales Person"))

			Return gauges
		End Function
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

			dashboardViewer1.Dashboard = New Dashboard()

			Dim dataSource As New DashboardObjectDataSource()
			AddHandler dashboardViewer1.AsyncDataLoading, Sub(s,ev)
				ev.Data = (New nwindDataSetTableAdapters.SalesPersonTableAdapter()).GetData()
			End Sub
			dashboardViewer1.Dashboard.DataSources.Add(dataSource)

			Dim gauges As GaugeDashboardItem = CreateGauges(dataSource)
			dashboardViewer1.Dashboard.Items.Add(gauges)

			dashboardViewer1.ReloadData()
		End Sub
	End Class
End Namespace
