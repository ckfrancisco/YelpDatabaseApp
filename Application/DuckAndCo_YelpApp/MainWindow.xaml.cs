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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace DuckAndCo_YelpApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Business
        {
            public string name { get; set; }
            public string address { get; set; }
            public string state { get; set; }
            public string city { get; set; }
            public string postalCode { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            addStates();
            addColumnsToGrid();
        }

        private string buildConnectionString()
        {
            return "Server=localhost; Database=yelpdb; User ID=postgres; Password=password;";
        }

        public void addStates()
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct state FROM businesses ORDER BY state";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stateComboBox.Items.Add(reader.GetString(0));
                        }
                    }
                }

                connection.Close();
            }
        }



        public void addColumnsToGrid()
        {
            DataGridTextColumn colBusinessName = new DataGridTextColumn();
            colBusinessName.Header = "Business Name";
            colBusinessName.Binding = new Binding("name");
            colBusinessName.Width = 255;
            businessGrid.Columns.Add(colBusinessName);

            DataGridTextColumn colState = new DataGridTextColumn();
            colState.Header = "State";
            colState.Binding = new Binding("state");
            colBusinessName.Width = 255;
            businessGrid.Columns.Add(colState);

            DataGridTextColumn colCity = new DataGridTextColumn();
            colCity.Header = "City";
            colCity.Binding = new Binding("city");
            colCity.Width = 255;
            businessGrid.Columns.Add(colCity);

            DataGridTextColumn colPostalCode = new DataGridTextColumn();
            colPostalCode.Header = "Postal Code";
            colPostalCode.Binding = new Binding("postalCode");
            colPostalCode.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            businessGrid.Columns.Add(colPostalCode);
        }

        private void stateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityComboBox.Items.Clear();
            postalCodeComboBox.Items.Clear();
            categoriesListBox.ItemsSource = null;
            businessGrid.Items.Clear();
            if (stateComboBox.SelectedItem == null)
                return;

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct city " +
                                      "FROM businesses " + 
                                      "WHERE state='" + stateComboBox.SelectedItem.ToString() + "' " +
                                      "ORDER BY city;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cityComboBox.Items.Add(reader.GetString(0));
                        }
                    }
                }

                connection.Close();
            }
        }

        private void cityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            postalCodeComboBox.Items.Clear();
            categoriesListBox.ItemsSource = null;
            businessGrid.Items.Clear();
            if (cityComboBox.SelectedItem == null)
                return;

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct postalcode " +
                                      "FROM businesses " +
                                      "WHERE state='" + stateComboBox.SelectedItem.ToString() + "' AND " + 
                                            "city='" + cityComboBox.SelectedItem.ToString() + "';";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            postalCodeComboBox.Items.Add(reader.GetString(0));
                        }
                    }
                }

                connection.Close();
            }
        }

        private void postalCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoriesListBox.ItemsSource = null;
            businessGrid.Items.Clear();
            if (postalCodeComboBox.SelectedItem == null)
                return;

            List<string> categories = new List<string>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct(c.name) " +
                                      "FROM " +
                                            "(SELECT distinct(b.bid) " +
                                            "FROM businesses as b, " +
                                            "categories as c " +
                                            "WHERE b.postalcode = '" + postalCodeComboBox.SelectedItem.ToString() + "' AND " + 
                                                  "b.city='" + cityComboBox.SelectedItem.ToString() + "') as bids, " + 
                                            "categories as c " +
                                      "WHERE bids.bid = c.bid;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(reader.GetString(0));
                        }
                    }

                    cmd.CommandText = "SELECT b.name, b.state, b.city, b.postalcode " +
                                      "FROM businesses as b " +
                                      "WHERE b.city='" + cityComboBox.SelectedItem.ToString() + "' AND " +
                                            "b.postalcode='" + postalCodeComboBox.SelectedItem.ToString() + "'; ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            businessGrid.Items.Add(new Business() { name = reader.GetString(0), state = reader.GetString(1), city = reader.GetString(2), postalCode = reader.GetString(3) });
                        }
                    }
                }

                connection.Close();
            }

            categoriesListBox.ItemsSource = categories;
        }

        private void categoriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            businessGrid.Items.Clear();
            if (postalCodeComboBox.SelectedItem == null)
                return;
            if (categoriesListBox.SelectedItem == null)
                return;

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT b.name, b.state, b.city, b.postalcode " +
                                      "FROM businesses as b " +
                                      "WHERE b.city='" + cityComboBox.SelectedItem.ToString() + "' AND " +
                                            "b.postalcode='" + postalCodeComboBox.SelectedItem.ToString() + "' AND " +
                                            "b.bid IN " +
                                                  "(SELECT bid " +
                                                  "FROM categories as c " +
                                                  "WHERE c.name='" + categoriesListBox.SelectedItem.ToString() + "');";


                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            businessGrid.Items.Add(new Business() { name = reader.GetString(0), state = reader.GetString(1), city = reader.GetString(2), postalCode = reader.GetString(3) });
                        }
                    }
                }

                connection.Close();
            }
        }
    }
}
