using DuckAndCo_YelpApp.DBEntities;
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
    /// Interaction logic for GridWindow.xaml
    /// </summary>
    public partial class GridWindow : Window
    {
        public GridWindow(List<Review> reviews)
        {
            InitializeComponent();
            populateDataGrid(reviews);
        }

        public void populateDataGrid(List<Review> reviews)
        {
            DataGridTextColumn colReviewDate = new DataGridTextColumn();
            colReviewDate.Header = "Date";
            colReviewDate.Binding = new Binding("date");
            colReviewDate.Width = 100;
            dataGrid.Columns.Add(colReviewDate);

            DataGridTextColumn colReviewUserName = new DataGridTextColumn();
            colReviewUserName.Header = "User Name";
            colReviewUserName.Binding = new Binding("userName");
            colReviewUserName.Width = 100;
            dataGrid.Columns.Add(colReviewUserName);

            DataGridTextColumn colReviewStars = new DataGridTextColumn();
            colReviewStars.Header = "Stars";
            colReviewStars.Binding = new Binding("stars");
            colReviewStars.Width = 50;
            dataGrid.Columns.Add(colReviewStars);

            DataGridTextColumn colReviewText = new DataGridTextColumn();
            colReviewText.Header = "Text";
            colReviewText.Binding = new Binding("text");
            colReviewText.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dataGrid.Columns.Add(colReviewText);

            DataGridTextColumn colReviewFunny = new DataGridTextColumn();
            colReviewFunny.Header = "Funny";
            colReviewFunny.Binding = new Binding("funny");
            colReviewFunny.Width = 50;
            dataGrid.Columns.Add(colReviewFunny);

            DataGridTextColumn colReviewUseful = new DataGridTextColumn();
            colReviewUseful.Header = "Useful";
            colReviewUseful.Binding = new Binding("useful");
            colReviewUseful.Width = 50;
            dataGrid.Columns.Add(colReviewUseful);

            DataGridTextColumn colReviewCool = new DataGridTextColumn();
            colReviewCool.Header = "Cool";
            colReviewCool.Binding = new Binding("cool");
            colReviewCool.Width = 50;
            dataGrid.Columns.Add(colReviewCool);

            foreach (var review in reviews)
                dataGrid.Items.Add(review);
        }
    }
}
