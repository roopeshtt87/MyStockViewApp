using System;
using System.IO;

namespace MyStockViewApp
{
    public static class SettingsManager
    {
        static SettingsManager()
        {
            File.Open(fileName, FileMode.OpenOrCreate).Close();
        }
        private static string fileName = "StockSettings.cfg";
        private static string _stockIds;
        private static FileStream OpenFile()
        {
            return File.Open(fileName, FileMode.OpenOrCreate);
        }

        private static bool ValidateFile()
        {
            FileStream fs = OpenFile();
            return true; // todo
        }

        public static string ReadFile()
        {
            //List<StockID> stockIds = new List<StockID>();
            string text;
            try
            {
                text = File.ReadAllText(fileName);
                _stockIds = text;
            }
            catch (Exception ex)
            {
                throw new Exception("SettingsManager :: ReadFile failed, Failed to read stock information from configuration file + '\n'" + ex.Message);
            }
            return text;
        }

        public static void WriteToFile(string subscription)
        {
            try
            {
                //if (stockIds.Count > 0)
                {
                    //_stockIds.RemoveAll(s => s.Exchange == stockIds[0].Exchange);
                    //string[] lines = new string[stockIds.Count + _stockIds.Count];
                    //int i = 0;
                    //foreach (var v in _stockIds)
                    //{
                    //    lines[i++] = v.Exchange + ":" + v.CompanyNameShort;
                    //}
                    //foreach (var v in stockIds)
                    //{
                    //    lines[i++] = v.Exchange + ":" + v.CompanyNameShort;
                    //}
                    File.WriteAllText(fileName, subscription);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SettingsManager :: WriteToFile failed, Failed to write stock information to configuration file + '\n'" + ex.Message);
            }
        }

        //public static List<StockID> ReadFile()
        //{
        //    List<StockID> stockIds = new List<StockID>();
        //    try
        //    {
        //        string[] lines = File.ReadAllLines(fileName);
        //        foreach (string s in lines)
        //        {
        //            string[] idinfo = s.Split(':');
        //            stockIds.Add(new StockID(idinfo[0], idinfo[1]));
        //        }
        //        _stockIds = stockIds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("SettingsManager :: ReadFile failed, Failed to read stock information from configuration file + '\n'" + ex.Message);
        //    }
        //    return stockIds; 
        //}

        //public static void WriteToFile(List<StockID> stockIds)
        //{
        //    try
        //    {
        //        if (stockIds.Count > 0)
        //        {
        //            //_stockIds.RemoveAll(s => s.Exchange == stockIds[0].Exchange);
        //            string[] lines = new string[stockIds.Count + _stockIds.Count];
        //            int i = 0;
        //            foreach (var v in _stockIds)
        //            {
        //                lines[i++] = v.Exchange + ":" + v.CompanyNameShort;
        //            }
        //            foreach (var v in stockIds)
        //            {
        //                lines[i++] = v.Exchange + ":" + v.CompanyNameShort;
        //            }
        //            File.WriteAllLines(fileName, lines);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("SettingsManager :: WriteToFile failed, Failed to write stock information to configuration file + '\n'" + ex.Message);
        //    }
        //}

        public static void WriteToFile(string e, string n)
        {
            try
            {
                if (!_stockIds.Contains(e + ":" + n)) // Exists(s => (s.Exchange + ":" + s.CompanyNameShort) == (e + ":" + n)))
                {
                    if (_stockIds.Contains(e + ":,"))
                    {
                        _stockIds.Replace(e + ":,", "");
                    }

                    _stockIds += (e + ":" + n + ","); //Add(new StockID(e, n));
                    WriteToFile(_stockIds);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SettingsManager :: WriteToFile failed, Failed to write stock information to configuration file + '\n'" + ex.Message);
            }
        }

        public static void DeleteFromFile(string e, string n)
        {
            try
            {
                if (_stockIds.Contains(e + ":" + n))
                {
                    _stockIds.Replace(e + ":" + n, "");//s => (s.Exchange + ":" + s.CompanyNameShort) == (e + ":" + n));
                    WriteToFile(_stockIds);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SettingsManager :: DeleteFromFile failed, Failed to delete stock information from configuration file + '\n'" + ex.Message);
            }
        }

    }
}
