using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;


namespace WS.Auto
{
    /// <summary>
    /// _____命名后缀
    /// 
    /// 声音
    /// _AudFre  //高出现频率声音,  比如枪声之类的
    /// _AudBb   //背景声音 或者 长声音
    /// _AudNor  //普通声音(除上面两种之外的声音)
    ///
    /// 模型
    /// _ModS  //带蒙皮的模型
    /// _Mod0  //不带蒙皮的模型
    ///
    /// 纹理
    /// _TexAM  //Alpha+Mipmap 
    /// _Tex0M  //只Mipmap
    /// _TexA0  //只Alpha
    /// _Tex00  //无Alpha 无Mipmap
    /// 
    /// _Tex2U  //2D图片 UI 
    /// _Tex2S  //2D图片 非UI
    /// 
    /// </summary>
    public class CustomAssetPostprocessor : AssetPostprocessor
    {
        //作用范围
        private const string _targetPathStr = ""; //"/_WS/";

        //预制路径
        private const string _audioPathStr = "Assets/_WS_Auto_/Editor/AssetImporter/Audio/AudioImporter_Aud";
        private const string _modelPathStr = "Assets/_WS_Auto_/Editor/AssetImporter/FBX/FBXImporter_Mod";
        private const string _texturePathStr = "Assets/_WS_Auto_/Editor/AssetImporter/Texture/TextureImporter_Tex";

        private const string _audioShortStr = "_Aud";
        private const string _modelShortStr = "_Mod";
        private const string _textureShortStr = "_Tex";


        #region 声音

        public void OnPreprocessAudio()
        {
            Debug.Log("OnPreprocessAudio=" + this.assetPath);
            if (this.assetImporter.assetPath.Contains(_targetPathStr))
            {
                if (this.assetImporter.assetPath.Contains(_audioShortStr + "Fre"))
                {
                    ApplyPreset(assetImporter, _audioPathStr + "Fre");
                }

                if (this.assetImporter.assetPath.Contains(_audioShortStr + "Nor"))
                {
                    ApplyPreset(assetImporter, _audioPathStr + "Nor");
                }

                if (this.assetImporter.assetPath.Contains(_audioShortStr + "Bb"))
                {
                    ApplyPreset(assetImporter, _audioPathStr + "Bb");
                }
            }
        }

        public void OnPostprocessAudio(AudioClip clip)
        {
            Debug.Log("OnPostprocessAudio=" + this.assetPath);
        }

        #endregion


        #region 模型

        //模型导入之前调用
        public void OnPreprocessModel()
        {
            Debug.Log("OnPreprocessModel=" + this.assetPath);

            if (this.assetImporter.assetPath.Contains(_targetPathStr))
            {
                if (this.assetImporter.assetPath.Contains(_modelShortStr + "S"))
                {
                    ApplyPreset(assetImporter, _modelPathStr + "S");
                }

                if (this.assetImporter.assetPath.Contains(_modelShortStr + "0"))
                {
                    ApplyPreset(assetImporter, _modelPathStr + "0");
                }
            }
        }

        public void OnPostprocessModel(GameObject go)
        {
            Debug.Log("OnPostprocessModel=" + go.name);
        }

        #endregion


        #region 图片

        //纹理导入之前调用，针对入到的纹理进行设置
        public void OnPreprocessTexture()
        {
            Debug.Log("OnPreProcessTexture=" + this.assetPath);

            if (this.assetImporter.assetPath.Contains(_targetPathStr))
            {
                if (this.assetImporter.assetPath.Contains(_textureShortStr + "AM"))
                {
                    ApplyPreset(assetImporter, _texturePathStr + "AM" + SetMaxSize(this.assetImporter.assetPath));
                    return;
                }

                if (this.assetImporter.assetPath.Contains(_textureShortStr + "0M"))
                {
                    ApplyPreset(assetImporter, _texturePathStr + "0M" + SetMaxSize(this.assetImporter.assetPath));
                    return;
                }

                if (this.assetImporter.assetPath.Contains(_textureShortStr + "A0"))
                {
                    ApplyPreset(assetImporter, _texturePathStr + "A0" + SetMaxSize(this.assetImporter.assetPath));
                    return;
                }

                if (this.assetImporter.assetPath.Contains(_textureShortStr + "00"))
                {
                    ApplyPreset(assetImporter, _texturePathStr + "00" + SetMaxSize(this.assetImporter.assetPath));
                    return;
                }
            }
        }

        private string SetMaxSize(string path)
        {
            if (path.Contains("_64_"))
            {
                return "_64";
            }

            if (path.Contains("_128_"))
            {
                return "_128";
            }

            if (path.Contains("_256_"))
            {
                return "_256";
            }

            if (path.Contains("_512_"))
            {
                return "_512";
            }

            return "";
        }


        private void TempTextureSettings()
        {
            var temp = this.assetImporter as TextureImporter;
            //temp.SetPlatformTextureSettings(,);
            if (!(temp is null))
            {
                temp.SetPlatformTextureSettings("Android", 1024,
                    TextureImporterFormat.ASTC_6x6);
                temp.SetPlatformTextureSettings("iPhone", 1024,
                    TextureImporterFormat.ASTC_6x6);
            }
        }


        public void OnPostprocessTexture(Texture2D tex)
        {
            Debug.Log("OnPostProcessTexture=" + this.assetPath);

            if (tex.name.EndsWith("sprite"))
            {
                // tex.
            }
        }

        #endregion


        //所有的资源的导入，删除，移动，都会调用此方法，注意，这个方法是static的
        public static void OnPostprocessAllAssets(string[] importedAsset, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            Debug.Log("OnPostprocessAllAssets");
            foreach (string str in importedAsset)
            {
                Debug.Log("importedAsset = " + str);
            }

            foreach (string str in deletedAssets)
            {
                Debug.Log("deletedAssets = " + str);
            }

            foreach (string str in movedAssets)
            {
                Debug.Log("movedAssets = " + str);
            }

            foreach (string str in movedFromAssetPaths)
            {
                Debug.Log("movedFromAssetPaths = " + str);
            }
        }


        private void ApplyPreset(AssetImporter importer, string presetPath)
        {
            var preset = AssetDatabase.LoadAssetAtPath<Preset>(presetPath + ".preset");
            if (preset == null)
                Debug.LogError("Unable to find required preset at path " + presetPath);
            else
                preset.ApplyTo(importer);
        }
    }
}