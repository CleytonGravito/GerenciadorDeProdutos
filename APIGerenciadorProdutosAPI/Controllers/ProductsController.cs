﻿using GerenciadorProdutosAPI.Application.Interfaces;
using GerenciadorProdutosAPI.Application.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema_escolar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<ProductViewModel> _productValidator;

        public ProductsController(IProductService productService,
            IValidator<ProductViewModel> productValidator)
        {
            _productService = productService;
            _productValidator = productValidator;
        }
        /// <summary>
        /// Buscar todos os produtos
        /// </summary>
        /// <remarks>
        /// Busca todos os produtos cadastrados no Banco de Dados
        /// </remarks>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAll()
        {
            try
            {
                var products = await _productService.GetProducts();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Buscar produto por Id
        /// </summary>
        /// <remarks>
        /// Ao enviar o Id do produto desejado o sistema retornará o produto.         
        /// </remarks>        /// 
        /// <param name="id">"Id do item"</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<ProductViewModel> BuscarPorId(int id)
        {
            try
            {
                if (id <= 0) new Exception("Erro"); //BadRequest("O número do produto não pode ser igual a 0 ou negativo");
                ProductViewModel produto = _productService.GetById(id);
                return Ok(produto);
            }
            catch (Exception erro)
            {

                throw new Exception(erro.Message);
            }
        }

        /// <summary>
        /// Adicionar um produto
        /// </summary>
        /// <remarks>
        /// Ao adicionar as informações, um novo produto será adicionado ao Banco de Dados. 
        /// 
        /// Obs: Não é necessário preencher o campo "id", pois ele é gerado automaticamente.
        /// 
        /// Exemplo:
        ///
        ///     {
        ///         "name": "Borracha",
        ///         "description": "Borracha branca",
        ///         "price": 5.20
        ///     }
        /// </remarks>
        /// <param name="productDTO">"Produto a ser adicionado"</param>      
        /// <returns></returns>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult<ProductViewModel> Adicionar([FromBody] ProductViewModel productDTO)
        {
            try
            {
                var validationResult = _productValidator.Validate(productDTO);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var produtReturn = _productService.Add(productDTO);
                return Ok(produtReturn);
            }
            catch (Exception erro)
            {
                return BadRequest(new Exception("Não foi possivel adicionar o produto: " + erro.Message));
            }
        }

        /// <summary>
        /// Editar um produto
        /// </summary>
        /// <remarks>
        /// Ao adicionar as informações, será retornado um produto caso ele esteja no Banco de Dados. 
        /// 
        /// Exemplo:
        ///
        ///     {
        ///         "id": 15,
        ///         "name": "Caneta",
        ///         "description": "Caneta esferográfica azul",
        ///         "price": 2.50
        ///     }
        /// </remarks>
        /// <param name="productDTO">"Produto a ser editado"</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Authorize(Roles = "manager")]
        public ActionResult<ProductViewModel> Editar([FromBody] ProductViewModel productDTO)
        {
            try
            {
                var validationResult = _productValidator.Validate(productDTO);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                if (_productService.GetById(productDTO.Id) == null)
                {
                    throw new Exception("Produto não existe no Banco de Dados");
                }

                _productService.Update(productDTO);
                return Ok(productDTO);
            }
            catch (Exception erro)
            {
                return BadRequest(new Exception("Não foi possivel realizar o update o produto: " + erro.Message));
            }
        }
        /// <summary>
        /// Deletar um produto
        /// </summary>
        /// <remarks>
        /// Ao adicionar as informações, um novo produto será adicionado ao Banco de Dados. 
        /// </remarks>
        /// <param name="id">"Id do produto a ser deletado"</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult<ProductViewModel> Deletar(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentNullException();

                _productService.Delete(id);
                return Ok("Produto Deletado");
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
    }
}