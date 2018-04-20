CREATE TABLE Businesses (
    bid                     VARCHAR(255),
    name                    VARCHAR(255)    NOT NULL,
    stars                   FLOAT           DEFAULT 0,
    reviewCount             INTEGER         DEFAULT 0,
    reviewRating            FLOAT           DEFAULT 0,
    numCheckIns             INTEGER         DEFAULT 0,
    isOpen                  BOOLEAN         DEFAULT FALSE,

    address                 VARCHAR(255)    NOT NULL,
    state                   CHAR(2)         NOT NULL,
    city                    VARCHAR(255)    NOT NULL,
    postalCode              CHAR(5)      	NOT NULL,
    latitude                FLOAT           NOT NULL,
    longitude               FLOAT           NOT NULL,

    PRIMARY KEY (bid)
);

CREATE TABLE Categories (
    bid     VARCHAR(255),
    name    VARCHAR(255),

    PRIMARY KEY (bid, name),
    FOREIGN KEY (bid) REFERENCES Businesses (bid)
);

CREATE TABLE Attributes (
    bid     VARCHAR(255),
    name    VARCHAR(255),
    value   VARCHAR(255),

    PRIMARY KEY (bid, name),
    FOREIGN KEY (bid) REFERENCES Businesses (bid)
);

CREATE TABLE Hours (
    bid     VARCHAR(255),
    day     VARCHAR(255),
    open    TIME            NOT NULL,
    close   TIME            NOT NULL,

    PRIMARY KEY (bid, day),
    FOREIGN KEY (bid) REFERENCES Businesses (bid)
);

CREATE TABLE CheckIns (
    bid         VARCHAR(255),
    day         VARCHAR(255),
    morning     INTEGER         DEFAULT 0,
    afternoon   INTEGER         DEFAULT 0,
    evening     INTEGER         DEFAULT 0,
    night       INTEGER         DEFAULT 0,

    PRIMARY KEY (bid, day),
    FOREIGN KEY (bid) REFERENCES Businesses (bid)
);

CREATE TABLE Users (
    uid             VARCHAR(255),
    name            VARCHAR(255),
    dateJoined      DATE,
    reviewCount     INTEGER         DEFAULT 0,
    fans            INTEGER         DEFAULT 0,       
    avgStars        FLOAT         DEFAULT 0,
    funny           INTEGER         DEFAULT 0,
    useful          INTEGER         DEFAULT 0,
    cool            INTEGER         DEFAULT 0,

    PRIMARY KEY (uid)
);
--Consider default value for dateJoin?

CREATE TABLE Friends (
    uid     VARCHAR(255),
    fid     VARCHAR(255),

    PRIMARY KEY (uid, fid),
    FOREIGN KEY (uid) REFERENCES Users (uid),
    FOREIGN KEY (fid) REFERENCES Users (uid)
);

CREATE TABLE Reviews (
    rid     VARCHAR(255),
    uid     VARCHAR(255),
    bid     VARCHAR(255),
    text    TEXT,
    date    DATE,
    stars   INTEGER         NOT NULL,
    funny   INTEGER         DEFAULT 0,
    useful  INTEGER         DEFAULT 0,
    cool    INTEGER         DEFAULT 0,

    PRIMARY KEY (rid),
    FOREIGN KEY (uid) REFERENCES Users (uid),
    FOREIGN KEY (bid) REFERENCES Businesses (bid)
);
--Consider default value for date and reconsider others?