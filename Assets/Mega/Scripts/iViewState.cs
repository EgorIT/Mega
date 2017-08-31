namespace Assets.Mega.Scripts {
    public interface iViewState {
        void EndState();
        void StartState();
        ViewStates GetViewStates();
        TypeCameraOnState GetTypeCameraOnState();
    }
}