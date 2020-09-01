using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LM_API_Samples
{
	class Program
	{
		static void Main( string[ ] args )
		{
			var url = System.Configuration.ConfigurationSettings.AppSettings[ "url" ];
			var key = System.Configuration.ConfigurationSettings.AppSettings[ "apikey" ];
			string secret = System.Configuration.ConfigurationSettings.AppSettings[ "apisecret" ];
			string tid = System.Configuration.ConfigurationSettings.AppSettings[ "tenantid" ];

			UpdateOrCreateCompany( url,key, secret, tid );
			UpdateOrCreateDepartment( url, key, secret, tid );
			ImportUsers( url, key, secret, tid );

			GetTrainingReport( url, key, secret, tid );
			GetUsersLearningProfile( url, key, secret, tid );
		}

		static void GetTrainingReport( string url, string key, string secret, string tid )
		{
			url = url + "?fromdate=22-MAY-2020 12:00:00.000&ct=&todate=22-MAY-2020 23:59:59.000&returnmandatorytrainingsonly=true";
			var httpWebRequest = ( HttpWebRequest )WebRequest.Create( url );
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "GET";

			httpWebRequest.Headers.Add( "X-BIZAPP-TID", tid );
			httpWebRequest.Headers.Add( "X-BIZAPP-APIKEY", key );
			httpWebRequest.Headers.Add( "X-BIZAPP-APISECRET", secret );
			httpWebRequest.Headers.Add( "X-BIZAPP-TYPEID", "EQMS7feb" );
			httpWebRequest.Headers.Add( "X-BIZAPP-METHOD", "GetTrainingCompletionReport" );

			var httpResponse = ( HttpWebResponse )httpWebRequest.GetResponse( );
			using ( var streamReader = new StreamReader( httpResponse.GetResponseStream( ) ) )
			{
				var result = streamReader.ReadToEnd( );
			}
		}


		static void GetUsersLearningProfile( string url, string key, string secret, string tid )
		{
			url = url
				+ "?fromdate=22-MAY-2018 12:00:00.000&todate=22-MAY-2020 23:59:59.000&employeeid=APP010&status=completed";
			var httpWebRequest = ( HttpWebRequest )WebRequest.Create( url );
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "GET";

			httpWebRequest.Headers.Add( "X-BIZAPP-TID", tid );
			httpWebRequest.Headers.Add( "X-BIZAPP-APIKEY", key );
			httpWebRequest.Headers.Add( "X-BIZAPP-APISECRET", secret );
			httpWebRequest.Headers.Add( "X-BIZAPP-TYPEID", "EQMS7feb" );
			httpWebRequest.Headers.Add( "X-BIZAPP-METHOD", "GetUsersLearningProfile" );

			var httpResponse = ( HttpWebResponse )httpWebRequest.GetResponse( );
			using ( var streamReader = new StreamReader( httpResponse.GetResponseStream( ) ) )
			{
				var result = streamReader.ReadToEnd( );
			}
		}


		static void UpdateOrCreateCompany( string url, string key, string secret, string tid )
		{
				
			var httpWebRequest = ( HttpWebRequest )WebRequest.Create( url );
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";

			httpWebRequest.Headers.Add( "X-BIZAPP-TID", tid );
			httpWebRequest.Headers.Add( "X-BIZAPP-APIKEY", key );
			httpWebRequest.Headers.Add( "X-BIZAPP-APISECRET", secret );
			httpWebRequest.Headers.Add( "X-BIZAPP-TYPEID", "System1242" );
			httpWebRequest.Headers.Add( "X-BIZAPP-METHOD", "UpdateOrCreateCompany" );

			using ( var streamWriter = new StreamWriter( httpWebRequest.GetRequestStream( ) ) )
			{
				string json = GetJSONFromResources( "LM_API_Samples.JSON.CompanyJson.txt" );
				streamWriter.Write( json );
			}

			var httpResponse = ( HttpWebResponse )httpWebRequest.GetResponse( );
			using ( var streamReader = new StreamReader( httpResponse.GetResponseStream( ) ) )
			{
				var result = streamReader.ReadToEnd( );
			}
		}


		static void UpdateOrCreateDepartment( string url, string key, string secret, string tid )
		{

			var httpWebRequest = ( HttpWebRequest )WebRequest.Create( url );
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";

			httpWebRequest.Headers.Add( "X-BIZAPP-TID", tid );
			httpWebRequest.Headers.Add( "X-BIZAPP-APIKEY", key );
			httpWebRequest.Headers.Add( "X-BIZAPP-APISECRET", secret );
			httpWebRequest.Headers.Add( "X-BIZAPP-TYPEID", "System1242" );
			httpWebRequest.Headers.Add( "X-BIZAPP-METHOD", "UpdateOrCreateDepartment" );

			using ( var streamWriter = new StreamWriter( httpWebRequest.GetRequestStream( ) ) )
			{
				string json = GetJSONFromResources( "LM_API_Samples.JSON.DeptJSON.txt" );
				streamWriter.Write( json );
			}

			var httpResponse = ( HttpWebResponse )httpWebRequest.GetResponse( );
			using ( var streamReader = new StreamReader( httpResponse.GetResponseStream( ) ) )
			{
				var result = streamReader.ReadToEnd( );
			}
		}


		static void ImportUsers( string url, string key, string secret, string tid )
		{

			var httpWebRequest = ( HttpWebRequest )WebRequest.Create( url );
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";

			httpWebRequest.Headers.Add( "X-BIZAPP-TID", tid );
			httpWebRequest.Headers.Add( "X-BIZAPP-APIKEY", key );
			httpWebRequest.Headers.Add( "X-BIZAPP-APISECRET", secret );
			httpWebRequest.Headers.Add( "X-BIZAPP-TYPEID", "System1242" );
			httpWebRequest.Headers.Add( "X-BIZAPP-METHOD", "UpdateOrCreateUsers" );

			using ( var streamWriter = new StreamWriter( httpWebRequest.GetRequestStream( ) ) )
			{
				string json = GetJSONFromResources( "LM_API_Samples.JSON.usersJSON.txt" );
				streamWriter.Write( json );
			}

			var httpResponse = ( HttpWebResponse )httpWebRequest.GetResponse( );
			using ( var streamReader = new StreamReader( httpResponse.GetResponseStream( ) ) )
			{
				var result = streamReader.ReadToEnd( );
			}
		}

		static string GetJSONFromResources( string resourceName )
		{
			Assembly assem = typeof( Program ).Assembly;
			var resources = assem.GetManifestResourceNames( );
			using ( Stream stream = assem.GetManifestResourceStream( resourceName ) )
			{
				using ( var reader = new StreamReader( stream ) )
				{
					return reader.ReadToEnd( );
				}
			}
		}
	}
}
