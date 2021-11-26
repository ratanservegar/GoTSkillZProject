using GoTSkillZ.Models.Game.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class GameTypeDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string GameName { get; set; }

        public GameTypeDTO()
        {
            Id = 0;
            GameName = "";
        }

        public GameTypeDTO(GameType gameType)
        {
            Id = gameType.Id;
            GameName = gameType.GameName;
        }
    }
}
