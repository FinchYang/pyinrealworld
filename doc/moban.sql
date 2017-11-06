CREATE TABLE `moban` (
  `name` varchar(150) NOT NULL,
  `comment` varchar(450) NOT NULL,
  `filename` varchar(450) NOT NULL,
  `tabletype` varchar(600) NOT NULL DEFAULT '0',
  `time` datetime NOT NULL,
  PRIMARY KEY (`name`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//word模板存贮';
