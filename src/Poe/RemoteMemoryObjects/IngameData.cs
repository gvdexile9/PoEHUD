using System;
using System.Text;
using PoeHUD.Controllers;
using PoeHUD.Poe.FilesInMemory;
using System.Collections.Generic;
using PoeHUD.Models.Enums;

namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public class IngameData : RemoteMemoryObject
    {
        public AreaTemplate CurrentArea => ReadObject<AreaTemplate>(Address + 0x28);
        public WorldArea CurrentWorldArea => GameController.Instance.Files.WorldAreas.GetByAddress(M.ReadLong(Address + 0x28));
        public int CurrentAreaLevel => (int)M.ReadByte(Address + 0x40);
        public uint CurrentAreaHash => M.ReadUInt(Address + 0x68);

        public Entity LocalPlayer => GameController.Instance.Cache.Enable && GameController.Instance.Cache.LocalPlayer != null
            ? GameController.Instance.Cache.LocalPlayer
            : GameController.Instance.Cache.Enable ? GameController.Instance.Cache.LocalPlayer = LocalPlayerReal : LocalPlayerReal;
        private Entity LocalPlayerReal => ReadObject<Entity>(Address + 0x380);
        public EntityList EntityList => GetObject<EntityList>(Address + 0x430);

        private long LabDataPtr => M.ReadLong(Address + 0x70);
        public LabyrinthData LabyrinthData => LabDataPtr == 0 ? null : GetObject<LabyrinthData>(LabDataPtr);


        public Dictionary<GameStat, int> MapStats
        {
            get
            {
                var statPtrStart = M.ReadLong(Address + 0x3A0);
                var statPtrEnd = M.ReadLong(Address + 0x3A8);

                int key = 0;
                int value = 0;
                int total_stats = (int)(statPtrEnd - statPtrStart);
                var bytes = M.ReadBytes(statPtrStart, total_stats);
                var result = new Dictionary<GameStat, int>(total_stats / 8);
                for (int i = 0; i < bytes.Length; i += 8)
                {
                    key = BitConverter.ToInt32(bytes, i);
                    value = BitConverter.ToInt32(bytes, i + 0x04);
                    result[(GameStat)key] = value;
                }
                return result;
            }
        }
    }
}