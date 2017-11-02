CREATE TABLE `reportsdata` (
  `date` varchar(10) NOT NULL,
  `unitid` varchar(50) NOT NULL,
  `content` varchar(4500) NOT NULL,
  `draft` smallint(2) NOT NULL DEFAULT '1' COMMENT '1-草稿，0-提交，2-拒绝，3-同意',
  `time` datetime NOT NULL,
  `comment` varchar(450) DEFAULT NULL,
  `signtype` smallint(2) NOT NULL DEFAULT '0',
  `declinereason` varchar(450) DEFAULT NULL,
  `submittime` datetime DEFAULT NULL,
  PRIMARY KEY (`date`,`unitid`),
  UNIQUE KEY `date_UNIQUE` (`date`),
  KEY `reportsdataunitid_idx` (`unitid`),
  CONSTRAINT `reportsdataunitid` FOREIGN KEY (`unitid`) REFERENCES `unit` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
