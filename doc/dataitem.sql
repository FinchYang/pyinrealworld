use tp;
CREATE TABLE `dataitem` (
  `id` varchar(50) NOT NULL,
  `mandated` smallint(2) NOT NULL DEFAULT '1',
  `comment` varchar(300) DEFAULT NULL,
  `units` varchar(300) NOT NULL,
  `seconditem` varchar(5000) DEFAULT NULL,
  `name` varchar(150) NOT NULL,
  `deleted` smallint(2) NOT NULL DEFAULT '0',
  `tabletype` varchar(300) NOT NULL COMMENT '对应 reports表name字段',
  `time` datetime NOT NULL,
  `inputtype` smallint(2) NOT NULL DEFAULT '2' COMMENT ' public enum secondItemType\n    {\n        unknown,\n        number,//\n        text,\n        date,\n        radio//单选框\n    }',
  `hassecond` smallint(2) NOT NULL DEFAULT '0',
  `statisticstype` smallint(2) NOT NULL DEFAULT '0' COMMENT '  public enum StatisticsType\n    {\n        unknown,//未知\n        sum,//求和\n        average,//平均\n        collect,//汇总\n        yearoveryear,//同比\n        linkrelative,//环比\n    }',
  `defaultvalue` varchar(450) DEFAULT NULL,
  `index` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//数据项存贮';
