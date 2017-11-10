CREATE DATABASE  IF NOT EXISTS `tp` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `tp`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 47.93.226.74    Database: tp
-- ------------------------------------------------------
-- Server version	5.7.19

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
-- Table structure for table `items-deprecated`
--

DROP TABLE IF EXISTS `items-deprecated`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `items-deprecated` (
  `id` int(11) NOT NULL,
  `mandated` smallint(2) NOT NULL DEFAULT '1',
  `comment` varchar(300) DEFAULT NULL,
  `units` varchar(300) NOT NULL,
  `seconditem` varchar(5000) DEFAULT NULL,
  `name` varchar(150) NOT NULL,
  `deleted` smallint(2) NOT NULL DEFAULT '0',
  `tabletype` smallint(2) NOT NULL DEFAULT '0' COMMENT '//\n  public enum dataItemType\n    {\n        unknown,\n        all=1,//所有表格适用\n        nine=9,//9点点名\n        four=4,//4点汇报\n    }',
  `time` datetime NOT NULL,
  `inputtype` smallint(2) NOT NULL DEFAULT '2' COMMENT ' public enum secondItemType\n    {\n        unknown,\n        number,//\n        text,\n        date,\n        radio//单选框\n    }',
  `hassecond` smallint(2) NOT NULL DEFAULT '0',
  `statisticstype` smallint(2) NOT NULL DEFAULT '0' COMMENT '  public enum StatisticsType\n    {\n        unknown,//未知\n        sum,//求和\n        average,//平均\n        collect,//汇总\n        yearoveryear,//同比\n        linkrelative,//环比\n    }',
  `defaultvalue` varchar(450) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

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
-- Table structure for table `reportlog--deprecated`
--

DROP TABLE IF EXISTS `reportlog--deprecated`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `reportlog--deprecated` (
  `date` varchar(10) NOT NULL,
  `unitid` varchar(50) NOT NULL,
  `content` varchar(4500) NOT NULL,
  `draft` smallint(2) NOT NULL DEFAULT '1' COMMENT '1-草稿，0-提交，2-拒绝，3-同意',
  `time` datetime NOT NULL,
  `declinereason` varchar(450) DEFAULT NULL,
  `submittime` datetime DEFAULT NULL,
  PRIMARY KEY (`date`,`unitid`),
  KEY `reportlogunitid_idx` (`unitid`),
  CONSTRAINT `reportlogunitid` FOREIGN KEY (`unitid`) REFERENCES `unit` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

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
-- Table structure for table `reportsdata`
--

DROP TABLE IF EXISTS `reportsdata`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `reportsdata` (
  `date` varchar(10) NOT NULL,
  `unitid` varchar(50) NOT NULL,
  `content` varchar(9000) NOT NULL,
  `draft` smallint(2) NOT NULL COMMENT '1-草稿，0-提交，2-拒绝，3-同意\n//1--草稿，0-正式提交,2-退回\n-----1-草稿，0-提交到网站，2-保存word文档到本地，3-待定',
  `time` datetime NOT NULL,
  `comment` varchar(450) DEFAULT NULL,
  `signtype` smallint(2) NOT NULL DEFAULT '0',
  `declinereason` varchar(450) DEFAULT NULL,
  `submittime` datetime DEFAULT NULL,
  `rname` varchar(100) NOT NULL,
  PRIMARY KEY (`date`,`unitid`),
  UNIQUE KEY `date_UNIQUE` (`date`),
  KEY `reportsdataunitid_idx` (`unitid`),
  CONSTRAINT `reportsdataunitid` FOREIGN KEY (`unitid`) REFERENCES `unit` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='//每日上报数据，包括草稿和提交，';
/*!40101 SET character_set_client = @saved_cs_client */;

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
) ENGINE=InnoDB AUTO_INCREMENT=163 DEFAULT CHARSET=utf8 COMMENT='使用日志';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `videoreport--deprecated`
--

DROP TABLE IF EXISTS `videoreport--deprecated`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `videoreport--deprecated` (
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
  KEY `reportlogunitid_idx` (`unitid`),
  CONSTRAINT `videoreportunitid` FOREIGN KEY (`unitid`) REFERENCES `unit` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

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

-- Dump completed on 2017-11-10  9:44:02
