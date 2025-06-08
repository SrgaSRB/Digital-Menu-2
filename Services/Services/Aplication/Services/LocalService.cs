using Microsoft.AspNetCore.Http.HttpResults;
using Services.Aplication.DTOs.Local;
using Services.Aplication.DTOs.UserMenuDTOs;
using Services.Aplication.Exceptions;
using Services.Aplication.Interfaces.Repositories;
using Services.Aplication.Interfaces.Services;

namespace Services.Aplication.Services
{
    public class LocalService : ILocalService
    {
        private readonly ILocalRepository _locRepo;

        public LocalService(ILocalRepository localRepository) => _locRepo = localRepository;

        public async Task<GetLocalHeaderDto> GetHeaderAsync(Guid localId)
            => await _locRepo.GetHeaderAsync(localId)
               ?? throw new NotFoundException($"Local {localId} not found");

        public async Task<GetLocalInfoDto> GetInfoAsync(Guid localId)
        {
            var local = await _locRepo.GetAsync(localId)
                        ?? throw new NotFoundException($"Local {localId} not found");

            return new GetLocalInfoDto
            {
                Id = local.Id,
                ImageUrl = local.LogoUrl,
                Name = local.Name,
                Subscription = local.Subscription?.EndDate
            };
        }

        public async Task UpdateAsync(UpdateLocalDto dto, Guid localId)
        {
            var local = await _locRepo.GetAsync(localId)
                        ?? throw new NotFoundException($"Local {localId} not found");

            local.Name = dto.Name;
            local.LogoUrl = !string.IsNullOrWhiteSpace(dto.ImageUrl) ? dto.ImageUrl : null!;
            local.HaveLogo = local.LogoUrl != null;

            await _locRepo.SaveAsync();
        }

    }
}
