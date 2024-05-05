CREATE TABLE SeatReservations (
    ReservationId INT PRIMARY KEY IDENTITY,
    TrainId INT,
    TrainName NVARCHAR(100),
    DepartureLocation NVARCHAR(100),
    Destination NVARCHAR(100),
    Date DATE,
    Time TIME,
    NumberOfSeats INT,
    CONSTRAINT FK_Train FOREIGN KEY (TrainId) REFERENCES Trains (trainId)
);
