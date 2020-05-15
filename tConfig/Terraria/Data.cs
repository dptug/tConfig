using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Terraria
{
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	public class Data
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(resourceMan, null))
				{
					ResourceManager resourceManager = resourceMan = new ResourceManager("Terraria.Data", typeof(Data).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		public static byte[] Item
		{
			get
			{
				object @object = ResourceManager.GetObject("Item", resourceCulture);
				return (byte[])@object;
			}
		}

		public static byte[] ItemNames
		{
			get
			{
				object @object = ResourceManager.GetObject("ItemNames", resourceCulture);
				return (byte[])@object;
			}
		}

		public static byte[] NPC
		{
			get
			{
				object @object = ResourceManager.GetObject("NPC", resourceCulture);
				return (byte[])@object;
			}
		}

		public static byte[] Projectile
		{
			get
			{
				object @object = ResourceManager.GetObject("Projectile", resourceCulture);
				return (byte[])@object;
			}
		}

		public static byte[] ProjectileNames
		{
			get
			{
				object @object = ResourceManager.GetObject("ProjectileNames", resourceCulture);
				return (byte[])@object;
			}
		}

		public static byte[] Recipes
		{
			get
			{
				object @object = ResourceManager.GetObject("Recipes", resourceCulture);
				return (byte[])@object;
			}
		}

		internal Data()
		{
		}
	}
}
