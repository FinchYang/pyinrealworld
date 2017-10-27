CREATE TABLE `dataitem` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `mandated` tinyint(1) NOT NULL DEFAULT '1',
  `comment` varchar(146) DEFAULT NULL,
  `unitdisplay` tinyint(1) NOT NULL DEFAULT '1',
  `centerdisplay` tinyint(1) NOT NULL DEFAULT '1',
  `seconditem` varchar(500) DEFAULT NULL,
  `name` varchar(45) NOT NULL,
  `deleted` tinyint(1) NOT NULL DEFAULT '0',
  `datatype` smallint(2) NOT NULL DEFAULT '0' COMMENT '//\n  public enum dataItemType\n    {\n        unknown,\n        all=1,//所有表格适用\n        nine=9,//9点点名\n        four=4,//4点汇报\n    }',
  `time` datetime NOT NULL,
  `inputtype` smallint(2) NOT NULL DEFAULT '2' COMMENT ' public enum secondItemType\n    {\n        unknown,\n        number,//\n        text,\n        date,\n        radio//单选框\n    }',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
