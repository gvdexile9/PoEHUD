using System;
using System.Collections.Generic;
using PoeHUD.Poe.Elements;
using PoeHUD.Poe.RemoteMemoryObjects;
using PoeHUD.Controllers;
using PoeHUD.Poe.FilesInMemory;
using PoeHUD.Models;
using PoeHUD.Poe.Components;
using PoeHUD.Models.Attributes;

namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public class ServerData : RemoteMemoryObject
    {
        #region PlayerData
        public bool IsInGame => GameController.UseGameStateController ? GameStateController.IsInGameState : NetworkState == NetworkStateE.Connected;
        public NetworkStateE NetworkState => (NetworkStateE)M.ReadByte(Address + 0x4F50);
        public int Latency => M.ReadInt(Address + 0x4FD0);
        public List<ushort> SkillBarIds
        {
            get
            {
                var result = new List<ushort>();

                var readAddr = Address + 0x5198;
                for (int i = 0; i < 8; i++)
                {
                    result.Add(M.ReadUShort(readAddr));
                    readAddr += 2;
                }
                return result;
            }
        }

        public enum NetworkStateE : byte
        {
            None,
            Disconnected,
            Connecting,
            Connected
        }
		#endregion
	}
}