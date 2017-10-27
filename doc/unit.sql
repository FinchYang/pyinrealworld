CREATE TABLE `unit` (
  `id` varchar(50) NOT NULL,
  `ip` varchar(45) NOT NULL,
  `name` varchar(145) NOT NULL,
  `level` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
