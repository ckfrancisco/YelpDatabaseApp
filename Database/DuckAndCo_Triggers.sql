--Update business review count and rating after a review is inserted
CREATE OR REPLACE FUNCTION updateBusinessReview() RETURNS trigger AS '
BEGIN
        UPDATE  Businesses
        SET     reviewCount = subquery.reviewCount,
                reviewRating = subquery.reviewRating
        FROM
            (SELECT bid, 
                    COUNT(bid) AS reviewCount,
                    AVG(CAST(stars AS FLOAT)) AS reviewRating
                FROM Reviews
                GROUP BY bid) AS subquery
        WHERE Businesses.bid = subquery.bid;
    RETURN NEW;
END
' LANGUAGE plpgsql;

CREATE TRIGGER updateBusinessesReviewTrigger
AFTER INSERT OR UPDATE ON Reviews
FOR EACH STATEMENT
    EXECUTE PROCEDURE updateBusinessReview();

--Update business check ins after a check in is inserted
CREATE OR REPLACE FUNCTION updateBusinessCheckIn() RETURNS trigger AS '
BEGIN
        UPDATE  Businesses
        SET     numCheckIns = subquery.numCheckIns
        FROM
                (SELECT bid,
                        SUM(morning + afternoon + evening + night) AS numCheckIns
                FROM CheckIns
                GROUP BY bid) AS subquery
        WHERE Businesses.bid = subquery.bid;
    RETURN NEW;
END
' LANGUAGE plpgsql;

CREATE TRIGGER updateBusinessesCheckInTrigger
AFTER INSERT OR UPDATE ON CheckIns
FOR EACH STATEMENT
    EXECUTE PROCEDURE updateBusinessCheckIn();