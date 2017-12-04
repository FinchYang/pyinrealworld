-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 47.93.226.74    Database: tp
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
  `sumunits` varchar(300) DEFAULT NULL,
  `lastdata` smallint(2) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//数据项存贮';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dataitem`
--

LOCK TABLES `dataitem` WRITE;
/*!40000 ALTER TABLE `dataitem` DISABLE KEYS */;
INSERT INTO `dataitem` VALUES ('11ce75e1ffd84aa3b33e4ccf7b1f1558',0,'','[1]','[{\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"\",\"index\":0}]','交通事件统计',0,'每日点名统计汇报','2017-11-20 09:05:21',1,1,'[]','',94,NULL,NULL),('142073c5b954407ba9c2bb9956542646',0,'','[1]','','指挥调度概况',0,'每日点名统计汇报','2017-11-21 09:21:05',3,0,'[4]','无',96,NULL,NULL),('1be2b30f135c437380170dcaed4b9ca4',0,'','[1]','','汇报日期',0,'每日点名统计汇报','2017-11-20 09:07:26',4,0,'[4]','',90,NULL,NULL),('271d7581436d475e8b5d503fb06ae0e2',1,'','[2,3,4,5]','','道路交通设施概况',0,'每日点名统计汇报','2017-11-21 09:20:58',3,0,'[4]','无',97,NULL,NULL),('3203b543a4a643e2a026b01c63237bda',0,'','[1]','','汇报日期',0,'每日交管动态汇报','2017-11-21 09:19:54',4,0,'[4]','',90,NULL,NULL),('3810df98d4734420bcad72ac406ff125',0,'','[1]','[{\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":true,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":true,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0}]','交通事件统计',0,'每日交管动态汇报','2017-11-30 19:24:19',1,1,'[]','',94,'[1]',0),('46b3cd6724f14d749f0fb32ad658db03',1,'','[1]','','撰写人',0,'每日交管动态汇报','2017-11-21 09:31:15',2,0,'[]','',92,NULL,NULL),('74d54782ad50418ab79306a2b35c8a65',1,'','[1]','','撰写人',0,'每日点名统计汇报','2017-11-21 09:21:59',2,0,'[]','',92,NULL,NULL),('7763f9f625f34c6c8d3e30f9027ee57e',1,'','[1]','','审核人',0,'每日点名统计汇报','2017-11-21 09:22:09',2,0,'[]','',91,NULL,NULL),('7bbaf2c9bc72476db09f925503292a73',0,'','[1]','','指挥调度概况',0,'每日交管动态汇报','2017-11-30 19:23:44',3,0,'[4]','无',96,'[1]',0),('7d190d17c6dd4e3989a7a849a1c23826',1,'','[1]','','工作动态',0,'每日点名统计汇报','2017-11-20 09:06:17',3,0,'[4]','',93,NULL,NULL),('88ae0eb8329242c09d42c7b2da0f8df3',1,'','[2,3,4,5,1]','','道路交通设施概况',0,'每日交管动态汇报','2017-11-30 15:09:55',3,0,'[4]','无',97,'[3,4,1]',1),('9a29801655244071822a715d3dc92790',0,'此处填写警务动态','[1]','[{\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":true,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":true,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"units\":[1],\"Mandated\":true,\"defaultValue\":\"0\",\"index\":0}]','勤务动态',0,'每日点名统计汇报','2017-11-21 09:20:24',1,1,'[]','',99,NULL,NULL),('9eb8073ffcaa4eb1ab74ad5f15d7e9b7',0,'','[1]','','市区路网交通运行评价',0,'每日点名统计汇报','2017-11-20 09:00:45',3,0,'[4]','',95,NULL,NULL),('9f30633ef1ba44fc9c8f8f8d839ceee4',1,'','[1]','','审核人',0,'每日交管动态汇报','2017-11-21 09:31:20',2,0,'[]','',91,NULL,NULL),('a6dba97a252a486fb22a33075ded99e9',0,'','[1]','[{\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"受伤人数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[2,1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"死亡事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[2,1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"死亡人数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0}]','交通事故概况',0,'每日交管动态汇报','2017-11-30 19:23:29',1,1,'[]','',98,'[1]',0),('c3f3f2f43a3c491989fd99fe4830eeca',0,'','[1]','','市区路网交通运行评价',0,'每日交管动态汇报','2017-11-30 19:23:56',3,0,'[4]','',95,'[1]',1),('c994bab56ca94413b8f8c09a99549a02',0,'此处填写警务动态','[1]','[{\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0}]','勤务动态',0,'每日交管动态汇报','2017-11-30 15:09:22',1,1,'[]','',99,'[1]',0),('cd7c4a29b69d4856819129f58419c4d3',1,'','[1]','','工作动态',0,'每日交管动态汇报','2017-11-30 19:24:29',3,0,'[4]','',93,'[1]',0),('ec3f3576a6d7404a80ffb650a04b3dc5',0,'','[1]','[{\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"受伤人数\",\"StatisticsType\":[4,5,1],\"sumunits\":[],\"units\":[3],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"死亡事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[],\"units\":[],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"secondtype\":1,\"name\":\"死亡人数\",\"StatisticsType\":[4,5,1],\"sumunits\":[],\"units\":[],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0}]','交通事故概况',0,'每日点名统计汇报','2017-11-28 14:44:04',1,1,'[]','',98,'[]',0);
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
INSERT INTO `moban` VALUES ('111original','','111original20171130193741.doc','每日交管动态汇报,origin','2017-11-30 19:37:41',0),('aaa','','aaa20171129142227.doc','每日交管动态汇报,print','2017-11-29 14:22:28',0),('sss','','sss20171129153240.doc','每日交管动态汇报,origin','2017-11-29 15:32:40',0),('每日交管动态汇报','','每日交管动态汇报20171130150225.doc','每日交管动态汇报,sum','2017-11-30 15:02:25',0),('每日点名统计汇报','','每日点名统计汇报20171129114900.doc','每日点名统计汇报,sum','2017-11-29 11:49:01',0);
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
INSERT INTO `reportsdata` VALUES ('2017-11-30','fushan','{\"date\":\"2017-11-30\",\"reportname\":\"每日交管动态汇报\",\"datalist\":[{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"汇报日期\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":4,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":90},{\"Content\":\"\",\"secondlist\":[{\"data\":\"88888\",\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"sumunits\":null,\"units\":[1],\"Mandated\":false,\"Lastdata\":true,\"defaultValue\":\"88888\",\"index\":0},{\"data\":\"99999\",\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"sumunits\":null,\"units\":[1],\"Mandated\":false,\"Lastdata\":true,\"defaultValue\":\"99999\",\"index\":0},{\"data\":\"10\",\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"sumunits\":null,\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"10\",\"index\":0},{\"data\":\"11\",\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"sumunits\":null,\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"11\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事件统计\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":94},{\"Content\":\"13\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"撰写人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"13\",\"tabletype\":\"每日交管动态汇报\",\"index\":92},{\"Content\":\"无6\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"指挥调度概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"无6\",\"tabletype\":\"每日交管动态汇报\",\"index\":96},{\"Content\":\"qixia栖霞招远啊\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"道路交通设施概况\",\"Mandated\":true,\"Lastdata\":true,\"Deleted\":false,\"Comment\":\"\",\"units\":[2,3,4,5,1],\"sumunits\":[3,4,1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"qixia栖霞招远啊\",\"tabletype\":\"每日交管动态汇报\",\"index\":97},{\"Content\":\"14\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"审核人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"14\",\"tabletype\":\"每日交管动态汇报\",\"index\":91},{\"Content\":\"\",\"secondlist\":[{\"data\":\"4\",\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"4\",\"index\":0},{\"data\":\"5\",\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"5\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事故概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":98},{\"Content\":\"77677\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"市区路网交通运行评价\",\"Mandated\":false,\"Lastdata\":true,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"77677\",\"tabletype\":\"每日交管动态汇报\",\"index\":95},{\"Content\":\"\",\"secondlist\":[{\"data\":\"1\",\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"1\",\"index\":0},{\"data\":\"2\",\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"2\",\"index\":0},{\"data\":\"3\",\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"3\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"勤务动态\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"此处填写警务动态\",\"units\":[1],\"sumunits\":[1],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":99},{\"Content\":\"12\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"工作动态\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"12\",\"tabletype\":\"每日交管动态汇报\",\"index\":93}],\"draft\":0}',0,'2017-11-30 19:21:48',NULL,0,NULL,'2017-11-30 19:25:52','每日交管动态汇报'),('2017-11-30','haiyang','{\"date\":\"2017-11-30\",\"reportname\":\"每日交管动态汇报\",\"datalist\":[{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"汇报日期\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":4,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":90},{\"Content\":\"\",\"secondlist\":[{\"data\":\"888881\",\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":true,\"defaultValue\":\"888881\",\"index\":0},{\"data\":\"999991\",\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":true,\"defaultValue\":\"999991\",\"index\":0},{\"data\":\"123\",\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"123\",\"index\":0},{\"data\":\"456\",\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"456\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事件统计\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":94},{\"Content\":\"3453453\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"撰写人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"3453453\",\"tabletype\":\"每日交管动态汇报\",\"index\":92},{\"Content\":\"无22222\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"指挥调度概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"无22222\",\"tabletype\":\"每日交管动态汇报\",\"index\":96},{\"Content\":\"qixia栖霞招远啊991111\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"道路交通设施概况\",\"Mandated\":true,\"Lastdata\":true,\"Deleted\":false,\"Comment\":\"\",\"units\":[2,3,4,5,1],\"sumunits\":[3,4,1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"qixia栖霞招远啊991111\",\"tabletype\":\"每日交管动态汇报\",\"index\":97},{\"Content\":\"345\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"审核人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"345\",\"tabletype\":\"每日交管动态汇报\",\"index\":91},{\"Content\":\"\",\"secondlist\":[{\"data\":\"666\",\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"666\",\"index\":0},{\"data\":\"777\",\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"777\",\"index\":0},{\"data\":\"888\",\"secondtype\":1,\"name\":\"受伤人数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[2,1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"888\",\"index\":0},{\"data\":\"999\",\"secondtype\":1,\"name\":\"死亡事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[2,1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"999\",\"index\":0},{\"data\":\"11111\",\"secondtype\":1,\"name\":\"死亡人数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"11111\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事故概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":98},{\"Content\":\"776773333\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"市区路网交通运行评价\",\"Mandated\":false,\"Lastdata\":true,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"776773333\",\"tabletype\":\"每日交管动态汇报\",\"index\":95},{\"Content\":\"\",\"secondlist\":[{\"data\":\"333\",\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"333\",\"index\":0},{\"data\":\"444\",\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"444\",\"index\":0},{\"data\":\"555\",\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"555\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"勤务动态\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"此处填写警务动态\",\"units\":[1],\"sumunits\":[1],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":99},{\"Content\":\"56456\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"工作动态\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"56456\",\"tabletype\":\"每日交管动态汇报\",\"index\":93}],\"draft\":0}',0,'2017-11-30 19:39:03',NULL,0,NULL,'2017-11-30 19:39:03','每日交管动态汇报'),('2017-11-30','laiyang','{\"date\":\"2017-11-30\",\"reportname\":\"每日交管动态汇报\",\"datalist\":[{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"汇报日期\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":4,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":90},{\"Content\":\"\",\"secondlist\":[{\"data\":\"8888812\",\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":true,\"defaultValue\":\"8888812\",\"index\":0},{\"data\":\"9999913\",\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":true,\"defaultValue\":\"9999913\",\"index\":0},{\"data\":\"77\",\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"77\",\"index\":0},{\"data\":\"88\",\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"88\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事件统计\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":94},{\"Content\":\"009\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"撰写人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"009\",\"tabletype\":\"每日交管动态汇报\",\"index\":92},{\"Content\":\"无555\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"指挥调度概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"无555\",\"tabletype\":\"每日交管动态汇报\",\"index\":96},{\"Content\":\"qixia栖霞招远啊991111444\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"道路交通设施概况\",\"Mandated\":true,\"Lastdata\":true,\"Deleted\":false,\"Comment\":\"\",\"units\":[2,3,4,5,1],\"sumunits\":[3,4,1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"qixia栖霞招远啊991111444\",\"tabletype\":\"每日交管动态汇报\",\"index\":97},{\"Content\":\"990\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"审核人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"990\",\"tabletype\":\"每日交管动态汇报\",\"index\":91},{\"Content\":\"\",\"secondlist\":[{\"data\":\"77777\",\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"77777\",\"index\":0},{\"data\":\"88888\",\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"88888\",\"index\":0},{\"data\":\"11111\",\"secondtype\":1,\"name\":\"受伤人数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[2,1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"11111\",\"index\":0},{\"data\":\"22222\",\"secondtype\":1,\"name\":\"死亡事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[2,1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"22222\",\"index\":0},{\"data\":\"33333\",\"secondtype\":1,\"name\":\"死亡人数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"33333\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事故概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":98},{\"Content\":\"776773333666\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"市区路网交通运行评价\",\"Mandated\":false,\"Lastdata\":true,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"776773333666\",\"tabletype\":\"每日交管动态汇报\",\"index\":95},{\"Content\":\"\",\"secondlist\":[{\"data\":\"44444\",\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"44444\",\"index\":0},{\"data\":\"55555\",\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"55555\",\"index\":0},{\"data\":\"66666\",\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"66666\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"勤务动态\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"此处填写警务动态\",\"units\":[1],\"sumunits\":[1],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":99},{\"Content\":\"99\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"工作动态\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"99\",\"tabletype\":\"每日交管动态汇报\",\"index\":93}],\"draft\":0}',0,'2017-11-30 19:39:55',NULL,0,NULL,'2017-11-30 19:39:55','每日交管动态汇报'),('2017-11-30','muping','{\"date\":\"2017-11-30\",\"reportname\":\"每日交管动态汇报\",\"datalist\":[{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"汇报日期\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":4,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":90},{\"Content\":\"\",\"secondlist\":[{\"data\":\"88888\",\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":true,\"defaultValue\":\"88888\",\"index\":0},{\"data\":\"99999\",\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":true,\"defaultValue\":\"99999\",\"index\":0},{\"data\":\"1111\",\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"1111\",\"index\":0},{\"data\":\"222\",\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"222\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事件统计\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":94},{\"Content\":\"444\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"撰写人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"444\",\"tabletype\":\"每日交管动态汇报\",\"index\":92},{\"Content\":\"无10\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"指挥调度概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"无10\",\"tabletype\":\"每日交管动态汇报\",\"index\":96},{\"Content\":\"qixia栖霞招远啊99\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"道路交通设施概况\",\"Mandated\":true,\"Lastdata\":true,\"Deleted\":false,\"Comment\":\"\",\"units\":[2,3,4,5,1],\"sumunits\":[3,4,1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"qixia栖霞招远啊99\",\"tabletype\":\"每日交管动态汇报\",\"index\":97},{\"Content\":\"555\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"审核人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"555\",\"tabletype\":\"每日交管动态汇报\",\"index\":91},{\"Content\":\"\",\"secondlist\":[{\"data\":\"44\",\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"44\",\"index\":0},{\"data\":\"55\",\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"55\",\"index\":0},{\"data\":\"66\",\"secondtype\":1,\"name\":\"受伤人数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[2,1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"66\",\"index\":0},{\"data\":\"77\",\"secondtype\":1,\"name\":\"死亡事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[2,1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"77\",\"index\":0},{\"data\":\"88\",\"secondtype\":1,\"name\":\"死亡人数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"88\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事故概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":98},{\"Content\":\"77677\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"市区路网交通运行评价\",\"Mandated\":false,\"Lastdata\":true,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"77677\",\"tabletype\":\"每日交管动态汇报\",\"index\":95},{\"Content\":\"\",\"secondlist\":[{\"data\":\"11\",\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"11\",\"index\":0},{\"data\":\"22\",\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"22\",\"index\":0},{\"data\":\"33\",\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"sumunits\":[1],\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"33\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"勤务动态\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"此处填写警务动态\",\"units\":[1],\"sumunits\":[1],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日交管动态汇报\",\"index\":99},{\"Content\":\"333\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"工作动态\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[1],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"333\",\"tabletype\":\"每日交管动态汇报\",\"index\":93}],\"draft\":0}',0,'2017-11-30 19:27:13',NULL,0,NULL,'2017-11-30 19:27:13','每日交管动态汇报'),('2017-12-01','changdao','{\"date\":\"2017-12-01\",\"reportname\":\"每日点名统计汇报\",\"datalist\":[{\"Content\":\"\",\"secondlist\":[{\"data\":\"\",\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"sumunits\":null,\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"sumunits\":null,\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"sumunits\":null,\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"sumunits\":null,\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事件统计\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日点名统计汇报\",\"index\":94},{\"Content\":\"无\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"指挥调度概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"无\",\"tabletype\":\"每日点名统计汇报\",\"index\":96},{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"汇报日期\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":4,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":\"每日点名统计汇报\",\"index\":90},{\"Content\":\"asdf\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"撰写人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"asdf\",\"tabletype\":\"每日点名统计汇报\",\"index\":92},{\"Content\":\"asdf\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"审核人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"asdf\",\"tabletype\":\"每日点名统计汇报\",\"index\":91},{\"Content\":\"asdf\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"工作动态\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"asdf\",\"tabletype\":\"每日点名统计汇报\",\"index\":93},{\"Content\":\"\",\"secondlist\":[{\"data\":\"0\",\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"data\":\"0\",\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"data\":\"0\",\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":[1],\"Mandated\":true,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"勤务动态\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"此处填写警务动态\",\"units\":[1],\"sumunits\":[],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日点名统计汇报\",\"index\":99},{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"市区路网交通运行评价\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":\"每日点名统计汇报\",\"index\":95},{\"Content\":\"\",\"secondlist\":[{\"data\":\"0\",\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0},{\"data\":\"0\",\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":[],\"units\":[1],\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"0\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事故概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":[],\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":\"每日点名统计汇报\",\"index\":98}],\"draft\":0}',0,'2017-12-01 11:58:06',NULL,0,NULL,'2017-12-01 11:58:06','每日点名统计汇报');
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
INSERT INTO `summarized` VALUES ('2017-11-28','{\"datastatus\":2,\"date\":\"2017-11-28\",\"reportname\":\"每日交管动态汇报\",\"datalist\":[{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"汇报日期\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":4,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":null,\"index\":90},{\"Content\":\"\",\"secondlist\":[{\"data\":\"\",\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事件统计\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":null,\"index\":94},{\"Content\":\"123\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"撰写人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"123\",\"tabletype\":null,\"index\":92},{\"Content\":\"132\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"指挥调度概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"132\",\"tabletype\":null,\"index\":96},{\"Content\":\"123\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"道路交通设施概况\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[2,3,4,5],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"123\",\"tabletype\":null,\"index\":97},{\"Content\":\"13\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"审核人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"13\",\"tabletype\":null,\"index\":91},{\"Content\":\"\",\"secondlist\":[{\"data\":\"\",\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"受伤人数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"死亡事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"死亡人数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事故概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":null,\"index\":98},{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"市区路网交通运行评价\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":null,\"index\":95},{\"Content\":\"\",\"secondlist\":[{\"data\":\"\",\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"勤务动态\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"此处填写警务动态\",\"units\":[1],\"sumunits\":null,\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":null,\"index\":99},{\"Content\":\"3\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"工作动态\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"3\",\"tabletype\":null,\"index\":93}],\"draft\":0}',2,'2017-11-30 15:02:49',NULL,'每日交管动态汇报'),('2017-11-29','{\"datastatus\":2,\"date\":\"2017-11-29\",\"reportname\":\"每日交管动态汇报\",\"datalist\":[{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"汇报日期\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":4,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":null,\"index\":90},{\"Content\":\"\",\"secondlist\":[{\"data\":\"\",\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事件统计\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":null,\"index\":94},{\"Content\":\"ddd\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"撰写人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"ddd\",\"tabletype\":null,\"index\":92},{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"指挥调度概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":null,\"index\":96},{\"Content\":\"aaa\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"道路交通设施概况\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[2,3,4,5],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"aaa\",\"tabletype\":null,\"index\":97},{\"Content\":\"fff\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"审核人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"fff\",\"tabletype\":null,\"index\":91},{\"Content\":\"\",\"secondlist\":[{\"data\":\"\",\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"受伤人数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"死亡事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"死亡人数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事故概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":null,\"index\":98},{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"市区路网交通运行评价\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":null,\"index\":95},{\"Content\":\"\",\"secondlist\":[{\"data\":\"\",\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0},{\"data\":\"\",\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"勤务动态\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"此处填写警务动态\",\"units\":[1],\"sumunits\":null,\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":null,\"index\":99},{\"Content\":\"sss\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"工作动态\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"sss\",\"tabletype\":null,\"index\":93}],\"draft\":0}',2,'2017-11-30 14:50:46',NULL,'每日交管动态汇报'),('2017-11-30','{\"datastatus\":2,\"date\":\"2017-11-30\",\"reportname\":\"每日交管动态汇报\",\"datalist\":[{\"Content\":\"\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"汇报日期\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":4,\"StatisticsType\":[4],\"defaultValue\":\"\",\"tabletype\":null,\"index\":90},{\"Content\":\"\",\"secondlist\":[{\"data\":\"123\",\"secondtype\":2,\"name\":\"辖区天气\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"123\",\"index\":0},{\"data\":\"234\",\"secondtype\":1,\"name\":\"最高气温\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"234\",\"index\":0},{\"data\":\"456\",\"secondtype\":1,\"name\":\"最低气温\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"456\",\"index\":0},{\"data\":\"789\",\"secondtype\":2,\"name\":\"预警事件\",\"StatisticsType\":[],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"789\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事件统计\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":null,\"index\":94},{\"Content\":\"121\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"撰写人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"121\",\"tabletype\":null,\"index\":92},{\"Content\":\"海阳：\\n无22222。\\n莱阳大队：\\n无555。\\n牟平：\\n无10。\\n\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"指挥调度概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"海阳：\\n无22222。\\n莱阳大队：\\n无555。\\n牟平：\\n无10。\\n\",\"tabletype\":null,\"index\":96},{\"Content\":\"福山大队：\\nqixia栖霞招远啊。\\n海阳：\\nqixia栖霞招远啊991111。\\n莱阳大队：\\nqixia栖霞招远啊991111444。\\n牟平：\\nqixia栖霞招远啊99。\\n\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"道路交通设施概况\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[2,3,4,5,1],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"福山大队：\\nqixia栖霞招远啊。\\n海阳：\\nqixia栖霞招远啊991111。\\n莱阳大队：\\nqixia栖霞招远啊991111444。\\n牟平：\\nqixia栖霞招远啊99。\\n\",\"tabletype\":null,\"index\":97},{\"Content\":\"234\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"审核人\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":2,\"StatisticsType\":[],\"defaultValue\":\"234\",\"tabletype\":null,\"index\":91},{\"Content\":\"\",\"secondlist\":[{\"data\":\"78491\",\"secondtype\":1,\"name\":\"交通事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"78491\",\"index\":0},{\"data\":\"89725\",\"secondtype\":1,\"name\":\"受伤事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"89725\",\"index\":0},{\"data\":\"12065\",\"secondtype\":1,\"name\":\"受伤人数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"12065\",\"index\":0},{\"data\":\"23298\",\"secondtype\":1,\"name\":\"死亡事故数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"23298\",\"index\":0},{\"data\":\"44532\",\"secondtype\":1,\"name\":\"死亡人数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"44532\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"交通事故概况\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":null,\"index\":98},{\"Content\":\"海阳：\\n776773333。\\n莱阳大队：\\n776773333666。\\n牟平：\\n77677。\\n\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"市区路网交通运行评价\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"海阳：\\n776773333。\\n莱阳大队：\\n776773333666。\\n牟平：\\n77677。\\n\",\"tabletype\":null,\"index\":95},{\"Content\":\"\",\"secondlist\":[{\"data\":\"44789\",\"secondtype\":1,\"name\":\"出警次数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"44789\",\"index\":0},{\"data\":\"56023\",\"secondtype\":1,\"name\":\"警车数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"56023\",\"index\":0},{\"data\":\"67257\",\"secondtype\":1,\"name\":\"现场执法数\",\"StatisticsType\":[4,5,1],\"sumunits\":null,\"units\":null,\"Mandated\":false,\"Lastdata\":false,\"defaultValue\":\"67257\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"勤务动态\",\"Mandated\":false,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"此处填写警务动态\",\"units\":[1],\"sumunits\":null,\"inputtype\":1,\"StatisticsType\":[],\"defaultValue\":\"\",\"tabletype\":null,\"index\":99},{\"Content\":\"海阳：\\n56456。\\n莱阳大队：\\n99。\\n牟平：\\n333。\\n\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"工作动态\",\"Mandated\":true,\"Lastdata\":false,\"Deleted\":false,\"Comment\":\"\",\"units\":[1],\"sumunits\":null,\"inputtype\":3,\"StatisticsType\":[4],\"defaultValue\":\"海阳：\\n56456。\\n莱阳大队：\\n99。\\n牟平：\\n333。\\n\",\"tabletype\":null,\"index\":93}],\"draft\":0}',2,'2017-11-30 20:18:03',NULL,'每日交管动态汇报');
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
  `index` smallint(3) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='大队设置';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `unit`
--

LOCK TABLES `unit` WRITE;
/*!40000 ALTER TABLE `unit` DISABLE KEYS */;
INSERT INTO `unit` VALUES ('center','10.50.230.253','中心',0,0),('changdao','10.231.53.209','长岛大队',1,7),('four','10.50.233.77','四大队',1,24),('fushan','10.231.52.53','福山大队',1,1),('haiyang','10.50.191.8','海阳',1,3),('jichang','10.50.219.241','机场大队',1,13),('kaifaqu','10.231.54.14','开发区大队',1,11),('laiyang','10.231.52.211','莱阳大队',1,4),('laizhou','10.231.59.103','莱州大队',1,10),('longkou','10.231.50.222','龙口大队',1,8),('muping','10.231.53.176','牟平',1,2),('one','10.50.230.253','一大队',1,21),('penglai','10.231.61.70','蓬莱大队',1,6),('qixia','10.231.52.99','栖霞大队',1,5),('three','10.50.231.183','三大队',1,23),('two','10.50.231.67','二大队',1,22),('yantaigang','10.231.55.189','港务大队',1,12),('zhaoyuan','10.231.200.87','招远大队',1,9);
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
INSERT INTO `user` VALUES ('admin','','admin','center','56c294824de248c09a87605f315fc48f',0,1,0),('c','c','c','center','d23e36a9ec7a49db8050da6c6b52b0ba',0,1,0),('cddd','','123456','changdao','3f55fb7d0abf4b78b5fa2efe9b0e2e5d',0,2,1),('fsdd','','123456','fushan','5ae0e7d33b3847818763b5031cbb5bb9',0,2,1),('gwdd','','123456','yantaigang','96b41ebf88284702a4272395e6751f0c',0,2,1),('hydd','','123456','haiyang','9326780ea5a14eda8af85f573962cf1f',0,2,1),('jcdd','','123456','jichang','5ea04d340d3f408eb0af24bf17f72ac8',0,2,1),('jj1dd','','123456','one','df0c00d077fe4e1b8575afa6706855c3',0,2,0),('jj2dd','','123456','two','866d3b5c97264ae887c17daa153789ab',0,2,0),('jj3dd','','123456','three','2b0b4df1f304447a937460b0b267d769',0,2,0),('jj4dd','','123456','four','6baedff5f3b84988a3208715f4f97ab7',0,2,0),('kfqdd','','123456','kaifaqu','568d4d79ea3b43529cbacf0262926c80',0,2,1),('lkdd','','123456','longkou','23b0c9fba32f4cac8c480ee4aefe8463',0,2,1),('lydd','','123456','laiyang','07e42652cb044bb5a3ae40bb2823bfe7',0,2,1),('lzdd','','123456','laizhou','121fdf618d8041e886903993ba7c8c9f',0,2,1),('mpdd','','123456','muping','8a51a2d61c44451fba52857c93c9a90e',0,2,1),('nerd','nerd','nerd','center','dab58b7e6cd944808cb77567d524c3d1',0,1,0),('pldd','','123456','penglai','9a1655468d36428caec61385173e57f9',0,2,1),('qxdd','','123456','qixia','ef8fc1b113c2423d979f0f8fa2a183e2',0,2,1),('zydd','','123456','zhaoyuan','86e99ed423c943ff8c621bc6c1ab99da',0,2,1);
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
) ENGINE=InnoDB AUTO_INCREMENT=611 DEFAULT CHARSET=utf8 COMMENT='使用日志';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userlog`
--

LOCK TABLES `userlog` WRITE;
/*!40000 ALTER TABLE `userlog` DISABLE KEYS */;
INSERT INTO `userlog` VALUES (543,'2017-11-22 10:11:08','c','login','119.180.98.142'),(544,'2017-11-22 10:16:41','admin','login','119.180.98.142'),(545,'2017-11-22 10:17:45','c','login','119.180.98.142'),(546,'2017-11-22 10:23:59','c','login','119.180.98.142'),(547,'2017-11-22 10:36:29','c','login','119.180.98.142'),(548,'2017-11-22 10:37:00','admin','login','119.180.98.142'),(549,'2017-11-22 10:37:00','c','login','119.180.98.142'),(550,'2017-11-27 10:29:51','admin','login','119.180.98.142'),(551,'2017-11-27 10:30:16','admin','login','119.180.98.142'),(552,'2017-11-27 14:30:22','c','login','119.180.98.142'),(553,'2017-11-27 15:01:00','nerd','login','119.180.98.142'),(554,'2017-11-27 16:37:47','nerd','login','119.180.98.142'),(555,'2017-11-27 16:38:09','c','login','119.180.98.142'),(556,'2017-11-27 17:29:26','cddd','login','119.180.98.142'),(557,'2017-11-27 17:38:45','c','login','119.180.98.142'),(558,'2017-11-27 17:48:08','cddd','login','119.180.98.142'),(559,'2017-11-27 17:49:47','c','login','119.180.98.142'),(560,'2017-11-27 19:35:08','cddd','login','119.180.98.142'),(561,'2017-11-28 14:38:39','c','login','112.224.69.18'),(562,'2017-11-28 14:41:48','c','login','112.224.69.18'),(563,'2017-11-28 14:43:01','cddd','login','112.224.69.18'),(564,'2017-11-28 14:43:30','c','login','112.224.69.18'),(565,'2017-11-28 14:44:27','cddd','login','112.224.69.18'),(566,'2017-11-28 14:44:56','c','login','112.224.69.18'),(567,'2017-11-28 14:48:07','cddd','login','112.224.69.18'),(568,'2017-11-28 14:58:42','c','login','112.224.69.18'),(569,'2017-11-28 15:00:24','nerd','login','221.0.90.52'),(570,'2017-11-28 15:03:17','fsdd','login','221.0.90.52'),(571,'2017-11-28 15:04:14','cddd','login','221.0.90.52'),(572,'2017-11-28 15:08:23','nerd','login','221.0.90.52'),(573,'2017-11-28 15:21:17','cddd','login','112.224.69.18'),(574,'2017-11-28 15:41:04','nerd','login','221.0.90.52'),(575,'2017-11-28 15:45:31','nerd','login','221.0.90.52'),(576,'2017-11-28 16:24:26','nerd','login','221.0.90.52'),(577,'2017-11-29 10:25:29','c','login','119.180.98.142'),(578,'2017-11-29 13:47:23','cddd','login','119.180.98.142'),(579,'2017-11-29 14:21:38','c','login','119.180.98.142'),(580,'2017-11-29 14:22:41','cddd','login','119.180.98.142'),(581,'2017-11-29 14:31:29','c','login','119.180.98.142'),(582,'2017-11-29 14:53:39','cddd','login','119.180.98.142'),(583,'2017-11-29 14:55:33','c','login','119.180.98.142'),(584,'2017-11-29 14:56:16','c','login','119.180.98.142'),(585,'2017-11-30 09:15:43','nerd','login','221.0.90.52'),(586,'2017-11-30 10:14:54','c','login','221.0.90.52'),(587,'2017-11-30 10:50:37','nerd','login','221.0.90.52'),(588,'2017-11-30 13:33:11','cddd','login','221.0.90.52'),(589,'2017-11-30 14:49:57','nerd','login','221.0.90.52'),(590,'2017-11-30 15:05:27','cddd','login','221.0.90.52'),(591,'2017-11-30 15:06:23','lzdd','login','221.0.90.52'),(592,'2017-11-30 15:07:04','hydd','login','221.0.90.52'),(593,'2017-11-30 15:07:57','nerd','login','221.0.90.52'),(594,'2017-11-30 15:10:57','qxdd','login','221.0.90.52'),(595,'2017-11-30 15:11:31','zydd','login','221.0.90.52'),(596,'2017-11-30 15:12:10','nerd','login','221.0.90.52'),(597,'2017-11-30 19:09:06','nerd','login','144.255.33.48'),(598,'2017-11-30 19:21:19','fsdd','login','144.255.33.48'),(599,'2017-11-30 19:21:56','nerd','login','144.255.33.48'),(600,'2017-11-30 19:25:06','fsdd','login','144.255.33.48'),(601,'2017-11-30 19:26:02','nerd','login','144.255.33.48'),(602,'2017-11-30 19:26:35','mpdd','login','144.255.33.48'),(603,'2017-11-30 19:27:22','nerd','login','144.255.33.48'),(604,'2017-11-30 19:29:21','c','login','144.255.33.48'),(605,'2017-11-30 19:37:54','hydd','login','144.255.33.48'),(606,'2017-11-30 19:39:12','lydd','login','144.255.33.48'),(607,'2017-11-30 19:40:04','c','login','144.255.33.48'),(608,'2017-12-01 11:25:40','c','login','112.224.65.214'),(609,'2017-12-01 11:57:48','cddd','login','112.224.65.214'),(610,'2017-12-01 11:58:12','c','login','112.224.65.214');
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

-- Dump completed on 2017-12-02  9:13:25
