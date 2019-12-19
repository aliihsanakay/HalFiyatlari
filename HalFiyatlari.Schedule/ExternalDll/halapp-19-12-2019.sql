/*
 Navicat Premium Data Transfer

 Source Server         : Local
 Source Server Type    : MySQL
 Source Server Version : 50728
 Source Host           : localhost:3306
 Source Schema         : halapp

 Target Server Type    : MySQL
 Target Server Version : 50728
 File Encoding         : 65001

 Date: 19/12/2019 14:12:09
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for customer
-- ----------------------------
DROP TABLE IF EXISTS `customer`;
CREATE TABLE `customer`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CODE` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `NAME` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CREATEDATE` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of customer
-- ----------------------------
INSERT INTO `customer` VALUES (1, '34', 'İstanbul BB Hali', '2019-12-19 10:23:33');

-- ----------------------------
-- Table structure for product
-- ----------------------------
DROP TABLE IF EXISTS `product`;
CREATE TABLE `product`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CREATEDATE` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  `CATEGORY` varchar(70) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UNIT` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of product
-- ----------------------------
INSERT INTO `product` VALUES (1, 'Domates', '2019-12-19 10:22:11', 'Sebze', 'KG');
INSERT INTO `product` VALUES (2, 'Salatalık', '2019-12-19 10:34:58', 'Sebze', 'KG');

-- ----------------------------
-- Table structure for product_price
-- ----------------------------
DROP TABLE IF EXISTS `product_price`;
CREATE TABLE `product_price`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PRODUCTID` int(11) NULL DEFAULT -1,
  `CUSTOMERID` int(11) NULL DEFAULT -1,
  `MINPRICE` double(15, 2) NULL DEFAULT 0.00,
  `MAXPRICE` double(15, 2) NULL DEFAULT 0.00,
  `CREATEDATE` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0),
  `CURRENCY` varchar(5) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of product_price
-- ----------------------------
INSERT INTO `product_price` VALUES (1, 1, 1, 2.00, 3.00, '2019-12-19 10:22:43', 'TL');
INSERT INTO `product_price` VALUES (2, 2, 1, 2.00, 3.00, '2019-12-19 10:35:26', 'TL');

-- ----------------------------
-- Procedure structure for INSERT_PRODUCT_DATA
-- ----------------------------
DROP PROCEDURE IF EXISTS `INSERT_PRODUCT_DATA`;
delimiter ;;
CREATE PROCEDURE `INSERT_PRODUCT_DATA`(IN pProductName VARCHAR(255),
IN pProductCategory VARCHAR(255),
IN pUnit VARCHAR(100),
IN pMinPrice DOUBLE,
IN pMaxPrice DOUBLE,
IN pCurrency VARCHAR(10),
IN pCustomerId INT)
BEGIN

DECLARE vProductId INT DEFAULT( -1);
DECLARE vProductControl INT DEFAULT 0;
DECLARE vProductPriceControl INT DEFAULT 0;
SELECT COALESCE(ID,0) INTO vProductControl FROM product WHERE product.`NAME`  LIKE CONCAT('%',pProductName,'%') LIMIT 1;

IF vProductControl=0 THEN
INSERT INTO product (
product.`NAME`,
product.CATEGORY,
product.UNIT

)
VALUES (
pProductName,
pProductCategory,
pUnit
);

SELECT LAST_INSERT_ID() INTO vProductId;

ELSE
SET vProductId=vProductControl;
END IF;

SELECT COALESCE(ID,0) INTO vProductPriceControl FROM   product_price p 
where  DATE_FORMAT(p.CREATEDATE,'dd.MM.yyyy') =DATE_FORMAT(NOW(),'dd.MM.yyyy')
AND p.CUSTOMERID=pCustomerId AND p.PRODUCTID=vProductId LIMIT 1; 

IF vProductPriceControl=0 THEN 
INSERT INTO product_price (
product_price.PRODUCTID,
product_price.MINPRICE,
product_price.MAXPRICE,
product_price.CUSTOMERID,
product_price.CURRENCY
)
VALUES(
vProductId,
pMinPrice,
pMaxPrice,
pCustomerId,
pCurrency
);
END IF;

END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
