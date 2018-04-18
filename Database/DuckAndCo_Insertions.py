import json
import psycopg2

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

def int2BoolStr (value):
    if value == 0:
        return 'False'
    else:
        return 'True'

def insert2BusinessTable():
    #reading the JSON file
    with open('.//yelp_dataset//yelp_business.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('.//yelp_dataset//yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:

            #conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='123'")
            #Roosevelt has a different password
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='password'")

        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            sql_str = "INSERT INTO businesses (bid, name, address,state,city,postalcode,latitude,longitude,stars,reviewcount,isopen) " \
                      "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + cleanStr4SQL(data["name"]) + "','" + cleanStr4SQL(data["address"]) + "','" + \
                      cleanStr4SQL(data["state"]) + "','" + cleanStr4SQL(data["city"]) + "','" + cleanStr4SQL(data["postal_code"]) + "','" + str(data["latitude"]) + "','" + \
                      str(data["longitude"]) + "','" + str(data["stars"]) + "','" + str(data["review_count"]) + "','" + int2BoolStr(data["is_open"]) + "');"
            try:
                cur.execute(sql_str)
            except:
                print(sql_str)
                print("Insert to businesses failed!")
            conn.commit()

            # outfile.write(sql_str)
            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2CategoriesTable():
    #reading the JSON file
    with open('.//yelp_dataset//yelp_business.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('.//yelp_dataset//yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            #conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='123'")
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='password'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            for item in data['categories']:
                sql_str = "INSERT INTO categories (bid, name) " \
                          "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + str(item).replace("'","\\") + "');"
                try:
                    cur.execute(sql_str)
                except:
                    print(sql_str)
                    print("Insert to categories failed!")
                conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2AttributesTable():
    #reading the JSON file
    with open('.//yelp_dataset//yelp_business.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('.//yelp_dataset//yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0
        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
                        #conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='123'")
                        #Roosevelt has a different password
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='password'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            for item in data['attributes']:
                sql_str = ""
                if isinstance(data['attributes'][item],dict):
                    for subItem in data['attributes'][item]:
                        #if(isinstance(data['attributes'][item][subItem],bool)) and data['attributes'][item][subItem] == False:
                            #sql_str = ""
                        #else:
                            sql_str = "INSERT INTO attributes (bid, name, value) " \
                                      "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + str(subItem).replace(
                                "'", "\\") + "','" + str(data['attributes'][item][subItem]) + "');"
                            if sql_str != "":
                                try:
                                    cur.execute(sql_str)
                                except:
                                    print("Insert to attributes failed!")
                                conn.commit()
                else:
                    #if (isinstance(data['attributes'][item], bool)) and data['attributes'][item] == False:
                        # = ""
                    #else:
                        sql_str = "INSERT INTO attributes (bid, name, value) " \
                                  "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + str(item).replace("'","\\") + "','" + str(data['attributes'][item]) + "');"
                        if sql_str != "":
                            try:
                                cur.execute(sql_str)
                            except:
                                print(sql_str)
                                print("Insert to attributes failed!")
                            conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()


def insert2HoursTable():
    #reading the JSON file
    with open('.//yelp_dataset//yelp_business.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('.//yelp_dataset//yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0
        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='password'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            sql_str = ""
            for item in data['hours']:
                sql_str = ""
                hoursList = str(data['hours'][item]).split("-")
                sql_str = "INSERT INTO hours (bid, day, open, close) " \
                          "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + str(item) + "','" + hoursList[0] + "','" + hoursList[1] + "');"
                if sql_str != "":
                    try:
                        cur.execute(sql_str)
                    except:
                        print(sql_str)
                        print("Insert to hours failed!")
                    conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2UsersTable():
    #reading the JSON file
    with open('.//yelp_dataset//yelp_user.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('.//yelp_dataset//yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0
        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='password'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)

            user_id = cleanStr4SQL(str(data['user_id']))
            name = cleanStr4SQL(str(data['name']))
            yelping_since = cleanStr4SQL(str(data['yelping_since']))
            review_count = cleanStr4SQL(str(data['review_count']))
            fans = cleanStr4SQL(str(data['fans']))
            average_stars = cleanStr4SQL(str(data['average_stars']))
            funny = cleanStr4SQL(str(data['funny']))
            useful = cleanStr4SQL(str(data['useful']))
            cool = cleanStr4SQL(str(data['cool']))


            sql_str = "INSERT INTO Users (uid, name, dateJoined, reviewCount, fans, avgStars, funny, useful, cool) " \
                          "VALUES ('" + user_id + "','" + name + "','" + yelping_since + "','" + review_count + "','" + fans + "','" + average_stars + "','" + funny + "','" + useful + "','" + cool + "');"

            if sql_str != "":
                try:
                    cur.execute(sql_str)
                except:
                    print(sql_str)
                    print("Insert to users failed!")
                conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2FriendsTable():
    #reading the JSON file
    with open('.//yelp_dataset//yelp_user.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('.//yelp_dataset//yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0
        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='password'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            user_id = cleanStr4SQL(str(data['user_id']))

            x = data['friends']
            for val in x:
                fid = cleanStr4SQL(str(val))
                sql_str = "INSERT INTO Friends (uid, fid) "  + "VALUES ('" + user_id + "','" + fid + "');"

                if sql_str != "":
                    try:
                        cur.execute(sql_str)
                    except:
                        print(sql_str)
                        print("Insert to Friends failed!")
                    conn.commit()
                count_line +=1
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)
            line = f.readline()

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()


def insert2CheckInsTable():
    #reading the JSON file
    with open('.//yelp_dataset//yelp_checkin.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('.//yelp_dataset//yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0
        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='password'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            bid = cleanStr4SQL(str(data['business_id']))
            #outfile.write(cleanStr4SQL(str(data['business_id'])))
            #outfile.write('\n')
            for dayOfWeek in data['time']:  #for every day of the week
                    temp = ""
                    morning = 0
                    afternoon = 0
                    evening = 0
                    night = 0
                    for timeOfDay in data['time'][dayOfWeek]:
                        if(timeOfDay == "6:00" or timeOfDay == "7:00" or timeOfDay == "8:00" or timeOfDay == "9:00" or timeOfDay == "10:00" or timeOfDay == "11:00"):
                            morning += data['time'][dayOfWeek][timeOfDay]
                        elif(timeOfDay == "12:00" or timeOfDay == "13:00" or timeOfDay == "14:00" or timeOfDay == "15:00" or timeOfDay == "16:00"):
                            afternoon += data['time'][dayOfWeek][timeOfDay]
                        elif(timeOfDay == "17:00" or timeOfDay == "18:00" or timeOfDay == "19:00" or timeOfDay == "20:00" or timeOfDay == "21:00" or timeOfDay == "22:00"):
                            evening += data['time'][dayOfWeek][timeOfDay]
                        elif(timeOfDay == "23:00" or timeOfDay == "0:00" or timeOfDay == "1:00" or timeOfDay == "2:00" or timeOfDay == "3:00" or timeOfDay == "4:00" or timeOfDay == "5:00"):
                            night += data['time'][dayOfWeek][timeOfDay]
                    #temp = (str)((dayOfWeek, morning, afternoon, evening, night))
                    sql_str = "INSERT INTO CheckIns (bid, day, morning, afternoon, evening, night) "  + "VALUES ('" + bid + "','" + cleanStr4SQL(str(dayOfWeek)) + "','" + str(morning) + "','" + str(afternoon) + "','" + str(evening) + "','" + str(night) + "');"
                    if sql_str != "":
                        try:
                            cur.execute(sql_str)
                        except:
                            print(sql_str)
                            print("Insert to CheckIns failed!")
                        conn.commit()
                    count_line +=1
                    #outfile.write('\t' +temp.replace("'","") + '\n')
            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2ReviewsTable():
    #reading the JSON file
    with open('.//yelp_dataset//yelp_review.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('.//yelp_dataset//yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0
        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='password'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)

            review_id = cleanStr4SQL(str(data['review_id']))
            user_id = cleanStr4SQL(str(data['user_id']))
            business_id = cleanStr4SQL(str(data['business_id']))
            text = cleanStr4SQL(str(data['text']))
            stars = cleanStr4SQL(str(data['stars']))
            date = cleanStr4SQL(str(data['date']))
            funny = cleanStr4SQL(str(data['funny']))
            useful = cleanStr4SQL(str(data['useful']))
            cool = cleanStr4SQL(str(data['cool']))


            sql_str = "INSERT INTO Reviews (rid, uid, bid, text, date, stars, funny, useful, cool) " \
                          "VALUES ('" + review_id + "','" + user_id + "','" + business_id + "','" + text + "','" + date + "','" + stars + "','" + funny + "','" + useful + "','" + cool + "');"

            if sql_str != "":
                try:
                    cur.execute(sql_str)
                except:
                    print(sql_str)
                    print("Insert to Reviews failed!")
                conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()


insert2BusinessTable()
insert2CategoriesTable()
insert2AttributesTable()
insert2HoursTable()

insert2UsersTable()
insert2FriendsTable()
insert2CheckInsTable()
insert2ReviewsTable()
