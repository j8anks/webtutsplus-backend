-- MySQL dump 10.13  Distrib 8.0.25, for Win64 (x86_64)
--
-- Host: localhost    Database: coresella
-- ------------------------------------------------------
-- Server version	8.0.25

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `cart`
--

DROP TABLE IF EXISTS `cart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cart` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `EntityId` varchar(36) DEFAULT NULL,
  `created_date` timestamp NULL DEFAULT NULL,
  `product_id` int DEFAULT NULL,
  `quantity` int NOT NULL,
  `user_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `UK_cart` (`EntityId`,`user_id`,`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=72 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cart`
--

LOCK TABLES `cart` WRITE;
/*!40000 ALTER TABLE `cart` DISABLE KEYS */;
INSERT INTO `cart` VALUES (71,'123456','2022-09-03 20:02:30',1000,1,1016);
/*!40000 ALTER TABLE `cart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `EntityId` varchar(255) DEFAULT NULL,
  `category_name` varchar(255) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `image_url` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1007 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1000,'123457','test','test','/test.jpg'),(1001,'123456','Test4','Descrip4','/descript4.jpg'),(1003,'123456','Category1','That Catetogory','/image.png'),(1004,'123456','fasd','asdf','asf'),(1005,'123456','Lefty','Fieldy123','/image13.png'),(1006,'123456','fsf','fasf','asf');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `entity`
--

DROP TABLE IF EXISTS `entity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `entity` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UniqueId` varchar(255) DEFAULT NULL,
  `Name` varchar(400) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Url` varchar(400) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Hosts` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CompanyName` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CompanyAddress` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CompanyPhoneNumber` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CompanyVat` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `SslEnabled` tinyint(1) NOT NULL,
  `DefaultLanguageId` int NOT NULL,
  `DisplayOrder` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `entity`
--

LOCK TABLES `entity` WRITE;
/*!40000 ALTER TABLE `entity` DISABLE KEYS */;
/*!40000 ALTER TABLE `entity` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderitems`
--

DROP TABLE IF EXISTS `orderitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderitems` (
  `order_item_id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `created_date` timestamp NULL DEFAULT NULL,
  `order_id` int DEFAULT NULL,
  `price` double DEFAULT NULL,
  `product_id` int DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  PRIMARY KEY (`order_item_id`),
  UNIQUE KEY `order_item_id` (`order_item_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderitems`
--

LOCK TABLES `orderitems` WRITE;
/*!40000 ALTER TABLE `orderitems` DISABLE KEYS */;
/*!40000 ALTER TABLE `orderitems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `EntityId` varchar(36) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `imageurl` varchar(255) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `price` double NOT NULL,
  `category_id` bigint NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1028 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (1000,'123456','test test test','https://localhost:7188/img/home.af78af94.png','Test101',800,1003),(1001,'123456','Red Paint','https://localhost:7188/img/icon.82e6fabe.png','Red-1010',14.5,1000),(1002,'123456','Blue Paint','https://localhost:7188/img/sorry.80cb9532.jpg','Blu-1011',14.5,1000),(1012,'123456','Test3 Description','/test3.jpg','Test3',0,1000),(1014,'123456','sdf','sf','fsadf',12,1000),(1015,'123456','fsadf','sadf','fsdf',13,1000),(1016,'123456','adf','adf','fsdaf',0,1000),(1017,'123456','string','1','newProduct',1500,1000),(1018,'123456','string','1','newProduct',1500,1000),(1019,'123456','string','string','Product1',14.5,1000),(1020,'123456','descrip','string','Product2',14,1000),(1021,'123456','string','string','string',0,0),(1022,'123456','string','string','string',0,0),(1023,'123456','string','string','string',0,0),(1024,'123456','string','string','string',0,0),(1025,'123456','That new product','/image.png','TestProduct99',14.5,1003),(1026,'123456','those things','/img','100Things',14.5,1003),(1027,'123456','string','string','string',0,0);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tokens`
--

DROP TABLE IF EXISTS `tokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tokens` (
  `id` int NOT NULL AUTO_INCREMENT,
  `created_date` timestamp NULL DEFAULT NULL,
  `token` varchar(255) DEFAULT NULL,
  `user_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tokens`
--

LOCK TABLES `tokens` WRITE;
/*!40000 ALTER TABLE `tokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `tokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uploads`
--

DROP TABLE IF EXISTS `uploads`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `uploads` (
  `id` int NOT NULL AUTO_INCREMENT,
  `FileName` varchar(255) DEFAULT NULL,
  `FileSize` bigint DEFAULT NULL,
  `FileContent` mediumblob,
  `ImageUrl` varchar(255) DEFAULT NULL,
  `Created` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1004 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uploads`
--

LOCK TABLES `uploads` WRITE;
/*!40000 ALTER TABLE `uploads` DISABLE KEYS */;
INSERT INTO `uploads` VALUES (1001,'home.af78af94.png',NULL,NULL,'https://localhost:7188/img/home.af78af94.png',NULL,NULL),(1002,'icon.82e6fabe.png',NULL,NULL,'https://localhost:7188/img/icon.82e6fabe.png',NULL,NULL),(1003,'sorry.80cb9532.jpg',NULL,NULL,'https://localhost:7188/img/sorry.80cb9532.jpg',NULL,NULL);
/*!40000 ALTER TABLE `uploads` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `EntityId` varchar(64) DEFAULT NULL,
  `UserGUID` varchar(64) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Username` varchar(100) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `SaltKey` varchar(255) NOT NULL DEFAULT '0',
  `FirstName` varchar(255) DEFAULT NULL,
  `LastName` varchar(255) DEFAULT NULL,
  `AffiliateID` int NOT NULL DEFAULT '0',
  `VendorId` int DEFAULT NULL,
  `BillingAddressID` int NOT NULL DEFAULT '0',
  `ShippingAddressID` int NOT NULL DEFAULT '0',
  `LastPaymentMethodID` int NOT NULL DEFAULT '0',
  `LastAppliedCouponCode` varchar(100) DEFAULT NULL,
  `GiftCardCouponCodes` text,
  `CheckoutAttributes` text,
  `LanguageID` int NOT NULL DEFAULT '0',
  `CurrencyID` int NOT NULL DEFAULT '0',
  `TaxDisplayTypeID` int NOT NULL DEFAULT '1',
  `IsTaxExempt` tinyint NOT NULL DEFAULT '0',
  `Role` varchar(25) DEFAULT NULL,
  `IsAdmin` tinyint NOT NULL DEFAULT '0',
  `IsGuest` tinyint NOT NULL DEFAULT '0',
  `IsForumModerator` tinyint NOT NULL DEFAULT '0',
  `TotalForumPosts` int NOT NULL DEFAULT '0',
  `Signature` varchar(300) NOT NULL DEFAULT '',
  `AdminComment` varchar(4000) NOT NULL DEFAULT '',
  `Active` tinyint NOT NULL DEFAULT '1',
  `Deleted` tinyint NOT NULL DEFAULT '0',
  `RegistrationDate` datetime NOT NULL COMMENT 'UTC',
  `TimeZoneID` varchar(200) NOT NULL DEFAULT '0',
  `AvatarID` int NOT NULL DEFAULT '0',
  `DateOfBirth` datetime DEFAULT NULL,
  `Gender` char(20) DEFAULT NULL,
  `CurrentWeight` double DEFAULT NULL,
  `LogCustom` tinyint DEFAULT NULL,
  `DiarySharing` char(1) DEFAULT '1',
  `DiaryKey` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `UserGUID` (`UserGUID`),
  UNIQUE KEY `Email` (`Email`),
  UNIQUE KEY `Username` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=1017 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1016,'123456','E4i7XhHGMGq','admin@coresella.com','admin','AK9FvnuKlJ0bHazrRP77jKVcwpesDecxCb3F0HEadycMWDAKoxXOpWtfnQLejaFDtA==','BTjlESqbuTcqZBqee8B1','James','Admin',0,NULL,0,0,0,NULL,NULL,NULL,0,0,1,0,'admin',0,0,0,0,'','',1,0,'2022-08-27 17:59:06','0',0,NULL,NULL,NULL,NULL,'1',NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `wishlist`
--

DROP TABLE IF EXISTS `wishlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wishlist` (
  `id` int NOT NULL AUTO_INCREMENT,
  `EntityId` varchar(36) DEFAULT NULL,
  `created_date` timestamp NULL DEFAULT NULL,
  `product_id` bigint DEFAULT NULL,
  `user_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `UK_wishlist` (`product_id`,`user_id`,`EntityId`),
  KEY `FK_wishlist_product_id` (`product_id`),
  CONSTRAINT `FK_wishlist_product_id` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1020 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `wishlist`
--

LOCK TABLES `wishlist` WRITE;
/*!40000 ALTER TABLE `wishlist` DISABLE KEYS */;
INSERT INTO `wishlist` VALUES (1017,'123456','2022-09-03 01:52:08',1002,1016),(1019,'123456','2022-09-03 19:52:31',1000,1016);
/*!40000 ALTER TABLE `wishlist` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-09-04 13:59:12
