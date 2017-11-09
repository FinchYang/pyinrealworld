use tp;CREATE TABLE `summarized` (
  `date` varchar(10) NOT NULL,
  `content` varchar(4500) NOT NULL,
  `draft` smallint(2) NOT NULL DEFAULT '1' COMMENT '1-草稿，0-提交，2-拒绝，3-同意',
  `time` datetime NOT NULL,
  `comment` varchar(450) DEFAULT NULL,
  `reportname` varchar(100) NOT NULL DEFAULT '-' COMMENT '报表种类',
  PRIMARY KEY (`date`,`reportname`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//日汇总数据';
