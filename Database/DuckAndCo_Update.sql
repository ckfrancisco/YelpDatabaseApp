--Update business review count and rating
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

--Update business check ins
UPDATE  Businesses
SET     numCheckIns = subquery.numCheckIns
FROM
        (SELECT bid,
                SUM(morning + afternoon + evening + night) AS numCheckIns
        FROM CheckIns
        GROUP BY bid) AS subquery
WHERE Businesses.bid = subquery.bid;