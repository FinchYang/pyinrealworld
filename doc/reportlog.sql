CREATE TABLE `reportlog` (
  `date` varchar(10) NOT NULL,
  `unitid` varchar(50) NOT NULL,
  `content` varchar(4500) NOT NULL,
  `draft` smallint(2) NOT NULL DEFAULT '1' COMMENT '1-草稿，0-提交，2-拒绝，3-同意',
  `time` datetime NOT NULL,
  `declinereason` varchar(450) DEFAULT NULL,
  PRIMARY KEY (`date`,`unitid`),
  UNIQUE KEY `date_UNIQUE` (`date`),
  KEY `reportlogunitid_idx` (`unitid`),
  CONSTRAINT `reportlogunitid` FOREIGN KEY (`unitid`) REFERENCES `unit` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
