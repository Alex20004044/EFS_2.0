using Sirenix.OdinInspector;

namespace MSFD.Data
{
    public class DebugDataDriver : InitBehaviour
    {
#if UNITY_EDITOR
        IDataDriver dataDriver;
        [Inject]
        public void Init(IDataDriver dataDriver)
        {
            CheckInit();
            this.dataDriver = dataDriver;
        }

        [Button]
        void SetIntData(string path, int value)
        {
            dataDriver.SetData(path, value);
        }
        [Button]
        void SetFloatData(string path, float value)
        {
            dataDriver.SetData(path, value);
        }        
        [Button]
        void SetBoolData(string path, bool value)
        {
            dataDriver.SetData(path, value);
        }
        [Button]
        void SetArrayStringData(string path, string[] value)
        {
            dataDriver.SetData(path, value);
        }
        [Button]
        void SetStringData(string path, string value)
        {
            dataDriver.SetData(path, value);
        }
#endif
    }
}