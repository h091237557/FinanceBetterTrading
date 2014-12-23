Feature: StockPriceServiceTests
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: 依照股票Code，來尋找出該支股票
	Given 查詢條件股票代號為 0001
	And 預計stockprice資料表應有
| Name | Code | Date      | OpenPrice | ClosePrice | HeightPrice | LowerPrice | Volumn | TradeAmount | TradeShare |
| Test | 0001 | 103/10/01 | 26.50     | 26.50      | 26.50       | 26.50      | 100    | 100         | 100        |
| Test | 0001 | 103/10/02 | 26.50     | 26.50      | 26.50       | 26.50      | 100    | 100         | 100        |
| Test | 0001 | 103/10/03 | 26.50     | 26.50      | 26.50       | 26.50      | 100    | 100         | 100        |
| Test | 0001 | 103/10/04 | 26.50     | 26.50      | 26.50       | 26.50      | 100    | 100         | 100        | 
	When 呼叫查詢結果
	Then 查詢結果應為
| Name | Code | Date      | OpenPrice | ClosePrice | HeightPrice | LowerPrice | Volumn | TradeAmount | TradeShare |
| Test | 0001 | 103/10/01 | 26.50     | 26.50      | 26.50       | 26.50      | 100    | 100         | 100        |
| Test | 0001 | 103/10/02 | 26.50     | 26.50      | 26.50       | 26.50      | 100    | 100         | 100        |
| Test | 0001 | 103/10/03 | 26.50     | 26.50      | 26.50       | 26.50      | 100    | 100         | 100        |
| Test | 0001 | 103/10/04 | 26.50     | 26.50      | 26.50       | 26.50      | 100    | 100         | 100        |
