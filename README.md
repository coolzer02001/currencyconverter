# currencyconverter

Discription:
=============================================
This is An attempt to implement a currencyconverter using the below requirements in ASP.NET C#.

You are to implement a Currency Exchange web application using your language of choice. Your application will have a decent user interface and will give users the ability to convert money units between any two currencies. Follows is a list of the minimum requirements:

1.       The backend should be implemented as a RESTful web service and other clients should be able to consume the following functionalities:

  a.       Exchange money between two currencies (example: 10 USD to EUR).
  
  b.      Return today’s exchange rate for a currency in respect to USD.

2.       The frontend must be totally separated from the backend and will be calling the backend functionalities through RESTful APIs.

3.       Your application will fetch Currency Rates data from the ECB Web Service (http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml).

4.       Your application will either store the currency rates data in a database or in memory using your in-memory data structure of choice.
==============================================
Service urls:
==============================================
a. Exchange money between two currencies 
  link : http://hostname:port/Service/Service1.svc/Convert/{from}/{to}/{amount}
  
  where 
  Convert : is the method name.
  from : the ISO code for the currency you want to exchange from. ex. USD 
  to  : the ISO code for the currency you want to exchange to. ex. EURO.
  amount : is the amount you want to exchange.

b.eturn today’s exchange rate for a currency in respect to USD.
  link : http://hostname:port/Service/Service1.svc/ConverttoUSD/{from}/{amount}
  
  where
  from : the ISO code for the currency you want to exchange from. ex. USD
  amount : is the amount you want to exchange.

Please Note that http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml has the values for exchange in rrespect to EURO only
so I had to do some math so values returned might not be 100% accurate.

This is only an expermental code. it me trying to use a new technology.

currencyconverter
