using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DuckAndCo_YelpApp
{
    /// <summary>
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        public ChartWindow(string chartTitle, string chartSeriesTitle, Dictionary<string, int> dataContext)
        {
            InitializeComponent();
            chart.Title = chartTitle;
            chartSeries.Title = chartSeriesTitle;
            populateChart(dataContext);
        }

        public void populateChart(Dictionary<string, int> dataContext)
        {
            chart.Width = dataContext.Count * 100;
            chart.DataContext = dataContext;
        }
    }
}
