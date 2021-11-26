using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class LoadingMessagesDTO
    {
        [DataMember]
        public string LoadingMessage { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public LoadingMessagesDTO()
        {
            LoadingMessage = Author = "";
            Active = false;
        }


    }
}
