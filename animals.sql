-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Gép: mysql:3306
-- Létrehozás ideje: 2026. Nov 23. 09:25
-- Kiszolgáló verziója: 26.7.0
-- PHP verzió: 8.3.35

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `animalshelter`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `animals`
--

CREATE TABLE `animals` (
  `Id` char(36) NOT NULL,
  `Name` varchar(40) NOT NULL,
  `Species` varchar(30) NOT NULL,
  `Age` int NOT NULL,
  `Weight` double NOT NULL,
  `RegTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `ShelterId` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `animals`
--

INSERT INTO `animals` (`Id`, `Name`, `Species`, `Age`, `Weight`, `RegTime`, `ShelterId`) VALUES
('0099d91c-7415-4e94-b38f-d47648c3e621', 'latyakosbrennmdon', 'ciccca', 130, 134.89, '2026-11-23 10:05:02', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('0f63f310-d7bc-4e0b-8601-d2fed67c863e', 'kevin', 'ciggany', 1, 1, '2026-11-23 10:17:33', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('51975c0a-ed3b-4165-854a-8f141dd61720', 'mate', 'szolga', 1, 1, '2026-11-23 10:17:44', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('94c69ab5-eb1f-4ca8-b1fa-b015aafcccb5', 'Mc Isti', 'hajlektalan', 1, 1, '2026-11-23 10:17:59', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('9ab85302-f221-4df7-bf46-fcc1f649e2b0', 'Postas Pet', 'magyar', 1, 1, '2026-11-23 10:19:44', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('b293b5b4-f6c7-4d02-a0a4-ca94e9f5f3cf', 'Lakatos Kevin Dzsasztin (Bieber)', 'csepp nevelt unokam', 1, 1, '2026-11-23 10:23:14', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('cdebf72d-0ad9-4ef0-9cab-1335bf455920', 'Lakatos Vinicius Junior', 'nem magyar', 1, 1, '2026-11-23 10:20:01', '8d32af8e-bca2-5907-997f-05ff3b325fe7');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `animals`
--
ALTER TABLE `animals`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `ShelterId` (`ShelterId`);

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `animals`
--
ALTER TABLE `animals`
  ADD CONSTRAINT `animals_ibfk_1` FOREIGN KEY (`ShelterId`) REFERENCES `shelters` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
