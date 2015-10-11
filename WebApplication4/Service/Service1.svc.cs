using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Runtime.Caching;
using System.Net;
using System.Xml;
using System.Web.Script.Serialization;

namespace WebApplication4.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1
    {
        private static ObjectCache cache = MemoryCache.Default;
        private CacheItemPolicy policy = null;


        public Result[] Convert(string from, string to, string amount)
        {

            Dictionary<string, float> CurrencyDic = cache["CurrencyDic"] as Dictionary<string, float>;
            if (CurrencyDic == null || CurrencyDic.Count == 0)
            {
                /**
                 * @todo a check must be implemented to see if the webservice is active and returns valid xml.
                 */
                WebClient webClient = new WebClient();
                String url = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";
                String xml = webClient.DownloadString(url);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);

                XmlNodeList LastDateCurrencyNodes = xmlDoc.DocumentElement.ChildNodes[2].ChildNodes[0].ChildNodes;


                Dictionary<string, float> CurrencyDictionary = new Dictionary<string, float>();
                for (int i = 0; i < LastDateCurrencyNodes.Count; i++)
                {

                    CurrencyDictionary.Add((string)LastDateCurrencyNodes[i].Attributes["currency"].Value, float.Parse(LastDateCurrencyNodes[i].Attributes["rate"].Value));
                }
                CurrencyDictionary.Add("EUR", float.Parse("1.00"));
                policy = new CacheItemPolicy();
                DateTime today = DateTime.Now;
                DateTime expire = today.AddDays(1);
                policy.AbsoluteExpiration = expire;
                cache.Set("CurrencyDic", CurrencyDictionary, policy);

                /**
                * check if we are converting to EUR since the webservice only return rates for EUR. 
                */
                if (to == "EUR")
                {
                    if (CurrencyDictionary.ContainsKey(from))
                    {
                        float convertedAmount = float.Parse(amount) * CurrencyDictionary[from];
                        
                        Result[] res = new Result[]{
                            new Result(){
                                    convertedAmount = convertedAmount.ToString(),
                                    from = from,
                                    to = to,
                                    originalAmount = amount
                                }
                        };


                        return res;
                    }
                    else
                    {
                        Result[] res = new Result[]{
                            new Result(){
                                    convertedAmount = "00.00",
                                    from = from,
                                    to = to,
                                    originalAmount = amount,
                                    errorMsg = "Api does not support this operation"
                                }
                        };
                        return res;
                    }
                }
                else
                {
                    if (CurrencyDictionary.ContainsKey(from) && CurrencyDictionary.ContainsKey(to))
                    {
                        float convertedAmount = float.Parse(amount) * CurrencyDictionary[from] / CurrencyDictionary[to];
                        Result[] res = new Result[]{
                            new Result(){
                                    convertedAmount = convertedAmount.ToString(),
                                    from = from,
                                    to = to,
                                    originalAmount = amount
                                }
                            };


                        return res;

                    }
                    else
                    {
                        Result[] res = new Result[]{
                            new Result(){
                                    convertedAmount = "00.00",
                                    from = from,
                                    to = to,
                                    originalAmount = amount,
                                    errorMsg = "Api does not support this operation"
                                }
                        };
                        return res;
                    }
                }

            }
            else
            {

                /**
                * check if we are converting to EUR since the webservice only return rates for EUR. 
                */
                if (to == "EUR")
                {
                    if (CurrencyDic.ContainsKey(from))
                    {
                        float convertedAmount = float.Parse(amount) * CurrencyDic[from];
                        Result[] res = new Result[]{
                            new Result(){
                                convertedAmount = convertedAmount.ToString(),
                                from = from,
                                to = to,
                                originalAmount = amount
                            }
                        };


                        return res;
                    }
                    else
                    {
                        Result[] res = new Result[]{
                            new Result(){
                                    convertedAmount = "00.00",
                                    from = from,
                                    to = to,
                                    originalAmount = amount,
                                    errorMsg = "Api does not support this operation"
                                }
                        };
                        return res;
                    }
                }
                else
                {
                    if (CurrencyDic.ContainsKey(from) && CurrencyDic.ContainsKey(to))
                    {
                        float convertedAmount = float.Parse(amount) * CurrencyDic[from] / CurrencyDic[to];
                        Result[] res = new Result[]{
                            new Result(){
                                convertedAmount = convertedAmount.ToString(),
                                from = from,
                                to = to,
                                originalAmount = amount
                            }
                        };


                        return res;
                    }
                    else
                    {
                        Result[] res = new Result[]{
                            new Result(){
                                    convertedAmount = "00.00",
                                    from = from,
                                    to = to,
                                    originalAmount = amount,
                                    errorMsg = "Api does not support this operation"
                                }
                        };
                        return res;
                    }
                }

            }

        }

        public UsdExRate[] ConverttoUSD(string from, string amount)
        {
            /**
            * this section can be moved to constructor since its a shared.
            */
            Dictionary<string, float> CurrencyDic = cache["CurrencyDic"] as Dictionary<string, float>;
            if (CurrencyDic == null || CurrencyDic.Count == 0)
            {
                WebClient webClient = new WebClient();
                String url = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";
                String xml = webClient.DownloadString(url);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);

                XmlNodeList LastDateCurrencyNodes = xmlDoc.DocumentElement.ChildNodes[2].ChildNodes[0].ChildNodes;


                Dictionary<string, float> CurrencyDictionary = new Dictionary<string, float>();
                for (int i = 0; i < LastDateCurrencyNodes.Count; i++)
                {

                    CurrencyDictionary.Add((string)LastDateCurrencyNodes[i].Attributes["currency"].Value, float.Parse(LastDateCurrencyNodes[i].Attributes["rate"].Value));
                }
                CurrencyDictionary.Add("EUR", float.Parse("1.00"));
                policy = new CacheItemPolicy();
                DateTime today = DateTime.Now;
                DateTime expire = today.AddDays(1);
                policy.AbsoluteExpiration = expire;
                cache.Set("CurrencyDic", CurrencyDictionary, policy);

               
                if (CurrencyDictionary.ContainsKey(from) && CurrencyDictionary.ContainsKey("USD"))
                {
                    float convertedAmount = float.Parse(amount) * CurrencyDictionary[from] / CurrencyDictionary["USD"];
                    float exchangeRate = 1 / convertedAmount;
                    UsdExRate[] res = new UsdExRate[]{
                        new UsdExRate(){
                            exRate = exchangeRate.ToString()
                        }
                    };


                    return res;

                }
                else
                {
                    UsdExRate[] res = new UsdExRate[]{
                        new UsdExRate(){
                            exRate = "0.00000",
                            errorMsg = "Api does not support this operation"
                        }
                    };
                    return res;
                }


            }
            else
            {

                if (CurrencyDic.ContainsKey(from) && CurrencyDic.ContainsKey("USD"))
                {
                    float convertedAmount = float.Parse(amount) * CurrencyDic[from] / CurrencyDic["USD"];
                    float exchangeRate = 1 / convertedAmount;
                    UsdExRate[] res = new UsdExRate[]{
                        new UsdExRate(){
                            exRate = exchangeRate.ToString()
                        }
                    };


                    return res;
                }
                else
                {
                    UsdExRate[] res = new UsdExRate[]{
                        new UsdExRate(){
                            exRate = "0.00000",
                            errorMsg = "Api does not support this operation"
                        }
                    };
                    return res;
                }
            }



        }
    }
    public class Result
    {
        public string from { get; set; }
        public string to { get; set; }
        public string originalAmount { get; set; }
        public string convertedAmount { get; set; }
        public string errorMsg { get; set; }
    }

    public class UsdExRate
    {
        public string exRate { get; set; }
        public string errorMsg { get; set; }
    }
}
