using PoeHUD.Poe.Elements;
using System.Collections;
using System.Collections.Generic;
using PoeHUD.Poe.FilesInMemory;
using PoeHUD.Controllers;
using System;
using System.Linq;
using PoeHUD.Framework;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics;

namespace PoeHUD.Poe.RemoteMemoryObjects
{
	public class IngameUIElements : RemoteMemoryObject
	{
		public Element QuestTracker => ReadObjectAt<Element>(0xC60);
		public Element OpenLeftPanel => ReadObjectAt<Element>(0xCB0);
		public Element OpenRightPanel => ReadObjectAt<Element>(0xCB8);
		public InventoryElement InventoryPanel => ReadObjectAt<InventoryElement>(0xCD8);
		public Element TreePanel => ReadObjectAt<Element>(0xD08);
		public Element AtlasPanel => ReadObjectAt<Element>(0xD10);
		public Map Map => ReadObjectAt<Map>(0xD68);
		public IEnumerable<ItemsOnGroundLabelElement> ItemsOnGroundLabels
		{
			get
			{
				var itemsOnGroundLabelRoot = ReadObjectAt<ItemsOnGroundLabelElement>(0xD70);
				return itemsOnGroundLabelRoot.Children;
			}
		}
		public Element GemLvlUpPanel => ReadObjectAt<Element>(0xFD8);
		public ItemOnGroundTooltip ItemOnGroundTooltip => ReadObjectAt<ItemOnGroundTooltip>(0x1048);
	}
}

