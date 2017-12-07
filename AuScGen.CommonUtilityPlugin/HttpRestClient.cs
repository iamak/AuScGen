// ***********************************************************************
// <copyright file="HttpRestClient.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>HttpRestClient class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using CodeScales.Http;
using CodeScales.Http.Entity;
using CodeScales.Http.Methods;
using Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AuScGen.CommonUtilityPlugin
{
	/// <summary>
	/// This Class provides method to make GET,POST,PUT requests
	/// and verify the response returned with the DB values
	/// </summary>
	[Export(typeof(IPlugin))]
	public class HttpRestClient : IPlugin
	{
		/// <summary>
		/// The client
		/// </summary>
		HttpClient client = null;
		/// <summary>
		/// The HTTP get
		/// </summary>
		HttpGet httpGet = null;
		/// <summary>
		/// The HTTP post
		/// </summary>
		HttpPost httpPost = null;
		/// <summary>
		/// The response
		/// </summary>
		HttpResponse response = null;
		/// <summary>
		/// Gets or sets the response code.
		/// </summary>
		/// <value>
		/// The response code.
		/// </value>
		public HttpRestClient()
		{
			client = new HttpClient();
		}

		/// <summary>
		/// Gets the request.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="requestParameter">The request parameter dic.</param>
		/// <returns></returns>
		public Dictionary<string, string> PostRequest(Uri url, Dictionary<string, string> requestParameter)
		{
			httpPost = new HttpPost(url);
			foreach (KeyValuePair<string, string> entry in requestParameter)
			{
				httpPost.Parameters.Add(entry.Key, entry.Value);
			}
			//Get the response
			response = client.Execute(httpPost);
			//Get the response string

			//Get the Response Code and Save it
			string responseCode = response.ResponseCode.ToString();
			//Parse the response and return the values in the Dictionary
			Dictionary<string, string> responseCollection = null;
			if (responseCode == "200" && response != null)
			{
				//Console.WriteLine("Received the response" + responseString);
				responseCollection = new Dictionary<string, string>();
			}
			else
			{
				//Console.WriteLine("No Result returned");
			}
			return responseCollection;
		}

		/// <summary>
		/// Gets the request.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="requestParameter">The request parameter dic.</param>
		/// <returns></returns>
		public IList<ResponseDataItem> GetRequest(Uri url, Dictionary<string, string> requestParameter)
		{
			httpGet = new HttpGet(url);

			foreach (KeyValuePair<string, string> entry in requestParameter)
			{
				httpGet.Parameters.Add(entry.Key, entry.Value);
			}
			//Get the response
			response = client.Execute(httpGet);
			//Get the response string
			string responseString = EntityUtils.ToString(response.Entity);	
			//Get the Response Code and Save it
			string responseCode = response.ResponseCode.ToString();
			//Parse the response and return the values in the Dictionary

			if (responseCode == "200" && response != null)
			{
			}
			else
			{
				//Console.WriteLine("No Result returned");
			}
			return GetData(responseString);
		}

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		private List<ResponseDataItem> GetData(string response)
		{
			JArray DeserializedResponse = ((JArray)JsonConvert.DeserializeObject(response));

			List<ResponseDataItem> DataList = new List<ResponseDataItem>();

			ResponseDataItem EachItem = new ResponseDataItem();

			//Dictionary<string,string> EachData = new Dictionary<string,string>();

			foreach (JToken dataObject in DeserializedResponse)
			{
				EachItem = new ResponseDataItem();

				foreach (JProperty dataProperty in dataObject)
				{
					//Console.WriteLine(dataProperty.Name);

					if (dataProperty.Name.Equals("data"))
					{
						foreach (JArray data in dataProperty)
						{
							foreach (JArray eachData in data)
							{
								//Console.WriteLine(eachData[0]);
								//Console.WriteLine(eachData[1]);

								EachItem.Data.Add(eachData[0].ToString(), eachData[1].ToString());
							}
						}
					}
					else
					{
						//Console.WriteLine(dataProperty.Value);
						EachItem.Name = dataProperty.Value.ToString();
					}

				}

				DataList.Add(EachItem);
			}

			return DataList;
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description
		{
			get
			{
				return "Service Access Plugin";
			}
			set
			{
				Description = value;
			}
		}
	}
}
