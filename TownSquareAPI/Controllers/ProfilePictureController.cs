using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs.ProfilePicture;
using TownSquareAPI.Models;
using TownSquareAPI.Services;


namespace TownSquareAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfilePictureController : ControllerBase
{
    
}
