namespace ApplicationDTO.Validator
{
    public interface IValidator<Entity>
    {
        bool Validate(Entity entity);

        string GetErrors();
    }
}
