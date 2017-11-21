-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 192.168.8.240    Database: tp
-- ------------------------------------------------------
-- Server version	5.7.20

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `dataitem`
--

DROP TABLE IF EXISTS `dataitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `dataitem` (
  `id` varchar(50) NOT NULL,
  `mandated` smallint(2) NOT NULL,
  `comment` varchar(450) DEFAULT NULL,
  `units` varchar(300) NOT NULL,
  `seconditem` varchar(5000) DEFAULT NULL,
  `name` varchar(150) NOT NULL,
  `deleted` smallint(2) NOT NULL DEFAULT '0',
  `tabletype` varchar(100) NOT NULL COMMENT '对应 reports表name字段',
  `time` datetime NOT NULL,
  `inputtype` smallint(2) NOT NULL COMMENT ' public enum secondItemType\n    {\n        unknown,\n        number,//\n        text,\n        date,\n        radio//单选框\n    }',
  `hassecond` smallint(2) NOT NULL,
  `statisticstype` varchar(600) NOT NULL COMMENT '  public enum StatisticsType\n    {\n        unknown,//未知\n        sum,//求和\n        average,//平均\n        collect,//汇总\n        yearoveryear,//同比\n        linkrelative,//环比\n    }',
  `defaultvalue` varchar(450) DEFAULT NULL,
  `index` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//数据项存贮';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dataitem`
--

LOCK TABLES `dataitem` WRITE;
/*!40000 ALTER TABLE `dataitem` DISABLE KEYS */;
INSERT INTO `dataitem` VALUES ('11ce75e1ffd84aa3b33e4ccf7b1f1558',0,'','[1]','[{\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0}]','交通事件统计',0,'每日点名统计汇报','2017-11-20 09:05:21',1,1,'[]','',94),('142073c5b954407ba9c2bb9956542646',0,'','[1]','','指挥调度概况',0,'每日点名统计汇报','2017-11-21 09:21:05',3,0,'[4]','无',96),('1be2b30f135c437380170dcaed4b9ca4',0,'','[1]','','汇报日期',0,'每日点名统计汇报','2017-11-20 09:07:26',4,0,'[4]','',90),('271d7581436d475e8b5d503fb06ae0e2',1,'','[2,3,4,5]','','道路交通设施概况',0,'每日点名统计汇报','2017-11-21 09:20:58',3,0,'[4]','无',97),('3203b543a4a643e2a026b01c63237bda',0,'','[1]','','汇报日期',0,'每日交管动态汇报','2017-11-21 09:19:54',4,0,'[4]','',90),('3810df98d4734420bcad72ac406ff125',0,'','[1]','[{\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0}]','交通事件统计',0,'每日交管动态汇报','2017-11-21 09:18:46',1,1,'[]','',94),('46b3cd6724f14d749f0fb32ad658db03',1,'','[1]','','撰写人',0,'每日交管动态汇报','2017-11-21 09:31:15',2,0,'[]','',92),('74d54782ad50418ab79306a2b35c8a65',1,'','[1]','','撰写人',0,'每日点名统计汇报','2017-11-21 09:21:59',2,0,'[]','',92),('7763f9f625f34c6c8d3e30f9027ee57e',1,'','[1]','','审核人',0,'每日点名统计汇报','2017-11-21 09:22:09',2,0,'[]','',91),('7bbaf2c9bc72476db09f925503292a73',0,'','[1]','','指挥调度概况',0,'每日交管动态汇报','2017-11-20 08:42:59',3,0,'[4]','无',96),('7d190d17c6dd4e3989a7a849a1c23826',1,'','[1]','','工作动态',0,'每日点名统计汇报','2017-11-20 09:06:17',3,0,'[4]','',93),('88ae0eb8329242c09d42c7b2da0f8df3',1,'','[2,3,4,5]','','道路交通设施概况',0,'每日交管动态汇报','2017-11-21 09:18:23',3,0,'[4]','无',97),('9a29801655244071822a715d3dc92790',0,'此处填写警务动态','[1]','[{\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":true,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":true,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":true,\"defaultValue\":\"0\",\"index\":0}]','勤务动态',0,'每日点名统计汇报','2017-11-21 09:20:24',1,1,'[]','',99),('9eb8073ffcaa4eb1ab74ad5f15d7e9b7',0,'','[1]','','市区路网交通运行评价',0,'每日点名统计汇报','2017-11-20 09:00:45',3,0,'[4]','',95),('9f30633ef1ba44fc9c8f8f8d839ceee4',1,'','[1]','','审核人',0,'每日交管动态汇报','2017-11-21 09:31:20',2,0,'[]','',91),('a6dba97a252a486fb22a33075ded99e9',0,'','[1]','[{\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"受伤人数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"死亡事故数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"死亡人数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0}]','交通事故概况',0,'每日交管动态汇报','2017-11-21 09:17:39',1,1,'[]','',98),('c3f3f2f43a3c491989fd99fe4830eeca',0,'','[1]','','市区路网交通运行评价',0,'每日交管动态汇报','2017-11-21 09:18:32',3,0,'[4]','',95),('c994bab56ca94413b8f8c09a99549a02',0,'此处填写警务动态','[1]','[{\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":true,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":true,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":true,\"defaultValue\":\"0\",\"index\":0}]','勤务动态',0,'每日交管动态汇报','2017-11-21 09:17:12',1,1,'[]','',99),('cd7c4a29b69d4856819129f58419c4d3',1,'','[1]','','工作动态',0,'每日交管动态汇报','2017-11-21 09:18:55',3,0,'[4]','',93),('ec3f3576a6d7404a80ffb650a04b3dc5',0,'','[1]','[{\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"受伤人数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"死亡事故数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"死亡人数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0}]','交通事故概况',0,'每日点名统计汇报','2017-11-21 09:20:47',1,1,'[]','',98);
/*!40000 ALTER TABLE `dataitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `moban`
--

DROP TABLE IF EXISTS `moban`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `moban` (
  `name` varchar(150) NOT NULL,
  `comment` varchar(450) NOT NULL,
  `filename` varchar(450) NOT NULL,
  `tabletype` varchar(100) NOT NULL,
  `time` datetime NOT NULL,
  `deleted` smallint(2) NOT NULL DEFAULT '0' COMMENT '1-删除',
  PRIMARY KEY (`name`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//word模板存贮';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `moban`
--

LOCK TABLES `moban` WRITE;
/*!40000 ALTER TABLE `moban` DISABLE KEYS */;
INSERT INTO `moban` VALUES ('每日交管动态汇报','','每日交管动态汇报20171120102657.doc','每日交管动态汇报','2017-11-20 10:26:58',0),('每日点名统计汇报','','每日点名统计汇报20171120090751.doc','每日点名统计汇报','2017-11-20 09:07:52',0);
/*!40000 ALTER TABLE `moban` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reportlog`
--

DROP TABLE IF EXISTS `reportlog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reportlog`
--

LOCK TABLES `reportlog` WRITE;
/*!40000 ALTER TABLE `reportlog` DISABLE KEYS */;
/*!40000 ALTER TABLE `reportlog` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reports`
--

DROP TABLE IF EXISTS `reports`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `reports` (
  `name` varchar(100) NOT NULL,
  `comment` varchar(600) DEFAULT NULL,
  `type` varchar(100) NOT NULL,
  `units` varchar(600) NOT NULL,
  PRIMARY KEY (`name`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//报表种类存贮';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reports`
--

LOCK TABLES `reports` WRITE;
/*!40000 ALTER TABLE `reports` DISABLE KEYS */;
INSERT INTO `reports` VALUES ('每日交管动态汇报','','four','[1]'),('每日点名统计汇报','','nine','[1]');
/*!40000 ALTER TABLE `reports` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reportsdata`
--

DROP TABLE IF EXISTS `reportsdata`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `reportsdata` (
  `date` varchar(10) NOT NULL,
  `unitid` varchar(50) NOT NULL,
  `content` varchar(20000) NOT NULL,
  `draft` smallint(2) NOT NULL COMMENT '1-草稿，0-提交，2-拒绝，3-同意,4;//签到\n//1--草稿，0-正式提交,2-退回\n-----1-草稿，0-提交到网站，2-保存word文档到本地，3-待定',
  `time` datetime NOT NULL,
  `comment` varchar(450) DEFAULT NULL,
  `signtype` smallint(2) NOT NULL DEFAULT '0',
  `declinereason` varchar(450) DEFAULT NULL,
  `submittime` datetime NOT NULL,
  `rname` varchar(100) NOT NULL,
  PRIMARY KEY (`date`,`unitid`,`rname`),
  KEY `reportsdataunitid_idx` (`unitid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//每日上报数据，包括草稿和提交，';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reportsdata`
--

LOCK TABLES `reportsdata` WRITE;
/*!40000 ALTER TABLE `reportsdata` DISABLE KEYS */;
/*!40000 ALTER TABLE `reportsdata` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `summarized`
--

DROP TABLE IF EXISTS `summarized`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `summarized` (
  `date` varchar(10) NOT NULL,
  `content` varchar(21000) NOT NULL,
  `draft` smallint(2) NOT NULL COMMENT '  public enum datastatus\n    {\n        submit,//0-提交到网站\n        draft,//1-草稿，\n        localword,//，2-保存word文档到本地，\n        undeterminied//3-待定\n    }',
  `time` datetime NOT NULL,
  `comment` varchar(450) DEFAULT NULL,
  `reportname` varchar(100) NOT NULL COMMENT '报表种类',
  PRIMARY KEY (`date`,`reportname`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//日汇总数据';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `summarized`
--

LOCK TABLES `summarized` WRITE;
/*!40000 ALTER TABLE `summarized` DISABLE KEYS */;
/*!40000 ALTER TABLE `summarized` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `unit`
--

DROP TABLE IF EXISTS `unit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `unit` (
  `id` varchar(50) NOT NULL COMMENT '    public enum unittype\n    {\n        unknown,//未知\n all,//所有\n        one,//一大队\n        two,//二大队\n        three,//三大队\n        four,//四大队\n        fushan,//福山大队\n        muping,//牟平大队	10.231.53.176\n        haiyang,//海阳大队	10.50.191.8\nlaiyang,//莱阳大队	10.231.52.211\nqixia,//栖霞大队	10.231.52.99\npenglai,//蓬莱大队	10.231.61.70\nchangdao,//长岛大队	10.231.53.209\nlongkou,//龙口大队	10.231.50.222\nzhaoyuan,//招远大队	10.231.200.87\nlaizhou,//莱州大队	10.231.59.103\nkaifaqu,//开发区大队	10.231.54.14\nyantaigang,//烟台港大队	10.231.55.189\njichang,//机场大队	10.50.219.241\n    }',
  `ip` varchar(45) NOT NULL,
  `name` varchar(145) NOT NULL,
  `level` smallint(1) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='大队设置';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `unit`
--

LOCK TABLES `unit` WRITE;
/*!40000 ALTER TABLE `unit` DISABLE KEYS */;
INSERT INTO `unit` VALUES ('center','10.50.230.253','中心',0),('changdao','10.231.53.209','长岛大队',1),('four','10.50.233.77','四大队',1),('fushan','10.231.52.53','福山大队',1),('haiyang','10.50.191.8','海阳',1),('jichang','10.50.219.241','机场大队',1),('kaifaqu','10.231.54.14','开发区大队',1),('laiyang','10.231.52.211','莱阳大队',1),('laizhou','10.231.59.103','莱州大队',1),('longkou','10.231.50.222','龙口大队',1),('muping','10.231.53.176','牟平',1),('one','10.50.230.253','一大队',1),('penglai','10.231.61.70','蓬莱大队',1),('qixia','10.231.52.99','栖霞大队',1),('three','10.50.231.183','三大队',1),('two','10.50.231.67','二大队',1),('yantaigang','10.231.55.189','港务大队',1),('zhaoyuan','10.231.200.87','招远大队',1);
/*!40000 ALTER TABLE `unit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `id` varchar(50) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `pass` varchar(45) NOT NULL,
  `unitid` varchar(50) NOT NULL,
  `token` varchar(45) DEFAULT NULL,
  `disabled` smallint(2) NOT NULL DEFAULT '0' COMMENT '0-enabled 1-disabled',
  `level` smallint(2) NOT NULL DEFAULT '1' COMMENT '1-one level 2-two level',
  `unitclass` smallint(2) NOT NULL COMMENT '0-直属大队，1-县市区大队',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `unitid_idx` (`unitid`),
  CONSTRAINT `unitid` FOREIGN KEY (`unitid`) REFERENCES `unit` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES ('admin','','admin','center',NULL,0,1,0),('cddd','','123456','changdao','28d918c6307a4c65a72c900beee4a201',0,2,1),('fsdd','','123456','fushan','e893cc6e789643188dfdec3a5c6e835a',0,2,1),('gwdd','','123456','yantaigang','96b41ebf88284702a4272395e6751f0c',0,2,1),('hydd','','123456','haiyang','a778972208d745f78029d8112f2f58eb',0,2,1),('jcdd','','123456','jichang','5ea04d340d3f408eb0af24bf17f72ac8',0,2,1),('jj1dd','','123456','one','df0c00d077fe4e1b8575afa6706855c3',0,2,0),('jj2dd','','123456','two','866d3b5c97264ae887c17daa153789ab',0,2,0),('jj3dd','','123456','three','2b0b4df1f304447a937460b0b267d769',0,2,0),('jj4dd','','123456','four','6baedff5f3b84988a3208715f4f97ab7',0,2,0),('kfqdd','','123456','kaifaqu','568d4d79ea3b43529cbacf0262926c80',0,2,1),('lkdd','','123456','longkou','23b0c9fba32f4cac8c480ee4aefe8463',0,2,1),('lydd','','123456','laiyang','7c74277d7d9a4a29af79c93ff47efe41',0,2,1),('lzdd','','123456','laizhou',NULL,0,2,1),('mpdd','','123456','muping','2bd0366f49504cfb9c677624c85a3adc',0,2,1),('pldd','','123456','penglai','9a1655468d36428caec61385173e57f9',0,2,1),('qxdd','','123456','qixia','7ed15bea4bb14f17ae74ca9955853f1e',0,2,1),('zydd','','123456','zhaoyuan','efe2cc27e8ba401eb4bd804ec5627a22',0,2,1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userlog`
--

DROP TABLE IF EXISTS `userlog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=543 DEFAULT CHARSET=utf8 COMMENT='使用日志';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userlog`
--

LOCK TABLES `userlog` WRITE;
/*!40000 ALTER TABLE `userlog` DISABLE KEYS */;
/*!40000 ALTER TABLE `userlog` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `weeksummarized`
--

DROP TABLE IF EXISTS `weeksummarized`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `weeksummarized` (
  `startdate` varchar(10) NOT NULL,
  `content` varchar(21000) NOT NULL,
  `draft` smallint(2) NOT NULL COMMENT '1-草稿，0-提交，2-拒绝，3-同意',
  `time` datetime NOT NULL,
  `comment` varchar(450) DEFAULT NULL,
  `enddate` varchar(10) NOT NULL,
  `reportname` varchar(100) NOT NULL,
  PRIMARY KEY (`startdate`,`enddate`,`reportname`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='时间段汇总数据';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `weeksummarized`
--

LOCK TABLES `weeksummarized` WRITE;
/*!40000 ALTER TABLE `weeksummarized` DISABLE KEYS */;
/*!40000 ALTER TABLE `weeksummarized` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'tp'
--

--
-- Dumping routines for database 'tp'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-11-21 15:20:29
