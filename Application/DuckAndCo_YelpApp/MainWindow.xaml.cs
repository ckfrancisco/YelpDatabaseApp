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

using DuckAndCo_YelpApp.DBEntities;
using System.Windows.Controls.Primitives;

namespace DuckAndCo_YelpApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            populateComboBoxes();
            populateDataGrids();

        }

        private string buildConnectionString()
        {
            return "Server=localhost; Database=yelpdb; User ID=postgres; Password=password;";
        }

        public void populateComboBoxes()
        {
            populateBusinessesLocationStateComboBox();
            populateBusinessesOpenDayComboBox();
            populateBusinessesOpenFromComboBox();
            populateBusinessesOpenToComboBox();
            populateBusinessesSortComboBox();
        }

        public void populateBusinessesLocationStateComboBox()
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct state " +
                                      "FROM businesses " + 
                                      "ORDER BY state";

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

        public void populateBusinessesOpenDayComboBox()
        {
            businessesOpenDayComboBox.Items.Add("");

            businessesOpenDayComboBox.Items.Add("Sunday");
            businessesOpenDayComboBox.Items.Add("Monday");
            businessesOpenDayComboBox.Items.Add("Tuesday");
            businessesOpenDayComboBox.Items.Add("Wednesday");
            businessesOpenDayComboBox.Items.Add("Thursday");
            businessesOpenDayComboBox.Items.Add("Friday");
            businessesOpenDayComboBox.Items.Add("Saturday");

            businessesOpenDayComboBox.SelectedIndex = 0;
        }

        public void populateBusinessesOpenFromComboBox()
        {
            businessesOpenFromComboBox.Items.Add("");

            for (int i = 0; i <= 23; i++)
                businessesOpenFromComboBox.Items.Add(i.ToString().PadLeft(2, '0') + ":00");
        }

        public void populateBusinessesOpenToComboBox()
        {
            businessesOpenToComboBox.Items.Add("");

            for (int i = 0; i <= 23; i++)
                businessesOpenToComboBox.Items.Add(i.ToString().PadLeft(2, '0') + ":00");

            businessesOpenToComboBox.SelectedIndex = 0;
        }

        public void populateBusinessesSortComboBox()
        {
            businessesSortComboBox.Items.Add(new ComboBoxItem() { Content = "Business Name", Tag = "ORDER BY name" });
            businessesSortComboBox.Items.Add(new ComboBoxItem() { Content = "Highest Rating", Tag = "ORDER BY stars DESC" });
            businessesSortComboBox.Items.Add(new ComboBoxItem() { Content = "Most Reviews", Tag = "ORDER BY reviewCount DESC" });
            businessesSortComboBox.Items.Add(new ComboBoxItem() { Content = "Best Review Rating", Tag = "ORDER BY reviewRating DESC" });
            businessesSortComboBox.Items.Add(new ComboBoxItem() { Content = "Most Checkins", Tag = "ORDER BY numCheckins DESC" });
            businessesSortComboBox.Items.Add(new ComboBoxItem() { Content = "Nearest", Tag = "ORDER BY distance DESC" });

            businessesSortComboBox.SelectedIndex = 0;
        }

        public void populateBusinessesReviewRatingComboBox()
        {
            for(int i = 1; i <= 5; i++)
                businessesReviewRatingComboBox.Items.Add(i.ToString());
        }

        public void populateDataGrids()
        {
            populateBusinessesBusinessesBusinessesDataGrid();
            populateUsersFriendsFriendsGrid();
            populateUsersReviewsReviewsDataGrid();
        }

        public void populateBusinessesBusinessesBusinessesDataGrid()
        {
            DataGridTextColumn colBusinessName = new DataGridTextColumn();
            colBusinessName.Header = "Business Name";
            colBusinessName.Binding = new Binding("name");
            colBusinessName.Width = 200;
            businessesBusinessesBusinessesDataGrid.Columns.Add(colBusinessName);

            DataGridTextColumn colBusinessAddress = new DataGridTextColumn();
            colBusinessAddress.Header = "Address";
            colBusinessAddress.Binding = new Binding("address");
            colBusinessAddress.Width = 200;
            businessesBusinessesBusinessesDataGrid.Columns.Add(colBusinessAddress);

            DataGridTextColumn colBusinessCity = new DataGridTextColumn();
            colBusinessCity.Header = "City";
            colBusinessCity.Binding = new Binding("city");
            colBusinessCity.Width = 100;
            businessesBusinessesBusinessesDataGrid.Columns.Add(colBusinessCity);

            DataGridTextColumn colBusinessState = new DataGridTextColumn();
            colBusinessState.Header = "State";
            colBusinessState.Binding = new Binding("state");
            colBusinessState.Width = 50;
            businessesBusinessesBusinessesDataGrid.Columns.Add(colBusinessState);

            DataGridTextColumn colBusinessDistance = new DataGridTextColumn();
            colBusinessDistance.Header = "Distance";
            colBusinessDistance.Binding = new Binding("distance");
            colBusinessDistance.Width = 75;
            businessesBusinessesBusinessesDataGrid.Columns.Add(colBusinessDistance);

            DataGridTextColumn colBusinessStars = new DataGridTextColumn();
            colBusinessStars.Header = "Stars";
            colBusinessStars.Binding = new Binding("stars");
            colBusinessStars.Width = 50;
            businessesBusinessesBusinessesDataGrid.Columns.Add(colBusinessStars);

            DataGridTextColumn colBusinessReviewCount = new DataGridTextColumn();
            colBusinessReviewCount.Header = "# of Reviews";
            colBusinessReviewCount.Binding = new Binding("reviewCount");
            colBusinessReviewCount.Width = 100;
            businessesBusinessesBusinessesDataGrid.Columns.Add(colBusinessReviewCount);

            DataGridTextColumn colBusinessReviewRating = new DataGridTextColumn();
            colBusinessReviewRating.Header = "Avg. Review Rating";
            colBusinessReviewRating.Binding = new Binding("reviewRating");
            colBusinessReviewRating.Width = 125;
            businessesBusinessesBusinessesDataGrid.Columns.Add(colBusinessReviewRating);

            DataGridTextColumn colBusinessNumCheckins = new DataGridTextColumn();
            colBusinessNumCheckins.Header = "# of Checkins";
            colBusinessNumCheckins.Binding = new Binding("numCheckins");
            colBusinessNumCheckins.Width = 100;
            businessesBusinessesBusinessesDataGrid.Columns.Add(colBusinessNumCheckins);
        }

        public void populateUsersFriendsFriendsGrid()
        {
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
            colSince.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            usersFriendsFriendsGrid.Columns.Add(colSince);
        }

        public void populateUsersReviewsReviewsDataGrid()
        {
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

            DataGridTextColumn colReviewDate = new DataGridTextColumn();
            colReviewDate.Header = "Date Posted";
            colReviewDate.Binding = new Binding("date");
            colReviewDate.Width = 100;
            usersReviewsReviewsDataGrid.Columns.Add(colReviewDate);

            DataGridTextColumn colReviewText = new DataGridTextColumn();
            colReviewText.Header = "Text";
            colReviewText.Binding = new Binding("text");
            colReviewText.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            usersReviewsReviewsDataGrid.Columns.Add(colReviewText);
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
                    cmd.CommandText = "SELECT " + 
                                          "distinct city " +
                                      "FROM " +
                                          "businesses " +
                                      "WHERE " +
                                          "state = '" + businessesLocationStateComboBox.SelectedItem.ToString() + "' " +
                                      "ORDER BY " +
                                          "city;";

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
                    cmd.CommandText = "SELECT " + 
                                          "distinct postalcode " +
                                      "FROM " +
                                          "businesses " +
                                      "WHERE " +
                                          "state='" + businessesLocationStateComboBox.SelectedItem.ToString() + "' AND " +
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
                    cmd.CommandText = "SELECT " +
                                          "distinct(categories.name) " +
                                      "FROM " +
                                          "(   SELECT distinct(businesses.bid) " +
                                              "FROM " + 
                                                  "businesses, " +
                                                  "categories " +
                                              "WHERE " +
                                                  "businesses.postalcode = '" + businessesLocationPostalCodeComboBox.SelectedItem.ToString() + "' AND " +
                                                  "businesses.city = '" + businessesLocationCityComboBox.SelectedItem.ToString() + "'" + 
                                          ") AS bids, " +
                                          "categories " +
                                      "WHERE " +
                                          "bids.bid = categories.bid;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(reader.GetString(0));
                        }
                    }
                }

                connection.Close();
            }

            businessesCategoriesCategoriesListBox.ItemsSource = categories;
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
                    cmd.CommandText = "SELECT " +
                                          "uid " +
                                      "FROM " +
                                          "users " +
                                      "WHERE " +
                                          "name = '" + usersUserNameTextBox.Text.ToString() + "' " +
                                      "ORDER BY " +
                                          "uid;";
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
                        cmd.CommandText = "SELECT " +
                                              "* " +
                                          "FROM " +
                                              "users " +
                                          "WHERE " +
                                              "uid = '" + usersUserIDComboBox.SelectedItem.ToString() + "';";
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
                        cmd.CommandText = "SELECT " +
                                              "* " +
                                          "FROM " +
                                               "users, " +
                                               "(   SELECT " +
                                                       "fid " +
                                                   "FROM " +
                                                       "friends " +
                                                   "WHERE " +
                                                       "uid = '" + usersUserIDComboBox.SelectedItem.ToString() + "'" + 
                                               ") AS userFriends " +
                                          "WHERE " +
                                               "users.uid = userFriends.fid " +
                                          "ORDER BY " +
                                               "uid;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usersFriendsFriendsGrid.Items.Add(new User() { name = reader.GetString(1), fid = reader.GetString(0), stars = reader.GetDouble(5).ToString(), date = reader.GetDate(2).ToString() });
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
                        cmd.CommandText = "SELECT " +
                                              "users.name, " +
                                              "businesses.name, " +
                                              "businesses.city, " +
                                              "reviews.text, " +
                                              "reviews.date " +
                                          "FROM " +
                                              "users, " +
                                              "(SELECT fid FROM friends WHERE uid = '" + usersUserIDComboBox.SelectedItem.ToString() + "') AS friends, " +
                                              "reviews, " +
                                              "(SELECT uid, MAX(date)AS date FROM reviews WHERE uid IN (SELECT fid from friends) GROUP BY uid) AS latestreviewperuser, " +
                                              "businesses " +
                                          "WHERE " +
                                              "users.uid = friends.fid AND " +
                                              "users.uid = reviews.uid AND " +
                                              "businesses.bid = reviews.bid AND " +
                                              "users.uid = latestreviewperuser.uid AND " +
                                              "reviews.date = latestreviewperuser.date " +
                                          "ORDER BY " +
                                              "users.uid;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usersReviewsReviewsDataGrid.Items.Add(new Review() { userName = reader.GetString(0), businessName = reader.GetString(1), businessCity = reader.GetString(2), text = reader.GetString(3), date = reader.GetDate(4).ToString() });
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

        private void businessesSearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (businessesLocationPostalCodeComboBox.SelectedItem == null)
                return;
            businessesBusinessesBusinessesDataGrid.Items.Clear();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    string inString = "";

                    if (businessesOpenDayComboBox.SelectedItem.ToString() != "" && businessesOpenFromComboBox.SelectedItem.ToString() != "" && businessesOpenToComboBox.SelectedItem.ToString() != "")
                    {
                        inString += "bid IN (SELECT " +
                                                "bid " +
                                            "FROM " +
                                                "hours " +
                                            "WHERE " +
                                                "hours.day = '" + businessesOpenDayComboBox.SelectedItem.ToString() + "' AND " +
                                                "hours.open <= '" + businessesOpenFromComboBox.SelectedItem.ToString() + "' AND " +
                                                "hours.close >= '" + businessesOpenToComboBox.SelectedItem.ToString() + "') AND ";
                    }

                    if (businessesCategoriesCategoriesListBox.SelectedItem != null)
                    {
                        foreach (var category in businessesCategoriesCategoriesListBox.SelectedItems)
                        {
                            inString += "bid IN (SELECT " +
                                                    "bid " +
                                                "FROM " +
                                                    "categories " +
                                                "WHERE " +
                                                    "categories.name = '" + category.ToString() + "'" +
                                               ") AND ";
                        }
                    }

                    Grid businessesPriceGrid = businessesPriceGroupBox.Content as Grid;
                    string priceString = "";
                    foreach (CheckBox price in businessesPriceGrid.Children)
                    {
                        if (price.IsChecked.Value)
                        {
                            if (priceString == "")
                            {
                                priceString += "bid IN (SELECT " +
                                                           "bid " +
                                                       "FROM " +
                                                           "attributes " +
                                                       "WHERE " +
                                                           "attributes.name = 'RestaurantsPriceRange2' AND (";
                            }

                            priceString += "attributes.value = '" + price.Tag.ToString() + "' OR ";
                        }
                    }

                    if (priceString != "")
                    {
                        priceString = priceString.Substring(0, priceString.Length - 4);
                        priceString += ")) AND ";

                        inString += priceString;
                    }

                    Grid businessesAttributesGrid = businessesAttributesGroupBox.Content as Grid;
                    foreach (CheckBox attribute in businessesAttributesGrid.Children)
                    {
                        if (attribute.IsChecked.Value)
                        {
                            inString += "bid IN (SELECT " +
                                                    "bid " +
                                                "FROM " +
                                                    "attributes " +
                                                "WHERE " +
                                                    "attributes.name = '" + attribute.Tag.ToString() + "' AND " +
                                                    "attributes.value = 'True') AND ";
                        }
                    }

                    Grid businessesMealGrid = businessesMealGroupBox.Content as Grid;
                    foreach (CheckBox meal in businessesMealGrid.Children)
                    {
                        if (meal.IsChecked.Value)
                        {
                            inString += "bid IN (SELECT " +
                                                    "bid " +
                                                "FROM " +
                                                    "attributes " +
                                                "WHERE " +
                                                    "attributes.name = '" + meal.Tag.ToString() + "' AND " +
                                                    "attributes.value = 'True') AND ";
                        }
                    }


                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT " +
                                          "bid, " +
                                          "name, " +
                                          "address, " +
                                          "city, " +
                                          "state, " +
                                          "stars, " +
                                          "reviewCount, " +
                                          "reviewRating, " +
                                          "numCheckIns " +
                                      "FROM " +
                                          "businesses " +
                                      "WHERE " +
                                          "city ='" + businessesLocationCityComboBox.SelectedItem.ToString() + "' AND " +
                                          "postalcode ='" + businessesLocationPostalCodeComboBox.SelectedItem.ToString() + "' AND " + inString;

                    cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 5);
                    cmd.CommandText += ((ComboBoxItem)businessesSortComboBox.SelectedItem).Tag.ToString() + ";";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            businessesBusinessesBusinessesDataGrid.Items.Add(new Business()
                            {
                                bid = reader.GetString(0),
                                name = reader.GetString(1),
                                address = reader.GetString(2),
                                city = reader.GetString(3),
                                state = reader.GetString(4),
                                stars = reader.GetDouble(5),
                                reviewCount = reader.GetInt32(6),
                                reviewRating = reader.GetDouble(7),
                                numCheckins = reader.GetInt32(8)
                            });
                        }
                    }

                    connection.Close();
                }
            }
        }

        private void usersFriendsRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (usersFriendsFriendsGrid.SelectedItem == null)
                return;
            User f = null;
            f = (User)usersFriendsFriendsGrid.SelectedItem;
            string fid = f.fid;

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "DELETE FROM friends WHERE fid='" + fid + "' AND uid='" + usersUserIDComboBox.SelectedItem.ToString() + "';";
                    using (var reader = cmd.ExecuteReader()) { }

                    //swapped the uid and fid
                    cmd.CommandText = "DELETE FROM friends WHERE fid='" + usersUserIDComboBox.SelectedItem.ToString() + "' AND uid='" + fid + "';";
                    using (var reader = cmd.ExecuteReader()) { }
                }
                connection.Close();
            }
            usersFriendsFriendsGrid.Items.Clear();
            fillFriendsGrid();
        }

        private void businessesReviewReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if(businessesReviewRatingComboBox.SelectedItem == null || !(string.IsNullOrEmpty(businessesReviewReviewTextBox.Text)) || !(string.IsNullOrEmpty(businessesReviewNameTextBox.Text)))
            {
                return;
            }
            else
            {
                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        //need to clean for sql 
                        string uid = usersUserNameTextBox.Text;
                        string bid = ((Business)businessesBusinessesBusinessesDataGrid.SelectedItem).bid;
                        string rid = "0";
                        DateTime datetime = DateTime.UtcNow.Date;
                        cmd.CommandText = "INSERT INTO " +
                                              "reviews (rid, " +
                                                       "uid, " +
                                                       "bid, " +
                                                       "text, " +
                                                       "date, " +
                                                       "stars, " +
                                                       "funny, " +
                                                       "useful, " +
                                                       "cool) " +
                                              "values ('" + 
                                                        rid + "','" + 
                                                        uid + "','" + 
                                                        bid + "','" + 
                                                        businessesReviewReviewTextBox.Text.ToString() + "','" + 
                                                        datetime.ToString("dd/mm/yyyy") + "','" + 
                                                        businessesReviewRatingComboBox.Text.ToString() + "','" + 
                                                        "0" + "','" + 
                                                        "0" + "','" + 
                                                        "0" + "');";

                        cmd.ExecuteReader();

                    }

                    connection.Close();
                }
            }
        }

        private void businessesBusinessesBusinessesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (businessesBusinessesBusinessesDataGrid.SelectedItem == null)
            {
                businessesReviewNameTextBox.Text = "";
            }

            else
            {
                string name = ((Business)businessesBusinessesBusinessesDataGrid.SelectedItem).name;
                businessesReviewNameTextBox.Text = name;
            }
        }

        private void businessesReviewCheckinButton_Click(object sender, RoutedEventArgs e)
        {
            if (businessesBusinessesBusinessesDataGrid.SelectedItem == null)
            {
                return;
            }

            else
            {
                using (var connection = new NpgsqlConnection(buildConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        string bid = ((Business)businessesBusinessesBusinessesDataGrid.SelectedItem).bid;
                        string checkinString = "";
                        string day = "Monday"; //DateTime.UtcNow.DayOfWeek.ToString();
                        TimeSpan time = new TimeSpan(0, 0, 0); //DateTime.UtcNow.ToLocalTime().TimeOfDay;

                        TimeSpan start = new TimeSpan(6, 0, 0);
                        TimeSpan end = new TimeSpan(11, 59, 59);
                        if (time >= start && time <= end)
                        {
                            checkinString = "SET morning = morning + 1 ";
                        }

                        start = new TimeSpan(12, 0, 0);
                        end = new TimeSpan(16, 59, 59);
                        if (time >= start && time <= end)
                        {
                            checkinString = "SET afternoon = afternoon + 1 ";
                        }

                        start = new TimeSpan(17, 0, 0);
                        end = new TimeSpan(22, 59, 59);
                        if (time >= start && time <= end)
                        {
                            checkinString = "SET evening = evening + 1 ";
                        }

                        else
                        {
                            checkinString = "SET night = night + 1 ";
                        }

                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE " +
                                              "checkins " +
                                           checkinString +
                                           "WHERE " +
                                               "bid = '" + bid + "' AND " +
                                               "day = '" + day + "';";
                        cmd.ExecuteReader();

                    }

                    connection.Close();
                }
            }
        }
    }
}
