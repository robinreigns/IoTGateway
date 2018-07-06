﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace Waher.Networking.XMPP.PEP
{
	/// <summary>
	/// Interface for personal event objects.
	/// </summary>
    public interface IPersonalEvent
    {
		/// <summary>
		/// Node on which the personal event is published on.
		/// </summary>
		string Node
		{
			get;
		}

		/// <summary>
		/// Local name of the event element.
		/// </summary>
		string LocalName
		{
			get;
		}

		/// <summary>
		/// Namespace of the event element.
		/// </summary>
		string Namespace
		{
			get;
		}

		/// <summary>
		/// XML representation of the event.
		/// </summary>
		string PayloadXml
		{
			get;
		}

		/// <summary>
		/// Parses a personal event from its XML representation
		/// </summary>
		/// <param name="E">XML representation of personal event.</param>
		/// <returns>Personal event object.</returns>
		IPersonalEvent Parse(XmlElement E);
    }
}
