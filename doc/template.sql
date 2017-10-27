CREATE TABLE `template` (
  `id` varchar(50) NOT NULL,
  `name` varchar(145) NOT NULL,
  `path` varchar(450) NOT NULL,
  `file` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
