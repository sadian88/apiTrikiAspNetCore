-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 27-02-2022 a las 23:41:35
-- Versión del servidor: 10.4.22-MariaDB
-- Versión de PHP: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `trikidb`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipodocumento`
--

CREATE TABLE `tipodocumento` (
  `Id` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `tipodocumento`
--

INSERT INTO `tipodocumento` (`Id`, `Name`) VALUES
(1, 'CC'),
(2, 'TI'),
(3, 'CC'),
(4, 'TI'),
(5, 'RC'),
(6, 'PA'),
(7, 'CE');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `user`
--

CREATE TABLE `user` (
  `Id` int(11) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Password` varchar(300) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `CreationDate` date NOT NULL,
  `TipoIdenID` int(11) NOT NULL,
  `LastLogin` date NOT NULL,
  `IndentityNumber` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `user`
--

INSERT INTO `user` (`Id`, `Email`, `Password`, `LastName`, `Name`, `CreationDate`, `TipoIdenID`, `LastLogin`, `IndentityNumber`) VALUES
(2, 'antonio@gmail.com', 'bateriass159', 'Diaz', 'Antonio', '0000-00-00', 1, '0000-00-00', 333),
(4, 'maria@gmail.com', 'maria321', 'Perez', 'Mari', '0000-00-00', 3, '0000-00-00', 67755),
(5, 'juan@gmail.com', '12345678', 'Perez', 'Juan', '0000-00-00', 5, '0000-00-00', 33422),
(6, 'pepito@gmail.com', 'pepito123', 'Perez', 'Pepito', '0000-00-00', 3, '0000-00-00', 432232);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `tipodocumento`
--
ALTER TABLE `tipodocumento`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `tipodocumento`
--
ALTER TABLE `tipodocumento`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `user`
--
ALTER TABLE `user`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
