CREATE TABLE `reports` (
  `name` varchar(300) NOT NULL,
  `comment` varchar(600) DEFAULT NULL,
  `type` varchar(600) NOT NULL,
  `units` varchar(600) NOT NULL,
  PRIMARY KEY (`name`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//报表种类存贮';
