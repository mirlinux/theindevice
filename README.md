# Device App



## Overview
![1](https://user-images.githubusercontent.com/19258598/190535255-cbb8b5d7-b9fb-41d0-8843-86bdf1a11503.PNG)

## System Structure

![캡처](https://user-images.githubusercontent.com/19258598/190535375-ee671aa4-9c04-4ec7-a02c-50af661d008f.PNG)


### 장비 상태정보
장비의 상태 (CPU, Memory) 정보를 주기적으로 Database에 저장.<br>
클라이언트의 TCP/IP Connection으로 상태정보를 전송

![캡처2](https://user-images.githubusercontent.com/19258598/191162322-df0fd281-b736-4e32-a4b6-533d2fa64ecb.PNG)

## 실행 동영상
https://user-images.githubusercontent.com/19258598/191189016-f9d51c6b-a609-4ece-9898-8130ca1da9fd.mp4

## Database Table Schema
### Member Table
```
CREATE TABLE `thein`.`member` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `userid` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NULL,
  `name` VARCHAR(45) NULL,
  `ip` VARCHAR(45) NULL,
  PRIMARY KEY (`id`));
```
### machine Table
```
CREATE TABLE `thein`.`machine` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NULL,
  `ip` VARCHAR(45) NULL,
  PRIMARY KEY (`id`));
```
### log Table
```
CREATE TABLE `thein`.`log` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `time` DATETIME NULL,
  `machine_id` INT NULL,
  `ip` VARCHAR(45) NULL,
  `cpu` DOUBLE NULL,
  `mem` INT NULL,
  `mem_usage` DOUBLE NULL,
  PRIMARY KEY (`id`));
```
