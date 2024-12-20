﻿using GerenciadorProdutosAPI.Application.Authentication;
using GerenciadorProdutosAPI.Application.Interfaces;
using GerenciadorProdutosAPI.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APIGerenciadorProdutosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IUserMktService _userMktService;
        private readonly IValidator<UserMkt> _userValidator;

        public AuthenticationController(IUserMktService userMktService, IValidator<UserMkt> userValidator)
        {
            _userMktService = userMktService;
            _userValidator = userValidator;
        }

        [HttpPost]
        [Route("Authentication")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserMkt userMktAutentication)
        {
            // Validation of the input "userMktAuthentication"
            var validationResult = _userValidator.Validate(userMktAutentication);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.RuleSetsExecuted);
            }

            // Check existence in the Database
            var user = await _userMktService.GetEmployeeByIdAsync(userMktAutentication.Id);

            if (user == null) return NotFound("Usuário não existe!");

            if (user.Username != userMktAutentication.Username || user.Password != userMktAutentication.Password)
            {
                return BadRequest("Usuário ou senha inválidos!");
            }

            // Generate the access token and return
            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }    
    }
}
