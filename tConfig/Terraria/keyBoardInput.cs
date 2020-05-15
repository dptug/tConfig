using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Terraria
{
	public class keyBoardInput
	{
		public class inKey : IMessageFilter
		{
			public bool PreFilterMessage(ref Message m)
			{
				if (m.Msg == 258)
				{
					char obj = (char)(int)m.WParam;
					if (keyBoardInput.newKeyEvent != null)
					{
						keyBoardInput.newKeyEvent(obj);
					}
				}
				else if (m.Msg == 256)
				{
					IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf((object)m));
					Marshal.StructureToPtr((object)m, intPtr, fDeleteOld: true);
					TranslateMessage(intPtr);
				}
				return false;
			}
		}

		public static event Action<char> newKeyEvent;

		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		public static extern bool TranslateMessage(IntPtr message);

		static keyBoardInput()
		{
			Application.AddMessageFilter(new inKey());
		}

		public static void ResetSubscriptions()
		{
			keyBoardInput.newKeyEvent = null;
		}
	}
}
