using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Specialized;
using WebApplication.Models;
using WebApplication.Models.Services;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadDataController : ControllerBase
    {
        private readonly UploadDataService _uploadDataService;

        public UploadDataController(UploadDataService uploadDataService) =>_uploadDataService = uploadDataService;
        
        [HttpGet("{id:length(32)}", Name = "GetUploadData")]
        public ActionResult<UploadData> Get(Guid id)
        {
            var book = _uploadDataService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
        
        [HttpPost]
        public ActionResult<UploadData> Create(UploadData uploadData)
        {
            _uploadDataService.Add(uploadData);

            return CreatedAtRoute("GetUploadData", new { id = uploadData.Id.ToString() }, uploadData);
        }
        
        [HttpDelete("{id:length(32)}")]
        public IActionResult Delete(Guid id)
        {
            var book = _uploadDataService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _uploadDataService.Delete(book.Id);

            return NoContent();
        }
    }
}