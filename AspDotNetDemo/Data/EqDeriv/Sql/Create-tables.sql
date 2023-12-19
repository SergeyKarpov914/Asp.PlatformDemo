SET DATEFORMAT mdy
GO
use "EqDeriv"
go

if exists (select * from sysobjects where id = object_id('dbo.Account') and sysstat & 0xf = 3)
	drop table "dbo"."Account"
GO
if exists (select * from sysobjects where id = object_id('dbo.OpenPosition') and sysstat & 0xf = 3)
	drop table "dbo"."OpenPosition"
GO
if exists (select * from sysobjects where id = object_id('dbo.PositionRisk') and sysstat & 0xf = 3)
	drop table "dbo"."PositionRisk"
GO
if exists (select * from sysobjects where id = object_id('dbo.TradeBlotter') and sysstat & 0xf = 3)
	drop table "dbo"."TradeBlotter"
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PositionRisk](
	[EODDATE] [int] NOT NULL,
	[UNDERLYING] [varchar](49) NOT NULL,
	[DOLLARDELTA] [float] NULL,
	[DOLLARGAMMA] [float] NULL,
	[DOLLARVEGA] [float] NULL,
	[DOLLARTHETA] [float] NULL,
	[DOLLARRHO] [float] NULL,
	[SHAREDELTA] [float] NULL,
	[SHAREGAMMA] [float] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Account](
	[MASTERCODE] [varchar](59) NULL,
	[DESICODE] [varchar](29) NULL,
	[ACCOUNTNAME] [nchar](56) NULL,
	[STATUS] [nchar](1) NULL,
	[LASTUPDATED] [datetime] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TradeBlotter](
	[TRADEDATE] [int] NULL,
	[CLIENTID] [varchar](10) NULL,
	[CLIENT] [varchar](199) NULL,
	[BUYSELL] [varchar](18) NULL,
	[QUANTITY] [Float] NULL,
	[UNDERLYING] [varchar](46) NULL,
	[EXPIRATION] [int] NULL,
	[STRIKE] [Float] NULL,
	[PUTCALL] [varchar](49) NULL,
	[PRICE] [float] NULL,
	[OTC] [bit] NULL DEFAULT ((0))
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OpenPosition](
	[MASTERCODE] [varchar](26) NULL,
	[TRADEDATE] [int] NOT NULL,
	[SYMBOL] [varchar](49) NULL,
	[QUANTITY] [int] NULL,
	[EXPIRATION] [int] NULL,
	[STRIKE] [Float] NULL,
	[PUTCALL] [varchar](5) NULL,
	[PRICE] [float] NULL,
	[CURRENTPRICE] [float] NULL,
	[DELTA] [float] NULL,
	[LASTUPDATED] [datetime] NULL,
	[CREATEDATE] [int] NOT NULL DEFAULT ((6)),
	[LASTTRADEDATE] [int] NOT NULL DEFAULT ((6)),
	[LASTPOSITIONEFFECT] [char](1) NULL,
	[CONSISTENCY] [char](1) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

