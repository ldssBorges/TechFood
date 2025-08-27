using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO;
using TechFood.Common.DTO.Enums;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Interfaces.Gateway;

namespace TechFood.Application.Gateway;

public class PreparationGateway : IPreparationGateway
{
    private readonly IPreparationDataSource _preparationDataSource;
    private readonly IUnitOfWorkDataSource _unitOfWorkDataSource;

    public PreparationGateway(IPreparationDataSource preparationDataSource,
        IUnitOfWorkDataSource unitOfWorkDataSource)
    {
        _unitOfWorkDataSource = unitOfWorkDataSource;
        _preparationDataSource = preparationDataSource;
    }

    public async Task<Guid> AddAsync(Preparation preparation)
    {
        var preparationDto = new PreparationDTO
        {
            Id = preparation.Id,
            OrderId = preparation.OrderId,
            Number = preparation.Number,
            CreatedAt = preparation.CreatedAt,
            FinishedAt = preparation.FinishedAt,
            IsDeleted = preparation.IsDeleted,
            StartedAt = preparation.StartedAt,
            Status = (PreparationStatusTypeDTO)preparation.Status
        };


        var result = await _preparationDataSource.AddAsync(preparationDto);

        await _unitOfWorkDataSource.CommitAsync();

        return result;
    }

    public async Task<IEnumerable<Preparation>> GetAllAsync()
    {
        var preparationDTO = await _preparationDataSource.GetAllAsync();

        return preparationDTO.Select(
            p => new Preparation(
                p.Id,
                p.OrderId,
                p.Number,
                p.CreatedAt,
                p.StartedAt,
                p.FinishedAt,
                (PreparationStatusType)p.Status
            )
        ).ToList();
    }

    public async Task<Preparation?> GetByIdAsync(Guid id)
    {
        var preparationDTO = await _preparationDataSource.GetByIdAsync(id);

        return preparationDTO is not null ? new Preparation(
            preparationDTO.Id,
            preparationDTO.OrderId,
            preparationDTO.Number,
            preparationDTO.CreatedAt,
            preparationDTO.StartedAt,
            preparationDTO.FinishedAt,
            (PreparationStatusType)preparationDTO.Status
        ) : null;
    }

    public async Task<Preparation?> GetByOrderIdAsync(Guid orderId)
    {
        var preparationDTO = await _preparationDataSource.GetByOrderIdAsync(orderId);

        return preparationDTO is not null ? new Preparation(
            preparationDTO.Id,
            preparationDTO.OrderId,
            preparationDTO.Number,
            preparationDTO.CreatedAt,
            preparationDTO.StartedAt,
            preparationDTO.FinishedAt,
            (PreparationStatusType)preparationDTO.Status
            ) : null;
    }

    public async Task UpdateAsync(Preparation preparation)
    {
        var request = new PreparationDTO
        {
            Id = preparation.Id,
            OrderId = preparation.OrderId,
            Number = preparation.Number,
            CreatedAt = preparation.CreatedAt,
            FinishedAt = preparation.FinishedAt,
            IsDeleted = preparation.IsDeleted,
            StartedAt = preparation.StartedAt,
            Status = (PreparationStatusTypeDTO)preparation.Status
        };

        await _preparationDataSource.UpdateAsync(request);

        await _unitOfWorkDataSource.CommitAsync();
    }
}
