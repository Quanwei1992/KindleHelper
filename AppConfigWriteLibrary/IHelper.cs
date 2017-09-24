using System;
namespace AppConfigWriteLibrary
{
    interface IHelper
    {
        string ReadConfig(string key);
        void SaveConfig(string Key, string Value);
    }
}
