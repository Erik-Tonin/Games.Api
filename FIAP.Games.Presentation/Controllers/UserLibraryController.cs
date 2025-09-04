using FIAP.Games.Application.Contracts.IApplicationService;
using FIAP.Games.Application.DTOs;
using FIAP.Games.Application.Implementations;
using FIAP.Games.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Games.Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserLibraryController : ApiController
    {
        private readonly IUserLibraryApplicationService _userLibraryApplicationService;

        public UserLibraryController(IUserLibraryApplicationService userLibraryApplicationService)
        {
            _userLibraryApplicationService = userLibraryApplicationService;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost("AddToLibrary")]
        public async Task<ValidationResultDTO<UserLibrary>> AddToLibrary([FromForm] UserLibraryDTO userLibraryDTO)
        {
            ValidationResultDTO<UserLibrary> user = await _userLibraryApplicationService.AddToLibrary(userLibraryDTO);

            return user;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetByUserProfileId")]
        public async Task<IEnumerable<UserLibraryDTO>> GetByUserProfileId(Guid userProfileId)
        {
            return await _userLibraryApplicationService.GetByUserProfileId(userProfileId);
        }
    }
}
