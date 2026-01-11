using System;
using System.IO;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Utils.UtilityTypes.Result;

namespace Utils.DiscInteraction
{
    public static class DiscWriting
    {
        public static async UniTask<ProcedureResult> ConvertAndWriteJson<T>(T value, string path, CancellationToken cancellationToken)
        {
            string folder = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            
            try
            {
                //string json = JsonUtility.ToJson(value, true);
                
                var jsonSerializerSettings = new JsonSerializerSettings() { 
                    TypeNameHandling = TypeNameHandling.All,
                    Formatting = Formatting.Indented
                };
                var json = JsonConvert.SerializeObject(value, jsonSerializerSettings);
                
                await File.WriteAllTextAsync(path, json, Encoding.Unicode, cancellationToken).AsUniTask();

                return ProcedureResultBuilder.Success();
            }
            catch (Exception exception)
            {
                return ProcedureResultBuilder.Failure(exception);
            }
        }
        
    }
}