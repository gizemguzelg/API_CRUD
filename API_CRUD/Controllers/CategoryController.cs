using API_CRUD.Infrastructure.Repositories.Interfaces;
using API_CRUD.Model.Abstract;
using API_CRUD.Models.DTOs;
using API_CRUD.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repo;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _repo = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categoryList = await _repo.GetCategories(
                selector: x => new GetCategoryVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Descripiton = x.Description,
                    Slug = x.Slug
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name));

            return Ok(categoryList);
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _repo.GetCategory(id);

            return Ok(category);
        }

        [HttpGet("{slug}", Name = "GetCategoryBySlug")]
        public async Task<IActionResult> GetCategoryBySlug(string slug)
        {
            var category = await _repo.GetCategory(slug);

            return Ok(category);
        }

        //Not: Post, Put, Patch, Delete gibi request'lerde gönderilne request'lerin header kısımlarında "Content-Type: application/json" bilgisi eklenilmelidir. Aksi taktirde media type error yaşanmaktadır. Bu uygulamada Post, Put yada Patch ve Delete olmaz üzere 3 tane content-type geçilmeis gereken action method bulunmaktadır. Isterseniz tek tek yazabilirisniz isterseniz bütün bir controllera bu attributte kazandırabilirsiniz. Şayet bazı action methodlar eski teknoloji olan XML kullanabilirler. BU senaryoda action method bazında ilerlemek daha doğrudur.

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO model)
        {
            //Create edilecek varlığın verisini 2 yolla post edebiliriz. Bunlardan biricisi "[FromBody]" atacağımız request'in gövdesinde veriti göndereceğimiz zaman tercih ediyoruz. İkincisi "[FromUri]" bu yaklaşımda browser'ın addres çubuğuna post etmek istediğimizi varlığın property name'lerini ve taşıayacağı değeri elle giriyoruz. Zahmetli bir iş olduğundan bir DTO söz konuus olduğundan biz burada FromBody kullandık. Lakin "id, name yada slug gibi tek bir bilgi post edileceği zaman tercih edilebilir. Yada en kötü authentication işelminde username ve password bilgileri gönderilebillir. Ayrıca FromUri kullanmayışımızın diğer bir nedeni API yönetmek için 3 rd araçların bulunmasıdır. PostMan, Fiddler, Swagger gibi araçlar ile bu tarz işlemler FromBody attribute ile daha rahat handle edilmektedir.

            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return BadRequest(model); //Status code 400
                }

                var catergoryExsist = await _repo.CategoryExists(model.Slug); //şayet veri tabanında model'den gelen slug değerine ait bir category var ise true değilse false dönecek. 

                if (catergoryExsist == true) //şayet yukarıdaki sorgu sonucunda catergoryExsist boş değilse böyle bir category veri tabınında bulunmaktadır. Bu yüzden ekleme işlemi yapılamayacaktır.
                {
                    ModelState.AddModelError(string.Empty, "The category already exsist..!");
                    return StatusCode(404, ModelState);
                }

                if (!await _repo.Create(model))
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong when saving the record {model.Name}");
                    return StatusCode(500, ModelState);
                }

                return StatusCode(201); //Status code 201 created
            }

            return BadRequest();
        }

        [HttpPatch(Name = "UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCatgoryDTO model)
        {
            //HttpPut ve HttpPatch
            //Update operasyonumuzda "HttpPut" kullanırsak verinin bir collection halinde değil objenin kendisini gönderirlir. Yani "Put" kullanıldığında entity'nin tüm alanları gönderilmiş yada işleme dahil edilmiş olur. "Path" ise yanlızca değiştirilecek alanları gönderir.

            //HttpPut tüm cvarlığı değiştirirken Patch ise sadece değişiklik sağlana alanalrı güncelleyerek bunu yapmaktadır. Buda bize performans olarak döner. Çünkü vir varlığın tüm alanları içerisindeki değişikliği trace ederken bir maliyet çıkacaktır.

            if (model == null)
            {
                return BadRequest(model);
            }

            if (!await _repo.Update(model))
            {
                ModelState.AddModelError(string.Empty, $"Something went wrong editing record {model.Name}");
                return StatusCode(500, model);
            }

            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!await _repo.Delete(id))
            {
                ModelState.AddModelError(string.Empty, $"Something went wrong, category not found..!");
                return StatusCode(404, ModelState);
            }

            return Ok();
        }
    }
}
