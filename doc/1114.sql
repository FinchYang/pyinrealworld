-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 47.93.226.74    Database: tp
-- ------------------------------------------------------
-- Server version	5.7.19
use tp;
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
INSERT INTO `dataitem` VALUES ('1da9169c539c465780762eb0e718cbc7',1,'eee','[1]','','www',0,'每日点名统计','2017-11-13 10:32:40',1,0,'[1]','11',2),('5d80ae65791d40ce985167b4ad221a2a',0,'asssddddddd','[1]','','dateeeee',0,'每日点名统计','2017-11-13 11:03:14',4,0,'[1,1]','2016-07-07',1),('d3791bf1f2f34a1fba8db1c7a8c571f6',0,'ssss','[1]','[{\"secondtype\":1,\"name\":\"aa\",\"StatisticsType\":[1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"1\",\"index\":0},{\"secondtype\":1,\"name\":\"ss\",\"StatisticsType\":[1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0}]','a',0,'每日点名统计','2017-11-12 11:38:09',1,1,'[1]','',0),('e1693f07516140379b6203f41df387a6',0,'qwer','[1]','','qwer',0,'每日点名统计','2017-11-13 10:21:44',3,0,'[1]','',20);
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
INSERT INTO `moban` VALUES ('aass','32141234','aass20171106142013.doc','four','2017-11-06 14:20:13',0),('tttt','yyyy','tttt20171107214033.doc','nine','2017-11-07 21:40:33',0);
/*!40000 ALTER TABLE `moban` ENABLE KEYS */;
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
INSERT INTO `reports` VALUES ('报告1','111','four','[1]'),('每日点名统计','test1','nine','[1]');
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
  `content` varchar(9000) NOT NULL,
  `draft` smallint(2) NOT NULL COMMENT '1-草稿，0-提交，2-拒绝，3-同意,4;//签到\n//1--草稿，0-正式提交,2-退回\n-----1-草稿，0-提交到网站，2-保存word文档到本地，3-待定',
  `time` datetime NOT NULL,
  `comment` varchar(450) DEFAULT NULL,
  `signtype` smallint(2) NOT NULL DEFAULT '0',
  `declinereason` varchar(450) DEFAULT NULL,
  `submittime` datetime DEFAULT NULL,
  `rname` varchar(100) NOT NULL,
  PRIMARY KEY (`date`,`unitid`),
  KEY `reportsdataunitid_idx` (`unitid`),
  CONSTRAINT `reportsdataunitid` FOREIGN KEY (`unitid`) REFERENCES `unit` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//每日上报数据，包括草稿和提交，';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reportsdata`
--

LOCK TABLES `reportsdata` WRITE;
/*!40000 ALTER TABLE `reportsdata` DISABLE KEYS */;
INSERT INTO `reportsdata` VALUES ('2017-11-01','haiyang','{\"date\":\"2017-11-01\",\"reportname\":\"每日点名统计\",\"datalist\":[{\"Content\":\"asdf\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"qwer\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"qwer\",\"units\":[1],\"inputtype\":3,\"StatisticsType\":[1],\"defaultValue\":\"asdf\",\"tabletype\":\"每日点名统计\",\"index\":20},{\"Content\":\"11\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"www\",\"Mandated\":true,\"Deleted\":false,\"Comment\":\"eee\",\"units\":[1],\"inputtype\":1,\"StatisticsType\":[1],\"defaultValue\":\"11\",\"tabletype\":\"每日点名统计\",\"index\":2},{\"Content\":\"2016-07-07\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"dateeeee\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"asssddddddd\",\"units\":[1],\"inputtype\":4,\"StatisticsType\":[1,1],\"defaultValue\":\"2016-07-07\",\"tabletype\":\"每日点名统计\",\"index\":1},{\"Content\":\"\",\"secondlist\":[{\"data\":\"1\",\"secondtype\":1,\"name\":\"aa\",\"StatisticsType\":[1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"1\",\"index\":0},{\"data\":\"0\",\"secondtype\":1,\"name\":\"ss\",\"StatisticsType\":[1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"a\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"ssss\",\"units\":[1],\"inputtype\":1,\"StatisticsType\":[1],\"defaultValue\":\"\",\"tabletype\":\"每日点名统计\",\"index\":0}],\"draft\":0}',0,'2017-11-13 11:26:26',NULL,0,NULL,'2017-11-13 11:26:26','每日点名统计'),('2017-11-13','haiyang','{\"date\":\"2017-11-13\",\"reportname\":\"每日点名统计\",\"datalist\":[{\"Content\":\"dsaf\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"qwer\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"qwer\",\"units\":[1],\"inputtype\":3,\"StatisticsType\":[1],\"defaultValue\":\"dsaf\",\"tabletype\":\"每日点名统计\",\"index\":20},{\"Content\":\"11\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"www\",\"Mandated\":true,\"Deleted\":false,\"Comment\":\"eee\",\"units\":[1],\"inputtype\":1,\"StatisticsType\":[1],\"defaultValue\":\"11\",\"tabletype\":\"每日点名统计\",\"index\":2},{\"Content\":\"2016-07-07\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"dateeeee\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"asssddddddd\",\"units\":[1],\"inputtype\":4,\"StatisticsType\":[1,1],\"defaultValue\":\"2016-07-07\",\"tabletype\":\"每日点名统计\",\"index\":1},{\"Content\":\"\",\"secondlist\":[{\"data\":\"1\",\"secondtype\":1,\"name\":\"aa\",\"StatisticsType\":[1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"1\",\"index\":0},{\"data\":\"0\",\"secondtype\":1,\"name\":\"ss\",\"StatisticsType\":[1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"4\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"a\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"ssss\",\"units\":[1],\"inputtype\":1,\"StatisticsType\":[1],\"defaultValue\":\"\",\"tabletype\":\"每日点名统计\",\"index\":0}],\"draft\":0}',4,'2017-11-13 11:24:14',NULL,2,'ssssssss','2017-11-13 11:30:23','每日点名统计'),('2017-11-13','one','{\"date\":\"2017-11-13\",\"reportname\":\"每日点名统计\",\"datalist\":[{\"Content\":\"adfasd\\nsadf\\nas\\ndf\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"qwer\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"qwer\",\"units\":[1],\"inputtype\":3,\"StatisticsType\":[1],\"defaultValue\":\"adfasd\\nsadf\\nas\\ndf\",\"tabletype\":\"每日点名统计\",\"index\":20},{\"Content\":\"11\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"www\",\"Mandated\":true,\"Deleted\":false,\"Comment\":\"eee\",\"units\":[1],\"inputtype\":1,\"StatisticsType\":[1],\"defaultValue\":\"11\",\"tabletype\":\"每日点名统计\",\"index\":2},{\"Content\":\"2016-07-08\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"date\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"asssddddddd\",\"units\":[1],\"inputtype\":4,\"StatisticsType\":[1,1],\"defaultValue\":\"2016-07-08\",\"tabletype\":\"每日点名统计\",\"index\":1},{\"Content\":\"\",\"secondlist\":[{\"data\":\"1\",\"secondtype\":1,\"name\":\"aa\",\"StatisticsType\":[1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"1\",\"index\":0},{\"data\":\"0\",\"secondtype\":1,\"name\":\"ss\",\"StatisticsType\":[1],\"units\":[1],\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0}],\"hasSecondItems\":true,\"Name\":\"a\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"ssss\",\"units\":[1],\"inputtype\":1,\"StatisticsType\":[1],\"defaultValue\":\"\",\"tabletype\":\"每日点名统计\",\"index\":0}],\"draft\":0}',4,'2017-11-13 10:46:37',NULL,5,NULL,'2017-11-13 10:46:54','每日点名统计');
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
  `content` varchar(9000) NOT NULL,
  `draft` smallint(2) NOT NULL COMMENT '1-草稿，0-提交，2-拒绝，3-同意',
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
INSERT INTO `summarized` VALUES ('2017-11-13','{\"datastatus\":2,\"date\":\"2017-11-13\",\"reportname\":\"每日交管动态汇总\",\"datalist\":[{\"Content\":\"1111\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"www\",\"Mandated\":true,\"Deleted\":false,\"Comment\":\"eee\",\"units\":[1],\"inputtype\":1,\"StatisticsType\":null,\"defaultValue\":\"1111\",\"tabletype\":null,\"index\":0},{\"Content\":\"2016-07-07\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"dateeeee\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"asssddddddd\",\"units\":[1],\"inputtype\":4,\"StatisticsType\":null,\"defaultValue\":\"2016-07-07\",\"tabletype\":null,\"index\":0},{\"Content\":\"\",\"secondlist\":[{\"data\":\"9\",\"secondtype\":1,\"name\":\"aa\",\"StatisticsType\":null,\"units\":null,\"Mandated\":false,\"defaultValue\":\"9\",\"index\":0},{\"data\":\"0\",\"secondtype\":1,\"name\":\"ss\",\"StatisticsType\":null,\"units\":null,\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0}],\"hasSecondItems\":false,\"Name\":\"a\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"ssss\",\"units\":[1],\"inputtype\":1,\"StatisticsType\":null,\"defaultValue\":\"\",\"tabletype\":null,\"index\":0},{\"Content\":\"dsafadfasd\\nsadf\\nas\\ndf\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"qwer\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"qwer\",\"units\":[1],\"inputtype\":3,\"StatisticsType\":null,\"defaultValue\":\"dsafadfasd\\nsadf\\nas\\ndf\",\"tabletype\":null,\"index\":0}],\"draft\":0}',2,'2017-11-13 15:35:47',NULL,'每日交管动态汇总'),('2017-11-13','{\"datastatus\":2,\"date\":\"2017-11-13\",\"reportname\":\"每日点名统计\",\"datalist\":[{\"Content\":\"1111\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"www\",\"Mandated\":true,\"Deleted\":false,\"Comment\":\"eee\",\"units\":[1],\"inputtype\":1,\"StatisticsType\":null,\"defaultValue\":\"1111\",\"tabletype\":null,\"index\":0},{\"Content\":\"2016-07-07\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"dateeeee\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"asssddddddd\",\"units\":[1],\"inputtype\":4,\"StatisticsType\":null,\"defaultValue\":\"2016-07-07\",\"tabletype\":null,\"index\":0},{\"Content\":\"\",\"secondlist\":[{\"data\":\"9\",\"secondtype\":1,\"name\":\"aa\",\"StatisticsType\":null,\"units\":null,\"Mandated\":false,\"defaultValue\":\"9\",\"index\":0},{\"data\":\"0\",\"secondtype\":1,\"name\":\"ss\",\"StatisticsType\":null,\"units\":null,\"Mandated\":false,\"defaultValue\":\"0\",\"index\":0}],\"hasSecondItems\":false,\"Name\":\"a\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"ssss\",\"units\":[1],\"inputtype\":1,\"StatisticsType\":null,\"defaultValue\":\"\",\"tabletype\":null,\"index\":0},{\"Content\":\"dsafadfasd\\nsadf\\nas\\ndf\",\"secondlist\":[],\"hasSecondItems\":false,\"Name\":\"qwer\",\"Mandated\":false,\"Deleted\":false,\"Comment\":\"qwer\",\"units\":[1],\"inputtype\":3,\"StatisticsType\":null,\"defaultValue\":\"dsafadfasd\\nsadf\\nas\\ndf\",\"tabletype\":null,\"index\":0}],\"draft\":0}',2,'2017-11-13 15:46:59',NULL,'每日点名统计'),('2017-11-14','{\"datastatus\":1,\"date\":\"2017-11-14\",\"reportname\":\"报告1\",\"datalist\":[],\"draft\":0}',1,'2017-11-14 09:19:25',NULL,'报告1');
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
INSERT INTO `unit` VALUES ('center','10.50.230.253','中心',0),('haiyang','2','海阳',1),('local','*','某大队',1),('muping','1','牟平',1),('one','1','一大堆',1);
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
INSERT INTO `user` VALUES ('aaa','aaname','123456','haiyang','9b38345f118748c28d20992525418421',0,1),('c','c','c','center','3eec7d201fa24f02a1f8ea037f20fb12',0,0),('center',NULL,'center','center','c1f89bfa10014a5085a476787ad01f76',0,0),('csdd','测试大队','123456','one','8a3b9261025b490393bf5a20c0b3441a',0,1),('hy','mp','mp','muping','332a97344bac4a81be652b2dd8d1cbe5',0,1),('hy1','hy1','hy1','haiyang','5c09233f24ff4e96be3a002ac5a779c7',0,1),('test1','测试1','test1','one','ac8e34041e50415f944af1f677457caf',0,1);
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
) ENGINE=InnoDB AUTO_INCREMENT=212 DEFAULT CHARSET=utf8 COMMENT='使用日志';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userlog`
--

LOCK TABLES `userlog` WRITE;
/*!40000 ALTER TABLE `userlog` DISABLE KEYS */;
INSERT INTO `userlog` VALUES (1,'2017-10-21 17:12:22','test1','login','::1'),(2,'2017-10-23 10:48:58','test1','login','221.0.90.52'),(3,'2017-10-23 10:53:17','test1','login','221.0.90.52'),(4,'2017-10-23 10:55:33','test1','login','221.0.90.52'),(5,'2017-10-23 10:56:33','test1','login','221.0.90.52'),(6,'2017-10-23 11:09:06','test1','login','::1'),(7,'2017-10-23 11:10:35','test1','login','221.0.90.52'),(8,'2017-10-23 11:14:03','test1','login','221.0.90.52'),(9,'2017-10-23 13:41:23','test1','login','221.0.90.52'),(10,'2017-10-24 08:41:39','test1','login','221.0.90.52'),(11,'2017-10-27 10:29:32','center','login','::1'),(12,'2017-10-28 14:38:00','center','login','::1'),(14,'2017-10-31 10:48:05','center','content','ip'),(15,'2017-10-31 10:52:07','center','content','ip'),(16,'2017-11-01 09:57:48','center','login','221.0.90.52'),(17,'2017-11-01 10:22:37','center','login','221.0.90.52'),(18,'2017-11-01 11:10:53','center','login','221.0.90.52'),(19,'2017-11-01 11:12:20','center','login','221.0.90.52'),(20,'2017-11-01 13:55:16','center','login','221.0.90.52'),(21,'2017-11-01 13:55:41','center','login','221.0.90.52'),(22,'2017-11-01 13:56:18','test1','login','221.0.90.52'),(23,'2017-11-01 13:58:10','test1','login','221.0.90.52'),(24,'2017-11-01 13:58:43','test1','login','221.0.90.52'),(25,'2017-11-01 14:00:56','test1','login','221.0.90.52'),(26,'2017-11-01 14:07:21','center','login','221.0.90.52'),(27,'2017-11-01 14:12:42','test1','login','221.0.90.52'),(28,'2017-11-01 15:39:21','test1','login','221.0.90.52'),(29,'2017-11-01 15:41:54','center','login','221.0.90.52'),(30,'2017-11-01 16:34:22','center','login','221.0.90.52'),(31,'2017-11-01 16:39:30','center','login','221.0.90.52'),(32,'2017-11-02 08:36:37','test1','login','221.0.90.52'),(33,'2017-11-02 09:22:54','test1','login','221.0.90.52'),(34,'2017-11-02 09:27:28','center','login','::1'),(35,'2017-11-02 09:28:50','test1','login','221.0.90.52'),(36,'2017-11-02 09:28:56','test1','login','::1'),(37,'2017-11-02 09:31:44','test1','login','221.0.90.52'),(38,'2017-11-02 10:49:35','test1','login','221.0.90.52'),(39,'2017-11-02 11:55:16','test1','login','221.0.90.52'),(40,'2017-11-02 14:13:30','test1','login','221.0.90.52'),(41,'2017-11-02 14:13:58','center','login','221.0.90.52'),(42,'2017-11-02 14:21:37','center','login','221.0.90.52'),(43,'2017-11-02 14:22:35','test1','login','221.0.90.52'),(44,'2017-11-02 14:27:16','center','login','221.0.90.52'),(45,'2017-11-02 14:36:30','test1','login','221.0.90.52'),(46,'2017-11-02 14:38:06','test1','login','221.0.90.52'),(47,'2017-11-02 15:03:52','test1','login','221.0.90.52'),(48,'2017-11-02 15:04:14','test1','login','221.0.90.52'),(49,'2017-11-02 15:08:09','center','login','221.0.90.52'),(50,'2017-11-02 15:08:26','test1','login','221.0.90.52'),(51,'2017-11-02 15:31:34','center','login','221.0.90.52'),(52,'2017-11-02 16:21:38','test1','login','221.0.90.52'),(53,'2017-11-02 16:29:53','test1','login','221.0.90.52'),(54,'2017-11-02 16:30:54','center','login','221.0.90.52'),(55,'2017-11-02 16:31:21','test1','login','221.0.90.52'),(56,'2017-11-02 16:40:34','center','login','221.0.90.52'),(57,'2017-11-02 18:53:30','test1','login','119.180.98.142'),(58,'2017-11-02 18:55:28','center','login','119.180.98.142'),(59,'2017-11-02 19:09:11','test1','login','119.180.98.142'),(60,'2017-11-02 19:12:50','center','login','119.180.98.142'),(61,'2017-11-02 20:57:15','center','login','119.180.98.142'),(62,'2017-11-02 20:57:22','center','login','119.180.98.142'),(63,'2017-11-02 21:01:33','test1','login','119.180.98.142'),(64,'2017-11-03 10:07:49','test1','login','221.0.90.52'),(65,'2017-11-03 10:47:26','test1','login','45.77.175.216'),(66,'2017-11-03 10:50:21','test1','login','221.0.90.52'),(67,'2017-11-03 10:51:28','center','login','221.0.90.52'),(68,'2017-11-03 11:08:34','center','login','221.0.90.52'),(69,'2017-11-03 16:42:43','test1','login','119.180.98.142'),(70,'2017-11-03 16:43:02','center','login','119.180.98.142'),(71,'2017-11-03 17:37:11','test1','login','119.180.98.142'),(72,'2017-11-03 17:37:26','center','login','119.180.98.142'),(73,'2017-11-04 10:59:33','test1','login','221.0.90.52'),(74,'2017-11-04 10:59:53','test1','login','221.0.90.52'),(75,'2017-11-04 11:01:23','center','login','221.0.90.52'),(76,'2017-11-04 11:07:15','center','login','221.0.90.52'),(77,'2017-11-04 13:28:53','center','login','221.0.90.52'),(78,'2017-11-04 13:38:01','test1','login','221.0.90.52'),(79,'2017-11-04 13:44:34','test1','login','::1'),(80,'2017-11-04 13:49:22','test1','login','::1'),(81,'2017-11-04 13:50:18','hy','login','::1'),(82,'2017-11-04 13:50:42','center','login','::1'),(83,'2017-11-04 14:13:32','center','login','172.104.188.115'),(84,'2017-11-04 14:13:57','test1','login','172.104.188.115'),(85,'2017-11-04 15:02:54','test1','login','221.0.90.52'),(86,'2017-11-04 15:17:37','center','login','221.0.90.52'),(87,'2017-11-04 16:35:41','hy','login','221.0.90.52'),(88,'2017-11-04 16:37:35','center','login','221.0.90.52'),(89,'2017-11-04 16:45:10','center','login','221.0.90.52'),(90,'2017-11-06 08:31:53','center','login','119.180.98.142'),(91,'2017-11-06 08:44:35','center','login','221.0.90.52'),(92,'2017-11-06 13:15:26','center','login','221.0.90.52'),(93,'2017-11-06 13:20:37','center','login','221.0.90.52'),(94,'2017-11-06 13:23:13','center','login','221.0.90.52'),(95,'2017-11-06 13:24:33','center','login','221.0.90.52'),(96,'2017-11-06 14:40:35','test1','login','221.0.90.52'),(97,'2017-11-06 14:41:10','center','login','221.0.90.52'),(98,'2017-11-06 15:02:12','center','login','119.180.98.142'),(99,'2017-11-06 15:06:59','test1','login','119.180.98.142'),(100,'2017-11-06 15:28:31','center','login','221.0.90.52'),(101,'2017-11-06 16:30:17','center','login','119.180.98.142'),(102,'2017-11-06 16:49:43','center','login','221.0.90.52'),(103,'2017-11-06 17:15:22','test1','login','221.0.90.52'),(104,'2017-11-07 10:08:24','center','login','119.180.98.142'),(105,'2017-11-07 10:35:16','test1','login','119.180.98.142'),(106,'2017-11-07 11:56:36','center','login','119.180.98.142'),(107,'2017-11-07 11:57:24','test1','login','119.180.98.142'),(108,'2017-11-07 14:35:45','center','login','::1'),(109,'2017-11-07 15:41:15','test1','login','119.180.98.142'),(110,'2017-11-07 17:16:33','test1','login','119.180.98.142'),(111,'2017-11-07 17:23:29','center','login','119.180.98.142'),(112,'2017-11-07 20:55:37','center','login','119.180.98.142'),(113,'2017-11-07 21:26:23','center','login','119.180.98.142'),(114,'2017-11-07 21:29:00','center','login','119.180.98.142'),(115,'2017-11-07 21:31:48','center','login','119.180.98.142'),(116,'2017-11-07 21:41:41','test1','login','119.180.98.142'),(117,'2017-11-07 21:42:00','center','login','119.180.98.142'),(118,'2017-11-07 21:45:31','test1','login','119.180.98.142'),(119,'2017-11-08 08:11:53','center','login','119.180.98.142'),(120,'2017-11-08 08:35:10','test1','login','119.180.98.142'),(121,'2017-11-08 08:40:38','center','login','221.0.90.52'),(122,'2017-11-08 08:44:15','center','login','119.180.98.142'),(123,'2017-11-08 09:13:58','test1','login','119.180.98.142'),(124,'2017-11-08 09:30:12','center','login','119.180.98.142'),(125,'2017-11-08 09:49:23','test1','login','119.180.98.142'),(126,'2017-11-08 11:47:06','test1','login','119.180.98.142'),(127,'2017-11-08 11:47:25','center','login','119.180.98.142'),(128,'2017-11-08 15:07:41','test1','login','119.180.98.142'),(129,'2017-11-08 15:11:59','center','login','119.180.98.142'),(130,'2017-11-08 15:30:27','c','login','221.0.90.52'),(131,'2017-11-08 15:33:55','c','login','119.180.98.142'),(132,'2017-11-08 15:45:32','hy1','login','221.0.90.52'),(133,'2017-11-08 19:21:03','test1','login','119.180.98.142'),(134,'2017-11-08 19:21:39','center','login','119.180.98.142'),(135,'2017-11-08 21:24:49','test1','login','119.180.98.142'),(136,'2017-11-08 21:33:14','center','login','119.180.98.142'),(137,'2017-11-08 21:36:27','test1','login','119.180.98.142'),(138,'2017-11-08 21:50:57','center','login','119.180.98.142'),(139,'2017-11-09 07:54:40','c','login','119.180.98.142'),(140,'2017-11-09 08:27:43','center','login','221.0.90.52'),(141,'2017-11-09 09:24:45','test1','login','119.180.98.142'),(142,'2017-11-09 09:46:10','c','login','119.180.98.142'),(143,'2017-11-09 10:07:30','c','login','::1'),(144,'2017-11-09 10:09:05','c','login','119.180.98.142'),(145,'2017-11-09 10:09:58','c','login','192.168.8.5'),(146,'2017-11-09 10:24:07','c','login','119.180.98.142'),(147,'2017-11-09 10:25:06','c','login','119.180.98.142'),(148,'2017-11-10 08:27:30','test1','login','119.180.98.142'),(149,'2017-11-10 08:27:49','center','login','119.180.98.142'),(150,'2017-11-10 08:28:28','test1','login','119.180.98.142'),(151,'2017-11-10 08:30:48','center','login','119.180.98.142'),(152,'2017-11-10 08:42:45','hy1','login','119.180.98.142'),(153,'2017-11-10 08:47:13','test1','login','119.180.98.142'),(154,'2017-11-10 08:51:55','hy1','login','119.180.98.142'),(155,'2017-11-10 08:59:43','test1','login','119.180.98.142'),(156,'2017-11-10 09:00:31','center','login','119.180.98.142'),(157,'2017-11-10 09:14:31','center','login','119.180.98.142'),(158,'2017-11-10 09:14:44','test1','login','119.180.98.142'),(159,'2017-11-10 09:15:21','center','login','119.180.98.142'),(160,'2017-11-10 09:41:32','hy1','login','119.180.98.142'),(161,'2017-11-10 09:41:37','test1','login','119.180.98.142'),(162,'2017-11-10 09:41:55','center','login','119.180.98.142'),(163,'2017-11-10 14:29:08','c','login','119.180.98.142'),(164,'2017-11-10 14:33:00','hy1','login','119.180.98.142'),(165,'2017-11-10 15:07:50','center','login','119.180.98.142'),(166,'2017-11-10 16:23:13','c','login','119.180.98.142'),(167,'2017-11-10 17:34:14','test1','login','119.180.98.142'),(168,'2017-11-12 09:12:30','test1','login','119.180.98.142'),(169,'2017-11-12 09:13:09','center','login','119.180.98.142'),(170,'2017-11-12 09:15:02','test1','login','119.180.98.142'),(171,'2017-11-12 09:35:41','hy1','login','182.42.64.114'),(172,'2017-11-12 09:56:54','center','login','119.180.98.142'),(173,'2017-11-12 10:12:12','test1','login','119.180.98.142'),(174,'2017-11-12 10:22:14','center','login','119.180.98.142'),(175,'2017-11-12 11:24:44','center','login','119.180.98.142'),(176,'2017-11-12 11:26:48','center','login','119.180.98.142'),(177,'2017-11-12 11:35:23','c','login','182.42.64.114'),(178,'2017-11-12 13:44:38','c','login','182.42.64.114'),(179,'2017-11-12 14:24:09','center','login','119.180.98.142'),(180,'2017-11-12 14:25:21','center','login','119.180.98.142'),(181,'2017-11-12 15:41:50','csdd','login','119.180.98.142'),(182,'2017-11-12 15:42:24','csdd','login','119.180.98.142'),(183,'2017-11-12 16:05:23','center','login','119.180.98.142'),(184,'2017-11-13 08:28:25','c','login','119.180.98.142'),(185,'2017-11-13 08:29:50','hy1','login','119.180.98.142'),(186,'2017-11-13 08:31:33','center','login','119.180.98.142'),(187,'2017-11-13 10:22:16','test1','login','119.180.98.142'),(188,'2017-11-13 10:30:22','center','login','119.180.98.142'),(189,'2017-11-13 10:46:15','test1','login','119.180.98.142'),(190,'2017-11-13 10:47:05','center','login','119.180.98.142'),(191,'2017-11-13 11:06:32','center','login','119.180.98.142'),(192,'2017-11-13 11:07:59','hy1','login','119.180.98.142'),(193,'2017-11-13 11:14:30','c','login','119.180.98.142'),(194,'2017-11-13 11:17:07','hy1','login','119.180.98.142'),(195,'2017-11-13 11:26:13','hy1','login','119.180.98.142'),(196,'2017-11-13 11:26:37','center','login','119.180.98.142'),(197,'2017-11-13 11:27:03','hy1','login','119.180.98.142'),(198,'2017-11-13 11:29:34','center','login','119.180.98.142'),(199,'2017-11-13 11:29:57','hy1','login','119.180.98.142'),(200,'2017-11-13 11:30:36','center','login','119.180.98.142'),(201,'2017-11-13 14:09:59','c','login','119.180.98.142'),(202,'2017-11-13 14:20:55','c','login','119.180.98.142'),(203,'2017-11-13 15:09:14','c','login','119.180.98.142'),(204,'2017-11-13 16:04:02','center','login','119.180.98.142'),(205,'2017-11-13 16:18:43','center','login','119.180.98.142'),(206,'2017-11-14 09:18:35','center','login','119.180.98.142'),(207,'2017-11-14 09:26:26','center','login','119.180.98.142'),(208,'2017-11-14 09:41:22','c','login','119.180.98.142'),(209,'2017-11-14 09:46:05','c','login','119.180.98.142'),(210,'2017-11-14 11:10:57','center','login','119.180.98.142'),(211,'2017-11-14 11:18:24','aaa','login','119.180.98.142');
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
  `content` varchar(9000) NOT NULL,
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

-- Dump completed on 2017-11-14 11:18:48
