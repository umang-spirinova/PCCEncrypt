using IFMSecurity;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using WebApplication_encrypt_decrypt;


namespace WebApplication_encrypt_decrypt.Controllers
{

    public class ValuesController : ApiController
    {


        [HttpPost]
        [Route("api/decrypt/")]
        public HttpResponseMessage Post([FromBody] DecrypRequest value)
        {

            Console.WriteLine("Value umang:" + value);
            var pk = new PKCS12();

            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var clientPath = Path.Combine(basePath, "Controllers", "client.p12");
            var serverPath = Path.Combine(basePath, "Controllers", "server.cer");
            var init = pk.initial(clientPath, serverPath);
           // var init = pk.initial("C:\\Users\\umang\\source\\repos\\WebApplication_encrypt_decrypt\\WebApplication_encrypt_decrypt\\Controllers\\client.p12", "C:\\Users\\umang\\source\\repos\\WebApplication_encrypt_decrypt\\WebApplication_encrypt_decrypt\\Controllers\\server.cer");
            PropertyInfo[] properties = value.GetType().GetProperties();
            var xml = value.xmlString;
            string wrappedXml = "<root>" + xml + "</root>";
            XDocument xmlDoc = XDocument.Parse(wrappedXml);


            




            Console.WriteLine("temp" + xmlDoc);
            List<string> values = new List<string>();
            
            var securedKeyPCC = value.securedKey;
            var digitalSignature = "";

            foreach (var property in xmlDoc.Root.Elements())
            {

                if ( property.Name != "digitalSignature")
                {


                    string[] decrypt = new string[1];
                    string strValue = property.Value.ToString();
                    string[] encryptedPCC = new string[] { strValue };      //Input: Field: RefNo1-5, UserId (Each)
                    pk.decrypt(securedKeyPCC, encryptedPCC, ref decrypt);
                    string temp = decrypt[0];
                    Console.WriteLine("Val" + temp);

                    values.Add(temp);

                }
                else
                {
                    digitalSignature = property.Value.ToString().Trim();
                }

            }

            var temp_dig = $@"<productId>{value.productId}</productId><frInstBrch>{value.frmInstBrch}</frInstBrch><agcyId>{value.agcyId}</agcyId><refno1>{values[0]}</refno1><refno2>{values[1]}</refno2><refno3>{values[2]}</refno3><refno4>{values[3]}</refno4><refno5>ACCT NAME</refno5><amt>{value.amt}</amt><postFepTraceNbr>{value.postFepTraceNbr}</postFepTraceNbr><responseCode>{value.responseCode}</responseCode><terminalId>{value.terminalId}"+"   "+ $@"</terminalId><userId>{values[5]}</userId>";
            var tot = "";

            var yo = XMLtoByte(temp_dig);
            //var digital = pk.sign(bytes);

            var flag = pk.verify(yo,digitalSignature);
            

            var data = new { Message = "Success!", Data = flag };
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json")
            };
            return response;



        }

        static XmlDocument JsonToXml(string json)
        {
            XmlDocument xmlDoc = new XmlDocument();
            JObject jsonObject = JObject.Parse(json);
            string xmlString = JsonConvert.DeserializeXmlNode(jsonObject.ToString(), "Root").OuterXml;
            xmlDoc.LoadXml(xmlString);
            return xmlDoc;
        }
        static string PrettyPrintXml(XmlDocument xmlDoc)
        {
            using (var stringWriter = new System.IO.StringWriter())
            using (var xmlTextWriter = new XmlTextWriter(stringWriter) { Formatting = System.Xml.Formatting.Indented })
            {
                xmlDoc.WriteTo(xmlTextWriter);
                return stringWriter.ToString();
            }
        }
        [HttpPost]
        [Route("api/encrypt/")]
        public HttpResponseMessage Post([FromBody] InquiryPCCRequest value)
        {



            var pk = new PKCS12();

            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var clientPath = Path.Combine(basePath, "Controllers", "client.p12");
            var serverPath = Path.Combine(basePath, "Controllers", "server.cer");
            var init = pk.initial(clientPath, serverPath);
            //var init = pk.initial("C:\\Users\\umang\\source\\repos\\WebApplication_encrypt_decrypt\\WebApplication_encrypt_decrypt\\Controllers\\client.p12", "C:\\Users\\umang\\source\\repos\\WebApplication_encrypt_decrypt\\WebApplication_encrypt_decrypt\\Controllers\\server.cer");

            //var digital_sign;
            //string json = JsonConvert.SerializeObject(value.dataInfo,Newtonsoft.Json.Formatting.Indented);
            //XmlDocument xmlDoc = JsonToXml(json);
            //Console.WriteLine(xmlDoc.ToString());


            var arr = new string[] { value.dataInfo.Refno1, value.dataInfo.Refno2, value.dataInfo.Refno3, value.dataInfo.Refno4, value.dataInfo.Refno5, value.dataInfo.UserId }; //Input: Field: RefNo1-5, UserId (Each)

            string xml = value.dataInfo.digitalSignature;

            var bytes = XMLtoByte(xml);
            var digital = pk.sign(bytes);

            string[] encryptedArr = new string[6];

            string securedKey = "";

            pk.encrypt(arr, ref encryptedArr, ref securedKey);

            var data = new { Message = "Success!", Data = new { encryptedArr, digital, securedKey } };
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json")
            };

            return response;


        }
        public static byte[] XMLtoByte(string xml)
        {
            return Encoding.UTF8.GetBytes(xml);
        }
        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
