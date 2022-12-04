using Application.Services.Interfaces;
using AutoMapper;
using Application.DTO;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;

        public PersonService(IMapper mapper, IPersonRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<PersonDTO>> GetAllPersons(int from, int to)
        {
            if (from < to)
            {
                return _mapper.Map<List<PersonDTO>>(await _repository.GetByRange(from - 1, to - from + 1));
            }
            else
            {
                return _mapper.Map<List<PersonDTO>>(await _repository.GetByRange(to - 1, from - to + 1)).OrderByDescending(l => l.Id).ToList();
            }
                
        }
    }
}
