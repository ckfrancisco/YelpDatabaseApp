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
                            businessesLocationStateComboBox.Items.Add(reader.GetString(0));
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
            businessesBusinessesBusinessesDataGrid.Columns.Add(colBusinessName);

            DataGridTextColumn colState = new DataGridTextColumn();
            colState.Header = "State";
            colState.Binding = new Binding("state");
            colBusinessName.Width = 255;
            businessesBusinessesBusinessesDataGrid.Columns.Add(colState);

            DataGridTextColumn colCity = new DataGridTextColumn();
            colCity.Header = "City";
            colCity.Binding = new Binding("city");
            colCity.Width = 255;
            businessesBusinessesBusinessesDataGrid.Columns.Add(colCity);

            DataGridTextColumn colPostalCode = new DataGridTextColumn();
            colPostalCode.Header = "Postal Code";
            colPostalCode.Binding = new Binding("postalCode");
            colPostalCode.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            businessesBusinessesBusinessesDataGrid.Columns.Add(colPostalCode);
        }

        private void businessesLocationStateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            businessesLocationCityComboBox.Items.Clear();
            businessesLocationPostalCodeComboBox.Items.Clear();
            businessesCategoriesCategoriesListBox.ItemsSource = null;
            businessesBusinessesBusinessesDataGrid.Items.Clear();
            if (businessesLocationStateComboBox.SelectedItem == null)
                return;

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct city " +
                                      "FROM businesses " + 
                                      "WHERE state='" + businessesLocationStateComboBox.SelectedItem.ToString() + "' " +
                                      "ORDER BY city;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            businessesLocationCityComboBox.Items.Add(reader.GetString(0));
                        }
                    }
                }

                connection.Close();
            }
        }

        private void businessesLocationCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            businessesLocationPostalCodeComboBox.Items.Clear();
            businessesCategoriesCategoriesListBox.ItemsSource = null;
            businessesBusinessesBusinessesDataGrid.Items.Clear();
            if (businessesLocationCityComboBox.SelectedItem == null)
                return;

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct postalcode " +
                                      "FROM businesses " +
                                      "WHERE state='" + businessesLocationStateComboBox.SelectedItem.ToString() + "' AND " + 
                                            "city='" + businessesLocationCityComboBox.SelectedItem.ToString() + "';";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            businessesLocationPostalCodeComboBox.Items.Add(reader.GetString(0));
                        }
                    }
                }

                connection.Close();
            }
        }

        private void businessesLocationPostalCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            businessesCategoriesCategoriesListBox.ItemsSource = null;
            businessesBusinessesBusinessesDataGrid.Items.Clear();
            if (businessesLocationPostalCodeComboBox.SelectedItem == null)
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
                                            "WHERE b.postalcode = '" + businessesLocationPostalCodeComboBox.SelectedItem.ToString() + "' AND " + 
                                                  "b.city='" + businessesLocationCityComboBox.SelectedItem.ToString() + "') as bids, " + 
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
                                      "WHERE b.city='" + businessesLocationCityComboBox.SelectedItem.ToString() + "' AND " +
                                            "b.postalcode='" + businessesLocationPostalCodeComboBox.SelectedItem.ToString() + "'; ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            businessesBusinessesBusinessesDataGrid.Items.Add(new Business() { name = reader.GetString(0), state = reader.GetString(1), city = reader.GetString(2), postalCode = reader.GetString(3) });
                        }
                    }
                }

                connection.Close();
            }

            businessesCategoriesCategoriesListBox.ItemsSource = categories;
        }

        private void businessesCategoriesCategoriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            businessesBusinessesBusinessesDataGrid.Items.Clear();
            if (businessesLocationPostalCodeComboBox.SelectedItem == null)
                return;
            if (businessesCategoriesCategoriesListBox.SelectedItem == null)
                return;

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT b.name, b.state, b.city, b.postalcode " +
                                      "FROM businesses as b " +
                                      "WHERE b.city='" + businessesLocationCityComboBox.SelectedItem.ToString() + "' AND " +
                                            "b.postalcode='" + businessesLocationPostalCodeComboBox.SelectedItem.ToString() + "' AND " +
                                            "b.bid IN " +
                                                  "(SELECT bid " +
                                                  "FROM categories as c " +
                                                  "WHERE c.name='" + businessesCategoriesCategoriesListBox.SelectedItem.ToString() + "');";


                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            businessesBusinessesBusinessesDataGrid.Items.Add(new Business() { name = reader.GetString(0), state = reader.GetString(1), city = reader.GetString(2), postalCode = reader.GetString(3) });
                        }
                    }
                }

                connection.Close();
            }
        }
    }
}
