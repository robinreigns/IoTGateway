﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace Waher.Layout.Layout2D.Model.Figures.SegmentNodes
{
	/// <summary>
	/// Draws a ellipse arc to a point, relative to the end of the last segment
	/// </summary>
	public class EllipseArcToRel : EllipseArcTo
	{
		/// <summary>
		/// Draws a ellipse arc to a point, relative to the end of the last segment
		/// </summary>
		/// <param name="Document">Layout document containing the element.</param>
		/// <param name="Parent">Parent element.</param>
		public EllipseArcToRel(Layout2DDocument Document, ILayoutElement Parent)
			: base(Document, Parent)
		{
		}

		/// <summary>
		/// Local name of type of element.
		/// </summary>
		public override string LocalName => "EllipseArcToRel";

		/// <summary>
		/// Creates a new instance of the layout element.
		/// </summary>
		/// <param name="Document">Document containing the new element.</param>
		/// <param name="Parent">Parent element.</param>
		/// <returns>New instance.</returns>
		public override ILayoutElement Create(Layout2DDocument Document, ILayoutElement Parent)
		{
			return new EllipseArcToRel(Document, Parent);
		}
	}
}
