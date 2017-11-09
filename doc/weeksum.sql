use tp;CREATE TABLE `weeksummarized` (
  `startdate` varchar(10) NOT NULL,
  `content` varchar(4500) NOT NULL,
  `draft` smallint(2) NOT NULL DEFAULT '1' COMMENT '1-草稿，0-提交，2-拒绝，3-同意',
  `time` datetime NOT NULL,
  `comment` varchar(450) DEFAULT NULL,
  `enddate` varchar(10) NOT NULL,
  `reportname` varchar(100) NOT NULL DEFAULT '-',
  PRIMARY KEY (`startdate`,`enddate`,`reportname`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='时间段汇总数据';
