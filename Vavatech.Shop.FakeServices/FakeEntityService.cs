using Bogus;
using System;
using System.Collections.Generic;
using Vavatech.Shop.IServices;
using Vavatech.Shop.Models;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Vavatech.Shop.FakeServices
{

    public class FakeOptions
    {
        public int Count { get; set; }
    }

    public class FakeEntityService<TEntity> : IEntityService<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly ICollection<TEntity> entities;

        private readonly FakeOptions options;


        public FakeEntityService(Faker<TEntity> faker, IOptions<FakeOptions> options)
        {
            this.options = options.Value;

            this.entities = faker.Generate(options.Value.Count);
        }

        public virtual void Add(TEntity entity)
        {
            var id = entities.Max(c => c.Id);
            entity.Id = ++id;

            entities.Add(entity);
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return entities;
        }

        public virtual TEntity Get(int id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public virtual void Remove(int id)
        {
            entities.Remove(Get(id));
        }

        public virtual void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
