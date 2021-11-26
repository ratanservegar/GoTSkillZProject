using GoTSkillZ.Models.CSGO.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class CSGOVideoConfigurationDTO
    {
        public CSGOVideoConfigurationDTO()
        {
            Id = 0;
            UserId = 0;
            ColorMode = "";
            Brightness = "";
            AspectRatio = "";
            Resolution = "";
            DisplayMode = "";
            LaptopPowerSavings = "";
            GlobalShadowQuality = "";
            ModelTextureDetail = "";
            EffectDetail = "";
            ShaderDetail = "";
            MultiCoreRendering = "";
            MultisamplingAntiAliasingMode = "";
            FXXAAAnti_Aliasing = "";
            TextureFilteringMode = "";
            WaitForVerticalSync = "";
            MotionBlur = "";
            TripleMonitorMode = "";
            GameView = "";
            CreatedDate = DateTime.Now;
            ModifiedDate = null;
        }

        public CSGOVideoConfigurationDTO(CSGOVideoConfiguration csgoVideoConfiguration)
        {
            Id = csgoVideoConfiguration.Id;
            UserId = csgoVideoConfiguration.UserId;
            ColorMode = csgoVideoConfiguration.ColorMode;
            Brightness = csgoVideoConfiguration.Brightness;
            AspectRatio = csgoVideoConfiguration.AspectRatio;
            Resolution = csgoVideoConfiguration.Resolution;
            DisplayMode = csgoVideoConfiguration.DisplayMode;
            LaptopPowerSavings = csgoVideoConfiguration.LaptopPowerSavings;
            GlobalShadowQuality = csgoVideoConfiguration.GlobalShadowQuality;
            ModelTextureDetail = csgoVideoConfiguration.ModelTextureDetail;
            EffectDetail = csgoVideoConfiguration.EffectDetail;
            ShaderDetail = csgoVideoConfiguration.ShaderDetail;
            MultiCoreRendering = csgoVideoConfiguration.MultiCoreRendering;
            MultisamplingAntiAliasingMode = csgoVideoConfiguration.MultisamplingAntiAliasingMode;
            FXXAAAnti_Aliasing = csgoVideoConfiguration.FXXAAAnti_Aliasing;
            TextureFilteringMode = csgoVideoConfiguration.TextureFilteringMode;
            WaitForVerticalSync = csgoVideoConfiguration.WaitForVerticalSync;
            MotionBlur = csgoVideoConfiguration.MotionBlur;
            TripleMonitorMode = csgoVideoConfiguration.TripleMonitorMode;
            GameView = csgoVideoConfiguration.GameView;
            CreatedDate = csgoVideoConfiguration.CreatedDate;
            ModifiedDate = csgoVideoConfiguration.ModifiedDate;
        }


        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string ColorMode { get; set; }

        [DataMember]
        public string Brightness { get; set; }

        [DataMember]
        public string AspectRatio { get; set; }

        [DataMember]
        public string Resolution { get; set; }

        [DataMember]
        public string DisplayMode { get; set; }

        [DataMember]
        public string LaptopPowerSavings { get; set; }

        [DataMember]
        public string GlobalShadowQuality { get; set; }

        [DataMember]
        public string ModelTextureDetail { get; set; }

        [DataMember]
        public string EffectDetail { get; set; }

        [DataMember]
        public string ShaderDetail { get; set; }

        [DataMember]
        public string MultiCoreRendering { get; set; }

        [DataMember]
        public string MultisamplingAntiAliasingMode { get; set; }

        [DataMember]
        public string FXXAAAnti_Aliasing { get; set; }

        [DataMember]
        public string TextureFilteringMode { get; set; }

        [DataMember]
        public string WaitForVerticalSync { get; set; }

        [DataMember]
        public string MotionBlur { get; set; }

        [DataMember]
        public string TripleMonitorMode { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime? ModifiedDate { get; set; }

        [DataMember]
        public string GameView { get; set; }
    }
}