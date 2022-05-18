using AutoMapper;
using Challenge_Sbi.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge_Sbi.Controllers
{
    /*
     * 
     * 
     * Entiendo que el mediador no va acá, lo dejo para finalizarlo rápido.....
     *
     *    
    */

    public class GetPostsQuery : IRequest<Salida>
    {
        public int Id { get; set; }
    }

    public class GetPostsQueryHandlers : IRequestHandler<GetPostsQuery, Salida>
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMapper _mapper;

        public GetPostsQueryHandlers(IHttpClientFactory clientFactory, IMapper mapper)
        {
            _clientFactory = clientFactory;
            _mapper = mapper;
        }

        public async Task<Salida> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var client = _clientFactory.CreateClient();
            var response = client.GetAsync("https://jsonplaceholder.typicode.com/posts").Result;
            var jsonString = response.Content.ReadAsStringAsync().Result;
            List<ServerPost> posts = JsonSerializer.Deserialize<List<ServerPost>>(jsonString);

            var choosen = posts.FirstOrDefault(x => x.id == request.Id);
            return _mapper.Map<Salida>(choosen);
        }
    }
    /*
     * 
     * 
     * Esto si .... 
     *
     *
     *    
    */

    [ApiController]
    [Route("[controller]")]
    public class SbiController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SbiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public ActionResult<Salida> Get(int id)
        {
            var result = _mediator.Send(new GetPostsQuery{ Id = id });
            return Ok(result);
        }
    }
}
