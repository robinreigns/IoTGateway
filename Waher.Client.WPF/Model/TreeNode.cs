﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows;
using System.Windows.Media;
using Waher.Events;

namespace Waher.Client.WPF.Model
{
	/// <summary>
	/// Abstract base class for tree nodes in the connection view.
	/// </summary>
	public abstract class TreeNode : IDisposable
	{
		private TreeNode parent;
		protected SortedDictionary<string, TreeNode> children = null;
		private object tag = null;
		private bool selected = false;
		private bool expanded = false;

		/// <summary>
		/// Abstract base class for tree nodes in the connection view.
		/// </summary>
		/// <param name="Parent">Parent node.</param>
		public TreeNode(TreeNode Parent)
		{
			this.parent = Parent;
		}

		/// <summary>
		/// Key in parent child collection.
		/// </summary>
		public abstract string Key
		{
			get;
		}

		/// <summary>
		/// If the node has child nodes or not. If null, the state is undefined, and might need to be checked by consulting with the
		/// back-end service corresponding to the node.
		/// </summary>
		public bool? HasChildren
		{
			get
			{
				if (this.children == null)
					return null;
				else
				{
					lock (this.children)
					{
						return this.children.Count > 0;
					}
				}
			}
		}

		/// <summary>
		/// Children of the node. If null, children are not loaded.
		/// </summary>
		public TreeNode[] Children
		{
			get 
			{
				if (this.children == null)
					return null;

				TreeNode[] Children;

				lock (this.children)
				{
					Children = new TreeNode[this.children.Count];
					this.children.Values.CopyTo(Children, 0);
				}

				return Children; 
			}
		}

		/// <summary>
		/// Parent node. May be null if a root node.
		/// </summary>
		public TreeNode Parent
		{
			get { return this.parent; }
		}

		/// <summary>
		/// Object tagged to the node.
		/// </summary>
		public object Tag
		{
			get { return this.tag; }
			set { this.tag = value; }
		}

		/// <summary>
		/// Tree Node header text.
		/// </summary>
		public abstract string Header
		{
			get;
		}

		/// <summary>
		/// Disposes of the node and its resources.
		/// </summary>
		public virtual void Dispose()
		{
		}

		/// <summary>
		/// Saves the object to a file.
		/// </summary>
		/// <param name="Output">Output</param>
		public abstract void Write(XmlWriter Output);

		/// <summary>
		/// Image resource for the node.
		/// </summary>
		public abstract ImageSource ImageResource
		{
			get;
		}

		/// <summary>
		/// Tool Tip for node.
		/// </summary>
		public abstract string ToolTip
		{
			get;
		}

		/// <summary>
		/// Raised when the node has been updated. The sender argument will contain a reference to the node.
		/// </summary>
		public event EventHandler Updated = null;

		/// <summary>
		/// Raises the <see cref="Updated"/> event.
		/// </summary>
		public virtual void OnUpdated()
		{
			this.Raise(this.Updated);
		}

		private void Raise(EventHandler h)
		{
			if (h != null)
			{
				try
				{
					h(this, new EventArgs());
				}
				catch (Exception ex)
				{
					Log.Critical(ex);
				}
			}
		}

		/// <summary>
		/// If the node is selected.
		/// </summary>
		public bool IsSelected
		{
			get { return this.selected; }
			set
			{
				if (this.selected != value)
				{
					this.selected = value;

					if (this.selected)
						this.OnSelected();
					else
						this.OnDeselected();
				}
			}
		}

		/// <summary>
		/// Event raised when the node has been selected.
		/// </summary>
		public event EventHandler Selected = null;

		/// <summary>
		/// Event raised when the node has been deselected.
		/// </summary>
		public event EventHandler Deselected = null;

		/// <summary>
		/// Raises the <see cref="Selected"/> event.
		/// </summary>
		protected virtual void OnSelected()
		{
			this.Raise(this.Selected);
		}

		/// <summary>
		/// Raises the <see cref="Deselected"/> event.
		/// </summary>
		protected virtual void OnDeselected()
		{
			this.Raise(this.Deselected);
		}

		/// <summary>
		/// If the node is expanded.
		/// </summary>
		public bool IsExpanded
		{
			get { return this.expanded; }
			set
			{
				if (this.expanded != value)
				{
					this.expanded = value;

					if (this.expanded)
						this.OnExpanded();
					else
						this.OnCollapsed();
				}
			}
		}

		/// <summary>
		/// Event raised when the node has been expanded.
		/// </summary>
		public event EventHandler Expanded = null;

		/// <summary>
		/// Event raised when the node has been collapsed.
		/// </summary>
		public event EventHandler Collapsed = null;

		/// <summary>
		/// Raises the <see cref="Expanded"/> event.
		/// </summary>
		protected virtual void OnExpanded()
		{
			this.Raise(this.Expanded);
		}

		/// <summary>
		/// Raises the <see cref="Collapsed"/> event.
		/// </summary>
		protected virtual void OnCollapsed()
		{
			this.Raise(this.Collapsed);
		}

		/// <summary>
		/// If children can be added to the node.
		/// </summary>
		public abstract bool CanAddChildren
		{
			get;
		}

		/// <summary>
		/// Is called when the user wants to add a node to the current node.
		/// </summary>
		public virtual void Add()
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// If the node can be recycled.
		/// </summary>
		public abstract bool CanRecycle
		{
			get;
		}

		/// <summary>
		/// Is called when the user wants to recycle the node.
		/// </summary>
		public virtual void Recycle()
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Removes a child node.
		/// </summary>
		/// <param name="Node">Child node.</param>
		/// <returns>If the node was found and removed.</returns>
		public virtual bool Delete(TreeNode Node)
		{
			if (this.children != null)
			{
				lock (this.children)
				{
					return this.children.Remove(Node.Key);
				}
			}
			else
				return false;
		}

	}
}
