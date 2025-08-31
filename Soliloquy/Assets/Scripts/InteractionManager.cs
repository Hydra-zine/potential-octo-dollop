using System;

public static class InteractionManager
{
    public static event Action OnInteractionEnd;

    private static bool isInteracting = false;

    public static bool IsInteracting => isInteracting;

    public static void StartInteraction()
    {
        isInteracting = true;
    }

    public static void EndInteraction()
    {
        isInteracting = false;
        OnInteractionEnd?.Invoke();
    }
}
