using Application.DTO;


namespace Application.Services.Interfaces
{
    public interface IPersonService
    {
        public Task<List<PersonDTO>> GetAllPersons(int from, int to);
    }
}
