using System.Collections.Generic;
using GameWideSystems.UIManagement.Screen.StateMachineGeneric;

namespace Game.GameMode.Login.UI.Screens
{
    public class LoginScreenContext : IUISMContext
    {
        public LogInScreenController LogInScreenController; 
        public List<string> ProfileList;

        public LoginScreenContext(LogInScreenController logInScreenController, List<string> profileList)
        {
            LogInScreenController = logInScreenController;
            ProfileList = profileList;
        }
    }
}