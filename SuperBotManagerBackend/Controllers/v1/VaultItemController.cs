using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperBotManagerBackend.DTOs;
using SuperBotManagerBackend.Services;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.Services;
using System.Security.Claims;


namespace SuperBotManagerBackend.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VaultItemController : ControllerBase
    {
        private readonly IAppUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        //private readonly IScheduleService scheduleService;

        public VaultItemController(IAppUnitOfWork uow, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<VaultItemDTO>> Get()
        {
            var userIdStr = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userId = int.Parse(userIdStr);
            var definitions = uow.ActionDefinitionRepository.GetAll().ToList();

            //uow.VaultItemRepository.RemoveUserVaultItems(userId);
            //await uow.SaveChangesAsync();

            await uow.VaultItemRepository.CreateMissingVaultItems(userId, definitions);
            await uow.SaveChangesAsync();   

            var res = uow.VaultItemRepository.GetAll().Where(a => a.OwnerId == userId).ToList();
            var dtos = mapper.Map<IEnumerable<VaultItemDTO>>(res);
            return dtos;
        }

        [HttpGet("{id}")]
        public async Task<VaultItemDTO> Get(int id)
        {
            var vaultItem = await uow.VaultItemRepository.GetById(id);
            var dto = mapper.Map<VaultItemDTO>(vaultItem);

            return dto;
        }


        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] VaultItemUpdateDTO dto)
        {
            var vaultItem = await uow.VaultItemRepository.GetById(id);
            mapper.Map(dto, vaultItem);
            if(vaultItem == null)
                throw HttpUtilsService.BadRequest("Bad VaultItemDTO format");
            vaultItem.OwnerId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if(dto.PlainValue != null)
            {
                if(dto.PlainValue.Length == 0)
                {
                    vaultItem.SecretId = null;
                }
                else
                {
                    var secret = new Secret()
                    {
                        Id = Guid.NewGuid(),
                        DecryptedSecretValue = dto.PlainValue,
                    };
                    await uow.SecretRepository.Create(secret);
                    vaultItem.SecretId = secret.Id;
                }
            }

            await uow.VaultItemRepository.Update(vaultItem);
            await uow.SaveChangesAsync();
        }

    }
}
