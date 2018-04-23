using System;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess;

namespace Dashboard_CreateGauges {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private GaugeDashboardItem CreateGauges(DataSource dataSource) {

            // Creates a gauge dashboard item and specifies its data source.
            GaugeDashboardItem gauges = new GaugeDashboardItem();
            gauges.DataSource = dataSource;

            // Creates the Gauge object with measures that provide data for calculating actual and target
            // values, and then adds this object to the Gauges collection of the gauge dashboard item.
            Gauge gauge = new Gauge();
            gauge.ActualValue = new Measure("Extended Price", SummaryType.Average);
            gauge.TargetValue = new Measure("Extended Price", SummaryType.Max);
            gauges.Gauges.Add(gauge);

            // Specifies the dimension that provides data for a gauge dashboard item series.
            gauges.SeriesDimensions.Add(new Dimension("Sales Person"));

            return gauges;
        }
        private void Form1_Load(object sender, EventArgs e) {

            // Creates a dashboard and sets it as the currently opened dashboard in the dashboard viewer.
            dashboardViewer1.Dashboard = new Dashboard();

            // Creates a data source and adds it to the dashboard data source collection.
            DataSource dataSource = new DataSource("Sales Person");
            dashboardViewer1.Dashboard.DataSources.Add(dataSource);

            // Creates a gauge dashboard item with the specified data source 
            // and adds it to the Items collection to display within the dashboard.
            GaugeDashboardItem gauges = CreateGauges(dataSource);
            dashboardViewer1.Dashboard.Items.Add(gauges);

            // Reloads data in the data sources.
            dashboardViewer1.ReloadData();
        }
        private void dashboardViewer1_DataLoading(object sender, DataLoadingEventArgs e) {

            // Specifies data for the current data source.
            e.Data = (new nwindDataSetTableAdapters.SalesPersonTableAdapter()).GetData();
        }
    }
}
