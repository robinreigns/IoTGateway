using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Waher.Networking.DNS.Communication;
using Waher.Networking.DNS.Enumerations;
using Waher.Networking.DNS.ResourceRecords;

namespace Waher.Networking.DNS.Test
{
	[TestClass]
	public class DnsUdpClientTests : DnsClientTests
	{
		protected override DnsClient CreateClient()
		{
			return new DnsUdpClient();
		}
	}
}
