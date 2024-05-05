CREATE TABLE Trains (
    TrainId INT NOT NULL,
    TrainName VARCHAR(100) NOT NULL,
    StartLocation VARCHAR(100) NOT NULL,
    EndLocation VARCHAR(100) NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    Capacity INT NOT NULL,
    Distance DECIMAL(10,2) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (TrainId)
);