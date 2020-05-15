﻿using System;
using System.Collections.Generic;
using System.Numerics;
using Waher.Runtime.Inventory;

namespace Waher.Script.Output
{
	/// <summary>
	/// Converts values of type Complex to expression strings.
	/// </summary>
	public class ComplexOutput : ICustomStringOutput
	{
		/// <summary>
		/// If the interface understands objects such as <paramref name="Object"/>.
		/// </summary>
		/// <param name="Object">Object</param>
		/// <returns>How well objects of this type are supported.</returns>
		public Grade Supports(Type Object) => Object == typeof(Complex) ? Grade.Ok : Grade.NotAtAll;

		/// <summary>
		/// Gets a string representing a value.
		/// </summary>
		/// <param name="Value">Value</param>
		/// <returns>Expression string.</returns>
		public string GetString(object Value)
		{
			return Expression.ToString((Complex)Value);
		}
	}
}
