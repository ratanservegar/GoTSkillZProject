using GoTSkillZ.Models.Game.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class GameRoleDTO
    {
        public GameRoleDTO()
        {
            Id = GameTypeId = 0;
            RoleName = "";
        }

        public GameRoleDTO(GameRoles gameRole)
        {
            Id = gameRole.Id;
            RoleName = gameRole.RoleName;
            GameTypeId = gameRole.GameTypeId;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public int GameTypeId { get; set; }
    }
}