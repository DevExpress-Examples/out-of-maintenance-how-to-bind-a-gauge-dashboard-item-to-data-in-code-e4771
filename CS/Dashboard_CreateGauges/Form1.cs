using System;
using System.Windows.Forms;
using DevExpress.DashboardCommon;

namespace Dashboard_CreateGauges {
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1() {
            InitializeComponent();
        }
        private GaugeDashboardItem CreateGauges(DashboardObjectDataSource dataSource) {

            GaugeDashboardItem gauges = new GaugeDashboardItem();
            gauges.ViewType = GaugeViewType.CircularHalf;
            gauges.DataSource = dataSource;

            Gauge gauge = new Gauge();
            gauge.ActualValue = new Measure("Extended Price", SummaryType.Sum);
            gauges.Gauges.Add(gauge);

            gauges.SeriesDimensions.Add(new Dimension("Sales Person"));

            return gauges;
        }
        private void Form1_Load(object sender, EventArgs e) {

            dashboardViewer1.Dashboard = new Dashboard();

            DashboardObjectDataSource dataSource = new DashboardObjectDataSource();
            dashboardViewer1.AsyncDataLoading+=(s,ev) => {
                ev.Data = (new nwindDataSetTableAdapters.SalesPersonTableAdapter()).GetData();
            };
            dashboardViewer1.Dashboard.DataSources.Add(dataSource);

            GaugeDashboardItem gauges = CreateGauges(dataSource);
            dashboardViewer1.Dashboard.Items.Add(gauges);

            dashboardViewer1.ReloadData();
        }
    }
}
