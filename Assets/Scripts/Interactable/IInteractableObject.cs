public interface IInteractableObject
{
    public string Message { get; }
    
    public bool TryInteract();
}