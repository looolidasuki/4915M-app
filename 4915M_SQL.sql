-- =============================================================================
-- 服装企业级 ERP 与供应链管理系统 - 完整 DDL 脚本
-- 全局规则：
-- 1. 核心交易表只做流水存储，严禁另建统计表。Search/Sort/Filter 靠 Index，展示靠 VIEW。
-- 2. 所有带 ID 结尾的字段均为自增主键（Auto Increment）。
-- 3. 业务 Code 字段在应用层需遵循固定前缀模式（如 'SO-'+salesOrderID）。
-- =============================================================================

CREATE DATABASE IF NOT EXISTS `furniture_erp_system` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `furniture_erp_system`;

-- ==========================================
-- 一、 基础字典、员工与客户模块
-- ==========================================

-- 1. 货币表
CREATE TABLE `Currency` (
    `currencyID` BIGINT NOT NULL AUTO_INCREMENT COMMENT '自增唯一标识',
    `currencyCode` VARCHAR(30) NOT NULL UNIQUE COMMENT '货币代码，如USD, HKD',
    `currencySymbol` VARCHAR(5) NOT NULL COMMENT '货币符号',
    `rateToBase` DECIMAL(10, 2) NOT NULL COMMENT '当前币种对基准货币的汇率',
    PRIMARY KEY (`currencyID`)
) ENGINE=InnoDB COMMENT='货币汇率基础表';

-- 2. 员工/系统用户表
CREATE TABLE `Staff` (
    `staffID` BIGINT NOT NULL AUTO_INCREMENT COMMENT '自增唯一标识',
    `username` VARCHAR(30) NOT NULL UNIQUE COMMENT '登录用户名',
    `password` VARCHAR(255) NOT NULL COMMENT '加密密码',
    `title` VARCHAR(30) NOT NULL COMMENT '职位职称',
    `department` VARCHAR(30) NOT NULL COMMENT '所属部门',
    `firstName` VARCHAR(30) NOT NULL,
    `lastName` VARCHAR(30) NOT NULL,
    `employDate` DATE NOT NULL COMMENT '入职日期',
    `phone` VARCHAR(30) NOT NULL,
    `email` VARCHAR(255) NOT NULL,
    `status` INT(10) NULL COMMENT '员工状态',
    PRIMARY KEY (`staffID`)
) ENGINE=InnoDB COMMENT='员工与用户主表';

-- 3. 客户表
CREATE TABLE `Customer` (
    `customerID` BIGINT NOT NULL AUTO_INCREMENT COMMENT '自增唯一标识',
    `customerName` VARCHAR(255) NULL COMMENT '客户名称',
    `billingAddress` VARCHAR(255) NULL COMMENT '账单地址',
    `paymentTerm` VARCHAR(100) NULL COMMENT '付款条款',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
    `lastModifyDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '最后修改时间',
    PRIMARY KEY (`customerID`)
) ENGINE=InnoDB COMMENT='客户主表';

-- 4. 客户联系人表
CREATE TABLE `ContactPerson` (
    `contactPersonID` BIGINT NOT NULL AUTO_INCREMENT COMMENT '自增唯一标识',
    `customerID` BIGINT NOT NULL COMMENT '关联客户',
    `contactPerson` VARCHAR(100) NULL COMMENT '联系人姓名',
    `title` VARCHAR(30) NULL COMMENT '称谓/职位',
    `phone` VARCHAR(30) NULL COMMENT '电话',
    `email` VARCHAR(255) NULL COMMENT '邮箱',
    PRIMARY KEY (`contactPersonID`),
    CONSTRAINT `fk_contact_customer` FOREIGN KEY (`customerID`) REFERENCES `Customer` (`customerID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='客户联系人明细表';

-- 5. 客户收货地址表
CREATE TABLE `CustomerDeliveryAddress` (
    `addressID` BIGINT NOT NULL AUTO_INCREMENT COMMENT '自增唯一标识',
    `customerID` BIGINT NOT NULL COMMENT '关联客户',
    `deliveryAddress` VARCHAR(255) NULL COMMENT '收货寄送地址',
    `contactPerson` VARCHAR(100) NULL COMMENT '收货联系人',
    `phone` VARCHAR(30) NULL COMMENT '收货电话',
    `email` VARCHAR(255) NULL,
    PRIMARY KEY (`addressID`),
    CONSTRAINT `fk_address_customer` FOREIGN KEY (`customerID`) REFERENCES `Customer` (`customerID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='客户收货地址表';

-- ==========================================
-- 二、 系统字典与退款管理模块
-- ==========================================

-- 1. 系统数据字典表
CREATE TABLE `SystemDictionary` (
    `dictionaryID` BIGINT NOT NULL AUTO_INCREMENT COMMENT '自增唯一标识',
    `category` VARCHAR(60) NOT NULL COMMENT '字典类别，例如 refundMethod, refundReason',
    `codeValue` TINYINT NOT NULL COMMENT '状态码值，如 1, 2, 3',
    `displayNameEnglish` VARCHAR(50) NOT NULL COMMENT '英文显示名',
    `sortOrder` INT(10) NOT NULL DEFAULT 0 COMMENT '排序权值',
    PRIMARY KEY (`dictionaryID`),
    UNIQUE KEY `uk_category_value` (`category`, `codeValue`)
) ENGINE=InnoDB COMMENT='数据字典配置表';

-- 2. 退款申请表
CREATE TABLE `RefundRequest` (
    `refundRequestID` BIGINT NOT NULL AUTO_INCREMENT COMMENT '自增唯一标识',
    `refundRequestCode` VARCHAR(30) NOT NULL UNIQUE COMMENT '固定模式 RF-+ID',
    `staffID` BIGINT NOT NULL COMMENT '经办员工',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `ReceiptVoucherID` BIGINT NULL COMMENT '关联收款凭证（可选）',
    `InvoiceID` BIGINT NULL COMMENT '关联发票（可选）',
    `refundAmount` DECIMAL(19, 2) NOT NULL COMMENT '退款金额',
    `refundMethod` TINYINT NOT NULL COMMENT '退款方式（固定选择，由字典表控制，1:bank transfer, 2:FPS, 3:cheque等）',
    `refundRef` VARCHAR(100) NULL COMMENT '员工输入的支付网关交易参考号',
    `refundReason` VARCHAR(100) NOT NULL COMMENT '退款原因（固定选择，如 damage, wrong shipment等）',
    `status` INT(10) NOT NULL COMMENT '单据状态',
    `lastModifyDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `remark` VARCHAR(255) NULL,
    PRIMARY KEY (`refundRequestID`),
    CONSTRAINT `fk_refund_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='退款申请流水表';

-- 3. 系统字典_退款申请中间桥表
CREATE TABLE `SystemDictionary_RefundRequest` (
    `SystemDictionarydictionaryID` BIGINT NOT NULL,
    `SystemDictionarycategory` VARCHAR(60) NOT NULL,
    `SystemDictionarycodeValue` TINYINT NOT NULL,
    `RefundRequestrefundRequestID` BIGINT NOT NULL,
    PRIMARY KEY (`SystemDictionarydictionaryID`, `RefundRequestrefundRequestID`),
    CONSTRAINT `fk_bridge_dict` FOREIGN KEY (`SystemDictionarydictionaryID`) REFERENCES `SystemDictionary` (`dictionaryID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_bridge_refund` FOREIGN KEY (`RefundRequestrefundRequestID`) REFERENCES `RefundRequest` (`refundRequestID`) ON UPDATE CASCADE ON DELETE CASCADE
) ENGINE=InnoDB COMMENT='字典与退款申请桥接映射表';

-- ==========================================
-- 三、 商品、多仓储与生产管理模块
-- ==========================================

-- 1. 仓库表
CREATE TABLE `Warehouse` (
    `warehouseID` BIGINT NOT NULL AUTO_INCREMENT COMMENT '自增唯一标识',
    `warehouseName` VARCHAR(30) NOT NULL COMMENT '仓库名称',
    `warehouseAddress` VARCHAR(255) NOT NULL COMMENT '仓库地址',
    PRIMARY KEY (`warehouseID`)
) ENGINE=InnoDB COMMENT='多仓储区域定义表';

-- 2. 成品商品表
CREATE TABLE `Product` (
    `productID` BIGINT NOT NULL AUTO_INCREMENT COMMENT '自增唯一标识',
    `productCode` VARCHAR(30) NOT NULL UNIQUE COMMENT '模式 P-+category+sequenceNumber',
    `category` VARCHAR(30) NOT NULL COMMENT '类别（如上衣、裤子）',
    `sequenceNumber` INT(10) NULL COMMENT '序列编号',
    `styleNumber` VARCHAR(30) NOT NULL COMMENT '衣服款号',
    `size` VARCHAR(30) NOT NULL COMMENT '尺码',
    `color` VARCHAR(30) NOT NULL COMMENT '颜色',
    `basePriceByCurrency` DECIMAL(10, 2) NOT NULL COMMENT '基本售价',
    `currencyID` BIGINT NOT NULL COMMENT '定价币种',
    `staffID` BIGINT NOT NULL COMMENT '录入员工',
    `unit` VARCHAR(30) NOT NULL COMMENT '计量单位',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `lastModifyDate` TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `status` INT(10) NOT NULL COMMENT '商品状态',
    `remark` VARCHAR(255) NULL,
    PRIMARY KEY (`productID`),
    CONSTRAINT `fk_product_currency` FOREIGN KEY (`currencyID`) REFERENCES `Currency` (`currencyID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_product_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='成品服饰SKU信息表';

-- 3. 商品图片表
CREATE TABLE `ProductImage` (
    `productID` BIGINT NOT NULL COMMENT '一对一或多对一关联Product',
    `productImageUrl` VARCHAR(255) NULL COMMENT '图片托管URL地址',
    PRIMARY KEY (`productID`),
    CONSTRAINT `fk_img_product` FOREIGN KEY (`productID`) REFERENCES `Product` (`productID`) ON UPDATE CASCADE ON DELETE CASCADE
) ENGINE=InnoDB COMMENT='商品图片附表';

-- 4. 仓库成品库存表
CREATE TABLE `WarehouseProduct` (
    `warehouseID` BIGINT NOT NULL,
    `productID` BIGINT NOT NULL,
    `physicalQuantity` DECIMAL(10, 2) NOT NULL DEFAULT 0.00 COMMENT '实物库存',
    `reservedQuantity` DECIMAL(10, 2) NOT NULL DEFAULT 0.00 COMMENT '被销售单预留锁定的库存',
    `purchasedQuantity` DECIMAL(10, 2) NOT NULL DEFAULT 0.00 COMMENT '已下单采购但尚未入库的数量',
    PRIMARY KEY (`warehouseID`, `productID`),
    CONSTRAINT `fk_wp_warehouse` FOREIGN KEY (`warehouseID`) REFERENCES `Warehouse` (`warehouseID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_wp_product` FOREIGN KEY (`productID`) REFERENCES `Product` (`productID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='仓库成品物理与逻辑库存对账表';

-- ==========================================
-- 四、 销售、报价、发货与收款流水模块
-- ==========================================

-- 1. 报价单表
CREATE TABLE `Quotation` (
    `quotationID` BIGINT NOT NULL AUTO_INCREMENT,
    `quotationCode` VARCHAR(30) NOT NULL UNIQUE COMMENT '模式 QT-+ID',
    `sequenceNumber` INT(10) NOT NULL,
    `staffID` BIGINT NOT NULL COMMENT '经办销售员工',
    `customerID` BIGINT NOT NULL COMMENT '意向客户',
    `currencyID` BIGINT NOT NULL COMMENT '报价币种',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `lastModifyDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `status` INT(10) NOT NULL,
    `remark` VARCHAR(255) NULL,
    PRIMARY KEY (`quotationID`),
    CONSTRAINT `fk_quote_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_quote_customer` FOREIGN KEY (`customerID`) REFERENCES `Customer` (`customerID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_quote_currency` FOREIGN KEY (`currencyID`) REFERENCES `Currency` (`currencyID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='前期销售报价单';

-- 2. 报价单商品明细
CREATE TABLE `QuotationProductLine` (
    `quotationID` BIGINT NOT NULL,
    `productID` BIGINT NOT NULL,
    `price` DECIMAL(10, 2) NOT NULL COMMENT '报价单价',
    `quantity` DECIMAL(10, 2) NOT NULL COMMENT '意向数量',
    `discountAmount` DECIMAL(10, 2) NOT NULL DEFAULT 0.00 COMMENT '折让金额',
    PRIMARY KEY (`quotationID`, `productID`),
    CONSTRAINT `fk_qline_quote` FOREIGN KEY (`quotationID`) REFERENCES `Quotation` (`quotationID`) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `fk_qline_product` FOREIGN KEY (`productID`) REFERENCES `Product` (`productID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='报价单成品明细';

-- 3. 销售订单表
CREATE TABLE `SalesOrder` (
    `salesOrderID` BIGINT NOT NULL AUTO_INCREMENT,
    `salesOrderCode` VARCHAR(30) NOT NULL UNIQUE COMMENT '模式 SO-+ID',
    `customerID` BIGINT NOT NULL,
    `staffID` BIGINT NOT NULL,
    `currencyCurrencyID` BIGINT NOT NULL COMMENT '交易币种',
    `deliveryAddress` VARCHAR(255) NOT NULL COMMENT '原图标记为date属笔误，修正为字符串存放发货地址',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `lastModifyDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `discountType` VARCHAR(30) NULL COMMENT '折扣类型分类',
    `discount` DECIMAL(10, 2) NOT NULL DEFAULT 0.00 COMMENT '总单减免折扣',
    `status` INT(10) NOT NULL COMMENT '状态机控制：草稿、已锁定、生产中、发货完成等',
    `remark` VARCHAR(255) NULL,
    PRIMARY KEY (`salesOrderID`),
    CONSTRAINT `fk_so_customer` FOREIGN KEY (`customerID`) REFERENCES `Customer` (`customerID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_so_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_so_currency` FOREIGN KEY (`currencyCurrencyID`) REFERENCES `Currency` (`currencyID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='核心销售订单表';

-- 4. 销售订单商品明细表
CREATE TABLE `SalesOrderProductLine` (
    `salesOrderID` BIGINT NOT NULL,
    `productID` BIGINT NOT NULL,
    `price` DECIMAL(10, 2) NOT NULL COMMENT '实际销售单价',
    `orderQuantity` DECIMAL(10, 2) NOT NULL COMMENT '定购总数量',
    `discountAmount` DECIMAL(10, 2) NOT NULL DEFAULT 0.00 COMMENT '单品折让',
    `warehouseReservedQty` INT(10) NOT NULL DEFAULT 0 COMMENT '已从实体仓库占用的预留配额',
    `shippedQuantity` INT(10) NOT NULL DEFAULT 0 COMMENT '已发货交付累计数',
    `invoicedQuantity` INT(10) NOT NULL DEFAULT 0 COMMENT '已开具发票累计数',
    PRIMARY KEY (`salesOrderID`, `productID`),
    CONSTRAINT `fk_soline_so` FOREIGN KEY (`salesOrderID`) REFERENCES `SalesOrder` (`salesOrderID`) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `fk_soline_product` FOREIGN KEY (`productID`) REFERENCES `Product` (`productID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='销售订单商品货品细项';

-- 5. 生产订单表
CREATE TABLE `ProductionOrder` (
    `productionOrderID` BIGINT NOT NULL AUTO_INCREMENT,
    `productionOrderCode` VARCHAR(30) NOT NULL UNIQUE COMMENT '模式 PO-+ID',
    `salesOrderID` BIGINT NOT NULL COMMENT '派生出此工单的销售单',
    `staffID` BIGINT NOT NULL COMMENT '车间排产跟进员工',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `estFinishDate` TIMESTAMP NOT NULL COMMENT '预计完工交期',
    `lastModifyDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `status` INT(10) NOT NULL COMMENT '车间生产状态控制',
    `remark` VARCHAR(255) NULL,
    PRIMARY KEY (`productionOrderID`),
    CONSTRAINT `fk_po_so` FOREIGN KEY (`salesOrderID`) REFERENCES `SalesOrder` (`salesOrderID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_po_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='车间服装生产工单表';

-- 6. 生产单商品明细表
CREATE TABLE `ProductionOrderProductLine` (
    `ProductionOrderID` BIGINT NOT NULL,
    `productID` BIGINT NOT NULL,
    `productionQty` INT(10) NOT NULL COMMENT '计算逻辑：salesOrderProductLine.quantity - warehouseReservedQty',
    PRIMARY KEY (`ProductionOrderID`, `productID`),
    CONSTRAINT `fk_poline_po` FOREIGN KEY (`ProductionOrderID`) REFERENCES `ProductionOrder` (`productionOrderID`) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `fk_poline_product` FOREIGN KEY (`productID`) REFERENCES `Product` (`productID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='生产工单货品及数量明细';

-- 7. 销售出库/发货单表
CREATE TABLE `DeliveryNote` (
    `deliveryNoteID` BIGINT NOT NULL AUTO_INCREMENT,
    `deliveryNoteCode` VARCHAR(30) NOT NULL UNIQUE,
    `customerID` BIGINT NOT NULL,
    `SalesOrderID` BIGINT NOT NULL,
    `staffID` BIGINT NOT NULL,
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `lastModifyDate` TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `WarehouseID` BIGINT NOT NULL COMMENT '从哪一个出货物理仓出库',
    `shipMethod` VARCHAR(30) NOT NULL COMMENT '发货运输方式',
    `trackingNumber` VARCHAR(30) NOT NULL COMMENT '快递/物流追踪单号',
    `remark` VARCHAR(255) NULL,
    `status` INT(10) NOT NULL,
    PRIMARY KEY (`deliveryNoteID`),
    CONSTRAINT `fk_dn_customer` FOREIGN KEY (`customerID`) REFERENCES `Customer` (`customerID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_dn_so` FOREIGN KEY (`SalesOrderID`) REFERENCES `SalesOrder` (`salesOrderID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_dn_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_dn_warehouse` FOREIGN KEY (`WarehouseID`) REFERENCES `Warehouse` (`warehouseID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='销售发货/出库流水单';

-- 8. 发货单商品明细
CREATE TABLE `DeliveryProductLine` (
    `deliveryNoteID` BIGINT NOT NULL,
    `productID` BIGINT NOT NULL,
    `shipQuantity` INT(10) NOT NULL COMMENT '本次包裹实际发货发出数量',
    PRIMARY KEY (`deliveryNoteID`, `productID`),
    CONSTRAINT `fk_dline_dn` FOREIGN KEY (`deliveryNoteID`) REFERENCES `DeliveryNote` (`deliveryNoteID`) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `fk_dline_product` FOREIGN KEY (`productID`) REFERENCES `Product` (`productID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='发货单货品打包封装明细';

-- 9. 发货发票表
CREATE TABLE `Invoice` (
    `invoiceID` BIGINT NOT NULL AUTO_INCREMENT,
    `invoiceCode` VARCHAR(30) NOT NULL UNIQUE,
    `customerID` BIGINT NOT NULL,
    `salesOrderID` BIGINT NOT NULL,
    `staffID` BIGINT NOT NULL,
    `invoiceType` INT(10) NOT NULL COMMENT '类型：deposit(定金发票), normal(出货正规发票)',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `lastModifyDate` TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    `remark` VARCHAR(255) NULL,
    `status` INT(10) NOT NULL COMMENT '开票对账状态',
    PRIMARY KEY (`invoiceID`),
    CONSTRAINT `fk_inv_customer` FOREIGN KEY (`customerID`) REFERENCES `Customer` (`customerID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_inv_so` FOREIGN KEY (`salesOrderID`) REFERENCES `SalesOrder` (`salesOrderID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_inv_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='应收发票对账主表';

-- 10. 发票明细表
CREATE TABLE `InvoiceLine` (
    `invoiceID` BIGINT NOT NULL,
    `deliveryNoteID` BIGINT NOT NULL COMMENT '多包裹分批出货时，开票需追溯对应的出库单',
    `productID` BIGINT NOT NULL COMMENT '特殊：如果是deposit定金开票类型，此ID可以写入一个虚拟符号并在明细存负项冲平',
    `invoiceQuantity` INT(10) NOT NULL COMMENT '本次开票计费数量',
    `amount` DECIMAL(12, 2) NOT NULL COMMENT '本次计费金额（包含负项冲减）',
    PRIMARY KEY (`invoiceID`, `deliveryNoteID`, `productID`),
    CONSTRAINT `fk_invline_inv` FOREIGN KEY (`invoiceID`) REFERENCES `Invoice` (`invoiceID`) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `fk_invline_product` FOREIGN KEY (`productID`) REFERENCES `Product` (`productID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='发票计费项目明细表（支持定金扣减逻辑）';

-- 11. 收款凭证表
CREATE TABLE `ReceiptVoucher` (
    `receiptVoucherID` BIGINT NOT NULL AUTO_INCREMENT,
    `receiptVoucherCode` VARCHAR(30) NOT NULL UNIQUE,
    `cusomerID` BIGINT NOT NULL COMMENT '原图拼写为 cusomerID 保持一致',
    `staffID` BIGINT NOT NULL,
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `lastModifyDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `paymentMethod` VARCHAR(30) NOT NULL COMMENT '付款通道',
    `paymentMethodRef` VARCHAR(30) NOT NULL COMMENT '支付流水参考凭证号',
    `paymentAmount` DECIMAL(10, 2) NOT NULL COMMENT '实收总金额',
    `currencyID` BIGINT NOT NULL COMMENT '实收币种',
    `paymentReceivedDate` DATE NOT NULL COMMENT '实际到账日期',
    `status` INT(10) NOT NULL,
    `remark` VARCHAR(255) NULL,
    PRIMARY KEY (`receiptVoucherID`),
    CONSTRAINT `fk_rv_customer` FOREIGN KEY (`cusomerID`) REFERENCES `Customer` (`customerID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_rv_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_rv_currency` FOREIGN KEY (`currencyID`) REFERENCES `Currency` (`currencyID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='客户财务收款进账流水单';

-- 12. 收款凭证发票核销明细表（多对多桥表）
CREATE TABLE `ReceiptVoucherInvoice` (
    `receiptVoucherID` BIGINT NOT NULL,
    `invoiceID` BIGINT NOT NULL,
    `receivedAmount` DECIMAL(10, 2) NOT NULL COMMENT '这笔收款核销拆分拨给该发票的额度。SUM(receivedAmount) 必须等于 receiptVoucher.paymentAmount',
    `type` INT(10) NOT NULL COMMENT '核销阶段类型：deposit, partial, final, exchangeLoss(汇损结转)',
    PRIMARY KEY (`receiptVoucherID`, `invoiceID`),
    CONSTRAINT `fk_rvi_rv` FOREIGN KEY (`receiptVoucherID`) REFERENCES `ReceiptVoucher` (`receiptVoucherID`) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `fk_rvi_inv` FOREIGN KEY (`invoiceID`) REFERENCES `Invoice` (`invoiceID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='财务应收实收关联核销表';

-- ==========================================
-- 五、 原材料与缺货需求追踪模块
-- ==========================================

-- 1. 原材料基础信息表
CREATE TABLE `RawMaterial` (
    `rawMaterialID` BIGINT NOT NULL AUTO_INCREMENT,
    `rawMaterialCode` VARCHAR(30) NOT NULL UNIQUE COMMENT '模式 RM-+category+sequenceNumber',
    `category` VARCHAR(30) NOT NULL COMMENT '原料种类（如面料、纽扣、拉链）',
    `SequenceNumber` INT(10) NULL,
    `size` VARCHAR(30) NOT NULL,
    `color` VARCHAR(30) NOT NULL,
    `minimumStockLevel` DECIMAL(10, 2) NOT NULL DEFAULT 0.00 COMMENT '物料安全库存红线',
    `status` INT(10) NOT NULL,
    PRIMARY KEY (`rawMaterialID`)
) ENGINE=InnoDB COMMENT='生产原材料SKU基础档案表';

-- 2. 产品材料配方表（BOM表）
CREATE TABLE `ProductRawMaterialLine` (
    `productID` BIGINT NOT NULL,
    `rawMaterialID` BIGINT NOT NULL,
    `rawMaterialNeedQty` DECIMAL(10, 2) NOT NULL COMMENT '标准工艺下单件成品所需此原料的数量消耗定额',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `lastModifyDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (`productID`, `rawMaterialID`),
    CONSTRAINT `fk_bom_product` FOREIGN KEY (`productID`) REFERENCES `Product` (`productID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_bom_raw` FOREIGN KEY (`rawMaterialID`) REFERENCES `RawMaterial` (`rawMaterialID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='商品衣服物料清单配方（BOM表）';

-- 3. 原材料仓库库存表
CREATE TABLE `RawMaterialWarehouse` (
    `rawMaterialID` BIGINT NOT NULL,
    `warehouseID` BIGINT NOT NULL,
    `physicalQuantity` DECIMAL(10, 2) NOT NULL DEFAULT 0.00 COMMENT '面料辅料实际物理库存',
    `reservedQuantity` DECIMAL(10, 2) NOT NULL DEFAULT 0.00 COMMENT '已被排产锁定消耗的原料数',
    `purchasedQuantity` DECIMAL(10, 2) NOT NULL DEFAULT 0.00 COMMENT '已下采购单等在途的原材料数',
    PRIMARY KEY (`rawMaterialID`, `warehouseID`),
    CONSTRAINT `fk_rmw_raw` FOREIGN KEY (`rawMaterialID`) REFERENCES `RawMaterial` (`rawMaterialID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_rmw_warehouse` FOREIGN KEY (`warehouseID`) REFERENCES `Warehouse` (`warehouseID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='原材料仓储动态库存对账表';

-- 4. 缺货报告主表
CREATE TABLE `ShortageReport` (
    `shortageReportID` BIGINT NOT NULL AUTO_INCREMENT,
    `shortageReportCode` VARCHAR(30) NOT NULL UNIQUE COMMENT '模式 SR-+date+sequenceNumber',
    `date` TIMESTAMP NOT NULL COMMENT '报告引发或计算生成的基准结算时间',
    `sequenceNumber` INT(10) NOT NULL,
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`shortageReportID`)
) ENGINE=InnoDB COMMENT='系统自动扫描或手动生成的原料缺货汇总报告';

-- 5. 原料缺货报告明细表
CREATE TABLE `RawMaterialShortageReportLine` (
    `shortageReportID` BIGINT NOT NULL,
    `rawMaterialID` BIGINT NOT NULL,
    `WarehousewarehouseID` BIGINT NOT NULL,
    `totalShortageQuantity` DECIMAL(10, 2) NOT NULL COMMENT '通过盘点自动轧出的真实缺货数量',
    PRIMARY KEY (`shortageReportID`, `rawMaterialID`, `WarehousewarehouseID`),
    CONSTRAINT `fk_srline_sr` FOREIGN KEY (`shortageReportID`) REFERENCES `ShortageReport` (`shortageReportID`) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `fk_srline_raw` FOREIGN KEY (`rawMaterialID`) REFERENCES `RawMaterial` (`rawMaterialID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_srline_wh` FOREIGN KEY (`WarehousewarehouseID`) REFERENCES `Warehouse` (`warehouseID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='物料缺货清单明细';

-- ==========================================
-- 六、 供应链：原材料采购、入库与付款模块
-- ==========================================

-- 1. 供应商主表
CREATE TABLE `Supplier` (
    `supplierID` BIGINT NOT NULL AUTO_INCREMENT,
    `supplierName` VARCHAR(255) NOT NULL,
    `billingAddress` VARCHAR(255) NULL,
    `contactPerson` VARCHAR(100) NULL,
    `phone` VARCHAR(30) NULL,
    `email` VARCHAR(255) NULL,
    `paymentTerm` VARCHAR(100) NULL COMMENT '与供应商约定的结算账期条件',
    `bankAccount` VARCHAR(100) NULL COMMENT '供应商收汇对公账户',
    `status` INT(10) NOT NULL,
    PRIMARY KEY (`supplierID`)
) ENGINE=InnoDB COMMENT='面料及配件上游供应商主体表';

-- 2. 供应商原料报价映射表
CREATE TABLE `RawMaterialSupplier` (
    `rawMaterialID` BIGINT NOT NULL,
    `supplierID` BIGINT NOT NULL,
    `supplierStyleNumber` VARCHAR(50) NULL COMMENT '供应商在自己厂内对应的物料款号',
    `basePrice` DECIMAL(10, 2) NOT NULL COMMENT '供货报价',
    `currencyID` BIGINT NOT NULL COMMENT '供货结算币种',
    `unit` VARCHAR(30) NOT NULL COMMENT '计量供应单位',
    `minimumOrderQuantity` INT(10) NOT NULL DEFAULT 1 COMMENT '最小起订量限制',
    `quoteDate` DATE NULL COMMENT '报价生效起始日',
    `lastModify` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `status` INT(10) NOT NULL,
    PRIMARY KEY (`rawMaterialID`, `supplierID`),
    CONSTRAINT `fk_rms_raw` FOREIGN KEY (`rawMaterialID`) REFERENCES `RawMaterial` (`rawMaterialID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_rms_sup` FOREIGN KEY (`supplierID`) REFERENCES `Supplier` (`supplierID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='供应商原材料价格与起订量名录';

-- 3. 原材料申领/请购单表
CREATE TABLE `RawMaterialRequestNote` (
    `rawMaterialRequestNoteID` BIGINT NOT NULL AUTO_INCREMENT,
    `rawMaterialRequestNoteCode` VARCHAR(30) NOT NULL UNIQUE,
    `ProductionOrderID` BIGINT NOT NULL COMMENT '车间关联的源头排产生产订单',
    `staffID` BIGINT NOT NULL COMMENT '申领发起员工',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `requestDate` DATE NOT NULL COMMENT '期望要求的到料领料日期',
    `remark` VARCHAR(255) NULL,
    PRIMARY KEY (`rawMaterialRequestNoteID`),
    CONSTRAINT `fk_rmreq_po` FOREIGN KEY (`ProductionOrderID`) REFERENCES `ProductionOrder` (`productionOrderID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_rmreq_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='车间向仓储/采购部提报的物料领料申领及请购单';

-- 4. 请购原料明细表
CREATE TABLE `RawMaterialRequestNoteRawMaterial_line` (
    `rawMaterialRequestNoteID` BIGINT NOT NULL,
    `productID` BIGINT NOT NULL COMMENT '要切片对应哪一个成品所需制造的配给',
    `rawMaterialID` BIGINT NOT NULL COMMENT '具体申领的原材料',
    `rawMaterialRequestQuantity` DECIMAL(10, 2) NOT NULL COMMENT '本次申请流转的数量',
    PRIMARY KEY (`rawMaterialRequestNoteID`, `productID`, `rawMaterialID`),
    CONSTRAINT `fk_rmreqline_note` FOREIGN KEY (`rawMaterialRequestNoteID`) REFERENCES `RawMaterialRequestNote` (`rawMaterialRequestNoteID`) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `fk_rmreqline_raw` FOREIGN KEY (`rawMaterialID`) REFERENCES `RawMaterial` (`rawMaterialID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='领料请购单精确物料行明细';

-- 5. 原材料采购订单表
CREATE TABLE `PurchaseOrder` (
    `purchaseOrderID` BIGINT NOT NULL AUTO_INCREMENT,
    `purchaseOrderCode` VARCHAR(30) NOT NULL UNIQUE COMMENT '模式 PO-+ID',
    `supplierID` BIGINT NOT NULL COMMENT '向哪家商户买料',
    `staffID` BIGINT NOT NULL COMMENT '采购员',
    `relatedShortageReport` BIGINT NULL COMMENT '可选追溯的系统缺货汇总单来源',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `lastModifyDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `requestDeliveryDate` DATE NOT NULL COMMENT '约束供货商到料交付的死线日期',
    `status` INT(10) NOT NULL,
    `remark` VARCHAR(255) NULL,
    PRIMARY KEY (`purchaseOrderID`),
    CONSTRAINT `fk_pur_supplier` FOREIGN KEY (`supplierID`) REFERENCES `Supplier` (`supplierID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_pur_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='上游供应链原材料采购订单';

-- 6. 采购订单物料明细表
CREATE TABLE `PurchaseOrderRawMaterialLine` (
    `purchaseOrderID` BIGINT NOT NULL,
    `rawMaterialID` BIGINT NOT NULL,
    `price` DECIMAL(10, 2) NOT NULL COMMENT '采购议定单价',
    `orderQuantity` DECIMAL(10, 2) NOT NULL COMMENT '采购面料配件总数',
    `receivedQuantity` DECIMAL(10, 2) NOT NULL DEFAULT 0.00 COMMENT '后期累计已完成收货清点的在途转实物入库数',
    PRIMARY KEY (`purchaseOrderID`, `rawMaterialID`),
    CONSTRAINT `fk_purline_pur` FOREIGN KEY (`purchaseOrderID`) REFERENCES `PurchaseOrder` (`purchaseOrderID`) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `fk_purline_raw` FOREIGN KEY (`rawMaterialID`) REFERENCES `RawMaterial` (`rawMaterialID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='供应链原材料采购订单明细行';

-- 7. 采购原料收货/入库单表
CREATE TABLE `GoodsReceivedNote` (
    `goodsReceivedNoteID` BIGINT NOT NULL AUTO_INCREMENT,
    `goodsReceivedNoteCode` VARCHAR(30) NOT NULL UNIQUE,
    `supplierID` BIGINT NOT NULL,
    `PurchaseOrderID` BIGINT NOT NULL COMMENT '关联的采购单源头',
    `staffID` BIGINT NOT NULL COMMENT '收货仓管验收员',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `lastModifyDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `status` INT(10) NOT NULL,
    `remark` VARCHAR(255) NULL,
    PRIMARY KEY (`goodsReceivedNoteID`),
    CONSTRAINT `fk_grn_supplier` FOREIGN KEY (`supplierID`) REFERENCES `Supplier` (`supplierID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_grn_pur` FOREIGN KEY (`PurchaseOrderID`) REFERENCES `PurchaseOrder` (`purchaseOrderID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_grn_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='原材料采购到货验收及入库单';

-- 8. 收货入库原料明细
CREATE TABLE `GoodsReceivedNoteRawMaterialLine` (
    `goodsReceivedNoteID` BIGINT NOT NULL,
    `rawMaterialID` BIGINT NOT NULL,
    `receivedQuantity` DECIMAL(10, 2) NOT NULL COMMENT '本次送达包裹清点合规后实际吃进库存的数量',
    PRIMARY KEY (`goodsReceivedNoteID`, `rawMaterialID`),
    CONSTRAINT `fk_grnline_grn` FOREIGN KEY (`goodsReceivedNoteID`) REFERENCES `GoodsReceivedNote` (`goodsReceivedNoteID`) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `fk_grnline_raw` FOREIGN KEY (`rawMaterialID`) REFERENCES `RawMaterial` (`rawMaterialID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='采购到货入库原料明细账目';

-- 9. 采购付款凭证表
CREATE TABLE `PaymentVoucher` (
    `paymentVoucherID` BIGINT NOT NULL AUTO_INCREMENT,
    `paymentVoucherCode` VARCHAR(30) NOT NULL UNIQUE,
    `supplierID` BIGINT NOT NULL,
    `staffID` BIGINT NOT NULL COMMENT '财务出纳审签经办人',
    `createDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `lastModifyDate` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `paymentMethod` VARCHAR(50) NOT NULL COMMENT '对公付汇渠道方式',
    `paymentMethodRef` VARCHAR(100) NOT NULL COMMENT '银行付款水单参考号',
    `totalAmount` DECIMAL(12, 2) NOT NULL COMMENT '本次实际支付给该供应商的总汇款数',
    `remark` VARCHAR(255) NULL,
    `status` INT(10) NOT NULL COMMENT '应付款对账核销状态',
    PRIMARY KEY (`paymentVoucherID`),
    CONSTRAINT `fk_pv_supplier` FOREIGN KEY (`supplierID`) REFERENCES `Supplier` (`supplierID`) ON UPDATE CASCADE ON DELETE RESTRICT,
    CONSTRAINT `fk_pv_staff` FOREIGN KEY (`staffID`) REFERENCES `Staff` (`staffID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='应付上游供应商采购货款财务出账单';

-- 10. 付款凭证采购单核销表（多对多桥表）
CREATE TABLE `PaymentVoucherPurchaseOrder` (
    `paymentVoucherID` BIGINT NOT NULL,
    `purchaseOrderID` BIGINT NOT NULL,
    `type` INT(10) NOT NULL COMMENT '应付对账阶段划分',
    `payAmount` DECIMAL(12, 2) NOT NULL COMMENT '本张水单里的款项中有多少额度被拿去核销了该张采购订单的欠款',
    PRIMARY KEY (`paymentVoucherID`, `purchaseOrderID`),
    CONSTRAINT `fk_pvpo_pv` FOREIGN KEY (`paymentVoucherID`) REFERENCES `PaymentVoucher` (`paymentVoucherID`) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT `fk_pvpo_po` FOREIGN KEY (`purchaseOrderID`) REFERENCES `PurchaseOrder` (`purchaseOrderID`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB COMMENT='财务应付账款实际冲账核销关联表';