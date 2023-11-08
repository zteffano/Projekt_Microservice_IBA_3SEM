
# Todo list

"1. Opsætning af SQL Server i Docker",
"2. Opsætning af API i docker",
"3. Lav en Docker compose fil og lav opsætning, så de er på samme netværk",
"4. Find ud af hvilke paths der skal være. F.eks. /api/v1/trailers/ ..og hvilke GET, POST, PUT / DELETE der skal være",
"5. Prioritere via MoSCOW metoden",
"6. Lav en class model og find ud hvilke egenskaber vi skal have",
"7. Når vores modeller er på plads, kan vi bruge EF til at oprette Database og lave migrations",
"8. Lav en controller"

# Endpoints

- /trailers/ - GET - Hent alle trailers
- /trailer:{id} (+ {type} + {brand} + osv.. )- GET - Hent en trailer
- /bookings - GET - Hent alle bookings
- /booking:{id} - GET - Hent en booking baseret på id
- /createbooking - POST - opret en booking
- /availble:{id} - GET - se om en trailer er ledig, baseret på dens id
- /getprice:{id}{days} - GET - se prisen på en trailer baseret på dens id og antal dage
- /getbooked:{bookingId}
------- mulige
- / create - delete & change customers endpoints ( POST, DELTE & PUT)
- / create - delete & change trailers endpoints ( POST, DELTE & PUT)
