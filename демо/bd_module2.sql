USE gostinka;

-- 1 Задача
SELECT 
    users.*,
    bookings.Amount + COALESCE(SUM(CASE
                WHEN
                    services.InHour = 0
                THEN
                    services.Price * DATEDIFF(bookings.DepartureDate,
                            bookings.ArrivalDate)
                ELSE services.Price
            END),
            0) AS FullPrice
FROM
    users
        JOIN
    bookings ON bookings.ClientId = users.IdUser
        LEFT JOIN
    bookings_services ON bookings_services.BookingId = bookings.IdBooking
        LEFT JOIN
    services ON bookings_services.ServiceId = services.IdService
GROUP BY bookings.IdBooking , users.IdUser;

-- 2 Задача
UPDATE rooms
        JOIN
    statuses ON rooms.StatusId = statuses.IdStatus 
SET 
    StatusId = (SELECT 
            IdStatus
        FROM
            statuses
        WHERE
            StatusName = 'Чистый')
WHERE
    StatusName = 'Назначен к уборке';