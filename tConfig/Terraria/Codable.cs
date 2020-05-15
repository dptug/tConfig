using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Terraria
{
	public class Codable
	{
		public static Dictionary<int, object> customMethodReturnDict;

		public static Dictionary<int, object[]> customMethodRefReturnDict;

		public static bool globalRunKnowledge = true;

		public static Dictionary<string, object> customVars;

		public static int thrownErrors = 0;

		public static HashSet<string> noMethod = new HashSet<string>();

		public Dictionary<string, Delegate> delegates;

		public object[] globsubclass;

		public byte[][] globsavedata;

		public byte[][] unloadedSavedata;

		public object subclass;

		public byte[] savedata;

		public string className;

		public string name;

		public Vector2 position;

		public Vector2 velocity;

		public int width;

		public int height;

		public Action<BinaryWriter> Save;

		public Action<BinaryReader, int> Load;

		public bool active;

		public bool wet;

		public byte wetCount;

		public bool lavaWet;

		public int direction;

		public float direction2;

		public Func<object[], object>[] HookList;

		public DefHandler defs = new DefHandler();

		public static object customMethodReturn
		{
			get
			{
				object value = null;
				customMethodReturnDict.TryGetValue(Thread.CurrentThread.ManagedThreadId, out value);
				return value;
			}
			set
			{
				customMethodReturnDict[Thread.CurrentThread.ManagedThreadId] = value;
			}
		}

		public static object[] customMethodRefReturn
		{
			get
			{
				object[] value = null;
				customMethodRefReturnDict.TryGetValue(Thread.CurrentThread.ManagedThreadId, out value);
				return value;
			}
			set
			{
				customMethodRefReturnDict[Thread.CurrentThread.ManagedThreadId] = value;
			}
		}

		public float gravDir
		{
			get
			{
				return direction2;
			}
			set
			{
				direction2 = value;
			}
		}

		public int directionY
		{
			get
			{
				return (int)direction2;
			}
			set
			{
				direction2 = value;
			}
		}

		public Vector2 ScreenPosition
		{
			get
			{
				return position - Main.screenPosition;
			}
			set
			{
				position = Main.screenPosition + value;
			}
		}

		public Vector2 ScreenCentre
		{
			get
			{
				return Centre - Main.screenPosition;
			}
			set
			{
				position = ScreenPosition - Center + position;
			}
		}

		public Vector2 ScreenCenter
		{
			get
			{
				return Centre - Main.screenPosition;
			}
			set
			{
				position = ScreenPosition - Center + position;
			}
		}

		public Vector2 Centre
		{
			get
			{
				return new Vector2(position.X + (float)width * 0.5f, position.Y + (float)height * 0.5f);
			}
			set
			{
				position = new Vector2(value.X - (float)width * 0.5f, value.Y - (float)height * 0.5f);
			}
		}

		public Vector2 Center
		{
			get
			{
				return new Vector2(position.X + (float)width * 0.5f, position.Y + (float)height * 0.5f);
			}
			set
			{
				position = new Vector2(value.X - (float)width * 0.5f, value.Y - (float)height * 0.5f);
			}
		}

		public Vector2 Left
		{
			get
			{
				return new Vector2(position.X, position.Y + (float)height * 0.5f);
			}
			set
			{
				position = new Vector2(value.X, value.Y - (float)height * 0.5f);
			}
		}

		public Vector2 Right
		{
			get
			{
				return new Vector2(position.X + (float)width, position.Y + (float)height * 0.5f);
			}
			set
			{
				position = new Vector2(value.X - (float)width, value.Y - (float)height * 0.5f);
			}
		}

		public Vector2 Top
		{
			get
			{
				return new Vector2(position.X + (float)width * 0.5f, position.Y);
			}
			set
			{
				position = new Vector2(value.X - (float)width * 0.5f, value.Y);
			}
		}

		public Vector2 Bottom
		{
			get
			{
				return new Vector2(position.X + (float)width * 0.5f, position.Y + (float)height);
			}
			set
			{
				position = new Vector2(value.X - (float)width * 0.5f, value.Y - (float)height);
			}
		}

		public Vector2 Size
		{
			get
			{
				return new Vector2(width, height);
			}
			set
			{
				width = (int)value.X;
				height = (int)value.Y;
			}
		}

		public Rectangle Hitbox
		{
			get
			{
				return new Rectangle((int)position.X, (int)position.Y, width, height);
			}
			set
			{
				position = new Vector2(value.X, value.Y);
				width = value.Width;
				height = value.Height;
			}
		}

		public virtual void ResetEvents()
		{
			Save = null;
			Load = null;
			delegates = new Dictionary<string, Delegate>();
		}

		public virtual void RegisterEvents(object code)
		{
			if (code != null)
			{
				Register(ref Save, code, "Save");
				Register(ref Load, code, "Load");
			}
		}

		public virtual void SetDefaultEvents()
		{
		}

		public static bool RunEvent(Delegate a, params object[] args)
		{
			if ((object)a == null)
			{
				return false;
			}
			customMethodReturn = a.DynamicInvoke(args);
			customMethodRefReturn = args;
			return true;
		}

		public bool RunEvent(string name, params object[] args)
		{
			Delegate value = null;
			delegates.TryGetValue(name, out value);
			if ((object)value == null)
			{
				return false;
			}
			customMethodReturn = value.DynamicInvoke(args);
			customMethodRefReturn = args;
			return true;
		}

		public bool RunEventRef(string methodName, params object[] parameters)
		{
			bool result = false;
			Delegate value = null;
			delegates.TryGetValue(name, out value);
			if ((object)value == null)
			{
				return false;
			}
			Delegate[] invocationList = value.GetInvocationList();
			foreach (Delegate a in invocationList)
			{
				if (RunEvent(a, parameters))
				{
					result = true;
				}
			}
			return result;
		}

		public static bool ExpectedEvent(Func<bool> f, bool expected)
		{
			if (f == null)
			{
				return false;
			}
			bool flag = f();
			return flag == expected;
		}

		public static bool ExpectedEvent(Delegate f, bool expected, params object[] args)
		{
			if ((object)f == null)
			{
				return false;
			}
			bool flag = (bool)f.DynamicInvoke(args);
			return flag == expected;
		}

		public static bool Exists(Delegate a)
		{
			return (object)a != null;
		}

		public static Delegate GetDelegate(object subclass, string methodName, Type delegateType)
		{
			Delegate result = null;
			MethodInfo method = subclass.GetType().GetMethod(methodName);
			if (method != null)
			{
				result = Delegate.CreateDelegate(delegateType, subclass, method, throwOnBindFailure: false);
			}
			return result;
		}

		public static T GetDel<T>(object subclass, string methodName) where T : class
		{
			Type typeFromHandle = typeof(T);
			Delegate @delegate = null;
			MethodInfo method = subclass.GetType().GetMethod(methodName);
			if (method != null)
			{
				@delegate = (method.IsStatic ? Delegate.CreateDelegate(typeFromHandle, method, throwOnBindFailure: false) : Delegate.CreateDelegate(typeFromHandle, subclass, method, throwOnBindFailure: false));
			}
			return @delegate as T;
		}

		public void Register<T>(ref T method, object subclass, string methodName) where T : class
		{
			T del = GetDel<T>(subclass, methodName);
			if (del != null)
			{
				Delegate @delegate = Delegate.Combine(method as Delegate, del as Delegate);
				delegates[methodName] = @delegate;
				method = (@delegate as T);
			}
			else
			{
				delegates[methodName] = (method as Delegate);
			}
		}

		public void RegisterDel<T>(ref T method, T newMethod, string methodName) where T : class
		{
			if (method == null)
			{
				method = newMethod;
				delegates[methodName] = (method as Delegate);
			}
			else
			{
				Delegate @delegate = Delegate.Combine(method as Delegate, newMethod as Delegate);
				delegates[methodName] = @delegate;
				method = (@delegate as T);
			}
		}

		public Codable()
		{
			globsubclass = new object[1];
			globsavedata = new byte[1][];
			savedata = null;
			unloadedSavedata = null;
			ResetEvents();
		}

		public void Reset()
		{
			subclass = null;
			globsubclass = new object[1];
			globsavedata = new byte[1][];
			unloadedSavedata = null;
			savedata = null;
			ResetEvents();
		}

		public virtual void Init(int type = -1)
		{
			Reset();
			delegates = new Dictionary<string, Delegate>();
			defs = new DefHandler();
			if (className == "Item")
			{
				defs = Config.itemDefs;
			}
			else if (className == "NPC")
			{
				defs = Config.npcDefs;
			}
			else if (className == "Projectile")
			{
				defs = Config.projDefs;
			}
			else if (className == "Buff")
			{
				defs = Config.buffDefs;
			}
			else
			{
				if (!(className == "Tile"))
				{
					return;
				}
				defs = Config.tileDefs;
			}
			if (string.IsNullOrEmpty(className) || defs == null || Config.modsLoading)
			{
				SetDefaultEvents();
				return;
			}
			Assembly assembly = null;
			if (type == -1)
			{
				assembly = defs.assemblyByName[name];
			}
			if (type > -1)
			{
				assembly = defs.assemblyByType[type];
			}
			ArrayList globalAssembly = defs.globalAssembly;
			unloadedSavedata = null;
			savedata = null;
			if (assembly != null)
			{
				if (className == "Tile")
				{
					subclass = assembly.CreateInstance(Config.namespaces[assembly.FullName] + "." + ParseName(name) + className);
				}
				else
				{
					subclass = assembly.CreateInstance(Config.namespaces[assembly.FullName] + "." + ParseName(name) + className, ignoreCase: false, BindingFlags.CreateInstance, null, new object[1]
					{
						this
					}, null, null);
				}
				RegisterEvents(subclass);
			}
			globsubclass = new object[globalAssembly.Count];
			globsavedata = new byte[globalAssembly.Count][];
			for (int i = 0; i < globsubclass.Length; i++)
			{
				if (className == "Tile")
				{
					globsubclass[i] = null;
				}
				else
				{
					globsubclass[i] = ((Assembly)globalAssembly[i]).CreateInstance(Config.namespaces[((Assembly)globalAssembly[i]).FullName] + ".Global_" + className, ignoreCase: false, BindingFlags.CreateInstance, null, new object[1]
					{
						this
					}, null, null);
				}
				RegisterEvents(globsubclass[i]);
			}
			SetDefaultEvents();
		}

		public static Vector2 GetPos(Vector2 pos)
		{
			int num = (int)pos.X;
			int num2 = (int)pos.Y;
			int type = Main.tile[num, num2].type;
			int frameNumber = Main.tile[num, num2].frameNumber;
			pos.X -= Main.tile[num, num2].frameX / 18;
			pos.X += Config.tileDefs.width[type] * frameNumber;
			pos.Y -= Main.tile[num, num2].frameY / 18;
			return pos;
		}

		public static void InitTile(Vector2 pos, int type)
		{
			if (Config.tileDefs.height[type] > 1 || Config.tileDefs.width[type] > 1)
			{
				pos = GetPos(pos);
			}
			if (!Config.tileDefs.code.ContainsKey(pos))
			{
				Codable codable = new Codable();
				codable.name = Main.tileName[type];
				codable.className = "Tile";
				codable.Init();
				if (codable.subclass != null)
				{
					codable.RunMethod("Initialize", (int)pos.X, (int)pos.Y);
					Config.tileDefs.code[pos] = codable;
					WorldGen.TileUpdate += (Action)GetDelegate(codable.subclass, "Update", typeof(Action));
				}
			}
		}

		public static bool RunTileMethod(bool init, Vector2 pos, int type, string methodName, params object[] parameters)
		{
			if (Config.tileDefs.height[type] > 1 || Config.tileDefs.width[type] > 1)
			{
				pos = GetPos(pos);
			}
			if (init)
			{
				InitTile(pos, type);
			}
			bool result = false;
			if (methodName != "Load" && methodName != "Save" && methodName != "Update")
			{
				foreach (string key in Config.globalMod["ModWorld"].Keys)
				{
					object obj = Config.globalMod["ModWorld"][key];
					if (obj != null && RunSpecifiedMethod(key + " ModWorld", obj, methodName, parameters))
					{
						result = true;
					}
				}
			}
			if (!Config.tileDefs.code.ContainsKey(pos))
			{
				return result;
			}
			if (Config.tileDefs.code[pos].RunMethod(methodName, parameters))
			{
				result = true;
			}
			return result;
		}

		public static void DestroyTile(Vector2 pos)
		{
			pos = GetPos(pos);
			if (Config.tileDefs.code.ContainsKey(pos))
			{
				if (Config.tileDefs.code[pos].subclass != null)
				{
					WorldGen.TileUpdate -= (Action)GetDelegate(Config.tileDefs.code[pos].subclass, "Update", typeof(Action));
				}
				Config.tileDefs.code.Remove(pos);
			}
		}

		public static bool RunTileMethodRef(bool init, Vector2 pos, int type, string methodName, params object[] parameters)
		{
			if (Config.tileDefs.height[type] > 1 || Config.tileDefs.width[type] > 1)
			{
				pos = GetPos(pos);
			}
			if (init)
			{
				InitTile(pos, type);
			}
			bool result = false;
			if (methodName != "Load" && methodName != "Save" && methodName != "Update")
			{
				foreach (string key in Config.globalMod["ModWorld"].Keys)
				{
					object obj = Config.globalMod["ModWorld"][key];
					if (obj != null && RunSpecifiedMethodRef(key + " ModWorld", obj, methodName, parameters))
					{
						if (customMethodRefReturn != null)
						{
							parameters = customMethodRefReturn;
						}
						result = true;
					}
				}
			}
			if (!Config.tileDefs.code.ContainsKey(pos))
			{
				return result;
			}
			if (Config.tileDefs.code[pos].RunMethodRef(methodName, parameters))
			{
				result = true;
			}
			return result;
		}

		public static bool RunGlobalMethod(string className, string methodName, params object[] parameters)
		{
			Delegate value = null;
			switch (className)
			{
			case "ModWorld":
				Events.world.delegates.TryGetValue(methodName, out value);
				break;
			case "ModPlayer":
				Events.player.delegates.TryGetValue(methodName, out value);
				break;
			case "ModGeneric":
				Events.generic.delegates.TryGetValue(methodName, out value);
				break;
			}
			if ((object)value != null)
			{
				return RunEvent(value, parameters);
			}
			Dictionary<string, object> dictionary = Config.globalMod[className];
			bool result = false;
			foreach (KeyValuePair<string, object> item in dictionary)
			{
				_ = item.Key;
				object value2 = item.Value;
				if (value2 != null && RunSpecifiedMethod(item.Key + " " + className, value2, methodName, parameters))
				{
					result = true;
				}
			}
			return result;
		}

		public static bool RunPlayerEvent(string methodName, bool useItem, Player player, params object[] parameters)
		{
			bool result = false;
			object[] array = new object[parameters.Length + 1];
			array[0] = player;
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i + 1] = parameters[i];
			}
			if (useItem)
			{
				Item item = player.inventory[player.selectedItem];
				Delegate value = null;
				item.playerEvents.delegates.TryGetValue(methodName, out value);
				if ((object)value != null)
				{
					value.DynamicInvoke(array);
					result = true;
				}
			}
			for (int j = 0; j < player.armor.Length; j++)
			{
				Item item2 = player.armor[j];
				Delegate value2 = null;
				item2.playerEvents.delegates.TryGetValue(methodName, out value2);
				if ((object)value2 != null)
				{
					value2.DynamicInvoke(array);
					result = true;
				}
			}
			for (int k = 0; k < player.buffType.Length; k++)
			{
				if (RunSpecifiedMethod("Buff " + Main.buffName[player.buffType[k]], player.buffCode[k], methodName, array))
				{
					result = true;
				}
			}
			Delegate value3 = null;
			Events.player.delegates.TryGetValue(methodName, out value3);
			if ((object)value3 != null)
			{
				value3.DynamicInvoke(array);
				result = true;
			}
			customMethodRefReturn = array;
			return result;
		}

		public static bool RunPlayerMethod(string methodName, bool useItem, Player player, params object[] parameters)
		{
			if (Events.player.delegates.ContainsKey(methodName))
			{
				return RunPlayerEvent(methodName, useItem, player, parameters);
			}
			bool result = false;
			object[] array = new object[parameters.Length + 1];
			array[0] = player;
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i + 1] = parameters[i];
			}
			if (useItem)
			{
				Item item = player.inventory[player.selectedItem];
				if (item.RunMethod(methodName, array))
				{
					result = true;
				}
			}
			for (int j = 0; j < player.armor.Length; j++)
			{
				Item item2 = player.armor[j];
				if (item2.RunMethod(methodName, array))
				{
					result = true;
				}
			}
			for (int k = 0; k < player.buffType.Length; k++)
			{
				if (RunSpecifiedMethod("Buff " + Main.buffName[player.buffType[k]], player.buffCode[k], methodName, array))
				{
					result = true;
				}
			}
			foreach (string key in Config.globalMod["ModPlayer"].Keys)
			{
				object obj = Config.globalMod["ModPlayer"][key];
				if (obj != null && RunSpecifiedMethod(key + " ModPlayer", obj, methodName, array))
				{
					result = true;
				}
			}
			return result;
		}

		public static bool RunPlayerMethodRef(string methodName, bool useItem, Player player, params object[] parameters)
		{
			if (Events.player.delegates.ContainsKey(methodName))
			{
				return RunPlayerEvent(methodName, useItem, player, parameters);
			}
			globalRunKnowledge = true;
			bool result = false;
			object[] array = new object[parameters.Length + 1];
			array[0] = player;
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i + 1] = parameters[i];
			}
			if (useItem && player.inventory[player.selectedItem].RunMethodRef(methodName, array))
			{
				if (customMethodRefReturn != null)
				{
					array = customMethodRefReturn;
				}
				result = true;
			}
			for (int j = 0; j < player.armor.Length; j++)
			{
				if (player.armor[j].RunMethodRef(methodName, array))
				{
					if (customMethodRefReturn != null)
					{
						array = customMethodRefReturn;
					}
					result = true;
				}
			}
			for (int k = 0; k < player.buffType.Length; k++)
			{
				if (RunSpecifiedMethodRef("Buff " + Main.buffName[player.buffType[k]], player.buffCode[k], methodName, array))
				{
					if (customMethodRefReturn != null)
					{
						array = customMethodRefReturn;
					}
					result = true;
				}
			}
			foreach (string key in Config.globalMod["ModPlayer"].Keys)
			{
				object obj = Config.globalMod["ModPlayer"][key];
				if (obj != null && RunSpecifiedMethodRef(key + " ModPlayer", obj, methodName, array))
				{
					if (customMethodRefReturn != null)
					{
						array = customMethodRefReturn;
					}
					result = true;
				}
			}
			globalRunKnowledge = false;
			return result;
		}

		public bool RunMethod(string methodName, params object[] parameters)
		{
			Delegate value = null;
			if (delegates != null)
			{
				delegates.TryGetValue(methodName, out value);
			}
			if ((object)value != null)
			{
				return RunEvent(value, parameters);
			}
			bool result = false;
			for (int i = 0; i < globsubclass.Length; i++)
			{
				object obj = globsubclass[i];
				string text = "";
				if (obj != null)
				{
					text = (string)defs.globalModname[i];
				}
				try
				{
					if (RunSpecifiedMethod("Global " + text + ", " + className + " " + name, obj, methodName, parameters))
					{
						result = true;
					}
				}
				catch (Exception)
				{
				}
			}
			if (RunSpecifiedMethod(className + " " + name, subclass, methodName, parameters))
			{
				result = true;
			}
			return result;
		}

		public bool RunMethodRef(string methodName, params object[] parameters)
		{
			Delegate value = null;
			delegates.TryGetValue(methodName, out value);
			if ((object)value != null)
			{
				return RunEvent(value, parameters);
			}
			bool result = false;
			for (int i = 0; i < globsubclass.Length; i++)
			{
				object obj = globsubclass[i];
				string text = "";
				if (obj != null)
				{
					text = (string)defs.globalModname[i];
				}
				if (RunSpecifiedMethodRef("Global " + text + ", " + className + " " + name, obj, methodName, parameters))
				{
					for (int j = 0; j < customMethodRefReturn.Length; j++)
					{
						parameters[j] = customMethodRefReturn[j];
					}
					result = true;
				}
			}
			if (RunSpecifiedMethodRef(className + " " + name, subclass, methodName, parameters))
			{
				result = true;
			}
			return result;
		}

		public static bool RunSpecifiedMethod(string details, object code, string methodName, params object[] parameters)
		{
			if (code == null)
			{
				return false;
			}
			try
			{
				MethodInfo method = code.GetType().GetMethod(methodName);
				if (method == null)
				{
					return false;
				}
				int num = method.GetParameters().Length;
				object[] array = new object[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = parameters[i];
				}
				customMethodReturn = method.Invoke(code, array);
			}
			catch (Exception ex)
			{
				string text = "";
				foreach (object obj in parameters)
				{
					text = text + obj.ToString() + ",";
				}
				string text2 = "Code (" + details + " " + methodName + "(" + text + ")) Error:\n" + ex;
				using (StreamWriter streamWriter = new StreamWriter(Main.SavePath + Path.DirectorySeparatorChar + "tconfig-errorlog.txt", append: true))
				{
					streamWriter.WriteLine(DateTime.Now);
					streamWriter.Write(text2);
					streamWriter.WriteLine("");
					streamWriter.WriteLine("");
				}
				if (thrownErrors <= 5)
				{
					MessageBox.Show(text2, details + ": Error");
				}
				thrownErrors++;
			}
			return true;
		}

		public static bool RunSpecifiedMethodRef(string details, object code, string methodName, params object[] parameters)
		{
			try
			{
				if (code == null)
				{
					return false;
				}
				MethodInfo method = code.GetType().GetMethod(methodName);
				if (method == null)
				{
					return false;
				}
				customMethodReturn = method.Invoke(code, parameters);
				customMethodRefReturn = parameters;
			}
			catch (Exception ex)
			{
				string text = "";
				foreach (object obj in parameters)
				{
					text = text + obj.ToString() + ",";
				}
				string text2 = "Code (" + details + " " + methodName + "(" + text + ")) Error:\n" + ex;
				using (StreamWriter streamWriter = new StreamWriter(Main.SavePath + Path.DirectorySeparatorChar + "tconfig-errorlog.txt", append: true))
				{
					streamWriter.WriteLine(DateTime.Now);
					streamWriter.Write(text2);
					streamWriter.WriteLine("");
					streamWriter.WriteLine("");
				}
				if (thrownErrors <= 5)
				{
					MessageBox.Show(text2, details + ": Error");
				}
				thrownErrors++;
			}
			return true;
		}

		public static string ParseName(string name)
		{
			name = name.Replace(' ', '_');
			name = name.Replace("'", "");
			name = name.Replace("-", "_");
			return name + "_";
		}

		public static bool SaveCustomData(Codable item, BinaryWriter writer, bool net = false)
		{
			if (net && (item.name == null || item.name == ""))
			{
				return false;
			}
			bool flag = false;
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			if (RunSpecifiedMethod(item.className + " " + item.name, item.subclass, "Save", binaryWriter))
			{
				binaryWriter.Close();
			}
			else if (item.savedata != null)
			{
				binaryWriter.Write(item.savedata);
			}
			writer.Write(memoryStream.ToArray().Length);
			writer.Write(memoryStream.ToArray());
			if (memoryStream.ToArray().Length > 0)
			{
				flag = true;
			}
			ArrayList arrayList = new ArrayList();
			if (item.className == "Item")
			{
				arrayList = Config.itemDefs.globalModname;
			}
			else if (item.className == "NPC")
			{
				arrayList = Config.npcDefs.globalModname;
			}
			else if (item.className == "Projectile")
			{
				arrayList = Config.projDefs.globalModname;
			}
			else if (item.className == "Tile")
			{
				arrayList = Config.tileDefs.globalModname;
			}
			int num = item.globsubclass.Length;
			int num2 = 0;
			memoryStream = new MemoryStream();
			binaryWriter = new BinaryWriter(memoryStream);
			ArrayList arrayList2 = new ArrayList();
			for (int i = 0; i < num; i++)
			{
				string text = (string)arrayList[i];
				if (!arrayList2.Contains(text))
				{
					arrayList2.Add(text);
					MemoryStream memoryStream2 = new MemoryStream();
					BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream2);
					try
					{
						if (!RunSpecifiedMethod("Global, " + item.className + " " + item.name, item.globsubclass[i], "Save", binaryWriter2) && item.globsavedata[i] != null)
						{
							binaryWriter2.Write(item.globsavedata[i]);
						}
					}
					catch (Exception)
					{
						continue;
					}
					if (memoryStream2.ToArray().Length > 0)
					{
						num2++;
						binaryWriter.Write(text);
						binaryWriter.Write(memoryStream2.ToArray().Length);
						binaryWriter.Write(memoryStream2.ToArray());
					}
				}
			}
			if (item.unloadedSavedata != null)
			{
				for (int j = 0; j < item.unloadedSavedata.Length; j++)
				{
					num2++;
					binaryWriter.Write(item.unloadedSavedata[j]);
				}
			}
			writer.Write(num2);
			writer.Write(memoryStream.ToArray());
			if (memoryStream.ToArray().Length > 0 || flag)
			{
				return true;
			}
			return false;
		}

		public static void LoadCustomDataNew(Codable item, BinaryReader reader, int version, int modVersion)
		{
			if (reader == null || version < 4)
			{
				return;
			}
			int num = reader.ReadInt32();
			if (num > 0)
			{
				MemoryStream memoryStream = new MemoryStream(reader.ReadBytes(num));
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				try
				{
					if (!RunSpecifiedMethod(item.className + " " + item.name, item.subclass, "Load", binaryReader, modVersion))
					{
						item.savedata = memoryStream.ToArray();
					}
				}
				catch (Exception)
				{
				}
			}
			ArrayList arrayList = new ArrayList();
			if (item.className == "Item")
			{
				arrayList = Config.itemDefs.globalModname;
			}
			else if (item.className == "NPC")
			{
				arrayList = Config.npcDefs.globalModname;
			}
			else if (item.className == "Projectile")
			{
				arrayList = Config.projDefs.globalModname;
			}
			else
			{
				if (!(item.className == "Tile"))
				{
					return;
				}
				arrayList = Config.tileDefs.globalModname;
			}
			int num2 = 0;
			int num3 = reader.ReadInt32();
			List<MemoryStream> list = new List<MemoryStream>();
			List<BinaryWriter> list2 = new List<BinaryWriter>();
			ArrayList arrayList2 = new ArrayList();
			for (int i = 0; i < num3; i++)
			{
				string text = reader.ReadString();
				int num4 = arrayList.IndexOf(text);
				int count = reader.ReadInt32();
				MemoryStream memoryStream2 = new MemoryStream(reader.ReadBytes(count));
				BinaryReader binaryReader2 = new BinaryReader(memoryStream2);
				if (!arrayList2.Contains(text))
				{
					arrayList2.Add(text);
					if (num4 > -1)
					{
						try
						{
							RunSpecifiedMethod("Global, " + item.className + " " + item.name, item.globsubclass[num4], "Load", binaryReader2, Config.loadedVersion[text]);
						}
						catch (Exception)
						{
						}
						continue;
					}
					MemoryStream memoryStream3 = new MemoryStream();
					list.Add(memoryStream3);
					list2.Add(new BinaryWriter(memoryStream3));
					list2[num2].Write(text);
					list2[num2].Write(memoryStream2.ToArray().Length);
					list2[num2].Write(memoryStream2.ToArray());
					num2++;
				}
			}
			if (num2 > 0)
			{
				item.unloadedSavedata = new byte[num2][];
				for (int j = 0; j < num2; j++)
				{
					item.unloadedSavedata[j] = list[j].ToArray();
					list[j].Close();
				}
			}
		}

		public static void LoadCustomData(Item item, BinaryReader reader, int version = 3, bool forceItemLoad = false)
		{
			if ((forceItemLoad || version < 3 || (item.netID != 0 && item.type != 0 && !string.IsNullOrEmpty(item.name))) && ((item.name != null && !(item.name == "")) || version < 2 || forceItemLoad))
			{
				LoadCustomData((Codable)item, reader, version, forceItemLoad);
			}
		}

		public static void LoadCustomData(Codable item, BinaryReader reader, int version = 3, bool forceItemLoad = false)
		{
			if ((item.name == null || item.name == "") && version >= 2 && !forceItemLoad)
			{
				return;
			}
			int num = reader.ReadInt32();
			if (num > 0)
			{
				MemoryStream memoryStream = new MemoryStream(reader.ReadBytes(num));
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				try
				{
					if (!RunSpecifiedMethod(item.className + " " + item.name, item.subclass, "Load", binaryReader, Config.GetModVersion(item)))
					{
						item.savedata = memoryStream.ToArray();
					}
				}
				catch (Exception)
				{
				}
			}
			ArrayList arrayList = new ArrayList();
			if (item.className == "Item")
			{
				arrayList = Config.itemDefs.globalModname;
			}
			else if (item.className == "NPC")
			{
				arrayList = Config.npcDefs.globalModname;
			}
			else if (item.className == "Projectile")
			{
				arrayList = Config.projDefs.globalModname;
			}
			else
			{
				if (!(item.className == "Tile"))
				{
					return;
				}
				arrayList = Config.tileDefs.globalModname;
			}
			if (version < 2)
			{
				return;
			}
			int num2 = 0;
			int num3 = reader.ReadInt32();
			List<MemoryStream> list = new List<MemoryStream>();
			List<BinaryWriter> list2 = new List<BinaryWriter>();
			ArrayList arrayList2 = new ArrayList();
			for (int i = 0; i < num3; i++)
			{
				string text = reader.ReadString();
				int num4 = arrayList.IndexOf(text);
				int count = reader.ReadInt32();
				MemoryStream memoryStream2 = new MemoryStream(reader.ReadBytes(count));
				BinaryReader binaryReader2 = new BinaryReader(memoryStream2);
				if (!arrayList2.Contains(text))
				{
					arrayList2.Add(text);
					if (num4 > -1)
					{
						try
						{
							RunSpecifiedMethod("Global, " + item.className + " " + item.name, item.globsubclass[num4], "Load", binaryReader2, Config.loadedVersion[text]);
						}
						catch (Exception)
						{
						}
						continue;
					}
					MemoryStream memoryStream3 = new MemoryStream();
					list.Add(memoryStream3);
					list2.Add(new BinaryWriter(memoryStream3));
					list2[num2].Write(text);
					list2[num2].Write(memoryStream2.ToArray().Length);
					list2[num2].Write(memoryStream2.ToArray());
					num2++;
				}
			}
			if (num2 > 0)
			{
				item.unloadedSavedata = new byte[num2][];
				for (int j = 0; j < num2; j++)
				{
					item.unloadedSavedata[j] = list[j].ToArray();
				}
			}
		}

		public float AngleTo(Vector2 Destination)
		{
			return (float)Math.Atan2(Destination.Y - Centre.Y, Destination.X - Centre.X);
		}

		public float AngleFrom(Vector2 Source)
		{
			return (float)Math.Atan2(Centre.Y - Source.Y, Centre.X - Source.X);
		}

		public float Distance(Vector2 Other)
		{
			return Vector2.Distance(Centre, Other);
		}

		public float DistanceSQ(Vector2 Other)
		{
			return Vector2.DistanceSquared(Centre, Other);
		}

		public Vector2 DirectionTo(Vector2 Destination)
		{
			return Vector2.Normalize(Destination - Centre);
		}

		public Vector2 DirectionFrom(Vector2 Source)
		{
			return Vector2.Normalize(Centre - Source);
		}

		public bool WithinRange(Vector2 Target, float MaxRange)
		{
			return Vector2.DistanceSquared(Centre, Target) <= MaxRange * MaxRange;
		}
	}
}
