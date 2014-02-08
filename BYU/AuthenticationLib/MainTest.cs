//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Runtime.Serialization.Json;
//using System.Text;
//using System.Threading.Tasks;

//namespace Authentication
//{
//    public class MainTest
//    {
//        public static void Main()
//        {
//            Console.Write("Enter NetID: ");
//            string username = Console.ReadLine();

//            Console.Write("Enter password: ");
//            string password = Console.ReadLine();

//            WebServiceSession session = NonceAuthentication.GetWsSession(username, password, 5);

//            string nonceHeader = NonceAuthentication.GetNonceAuthHeader(session);

//            try
//            {
//                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ws.byu.edu/rest/v1.0/academic/registration/studentschedule/" + session.personId + "/20141");
//                request.Method = "GET";
//                //request.Accept = "application/json";
//                request.Headers.Add("Authorization", nonceHeader);
//                //Stream requestStream = request.GetRequestStream();
//                Stream response = request.GetResponse().GetResponseStream();
//                //string testResult = new StreamReader(response).ReadToEnd();

//                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ClassSchedule));
//                ClassSchedule schedule = (ClassSchedule)serializer.ReadObject(response);
//                Console.WriteLine("Email:" + schedule.WeeklySchedService.response.email_address);

                
//            }
//            catch (WebException ex)
//            {
//                Stream errorStream = ex.Response.GetResponseStream();
//                StreamReader streamReader = new StreamReader(errorStream);

//                Console.WriteLine(streamReader.ReadToEnd());
//            }
//            Console.ReadLine();
            
//            /*RecMainServiceType myTest = new RecMainServiceType();
//            myTest.request = new requestType();
//            myTest.request.method = "Courses";
//            myTest.request.attributes = "";
//            myTest.request.resource = username;
//            myTest.request.status = "1";
//            myTest.request.statusMessage = "OK";

//            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RecMainServiceType));
//            //serializer.WriteObject(requestStream, myTest);
//            //requestStream.Close();
            



//            try
//            {
//                Console.WriteLine("result: " + new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd());
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("An error ocurred: " + ex.Message);
//            }*/
//        }
//    }
//}
