using ConfigAdapter;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Kaomi.Core.Model
{
    public class ConfigFileManager : KaomiPlugin
    {
        private Config config;

        public override void Initialize()
        {
            var asm = Assembly.GetExecutingAssembly().GetName().Name;
            config = Config.From($"{asm}.xml");
        }

        public string Read(string key) => config.Read(key);

        public void Write(string key, string value) => config.Write(key, value);
    }
}
