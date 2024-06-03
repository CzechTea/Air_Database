SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;


CREATE TABLE `Airlines` (
  `airline_id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `country` varchar(255) NOT NULL,
  `primary_airport_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `Airplanes` (
  `airplane_id` int(11) NOT NULL,
  `model` varchar(255) NOT NULL,
  `manufacturer` varchar(255) NOT NULL,
  `capacity` int(11) NOT NULL,
  `airline_id` int(11) DEFAULT NULL,
  `registration_number` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `Airports` (
  `airport_id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `city` varchar(255) NOT NULL,
  `country` varchar(255) NOT NULL,
  `IATA_code` varchar(10) DEFAULT NULL,
  `ICAO_code` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `Flights` (
  `flight_id` int(11) NOT NULL,
  `flight_number` varchar(50) NOT NULL,
  `airline_id` int(11) DEFAULT NULL,
  `airplane_id` int(11) DEFAULT NULL,
  `departure_airport_id` int(11) DEFAULT NULL,
  `arrival_airport_id` int(11) DEFAULT NULL,
  `departure_time` datetime NOT NULL,
  `arrival_time` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


ALTER TABLE `Airlines`
  ADD PRIMARY KEY (`airline_id`),
  ADD KEY `primary_airport_id` (`primary_airport_id`);

ALTER TABLE `Airplanes`
  ADD PRIMARY KEY (`airplane_id`),
  ADD KEY `airline_id` (`airline_id`);

ALTER TABLE `Airports`
  ADD PRIMARY KEY (`airport_id`);

ALTER TABLE `Flights`
  ADD PRIMARY KEY (`flight_id`),
  ADD KEY `airline_id` (`airline_id`),
  ADD KEY `airplane_id` (`airplane_id`),
  ADD KEY `departure_airport_id` (`departure_airport_id`),
  ADD KEY `arrival_airport_id` (`arrival_airport_id`);


ALTER TABLE `Airlines`
  MODIFY `airline_id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `Airplanes`
  MODIFY `airplane_id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `Airports`
  MODIFY `airport_id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `Flights`
  MODIFY `flight_id` int(11) NOT NULL AUTO_INCREMENT;


ALTER TABLE `Airlines`
  ADD CONSTRAINT `airlines_ibfk_1` FOREIGN KEY (`primary_airport_id`) REFERENCES `Airports` (`airport_id`);

ALTER TABLE `Airplanes`
  ADD CONSTRAINT `airplanes_ibfk_1` FOREIGN KEY (`airline_id`) REFERENCES `Airlines` (`airline_id`);

ALTER TABLE `Flights`
  ADD CONSTRAINT `flights_ibfk_1` FOREIGN KEY (`airline_id`) REFERENCES `Airlines` (`airline_id`),
  ADD CONSTRAINT `flights_ibfk_2` FOREIGN KEY (`airplane_id`) REFERENCES `Airplanes` (`airplane_id`),
  ADD CONSTRAINT `flights_ibfk_3` FOREIGN KEY (`departure_airport_id`) REFERENCES `Airports` (`airport_id`),
  ADD CONSTRAINT `flights_ibfk_4` FOREIGN KEY (`arrival_airport_id`) REFERENCES `Airports` (`airport_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
