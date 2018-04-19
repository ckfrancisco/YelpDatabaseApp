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

        public class Friend
        {
            public string name { get; set; }
            public string stars { get; set; }
            public string date { get; set; }
        }

        public class Review
        {
            public string userName { get; set; }
            public string businessName { get; set; }
            public string businessCity { get; set; }
            public string text { get; set; }
            public string date { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            addStates();
            addColumnsToGrid();
        }

        private string buildConnectionString()
        {
            return "Server=localhost; Database=yelpdb; User ID=postgres; Password=123;";
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

            DataGridTextColumn colFriendsName = new DataGridTextColumn();
            colFriendsName.Header = "Name";
            colFriendsName.Binding = new Binding("name");
            colFriendsName.Width = 75;
            usersFriendsFriendsGrid.Columns.Add(colFriendsName);

            DataGridTextColumn colAvgStars = new DataGridTextColumn();
            colAvgStars.Header = "Avg Stars";
            colAvgStars.Binding = new Binding("stars");
            colAvgStars.Width = 65;
            usersFriendsFriendsGrid.Columns.Add(colAvgStars);

            DataGridTextColumn colSince = new DataGridTextColumn();
            colSince.Header = "Yelping Since";
            colSince.Binding = new Binding("date");
            colSince.Width = 200;
            usersFriendsFriendsGrid.Columns.Add(colSince);

            DataGridTextColumn colReviewUserName = new DataGridTextColumn();
            colReviewUserName.Header = "User Name";
            colReviewUserName.Binding = new Binding("userName");
            colReviewUserName.Width = 75;
            usersReviewsReviewsDataGrid.Columns.Add(colReviewUserName);

            DataGridTextColumn colReviewBusinessName = new DataGridTextColumn();
            colReviewBusinessName.Header = "Business";
            colReviewBusinessName.Binding = new Binding("businessName");
            colReviewBusinessName.Width = 200;
            usersReviewsReviewsDataGrid.Columns.Add(colReviewBusinessName);

            DataGridTextColumn colReviewBusinessCity = new DataGridTextColumn();
            colReviewBusinessCity.Header = "City";
            colReviewBusinessCity.Binding = new Binding("businessCity");
            colReviewBusinessCity.Width = 100;
            usersReviewsReviewsDataGrid.Columns.Add(colReviewBusinessCity);

            DataGridTextColumn colReviewText = new DataGridTextColumn();
            colReviewText.Header = "Text";
            colReviewText.Binding = new Binding("text");
            colReviewText.Width = 200;
            usersReviewsReviewsDataGrid.Columns.Add(colReviewText);

            DataGridTextColumn colReviewDate = new DataGridTextColumn();
            colReviewDate.Header = "Date Posted";
            colReviewDate.Binding = new Binding("date");
            colReviewDate.Width = 200;
            usersReviewsReviewsDataGrid.Columns.Add(colReviewDate);
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

        private void usersUserNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            usersUserIDComboBox.Items.Clear();
            usersFriendsFriendsGrid.Items.Clear();
            usersReviewsReviewsDataGrid.Items.Clear();
            usersInfoNameTextBox.Text = "";
            usersInfoUsefulTextBox.Text = "";
            usersInfoCoolTextBox.Text = "";
            usersInfoFunnyTextBox.Text = "";
            usersInfoStarsTextBox.Text = "";
            usersInfoSinceTextBox.Text = "";
            usersInfoFansTextBox.Text = "";

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT uid " +
                                      "FROM users " +
                                      "WHERE name='" + usersUserNameTextBox.Text.ToString() + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usersUserIDComboBox.Items.Add(reader.GetString(0));
                        }
                    }
                }

                connection.Close();
            }

        }


        private void fillUserInfo()
        {
            usersFriendsFriendsGrid.Items.Clear();
            usersReviewsReviewsDataGrid.Items.Clear();
            usersInfoNameTextBox.Text = "";
            usersInfoUsefulTextBox.Text = "";
            usersInfoCoolTextBox.Text = "";
            usersInfoFunnyTextBox.Text = "";
            usersInfoStarsTextBox.Text = "";
            usersInfoSinceTextBox.Text = "";
            usersInfoFansTextBox.Text = "";

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    if (usersUserIDComboBox.SelectedItem != null)
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT * " +
                                          "FROM users " +
                                          "WHERE uid='" + usersUserIDComboBox.SelectedItem.ToString() + "';";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usersInfoNameTextBox.Text = (reader.GetString(1));
                                usersInfoStarsTextBox.Text = (reader.GetDouble(5).ToString());
                                usersInfoFansTextBox.Text = (reader.GetInt16(4).ToString());
                                usersInfoSinceTextBox.Text = (reader.GetDate(2).ToString());
                                usersInfoFunnyTextBox.Text = (reader.GetInt16(6).ToString());
                                usersInfoCoolTextBox.Text = (reader.GetInt16(8).ToString());
                                usersInfoUsefulTextBox.Text = (reader.GetInt16(7).ToString());
                            }
                        }
                    }
                }

                connection.Close();
            }
        }


        private void fillFriendsGrid()
        {
            usersReviewsReviewsDataGrid.Items.Clear();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                if (usersUserIDComboBox.SelectedItem != null)
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT * " +
                                          "FROM users,(SELECT fid FROM friends WHERE uid='" + usersUserIDComboBox.SelectedItem.ToString() + "') as userFriends " +
                                          "WHERE users.uid=userFriends.fid;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usersFriendsFriendsGrid.Items.Add(new Friend() { name = reader.GetString(1), stars = reader.GetDouble(5).ToString(), date = reader.GetDate(2).ToString() });
                            }
                        }
                    }
                }
                connection.Close();
            }
        }

        private void fillReviewsGrid()
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    if (usersUserIDComboBox.SelectedItem != null)
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT users.name,businesses.name,businesses.city,reviews.text,reviews.date " +
                                          "FROM businesses,reviews,users,(SELECT fid FROM friends WHERE uid='" + usersUserIDComboBox.SelectedItem.ToString() + "') as userFriends " +
                                          "WHERE users.uid=userFriends.fid AND users.uid = reviews.uid AND businesses.bid = reviews.bid " +
                                          "ORDER BY reviews.date DESC;";

                        using (var reader = cmd.ExecuteReader())
                        {
                            int i = 0;
                            while (reader.Read() && i < 50) //gets all
                            {
                                usersReviewsReviewsDataGrid.Items.Add(new Review() { userName = reader.GetString(0), businessName = reader.GetString(1), businessCity = reader.GetString(2), text = reader.GetString(3), date = reader.GetDate(4).ToString() });
                                i++;
                            }
                        }
                    }
                }

                connection.Close();
            }
        }

        private void usersUserIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillUserInfo();
            fillFriendsGrid();
            fillReviewsGrid();
        }

        private void usersFriendsFriendsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
