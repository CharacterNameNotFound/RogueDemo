using GameWideSystems.UIManagement.Screen;

namespace GameWideSystems.UIManagement.UIManagerRequests
{
    public static class UIRequestBuilder
    {

        public static UIRequest OpenScreenRequest(
            this UIManager uiManager, 
            IUIScreenBuilder screenBuilder, 
            IScreenParams screenParams,
            out ScreenHolder openedScreen)
        {
            UIRequest uiRequest = UIRequest.New;
            if (screenBuilder.ScreenType == ScreenType.Screen)
            {
                uiRequest.CloseAll();
            }
            else if (uiManager.PreloadedScreens.Count > 0)
            {
                uiRequest.UnloadTopScreen();
            }
            

            uiRequest.OpenScreen(screenBuilder, screenParams, out openedScreen);
            return uiRequest;
        }

        public static UIRequest SwapTopScreenRequest( 
            IUIScreenBuilder screenBuilder, 
            IScreenParams screenParams,
            out ScreenHolder openedScreen)
        {
            UIRequest uiRequest = UIRequest.New;
            if (screenBuilder.ScreenType == ScreenType.Dialog)
            {
                uiRequest.CloseAll();
            }
            else
            {
                uiRequest.CloseTop();
            }

            uiRequest.OpenScreen(screenBuilder, screenParams, out openedScreen);
            return uiRequest;
        }

        public static UIRequest CloseTopRequest()
        {
            return UIRequest.New.CloseTop().LoadTopScreen();
        }

        public static UIRequest CloseAllRequest(ScreenType screenType = ScreenType.Screen)
        {
            return UIRequest.New.CloseAll(screenType);
        }
        
    }
}