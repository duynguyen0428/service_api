using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;
using System.IO;
using System.Configuration;
using System.Xml.Linq;
using System.Net;
using System.Globalization;
using System.Xml.Serialization;
using McAfee.Utilities.SecurityProvider;
using System.ComponentModel;
using System.Reflection;

namespace Services.Ultility
{
    public class Ultility
    {
        public AESCryptoProviderNew Crypto { get; set; }
        public Ultility(string aeskey)
        {
            Crypto = new AESCryptoProviderNew(aeskey);
            //isMockEnable = ConfigurationManager.AppSettings["EnableMockSCM"].ToLower(CultureInfo.InvariantCulture) == "true";
        }
        public static async Task<string> PostXML(string url,string body)
        {
            string responseBody = string.Empty;
            //if(!isMockEnable)
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            using (HttpClient client = new HttpClient())
            {
                ///set timeout 5 mins
                client.Timeout = System.TimeSpan.FromMinutes(5);
                try
                {
                    var stringContent = new StringContent(body,Encoding.UTF8, "text/xml");
                    HttpResponseMessage response = await client.PostAsync(url, stringContent);
                    var stream = await response.Content.ReadAsStreamAsync();
                    StreamReader loResponseStream = new StreamReader(stream, Encoding.UTF8);
                    responseBody = await loResponseStream.ReadToEndAsync();              
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (HttpRequestException e)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    //TODO: Log exceptions
                   
                    throw;
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch(Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    throw;
                }
                return responseBody;
            }
        }

        public static XDocument LoadXML(string path)
        {
            XDocument doc = null;
            if (string.IsNullOrWhiteSpace(path) | !File.Exists(path))
                return doc;
            doc = XDocument.Load(path);
            return doc; 
        }
        public static string PathBuilder(string root,string affid,string filename)
        {
            var path = string.Empty;
            if (Directory.Exists(string.Format(CultureInfo.InvariantCulture,"{0}/{1}", root, affid)) && File.Exists(string.Format(CultureInfo.InvariantCulture,"{0}/{1}/{2}", root, affid, filename)))
                path = Path.Combine(root, affid, filename);
            return path;
        }

        public static string PathBuilder(string root, string filename)
        {
            var path = string.Empty;
            if (File.Exists(string.Format(CultureInfo.InvariantCulture,"{0}/{1}", root, filename)))
                path = Path.Combine(root, filename);
            return path;
        }

        public void encryptXMLDoc(XDocument doc)
        {
            foreach (XElement element in doc.Descendants())
            {
                encryptXmlElementValue(element);
            }
        }

        private void encryptXmlElementValue(XElement element)
        {
            // update value with encrypted value
            if (element.HasElements)
            {
                foreach (XElement child in element.Elements())
                {
                    if (child is XElement && child.HasElements)
                    {
                        encryptXmlElementValue(child as XElement);
                    }
                }
            }
            else
            {
                if (element.Name != null && element.Name != "PartnerNumber")
                {
                    element.Value = Crypto.URLEncodeAESEncryption(element.Value);
                }
            }
        }


        public string EncryptStringValue(string rawText, bool isUnicode)
        {
            return (isUnicode) ? Crypto.AESEncrypt(rawText) : Crypto.URLEncodeAESEncryption(rawText);
        }

        public string DecryptStringValue(string cipherText, bool isUnicode)
        {
            return (isUnicode) ? Crypto.AESDecrypt(cipherText) : Crypto.URLDecodeAESDecryption(cipherText);
        }

        /// <summary>
        /// Get settings by aff_id
        /// aff_id will be the key to determine which setting to load
        /// </summary>
        /// <param name="aff_id"></param>
        /// <returns></returns>
        //public static XDocument LoadSettingByAffid(int aff_id,string filename)
        //{
        //    /// local vars
        //    XDocument settingXML = null;
        //    string settingPath = ConfigurationManager.AppSettings["XMLSettingsPath"];
        //    ///     + get setting path
        //    string settingsPath = PathBuilder(settingPath,aff_id.ToString(CultureInfo.InvariantCulture), filename);
        //    ///     + load settings from xml to dictionary 
        //    if (!string.IsNullOrWhiteSpace(settingsPath))
        //        settingXML = LoadXML(settingsPath);
        //    return settingXML;
        //}

        /// <summary>
        /// Get template by aff_id
        /// </summary>
        /// <param name="aff_id"></param>
        /// <returns></returns>
        //public static XDocument LoadTemplateByAffid(int aff_id, string filename)
        //{
        //    /// local vars
        //    XDocument settingXML = null;
        //    string tamplatePath = ConfigurationManager.AppSettings["TemplatePath"];
        //    ///     + get setting path
        //    string settingsPath = PathBuilder(tamplatePath, filename);
        //    ///     + load settings from xml to dictionary 
        //    if (!string.IsNullOrWhiteSpace(settingsPath))
        //        settingXML = LoadXML(settingsPath);
        //    return settingXML;
        //}

    }
}
