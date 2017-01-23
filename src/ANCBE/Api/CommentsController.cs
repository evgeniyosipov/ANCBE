﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ANCBE.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ANCBE.Api
{
    [Route("api/posts/{postId:long}/comments")]
    public class CommentsController : Controller
    {
        private readonly BlogDataContext _db;

        public CommentsController(BlogDataContext db)
        {
            _db = db;
        }

        // GET: api/values
        [HttpGet]
        public IQueryable<Comment> Get(long postId)
        {
            return _db.Comments.Where(x => x.PostId == postId);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<Comment> Post(long postId, [FromBody]Comment comment)
        {
            comment.PostId = postId;
            comment.Author = User.Identity.Name;
            comment.Posted = DateTime.Now;

            _db.Comments.Add(comment);
            await _db.SaveChangesAsync();

            return comment;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
