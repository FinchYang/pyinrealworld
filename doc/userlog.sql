CREATE TABLE `userlog` (
  `ordinal` int(11) NOT NULL AUTO_INCREMENT,
  `time` datetime NOT NULL,
  `userid` varchar(50) NOT NULL,
  `content` varchar(450) DEFAULT NULL,
  `ip` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ordinal`),
  UNIQUE KEY `ordinal_UNIQUE` (`ordinal`),
  KEY `userid_idx` (`userid`),
  CONSTRAINT `userid` FOREIGN KEY (`userid`) REFERENCES `user` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
