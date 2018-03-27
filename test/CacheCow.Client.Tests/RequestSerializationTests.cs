﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using CacheCow.Client;
using CacheCow.Common;
using Xunit;

namespace CacheCow.Tests.Client
{
	
	public class RequestSerializationTests
	{
		[Fact]
		public void IntegrationTest_Serialize()
		{
			var requestMessage = new HttpRequestMessage( HttpMethod.Get, "http://some.server/api/foo");
			requestMessage.Headers.Range = new RangeHeaderValue(0, 1) { Unit = "custom" };
			var serializer = new MessageContentHttpMessageSerializer();
			var memoryStream = new MemoryStream();
			serializer.SerializeAsync(requestMessage, memoryStream).Wait();
			memoryStream.Position = 0;
			var request = serializer.DeserializeToRequestAsync(memoryStream).Result;
			Assert.Equal(requestMessage.Headers.Range.Unit, request.Headers.Range.Unit);
		}
	}
}
