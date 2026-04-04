namespace Game.ManagementSystems.LookUpTableManagement
{
    public interface ILookUpTableConfigProvider
    {
        public string GetLookUpTablesFolderPath();
        public string GetLookUpTablePath(LookUpTableGroup group);
    }
}